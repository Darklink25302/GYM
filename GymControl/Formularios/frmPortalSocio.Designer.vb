<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPortalSocio
    Inherits Form
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        lblTitulo = New Label()
        lblSocio = New Label()
        cboSocio = New ComboBox()
        btnConsultar = New Button()
        btnEstadoCuenta = New Button()
        lblSaldo = New Label()
        dgvMembresias = New DataGridView()
        lblPagos = New Label()
        dgvPagos = New DataGridView()
        btnCerrar = New Button()
        CType(dgvMembresias, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvPagos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Location = New Point(22, 18)
        lblTitulo.Size = New Size(500, 22)
        lblTitulo.Text = "Portal del socio"
        Controls.Add(lblTitulo)
        lblSocio.Name = "lblSocio"
        lblSocio.Location = New Point(22, 60)
        lblSocio.Size = New Size(180, 22)
        lblSocio.Text = "Socio"
        Controls.Add(lblSocio)
        cboSocio.Name = "cboSocio"
        cboSocio.Location = New Point(100, 58)
        cboSocio.Size = New Size(620, 28)
        cboSocio.DropDownStyle = ComboBoxStyle.DropDownList
        Controls.Add(cboSocio)
        btnConsultar.Name = "btnConsultar"
        btnConsultar.Location = New Point(740, 54)
        btnConsultar.Size = New Size(130, 34)
        btnConsultar.Text = "Consultar"
        Controls.Add(btnConsultar)
        btnEstadoCuenta.Name = "btnEstadoCuenta"
        btnEstadoCuenta.Location = New Point(880, 54)
        btnEstadoCuenta.Size = New Size(180, 34)
        btnEstadoCuenta.Text = "Estado de cuenta"
        Controls.Add(btnEstadoCuenta)
        lblSaldo.Name = "lblSaldo"
        lblSaldo.Location = New Point(22, 112)
        lblSaldo.Size = New Size(600, 22)
        lblSaldo.Text = "Saldo pendiente: C$ 0.00"
        Controls.Add(lblSaldo)
        dgvMembresias.Name = "dgvMembresias"
        dgvMembresias.Location = New Point(22, 150)
        dgvMembresias.Size = New Size(1036, 210)
        dgvMembresias.ReadOnly = True
        dgvMembresias.MultiSelect = False
        dgvMembresias.AllowUserToAddRows = False
        dgvMembresias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMembresias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMembresias.RowHeadersVisible = False
        Controls.Add(dgvMembresias)
        lblPagos.Name = "lblPagos"
        lblPagos.Location = New Point(22, 383)
        lblPagos.Size = New Size(700, 22)
        lblPagos.Text = "Pagos de la membresía seleccionada"
        Controls.Add(lblPagos)
        dgvPagos.Name = "dgvPagos"
        dgvPagos.Location = New Point(22, 415)
        dgvPagos.Size = New Size(1036, 200)
        dgvPagos.ReadOnly = True
        dgvPagos.MultiSelect = False
        dgvPagos.AllowUserToAddRows = False
        dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPagos.RowHeadersVisible = False
        Controls.Add(dgvPagos)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Location = New Point(918, 636)
        btnCerrar.Size = New Size(140, 34)
        btnCerrar.Text = "Cerrar"
        Controls.Add(btnCerrar)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1080, 690)
        MinimumSize = New Size(1096, 729)
        Name = "frmPortalSocio"
        Text = "GymControl · Portal del socio"
        StartPosition = FormStartPosition.CenterParent
        CType(dgvMembresias, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvPagos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSocio As Label
    Friend WithEvents cboSocio As ComboBox
    Friend WithEvents btnConsultar As Button
    Friend WithEvents btnEstadoCuenta As Button
    Friend WithEvents lblSaldo As Label
    Friend WithEvents dgvMembresias As DataGridView
    Friend WithEvents lblPagos As Label
    Friend WithEvents dgvPagos As DataGridView
    Friend WithEvents btnCerrar As Button
End Class
