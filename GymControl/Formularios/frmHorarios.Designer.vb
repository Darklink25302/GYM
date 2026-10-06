<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHorarios
    Inherits System.Windows.Forms.Form

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
        pnlDatos = New Panel()
        lblInstructor = New Label()
        lblActividad = New Label()
        lblSala = New Label()
        lblDia = New Label()
        lblInicio = New Label()
        lblFin = New Label()
        cboInstructor = New ComboBox()
        cboActividad = New ComboBox()
        cboSala = New ComboBox()
        cboDia = New ComboBox()
        dtpInicio = New DateTimePicker()
        dtpFin = New DateTimePicker()
        btnNuevo = New Button()
        btnGuardar = New Button()
        btnInactivar = New Button()
        btnActualizar = New Button()
        dgvHorarios = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(dgvHorarios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        pnlDatos.Location = New Point(20, 20)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(940, 155)

        lblInstructor.Text = "Instructor"
        lblInstructor.Location = New Point(5, 10)
        lblInstructor.Size = New Size(95, 25)
        cboInstructor.Location = New Point(105, 8)
        cboInstructor.Name = "cboInstructor"
        cboInstructor.Size = New Size(310, 25)
        cboInstructor.DropDownStyle = ComboBoxStyle.DropDownList
        cboInstructor.TabIndex = 0

        lblActividad.Text = "Actividad"
        lblActividad.Location = New Point(455, 10)
        lblActividad.Size = New Size(95, 25)
        cboActividad.Location = New Point(560, 8)
        cboActividad.Name = "cboActividad"
        cboActividad.Size = New Size(310, 25)
        cboActividad.DropDownStyle = ComboBoxStyle.DropDownList
        cboActividad.TabIndex = 1

        lblSala.Text = "Sala"
        lblSala.Location = New Point(5, 55)
        lblSala.Size = New Size(95, 25)
        cboSala.Location = New Point(105, 53)
        cboSala.Name = "cboSala"
        cboSala.Size = New Size(310, 25)
        cboSala.DropDownStyle = ComboBoxStyle.DropDownList
        cboSala.TabIndex = 2

        lblDia.Text = "Día"
        lblDia.Location = New Point(455, 55)
        lblDia.Size = New Size(95, 25)
        cboDia.Location = New Point(560, 53)
        cboDia.Name = "cboDia"
        cboDia.Size = New Size(310, 25)
        cboDia.DropDownStyle = ComboBoxStyle.DropDownList
        cboDia.TabIndex = 3

        lblInicio.Text = "Hora inicial"
        lblInicio.Location = New Point(5, 105)
        lblInicio.Size = New Size(95, 25)
        dtpInicio.Location = New Point(105, 103)
        dtpInicio.Name = "dtpInicio"
        dtpInicio.Size = New Size(130, 25)
        dtpInicio.Format = DateTimePickerFormat.Custom
        dtpInicio.CustomFormat = "HH:mm"
        dtpInicio.ShowUpDown = True
        dtpInicio.TabIndex = 4

        lblFin.Text = "Hora final"
        lblFin.Location = New Point(455, 105)
        lblFin.Size = New Size(95, 25)
        dtpFin.Location = New Point(560, 103)
        dtpFin.Name = "dtpFin"
        dtpFin.Size = New Size(130, 25)
        dtpFin.Format = DateTimePickerFormat.Custom
        dtpFin.CustomFormat = "HH:mm"
        dtpFin.ShowUpDown = True
        dtpFin.TabIndex = 5

        pnlDatos.Controls.Add(lblInstructor)
        pnlDatos.Controls.Add(cboInstructor)
        pnlDatos.Controls.Add(lblActividad)
        pnlDatos.Controls.Add(cboActividad)
        pnlDatos.Controls.Add(lblSala)
        pnlDatos.Controls.Add(cboSala)
        pnlDatos.Controls.Add(lblDia)
        pnlDatos.Controls.Add(cboDia)
        pnlDatos.Controls.Add(lblInicio)
        pnlDatos.Controls.Add(dtpInicio)
        pnlDatos.Controls.Add(lblFin)
        pnlDatos.Controls.Add(dtpFin)

        btnNuevo.Location = New Point(25, 185)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(110, 35)
        btnNuevo.Text = "Nuevo"
        btnNuevo.TabIndex = 1
        btnGuardar.Location = New Point(150, 185)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(110, 35)
        btnGuardar.Text = "Guardar"
        btnGuardar.TabIndex = 2
        btnInactivar.Location = New Point(275, 185)
        btnInactivar.Name = "btnInactivar"
        btnInactivar.Size = New Size(110, 35)
        btnInactivar.Text = "Inactivar"
        btnInactivar.TabIndex = 3
        btnActualizar.Location = New Point(400, 185)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(110, 35)
        btnActualizar.Text = "Actualizar lista"
        btnActualizar.TabIndex = 4

        dgvHorarios.Location = New Point(25, 240)
        dgvHorarios.Name = "dgvHorarios"
        dgvHorarios.Size = New Size(930, 320)
        dgvHorarios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvHorarios.TabIndex = 5

        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(980, 590)
        MinimumSize = New Size(996, 629)
        StartPosition = FormStartPosition.CenterParent
        Name = "frmHorarios"
        Text = "Horarios"
        Controls.Add(pnlDatos)
        Controls.Add(btnNuevo)
        Controls.Add(btnGuardar)
        Controls.Add(btnInactivar)
        Controls.Add(btnActualizar)
        Controls.Add(dgvHorarios)
        pnlDatos.ResumeLayout(False)
        CType(dgvHorarios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblInstructor As Label
    Friend WithEvents lblActividad As Label
    Friend WithEvents lblSala As Label
    Friend WithEvents lblDia As Label
    Friend WithEvents lblInicio As Label
    Friend WithEvents lblFin As Label
    Friend WithEvents cboInstructor As ComboBox
    Friend WithEvents cboActividad As ComboBox
    Friend WithEvents cboSala As ComboBox
    Friend WithEvents cboDia As ComboBox
    Friend WithEvents dtpInicio As DateTimePicker
    Friend WithEvents dtpFin As DateTimePicker
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnInactivar As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents dgvHorarios As DataGridView
End Class
