Option Strict On
Option Infer On

Imports MySqlConnector

Public Class PermisosMenu
    Public Shared Sub Exigir(ParamArray rolesPermitidos As String())
        If Sesion.IdUsuario <= 0 Then
            Throw New UnauthorizedAccessException("Debes iniciar sesión.")
        End If

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Dim sql = "SELECT u.nombre_usuario, r.nombre AS rol, u.id_socio, u.id_instructor " &
                      "FROM usuarios u INNER JOIN roles r ON r.id_rol = u.id_rol " &
                      "WHERE u.id_usuario = @id AND u.activo = TRUE AND u.bloqueado = FALSE"
            Using comando As New MySqlCommand(sql, conexion)
                comando.Parameters.AddWithValue("@id", Sesion.IdUsuario)
                Using lector = comando.ExecuteReader()
                    If Not lector.Read() Then
                        Sesion.Limpiar()
                        Throw New UnauthorizedAccessException("Tu cuenta ya no está habilitada. Inicia sesión de nuevo.")
                    End If
                    Dim rolActual = lector.GetString("rol")
                    If rolActual <> Sesion.Rol Then
                        Sesion.Limpiar()
                        Throw New UnauthorizedAccessException("Tu rol cambió. Inicia sesión de nuevo.")
                    End If
                    Dim permitido = False
                    For Each rol In rolesPermitidos
                        If rolActual = rol Then permitido = True
                    Next
                    If Not permitido Then
                        Throw New UnauthorizedAccessException("Tu rol no tiene permiso para abrir este módulo.")
                    End If
                    Sesion.NombreUsuario = lector.GetString("nombre_usuario")
                    Sesion.IdSocio = Nothing
                    Sesion.IdInstructor = Nothing
                    If Not lector.IsDBNull(lector.GetOrdinal("id_socio")) Then
                        Sesion.IdSocio = lector.GetInt32("id_socio")
                    End If
                    If Not lector.IsDBNull(lector.GetOrdinal("id_instructor")) Then
                        Sesion.IdInstructor = lector.GetInt32("id_instructor")
                    End If
                End Using
            End Using
        End Using
    End Sub
End Class
