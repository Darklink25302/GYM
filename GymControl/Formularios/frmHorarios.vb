Public Class frmHorarios

    Private idSeleccionado As Integer = 0

    Private Sub frmHorarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ExigirConsultaHorarios()
            dgvHorarios.ReadOnly = True
            dgvHorarios.AllowUserToAddRows = False
            dgvHorarios.AllowUserToDeleteRows = False
            dgvHorarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvHorarios.MultiSelect = False
            dgvHorarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            pnlDatos.Visible = Sesion.Rol = "Administrador"
            btnNuevo.Visible = Sesion.Rol = "Administrador"
            btnGuardar.Visible = Sesion.Rol = "Administrador"
            btnInactivar.Visible = Sesion.Rol = "Administrador"
            If Sesion.Rol = "Administrador" Then
                cboDia.Items.AddRange(New Object() {"Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"})
                CargarCatalogos()
                Limpiar()
            Else
                Text = "Mis horarios"
            End If
            CargarHorarios()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
            Close()
        End Try
    End Sub

    Private Sub CargarCatalogos()
        Dim activos As New List(Of Instructor)
        For Each instructor In InstructorDAO.ObtenerTodos()
            If instructor.Activo Then activos.Add(instructor)
        Next
        cboInstructor.DisplayMember = "NombreCompleto"
        cboInstructor.ValueMember = "IdInstructor"
        cboInstructor.DataSource = activos
        cboActividad.DisplayMember = "Nombre"
        cboActividad.ValueMember = "IdActividad"
        cboActividad.DataSource = CatalogoDAO.ObtenerActividades(True)
        cboSala.DisplayMember = "Nombre"
        cboSala.ValueMember = "IdSala"
        cboSala.DataSource = CatalogoDAO.ObtenerSalas(True)
    End Sub

    Private Sub CargarHorarios()
        dgvHorarios.DataSource = HorarioDAO.ObtenerTodos()
        dgvHorarios.Columns("IdInstructor").Visible = False
        dgvHorarios.Columns("IdActividad").Visible = False
        dgvHorarios.Columns("IdSala").Visible = False
        dgvHorarios.Columns("NombreInstructor").HeaderText = "Instructor"
        dgvHorarios.Columns("NombreActividad").HeaderText = "Actividad"
        dgvHorarios.Columns("NombreSala").HeaderText = "Sala"
        dgvHorarios.ClearSelection()
    End Sub

    Private Sub Limpiar()
        idSeleccionado = 0
        cboInstructor.SelectedIndex = -1
        cboActividad.SelectedIndex = -1
        cboSala.SelectedIndex = -1
        cboDia.SelectedIndex = -1
        dtpInicio.Value = Date.Today.AddHours(8)
        dtpFin.Value = Date.Today.AddHours(9)
        btnGuardar.Text = "Guardar"
        dgvHorarios.ClearSelection()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Try
            ExigirAdministrador()
            CargarCatalogos()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            ExigirAdministrador()
            If cboInstructor.SelectedIndex < 0 OrElse cboActividad.SelectedIndex < 0 OrElse
               cboSala.SelectedIndex < 0 OrElse cboDia.SelectedIndex < 0 Then
                MessageBox.Show("Selecciona instructor, actividad, sala y día.")
                Return
            End If
            If dtpFin.Value.TimeOfDay <= dtpInicio.Value.TimeOfDay Then
                MessageBox.Show("La hora final debe ser mayor que la inicial.")
                Return
            End If
            Dim horario As New Horario With {
                .IdHorario = idSeleccionado,
                .IdInstructor = CInt(cboInstructor.SelectedValue),
                .IdActividad = CInt(cboActividad.SelectedValue),
                .IdSala = CInt(cboSala.SelectedValue),
                .DiaSemana = cboDia.SelectedItem.ToString(),
                .HoraInicio = dtpInicio.Value.TimeOfDay,
                .HoraFin = dtpFin.Value.TimeOfDay
            }
            If HorarioDAO.Guardar(horario) Then
                CargarHorarios()
                Limpiar()
                MessageBox.Show("Horario guardado.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
        End Try
    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click
        Try
            ExigirAdministrador()
            If idSeleccionado = 0 Then
                MessageBox.Show("Selecciona un horario de la tabla.")
                Return
            End If
            If MessageBox.Show("¿Inactivar este horario? Se conservará su historial.", "Horarios", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
            HorarioDAO.Inactivar(idSeleccionado)
            CargarHorarios()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
        End Try
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            ExigirConsultaHorarios()
            If Sesion.Rol = "Administrador" Then
                CargarCatalogos()
                Limpiar()
            End If
            CargarHorarios()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
        End Try
    End Sub

    Private Sub dgvHorarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHorarios.CellClick
        If e.RowIndex < 0 OrElse Sesion.Rol <> "Administrador" Then Return
        Try
            Dim seleccionado = TryCast(dgvHorarios.Rows(e.RowIndex).DataBoundItem, Horario)
            If seleccionado Is Nothing Then Return
            If Not seleccionado.Activo Then
                Limpiar()
                MessageBox.Show("Este horario está inactivo. Puedes consultar su historial en la tabla.")
                Return
            End If
            CargarCatalogos()
            idSeleccionado = seleccionado.IdHorario
            cboInstructor.SelectedValue = seleccionado.IdInstructor
            cboActividad.SelectedValue = seleccionado.IdActividad
            cboSala.SelectedValue = seleccionado.IdSala
            cboDia.SelectedItem = seleccionado.DiaSemana
            dtpInicio.Value = Date.Today.Add(seleccionado.HoraInicio)
            dtpFin.Value = Date.Today.Add(seleccionado.HoraFin)
            btnGuardar.Text = "Editar"
            If cboInstructor.SelectedIndex < 0 OrElse cboActividad.SelectedIndex < 0 OrElse cboSala.SelectedIndex < 0 Then
                MessageBox.Show("Uno de los recursos está inactivo. Elige recursos activos antes de guardar.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Horarios")
        End Try
    End Sub

End Class
