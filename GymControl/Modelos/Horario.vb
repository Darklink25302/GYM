Public Class Horario
    Public Property IdHorario As Integer
    Public Property IdInstructor As Integer
    Public Property IdActividad As Integer
    Public Property IdSala As Integer
    Public Property DiaSemana As String
    Public Property HoraInicio As TimeSpan
    Public Property HoraFin As TimeSpan
    Public Property Activo As Boolean = True

    ' Propiedades extra para mostrar en pantalla (no se guardan en BD)
    Public Property NombreInstructor As String
    Public Property NombreActividad As String
    Public Property NombreSala As String
End Class
