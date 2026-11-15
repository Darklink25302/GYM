Option Strict On

Imports MySqlConnector

Public Class PagoDAO
    Private Shared Sub ValidarPago(pago As Pago)
        If pago Is Nothing Then Throw New ArgumentException("Faltan los datos del pago.")
        If pago.Monto <= 0 OrElse Decimal.Round(pago.Monto, 2) <> pago.Monto Then Throw New ArgumentException("Escribe un monto positivo con un máximo de dos decimales.")
        If pago.MetodoPago <> "Efectivo" AndAlso pago.MetodoPago <> "Tarjeta" AndAlso pago.MetodoPago <> "Transferencia" Then
            Throw New ArgumentException("Selecciona el método de pago.")
        End If
    End Sub

    Private Shared Function GuardarPago(conexion As MySqlConnection, transaccion As MySqlTransaction, idMembresia As Integer, pago As Pago) As Integer
        Dim sql = "INSERT INTO pagos(id_membresia,fecha_pago,monto,metodo_pago,id_usuario) " &
                  "VALUES(@membresia,@fecha,@monto,@metodo,@usuario)"
        Using comando As New MySqlCommand(sql, conexion, transaccion)
            comando.Parameters.AddWithValue("@membresia", idMembresia)
            comando.Parameters.AddWithValue("@fecha", Date.Now)
            comando.Parameters.AddWithValue("@monto", pago.Monto)
            comando.Parameters.AddWithValue("@metodo", pago.MetodoPago)
            comando.Parameters.AddWithValue("@usuario", Sesion.IdUsuario)
            comando.ExecuteNonQuery()
            Return CInt(comando.LastInsertedId)
        End Using
    End Function

    Public Function Insertar(pago As Pago) As Boolean
        Return InsertarYObtenerId(pago) > 0
    End Function

    Public Function InsertarYObtenerId(pago As Pago) As Integer
        PermisosMenu.Exigir("Administrador", "Recepcionista")
        ValidarPago(pago)
        If pago.IdMembresia <= 0 Then Throw New ArgumentException("Selecciona una membresía.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                Dim precio As Decimal
                Using comando As New MySqlCommand("SELECT total FROM membresias WHERE id_membresia=@id FOR UPDATE", conexion, transaccion)
                    comando.Parameters.AddWithValue("@id", pago.IdMembresia)
                    Dim valor = comando.ExecuteScalar()
                    If valor Is Nothing Then Throw New ArgumentException("La membresía ya no existe.")
                    precio = Convert.ToDecimal(valor)
                End Using
                Dim pagado As Decimal
                Using comando As New MySqlCommand("SELECT COALESCE(SUM(monto),0) FROM pagos WHERE id_membresia=@id AND anulado=FALSE", conexion, transaccion)
                    comando.Parameters.AddWithValue("@id", pago.IdMembresia)
                    pagado = Convert.ToDecimal(comando.ExecuteScalar())
                End Using
                If pago.Monto > precio - pagado Then Throw New ArgumentException("El pago supera el saldo pendiente actual.")
                Dim id = GuardarPago(conexion, transaccion, pago.IdMembresia, pago)
                transaccion.Commit()
                pago.IdPago = id
                Return id
            End Using
        End Using
    End Function

    Public Function RegistrarMembresia(membresia As Membresia, primerPago As Pago) As Integer
        PermisosMenu.Exigir("Administrador", "Recepcionista")
        If membresia Is Nothing OrElse primerPago Is Nothing Then Throw New ArgumentException("Faltan los datos de la membresía.")
        If membresia.IdSocio <= 0 OrElse membresia.IdTipoMembresia <= 0 Then Throw New ArgumentException("Selecciona un socio y un plan.")
        If membresia.Total < 0 OrElse Decimal.Round(membresia.Total, 2) <> membresia.Total Then Throw New ArgumentException("El precio no es válido.")
        If membresia.FechaInicio.Date > membresia.FechaVencimiento.Date Then Throw New ArgumentException("Revisa el período de la membresía.")
        If primerPago.Monto < 0 OrElse primerPago.Monto > membresia.Total Then Throw New ArgumentException("El primer abono supera el precio pactado o es negativo.")
        If primerPago.Monto > 0 Then ValidarPago(primerPago)
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction()
                Dim id As Integer
                Using comando As New MySqlCommand("INSERT INTO membresias(id_socio,id_tipo_membresia,fecha_inicio,fecha_vencimiento,total,estado) " &
                                                  "VALUES(@socio,@tipo,@inicio,@vence,@total,'Activa')", conexion, transaccion)
                    comando.Parameters.AddWithValue("@socio", membresia.IdSocio)
                    comando.Parameters.AddWithValue("@tipo", membresia.IdTipoMembresia)
                    comando.Parameters.AddWithValue("@inicio", membresia.FechaInicio.Date)
                    comando.Parameters.AddWithValue("@vence", membresia.FechaVencimiento.Date)
                    comando.Parameters.AddWithValue("@total", membresia.Total)
                    comando.ExecuteNonQuery()
                    id = CInt(comando.LastInsertedId)
                End Using
                Dim idPago As Integer = 0
                If primerPago.Monto > 0 Then idPago = GuardarPago(conexion, transaccion, id, primerPago)
                transaccion.Commit()
                primerPago.IdMembresia = id
                primerPago.IdPago = idPago
                Return id
            End Using
        End Using
    End Function

    Public Function ConsultarPorMembresia(idMembresia As Integer) As DataTable
        PermisosMenu.Exigir("Administrador", "Recepcionista")
        Dim tabla As New DataTable()
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand("SELECT p.id_pago,p.fecha_pago,p.metodo_pago,p.monto,p.anulado," &
                                              "COALESCE(u.nombre_usuario,'Sin registrar') AS usuario FROM pagos p " &
                                              "LEFT JOIN usuarios u ON u.id_usuario=p.id_usuario WHERE p.id_membresia=@id ORDER BY p.fecha_pago DESC,p.id_pago DESC", conexion)
                comando.Parameters.AddWithValue("@id", idMembresia)
                Using lector = comando.ExecuteReader()
                    tabla.Load(lector)
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Sub Anular(idPago As Integer, motivo As String)
        PermisosMenu.Exigir("Administrador", "Recepcionista")
        If idPago <= 0 OrElse String.IsNullOrWhiteSpace(motivo) OrElse motivo.Trim().Length > 255 Then
            Throw New ArgumentException("Selecciona un pago y escribe el motivo de anulación (máximo 255 caracteres).")
        End If
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                Dim idMembresia As Integer
                Using comando As New MySqlCommand("SELECT id_membresia FROM pagos WHERE id_pago=@id", conexion, transaccion)
                    comando.Parameters.AddWithValue("@id", idPago)
                    Dim valor = comando.ExecuteScalar()
                    If valor Is Nothing Then Throw New ArgumentException("El pago ya no existe.")
                    idMembresia = Convert.ToInt32(valor)
                End Using
                Using comando As New MySqlCommand("SELECT id_membresia FROM membresias WHERE id_membresia=@id FOR UPDATE", conexion, transaccion)
                    comando.Parameters.AddWithValue("@id", idMembresia)
                    comando.ExecuteScalar()
                End Using
                Using comando As New MySqlCommand("UPDATE pagos SET anulado=TRUE,motivo_anulacion=@motivo,fecha_anulacion=NOW(),id_usuario_anula=@usuario " &
                                                  "WHERE id_pago=@id AND anulado=FALSE", conexion, transaccion)
                    comando.Parameters.AddWithValue("@id", idPago)
                    comando.Parameters.AddWithValue("@motivo", motivo.Trim())
                    comando.Parameters.AddWithValue("@usuario", Sesion.IdUsuario)
                    If comando.ExecuteNonQuery() <> 1 Then Throw New ArgumentException("El pago ya estaba anulado.")
                End Using
                transaccion.Commit()
            End Using
        End Using
    End Sub
End Class
