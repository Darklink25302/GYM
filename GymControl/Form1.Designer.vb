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
        pnlContenido = New Panel()
        lblTitulo = New Label()
        lblUsuario = New Label()
        txtUsuario = New TextBox()
        lblContrasena = New Label()
        txtContrasena = New TextBox()
        btnEntrar = New Button()
        btnPrimerAdministrador = New Button()
        lblEstado = New Label()
        pnlContenido.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlContenido
        ' 
        pnlContenido.Controls.Add(lblTitulo)
        pnlContenido.Controls.Add(lblUsuario)
        pnlContenido.Controls.Add(txtUsuario)
        pnlContenido.Controls.Add(lblContrasena)
        pnlContenido.Controls.Add(txtContrasena)
        pnlContenido.Controls.Add(btnEntrar)
        pnlContenido.Controls.Add(btnPrimerAdministrador)
        pnlContenido.Controls.Add(lblEstado)
        pnlContenido.Dock = DockStyle.Fill
        pnlContenido.Location = New Point(0, 0)
        pnlContenido.Name = "pnlContenido"
        pnlContenido.Size = New Size(482, 453)
        pnlContenido.TabIndex = 0
        ' 
        ' lblTitulo
        ' 
        lblTitulo.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitulo.Location = New Point(28, 28)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(374, 32)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Bienvenido a GymControl"
        ' 
        ' lblUsuario
        ' 
        lblUsuario.Location = New Point(28, 82)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(374, 20)
        lblUsuario.TabIndex = 1
        lblUsuario.Text = "Usuario"
        ' 
        ' txtUsuario
        ' 
        txtUsuario.Location = New Point(28, 106)
        txtUsuario.MaxLength = 50
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New Size(374, 25)
        txtUsuario.TabIndex = 0
        ' 
        ' lblContrasena
        ' 
        lblContrasena.Location = New Point(28, 147)
        lblContrasena.Name = "lblContrasena"
        lblContrasena.Size = New Size(374, 20)
        lblContrasena.TabIndex = 2
        lblContrasena.Text = "Contraseña"
        ' 
        ' txtContrasena
        ' 
        txtContrasena.Location = New Point(28, 171)
        txtContrasena.Name = "txtContrasena"
        txtContrasena.Size = New Size(374, 25)
        txtContrasena.TabIndex = 1
        txtContrasena.UseSystemPasswordChar = True
        ' 
        ' btnEntrar
        ' 
        btnEntrar.Location = New Point(28, 217)
        btnEntrar.Name = "btnEntrar"
        btnEntrar.Size = New Size(374, 36)
        btnEntrar.TabIndex = 2
        btnEntrar.Text = "Entrar"
        btnEntrar.UseVisualStyleBackColor = True
        ' 
        ' btnPrimerAdministrador
        ' 
        btnPrimerAdministrador.Location = New Point(28, 260)
        btnPrimerAdministrador.Name = "btnPrimerAdministrador"
        btnPrimerAdministrador.Size = New Size(374, 36)
        btnPrimerAdministrador.TabIndex = 3
        btnPrimerAdministrador.Text = "Crear primer administrador"
        btnPrimerAdministrador.UseVisualStyleBackColor = True
        btnPrimerAdministrador.Visible = False
        ' 
        ' lblEstado
        ' 
        lblEstado.ForeColor = Color.Firebrick
        lblEstado.Location = New Point(28, 304)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(374, 60)
        lblEstado.TabIndex = 4
        ' 
        ' Form1
        ' 
        AcceptButton = btnEntrar
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(482, 453)
        Controls.Add(pnlContenido)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "GymControl - Iniciar sesión"
        pnlContenido.ResumeLayout(False)
        pnlContenido.PerformLayout()
        ResumeLayout(False)
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
