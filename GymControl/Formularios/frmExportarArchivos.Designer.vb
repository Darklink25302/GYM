<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmExportarArchivos
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
        rdbCartera = New RadioButton()
        rdbInformeMensual = New RadioButton()
        rdbLibroAnual = New RadioButton()
        lblMes = New Label()
        dtpMes = New DateTimePicker()
        lblAnio = New Label()
        nudAnio = New NumericUpDown()
        chkIncluirAlDia = New CheckBox()
        lblRuta = New Label()
        txtRuta = New TextBox()
        btnExaminar = New Button()
        chkAbrirAlTerminar = New CheckBox()
        btnGenerar = New Button()
        btnCerrar = New Button()
        lblEstado = New Label()
        SuspendLayout()
        rdbCartera.Name = "rdbCartera"
        rdbCartera.Location = New Point(25, 25)
        rdbCartera.Size = New Size(650, 30)
        rdbCartera.Text = "R07 · Cartera de saldos pendientes (Excel)"
        Controls.Add(rdbCartera)
        rdbInformeMensual.Name = "rdbInformeMensual"
        rdbInformeMensual.Location = New Point(25, 65)
        rdbInformeMensual.Size = New Size(650, 30)
        rdbInformeMensual.Text = "R08 · Informe gerencial mensual (PDF)"
        Controls.Add(rdbInformeMensual)
        rdbLibroAnual.Name = "rdbLibroAnual"
        rdbLibroAnual.Location = New Point(25, 105)
        rdbLibroAnual.Size = New Size(650, 30)
        rdbLibroAnual.Text = "R09 · Libro anual de ingresos (Excel)"
        Controls.Add(rdbLibroAnual)
        lblMes.Name = "lblMes"
        lblMes.Location = New Point(25, 165)
        lblMes.Size = New Size(120, 25)
        lblMes.Text = "Mes del informe"
        Controls.Add(lblMes)
        dtpMes.Name = "dtpMes"
        dtpMes.Location = New Point(170, 160)
        dtpMes.Size = New Size(230, 28)
        dtpMes.Format = DateTimePickerFormat.Custom
        dtpMes.CustomFormat = "MMMM yyyy"
        dtpMes.ShowUpDown = True
        Controls.Add(dtpMes)
        lblAnio.Name = "lblAnio"
        lblAnio.Location = New Point(430, 165)
        lblAnio.Size = New Size(100, 25)
        lblAnio.Text = "Año del libro"
        Controls.Add(lblAnio)
        nudAnio.Name = "nudAnio"
        nudAnio.Location = New Point(545, 160)
        nudAnio.Size = New Size(130, 28)
        nudAnio.Minimum = 1900D
        nudAnio.Maximum = 9998D
        nudAnio.Value = 2026D
        Controls.Add(nudAnio)
        chkIncluirAlDia.Name = "chkIncluirAlDia"
        chkIncluirAlDia.Location = New Point(25, 215)
        chkIncluirAlDia.Size = New Size(650, 30)
        chkIncluirAlDia.Text = "Incluir membresías sin saldo pendiente en la cartera"
        Controls.Add(chkIncluirAlDia)
        lblRuta.Name = "lblRuta"
        lblRuta.Location = New Point(25, 270)
        lblRuta.Size = New Size(650, 25)
        lblRuta.Text = "Archivo de destino"
        Controls.Add(lblRuta)
        txtRuta.Name = "txtRuta"
        txtRuta.Location = New Point(25, 300)
        txtRuta.Size = New Size(540, 28)
        txtRuta.ReadOnly = True
        Controls.Add(txtRuta)
        btnExaminar.Name = "btnExaminar"
        btnExaminar.Location = New Point(580, 296)
        btnExaminar.Size = New Size(135, 35)
        btnExaminar.Text = "Examinar…"
        Controls.Add(btnExaminar)
        chkAbrirAlTerminar.Name = "chkAbrirAlTerminar"
        chkAbrirAlTerminar.Location = New Point(25, 355)
        chkAbrirAlTerminar.Size = New Size(650, 30)
        chkAbrirAlTerminar.Text = "Abrir el archivo al terminar"
        chkAbrirAlTerminar.Checked = True
        Controls.Add(chkAbrirAlTerminar)
        btnGenerar.Name = "btnGenerar"
        btnGenerar.Location = New Point(25, 410)
        btnGenerar.Size = New Size(240, 40)
        btnGenerar.Text = "Generar y guardar"
        Controls.Add(btnGenerar)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Location = New Point(575, 410)
        btnCerrar.Size = New Size(140, 40)
        btnCerrar.Text = "Cerrar"
        Controls.Add(btnCerrar)
        lblEstado.Name = "lblEstado"
        lblEstado.Location = New Point(25, 475)
        lblEstado.Size = New Size(690, 55)
        lblEstado.Text = "Selecciona un reporte y una ubicación para guardarlo."
        Controls.Add(lblEstado)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(740, 550)
        MinimumSize = New Size(756, 589)
        Name = "frmExportarArchivos"
        Text = "GymControl · Exportar archivos"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents rdbCartera As RadioButton
    Friend WithEvents rdbInformeMensual As RadioButton
    Friend WithEvents rdbLibroAnual As RadioButton
    Friend WithEvents lblMes As Label
    Friend WithEvents dtpMes As DateTimePicker
    Friend WithEvents lblAnio As Label
    Friend WithEvents nudAnio As NumericUpDown
    Friend WithEvents chkIncluirAlDia As CheckBox
    Friend WithEvents lblRuta As Label
    Friend WithEvents txtRuta As TextBox
    Friend WithEvents btnExaminar As Button
    Friend WithEvents chkAbrirAlTerminar As CheckBox
    Friend WithEvents btnGenerar As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblEstado As Label
End Class
