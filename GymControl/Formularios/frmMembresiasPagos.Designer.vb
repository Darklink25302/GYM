<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMembresiasPagos
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
        lblSocio = New Label()
        lblTipoMembresia = New Label()
        lblFechaInicio = New Label()
        lblFechaVencimiento = New Label()
        lblTotal = New Label()
        cboSocio = New ComboBox()
        cboTipoMembresia = New ComboBox()
        dtpFechaInicio = New DateTimePicker()
        dtpFechaVencimiento = New DateTimePicker()
        txtTotal = New TextBox()
        txtPrimerAbono = New TextBox()
        lblPrimerAbono = New Label()
        btnRegistrarMembresia = New Button()
        btnLimpiar = New Button()
        dgvMembresias = New DataGridView()
        CType(dgvMembresias, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblSocio
        ' 
        lblSocio.AutoSize = True
        lblSocio.Location = New Point(32, 9)
        lblSocio.Name = "lblSocio"
        lblSocio.Size = New Size(36, 15)
        lblSocio.TabIndex = 0
        lblSocio.Text = "Socio"
        ' 
        ' lblTipoMembresia
        ' 
        lblTipoMembresia.AutoSize = True
        lblTipoMembresia.Location = New Point(1, 38)
        lblTipoMembresia.Name = "lblTipoMembresia"
        lblTipoMembresia.Size = New Size(109, 15)
        lblTipoMembresia.TabIndex = 1
        lblTipoMembresia.Text = "Tipo de membresía"
        ' 
        ' lblFechaInicio
        ' 
        lblFechaInicio.AutoSize = True
        lblFechaInicio.Location = New Point(275, 9)
        lblFechaInicio.Name = "lblFechaInicio"
        lblFechaInicio.Size = New Size(86, 15)
        lblFechaInicio.TabIndex = 2
        lblFechaInicio.Text = "Fecha de inicio"
        ' 
        ' lblFechaVencimiento
        ' 
        lblFechaVencimiento.AutoSize = True
        lblFechaVencimiento.Location = New Point(275, 38)
        lblFechaVencimiento.Name = "lblFechaVencimiento"
        lblFechaVencimiento.Size = New Size(123, 15)
        lblFechaVencimiento.TabIndex = 3
        lblFechaVencimiento.Text = "Fecha de vencimiento"
        ' 
        ' lblTotal
        ' 
        lblTotal.AutoSize = True
        lblTotal.Location = New Point(1, 68)
        lblTotal.Name = "lblTotal"
        lblTotal.Size = New Size(33, 15)
        lblTotal.TabIndex = 4
        lblTotal.Text = "Total"
        ' 
        ' cboSocio
        ' 
        cboSocio.DropDownStyle = ComboBoxStyle.DropDownList
        cboSocio.FormattingEnabled = True
        cboSocio.Location = New Point(116, 1)
        cboSocio.Name = "cboSocio"
        cboSocio.Size = New Size(121, 23)
        cboSocio.TabIndex = 5
        ' 
        ' cboTipoMembresia
        ' 
        cboTipoMembresia.DropDownStyle = ComboBoxStyle.DropDownList
        cboTipoMembresia.FormattingEnabled = True
        cboTipoMembresia.Location = New Point(116, 30)
        cboTipoMembresia.Name = "cboTipoMembresia"
        cboTipoMembresia.Size = New Size(121, 23)
        cboTipoMembresia.TabIndex = 6
        ' 
        ' dtpFechaInicio
        ' 
        dtpFechaInicio.Location = New Point(412, 3)
        dtpFechaInicio.Name = "dtpFechaInicio"
        dtpFechaInicio.Size = New Size(200, 23)
        dtpFechaInicio.TabIndex = 7
        ' 
        ' dtpFechaVencimiento
        ' 
        dtpFechaVencimiento.Location = New Point(412, 32)
        dtpFechaVencimiento.Name = "dtpFechaVencimiento"
        dtpFechaVencimiento.Size = New Size(200, 23)
        dtpFechaVencimiento.TabIndex = 8
        ' 
        ' txtTotal
        ' 
        txtTotal.Location = New Point(137, 59)
        txtTotal.Name = "txtTotal"
        txtTotal.Size = New Size(100, 23)
        txtTotal.TabIndex = 9
        ' 
        ' txtPrimerAbono
        ' 
        txtPrimerAbono.Location = New Point(512, 65)
        txtPrimerAbono.Name = "txtPrimerAbono"
        txtPrimerAbono.Size = New Size(100, 23)
        txtPrimerAbono.TabIndex = 10
        ' 
        ' lblPrimerAbono
        ' 
        lblPrimerAbono.AutoSize = True
        lblPrimerAbono.Location = New Point(412, 73)
        lblPrimerAbono.Name = "lblPrimerAbono"
        lblPrimerAbono.Size = New Size(79, 15)
        lblPrimerAbono.TabIndex = 11
        lblPrimerAbono.Text = "Primer abono"
        ' 
        ' btnRegistrarMembresia
        ' 
        btnRegistrarMembresia.Location = New Point(197, 108)
        btnRegistrarMembresia.Name = "btnRegistrarMembresia"
        btnRegistrarMembresia.Size = New Size(75, 23)
        btnRegistrarMembresia.TabIndex = 12
        btnRegistrarMembresia.Text = "Registrar membresía"
        btnRegistrarMembresia.UseVisualStyleBackColor = True
        ' 
        ' btnLimpiar
        ' 
        btnLimpiar.Location = New Point(335, 108)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(75, 23)
        btnLimpiar.TabIndex = 13
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = True
        ' 
        ' dgvMembresias
        ' 
        dgvMembresias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMembresias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMembresias.Location = New Point(157, 196)
        dgvMembresias.MultiSelect = False
        dgvMembresias.Name = "dgvMembresias"
        dgvMembresias.ReadOnly = True
        dgvMembresias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMembresias.Size = New Size(426, 206)
        dgvMembresias.TabIndex = 14
        ' 
        ' frmMembresiasPagos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvMembresias)
        Controls.Add(btnLimpiar)
        Controls.Add(btnRegistrarMembresia)
        Controls.Add(lblPrimerAbono)
        Controls.Add(txtPrimerAbono)
        Controls.Add(txtTotal)
        Controls.Add(dtpFechaVencimiento)
        Controls.Add(dtpFechaInicio)
        Controls.Add(cboTipoMembresia)
        Controls.Add(cboSocio)
        Controls.Add(lblTotal)
        Controls.Add(lblFechaVencimiento)
        Controls.Add(lblFechaInicio)
        Controls.Add(lblTipoMembresia)
        Controls.Add(lblSocio)
        Name = "frmMembresiasPagos"
        Text = "frmMembresiasPagos"
        CType(dgvMembresias, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblSocio As Label
    Friend WithEvents lblTipoMembresia As Label
    Friend WithEvents lblFechaInicio As Label
    Friend WithEvents lblFechaVencimiento As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents cboSocio As ComboBox
    Friend WithEvents cboTipoMembresia As ComboBox
    Friend WithEvents dtpFechaInicio As DateTimePicker
    Friend WithEvents dtpFechaVencimiento As DateTimePicker
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents txtPrimerAbono As TextBox
    Friend WithEvents lblPrimerAbono As Label
    Friend WithEvents btnRegistrarMembresia As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents dgvMembresias As DataGridView
End Class
