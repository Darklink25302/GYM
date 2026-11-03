Option Strict On

Imports MySqlConnector

Public Class ConstanciaDAO
    Public Shared Function Obtener(idMembresia As Integer) As DatosConstancia
        PermisosReportes.Exigir("R02")
        If idMembresia <= 0 Then Throw New ArgumentException("Selecciona una membresía.")
        Using conexion = ConexionBD.CrearConexionReportes()
            conexion.Open()
            Dim sql = "SELECT id_membresia, socio, cedula, plan, inicio, vence, estado, " &
                      "precio_pactado, pagado, saldo FROM vw_saldos_membresia " &
                      "WHERE id_membresia = @id AND estado = 'Activa' " &
                      "AND inicio <= CURRENT_DATE() AND vence >= CURRENT_DATE()"
            Using comando As New MySqlCommand(sql, conexion)
                comando.Parameters.AddWithValue("@id", idMembresia)
                Using lector = comando.ExecuteReader()
                    If Not lector.Read() Then
                        Throw New ArgumentException("La membresía no existe o no está vigente. Revisa su estado y sus fechas.")
                    End If
                    Return New DatosConstancia With {
                        .IdMembresia = lector.GetInt32("id_membresia"),
                        .Socio = lector.GetString("socio"),
                        .Cedula = lector.GetString("cedula"),
                        .Plan = lector.GetString("plan"),
                        .Inicio = lector.GetDateTime("inicio"),
                        .Vence = lector.GetDateTime("vence"),
                        .Estado = lector.GetString("estado"),
                        .Precio = lector.GetDecimal("precio_pactado"),
                        .Pagado = lector.GetDecimal("pagado"),
                        .Saldo = lector.GetDecimal("saldo")
                    }
                End Using
            End Using
        End Using
    End Function
End Class
