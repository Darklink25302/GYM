Option Strict On
Option Infer On

Imports System.Threading.Tasks

Public Class FrmPrimerAdministrador

    Private procesando As Boolean

    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property NombreCreado As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Async Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        If procesando Then Return
        Dim nombre = txtUsuario.Text.Trim()
        Dim clave = txtContrasena.Text
        If nombre.Length = 0 Then
            lblEstado.Text = "Escribe un nombre de usuario."
            Return
        End If
        If clave.Length < 12 Then
            lblEstado.Text = "La contraseña debe tener al menos 12 caracteres."
            Return
        End If
        If Not String.Equals(clave, txtConfirmacion.Text, StringComparison.Ordinal) Then
            lblEstado.Text = "Las contraseñas no coinciden."
            Return
        End If

        EstablecerOcupado(True)
        lblEstado.Text = "Creando administrador…"
        Dim creado As Boolean = False
        Try
            Await Task.Run(Function() UsuarioDAO.CrearPrimerAdministrador(nombre, clave))
            NombreCreado = nombre
            creado = True
        Catch ex As ArgumentException
            lblEstado.Text = ex.Message
        Catch ex As InvalidOperationException
            lblEstado.Text = ex.Message
        Catch ex As Exception
            lblEstado.Text = "No se pudo crear el administrador. Comprueba la conexión, las tablas y los permisos del usuario de la aplicación."
        Finally
            txtContrasena.Clear()
            txtConfirmacion.Clear()
            EstablecerOcupado(False)
        End Try
        If creado Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub EstablecerOcupado(ocupado As Boolean)
        procesando = ocupado
        UseWaitCursor = ocupado
        txtUsuario.Enabled = Not ocupado
        txtContrasena.Enabled = Not ocupado
        txtConfirmacion.Enabled = Not ocupado
        btnCrear.Enabled = Not ocupado
        btnCancelar.Enabled = Not ocupado
    End Sub

    Private Sub FrmPrimerAdministrador_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If procesando Then e.Cancel = True
    End Sub
End Class
