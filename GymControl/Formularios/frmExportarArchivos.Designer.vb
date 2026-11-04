<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportarArchivos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        rdbCartera = New RadioButton()
        rdbInformeMensual = New RadioButton()
        rdbLibroAnual = New RadioButton()
        chkIncluirAlDia = New CheckBox()
        Label1 = New Label()
        nudAnio = New Label()
        nudAnios = New NumericUpDown()
        btnGenerar = New Button()
        chkAbrirAlTerminar = New CheckBox()
        rdbHorario = New RadioButton()
        txtRuta = New TextBox()
        btnExaminar = New Button()
        sfdGuardar = New SaveFileDialog()
        prgAvance = New ProgressBar()
        lblEstado = New Label()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        dtpMes = New DateTimePicker()
        Label2 = New Label()
        CType(nudAnios, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' rdbCartera
        ' 
        rdbCartera.AutoSize = True
        rdbCartera.Checked = True
        rdbCartera.Location = New Point(6, 22)
        rdbCartera.Name = "rdbCartera"
        rdbCartera.Size = New Size(145, 19)
        rdbCartera.TabIndex = 0
        rdbCartera.TabStop = True
        rdbCartera.Text = "Cartera de saldos (R07)"
        rdbCartera.UseVisualStyleBackColor = True
        ' 
        ' rdbInformeMensual
        ' 
        rdbInformeMensual.AutoSize = True
        rdbInformeMensual.Location = New Point(6, 47)
        rdbInformeMensual.Name = "rdbInformeMensual"
        rdbInformeMensual.Size = New Size(145, 19)
        rdbInformeMensual.TabIndex = 1
        rdbInformeMensual.Text = "Informe mensual (R08)"
        rdbInformeMensual.UseVisualStyleBackColor = True
        ' 
        ' rdbLibroAnual
        ' 
        rdbLibroAnual.AutoSize = True
        rdbLibroAnual.Location = New Point(6, 80)
        rdbLibroAnual.Name = "rdbLibroAnual"
        rdbLibroAnual.Size = New Size(174, 19)
        rdbLibroAnual.TabIndex = 2
        rdbLibroAnual.Text = "Libro de ingresos anual(R09)"
        rdbLibroAnual.UseVisualStyleBackColor = True
        ' 
        ' chkIncluirAlDia
        ' 
        chkIncluirAlDia.AutoSize = True
        chkIncluirAlDia.Location = New Point(22, 72)
        chkIncluirAlDia.Name = "chkIncluirAlDia"
        chkIncluirAlDia.Size = New Size(178, 19)
        chkIncluirAlDia.TabIndex = 3
        chkIncluirAlDia.Text = "Incluir socios al dia (soloR07)"
        chkIncluirAlDia.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(7, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(93, 15)
        Label1.TabIndex = 4
        Label1.Text = "Mes del informe"
        ' 
        ' nudAnio
        ' 
        nudAnio.AutoSize = True
        nudAnio.Location = New Point(7, 43)
        nudAnio.Name = "nudAnio"
        nudAnio.Size = New Size(75, 15)
        nudAnio.TabIndex = 6
        nudAnio.Text = "Año del libro"
        ' 
        ' nudAnios
        ' 
        nudAnios.Location = New Point(118, 43)
        nudAnios.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnios.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
        nudAnios.Name = "nudAnios"
        nudAnios.Size = New Size(120, 23)
        nudAnios.TabIndex = 7
        nudAnios.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        ' 
        ' btnGenerar
        ' 
        btnGenerar.Location = New Point(128, 338)
        btnGenerar.Name = "btnGenerar"
        btnGenerar.Size = New Size(114, 23)
        btnGenerar.TabIndex = 8
        btnGenerar.Text = "Generar Archivo"
        btnGenerar.UseVisualStyleBackColor = True
        ' 
        ' chkAbrirAlTerminar
        ' 
        chkAbrirAlTerminar.AutoSize = True
        chkAbrirAlTerminar.Location = New Point(22, 91)
        chkAbrirAlTerminar.Name = "chkAbrirAlTerminar"
        chkAbrirAlTerminar.Size = New Size(154, 19)
        chkAbrirAlTerminar.TabIndex = 9
        chkAbrirAlTerminar.Text = "Abrir archivo al terminar"
        chkAbrirAlTerminar.UseVisualStyleBackColor = True
        ' 
        ' rdbHorario
        ' 
        rdbHorario.AutoSize = True
        rdbHorario.Location = New Point(6, 103)
        rdbHorario.Name = "rdbHorario"
        rdbHorario.Size = New Size(112, 19)
        rdbHorario.TabIndex = 10
        rdbHorario.TabStop = True
        rdbHorario.Text = "Horario semanal"
        rdbHorario.UseVisualStyleBackColor = True
        ' 
        ' txtRuta
        ' 
        txtRuta.Location = New Point(189, 278)
        txtRuta.Name = "txtRuta"
        txtRuta.ReadOnly = True
        txtRuta.Size = New Size(482, 23)
        txtRuta.TabIndex = 12
        ' 
        ' btnExaminar
        ' 
        btnExaminar.Location = New Point(696, 278)
        btnExaminar.Name = "btnExaminar"
        btnExaminar.Size = New Size(75, 23)
        btnExaminar.TabIndex = 13
        btnExaminar.Text = "Examinar"
        btnExaminar.UseVisualStyleBackColor = True
        ' 
        ' prgAvance
        ' 
        prgAvance.Location = New Point(259, 338)
        prgAvance.Name = "prgAvance"
        prgAvance.Size = New Size(425, 23)
        prgAvance.Style = ProgressBarStyle.Marquee
        prgAvance.TabIndex = 14
        prgAvance.Visible = False
        ' 
        ' lblEstado
        ' 
        lblEstado.AutoSize = True
        lblEstado.Location = New Point(588, 338)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(0, 15)
        lblEstado.TabIndex = 15
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(rdbCartera)
        GroupBox1.Controls.Add(rdbInformeMensual)
        GroupBox1.Controls.Add(rdbLibroAnual)
        GroupBox1.Controls.Add(rdbHorario)
        GroupBox1.Location = New Point(53, 13)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(216, 129)
        GroupBox1.TabIndex = 16
        GroupBox1.TabStop = False
        GroupBox1.Text = "Archivo a generar"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(dtpMes)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(nudAnio)
        GroupBox2.Controls.Add(nudAnios)
        GroupBox2.Controls.Add(chkIncluirAlDia)
        GroupBox2.Controls.Add(chkAbrirAlTerminar)
        GroupBox2.Location = New Point(302, 13)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(316, 122)
        GroupBox2.TabIndex = 17
        GroupBox2.TabStop = False
        GroupBox2.Text = "Opciones"
        ' 
        ' dtpMes
        ' 
        dtpMes.CustomFormat = "MMM 'de' yyyy"
        dtpMes.Format = DateTimePickerFormat.Custom
        dtpMes.Location = New Point(106, 13)
        dtpMes.Name = "dtpMes"
        dtpMes.Size = New Size(200, 23)
        dtpMes.TabIndex = 10
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(101, 281)
        Label2.Name = "Label2"
        Label2.Size = New Size(65, 15)
        Label2.TabIndex = 18
        Label2.Text = "Guardar en"
        ' 
        ' frmExportarArchivos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label2)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(lblEstado)
        Controls.Add(prgAvance)
        Controls.Add(btnExaminar)
        Controls.Add(txtRuta)
        Controls.Add(btnGenerar)
        Name = "frmExportarArchivos"
        Text = "frmExportarArchivos"
        CType(nudAnios, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rdbCartera As RadioButton
    Friend WithEvents rdbInformeMensual As RadioButton
    Friend WithEvents rdbLibroAnual As RadioButton
    Friend WithEvents chkIncluirAlDia As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents nudAnio As Label
    Friend WithEvents nudAnios As NumericUpDown
    Friend WithEvents btnGenerar As Button
    Friend WithEvents chkAbrirAlTerminar As CheckBox
    Friend WithEvents rdbHorario As RadioButton
    Friend WithEvents txtRuta As TextBox
    Friend WithEvents btnExaminar As Button
    Friend WithEvents sfdGuardar As SaveFileDialog
    Friend WithEvents prgAvance As ProgressBar
    Friend WithEvents lblEstado As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpMes As DateTimePicker
End Class
