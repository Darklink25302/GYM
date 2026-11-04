<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPortalSocio
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
        cboSocio = New ComboBox()
        btnConsultar = New Button()
        dgvPagos = New DataGridView()
        dgvMembresias = New DataGridView()
        lblSaldo = New Label()
        btnPortalSocio = New Button()
        CType(dgvPagos, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvMembresias, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cboSocio
        ' 
        cboSocio.FormattingEnabled = True
        cboSocio.Location = New Point(408, 11)
        cboSocio.Name = "cboSocio"
        cboSocio.Size = New Size(344, 23)
        cboSocio.TabIndex = 0
        ' 
        ' btnConsultar
        ' 
        btnConsultar.Location = New Point(135, 11)
        btnConsultar.Name = "btnConsultar"
        btnConsultar.Size = New Size(75, 23)
        btnConsultar.TabIndex = 1
        btnConsultar.Text = "Consultar"
        btnConsultar.UseVisualStyleBackColor = True
        ' 
        ' dgvPagos
        ' 
        dgvPagos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPagos.Location = New Point(444, 206)
        dgvPagos.Name = "dgvPagos"
        dgvPagos.Size = New Size(344, 232)
        dgvPagos.TabIndex = 2
        ' 
        ' dgvMembresias
        ' 
        dgvMembresias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMembresias.Location = New Point(37, 206)
        dgvMembresias.Name = "dgvMembresias"
        dgvMembresias.Size = New Size(324, 232)
        dgvMembresias.TabIndex = 3
        ' 
        ' lblSaldo
        ' 
        lblSaldo.AutoSize = True
        lblSaldo.Location = New Point(346, 90)
        lblSaldo.Name = "lblSaldo"
        lblSaldo.Size = New Size(136, 15)
        lblSaldo.TabIndex = 4
        lblSaldo.Text = "Saldo pendiente: C$ 0.00"
        ' 
        ' btnPortalSocio
        ' 
        btnPortalSocio.Location = New Point(135, 60)
        btnPortalSocio.Name = "btnPortalSocio"
        btnPortalSocio.Size = New Size(102, 23)
        btnPortalSocio.TabIndex = 5
        btnPortalSocio.Text = "Portal del socio"
        btnPortalSocio.UseVisualStyleBackColor = True
        ' 
        ' frmPortalSocio
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnPortalSocio)
        Controls.Add(lblSaldo)
        Controls.Add(dgvMembresias)
        Controls.Add(dgvPagos)
        Controls.Add(btnConsultar)
        Controls.Add(cboSocio)
        Name = "frmPortalSocio"
        Text = "frmPortalSocio"
        CType(dgvPagos, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvMembresias, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cboSocio As ComboBox
    Friend WithEvents btnConsultar As Button
    Friend WithEvents dgvPagos As DataGridView
    Friend WithEvents dgvMembresias As DataGridView
    Friend WithEvents lblSaldo As Label
    Friend WithEvents btnPortalSocio As Button
End Class
