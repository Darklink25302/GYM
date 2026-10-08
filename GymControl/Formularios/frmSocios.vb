Public Class frmSocios

    Private socioSeleccionadoId As Integer = 0

    Private Sub frmSocios_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        CargarSocios()

    End Sub

    Private Sub CargarSocios()

        Try

            Dim dao As New SocioDAO()

            dgvSocios.DataSource = Nothing
            dgvSocios.DataSource = dao.Listar()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub btnRegistrar_Click(
    sender As Object,
    e As EventArgs
) Handles btnRegistrar.Click

        Try

            If String.IsNullOrWhiteSpace(txtCedula.Text) OrElse
               String.IsNullOrWhiteSpace(txtNombres.Text) OrElse
               String.IsNullOrWhiteSpace(txtApellidos.Text) Then

                MessageBox.Show(
                    "Complete cédula, nombres y apellidos.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                Exit Sub
            End If

            Dim socio As New Socio With {
                .Cedula = txtCedula.Text.Trim(),
                .Nombres = txtNombres.Text.Trim(),
                .Apellidos = txtApellidos.Text.Trim(),
                .Telefono = txtTelefono.Text.Trim(),
                .Correo = txtCorreo.Text.Trim(),
                .Direccion = txtDireccion.Text.Trim(),
                .FechaNacimiento = dtpFechaNacimiento.Value.Date,
                .Activo = True
            }

            Dim dao As New SocioDAO()

            If dao.Insertar(socio) Then

                MessageBox.Show(
                    "Socio registrado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

                LimpiarCampos()
                CargarSocios()

            Else

                MessageBox.Show(
                    "No se pudo registrar el socio.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

            End If

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub
    Private Sub LimpiarCampos()

        socioSeleccionadoId = 0

        txtCedula.Clear()
        txtNombres.Clear()
        txtApellidos.Clear()
        txtTelefono.Clear()
        txtCorreo.Clear()
        txtDireccion.Clear()

        dtpFechaNacimiento.Value = Date.Today

        txtCedula.Focus()

    End Sub
    Private Sub btnLimpiar_Click(
    sender As Object,
    e As EventArgs
) Handles btnLimpiar.Click

        LimpiarCampos()

    End Sub

    Private Sub txtCedula_TextChanged(sender As Object, e As EventArgs) Handles txtCedula.TextChanged

    End Sub
    Private Sub dgvSocios_CellClick(
    sender As Object,
    e As DataGridViewCellEventArgs
) Handles dgvSocios.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim fila As DataGridViewRow = dgvSocios.Rows(e.RowIndex)

        socioSeleccionadoId = Convert.ToInt32(fila.Cells("IdSocio").Value)

        txtCedula.Text = fila.Cells("Cedula").Value.ToString()
        txtNombres.Text = fila.Cells("Nombres").Value.ToString()
        txtApellidos.Text = fila.Cells("Apellidos").Value.ToString()
        txtTelefono.Text = fila.Cells("Telefono").Value.ToString()
        txtCorreo.Text = fila.Cells("Correo").Value.ToString()
        txtDireccion.Text = fila.Cells("Direccion").Value.ToString()

        If fila.Cells("FechaNacimiento").Value IsNot Nothing AndAlso
           fila.Cells("FechaNacimiento").Value IsNot DBNull.Value Then

            dtpFechaNacimiento.Value =
                Convert.ToDateTime(fila.Cells("FechaNacimiento").Value)

        End If

    End Sub
    Private Sub btnActualizar_Click(
    sender As Object,
    e As EventArgs
) Handles btnActualizar.Click

        If socioSeleccionadoId = 0 Then

            MessageBox.Show(
                "Seleccione un socio.",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub
        End If

        Try

            Dim socio As New Socio With {
                .IdSocio = socioSeleccionadoId,
                .Cedula = txtCedula.Text.Trim(),
                .Nombres = txtNombres.Text.Trim(),
                .Apellidos = txtApellidos.Text.Trim(),
                .Telefono = txtTelefono.Text.Trim(),
                .Correo = txtCorreo.Text.Trim(),
                .Direccion = txtDireccion.Text.Trim(),
                .FechaNacimiento = dtpFechaNacimiento.Value.Date
            }

            Dim dao As New SocioDAO()

            If dao.Actualizar(socio) Then

                MessageBox.Show(
                    "Socio actualizado correctamente."
                )

                LimpiarCampos()
                CargarSocios()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub btnInactivar_Click(
    sender As Object,
    e As EventArgs
) Handles btnInactivar.Click

        If socioSeleccionadoId = 0 Then

            MessageBox.Show(
                "Seleccione un socio."
            )

            Exit Sub

        End If

        Dim respuesta =
            MessageBox.Show(
                "¿Desea inactivar este socio?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

        If respuesta = DialogResult.Yes Then

            Try

                Dim dao As New SocioDAO()

                If dao.Inactivar(socioSeleccionadoId) Then

                    MessageBox.Show(
                        "Socio inactivado correctamente."
                    )

                    LimpiarCampos()
                    CargarSocios()

                End If

            Catch ex As Exception

                MessageBox.Show(ex.Message)

            End Try

        End If

    End Sub


End Class