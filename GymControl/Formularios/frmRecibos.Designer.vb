<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRecibos
    Inherits Form
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If impresor IsNot Nothing Then impresor.Dispose()
                If ppdVista IsNot Nothing Then ppdVista.Dispose()
                If prdImpresora IsNot Nothing Then prdImpresora.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        tlpContenido = New TableLayoutPanel()
        lblTitulo = New Label()
        flpFiltros = New FlowLayoutPanel()
        lblDesde = New Label()
        dtpDesde = New DateTimePicker()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        lblBuscar = New Label()
        txtBuscar = New TextBox()
        btnCargar = New Button()
        dgvPagos = New DataGridView()
        colRecibo = New DataGridViewTextBoxColumn()
        colFecha = New DataGridViewTextBoxColumn()
        colSocio = New DataGridViewTextBoxColumn()
        colCedula = New DataGridViewTextBoxColumn()
        colPlan = New DataGridViewTextBoxColumn()
        colMetodo = New DataGridViewTextBoxColumn()
        colMonto = New DataGridViewTextBoxColumn()
        colEstado = New DataGridViewTextBoxColumn()
        lblEstado = New Label()
        flpBotones = New FlowLayoutPanel()
        btnVistaPrevia = New Button()
        btnImprimir = New Button()
        btnCerrar = New Button()
        ppdVista = New PrintPreviewDialog()
        prdImpresora = New PrintDialog()
        SuspendLayout()
        tlpContenido.Name = "tlpContenido"
        tlpContenido.Dock = DockStyle.Fill
        tlpContenido.ColumnCount = 1
        tlpContenido.RowCount = 5
        tlpContenido.Padding = New Padding(16)

        tlpContenido.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpContenido.RowStyles.Add(New RowStyle(SizeType.Absolute, 48.0F))
        tlpContenido.RowStyles.Add(New RowStyle(SizeType.Absolute, 90.0F))
        tlpContenido.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpContenido.RowStyles.Add(New RowStyle(SizeType.Absolute, 70.0F))
        tlpContenido.RowStyles.Add(New RowStyle(SizeType.Absolute, 48.0F))
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Text = "R01 · Recibos de pago"
        lblTitulo.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblTitulo.Dock = DockStyle.Fill

        flpFiltros.Name = "flpFiltros"
        flpFiltros.Dock = DockStyle.Fill
        flpFiltros.WrapContents = True

        lblDesde.Name = "lblDesde"
        lblDesde.Text = "Desde"
        lblDesde.AutoSize = True
        lblDesde.Margin = New Padding(3, 8, 3, 3)
        flpFiltros.Controls.Add(lblDesde)

        dtpDesde.Name = "dtpDesde"
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Width = 140
        flpFiltros.Controls.Add(dtpDesde)

        lblHasta.Name = "lblHasta"
        lblHasta.Text = "Hasta"
        lblHasta.AutoSize = True
        lblHasta.Margin = New Padding(3, 8, 3, 3)
        flpFiltros.Controls.Add(lblHasta)

        dtpHasta.Name = "dtpHasta"
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Width = 140
        flpFiltros.Controls.Add(dtpHasta)

        lblBuscar.Name = "lblBuscar"
        lblBuscar.Text = "Socio o cédula"
        lblBuscar.AutoSize = True
        lblBuscar.Margin = New Padding(12, 8, 3, 3)
        flpFiltros.Controls.Add(lblBuscar)

        txtBuscar.Name = "txtBuscar"
        txtBuscar.Width = 210
        txtBuscar.MaxLength = 160
        flpFiltros.Controls.Add(txtBuscar)

        btnCargar.Name = "btnCargar"
        btnCargar.Text = "Buscar pagos"
        btnCargar.Size = New Size(120, 32)
        btnCargar.UseVisualStyleBackColor = True
        flpFiltros.Controls.Add(btnCargar)

        dgvPagos.Name = "dgvPagos"
        dgvPagos.Dock = DockStyle.Fill
        dgvPagos.ReadOnly = True
        dgvPagos.MultiSelect = False
        dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPagos.AllowUserToAddRows = False
        dgvPagos.AllowUserToDeleteRows = False
        dgvPagos.RowHeadersVisible = False
        dgvPagos.AutoGenerateColumns = False
        dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvPagos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        colRecibo.Name = "colRecibo"
        colRecibo.DataPropertyName = "id_pago"
        colRecibo.HeaderText = "Recibo"
        colRecibo.FillWeight = 65.0F
        colRecibo.ReadOnly = True

        colRecibo.DefaultCellStyle.Format = "D6"
        dgvPagos.Columns.Add(colRecibo)
        colFecha.Name = "colFecha"
        colFecha.DataPropertyName = "fecha"
        colFecha.HeaderText = "Fecha y hora"
        colFecha.FillWeight = 145.0F
        colFecha.ReadOnly = True

        colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
        dgvPagos.Columns.Add(colFecha)
        colSocio.Name = "colSocio"
        colSocio.DataPropertyName = "socio"
        colSocio.HeaderText = "Socio"
        colSocio.FillWeight = 210.0F
        colSocio.ReadOnly = True

        dgvPagos.Columns.Add(colSocio)
        colCedula.Name = "colCedula"
        colCedula.DataPropertyName = "cedula"
        colCedula.HeaderText = "Cédula"
        colCedula.FillWeight = 130.0F
        colCedula.ReadOnly = True

        dgvPagos.Columns.Add(colCedula)
        colPlan.Name = "colPlan"
        colPlan.DataPropertyName = "plan"
        colPlan.HeaderText = "Plan"
        colPlan.FillWeight = 100.0F
        colPlan.ReadOnly = True

        dgvPagos.Columns.Add(colPlan)
        colMetodo.Name = "colMetodo"
        colMetodo.DataPropertyName = "metodo_pago"
        colMetodo.HeaderText = "Método"
        colMetodo.FillWeight = 100.0F
        colMetodo.ReadOnly = True

        dgvPagos.Columns.Add(colMetodo)
        colMonto.Name = "colMonto"
        colMonto.DataPropertyName = "monto"
        colMonto.HeaderText = "Monto C$"
        colMonto.FillWeight = 100.0F
        colMonto.ReadOnly = True

        colMonto.DefaultCellStyle.Format = "N2"
        colMonto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvPagos.Columns.Add(colMonto)
        colEstado.Name = "colEstado"
        colEstado.DataPropertyName = "anulado"
        colEstado.HeaderText = "Estado"
        colEstado.FillWeight = 85.0F
        colEstado.ReadOnly = True

        dgvPagos.Columns.Add(colEstado)
        dgvPagos.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        lblEstado.Name = "lblEstado"
        lblEstado.Dock = DockStyle.Fill
        lblEstado.Padding = New Padding(0, 12, 0, 0)
        lblEstado.ForeColor = Color.DimGray

        flpBotones.Name = "flpBotones"
        flpBotones.Dock = DockStyle.Fill

        btnVistaPrevia.Name = "btnVistaPrevia"
        btnVistaPrevia.Text = "Vista previa"
        btnVistaPrevia.Size = New Size(130, 36)
        btnVistaPrevia.UseVisualStyleBackColor = True
        btnVistaPrevia.Enabled = False
        flpBotones.Controls.Add(btnVistaPrevia)

        btnImprimir.Name = "btnImprimir"
        btnImprimir.Text = "Imprimir"
        btnImprimir.Size = New Size(130, 36)
        btnImprimir.UseVisualStyleBackColor = True
        btnImprimir.Enabled = False
        flpBotones.Controls.Add(btnImprimir)

        btnCerrar.Name = "btnCerrar"
        btnCerrar.Text = "Cerrar"
        btnCerrar.Size = New Size(130, 36)
        btnCerrar.UseVisualStyleBackColor = True
        flpBotones.Controls.Add(btnCerrar)

        ppdVista.Name = "ppdVista"
        ppdVista.UseAntiAlias = True
        ppdVista.WindowState = FormWindowState.Maximized
        ppdVista.ShowIcon = False

        prdImpresora.UseEXDialog = True
        prdImpresora.AllowSomePages = False
        prdImpresora.AllowPrintToFile = True

        tlpContenido.Controls.Add(lblTitulo, 0, 0)
        tlpContenido.Controls.Add(flpFiltros, 0, 1)
        tlpContenido.Controls.Add(dgvPagos, 0, 2)
        tlpContenido.Controls.Add(lblEstado, 0, 3)
        tlpContenido.Controls.Add(flpBotones, 0, 4)
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        Font = New Font("Segoe UI", 10.0F)
        ClientSize = New Size(1100, 620)
        MinimumSize = New Size(900, 570)
        StartPosition = FormStartPosition.CenterParent
        Text = "Recibos de pago - GymControl"
        Name = "frmRecibos"
        AcceptButton = btnCargar
        CancelButton = btnCerrar
        Controls.Add(tlpContenido)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlpContenido As TableLayoutPanel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents flpFiltros As FlowLayoutPanel
    Friend WithEvents lblDesde As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents lblBuscar As Label
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents btnCargar As Button
    Friend WithEvents dgvPagos As DataGridView
    Friend WithEvents colRecibo As DataGridViewTextBoxColumn
    Friend WithEvents colFecha As DataGridViewTextBoxColumn
    Friend WithEvents colSocio As DataGridViewTextBoxColumn
    Friend WithEvents colCedula As DataGridViewTextBoxColumn
    Friend WithEvents colPlan As DataGridViewTextBoxColumn
    Friend WithEvents colMetodo As DataGridViewTextBoxColumn
    Friend WithEvents colMonto As DataGridViewTextBoxColumn
    Friend WithEvents colEstado As DataGridViewTextBoxColumn
    Friend WithEvents lblEstado As Label
    Friend WithEvents flpBotones As FlowLayoutPanel
    Friend WithEvents btnVistaPrevia As Button
    Friend WithEvents btnImprimir As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents ppdVista As PrintPreviewDialog
    Friend WithEvents prdImpresora As PrintDialog
End Class
