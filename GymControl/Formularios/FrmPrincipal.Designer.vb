<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrincipal
    Inherits Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        pnlContenido = New Panel()
        lblBienvenida = New Label()
        lblRol = New Label()
        lblAviso = New Label()
        btnCerrarSesion = New Button()
        btnUsuarios = New Button()
        btnSocios = New Button()
        btnTiposMembresia = New Button()
        btnMembresias = New Button()
        btnInstructores = New Button()
        btnActividades = New Button()
        btnSalas = New Button()
        btnHorarios = New Button()
        pnlContenido.SuspendLayout()
        SuspendLayout()

        pnlContenido.Name = "pnlContenido"
        pnlContenido.Dock = DockStyle.Fill
        pnlContenido.Controls.Add(lblBienvenida)
        pnlContenido.Controls.Add(lblRol)
        pnlContenido.Controls.Add(lblAviso)
        pnlContenido.Controls.Add(btnCerrarSesion)

        lblBienvenida.Name = "lblBienvenida"
        lblBienvenida.Location = New Point(32, 24)
        lblBienvenida.Size = New Size(552, 65)
        lblBienvenida.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)

        lblRol.Name = "lblRol"
        lblRol.Location = New Point(32, 96)
        lblRol.Size = New Size(552, 28)

        lblAviso.Name = "lblAviso"
        lblAviso.Location = New Point(32, 352)
        lblAviso.Size = New Size(552, 42)

        btnCerrarSesion.Name = "btnCerrarSesion"
        btnCerrarSesion.Location = New Point(32, 410)
        btnCerrarSesion.Size = New Size(180, 38)
        btnCerrarSesion.Text = "Cerrar sesión"
        btnCerrarSesion.TabIndex = 8
        btnCerrarSesion.UseVisualStyleBackColor = True

        btnUsuarios.Name = "btnUsuarios"
        btnUsuarios.Location = New Point(32, 144)
        btnUsuarios.Size = New Size(272, 38)
        btnUsuarios.Text = "Administrar usuarios"
        btnUsuarios.Visible = False
        btnUsuarios.TabIndex = 0
        btnUsuarios.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnUsuarios)

        btnSocios.Name = "btnSocios"
        btnSocios.Location = New Point(312, 144)
        btnSocios.Size = New Size(272, 38)
        btnSocios.Text = "Socios"
        btnSocios.Visible = False
        btnSocios.TabIndex = 1
        btnSocios.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnSocios)

        btnTiposMembresia.Name = "btnTiposMembresia"
        btnTiposMembresia.Location = New Point(32, 194)
        btnTiposMembresia.Size = New Size(272, 38)
        btnTiposMembresia.Text = "Tipos de membresía"
        btnTiposMembresia.Visible = False
        btnTiposMembresia.TabIndex = 2
        btnTiposMembresia.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnTiposMembresia)

        btnMembresias.Name = "btnMembresias"
        btnMembresias.Location = New Point(312, 194)
        btnMembresias.Size = New Size(272, 38)
        btnMembresias.Text = "Membresías y primer abono"
        btnMembresias.Visible = False
        btnMembresias.TabIndex = 3
        btnMembresias.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnMembresias)

        btnInstructores.Name = "btnInstructores"
        btnInstructores.Location = New Point(32, 244)
        btnInstructores.Size = New Size(272, 38)
        btnInstructores.Text = "Instructores"
        btnInstructores.Visible = False
        btnInstructores.TabIndex = 4
        btnInstructores.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnInstructores)

        btnActividades.Name = "btnActividades"
        btnActividades.Location = New Point(312, 244)
        btnActividades.Size = New Size(272, 38)
        btnActividades.Text = "Actividades"
        btnActividades.Visible = False
        btnActividades.TabIndex = 5
        btnActividades.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnActividades)

        btnSalas.Name = "btnSalas"
        btnSalas.Location = New Point(32, 294)
        btnSalas.Size = New Size(272, 38)
        btnSalas.Text = "Salas"
        btnSalas.Visible = False
        btnSalas.TabIndex = 6
        btnSalas.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnSalas)

        btnHorarios.Name = "btnHorarios"
        btnHorarios.Location = New Point(312, 294)
        btnHorarios.Size = New Size(272, 38)
        btnHorarios.Text = "Horarios"
        btnHorarios.Visible = False
        btnHorarios.TabIndex = 7
        btnHorarios.UseVisualStyleBackColor = True
        pnlContenido.Controls.Add(btnHorarios)

        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(616, 480)
        Font = New Font("Segoe UI", 10.0F)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        StartPosition = FormStartPosition.CenterParent
        Name = "FrmPrincipal"
        Text = "GymControl"
        Controls.Add(pnlContenido)
        pnlContenido.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblRol As Label
    Friend WithEvents lblAviso As Label
    Friend WithEvents btnCerrarSesion As Button
    Friend WithEvents btnUsuarios As Button
    Friend WithEvents btnSocios As Button
    Friend WithEvents btnTiposMembresia As Button
    Friend WithEvents btnMembresias As Button
    Friend WithEvents btnInstructores As Button
    Friend WithEvents btnActividades As Button
    Friend WithEvents btnSalas As Button
    Friend WithEvents btnHorarios As Button
End Class
