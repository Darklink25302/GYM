<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMembresiasPagos
    Inherits Form
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If impresorConstancia IsNot Nothing Then impresorConstancia.Dispose()
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
        lblTipoMembresia = New Label()
        cboTipoMembresia = New ComboBox()
        lblFechaInicio = New Label()
        dtpFechaInicio = New DateTimePicker()
        lblFechaVencimiento = New Label()
        dtpFechaVencimiento = New DateTimePicker()
        lblTotal = New Label()
        txtTotal = New TextBox()
        lblPrimerAbono = New Label()
        txtPrimerAbono = New TextBox()
        lblMetodo = New Label()
        cboMetodo = New ComboBox()
        chkImprimirAlRegistrar = New CheckBox()
        btnRegistrarMembresia = New Button()
        btnLimpiar = New Button()
        lblMembresias = New Label()
        dgvMembresias = New DataGridView()
        lblPagos = New Label()
        dgvPagos = New DataGridView()
        btnRegistrarPago = New Button()
        btnAnularPago = New Button()
        btnRecibosMembresia = New Button()
        btnConstancia = New Button()
        btnImprimirConstancia = New Button()
        btnPaginaConstancia = New Button()
        ppdConstancia = New PrintPreviewDialog()
        prdConstancia = New PrintDialog()
        psdPaginaConstancia = New PageSetupDialog()
        CType(dgvMembresias, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvPagos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Location = New Point(22, 18)
        lblTitulo.Size = New Size(400, 22)
        lblTitulo.Text = "Membresías y pagos"
        Controls.Add(lblTitulo)
        lblSocio.Name = "lblSocio"
        lblSocio.Location = New Point(22, 56)
        lblSocio.Size = New Size(180, 22)
        lblSocio.Text = "Socio"
        Controls.Add(lblSocio)
        cboSocio.Name = "cboSocio"
        cboSocio.Location = New Point(22, 78)
        cboSocio.Size = New Size(300, 28)
        cboSocio.DropDownStyle = ComboBoxStyle.DropDownList
        Controls.Add(cboSocio)
        lblTipoMembresia.Name = "lblTipoMembresia"
        lblTipoMembresia.Location = New Point(340, 56)
        lblTipoMembresia.Size = New Size(180, 22)
        lblTipoMembresia.Text = "Tipo de membresía"
        Controls.Add(lblTipoMembresia)
        cboTipoMembresia.Name = "cboTipoMembresia"
        cboTipoMembresia.Location = New Point(340, 78)
        cboTipoMembresia.Size = New Size(200, 28)
        cboTipoMembresia.DropDownStyle = ComboBoxStyle.DropDownList
        Controls.Add(cboTipoMembresia)
        lblFechaInicio.Name = "lblFechaInicio"
        lblFechaInicio.Location = New Point(560, 56)
        lblFechaInicio.Size = New Size(180, 22)
        lblFechaInicio.Text = "Inicio"
        Controls.Add(lblFechaInicio)
        dtpFechaInicio.Name = "dtpFechaInicio"
        dtpFechaInicio.Location = New Point(560, 78)
        dtpFechaInicio.Size = New Size(140, 28)
        dtpFechaInicio.Format = DateTimePickerFormat.Short
        Controls.Add(dtpFechaInicio)
        lblFechaVencimiento.Name = "lblFechaVencimiento"
        lblFechaVencimiento.Location = New Point(720, 56)
        lblFechaVencimiento.Size = New Size(180, 22)
        lblFechaVencimiento.Text = "Vencimiento"
        Controls.Add(lblFechaVencimiento)
        dtpFechaVencimiento.Name = "dtpFechaVencimiento"
        dtpFechaVencimiento.Location = New Point(720, 78)
        dtpFechaVencimiento.Size = New Size(140, 28)
        dtpFechaVencimiento.Format = DateTimePickerFormat.Short
        Controls.Add(dtpFechaVencimiento)
        lblTotal.Name = "lblTotal"
        lblTotal.Location = New Point(22, 124)
        lblTotal.Size = New Size(180, 22)
        lblTotal.Text = "Precio pactado C$"
        Controls.Add(lblTotal)
        txtTotal.Name = "txtTotal"
        txtTotal.Location = New Point(22, 147)
        txtTotal.Size = New Size(130, 28)
        Controls.Add(txtTotal)
        lblPrimerAbono.Name = "lblPrimerAbono"
        lblPrimerAbono.Location = New Point(180, 124)
        lblPrimerAbono.Size = New Size(180, 22)
        lblPrimerAbono.Text = "Primer abono C$"
        Controls.Add(lblPrimerAbono)
        txtPrimerAbono.Name = "txtPrimerAbono"
        txtPrimerAbono.Location = New Point(180, 147)
        txtPrimerAbono.Size = New Size(130, 28)
        Controls.Add(txtPrimerAbono)
        lblMetodo.Name = "lblMetodo"
        lblMetodo.Location = New Point(340, 124)
        lblMetodo.Size = New Size(180, 22)
        lblMetodo.Text = "Método de pago"
        Controls.Add(lblMetodo)
        cboMetodo.Name = "cboMetodo"
        cboMetodo.Location = New Point(340, 147)
        cboMetodo.Size = New Size(200, 28)
        cboMetodo.DropDownStyle = ComboBoxStyle.DropDownList
        Controls.Add(cboMetodo)
        chkImprimirAlRegistrar.Name = "chkImprimirAlRegistrar"
        chkImprimirAlRegistrar.Location = New Point(560, 147)
        chkImprimirAlRegistrar.Size = New Size(400, 30)
        chkImprimirAlRegistrar.Text = "Imprimir recibo al registrar el pago"
        chkImprimirAlRegistrar.Checked = True
        Controls.Add(chkImprimirAlRegistrar)
        btnRegistrarMembresia.Name = "btnRegistrarMembresia"
        btnRegistrarMembresia.Location = New Point(22, 192)
        btnRegistrarMembresia.Size = New Size(210, 34)
        btnRegistrarMembresia.Text = "Registrar membresía"
        Controls.Add(btnRegistrarMembresia)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Location = New Point(246, 192)
        btnLimpiar.Size = New Size(105, 34)
        btnLimpiar.Text = "Limpiar"
        Controls.Add(btnLimpiar)
        lblMembresias.Name = "lblMembresias"
        lblMembresias.Location = New Point(22, 244)
        lblMembresias.Size = New Size(400, 22)
        lblMembresias.Text = "Membresías registradas"
        Controls.Add(lblMembresias)
        dgvMembresias.Name = "dgvMembresias"
        dgvMembresias.Location = New Point(22, 269)
        dgvMembresias.Size = New Size(1056, 160)
        dgvMembresias.ReadOnly = True
        dgvMembresias.MultiSelect = False
        dgvMembresias.AllowUserToAddRows = False
        dgvMembresias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMembresias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMembresias.RowHeadersVisible = False
        Controls.Add(dgvMembresias)
        lblPagos.Name = "lblPagos"
        lblPagos.Location = New Point(22, 447)
        lblPagos.Size = New Size(600, 22)
        lblPagos.Text = "Pagos de la membresía seleccionada"
        Controls.Add(lblPagos)
        dgvPagos.Name = "dgvPagos"
        dgvPagos.Location = New Point(22, 471)
        dgvPagos.Size = New Size(1056, 155)
        dgvPagos.ReadOnly = True
        dgvPagos.MultiSelect = False
        dgvPagos.AllowUserToAddRows = False
        dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPagos.RowHeadersVisible = False
        Controls.Add(dgvPagos)
        btnRegistrarPago.Name = "btnRegistrarPago"
        btnRegistrarPago.Location = New Point(22, 647)
        btnRegistrarPago.Size = New Size(145, 34)
        btnRegistrarPago.Text = "Registrar pago"
        btnRegistrarPago.Enabled = False
        Controls.Add(btnRegistrarPago)
        btnAnularPago.Name = "btnAnularPago"
        btnAnularPago.Location = New Point(177, 647)
        btnAnularPago.Size = New Size(130, 34)
        btnAnularPago.Text = "Anular pago"
        btnAnularPago.Enabled = False
        Controls.Add(btnAnularPago)
        btnRecibosMembresia.Name = "btnRecibosMembresia"
        btnRecibosMembresia.Location = New Point(317, 647)
        btnRecibosMembresia.Size = New Size(120, 34)
        btnRecibosMembresia.Text = "Recibos"
        btnRecibosMembresia.Enabled = False
        Controls.Add(btnRecibosMembresia)
        btnConstancia.Name = "btnConstancia"
        btnConstancia.Location = New Point(447, 647)
        btnConstancia.Size = New Size(195, 34)
        btnConstancia.Text = "Vista previa constancia"
        btnConstancia.Enabled = False
        Controls.Add(btnConstancia)
        btnImprimirConstancia.Name = "btnImprimirConstancia"
        btnImprimirConstancia.Location = New Point(652, 647)
        btnImprimirConstancia.Size = New Size(185, 34)
        btnImprimirConstancia.Text = "Imprimir constancia"
        btnImprimirConstancia.Enabled = False
        Controls.Add(btnImprimirConstancia)
        btnPaginaConstancia.Name = "btnPaginaConstancia"
        btnPaginaConstancia.Location = New Point(847, 647)
        btnPaginaConstancia.Size = New Size(200, 34)
        btnPaginaConstancia.Text = "Configurar página"
        btnPaginaConstancia.Enabled = False
        Controls.Add(btnPaginaConstancia)
        components.Add(ppdConstancia)
        ppdConstancia.UseAntiAlias = True
        ppdConstancia.WindowState = FormWindowState.Maximized
        ppdConstancia.ShowIcon = False
        components.Add(prdConstancia)
        prdConstancia.UseEXDialog = True
        prdConstancia.AllowSomePages = False
        components.Add(psdPaginaConstancia)
        psdPaginaConstancia.EnableMetric = True
        psdPaginaConstancia.AllowPaper = False
        psdPaginaConstancia.AllowOrientation = False
        psdPaginaConstancia.AllowMargins = True
        psdPaginaConstancia.AllowPrinter = True
        psdPaginaConstancia.MinMargins = New System.Drawing.Printing.Margins(25, 25, 25, 25)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1100, 700)
        MinimumSize = New Size(1116, 739)
        Name = "frmMembresiasPagos"
        Text = "GymControl · Membresías y pagos"
        StartPosition = FormStartPosition.CenterParent
        CType(dgvMembresias, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvPagos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSocio As Label
    Friend WithEvents cboSocio As ComboBox
    Friend WithEvents lblTipoMembresia As Label
    Friend WithEvents cboTipoMembresia As ComboBox
    Friend WithEvents lblFechaInicio As Label
    Friend WithEvents dtpFechaInicio As DateTimePicker
    Friend WithEvents lblFechaVencimiento As Label
    Friend WithEvents dtpFechaVencimiento As DateTimePicker
    Friend WithEvents lblTotal As Label
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents lblPrimerAbono As Label
    Friend WithEvents txtPrimerAbono As TextBox
    Friend WithEvents lblMetodo As Label
    Friend WithEvents cboMetodo As ComboBox
    Friend WithEvents chkImprimirAlRegistrar As CheckBox
    Friend WithEvents btnRegistrarMembresia As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents lblMembresias As Label
    Friend WithEvents dgvMembresias As DataGridView
    Friend WithEvents lblPagos As Label
    Friend WithEvents dgvPagos As DataGridView
    Friend WithEvents btnRegistrarPago As Button
    Friend WithEvents btnAnularPago As Button
    Friend WithEvents btnRecibosMembresia As Button
    Friend WithEvents btnConstancia As Button
    Friend WithEvents btnImprimirConstancia As Button
    Friend WithEvents btnPaginaConstancia As Button
    Friend WithEvents ppdConstancia As PrintPreviewDialog
    Friend WithEvents prdConstancia As PrintDialog
    Friend WithEvents psdPaginaConstancia As PageSetupDialog
End Class
