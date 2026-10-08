Public Module PermisosPersona4

    Public Sub ExigirAdministrador()
        If Sesion.IdUsuario <= 0 OrElse Sesion.Rol <> "Administrador" Then
            Throw New UnauthorizedAccessException("Solo el Administrador puede gestionar este módulo.")
        End If
    End Sub

    Public Sub ExigirConsultaHorarios()
        If Sesion.IdUsuario <= 0 Then
            Throw New UnauthorizedAccessException("Debes iniciar sesión.")
        End If
        If Sesion.Rol = "Administrador" Then Return
        If Sesion.Rol <> "Instructor" Then
            Throw New UnauthorizedAccessException("Tu rol no tiene permiso para consultar horarios.")
        End If
        If Not Sesion.IdInstructor.HasValue OrElse Sesion.IdInstructor.Value <= 0 Then
            Throw New UnauthorizedAccessException("Un Administrador debe vincular tu cuenta con un instructor.")
        End If
    End Sub

End Module
