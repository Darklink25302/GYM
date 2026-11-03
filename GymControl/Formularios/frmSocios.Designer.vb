<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSocios
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        lblCedula = New Label()
        txtCedula = New TextBox()
        lblNombres = New Label()
        lblApellidos = New Label()
        lblTelefono = New Label()
        lblFechaNacimiento = New Label()
        txtNombres = New TextBox()
        txtApellidos = New TextBox()
        lblCorreo = New Label()
        lblDireccion = New Label()
        txtCorreo = New TextBox()
        txtTelefono = New TextBox()
        txtDireccion = New TextBox()
        dtpFechaNacimiento = New DateTimePicker()
        btnRegistrar = New Button()
        btnActualizar = New Button()
        btnInactivar = New Button()
        btnLimpiar = New Button()
        dgvSocios = New DataGridView()
        CType(dgvSocios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblCedula
        ' 
        lblCedula.AutoSize = True
        lblCedula.Location = New Point(3, 8)
        lblCedula.Name = "lblCedula"
        lblCedula.Size = New Size(44, 15)
        lblCedula.TabIndex = 0
        lblCedula.Text = "Cédula"
        ' 
        ' txtCedula
        ' 
        txtCedula.Location = New Point(65, 0)
        txtCedula.Name = "txtCedula"
        txtCedula.Size = New Size(142, 23)
        txtCedula.TabIndex = 1
        ' 
        ' lblNombres
        ' 
        lblNombres.AutoSize = True
        lblNombres.Location = New Point(3, 35)
        lblNombres.Name = "lblNombres"
        lblNombres.Size = New Size(56, 15)
        lblNombres.TabIndex = 2
        lblNombres.Text = "Nombres"
        ' 
        ' lblApellidos
        ' 
        lblApellidos.AutoSize = True
        lblApellidos.Location = New Point(3, 65)
        lblApellidos.Name = "lblApellidos"
        lblApellidos.Size = New Size(56, 15)
        lblApellidos.TabIndex = 3
        lblApellidos.Text = "Apellidos"
        ' 
        ' lblTelefono
        ' 
        lblTelefono.AutoSize = True
        lblTelefono.Location = New Point(3, 90)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New Size(53, 15)
        lblTelefono.TabIndex = 4
        lblTelefono.Text = "Teléfono"
        ' 
        ' lblFechaNacimiento
        ' 
        lblFechaNacimiento.AutoSize = True
        lblFechaNacimiento.Location = New Point(332, 8)
        lblFechaNacimiento.Name = "lblFechaNacimiento"
        lblFechaNacimiento.Size = New Size(117, 15)
        lblFechaNacimiento.TabIndex = 5
        lblFechaNacimiento.Text = "Fecha de nacimiento"
        ' 
        ' txtNombres
        ' 
        txtNombres.Location = New Point(65, 27)
        txtNombres.Name = "txtNombres"
        txtNombres.Size = New Size(142, 23)
        txtNombres.TabIndex = 6
        ' 
        ' txtApellidos
        ' 
        txtApellidos.Location = New Point(65, 57)
        txtApellidos.Name = "txtApellidos"
        txtApellidos.Size = New Size(142, 23)
        txtApellidos.TabIndex = 7
        ' 
        ' lblCorreo
        ' 
        lblCorreo.AutoSize = True
        lblCorreo.Location = New Point(3, 114)
        lblCorreo.Name = "lblCorreo"
        lblCorreo.Size = New Size(43, 15)
        lblCorreo.TabIndex = 8
        lblCorreo.Text = "Correo"
        ' 
        ' lblDireccion
        ' 
        lblDireccion.AutoSize = True
        lblDireccion.Location = New Point(3, 149)
        lblDireccion.Name = "lblDireccion"
        lblDireccion.Size = New Size(57, 15)
        lblDireccion.TabIndex = 9
        lblDireccion.Text = "Dirección"
        ' 
        ' txtCorreo
        ' 
        txtCorreo.Location = New Point(65, 114)
        txtCorreo.Name = "txtCorreo"
        txtCorreo.Size = New Size(142, 23)
        txtCorreo.TabIndex = 10
        ' 
        ' txtTelefono
        ' 
        txtTelefono.Location = New Point(65, 86)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(142, 23)
        txtTelefono.TabIndex = 11
        ' 
        ' txtDireccion
        ' 
        txtDireccion.Location = New Point(65, 141)
        txtDireccion.Name = "txtDireccion"
        txtDireccion.Size = New Size(142, 23)
        txtDireccion.TabIndex = 12
        ' 
        ' dtpFechaNacimiento
        ' 
        dtpFechaNacimiento.Format = DateTimePickerFormat.Short
        dtpFechaNacimiento.Location = New Point(470, 2)
        dtpFechaNacimiento.Name = "dtpFechaNacimiento"
        dtpFechaNacimiento.Size = New Size(253, 23)
        dtpFechaNacimiento.TabIndex = 13
        ' 
        ' btnRegistrar
        ' 
        btnRegistrar.Location = New Point(332, 35)
        btnRegistrar.Name = "btnRegistrar"
        btnRegistrar.Size = New Size(75, 23)
        btnRegistrar.TabIndex = 14
        btnRegistrar.Text = "Registrar"
        btnRegistrar.UseVisualStyleBackColor = True
        ' 
        ' btnActualizar
        ' 
        btnActualizar.Location = New Point(413, 35)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(75, 23)
        btnActualizar.TabIndex = 15
        btnActualizar.Text = "Actualizar"
        btnActualizar.UseVisualStyleBackColor = True
        ' 
        ' btnInactivar
        ' 
        btnInactivar.Location = New Point(494, 35)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 16
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        ' 
        ' btnLimpiar
        ' 
        btnLimpiar.Location = New Point(587, 35)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(75, 23)
        btnLimpiar.TabIndex = 17
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = True
        ' 
        ' dgvSocios
        ' 
        dgvSocios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSocios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSocios.Location = New Point(265, 162)
        dgvSocios.MultiSelect = False
        dgvSocios.Name = "dgvSocios"
        dgvSocios.ReadOnly = True
        dgvSocios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSocios.Size = New Size(458, 265)
        dgvSocios.TabIndex = 18
        ' 
        ' frmSocios
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvSocios)
        Controls.Add(btnLimpiar)
        Controls.Add(btnInactivar)
        Controls.Add(btnActualizar)
        Controls.Add(btnRegistrar)
        Controls.Add(dtpFechaNacimiento)
        Controls.Add(txtDireccion)
        Controls.Add(txtTelefono)
        Controls.Add(txtCorreo)
        Controls.Add(lblDireccion)
        Controls.Add(lblCorreo)
        Controls.Add(txtApellidos)
        Controls.Add(txtNombres)
        Controls.Add(lblFechaNacimiento)
        Controls.Add(lblTelefono)
        Controls.Add(lblApellidos)
        Controls.Add(lblNombres)
        Controls.Add(txtCedula)
        Controls.Add(lblCedula)
        Name = "frmSocios"
        Text = "Gestión de Socios"
        CType(dgvSocios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblCedula As Label
    Friend WithEvents txtCedula As TextBox
    Friend WithEvents lblNombres As Label
    Friend WithEvents lblApellidos As Label
    Friend WithEvents lblTelefono As Label
    Friend WithEvents lblFechaNacimiento As Label
    Friend WithEvents txtNombres As TextBox
    Friend WithEvents txtApellidos As TextBox
    Friend WithEvents lblCorreo As Label
    Friend WithEvents lblDireccion As Label
    Friend WithEvents txtCorreo As TextBox
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents txtDireccion As TextBox
    Friend WithEvents dtpFechaNacimiento As DateTimePicker
    Friend WithEvents btnRegistrar As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents dgvSocios As DataGridView
End Class
