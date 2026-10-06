Public Class frmSalas

    Private idSeleccionado As Integer = 0

    Private Sub frmSalas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ExigirAdministrador()
            Text = "Salas"
            txtNombre.MaxLength = 100
            dgvSalas.ReadOnly = True
            dgvSalas.AllowUserToAddRows = False
            dgvSalas.AllowUserToDeleteRows = False
            dgvSalas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvSalas.MultiSelect = False
            dgvSalas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvSalas.Height = 170
            CargarSalas()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Salas")
            Close()
        End Try
    End Sub

    Private Sub CargarSalas()
        dgvSalas.DataSource = CatalogoDAO.ObtenerSalas()
        dgvSalas.ClearSelection()
    End Sub

    Private Sub Limpiar()
        idSeleccionado = 0
        txtNombre.Clear()
        txtcapacidad.Clear()
        dgvSalas.ClearSelection()
        btnGuardar.Text = "Guardar"
        txtNombre.Focus()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim capacidad As Integer
            If txtNombre.Text.Trim() = "" Then
                MessageBox.Show("El nombre de la sala es obligatorio.")
                Return
            End If
            If Not Integer.TryParse(txtcapacidad.Text.Trim(), capacidad) OrElse capacidad <= 0 Then
                MessageBox.Show("La capacidad debe ser un número entero mayor que cero.")
                Return
            End If
            Dim guardado As Boolean
            If idSeleccionado = 0 Then
                guardado = CatalogoDAO.InsertarSala(txtNombre.Text.Trim(), capacidad)
            Else
                guardado = CatalogoDAO.ActualizarSala(idSeleccionado, txtNombre.Text.Trim(), capacidad)
            End If
            If guardado Then
                CargarSalas()
                Limpiar()
                MessageBox.Show("Sala guardada.")
            Else
                MessageBox.Show("La sala ya no existe. Actualiza la lista.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Salas")
        End Try
    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click
        Try
            If idSeleccionado = 0 Then
                MessageBox.Show("Selecciona una sala de la tabla.")
                Return
            End If
            If MessageBox.Show("¿Inactivar esta sala?", "Salas", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
            CatalogoDAO.InactivarSala(idSeleccionado)
            CargarSalas()
            Limpiar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Salas")
        End Try
    End Sub

    Private Sub dgvSalas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSalas.CellClick
        If e.RowIndex < 0 Then Return
        Dim seleccionada = TryCast(dgvSalas.Rows(e.RowIndex).DataBoundItem, Sala)
        If seleccionada Is Nothing Then Return
        idSeleccionado = seleccionada.IdSala
        txtNombre.Text = seleccionada.Nombre
        txtcapacidad.Text = seleccionada.Capacidad.ToString()
        btnGuardar.Text = "Editar"
    End Sub

End Class
