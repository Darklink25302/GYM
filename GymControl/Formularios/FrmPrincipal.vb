Option Strict On
Option Infer On

Public Class FrmPrincipal
    Public Sub New()
        InitializeComponent()

        If Sesion.IdUsuario <= 0 Then
            Throw New InvalidOperationException("Debes iniciar sesión para abrir esta ventana.")
        End If

        lblBienvenida.Text = "Bienvenido, " & Sesion.NombreUsuario
        lblRol.Text = "Rol: " & Sesion.Rol
    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Close()
    End Sub

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Sesion.Limpiar()
    End Sub
End Class
