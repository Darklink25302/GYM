Option Strict On
Option Infer On

Imports System.Threading.Tasks

Public Class FrmLogin
    Private procesando As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Async Sub FrmLogin_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Sesion.Limpiar()
        EstablecerOcupado(True)
        Try
            btnPrimerAdministrador.Visible = Await Task.Run(Function() UsuarioDAO.NecesitaAdministradorInicial())
            If btnPrimerAdministrador.Visible Then
                lblEstado.Text = "La base todavía no tiene usuarios. Crea el primer administrador."
            End If
        Catch ex As Exception
            lblEstado.Text = "No se pudo consultar la base de datos. Comprueba la conexión y las tablas de seguridad."
        Finally
            EstablecerOcupado(False)
            txtUsuario.Select()
        End Try
    End Sub

    Private Async Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        If procesando Then Return
        Dim nombre = txtUsuario.Text.Trim()
        Dim clave = txtContrasena.Text
        If nombre.Length = 0 OrElse clave.Length = 0 Then
            lblEstado.Text = "Escribe tu usuario y contraseña."
            Return
        End If

        Sesion.Limpiar()
        lblEstado.Text = "Verificando acceso…"
        EstablecerOcupado(True)
        Dim resultado As ResultadoAutenticacion = Nothing
        Try
            resultado = Await Task.Run(Function() UsuarioDAO.Autenticar(nombre, clave))
        Catch ex As Exception
            lblEstado.Text = "No se pudo completar el acceso. Comprueba la conexión con MariaDB."
        Finally
            txtContrasena.Clear()
            EstablecerOcupado(False)
        End Try
        If resultado Is Nothing Then Return

        Select Case resultado.Estado
            Case EstadoAutenticacion.Exitoso
                Dim usuario = resultado.Usuario
                ' Guarda el usuario que inició sesión.
                Sesion.IdUsuario = usuario.IdUsuario
                Sesion.NombreUsuario = usuario.NombreUsuario
                Sesion.Rol = usuario.Rol
                Sesion.IdSocio = usuario.IdSocio
                Sesion.IdInstructor = usuario.IdInstructor
                Hide()
                Try
                    Using principal As New FrmPrincipal()
                        principal.ShowDialog(Me)
                    End Using
                Finally
                    Sesion.Limpiar()
                    txtUsuario.Clear()
                    lblEstado.Text = "Sesión cerrada."
                    Show()
                    txtUsuario.Select()
                End Try
            Case Else
                lblEstado.Text = "No se pudo iniciar sesión. Revisa tus datos o consulta al administrador."
                txtContrasena.Select()
        End Select
    End Sub

    Private Sub btnPrimerAdministrador_Click(sender As Object, e As EventArgs) Handles btnPrimerAdministrador.Click
        If procesando Then Return
        Using formulario As New FrmPrimerAdministrador()
            If formulario.ShowDialog(Me) = DialogResult.OK Then
                btnPrimerAdministrador.Visible = False
                txtUsuario.Text = formulario.NombreCreado
                lblEstado.Text = "Administrador creado. Escribe su contraseña para entrar."
                txtContrasena.Select()
            End If
        End Using
    End Sub

    ' Desactiva los controles mientras se consulta la base.
    Private Sub EstablecerOcupado(ocupado As Boolean)
        procesando = ocupado
        UseWaitCursor = ocupado
        txtUsuario.Enabled = Not ocupado
        txtContrasena.Enabled = Not ocupado
        btnEntrar.Enabled = Not ocupado
        btnPrimerAdministrador.Enabled = Not ocupado
    End Sub

    Private Sub FrmLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If procesando Then
            e.Cancel = True
        Else
            Sesion.Limpiar()
        End If
    End Sub
End Class
