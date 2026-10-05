<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        grpBoxResumen = New GroupBox()
        lblValorDeudaTotal = New Label()
        lblValorPagosDelMes = New Label()
        lblValorUsuariosActivos = New Label()
        lblDeudaTotal = New Label()
        PictureBox1 = New PictureBox()
        lblPagosDelMes = New Label()
        picBoxCalendario = New PictureBox()
        lblUsuariosActivos = New Label()
        picBoxUsuarios = New PictureBox()
        grpBoxHistorial = New GroupBox()
        btnExportar = New Button()
        btnBuscar = New Button()
        DataGridView1 = New DataGridView()
        grpBoxMensualidades = New GroupBox()
        Chart1 = New DataVisualization.Charting.Chart()
        grpBoxIngresos = New GroupBox()
        Chart2 = New DataVisualization.Charting.Chart()
        btnNuevoUsuario = New Button()
        btnRegistrarPago = New Button()
        btnVerDeudores = New Button()
        lblIngresosTotales = New Label()
        lblGastos = New Label()
        lblValorIngresosTotales = New Label()
        lblValorGastos = New Label()
        grpBoxResumen.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(picBoxCalendario, ComponentModel.ISupportInitialize).BeginInit()
        CType(picBoxUsuarios, ComponentModel.ISupportInitialize).BeginInit()
        grpBoxHistorial.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        grpBoxMensualidades.SuspendLayout()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        grpBoxIngresos.SuspendLayout()
        CType(Chart2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' grpBoxResumen
        ' 
        grpBoxResumen.Controls.Add(lblValorDeudaTotal)
        grpBoxResumen.Controls.Add(lblValorPagosDelMes)
        grpBoxResumen.Controls.Add(lblValorUsuariosActivos)
        grpBoxResumen.Controls.Add(lblDeudaTotal)
        grpBoxResumen.Controls.Add(PictureBox1)
        grpBoxResumen.Controls.Add(lblPagosDelMes)
        grpBoxResumen.Controls.Add(picBoxCalendario)
        grpBoxResumen.Controls.Add(lblUsuariosActivos)
        grpBoxResumen.Controls.Add(picBoxUsuarios)
        grpBoxResumen.Location = New Point(12, 12)
        grpBoxResumen.Name = "grpBoxResumen"
        grpBoxResumen.Size = New Size(428, 196)
        grpBoxResumen.TabIndex = 0
        grpBoxResumen.TabStop = False
        grpBoxResumen.Text = "Resumen General"
        ' 
        ' lblValorDeudaTotal
        ' 
        lblValorDeudaTotal.AutoSize = True
        lblValorDeudaTotal.Location = New Point(347, 161)
        lblValorDeudaTotal.Name = "lblValorDeudaTotal"
        lblValorDeudaTotal.Size = New Size(17, 20)
        lblValorDeudaTotal.TabIndex = 8
        lblValorDeudaTotal.Text = "0"
        ' 
        ' lblValorPagosDelMes
        ' 
        lblValorPagosDelMes.AutoSize = True
        lblValorPagosDelMes.Location = New Point(207, 161)
        lblValorPagosDelMes.Name = "lblValorPagosDelMes"
        lblValorPagosDelMes.Size = New Size(17, 20)
        lblValorPagosDelMes.TabIndex = 7
        lblValorPagosDelMes.Text = "0"
        ' 
        ' lblValorUsuariosActivos
        ' 
        lblValorUsuariosActivos.AutoSize = True
        lblValorUsuariosActivos.Location = New Point(62, 161)
        lblValorUsuariosActivos.Name = "lblValorUsuariosActivos"
        lblValorUsuariosActivos.Size = New Size(17, 20)
        lblValorUsuariosActivos.TabIndex = 6
        lblValorUsuariosActivos.Text = "0"
        ' 
        ' lblDeudaTotal
        ' 
        lblDeudaTotal.AutoSize = True
        lblDeudaTotal.Location = New Point(316, 132)
        lblDeudaTotal.Name = "lblDeudaTotal"
        lblDeudaTotal.Size = New Size(88, 20)
        lblDeudaTotal.TabIndex = 5
        lblDeudaTotal.Text = "Deuda total"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Location = New Point(305, 26)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(105, 103)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 4
        PictureBox1.TabStop = False
        ' 
        ' lblPagosDelMes
        ' 
        lblPagosDelMes.AutoSize = True
        lblPagosDelMes.Location = New Point(160, 132)
        lblPagosDelMes.Name = "lblPagosDelMes"
        lblPagosDelMes.Size = New Size(104, 20)
        lblPagosDelMes.TabIndex = 3
        lblPagosDelMes.Text = "Pagos del Mes"
        ' 
        ' picBoxCalendario
        ' 
        picBoxCalendario.BackgroundImage = CType(resources.GetObject("picBoxCalendario.BackgroundImage"), Image)
        picBoxCalendario.BackgroundImageLayout = ImageLayout.Stretch
        picBoxCalendario.Location = New Point(162, 26)
        picBoxCalendario.Name = "picBoxCalendario"
        picBoxCalendario.Size = New Size(105, 103)
        picBoxCalendario.SizeMode = PictureBoxSizeMode.StretchImage
        picBoxCalendario.TabIndex = 2
        picBoxCalendario.TabStop = False
        ' 
        ' lblUsuariosActivos
        ' 
        lblUsuariosActivos.AutoSize = True
        lblUsuariosActivos.Location = New Point(11, 132)
        lblUsuariosActivos.Name = "lblUsuariosActivos"
        lblUsuariosActivos.Size = New Size(117, 20)
        lblUsuariosActivos.TabIndex = 1
        lblUsuariosActivos.Text = "Usuarios Activos"
        ' 
        ' picBoxUsuarios
        ' 
        picBoxUsuarios.BackColor = Color.Transparent
        picBoxUsuarios.BackgroundImage = CType(resources.GetObject("picBoxUsuarios.BackgroundImage"), Image)
        picBoxUsuarios.BackgroundImageLayout = ImageLayout.Stretch
        picBoxUsuarios.Location = New Point(15, 26)
        picBoxUsuarios.Name = "picBoxUsuarios"
        picBoxUsuarios.Size = New Size(105, 103)
        picBoxUsuarios.SizeMode = PictureBoxSizeMode.StretchImage
        picBoxUsuarios.TabIndex = 0
        picBoxUsuarios.TabStop = False
        ' 
        ' grpBoxHistorial
        ' 
        grpBoxHistorial.Controls.Add(btnExportar)
        grpBoxHistorial.Controls.Add(btnBuscar)
        grpBoxHistorial.Controls.Add(DataGridView1)
        grpBoxHistorial.Location = New Point(12, 226)
        grpBoxHistorial.Name = "grpBoxHistorial"
        grpBoxHistorial.Size = New Size(428, 322)
        grpBoxHistorial.TabIndex = 1
        grpBoxHistorial.TabStop = False
        grpBoxHistorial.Text = "Historial de Cambios"
        ' 
        ' btnExportar
        ' 
        btnExportar.Location = New Point(316, 276)
        btnExportar.Name = "btnExportar"
        btnExportar.Size = New Size(94, 29)
        btnExportar.TabIndex = 2
        btnExportar.Text = "Exportar"
        btnExportar.UseVisualStyleBackColor = True
        ' 
        ' btnBuscar
        ' 
        btnBuscar.Location = New Point(216, 276)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(94, 29)
        btnBuscar.TabIndex = 1
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(15, 26)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(395, 232)
        DataGridView1.TabIndex = 0
        ' 
        ' grpBoxMensualidades
        ' 
        grpBoxMensualidades.Controls.Add(Chart1)
        grpBoxMensualidades.Location = New Point(464, 12)
        grpBoxMensualidades.Name = "grpBoxMensualidades"
        grpBoxMensualidades.Size = New Size(493, 252)
        grpBoxMensualidades.TabIndex = 1
        grpBoxMensualidades.TabStop = False
        grpBoxMensualidades.Text = "Mensualidades"
        ' 
        ' Chart1
        ' 
        ChartArea3.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Chart1.Legends.Add(Legend3)
        Chart1.Location = New Point(27, 26)
        Chart1.Name = "Chart1"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = DataVisualization.Charting.SeriesChartType.Pie
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Chart1.Series.Add(Series3)
        Chart1.Size = New Size(460, 209)
        Chart1.TabIndex = 0
        Chart1.Text = "Chart1"
        ' 
        ' grpBoxIngresos
        ' 
        grpBoxIngresos.Controls.Add(Chart2)
        grpBoxIngresos.Location = New Point(464, 280)
        grpBoxIngresos.Name = "grpBoxIngresos"
        grpBoxIngresos.Size = New Size(493, 268)
        grpBoxIngresos.TabIndex = 2
        grpBoxIngresos.TabStop = False
        grpBoxIngresos.Text = "Ingresos del último mes"
        ' 
        ' Chart2
        ' 
        ChartArea4.Name = "ChartArea1"
        Chart2.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Chart2.Legends.Add(Legend4)
        Chart2.Location = New Point(27, 26)
        Chart2.Name = "Chart2"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Chart2.Series.Add(Series4)
        Chart2.Size = New Size(460, 225)
        Chart2.TabIndex = 0
        Chart2.Text = "Chart2"
        ' 
        ' btnNuevoUsuario
        ' 
        btnNuevoUsuario.Location = New Point(102, 596)
        btnNuevoUsuario.Name = "btnNuevoUsuario"
        btnNuevoUsuario.Size = New Size(134, 29)
        btnNuevoUsuario.TabIndex = 3
        btnNuevoUsuario.Text = "Nuevo Usuario"
        btnNuevoUsuario.UseVisualStyleBackColor = True
        ' 
        ' btnRegistrarPago
        ' 
        btnRegistrarPago.Location = New Point(242, 596)
        btnRegistrarPago.Name = "btnRegistrarPago"
        btnRegistrarPago.Size = New Size(134, 29)
        btnRegistrarPago.TabIndex = 4
        btnRegistrarPago.Text = "Registrar Pago"
        btnRegistrarPago.UseVisualStyleBackColor = True
        ' 
        ' btnVerDeudores
        ' 
        btnVerDeudores.Location = New Point(382, 596)
        btnVerDeudores.Name = "btnVerDeudores"
        btnVerDeudores.Size = New Size(134, 29)
        btnVerDeudores.TabIndex = 5
        btnVerDeudores.Text = "Ver Deudores"
        btnVerDeudores.UseVisualStyleBackColor = True
        ' 
        ' lblIngresosTotales
        ' 
        lblIngresosTotales.AutoSize = True
        lblIngresosTotales.Location = New Point(732, 568)
        lblIngresosTotales.Name = "lblIngresosTotales"
        lblIngresosTotales.Size = New Size(118, 20)
        lblIngresosTotales.TabIndex = 6
        lblIngresosTotales.Text = "Ingresos Totales:"
        ' 
        ' lblGastos
        ' 
        lblGastos.AutoSize = True
        lblGastos.Location = New Point(794, 605)
        lblGastos.Name = "lblGastos"
        lblGastos.Size = New Size(56, 20)
        lblGastos.TabIndex = 7
        lblGastos.Text = "Gastos:"
        ' 
        ' lblValorIngresosTotales
        ' 
        lblValorIngresosTotales.AutoSize = True
        lblValorIngresosTotales.Location = New Point(873, 568)
        lblValorIngresosTotales.Name = "lblValorIngresosTotales"
        lblValorIngresosTotales.Size = New Size(17, 20)
        lblValorIngresosTotales.TabIndex = 8
        lblValorIngresosTotales.Text = "0"
        ' 
        ' lblValorGastos
        ' 
        lblValorGastos.AutoSize = True
        lblValorGastos.Location = New Point(873, 605)
        lblValorGastos.Name = "lblValorGastos"
        lblValorGastos.Size = New Size(17, 20)
        lblValorGastos.TabIndex = 9
        lblValorGastos.Text = "0"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1002, 681)
        Controls.Add(lblValorGastos)
        Controls.Add(lblValorIngresosTotales)
        Controls.Add(lblGastos)
        Controls.Add(lblIngresosTotales)
        Controls.Add(btnVerDeudores)
        Controls.Add(btnRegistrarPago)
        Controls.Add(btnNuevoUsuario)
        Controls.Add(grpBoxIngresos)
        Controls.Add(grpBoxMensualidades)
        Controls.Add(grpBoxHistorial)
        Controls.Add(grpBoxResumen)
        Name = "Form1"
        Text = "Control de Gimnasio"
        grpBoxResumen.ResumeLayout(False)
        grpBoxResumen.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(picBoxCalendario, ComponentModel.ISupportInitialize).EndInit()
        CType(picBoxUsuarios, ComponentModel.ISupportInitialize).EndInit()
        grpBoxHistorial.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        grpBoxMensualidades.ResumeLayout(False)
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        grpBoxIngresos.ResumeLayout(False)
        CType(Chart2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents grpBoxResumen As GroupBox
    Friend WithEvents grpBoxHistorial As GroupBox
    Friend WithEvents grpBoxMensualidades As GroupBox
    Friend WithEvents grpBoxIngresos As GroupBox
    Friend WithEvents btnExportar As Button
    Friend WithEvents btnBuscar As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents btnNuevoUsuario As Button
    Friend WithEvents btnRegistrarPago As Button
    Friend WithEvents btnVerDeudores As Button
    Friend WithEvents lblIngresosTotales As Label
    Friend WithEvents lblGastos As Label
    Friend WithEvents lblValorIngresosTotales As Label
    Friend WithEvents lblValorGastos As Label
    Friend WithEvents picBoxUsuarios As PictureBox
    Friend WithEvents lblUsuariosActivos As Label
    Friend WithEvents picBoxCalendario As PictureBox
    Friend WithEvents lblValorUsuariosActivos As Label
    Friend WithEvents lblDeudaTotal As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblPagosDelMes As Label
    Friend WithEvents lblValorDeudaTotal As Label
    Friend WithEvents lblValorPagosDelMes As Label

End Class
