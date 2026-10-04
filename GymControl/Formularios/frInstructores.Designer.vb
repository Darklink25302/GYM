<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frInstructores
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
        Label1 = New Label()
        TxtNombre = New TextBox()
        Label2 = New Label()
        TextBox2 = New TextBox()
        Lb1Especialidad = New Label()
        TxtEspecialidad = New TextBox()
        Label3 = New Label()
        lb1Telefono = New TextBox()
        Lb1Email = New Label()
        TxtEmail = New TextBox()
        ChkActivo = New CheckBox()
        dgvInstructores = New DataGridView()
        btnNuevo = New Button()
        btnEditar = New Button()
        btnInactivar = New Button()
        btnGuardar = New Button()
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
        ' TxtNombre
        ' 
        TxtNombre.Location = New Point(207, 47)
        TxtNombre.Name = "TxtNombre"
        TxtNombre.Size = New Size(100, 23)
        TxtNombre.TabIndex = 1
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
        ' TextBox2
        ' 
        TextBox2.Location = New Point(409, 50)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(100, 23)
        TextBox2.TabIndex = 3
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
        ' TxtEspecialidad
        ' 
        TxtEspecialidad.Location = New Point(210, 113)
        TxtEspecialidad.Name = "TxtEspecialidad"
        TxtEspecialidad.Size = New Size(100, 23)
        TxtEspecialidad.TabIndex = 5
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
        ' lb1Telefono
        ' 
        lb1Telefono.Location = New Point(409, 114)
        lb1Telefono.Name = "lb1Telefono"
        lb1Telefono.Size = New Size(100, 23)
        lb1Telefono.TabIndex = 7
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
        ' TxtEmail
        ' 
        TxtEmail.Location = New Point(210, 170)
        TxtEmail.Name = "TxtEmail"
        TxtEmail.Size = New Size(100, 23)
        TxtEmail.TabIndex = 9
        ' 
        ' ChkActivo
        ' 
        ChkActivo.AutoSize = True
        ChkActivo.Location = New Point(409, 174)
        ChkActivo.Name = "ChkActivo"
        ChkActivo.Size = New Size(60, 19)
        ChkActivo.TabIndex = 10
        ChkActivo.Text = "Activo"
        ChkActivo.UseVisualStyleBackColor = True
        ' 
        ' dgvInstructores
        ' 
        dgvInstructores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInstructores.Location = New Point(31, 257)
        dgvInstructores.Name = "dgvInstructores"
        dgvInstructores.Size = New Size(739, 79)
        dgvInstructores.TabIndex = 12
        ' 
        ' btnNuevo
        ' 
        btnNuevo.Location = New Point(409, 199)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(75, 23)
        btnNuevo.TabIndex = 13
        btnNuevo.Text = "Nuevo"
        btnNuevo.UseVisualStyleBackColor = True
        ' 
        ' btnEditar
        ' 
        btnEditar.Location = New Point(566, 199)
        btnEditar.Name = "btnEditar"
        btnEditar.Size = New Size(75, 23)
        btnEditar.TabIndex = 14
        btnEditar.Text = "Editar"
        btnEditar.UseVisualStyleBackColor = True
        ' 
        ' btnInactivar
        ' 
        btnInactivar.Location = New Point(663, 199)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 15
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        ' 
        ' btnGuardar
        ' 
        btnGuardar.Location = New Point(490, 199)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(75, 23)
        btnGuardar.TabIndex = 16
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        ' 
        ' frInstructores
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnGuardar)
        Controls.Add(btnInactivar)
        Controls.Add(btnEditar)
        Controls.Add(btnNuevo)
        Controls.Add(dgvInstructores)
        Controls.Add(ChkActivo)
        Controls.Add(TxtEmail)
        Controls.Add(Lb1Email)
        Controls.Add(lb1Telefono)
        Controls.Add(Label3)
        Controls.Add(TxtEspecialidad)
        Controls.Add(Lb1Especialidad)
        Controls.Add(TextBox2)
        Controls.Add(Label2)
        Controls.Add(TxtNombre)
        Controls.Add(Label1)
        Name = "frInstructores"
        Text = "frInstructores"
        CType(dgvInstructores, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TxtNombre As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Lb1Especialidad As Label
    Friend WithEvents TxtEspecialidad As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lb1Telefono As TextBox
    Friend WithEvents Lb1Email As Label
    Friend WithEvents TxtEmail As TextBox
    Friend WithEvents ChkActivo As CheckBox
    Friend WithEvents dgvInstructores As DataGridView
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents btnGuardar As Button
End Class
