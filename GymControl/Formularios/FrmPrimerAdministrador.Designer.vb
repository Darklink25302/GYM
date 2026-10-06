<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrimerAdministrador
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
        Me.lblIndicacion = New Label()
        Me.lblUsuario = New Label()
        Me.txtUsuario = New TextBox()
        Me.lblContrasena = New Label()
        Me.txtContrasena = New TextBox()
        Me.lblConfirmacion = New Label()
        Me.txtConfirmacion = New TextBox()
        Me.btnCrear = New Button()
        Me.btnCancelar = New Button()
        Me.lblEstado = New Label()
        Me.pnlContenido.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Dock = DockStyle.Fill
        Me.pnlContenido.Controls.Add(Me.lblTitulo)
        Me.pnlContenido.Controls.Add(Me.lblIndicacion)
        Me.pnlContenido.Controls.Add(Me.lblUsuario)
        Me.pnlContenido.Controls.Add(Me.txtUsuario)
        Me.pnlContenido.Controls.Add(Me.lblContrasena)
        Me.pnlContenido.Controls.Add(Me.txtContrasena)
        Me.pnlContenido.Controls.Add(Me.lblConfirmacion)
        Me.pnlContenido.Controls.Add(Me.txtConfirmacion)
        Me.pnlContenido.Controls.Add(Me.btnCrear)
        Me.pnlContenido.Controls.Add(Me.btnCancelar)
        Me.pnlContenido.Controls.Add(Me.lblEstado)

        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Location = New Point(28, 24)
        Me.lblTitulo.Size = New Size(384, 28)
        Me.lblTitulo.Text = "Crear primer administrador"
        Me.lblTitulo.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)

        Me.lblIndicacion.Name = "lblIndicacion"
        Me.lblIndicacion.Location = New Point(28, 58)
        Me.lblIndicacion.Size = New Size(384, 40)
        Me.lblIndicacion.Text = "Usa una contraseña de al menos 12 caracteres."

        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Location = New Point(28, 103)
        Me.lblUsuario.Size = New Size(384, 20)
        Me.lblUsuario.Text = "Usuario"

        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Location = New Point(28, 124)
        Me.txtUsuario.Size = New Size(384, 27)
        Me.txtUsuario.MaxLength = 50
        Me.txtUsuario.TabIndex = 0

        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Location = New Point(28, 164)
        Me.lblContrasena.Size = New Size(384, 20)
        Me.lblContrasena.Text = "Contraseña"

        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.Location = New Point(28, 186)
        Me.txtContrasena.Size = New Size(384, 27)
        Me.txtContrasena.UseSystemPasswordChar = True
        Me.txtContrasena.TabIndex = 1

        Me.lblConfirmacion.Name = "lblConfirmacion"
        Me.lblConfirmacion.Location = New Point(28, 225)
        Me.lblConfirmacion.Size = New Size(384, 20)
        Me.lblConfirmacion.Text = "Confirmar contraseña"

        Me.txtConfirmacion.Name = "txtConfirmacion"
        Me.txtConfirmacion.Location = New Point(28, 247)
        Me.txtConfirmacion.Size = New Size(384, 27)
        Me.txtConfirmacion.UseSystemPasswordChar = True
        Me.txtConfirmacion.TabIndex = 2

        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Location = New Point(28, 294)
        Me.btnCrear.Size = New Size(384, 36)
        Me.btnCrear.Text = "Crear administrador"
        Me.btnCrear.UseVisualStyleBackColor = True
        Me.btnCrear.TabIndex = 3

        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Location = New Point(28, 336)
        Me.btnCancelar.Size = New Size(384, 36)
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        Me.btnCancelar.DialogResult = DialogResult.Cancel
        Me.btnCancelar.TabIndex = 4

        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Location = New Point(28, 382)
        Me.lblEstado.Size = New Size(384, 70)
        Me.lblEstado.Text = ""
        Me.lblEstado.ForeColor = Color.Firebrick

        Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New Size(440, 460)
        Me.Font = New Font("Segoe UI", 10.0F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "GymControl - Primer administrador"
        Me.Controls.Add(Me.pnlContenido)
        Me.AcceptButton = Me.btnCrear
        Me.CancelButton = Me.btnCancelar
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlContenido.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblIndicacion As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblContrasena As Label
    Friend WithEvents txtContrasena As TextBox
    Friend WithEvents lblConfirmacion As Label
    Friend WithEvents txtConfirmacion As TextBox
    Friend WithEvents btnCrear As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents lblEstado As Label
End Class
