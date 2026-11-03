Public Class frmTiposMembresia

    Private dao As New TipoMembresiaDAO()
    Private idSeleccionado As Integer = 0

    Private Sub frmTiposMembresia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chkActivo.Checked = True
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Try
            dgvTiposMembresia.DataSource = dao.Listar()
        Catch ex As Exception
            MessageBox.Show("Error al cargar los tipos de membresía: " & ex.Message)
        End Try
    End Sub

    Private Function ValidarCampos() As Boolean

        If txtNombre.Text.Trim() = "" Then
            MessageBox.Show("Ingrese el nombre.")
            txtNombre.Focus()
            Return False
        End If

        Dim precio As Decimal
        If Not Decimal.TryParse(txtPrecio.Text, precio) OrElse precio <= 0 Then
            MessageBox.Show("Ingrese un precio válido mayor que 0.")
            txtPrecio.Focus()
            Return False
        End If

        Dim duracion As Integer
        If Not Integer.TryParse(txtDuracion.Text, duracion) OrElse duracion <= 0 Then
            MessageBox.Show("Ingrese una duración válida.")
            txtDuracion.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If Not ValidarCampos() Then Exit Sub

        Try
            Dim tipo As New TipoMembresia With {
                .Nombre = txtNombre.Text.Trim(),
                .Precio = Decimal.Parse(txtPrecio.Text),
                .DuracionDias = Integer.Parse(txtDuracion.Text),
                .Activo = chkActivo.Checked
            }

            dao.Insertar(tipo)

            MessageBox.Show("Tipo de membresía guardado correctamente.")

            Limpiar()
            CargarDatos()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        End Try

    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Limpiar()
    End Sub

    Private Sub Limpiar()
        idSeleccionado = 0
        txtNombre.Clear()
        txtPrecio.Clear()
        txtDuracion.Clear()
        chkActivo.Checked = True
        txtNombre.Focus()
    End Sub

    Private Sub dgvTiposMembresia_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTiposMembresia.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvTiposMembresia.Rows(e.RowIndex)

        idSeleccionado = Convert.ToInt32(fila.Cells("id_tipo_membresia").Value)

        txtNombre.Text = fila.Cells("nombre").Value.ToString()
        txtPrecio.Text = fila.Cells("precio").Value.ToString()
        txtDuracion.Text = fila.Cells("duracion_dias").Value.ToString()

        chkActivo.Checked = Convert.ToBoolean(fila.Cells("activo").Value)

    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click

        If idSeleccionado = 0 Then
            MessageBox.Show("Seleccione un tipo de membresía.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try

            Dim tipo As New TipoMembresia With {
                .IdTipoMembresia = idSeleccionado,
                .Nombre = txtNombre.Text.Trim(),
                .Precio = Decimal.Parse(txtPrecio.Text),
                .DuracionDias = Integer.Parse(txtDuracion.Text),
                .Activo = chkActivo.Checked
            }

            dao.Actualizar(tipo)

            MessageBox.Show("Tipo de membresía actualizado.")

            Limpiar()
            CargarDatos()

        Catch ex As Exception
            MessageBox.Show("Error al editar: " & ex.Message)
        End Try

    End Sub

    Private Sub btnInactivar_Click(sender As Object, e As EventArgs) Handles btnInactivar.Click

        If idSeleccionado = 0 Then
            MessageBox.Show("Seleccione un tipo de membresía.")
            Exit Sub
        End If

        If MessageBox.Show(
            "¿Desea inactivar este tipo de membresía?",
            "Confirmar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) = DialogResult.No Then

            Exit Sub
        End If

        Try

            dao.Inactivar(idSeleccionado)

            MessageBox.Show("Tipo de membresía inactivado.")

            Limpiar()
            CargarDatos()

        Catch ex As Exception
            MessageBox.Show("Error al inactivar: " & ex.Message)
        End Try

    End Sub

End Class