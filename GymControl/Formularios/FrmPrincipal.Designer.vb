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
        pnlContenido = New Panel()
        lblBienvenida = New Label()
        lblRol = New Label()
        btnCerrarSesion = New Button()
        btnVerificarPagos = New Button()
        pnlContenido.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlContenido
        ' 
        pnlContenido.Controls.Add(btnVerificarPagos)
        pnlContenido.Controls.Add(lblBienvenida)
        pnlContenido.Controls.Add(lblRol)
        pnlContenido.Controls.Add(btnCerrarSesion)
        pnlContenido.Dock = DockStyle.Fill
        pnlContenido.Location = New Point(0, 0)
        pnlContenido.Name = "pnlContenido"
        pnlContenido.Size = New Size(560, 260)
        pnlContenido.TabIndex = 0
        ' 
        ' lblBienvenida
        ' 
        lblBienvenida.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblBienvenida.Location = New Point(32, 32)
        lblBienvenida.Name = "lblBienvenida"
        lblBienvenida.Size = New Size(496, 64)
        lblBienvenida.TabIndex = 0
        ' 
        ' lblRol
        ' 
        lblRol.Location = New Point(32, 108)
        lblRol.Name = "lblRol"
        lblRol.Size = New Size(496, 24)
        lblRol.TabIndex = 1
        ' 
        ' btnCerrarSesion
        ' 
        btnCerrarSesion.Location = New Point(32, 157)
        btnCerrarSesion.Name = "btnCerrarSesion"
        btnCerrarSesion.Size = New Size(140, 36)
        btnCerrarSesion.TabIndex = 0
        btnCerrarSesion.Text = "Cerrar sesión"
        btnCerrarSesion.UseVisualStyleBackColor = True
        ' 
        ' btnVerificarPagos
        ' 
        btnVerificarPagos.Location = New Point(32, 73)
        btnVerificarPagos.Name = "btnVerificarPagos"
        btnVerificarPagos.Size = New Size(75, 23)
        btnVerificarPagos.TabIndex = 1
        btnVerificarPagos.Text = "Verificar pagos"
        btnVerificarPagos.UseVisualStyleBackColor = True
        ' 
        ' FrmPrincipal
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(560, 260)
        Controls.Add(pnlContenido)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "FrmPrincipal"
        StartPosition = FormStartPosition.CenterParent
        Text = "GymControl"
        pnlContenido.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblRol As Label
    Friend WithEvents btnCerrarSesion As Button
    Friend WithEvents btnVerificarPagos As Button
End Class
