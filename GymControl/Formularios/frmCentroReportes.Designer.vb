<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCentroReportes
    Inherits Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        pnlEncabezado = New Panel()
        lblTitulo = New Label()
        tvReportes = New TreeView()
        pnlDetalle = New Panel()
        tlpDetalle = New TableLayoutPanel()
        lblNombreReporte = New Label()
        lblDescripcion = New Label()
        grpParametros = New GroupBox()
        lblEstadoReporte = New Label()
        flpBotones = New FlowLayoutPanel()
        pnlSeleccion = New Panel()
        lblSeleccion = New Label()
        pnlFechas = New Panel()
        lblDesde = New Label()
        dtpDesde = New DateTimePicker()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        lblMetodo = New Label()
        cboMetodo = New ComboBox()
        pnlSocio = New Panel()
        lblSocio = New Label()
        cboSocio = New ComboBox()
        lblSocioSesion = New Label()
        lblCuentaDesde = New Label()
        dtpCuentaDesde = New DateTimePicker()
        pnlDias = New Panel()
        lblDiasAdelante = New Label()
        nudDiasAdelante = New NumericUpDown()
        lblDiasAtras = New Label()
        nudDiasAtras = New NumericUpDown()
        chkSoloConSaldo = New CheckBox()
        chkHorizontal = New CheckBox()
        pnlHorario = New Panel()
        lblInstructor = New Label()
        cboInstructor = New ComboBox()
        lblInstructorSesion = New Label()
        lblSala = New Label()
        cboSala = New ComboBox()
        pnlCartera = New Panel()
        chkIncluirAlDia = New CheckBox()
        pnlMes = New Panel()
        lblMes = New Label()
        dtpMes = New DateTimePicker()
        pnlAnio = New Panel()
        lblAnio = New Label()
        nudAnio = New NumericUpDown()
        btnVistaPrevia = New Button()
        btnImprimir = New Button()
        btnExportarPdf = New Button()
        btnExportarExcel = New Button()
        btnCerrar = New Button()
        stsEstado = New StatusStrip()
        tslEstado = New ToolStripStatusLabel()
        tspProgreso = New ToolStripProgressBar()
        tslUsuario = New ToolStripStatusLabel()
        SuspendLayout()

        pnlEncabezado.Name = "pnlEncabezado"
        pnlEncabezado.Dock = DockStyle.Top
        pnlEncabezado.Height = 64
        pnlEncabezado.BackColor = Color.FromArgb(25, 61, 136)

        lblTitulo.Name = "lblTitulo"
        lblTitulo.Text = "Centro de reportes"
        lblTitulo.ForeColor = Color.White
        lblTitulo.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitulo.AutoSize = False
        lblTitulo.Dock = DockStyle.Fill
        lblTitulo.Padding = New Padding(20, 12, 0, 0)
        pnlEncabezado.Controls.Add(lblTitulo)

        tvReportes.Name = "tvReportes"
        tvReportes.Dock = DockStyle.Left
        tvReportes.Width = 280
        tvReportes.HideSelection = False
        tvReportes.FullRowSelect = True
        tvReportes.ShowLines = False
        tvReportes.ItemHeight = 30
        tvReportes.TabIndex = 0

        pnlDetalle.Name = "pnlDetalle"
        pnlDetalle.Dock = DockStyle.Fill

        tlpDetalle.Name = "tlpDetalle"
        tlpDetalle.Dock = DockStyle.Fill
        tlpDetalle.Padding = New Padding(20)
        tlpDetalle.ColumnCount = 1
        tlpDetalle.RowCount = 5
        pnlDetalle.Controls.Add(tlpDetalle)

        tlpDetalle.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.Absolute, 44.0F))
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.Absolute, 72.0F))
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.Absolute, 226.0F))
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.Absolute, 84.0F))

        lblNombreReporte.Name = "lblNombreReporte"
        lblNombreReporte.Text = "Selecciona un reporte"
        lblNombreReporte.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        lblNombreReporte.Dock = DockStyle.Fill

        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Dock = DockStyle.Fill

        grpParametros.Name = "grpParametros"
        grpParametros.Text = "Parámetros"
        grpParametros.Dock = DockStyle.Fill

        lblEstadoReporte.Name = "lblEstadoReporte"
        lblEstadoReporte.Dock = DockStyle.Fill
        lblEstadoReporte.ForeColor = Color.DimGray
        lblEstadoReporte.Padding = New Padding(0, 12, 0, 0)

        flpBotones.Name = "flpBotones"
        flpBotones.Dock = DockStyle.Fill
        flpBotones.FlowDirection = FlowDirection.LeftToRight
        flpBotones.WrapContents = True

        tlpDetalle.Controls.Add(lblNombreReporte, 0, 0)
        tlpDetalle.Controls.Add(lblDescripcion, 0, 1)
        tlpDetalle.Controls.Add(grpParametros, 0, 2)
        tlpDetalle.Controls.Add(lblEstadoReporte, 0, 3)
        tlpDetalle.Controls.Add(flpBotones, 0, 4)

        pnlSeleccion.Name = "pnlSeleccion"
        pnlSeleccion.Dock = DockStyle.Fill
        pnlSeleccion.Visible = False
        grpParametros.Controls.Add(pnlSeleccion)

        lblSeleccion.Name = "lblSeleccion"
        lblSeleccion.Location = New Point(16, 24)
        lblSeleccion.Size = New Size(600, 100)
        lblSeleccion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pnlSeleccion.Controls.Add(lblSeleccion)

        pnlFechas.Name = "pnlFechas"
        pnlFechas.Dock = DockStyle.Fill
        pnlFechas.Visible = False
        grpParametros.Controls.Add(pnlFechas)

        lblDesde.Name = "lblDesde"
        lblDesde.Text = "Desde"
        lblDesde.Location = New Point(16, 28)
        lblDesde.Size = New Size(110, 28)
        pnlFechas.Controls.Add(lblDesde)

        dtpDesde.Name = "dtpDesde"
        dtpDesde.Location = New Point(130, 24)
        dtpDesde.Size = New Size(190, 30)
        dtpDesde.Format = DateTimePickerFormat.Short
        pnlFechas.Controls.Add(dtpDesde)

        lblHasta.Name = "lblHasta"
        lblHasta.Text = "Hasta"
        lblHasta.Location = New Point(16, 76)
        lblHasta.Size = New Size(110, 28)
        pnlFechas.Controls.Add(lblHasta)

        dtpHasta.Name = "dtpHasta"
        dtpHasta.Location = New Point(130, 72)
        dtpHasta.Size = New Size(190, 30)
        dtpHasta.Format = DateTimePickerFormat.Short
        pnlFechas.Controls.Add(dtpHasta)

        lblMetodo.Name = "lblMetodo"
        lblMetodo.Text = "Método"
        lblMetodo.Location = New Point(16, 124)
        lblMetodo.Size = New Size(110, 28)
        pnlFechas.Controls.Add(lblMetodo)

        cboMetodo.Name = "cboMetodo"
        cboMetodo.Location = New Point(130, 120)
        cboMetodo.Size = New Size(220, 30)
        cboMetodo.DropDownStyle = ComboBoxStyle.DropDownList
        pnlFechas.Controls.Add(cboMetodo)

        cboMetodo.Items.AddRange(New Object() {"Todos", "Efectivo", "Tarjeta", "Transferencia"})
        cboMetodo.SelectedIndex = 0

        pnlSocio.Name = "pnlSocio"
        pnlSocio.Dock = DockStyle.Fill
        pnlSocio.Visible = False
        grpParametros.Controls.Add(pnlSocio)

        lblSocio.Name = "lblSocio"
        lblSocio.Text = "Socio"
        lblSocio.Location = New Point(16, 28)
        lblSocio.Size = New Size(110, 28)
        pnlSocio.Controls.Add(lblSocio)

        cboSocio.Name = "cboSocio"
        cboSocio.Location = New Point(130, 24)
        cboSocio.Size = New Size(350, 30)
        cboSocio.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cboSocio.DropDownStyle = ComboBoxStyle.DropDownList
        cboSocio.Enabled = False
        pnlSocio.Controls.Add(cboSocio)

        cboSocio.Items.Add("Selecciona un socio")
        cboSocio.SelectedIndex = 0
        lblSocioSesion.Name = "lblSocioSesion"
        lblSocioSesion.Location = New Point(130, 24)
        lblSocioSesion.Size = New Size(390, 60)
        lblSocioSesion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblSocioSesion.Visible = False
        pnlSocio.Controls.Add(lblSocioSesion)

        lblCuentaDesde.Name = "lblCuentaDesde"
        lblCuentaDesde.Text = "Desde"
        lblCuentaDesde.Location = New Point(16, 102)
        lblCuentaDesde.Size = New Size(110, 28)
        pnlSocio.Controls.Add(lblCuentaDesde)

        dtpCuentaDesde.Name = "dtpCuentaDesde"
        dtpCuentaDesde.Location = New Point(130, 98)
        dtpCuentaDesde.Size = New Size(190, 30)
        dtpCuentaDesde.Format = DateTimePickerFormat.Short
        pnlSocio.Controls.Add(dtpCuentaDesde)

        pnlDias.Name = "pnlDias"
        pnlDias.Dock = DockStyle.Fill
        pnlDias.Visible = False
        grpParametros.Controls.Add(pnlDias)

        lblDiasAdelante.Name = "lblDiasAdelante"
        lblDiasAdelante.Text = "Próximos días"
        lblDiasAdelante.Location = New Point(16, 28)
        lblDiasAdelante.Size = New Size(220, 28)
        pnlDias.Controls.Add(lblDiasAdelante)

        nudDiasAdelante.Name = "nudDiasAdelante"
        nudDiasAdelante.Location = New Point(260, 24)
        nudDiasAdelante.Size = New Size(90, 30)
        nudDiasAdelante.Minimum = 1D
        nudDiasAdelante.Maximum = 60D
        nudDiasAdelante.Value = 7D
        pnlDias.Controls.Add(nudDiasAdelante)

        lblDiasAtras.Name = "lblDiasAtras"
        lblDiasAtras.Text = "Vencidas hace hasta días"
        lblDiasAtras.Location = New Point(16, 76)
        lblDiasAtras.Size = New Size(220, 28)
        pnlDias.Controls.Add(lblDiasAtras)

        nudDiasAtras.Name = "nudDiasAtras"
        nudDiasAtras.Location = New Point(260, 72)
        nudDiasAtras.Size = New Size(90, 30)
        nudDiasAtras.Minimum = 0D
        nudDiasAtras.Maximum = 90D
        nudDiasAtras.Value = 15D
        pnlDias.Controls.Add(nudDiasAtras)

        chkSoloConSaldo.Name = "chkSoloConSaldo"
        chkSoloConSaldo.Text = "Solo con saldo"
        chkSoloConSaldo.Location = New Point(16, 116)
        chkSoloConSaldo.Size = New Size(400, 30)
        pnlDias.Controls.Add(chkSoloConSaldo)

        chkHorizontal.Name = "chkHorizontal"
        chkHorizontal.Text = "Hoja horizontal"
        chkHorizontal.Location = New Point(16, 154)
        chkHorizontal.Size = New Size(400, 30)
        pnlDias.Controls.Add(chkHorizontal)

        pnlHorario.Name = "pnlHorario"
        pnlHorario.Dock = DockStyle.Fill
        pnlHorario.Visible = False
        grpParametros.Controls.Add(pnlHorario)

        lblInstructor.Name = "lblInstructor"
        lblInstructor.Text = "Instructor"
        lblInstructor.Location = New Point(16, 28)
        lblInstructor.Size = New Size(110, 28)
        pnlHorario.Controls.Add(lblInstructor)

        cboInstructor.Name = "cboInstructor"
        cboInstructor.Location = New Point(130, 24)
        cboInstructor.Size = New Size(350, 30)
        cboInstructor.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cboInstructor.DropDownStyle = ComboBoxStyle.DropDownList
        cboInstructor.Enabled = False
        pnlHorario.Controls.Add(cboInstructor)

        cboInstructor.Items.Add("Todos los instructores")
        cboInstructor.SelectedIndex = 0
        lblInstructorSesion.Name = "lblInstructorSesion"
        lblInstructorSesion.Location = New Point(130, 24)
        lblInstructorSesion.Size = New Size(390, 60)
        lblInstructorSesion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblInstructorSesion.Visible = False
        pnlHorario.Controls.Add(lblInstructorSesion)

        lblSala.Name = "lblSala"
        lblSala.Text = "Sala"
        lblSala.Location = New Point(16, 108)
        lblSala.Size = New Size(110, 28)
        pnlHorario.Controls.Add(lblSala)

        cboSala.Name = "cboSala"
        cboSala.Location = New Point(130, 104)
        cboSala.Size = New Size(350, 30)
        cboSala.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cboSala.DropDownStyle = ComboBoxStyle.DropDownList
        cboSala.Enabled = False
        pnlHorario.Controls.Add(cboSala)

        cboSala.Items.Add("Todas las salas")
        cboSala.SelectedIndex = 0
        pnlCartera.Name = "pnlCartera"
        pnlCartera.Dock = DockStyle.Fill
        pnlCartera.Visible = False
        grpParametros.Controls.Add(pnlCartera)

        chkIncluirAlDia.Name = "chkIncluirAlDia"
        chkIncluirAlDia.Text = "Incluir membresías con saldo cero"
        chkIncluirAlDia.Location = New Point(16, 24)
        chkIncluirAlDia.Size = New Size(400, 30)
        pnlCartera.Controls.Add(chkIncluirAlDia)

        pnlMes.Name = "pnlMes"
        pnlMes.Dock = DockStyle.Fill
        pnlMes.Visible = False
        grpParametros.Controls.Add(pnlMes)

        lblMes.Name = "lblMes"
        lblMes.Text = "Mes del informe"
        lblMes.Location = New Point(16, 28)
        lblMes.Size = New Size(150, 28)
        pnlMes.Controls.Add(lblMes)

        dtpMes.Name = "dtpMes"
        dtpMes.Location = New Point(180, 24)
        dtpMes.Size = New Size(190, 30)
        dtpMes.Format = DateTimePickerFormat.Custom
        pnlMes.Controls.Add(dtpMes)

        dtpMes.CustomFormat = "MMMM 'de' yyyy"
        dtpMes.ShowUpDown = True

        pnlAnio.Name = "pnlAnio"
        pnlAnio.Dock = DockStyle.Fill
        pnlAnio.Visible = False
        grpParametros.Controls.Add(pnlAnio)

        lblAnio.Name = "lblAnio"
        lblAnio.Text = "Año del libro"
        lblAnio.Location = New Point(16, 28)
        lblAnio.Size = New Size(150, 28)
        pnlAnio.Controls.Add(lblAnio)

        nudAnio.Name = "nudAnio"
        nudAnio.Location = New Point(180, 24)
        nudAnio.Size = New Size(110, 30)
        nudAnio.Minimum = 2024D
        nudAnio.Maximum = 9999D
        nudAnio.Value = 2024D
        pnlAnio.Controls.Add(nudAnio)

        btnVistaPrevia.Name = "btnVistaPrevia"
        btnVistaPrevia.Text = "Vista previa"
        btnVistaPrevia.Size = New Size(120, 36)
        btnVistaPrevia.UseVisualStyleBackColor = True
        btnVistaPrevia.TabIndex = 1
        btnVistaPrevia.Enabled = False
        btnVistaPrevia.Visible = False
        flpBotones.Controls.Add(btnVistaPrevia)

        btnImprimir.Name = "btnImprimir"
        btnImprimir.Text = "Imprimir"
        btnImprimir.Size = New Size(100, 36)
        btnImprimir.UseVisualStyleBackColor = True
        btnImprimir.TabIndex = 2
        btnImprimir.Enabled = False
        btnImprimir.Visible = False
        flpBotones.Controls.Add(btnImprimir)

        btnExportarPdf.Name = "btnExportarPdf"
        btnExportarPdf.Text = "Exportar PDF"
        btnExportarPdf.Size = New Size(130, 36)
        btnExportarPdf.UseVisualStyleBackColor = True
        btnExportarPdf.TabIndex = 3
        btnExportarPdf.Enabled = False
        btnExportarPdf.Visible = False
        flpBotones.Controls.Add(btnExportarPdf)

        btnExportarExcel.Name = "btnExportarExcel"
        btnExportarExcel.Text = "Exportar Excel"
        btnExportarExcel.Size = New Size(135, 36)
        btnExportarExcel.UseVisualStyleBackColor = True
        btnExportarExcel.TabIndex = 4
        btnExportarExcel.Enabled = False
        btnExportarExcel.Visible = False
        flpBotones.Controls.Add(btnExportarExcel)

        btnCerrar.Name = "btnCerrar"
        btnCerrar.Text = "Cerrar"
        btnCerrar.Size = New Size(95, 36)
        btnCerrar.UseVisualStyleBackColor = True
        btnCerrar.TabIndex = 5
        flpBotones.Controls.Add(btnCerrar)

        stsEstado.Name = "stsEstado"
        stsEstado.Dock = DockStyle.Bottom
        stsEstado.Items.AddRange(New ToolStripItem() {tslEstado, tspProgreso, tslUsuario})
        tslEstado.Name = "tslEstado"
        tslEstado.Spring = True
        tslEstado.TextAlign = ContentAlignment.MiddleLeft
        tslEstado.Text = "Selecciona un reporte"
        tspProgreso.Name = "tspProgreso"
        tspProgreso.Style = ProgressBarStyle.Marquee
        tspProgreso.Visible = False
        tslUsuario.Name = "tslUsuario"

        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(1100, 680)
        MinimumSize = New Size(900, 650)
        Font = New Font("Segoe UI", 10.0F)
        Name = "frmCentroReportes"
        Text = "Centro de reportes - GymControl"
        StartPosition = FormStartPosition.CenterParent
        CancelButton = btnCerrar
        Controls.Add(pnlDetalle)
        Controls.Add(tvReportes)
        Controls.Add(stsEstado)
        Controls.Add(pnlEncabezado)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents tvReportes As TreeView
    Friend WithEvents pnlDetalle As Panel
    Friend WithEvents tlpDetalle As TableLayoutPanel
    Friend WithEvents lblNombreReporte As Label
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents grpParametros As GroupBox
    Friend WithEvents lblEstadoReporte As Label
    Friend WithEvents flpBotones As FlowLayoutPanel
    Friend WithEvents pnlSeleccion As Panel
    Friend WithEvents lblSeleccion As Label
    Friend WithEvents pnlFechas As Panel
    Friend WithEvents lblDesde As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents lblMetodo As Label
    Friend WithEvents cboMetodo As ComboBox
    Friend WithEvents pnlSocio As Panel
    Friend WithEvents lblSocio As Label
    Friend WithEvents cboSocio As ComboBox
    Friend WithEvents lblSocioSesion As Label
    Friend WithEvents lblCuentaDesde As Label
    Friend WithEvents dtpCuentaDesde As DateTimePicker
    Friend WithEvents pnlDias As Panel
    Friend WithEvents lblDiasAdelante As Label
    Friend WithEvents nudDiasAdelante As NumericUpDown
    Friend WithEvents lblDiasAtras As Label
    Friend WithEvents nudDiasAtras As NumericUpDown
    Friend WithEvents chkSoloConSaldo As CheckBox
    Friend WithEvents chkHorizontal As CheckBox
    Friend WithEvents pnlHorario As Panel
    Friend WithEvents lblInstructor As Label
    Friend WithEvents cboInstructor As ComboBox
    Friend WithEvents lblInstructorSesion As Label
    Friend WithEvents lblSala As Label
    Friend WithEvents cboSala As ComboBox
    Friend WithEvents pnlCartera As Panel
    Friend WithEvents chkIncluirAlDia As CheckBox
    Friend WithEvents pnlMes As Panel
    Friend WithEvents lblMes As Label
    Friend WithEvents dtpMes As DateTimePicker
    Friend WithEvents pnlAnio As Panel
    Friend WithEvents lblAnio As Label
    Friend WithEvents nudAnio As NumericUpDown
    Friend WithEvents btnVistaPrevia As Button
    Friend WithEvents btnImprimir As Button
    Friend WithEvents btnExportarPdf As Button
    Friend WithEvents btnExportarExcel As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents stsEstado As StatusStrip
    Friend WithEvents tslEstado As ToolStripStatusLabel
    Friend WithEvents tspProgreso As ToolStripProgressBar
    Friend WithEvents tslUsuario As ToolStripStatusLabel
End Class
