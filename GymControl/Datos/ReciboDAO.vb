Option Strict On
Option Infer On

Imports MySqlConnector

Public Class ReciboDAO
    Public Shared Function ListarPagos(desde As Date, hasta As Date, buscar As String, Optional idMembresia As Integer = 0) As DataTable
        PermisosReportes.Exigir("R01")
        If desde.Date > hasta.Date Then Throw New ArgumentException("La fecha Desde no puede ser posterior a Hasta.")
        If idMembresia < 0 Then Throw New ArgumentException("La membresía indicada no es válida.")
        Dim tabla As New DataTable()
        Using conexion = ConexionBD.CrearConexionReportes()
            conexion.Open()
            Dim sql = "SELECT id_pago, fecha, socio, cedula, plan, metodo_pago, monto, anulado " &
                      "FROM vw_ingresos_detalle WHERE fecha >= @desde AND fecha < @hasta " &
                      "AND (@membresia = 0 OR id_membresia = @membresia) " &
                      "AND (socio LIKE @buscar OR cedula LIKE @buscar) ORDER BY fecha DESC, id_pago DESC"
            Using comando As New MySqlCommand(sql, conexion)
                comando.Parameters.AddWithValue("@desde", desde.Date)
                comando.Parameters.AddWithValue("@hasta", hasta.Date.AddDays(1))
                comando.Parameters.AddWithValue("@membresia", idMembresia)
                comando.Parameters.AddWithValue("@buscar", "%" & buscar.Trim() & "%")
                Using lector = comando.ExecuteReader()
                    tabla.Load(lector)
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Shared Function Obtener(idPago As Integer) As DatosRecibo
        PermisosReportes.Exigir("R01")
        If idPago <= 0 Then Throw New ArgumentException("Selecciona un pago registrado.")
        Using conexion = ConexionBD.CrearConexionReportes()
            conexion.Open()
            ' El saldo corresponde a los pagos válidos hasta la fecha y el número del recibo.
            Dim sql = "SELECT i.id_pago, i.id_membresia, i.fecha, i.socio, i.cedula, i.plan, " &
                      "i.metodo_pago, i.monto, i.anulado, i.usuario, s.inicio, s.vence, " &
                      "s.precio_pactado - COALESCE((SELECT SUM(p.monto) FROM vw_ingresos_detalle p " &
                      "WHERE p.id_membresia = i.id_membresia AND p.anulado = FALSE " &
                      "AND (p.fecha < i.fecha OR (p.fecha = i.fecha AND p.id_pago <= i.id_pago))), 0) AS saldo " &
                      "FROM vw_ingresos_detalle i INNER JOIN vw_saldos_membresia s " &
                      "ON s.id_membresia = i.id_membresia WHERE i.id_pago = @id"
            Using comando As New MySqlCommand(sql, conexion)
                comando.Parameters.AddWithValue("@id", idPago)
                Using lector = comando.ExecuteReader()
                    If Not lector.Read() Then Throw New ArgumentException("El pago seleccionado ya no está disponible.")
                    Return New DatosRecibo With {
                        .IdPago = lector.GetInt32("id_pago"),
                        .IdMembresia = lector.GetInt32("id_membresia"),
                        .Fecha = lector.GetDateTime("fecha"),
                        .Socio = lector.GetString("socio"),
                        .Cedula = lector.GetString("cedula"),
                        .Plan = lector.GetString("plan"),
                        .Inicio = lector.GetDateTime("inicio"),
                        .Vence = lector.GetDateTime("vence"),
                        .Monto = lector.GetDecimal("monto"),
                        .Metodo = lector.GetString("metodo_pago"),
                        .Saldo = lector.GetDecimal("saldo"),
                        .Anulado = lector.GetBoolean("anulado"),
                        .Usuario = lector.GetString("usuario")
                    }
                End Using
            End Using
        End Using
    End Function
End Class
