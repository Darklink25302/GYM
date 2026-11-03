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
        cmbMes = New ComboBox()
        nudAnio = New Label()
        nudAnios = New NumericUpDown()
        btnGenerar = New Button()
        chkAbrirAlTerminar = New CheckBox()
        rdbHorario = New RadioButton()
        CType(nudAnios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rdbCartera
        ' 
        rdbCartera.AutoSize = True
        rdbCartera.Checked = True
        rdbCartera.Location = New Point(117, 50)
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
        rdbInformeMensual.Location = New Point(317, 51)
        rdbInformeMensual.Name = "rdbInformeMensual"
        rdbInformeMensual.Size = New Size(145, 19)
        rdbInformeMensual.TabIndex = 1
        rdbInformeMensual.Text = "Informe mensual (R08)"
        rdbInformeMensual.UseVisualStyleBackColor = True
        ' 
        ' rdbLibroAnual
        ' 
        rdbLibroAnual.AutoSize = True
        rdbLibroAnual.Location = New Point(552, 52)
        rdbLibroAnual.Name = "rdbLibroAnual"
        rdbLibroAnual.Size = New Size(174, 19)
        rdbLibroAnual.TabIndex = 2
        rdbLibroAnual.Text = "Libro de ingresos anual(R09)"
        rdbLibroAnual.UseVisualStyleBackColor = True
        ' 
        ' chkIncluirAlDia
        ' 
        chkIncluirAlDia.AutoSize = True
        chkIncluirAlDia.Location = New Point(117, 124)
        chkIncluirAlDia.Name = "chkIncluirAlDia"
        chkIncluirAlDia.Size = New Size(178, 19)
        chkIncluirAlDia.TabIndex = 3
        chkIncluirAlDia.Text = "Incluir socios al dia (soloR07)"
        chkIncluirAlDia.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(366, 124)
        Label1.Name = "Label1"
        Label1.Size = New Size(29, 15)
        Label1.TabIndex = 4
        Label1.Text = "Mes"
        ' 
        ' cmbMes
        ' 
        cmbMes.FormattingEnabled = True
        cmbMes.Location = New Point(442, 126)
        cmbMes.Name = "cmbMes"
        cmbMes.Size = New Size(121, 23)
        cmbMes.TabIndex = 5
        ' 
        ' nudAnio
        ' 
        nudAnio.AutoSize = True
        nudAnio.Location = New Point(366, 185)
        nudAnio.Name = "nudAnio"
        nudAnio.Size = New Size(29, 15)
        nudAnio.TabIndex = 6
        nudAnio.Text = "Año"
        ' 
        ' nudAnios
        ' 
        nudAnios.Location = New Point(442, 183)
        nudAnios.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnios.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
        nudAnios.Name = "nudAnios"
        nudAnios.Size = New Size(120, 23)
        nudAnios.TabIndex = 7
        nudAnios.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        ' 
        ' btnGenerar
        ' 
        btnGenerar.Location = New Point(645, 304)
        btnGenerar.Name = "btnGenerar"
        btnGenerar.Size = New Size(114, 23)
        btnGenerar.TabIndex = 8
        btnGenerar.Text = "Generar y Guardar"
        btnGenerar.UseVisualStyleBackColor = True
        ' 
        ' chkAbrirAlTerminar
        ' 
        chkAbrirAlTerminar.AutoSize = True
        chkAbrirAlTerminar.Location = New Point(376, 295)
        chkAbrirAlTerminar.Name = "chkAbrirAlTerminar"
        chkAbrirAlTerminar.Size = New Size(112, 19)
        chkAbrirAlTerminar.TabIndex = 9
        chkAbrirAlTerminar.Text = "Abrir al terminar"
        chkAbrirAlTerminar.UseVisualStyleBackColor = True
        ' 
        ' rdbHorario
        ' 
        rdbHorario.AutoSize = True
        rdbHorario.Location = New Point(217, 293)
        rdbHorario.Name = "rdbHorario"
        rdbHorario.Size = New Size(112, 19)
        rdbHorario.TabIndex = 10
        rdbHorario.TabStop = True
        rdbHorario.Text = "Horario semanal"
        rdbHorario.UseVisualStyleBackColor = True
        ' 
        ' frmExportarArchivos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(rdbHorario)
        Controls.Add(chkAbrirAlTerminar)
        Controls.Add(btnGenerar)
        Controls.Add(nudAnios)
        Controls.Add(nudAnio)
        Controls.Add(cmbMes)
        Controls.Add(Label1)
        Controls.Add(chkIncluirAlDia)
        Controls.Add(rdbLibroAnual)
        Controls.Add(rdbInformeMensual)
        Controls.Add(rdbCartera)
        Name = "frmExportarArchivos"
        Text = "frmExportarArchivos"
        CType(nudAnios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rdbCartera As RadioButton
    Friend WithEvents rdbInformeMensual As RadioButton
    Friend WithEvents rdbLibroAnual As RadioButton
    Friend WithEvents chkIncluirAlDia As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbMes As ComboBox
    Friend WithEvents nudAnio As Label
    Friend WithEvents nudAnios As NumericUpDown
    Friend WithEvents btnGenerar As Button
    Friend WithEvents chkAbrirAlTerminar As CheckBox
    Friend WithEvents rdbHorario As RadioButton
End Class
