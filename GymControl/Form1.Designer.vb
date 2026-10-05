<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlContenido = New Panel()
        Me.lblTitulo = New Label()
        Me.lblUsuario = New Label()
        Me.txtUsuario = New TextBox()
        Me.lblContrasena = New Label()
        Me.txtContrasena = New TextBox()
        Me.btnEntrar = New Button()
        Me.btnPrimerAdministrador = New Button()
        Me.lblEstado = New Label()
        Me.pnlContenido.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Dock = DockStyle.Fill
        Me.pnlContenido.Controls.Add(Me.lblTitulo)
        Me.pnlContenido.Controls.Add(Me.lblUsuario)
        Me.pnlContenido.Controls.Add(Me.txtUsuario)
        Me.pnlContenido.Controls.Add(Me.lblContrasena)
        Me.pnlContenido.Controls.Add(Me.txtContrasena)
        Me.pnlContenido.Controls.Add(Me.btnEntrar)
        Me.pnlContenido.Controls.Add(Me.btnPrimerAdministrador)
        Me.pnlContenido.Controls.Add(Me.lblEstado)

        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Location = New Point(28, 28)
        Me.lblTitulo.Size = New Size(374, 32)
        Me.lblTitulo.Text = "Bienvenido a GymControl"
        Me.lblTitulo.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)

        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Location = New Point(28, 82)
        Me.lblUsuario.Size = New Size(374, 20)
        Me.lblUsuario.Text = "Usuario"

        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Location = New Point(28, 106)
        Me.txtUsuario.Size = New Size(374, 27)
        Me.txtUsuario.MaxLength = 50
        Me.txtUsuario.TabIndex = 0

        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Location = New Point(28, 147)
        Me.lblContrasena.Size = New Size(374, 20)
        Me.lblContrasena.Text = "Contraseña"

        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.Location = New Point(28, 171)
        Me.txtContrasena.Size = New Size(374, 27)
        Me.txtContrasena.UseSystemPasswordChar = True
        Me.txtContrasena.TabIndex = 1

        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Location = New Point(28, 217)
        Me.btnEntrar.Size = New Size(374, 36)
        Me.btnEntrar.Text = "Entrar"
        Me.btnEntrar.UseVisualStyleBackColor = True
        Me.btnEntrar.TabIndex = 2

        Me.btnPrimerAdministrador.Name = "btnPrimerAdministrador"
        Me.btnPrimerAdministrador.Location = New Point(28, 260)
        Me.btnPrimerAdministrador.Size = New Size(374, 36)
        Me.btnPrimerAdministrador.Text = "Crear primer administrador"
        Me.btnPrimerAdministrador.UseVisualStyleBackColor = True
        Me.btnPrimerAdministrador.Visible = False
        Me.btnPrimerAdministrador.TabIndex = 3

        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Location = New Point(28, 304)
        Me.lblEstado.Size = New Size(374, 60)
        Me.lblEstado.Text = ""
        Me.lblEstado.ForeColor = Color.Firebrick

        Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New Size(430, 370)
        Me.Font = New Font("Segoe UI", 10.0F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "GymControl - Iniciar sesión"
        Me.Controls.Add(Me.pnlContenido)
        Me.AcceptButton = Me.btnEntrar
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlContenido.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblContrasena As Label
    Friend WithEvents txtContrasena As TextBox
    Friend WithEvents btnEntrar As Button
    Friend WithEvents btnPrimerAdministrador As Button
    Friend WithEvents lblEstado As Label
End Class
