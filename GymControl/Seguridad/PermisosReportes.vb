Option Strict On
Option Infer On

Public Class PermisosReportes
    Public Shared Sub ExigirCentro()
        PermisosMenu.Exigir("Administrador", "Recepcionista", "Instructor", "Socio")
    End Sub

    Public Shared Function PuedeVer(codigo As String, rol As String) As Boolean
        Select Case codigo
            Case "R01", "R02", "R03"
                Return rol = "Administrador" OrElse rol = "Recepcionista"
            Case "R04", "R07", "R08", "R09"
                Return rol = "Administrador"
            Case "R05"
                Return rol = "Administrador" OrElse rol = "Recepcionista" OrElse rol = "Socio"
            Case "R06"
                Return rol = "Administrador" OrElse rol = "Recepcionista" OrElse rol = "Instructor" OrElse rol = "Socio"
            Case Else
                Return False
        End Select
    End Function

    Public Shared Sub Exigir(codigo As String)
        Select Case codigo
            Case "R01", "R02", "R03"
                PermisosMenu.Exigir("Administrador", "Recepcionista")
            Case "R04", "R07", "R08", "R09"
                PermisosMenu.Exigir("Administrador")
            Case "R05"
                PermisosMenu.Exigir("Administrador", "Recepcionista", "Socio")
                If Sesion.Rol = "Socio" AndAlso (Not Sesion.IdSocio.HasValue OrElse Sesion.IdSocio.Value <= 0) Then
                    Throw New ArgumentException("Un administrador debe vincular tu cuenta con un socio.")
                End If
            Case "R06"
                PermisosMenu.Exigir("Administrador", "Recepcionista", "Instructor", "Socio")
                If Sesion.Rol = "Instructor" AndAlso (Not Sesion.IdInstructor.HasValue OrElse Sesion.IdInstructor.Value <= 0) Then
                    Throw New ArgumentException("Un administrador debe vincular tu cuenta con un instructor.")
                End If
            Case Else
                Throw New ArgumentException("El reporte indicado no existe.")
        End Select
    End Sub

    Public Shared Function ObtenerSocioPermitido(idSolicitado As Integer) As Integer
        Exigir("R05")
        If Sesion.Rol = "Socio" Then
            If idSolicitado <> 0 AndAlso idSolicitado <> Sesion.IdSocio.Value Then
                Throw New UnauthorizedAccessException("Solo puedes consultar tu propio estado de cuenta.")
            End If
            Return Sesion.IdSocio.Value
        End If
        If idSolicitado <= 0 Then Throw New ArgumentException("Selecciona un socio.")
        Return idSolicitado
    End Function

    Public Shared Function ObtenerInstructorPermitido(idSolicitado As Integer) As Integer
        Exigir("R06")
        If Sesion.Rol = "Instructor" Then
            If idSolicitado <> 0 AndAlso idSolicitado <> Sesion.IdInstructor.Value Then
                Throw New UnauthorizedAccessException("Solo puedes consultar tus propios horarios.")
            End If
            Return Sesion.IdInstructor.Value
        End If
        If idSolicitado < 0 Then Throw New ArgumentException("El instructor indicado no es válido.")
        Return idSolicitado
    End Function
End Class
