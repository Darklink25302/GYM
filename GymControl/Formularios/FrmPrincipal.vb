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
        btnUsuarios.Visible = Sesion.Rol = "Administrador"
    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click
        Try
            Using formulario As New FrmUsuarios()
                formulario.ShowDialog(Me)
            End Using
            lblBienvenida.Text = "Bienvenido, " & Sesion.NombreUsuario
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(ex.Message, "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Close()
    End Sub

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Sesion.Limpiar()
    End Sub
End Class
