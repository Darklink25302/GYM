<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmUsuarios
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
        Me.dgvUsuarios = New DataGridView()
        Me.lblUsuario = New Label()
        Me.txtUsuario = New TextBox()
        Me.lblRol = New Label()
        Me.cmbRol = New ComboBox()
        Me.chkActivo = New CheckBox()
        Me.lblSocio = New Label()
        Me.nudSocio = New NumericUpDown()
        Me.lblInstructor = New Label()
        Me.nudInstructor = New NumericUpDown()
        Me.lblContrasena = New Label()
        Me.txtContrasena = New TextBox()
        Me.lblConfirmacion = New Label()
        Me.txtConfirmacion = New TextBox()
        Me.btnNuevo = New Button()
        Me.btnGuardar = New Button()
        Me.btnDesbloquear = New Button()
        Me.btnRestablecer = New Button()
        Me.btnActualizar = New Button()
        Me.btnCerrar = New Button()
        Me.lblEstado = New Label()
        Me.col_id_usuario = New DataGridViewTextBoxColumn()
        Me.col_nombre_usuario = New DataGridViewTextBoxColumn()
        Me.col_id_rol = New DataGridViewTextBoxColumn()
        Me.col_rol = New DataGridViewTextBoxColumn()
        Me.col_activo = New DataGridViewCheckBoxColumn()
        Me.col_bloqueado = New DataGridViewCheckBoxColumn()
        Me.col_intentos_fallidos = New DataGridViewTextBoxColumn()
        Me.col_id_socio = New DataGridViewTextBoxColumn()
        Me.col_id_instructor = New DataGridViewTextBoxColumn()
        CType(Me.dgvUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSocio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudInstructor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlContenido.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Dock = DockStyle.Fill
        Me.pnlContenido.Controls.Add(Me.lblTitulo)
        Me.pnlContenido.Controls.Add(Me.dgvUsuarios)
        Me.pnlContenido.Controls.Add(Me.lblUsuario)
        Me.pnlContenido.Controls.Add(Me.txtUsuario)
        Me.pnlContenido.Controls.Add(Me.lblRol)
        Me.pnlContenido.Controls.Add(Me.cmbRol)
        Me.pnlContenido.Controls.Add(Me.chkActivo)
        Me.pnlContenido.Controls.Add(Me.lblSocio)
        Me.pnlContenido.Controls.Add(Me.nudSocio)
        Me.pnlContenido.Controls.Add(Me.lblInstructor)
        Me.pnlContenido.Controls.Add(Me.nudInstructor)
        Me.pnlContenido.Controls.Add(Me.lblContrasena)
        Me.pnlContenido.Controls.Add(Me.txtContrasena)
        Me.pnlContenido.Controls.Add(Me.lblConfirmacion)
        Me.pnlContenido.Controls.Add(Me.txtConfirmacion)
        Me.pnlContenido.Controls.Add(Me.btnNuevo)
        Me.pnlContenido.Controls.Add(Me.btnGuardar)
        Me.pnlContenido.Controls.Add(Me.btnDesbloquear)
        Me.pnlContenido.Controls.Add(Me.btnRestablecer)
        Me.pnlContenido.Controls.Add(Me.btnActualizar)
        Me.pnlContenido.Controls.Add(Me.btnCerrar)
        Me.pnlContenido.Controls.Add(Me.lblEstado)

        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Location = New Point(24, 20)
        Me.lblTitulo.Size = New Size(860, 34)
        Me.lblTitulo.Text = "Administración de usuarios"
        Me.lblTitulo.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)

        Me.dgvUsuarios.Name = "dgvUsuarios"
        Me.dgvUsuarios.Location = New Point(24, 64)
        Me.dgvUsuarios.Size = New Size(860, 222)
        Me.dgvUsuarios.ReadOnly = True
        Me.dgvUsuarios.AllowUserToAddRows = False
        Me.dgvUsuarios.AllowUserToDeleteRows = False
        Me.dgvUsuarios.AllowUserToResizeRows = False
        Me.dgvUsuarios.AutoGenerateColumns = False
        Me.dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsuarios.MultiSelect = False
        Me.dgvUsuarios.RowHeadersVisible = False
        Me.dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsuarios.TabIndex = 0

        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Location = New Point(24, 306)
        Me.lblUsuario.Size = New Size(140, 22)
        Me.lblUsuario.Text = "Usuario"

        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Location = New Point(24, 331)
        Me.txtUsuario.Size = New Size(270, 27)
        Me.txtUsuario.MaxLength = 50
        Me.txtUsuario.TabIndex = 1

        Me.lblRol.Name = "lblRol"
        Me.lblRol.Location = New Point(316, 306)
        Me.lblRol.Size = New Size(180, 22)
        Me.lblRol.Text = "Rol"

        Me.cmbRol.Name = "cmbRol"
        Me.cmbRol.Location = New Point(316, 331)
        Me.cmbRol.Size = New Size(230, 27)
        Me.cmbRol.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbRol.TabIndex = 2

        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Location = New Point(570, 334)
        Me.chkActivo.Size = New Size(120, 24)
        Me.chkActivo.Text = "Activo"
        Me.chkActivo.Checked = True
        Me.chkActivo.TabIndex = 3

        Me.lblSocio.Name = "lblSocio"
        Me.lblSocio.Location = New Point(24, 376)
        Me.lblSocio.Size = New Size(300, 22)
        Me.lblSocio.Text = "ID de socio (0 = sin asignar)"

        Me.nudSocio.Name = "nudSocio"
        Me.nudSocio.Location = New Point(24, 402)
        Me.nudSocio.Size = New Size(270, 27)
        Me.nudSocio.Maximum = 2147483647D
        Me.nudSocio.Enabled = False
        Me.nudSocio.TabIndex = 4

        Me.lblInstructor.Name = "lblInstructor"
        Me.lblInstructor.Location = New Point(316, 376)
        Me.lblInstructor.Size = New Size(300, 22)
        Me.lblInstructor.Text = "ID de instructor (0 = sin asignar)"

        Me.nudInstructor.Name = "nudInstructor"
        Me.nudInstructor.Location = New Point(316, 402)
        Me.nudInstructor.Size = New Size(270, 27)
        Me.nudInstructor.Maximum = 2147483647D
        Me.nudInstructor.Enabled = False
        Me.nudInstructor.TabIndex = 5

        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Location = New Point(24, 448)
        Me.lblContrasena.Size = New Size(270, 22)
        Me.lblContrasena.Text = "Contraseña nueva (mínimo 12 caracteres)"

        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.Location = New Point(24, 474)
        Me.txtContrasena.Size = New Size(270, 27)
        Me.txtContrasena.UseSystemPasswordChar = True
        Me.txtContrasena.TabIndex = 6

        Me.lblConfirmacion.Name = "lblConfirmacion"
        Me.lblConfirmacion.Location = New Point(316, 448)
        Me.lblConfirmacion.Size = New Size(270, 22)
        Me.lblConfirmacion.Text = "Confirmar contraseña"

        Me.txtConfirmacion.Name = "txtConfirmacion"
        Me.txtConfirmacion.Location = New Point(316, 474)
        Me.txtConfirmacion.Size = New Size(270, 27)
        Me.txtConfirmacion.UseSystemPasswordChar = True
        Me.txtConfirmacion.TabIndex = 7

        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Location = New Point(24, 524)
        Me.btnNuevo.Size = New Size(90, 36)
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TabIndex = 8

        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Location = New Point(124, 524)
        Me.btnGuardar.Size = New Size(100, 36)
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.TabIndex = 9

        Me.btnDesbloquear.Name = "btnDesbloquear"
        Me.btnDesbloquear.Location = New Point(234, 524)
        Me.btnDesbloquear.Size = New Size(120, 36)
        Me.btnDesbloquear.Text = "Desbloquear"
        Me.btnDesbloquear.TabIndex = 10

        Me.btnRestablecer.Name = "btnRestablecer"
        Me.btnRestablecer.Location = New Point(364, 524)
        Me.btnRestablecer.Size = New Size(210, 36)
        Me.btnRestablecer.Text = "Restablecer contraseña"
        Me.btnRestablecer.TabIndex = 11

        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Location = New Point(584, 524)
        Me.btnActualizar.Size = New Size(150, 36)
        Me.btnActualizar.Text = "Actualizar lista"
        Me.btnActualizar.TabIndex = 12

        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Location = New Point(744, 524)
        Me.btnCerrar.Size = New Size(140, 36)
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.TabIndex = 13

        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Location = New Point(24, 578)
        Me.lblEstado.Size = New Size(860, 58)
        Me.lblEstado.Text = ""
        Me.lblEstado.ForeColor = Color.Firebrick

        Me.col_id_usuario.Name = "id_usuario"
        Me.col_id_usuario.DataPropertyName = "id_usuario"
        Me.col_id_usuario.HeaderText = "ID"
        Me.col_id_usuario.FillWeight = 40.0F
        Me.col_id_usuario.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_id_usuario)
        Me.col_nombre_usuario.Name = "nombre_usuario"
        Me.col_nombre_usuario.DataPropertyName = "nombre_usuario"
        Me.col_nombre_usuario.HeaderText = "Usuario"
        Me.col_nombre_usuario.FillWeight = 160.0F
        Me.col_nombre_usuario.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_nombre_usuario)
        Me.col_id_rol.Name = "id_rol"
        Me.col_id_rol.DataPropertyName = "id_rol"
        Me.col_id_rol.HeaderText = "IdRol"
        Me.col_id_rol.Visible = False
        Me.dgvUsuarios.Columns.Add(Me.col_id_rol)
        Me.col_rol.Name = "rol"
        Me.col_rol.DataPropertyName = "rol"
        Me.col_rol.HeaderText = "Rol"
        Me.col_rol.FillWeight = 100.0F
        Me.col_rol.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_rol)
        Me.col_activo.Name = "activo"
        Me.col_activo.DataPropertyName = "activo"
        Me.col_activo.HeaderText = "Activo"
        Me.col_activo.FillWeight = 60.0F
        Me.col_activo.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_activo)
        Me.col_bloqueado.Name = "bloqueado"
        Me.col_bloqueado.DataPropertyName = "bloqueado"
        Me.col_bloqueado.HeaderText = "Bloqueado"
        Me.col_bloqueado.FillWeight = 75.0F
        Me.col_bloqueado.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_bloqueado)
        Me.col_intentos_fallidos.Name = "intentos_fallidos"
        Me.col_intentos_fallidos.DataPropertyName = "intentos_fallidos"
        Me.col_intentos_fallidos.HeaderText = "Intentos"
        Me.col_intentos_fallidos.FillWeight = 60.0F
        Me.col_intentos_fallidos.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_intentos_fallidos)
        Me.col_id_socio.Name = "id_socio"
        Me.col_id_socio.DataPropertyName = "id_socio"
        Me.col_id_socio.HeaderText = "ID socio"
        Me.col_id_socio.FillWeight = 65.0F
        Me.col_id_socio.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_id_socio)
        Me.col_id_instructor.Name = "id_instructor"
        Me.col_id_instructor.DataPropertyName = "id_instructor"
        Me.col_id_instructor.HeaderText = "ID instructor"
        Me.col_id_instructor.FillWeight = 65.0F
        Me.col_id_instructor.Visible = True
        Me.dgvUsuarios.Columns.Add(Me.col_id_instructor)

        Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New Size(908, 656)
        Me.Font = New Font("Segoe UI", 10.0F)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "GymControl - Usuarios"
        Me.Controls.Add(Me.pnlContenido)

        CType(Me.dgvUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSocio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudInstructor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlContenido.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents dgvUsuarios As DataGridView
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblRol As Label
    Friend WithEvents cmbRol As ComboBox
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents lblSocio As Label
    Friend WithEvents nudSocio As NumericUpDown
    Friend WithEvents lblInstructor As Label
    Friend WithEvents nudInstructor As NumericUpDown
    Friend WithEvents lblContrasena As Label
    Friend WithEvents txtContrasena As TextBox
    Friend WithEvents lblConfirmacion As Label
    Friend WithEvents txtConfirmacion As TextBox
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnDesbloquear As Button
    Friend WithEvents btnRestablecer As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblEstado As Label
    Friend WithEvents col_id_usuario As DataGridViewTextBoxColumn
    Friend WithEvents col_nombre_usuario As DataGridViewTextBoxColumn
    Friend WithEvents col_id_rol As DataGridViewTextBoxColumn
    Friend WithEvents col_rol As DataGridViewTextBoxColumn
    Friend WithEvents col_activo As DataGridViewCheckBoxColumn
    Friend WithEvents col_bloqueado As DataGridViewCheckBoxColumn
    Friend WithEvents col_intentos_fallidos As DataGridViewTextBoxColumn
    Friend WithEvents col_id_socio As DataGridViewTextBoxColumn
    Friend WithEvents col_id_instructor As DataGridViewTextBoxColumn
End Class
