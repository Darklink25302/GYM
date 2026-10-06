Option Strict On
Option Infer On

Public Class Usuario
    Public Property IdUsuario As Integer
    Public Property NombreUsuario As String = ""
    Public Property IdRol As Integer
    Public Property Rol As String = ""
    Public Property IdSocio As Integer?
    Public Property IdInstructor As Integer?
    Public Property Activo As Boolean
    Public Property Bloqueado As Boolean
    Public Property IntentosFallidos As Integer
End Class

Public Enum EstadoAutenticacion
    Exitoso
    Rechazado
    Bloqueado
End Enum

Public Class ResultadoAutenticacion
    Public ReadOnly Property Estado As EstadoAutenticacion
    Public ReadOnly Property Usuario As Usuario

    Public Sub New(estado As EstadoAutenticacion, Optional usuario As Usuario = Nothing)
        Me.Estado = estado
        Me.Usuario = usuario
    End Sub
End Class
