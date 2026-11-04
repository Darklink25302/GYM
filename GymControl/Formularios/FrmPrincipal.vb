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

    Private Sub btnVerificarPagos_Click(sender As Object, e As EventArgs) Handles btnVerificarPagos.Click

        Try
            Dim dao As New PagoDAO()
            Dim columnas As String = dao.VerificarColumnasAnulacion()

            MessageBox.Show(
            columnas,
            "Columnas de la tabla pagos"
        )

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try

    End Sub

End Class
