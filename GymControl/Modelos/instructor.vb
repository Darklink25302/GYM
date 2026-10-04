Public Class Instructor
    ' Campos principales del Instructor
    Public Property IdInstructor As Integer
    Public Property Nombre As String
    Public Property Apellido As String
    Public Property Especialidad As String
    Public Property Telefono As String
    Public Property Email As String
    Public Property Activo As Boolean

    ' Propiedad calculada para mostrar el nombre completo en pantalla
    Public ReadOnly Property NombreCompleto As String
        Get
            Return $"{Nombre} {Apellido}".Trim()
        End Get
    End Property

    ' Constructor vacío (necesario para crear objetos desde el formulario)
    Public Sub New()
        Activo = True ' Por defecto, un instructor nuevo está activo
    End Sub
End Class
