Public Class frmMembresiasPagos

    Private Sub frmMembresiasPagos_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        CargarSocios()
        CargarTiposMembresia()
        CargarMembresias()


        dtpFechaInicio.Value = Date.Today

    End Sub

    Private Sub CargarSocios()

        Try

            Dim dao As New SocioDAO()
            Dim socios = dao.Listar()

            cboSocio.DataSource = socios
            cboSocio.DisplayMember = "Nombres"
            cboSocio.ValueMember = "IdSocio"

        Catch ex As Exception

            MessageBox.Show(
                "Error al cargar socios: " & ex.Message
            )

        End Try

    End Sub

    Private Sub CargarTiposMembresia()

        Try

            Dim dao As New TipoMembresiaDAO()
            Dim tabla As DataTable = dao.Listar()

            Dim vista As New DataView(tabla)

            vista.RowFilter = "activo = 1"

            cboTipoMembresia.DataSource = vista
            cboTipoMembresia.DisplayMember = "nombre"
            cboTipoMembresia.ValueMember = "id_tipo_membresia"

        Catch ex As Exception

            MessageBox.Show(
            "Error al cargar los tipos de membresía: " & ex.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

        End Try

    End Sub

    Private Sub cboTipoMembresia_SelectedIndexChanged(
    sender As Object,
    e As EventArgs
) Handles cboTipoMembresia.SelectedIndexChanged

        Try

            If cboTipoMembresia.SelectedItem Is Nothing Then
                Exit Sub
            End If

            Dim vista As DataRowView =
                TryCast(cboTipoMembresia.SelectedItem, DataRowView)

            If vista Is Nothing Then
                Exit Sub
            End If

            Dim precio As Decimal =
                Convert.ToDecimal(vista("precio"))

            Dim duracionDias As Integer =
                Convert.ToInt32(vista("duracion_dias"))

            txtTotal.Text = precio.ToString("0.00")

            dtpFechaVencimiento.Value =
                dtpFechaInicio.Value.AddDays(duracionDias)

        Catch ex As Exception

            MessageBox.Show(
                "Error al cargar los datos de la membresía: " & ex.Message
            )

        End Try

    End Sub
    Private Sub dtpFechaInicio_ValueChanged(
    sender As Object,
    e As EventArgs
) Handles dtpFechaInicio.ValueChanged

        Try

            If cboTipoMembresia.SelectedItem Is Nothing Then
                Exit Sub
            End If

            Dim vista As DataRowView =
                TryCast(cboTipoMembresia.SelectedItem, DataRowView)

            If vista Is Nothing Then
                Exit Sub
            End If

            Dim duracionDias As Integer =
                Convert.ToInt32(vista("duracion_dias"))

            dtpFechaVencimiento.Value =
                dtpFechaInicio.Value.AddDays(duracionDias)

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub btnRegistrarMembresia_Click(
    sender As Object,
    e As EventArgs
) Handles btnRegistrarMembresia.Click

        Try

            If cboSocio.SelectedValue Is Nothing Then
                MessageBox.Show(
                    "Seleccione un socio.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Exit Sub
            End If

            If cboTipoMembresia.SelectedValue Is Nothing Then
                MessageBox.Show(
                    "Seleccione un tipo de membresía.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Exit Sub
            End If

            Dim total As Decimal

            If Not Decimal.TryParse(txtTotal.Text, total) Then
                MessageBox.Show(
                    "El total de la membresía no es válido."
                )
                Exit Sub
            End If

            Dim primerAbono As Decimal

            If String.IsNullOrWhiteSpace(txtPrimerAbono.Text) Then
                primerAbono = 0
            ElseIf Not Decimal.TryParse(
                txtPrimerAbono.Text,
                primerAbono
            ) Then

                MessageBox.Show(
                    "El primer abono no es válido."
                )
                Exit Sub
            End If

            If primerAbono < 0 Then
                MessageBox.Show(
                    "El primer abono no puede ser negativo."
                )
                Exit Sub
            End If

            If primerAbono > total Then
                MessageBox.Show(
                    "El primer abono no puede ser mayor que el total."
                )
                Exit Sub
            End If

            Dim membresia As New Membresia With {
                .IdSocio = Convert.ToInt32(cboSocio.SelectedValue),
                .IdTipoMembresia =
                    Convert.ToInt32(cboTipoMembresia.SelectedValue),
                .FechaInicio = dtpFechaInicio.Value.Date,
                .FechaVencimiento =
                    dtpFechaVencimiento.Value.Date,
                .Total = total,
                .Estado = "Activa"
            }

            Dim membresiaDAO As New MembresiaDAO()

            Dim idMembresia As Integer =
                membresiaDAO.Insertar(membresia)

            If idMembresia <= 0 Then

                MessageBox.Show(
                    "No se pudo registrar la membresía."
                )

                Exit Sub

            End If

            If primerAbono > 0 Then

                Dim pago As New Pago With {
                    .IdMembresia = idMembresia,
                    .FechaPago = Date.Today,
                    .Monto = primerAbono
                }

                Dim pagoDAO As New PagoDAO()

                pagoDAO.Insertar(pago)

            End If

            Dim saldo As Decimal =
                total - primerAbono

            MessageBox.Show(
                "Membresía registrada correctamente." &
                Environment.NewLine &
                "Total: C$ " & total.ToString("0.00") &
                Environment.NewLine &
                "Primer abono: C$ " &
                primerAbono.ToString("0.00") &
                Environment.NewLine &
                "Saldo pendiente: C$ " &
                saldo.ToString("0.00"),
                "Registro completado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            LimpiarFormulario()
            CargarMembresias()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub
    Private Sub LimpiarFormulario()

        If cboSocio.Items.Count > 0 Then
            cboSocio.SelectedIndex = 0
        End If

        If cboTipoMembresia.Items.Count > 0 Then
            cboTipoMembresia.SelectedIndex = 0
        End If

        dtpFechaInicio.Value = Date.Today

        txtPrimerAbono.Clear()

    End Sub
    Private Sub btnLimpiar_Click(
    sender As Object,
    e As EventArgs
) Handles btnLimpiar.Click

        LimpiarFormulario()

    End Sub

    Private Sub CargarMembresias()

        Try

            Dim dao As New MembresiaDAO()

            dgvMembresias.DataSource = Nothing
            dgvMembresias.DataSource = dao.Listar()

            If dgvMembresias.Columns.Count > 0 Then

                dgvMembresias.Columns("id_membresia").Visible = False

                dgvMembresias.Columns("socio").HeaderText =
                    "Socio"

                dgvMembresias.Columns("tipo_membresia").HeaderText =
                    "Tipo de membresía"

                dgvMembresias.Columns("fecha_inicio").HeaderText =
                    "Inicio"

                dgvMembresias.Columns("fecha_vencimiento").HeaderText =
                    "Vencimiento"

                dgvMembresias.Columns("total").HeaderText =
                    "Total"

                dgvMembresias.Columns("abonado").HeaderText =
                    "Abonado"

                dgvMembresias.Columns("saldo").HeaderText =
                    "Saldo"

                dgvMembresias.Columns("estado").HeaderText =
                    "Estado"

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

End Class