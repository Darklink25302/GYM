<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrincipal
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
        Me.lblBienvenida = New Label()
        Me.lblRol = New Label()
        Me.btnCerrarSesion = New Button()
        Me.btnUsuarios = New Button()
        Me.pnlContenido.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Dock = DockStyle.Fill
        Me.pnlContenido.Controls.Add(Me.lblBienvenida)
        Me.pnlContenido.Controls.Add(Me.lblRol)
        Me.pnlContenido.Controls.Add(Me.btnCerrarSesion)
        Me.pnlContenido.Controls.Add(Me.btnUsuarios)

        Me.lblBienvenida.Name = "lblBienvenida"
        Me.lblBienvenida.Location = New Point(32, 32)
        Me.lblBienvenida.Size = New Size(496, 64)
        Me.lblBienvenida.Text = ""
        Me.lblBienvenida.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)

        Me.lblRol.Name = "lblRol"
        Me.lblRol.Location = New Point(32, 108)
        Me.lblRol.Size = New Size(496, 24)
        Me.lblRol.Text = ""

        Me.btnCerrarSesion.Name = "btnCerrarSesion"
        Me.btnCerrarSesion.Location = New Point(32, 211)
        Me.btnCerrarSesion.Size = New Size(140, 36)
        Me.btnCerrarSesion.Text = "Cerrar sesión"
        Me.btnCerrarSesion.UseVisualStyleBackColor = True
        Me.btnCerrarSesion.TabIndex = 0

        Me.btnUsuarios.Name = "btnUsuarios"
        Me.btnUsuarios.Location = New Point(32, 157)
        Me.btnUsuarios.Size = New Size(240, 36)
        Me.btnUsuarios.Text = "Administrar usuarios"
        Me.btnUsuarios.UseVisualStyleBackColor = True
        Me.btnUsuarios.Visible = False
        Me.btnUsuarios.TabIndex = 0

        Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New Size(560, 290)
        Me.Font = New Font("Segoe UI", 10.0F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "GymControl"
        Me.Controls.Add(Me.pnlContenido)

        Me.pnlContenido.ResumeLayout(False)
        Me.pnlContenido.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblRol As Label
    Friend WithEvents btnCerrarSesion As Button
    Friend WithEvents btnUsuarios As Button
End Class
