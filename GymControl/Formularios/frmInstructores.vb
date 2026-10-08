Public Class frmInstructores

    Private idSeleccionado As Integer = 0

    Private Sub frmInstructores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ExigirAdministrador()
            Text = "Instructores"
            txtNombre.MaxLength = 100
            txtApellido.MaxLength = 100
            txtEspecialidad.MaxLength = 100
            txtTelefono.MaxLength = 30
            txtEmail.MaxLength = 150
            dgvInstructores.ReadOnly = True
            dgvInstructores.AllowUserToAddRows = False
            dgvInstructores.AllowUserToDeleteRows = False
            dgvInstructores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvInstructores.MultiSelect = False
            dgvInstructores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvInstructores.Height = 160
            CargarInstructores()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Instructores")
            Close()
        End Try
    End Sub

    Private Sub CargarInstructores()
        dgvInstructores.DataSource = InstructorDAO.ObtenerTodos()
        dgvInstructores.ClearSelection()
    End Sub

    Private Sub Limpiar()
        idSeleccionado = 0
        txtNombre.Clear()
        txtApellido.Clear()
        txtEspecialidad.Clear()
        txtTelefono.Clear()
        txtEmail.Clear()
        chkActivo.Checked = True
        dgvInstructores.ClearSelection()
        txtNombre.Focus()
    End Sub

    Private Function LeerInstructor() As Instructor
        If txtNombre.Text.Trim() = "" OrElse txtApellido.Text.Trim() = "" Then
            Throw New ArgumentException("Nombre y apellido son obligatorios.")
        End If
        Return New Instructor With {
            .IdInstructor = idSeleccionado,
            .Nombre = txtNombre.Text.Trim(),
            .Apellido = txtApellido.Text.Trim(),
            .Especialidad = txtEspecialidad.Text.Trim(),
            .Telefono = txtTelefono.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Activo = chkActivo.Checked
        }
    End Function

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If idSeleccionado > 0 Then
                MessageBox.Show("Usa Editar para cambiar el instructor seleccionado o Nuevo para registrar otro.")
                Return
            End If
            If InstructorDAO.Insertar(LeerInstructor()) Then
                CargarInstructores()
                Limpiar()
                MessageBox.Show("Instructor registrado.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Instructores")
        End Try
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Try
            If idSeleccionado = 0 Then
                MessageBox.Show("Selecciona un instructor de la tabla.")
                Return
            End If
            If InstructorDAO.Actualizar(LeerInstructor()) Then
                CargarInstructores()
                Limpiar()
                MessageBox.Show("Instructor actualizado.")
            Else
                MessageBox.Show("El instructor ya no existe. Actualiza la lista.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Instructores")
        End Try
    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click
        Try
            If idSeleccionado = 0 Then
                MessageBox.Show("Selecciona un instructor de la tabla.")
                Return
            End If
            If MessageBox.Show("¿Inactivar este instructor? Se conservará su historial.", "Instructores", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
            InstructorDAO.Inactivar(idSeleccionado)
            CargarInstructores()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Instructores")
        End Try
    End Sub

    Private Sub dgvInstructores_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInstructores.CellClick
        If e.RowIndex < 0 Then Return
        Dim seleccionado = TryCast(dgvInstructores.Rows(e.RowIndex).DataBoundItem, Instructor)
        If seleccionado Is Nothing Then Return
        idSeleccionado = seleccionado.IdInstructor
        txtNombre.Text = seleccionado.Nombre
        txtApellido.Text = seleccionado.Apellido
        txtEspecialidad.Text = seleccionado.Especialidad
        txtTelefono.Text = seleccionado.Telefono
        txtEmail.Text = seleccionado.Email
        chkActivo.Checked = seleccionado.Activo
    End Sub

End Class
