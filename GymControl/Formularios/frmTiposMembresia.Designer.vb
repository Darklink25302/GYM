<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTiposMembresia
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
        txtNombre = New TextBox()
        chkActivo = New CheckBox()
        btnGuardar = New Button()
        dgvTiposMembresia = New DataGridView()
        Label2 = New Label()
        Label3 = New Label()
        btnEditar = New Button()
        btnInactivar = New Button()
        btnLimpiar = New Button()
        txtPrecio = New TextBox()
        txtDuracion = New TextBox()
        CType(dgvTiposMembresia, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(51, 15)
        Label1.TabIndex = 0
        Label1.Text = "Nombre"
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(93, 0)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(100, 23)
        txtNombre.TabIndex = 1
        ' 
        ' chkActivo
        ' 
        chkActivo.AutoSize = True
        chkActivo.Location = New Point(145, 107)
        chkActivo.Name = "chkActivo"
        chkActivo.Size = New Size(60, 19)
        chkActivo.TabIndex = 2
        chkActivo.Text = "Activo"
        chkActivo.UseVisualStyleBackColor = True
        ' 
        ' btnGuardar
        ' 
        btnGuardar.Location = New Point(12, 145)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(75, 23)
        btnGuardar.TabIndex = 3
        btnGuardar.Text = "Guardar" & vbLf
        btnGuardar.UseVisualStyleBackColor = True
        ' 
        ' dgvTiposMembresia
        ' 
        dgvTiposMembresia.AccessibleName = " "
        dgvTiposMembresia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTiposMembresia.Location = New Point(145, 189)
        dgvTiposMembresia.Name = "dgvTiposMembresia"
        dgvTiposMembresia.Size = New Size(485, 231)
        dgvTiposMembresia.TabIndex = 4
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 40)
        Label2.Name = "Label2"
        Label2.Size = New Size(43, 15)
        Label2.TabIndex = 5
        Label2.Text = " Precio"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 72)
        Label3.Name = "Label3"
        Label3.Size = New Size(58, 15)
        Label3.TabIndex = 6
        Label3.Text = " Duración"
        ' 
        ' btnEditar
        ' 
        btnEditar.Location = New Point(93, 145)
        btnEditar.Name = "btnEditar"
        btnEditar.Size = New Size(75, 23)
        btnEditar.TabIndex = 9
        btnEditar.Text = "Editar"
        btnEditar.UseVisualStyleBackColor = True
        ' 
        ' btnInactivar
        ' 
        btnInactivar.Location = New Point(185, 145)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(75, 23)
        btnInactivar.TabIndex = 10
        btnInactivar.Text = "Inactivar"
        btnInactivar.UseVisualStyleBackColor = True
        ' 
        ' btnLimpiar
        ' 
        btnLimpiar.Location = New Point(266, 145)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(75, 23)
        btnLimpiar.TabIndex = 11
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = True
        ' 
        ' txtPrecio
        ' 
        txtPrecio.Location = New Point(93, 32)
        txtPrecio.Name = "txtPrecio"
        txtPrecio.Size = New Size(100, 23)
        txtPrecio.TabIndex = 12
        ' 
        ' txtDuracion
        ' 
        txtDuracion.Location = New Point(93, 64)
        txtDuracion.Name = "txtDuracion"
        txtDuracion.Size = New Size(100, 23)
        txtDuracion.TabIndex = 13
        ' 
        ' frmTiposMembresia
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(txtDuracion)
        Controls.Add(txtPrecio)
        Controls.Add(btnLimpiar)
        Controls.Add(btnInactivar)
        Controls.Add(btnEditar)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(dgvTiposMembresia)
        Controls.Add(btnGuardar)
        Controls.Add(chkActivo)
        Controls.Add(txtNombre)
        Controls.Add(Label1)
        Name = "frmTiposMembresia"
        Text = "frmTiposMembresia"
        CType(dgvTiposMembresia, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents dgvTiposMembresia As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtPrecio As TextBox
    Friend WithEvents txtDuracion As TextBox
End Class
