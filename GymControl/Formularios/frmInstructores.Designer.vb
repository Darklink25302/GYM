<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInstructores
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        txtNombre = New TextBox()
        Label2 = New Label()
        txtApellido = New TextBox()
        Lb1Especialidad = New Label()
        txtEspecialidad = New TextBox()
        Label3 = New Label()
        txtTelefono = New TextBox()
        Lb1Email = New Label()
        txtEmail = New TextBox()
        chkActivo = New CheckBox()
        dgvInstructores = New DataGridView()
        btnEditar = New Button()
        btnInactivar = New Button()
        btnGuardar = New Button()
        btnNuevo = New Button()
        CType(dgvInstructores, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(132, 47)
        Label1.Name = "Label1"
        Label1.Size = New Size(51, 15)
        Label1.TabIndex = 0
        Label1.Text = "Nombre"
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(207, 47)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(100, 23)
        txtNombre.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(338, 50)
        Label2.Name = "Label2"
        Label2.Size = New Size(51, 15)
        Label2.TabIndex = 2
        Label2.Text = "Apellido"
        ' 
        ' txtApellido
        ' 
        txtApellido.Location = New Point(409, 50)
        txtApellido.Name = "txtApellido"
        txtApellido.Size = New Size(100, 23)
        txtApellido.TabIndex = 3
        ' 
        ' Lb1Especialidad
        ' 
        Lb1Especialidad.AutoSize = True
        Lb1Especialidad.Location = New Point(132, 113)
        Lb1Especialidad.Name = "Lb1Especialidad"
        Lb1Especialidad.Size = New Size(72, 15)
        Lb1Especialidad.TabIndex = 4
        Lb1Especialidad.Text = "Especialidad"
        ' 
        ' txtEspecialidad
        ' 
        txtEspecialidad.Location = New Point(210, 113)
        txtEspecialidad.Name = "txtEspecialidad"
        txtEspecialidad.Size = New Size(100, 23)
        txtEspecialidad.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(338, 117)
        Label3.Name = "Label3"
        Label3.Size = New Size(53, 15)
        Label3.TabIndex = 6
        Label3.Text = "Telefono"
        ' 
        ' txtTelefono
        ' 
        txtTelefono.Location = New Point(409, 114)
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(100, 23)
        txtTelefono.TabIndex = 7
        ' 
        ' Lb1Email
        ' 
        Lb1Email.AutoSize = True
        Lb1Email.Location = New Point(132, 170)
        Lb1Email.Name = "Lb1Email"
        Lb1Email.Size = New Size(36, 15)
        Lb1Email.TabIndex = 8
        Lb1Email.Text = "Email"
        ' 
        ' txtEmail
        ' 
        txtEmail.Location = New Point(210, 170)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(100, 23)
        txtEmail.TabIndex = 9
        ' 
        ' chkActivo
        ' 
        chkActivo.AutoSize = True
        chkActivo.Location = New Point(409, 174)
        chkActivo.Name = "chkActivo"
        chkActivo.Size = New Size(60, 19)
        chkActivo.TabIndex = 10
        chkActivo.Text = "Activo"
        chkActivo.UseVisualStyleBackColor = True
        ' 
        ' dgvInstructores
        ' 
        dgvInstructores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInstructores.Location = New Point(31, 257)
        dgvInstructores.Name = "dgvInstructores"
        dgvInstructores.Size = New Size(739, 79)
        dgvInstructores.TabIndex = 12
        ' 
        ' btnEditar
        ' 
        btnEditar.Location = New Point(302, 218)
        btnEditar.Name = "btnEditar"
        btnEditar.Size = New Size(75, 23)
        btnEditar.TabIndex = 14
        btnEditar.Text = "Editar"
        btnEditar.UseVisualStyleBackColor = True
        ' 
        ' btnInactivar
        ' 
        btnInactivar.Location = New Point(409, 218)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 15
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        ' 
        ' btnGuardar
        ' 
        btnGuardar.Location = New Point(213, 218)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(75, 23)
        btnGuardar.TabIndex = 16
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        ' 
        ' btnNuevo
        ' 
        btnNuevo.Location = New Point(132, 218)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(75, 23)
        btnNuevo.TabIndex = 17
        btnNuevo.Text = "Nuevo"
        btnNuevo.UseVisualStyleBackColor = True
        ' 
        ' frmInstructores
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnNuevo)
        Controls.Add(btnGuardar)
        Controls.Add(btnInactivar)
        Controls.Add(btnEditar)
        Controls.Add(dgvInstructores)
        Controls.Add(chkActivo)
        Controls.Add(txtEmail)
        Controls.Add(Lb1Email)
        Controls.Add(txtTelefono)
        Controls.Add(Label3)
        Controls.Add(txtEspecialidad)
        Controls.Add(Lb1Especialidad)
        Controls.Add(txtApellido)
        Controls.Add(Label2)
        Controls.Add(txtNombre)
        Controls.Add(Label1)
        Name = "frmInstructores"
        Text = "frInstructores"
        CType(dgvInstructores, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtApellido As TextBox
    Friend WithEvents Lb1Especialidad As Label
    Friend WithEvents txtEspecialidad As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents Lb1Email As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents dgvInstructores As DataGridView
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnNuevo As Button

End Class
