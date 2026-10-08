Public Class frmActividades

    Private idSeleccionado As Integer = 0

    Private Sub frmActividades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ExigirAdministrador()
            Text = "Actividades"
            txtNombre.MaxLength = 100
            txtDescripcion.MaxLength = 255
            dgvActividades.ReadOnly = True
            dgvActividades.AllowUserToAddRows = False
            dgvActividades.AllowUserToDeleteRows = False
            dgvActividades.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvActividades.MultiSelect = False
            dgvActividades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvActividades.Height = 170
            CargarActividades()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Actividades")
            Close()
        End Try
    End Sub

    Private Sub CargarActividades()
        dgvActividades.DataSource = CatalogoDAO.ObtenerActividades()
        dgvActividades.ClearSelection()
    End Sub

    Private Sub Limpiar()
        idSeleccionado = 0
        txtNombre.Clear()
        txtDescripcion.Clear()
        dgvActividades.ClearSelection()
        btnGuardar.Text = "Guardar"
        txtNombre.Focus()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If txtNombre.Text.Trim() = "" Then
                MessageBox.Show("El nombre de la actividad es obligatorio.")
                Return
            End If
            Dim guardado As Boolean
            If idSeleccionado = 0 Then
                guardado = CatalogoDAO.InsertarActividad(txtNombre.Text.Trim(), txtDescripcion.Text.Trim())
            Else
                guardado = CatalogoDAO.ActualizarActividad(idSeleccionado, txtNombre.Text.Trim(), txtDescripcion.Text.Trim())
            End If
            If guardado Then
                CargarActividades()
                Limpiar()
                MessageBox.Show("Actividad guardada.")
            Else
                MessageBox.Show("La actividad ya no existe. Actualiza la lista.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Actividades")
        End Try
    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click
        Try
            If idSeleccionado = 0 Then
                MessageBox.Show("Selecciona una actividad de la tabla.")
                Return
            End If
            If MessageBox.Show("¿Inactivar esta actividad?", "Actividades", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
            CatalogoDAO.InactivarActividad(idSeleccionado)
            CargarActividades()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Actividades")
        End Try
    End Sub

    Private Sub dgvActividades_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvActividades.CellClick
        If e.RowIndex < 0 Then Return
        Dim seleccionada = TryCast(dgvActividades.Rows(e.RowIndex).DataBoundItem, Actividad)
        If seleccionada Is Nothing Then Return
        idSeleccionado = seleccionada.IdActividad
        txtNombre.Text = seleccionada.Nombre
        txtDescripcion.Text = seleccionada.Descripcion
        btnGuardar.Text = "Editar"
    End Sub

End Class
