<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActividades
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
        lb1Nombre = New Label()
        txtNombre = New TextBox()
        lblDescripcion = New Label()
        txtDescripcion = New TextBox()
        btnNuevo = New Button()
        btnGuardar = New Button()
        btnInactivar = New Button()
        dgvActividades = New DataGridView()
        CType(dgvActividades, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lb1Nombre
        '
        lb1Nombre.AutoSize = True
        lb1Nombre.Location = New Point(64, 37)
        lb1Nombre.Name = "lb1Nombre"
        lb1Nombre.Size = New Size(51, 15)
        lb1Nombre.TabIndex = 0
        lb1Nombre.Text = "Nombre"
        '
        ' txtNombre
        '
        txtNombre.Location = New Point(134, 37)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(100, 23)
        txtNombre.TabIndex = 1
        '
        ' lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Location = New Point(64, 93)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(69, 15)
        lblDescripcion.TabIndex = 2
        lblDescripcion.Text = "Descripcion"
        '
        ' txtDescripcion
        '
        txtDescripcion.Location = New Point(134, 90)
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(100, 23)
        txtDescripcion.TabIndex = 3
        '
        ' btnGuardar
        '
        btnGuardar.Location = New Point(64, 186)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(75, 23)
        btnGuardar.TabIndex = 4
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        '
        ' btnInactivar
        '
        btnInactivar.Location = New Point(179, 186)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 5
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        '
        ' dgvActividades
        '
        dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvActividades.Location = New Point(134, 234)
        dgvActividades.Name = "dgvActividades"
        dgvActividades.Size = New Size(614, 105)
        dgvActividades.TabIndex = 6
        '
        ' txtDescripcion
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Text = "Nuevo"
        btnNuevo.Location = New Point(300, 171)
        btnNuevo.Size = New Size(90, 30)
        Controls.Add(btnNuevo)
        Controls.Add(dgvActividades)
        Controls.Add(btnInactivar)
        Controls.Add(btnGuardar)
        Controls.Add(txtDescripcion)
        Controls.Add(lblDescripcion)
        Controls.Add(txtNombre)
        Controls.Add(lb1Nombre)
        Name = "frmActividades"
        Text = "Actividades"
        CType(dgvActividades, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lb1Nombre As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents dgvActividades As DataGridView
End Class
