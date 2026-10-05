Public Class frmInstructores

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        txtNombre.Clear()
        txtApellido.Clear()
        txtEspecialidad.Clear()
        txtTelefono.Clear()
        txtEmail.Clear()
        chkActivo.Checked = True
        txtNombre.Focus()
    End Sub

    Private Sub txtNombre_TextChanged(sender As Object, e As EventArgs) Handles txtNombre.TextChanged

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' 1. Validar que los campos obligatorios no estén vacíos
        If txtNombre.Text.Trim() = "" Or txtApellido.Text.Trim() = "" Then
            MessageBox.Show("Nombre y Apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Crear el objeto Instructor
        Dim nuevo As New Instructor With {
            .Nombre = txtNombre.Text.Trim(),
            .Apellido = txtApellido.Text.Trim(),
            .Especialidad = txtEspecialidad.Text.Trim(),
            .Telefono = txtTelefono.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Activo = chkActivo.Checked
        }

        ' 3. Guardar en base de datos
        Try
            If InstructorDAO.Insertar(nuevo) Then
                MessageBox.Show("Instructor guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Limpiar los campos después de guardar
                txtNombre.Clear()
                txtApellido.Clear()
                txtEspecialidad.Clear()
                txtTelefono.Clear()
                txtEmail.Clear()
                chkActivo.Checked = True
            Else
                MessageBox.Show("No se pudo guardar el instructor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        ' 1. Validar que los campos obligatorios no estén vacíos
        If txtNombre.Text.Trim() = "" Or txtApellido.Text.Trim() = "" Then
            MessageBox.Show("Nombre y Apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' NOTA: Para editar, primero necesitas seleccionar un instructor de la tabla.
        ' Este código asume que ya tienes el IdInstructor guardado en algún lado (lo veremos después).
        ' Por ahora, este botón solo funcionará cuando selecciones una fila de la tabla.

        MessageBox.Show("Función de editar en construcción. Primero selecciona un instructor de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click
        ' NOTA: Similar a editar, necesitas seleccionar un instructor de la tabla.
        MessageBox.Show("Función de inactivar en construcción. Primero selecciona un instructor de la tabla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub dgvInstructores_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInstructores.CellContentClick
        ' Cargar la lista de instructores en la tabla
        Try
            dgvInstructores.DataSource = InstructorDAO.ObtenerTodos()
        Catch ex As Exception
            MessageBox.Show("Error al cargar instructores: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
