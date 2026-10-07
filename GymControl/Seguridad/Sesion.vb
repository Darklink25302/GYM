Public NotInheritable Class Sesion

    Private Sub New()
    End Sub

    Public Shared Property IdUsuario As Integer
    Public Shared Property NombreUsuario As String = ""
    Public Shared Property Rol As String = ""

    Public Shared Property IdSocio As Integer?
    Public Shared Property IdInstructor As Integer?

    Public Shared Sub Limpiar()
        IdUsuario = 0
        NombreUsuario = ""
        Rol = ""

        IdSocio = Nothing
        IdInstructor = Nothing
    End Sub

End Class