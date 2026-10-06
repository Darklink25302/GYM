<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalas
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
        lblCapacidad = New Label()
        txtcapacidad = New TextBox()
        btnNuevo = New Button()
        btnGuardar = New Button()
        btnInactivar = New Button()
        dgvSalas = New DataGridView()
        CType(dgvSalas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lb1Nombre
        '
        lb1Nombre.AutoSize = True
        lb1Nombre.Location = New Point(67, 27)
        lb1Nombre.Name = "lb1Nombre"
        lb1Nombre.Size = New Size(54, 15)
        lb1Nombre.TabIndex = 0
        lb1Nombre.Text = "Nombre:"
        '
        ' txtNombre
        '
        txtNombre.Location = New Point(146, 27)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(100, 23)
        txtNombre.TabIndex = 1
        '
        ' lblCapacidad
        '
        lblCapacidad.AutoSize = True
        lblCapacidad.Location = New Point(67, 88)
        lblCapacidad.Name = "lblCapacidad"
        lblCapacidad.Size = New Size(63, 15)
        lblCapacidad.TabIndex = 2
        lblCapacidad.Text = "Capacidad"
        '
        ' txtcapacidad
        '
        txtcapacidad.Location = New Point(146, 85)
        txtcapacidad.Name = "txtcapacidad"
        txtcapacidad.Size = New Size(100, 23)
        txtcapacidad.TabIndex = 3
        '
        ' btnGuardar
        '
        btnGuardar.Location = New Point(76, 171)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(75, 23)
        btnGuardar.TabIndex = 4
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = True
        '
        ' btnInactivar
        '
        btnInactivar.Location = New Point(193, 171)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 5
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        '
        ' dgvSalas
        '
        dgvSalas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSalas.Location = New Point(76, 250)
        dgvSalas.Name = "dgvSalas"
        dgvSalas.Size = New Size(545, 71)
        dgvSalas.TabIndex = 6
        '
        ' frmSalas
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Text = "Nuevo"
        btnNuevo.Location = New Point(300, 171)
        btnNuevo.Size = New Size(90, 30)
        Controls.Add(btnNuevo)
        Controls.Add(dgvSalas)
        Controls.Add(btnInactivar)
        Controls.Add(btnGuardar)
        Controls.Add(txtcapacidad)
        Controls.Add(lblCapacidad)
        Controls.Add(txtNombre)
        Controls.Add(lb1Nombre)
        Name = "frmSalas"
        Text = "frmSalas"
        CType(dgvSalas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lb1Nombre As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents lblCapacidad As Label
    Friend WithEvents txtcapacidad As TextBox
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents dgvSalas As DataGridView
End Class
