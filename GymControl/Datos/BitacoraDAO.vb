Option Strict On
Option Infer On

Imports MySqlConnector

Public NotInheritable Class BitacoraDAO
    Private Sub New()
    End Sub

    ' Guarda el intento junto con los cambios del usuario.
    Friend Shared Sub Registrar(
        conexion As MySqlConnection,
        transaccion As MySqlTransaction,
        idUsuario As Integer?,
        nombreUsuario As String,
        resultado As String,
        detalle As String
    )
        If resultado <> "EXITOSO" AndAlso resultado <> "FALLIDO" AndAlso
           resultado <> "BLOQUEADO" Then
            Throw New ArgumentException("Resultado de acceso no valido.")
        End If

        Dim nombre = If(nombreUsuario, "")
        If nombre.Length > 50 Then nombre = nombre.Substring(0, 50)
        Using comando As New MySqlCommand(
            "INSERT INTO bitacora_accesos " &
            "(id_usuario, nombre_usuario_intentado, resultado, detalle) " &
            "VALUES (@id, @nombre, @resultado, @detalle)", conexion, transaccion)
            If idUsuario.HasValue Then
                comando.Parameters.AddWithValue("@id", idUsuario.Value)
            Else
                comando.Parameters.AddWithValue("@id", DBNull.Value)
            End If
            comando.Parameters.AddWithValue("@nombre", nombre)
            comando.Parameters.AddWithValue("@resultado", resultado)
            comando.Parameters.AddWithValue("@detalle", detalle)
            comando.ExecuteNonQuery()
        End Using
    End Sub
End Class
