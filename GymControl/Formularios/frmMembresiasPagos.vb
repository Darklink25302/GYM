Public Class frmMembresiasPagos

    Private Sub frmMembresiasPagos_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        CargarSocios()
        CargarTiposMembresia()
        CargarMembresias()


        dtpFechaInicio.Value = Date.Today
        ActualizarBotonesConstancia()

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

    Private impresorConstancia As ImpresorConstancia
    Private idConstancia As Integer
    Private errorConstancia As String = ""

    Private Function MembresiaSeleccionada() As Integer
        If dgvMembresias.SelectedRows.Count <> 1 OrElse dgvMembresias.SelectedRows(0).IsNewRow Then
            Throw New ArgumentException("Selecciona una membresía en la tabla.")
        End If
        Return Convert.ToInt32(dgvMembresias.SelectedRows(0).Cells("id_membresia").Value)
    End Function

    Private Sub ActualizarBotonesConstancia() Handles dgvMembresias.SelectionChanged
        Dim disponible As Boolean = False
        Dim seleccionada = dgvMembresias.SelectedRows.Count = 1 AndAlso Not dgvMembresias.SelectedRows(0).IsNewRow
        btnRecibosMembresia.Enabled = seleccionada AndAlso PermisosReportes.PuedeVer("R01", Sesion.Rol)
        If seleccionada AndAlso dgvMembresias.Columns.Contains("estado") Then
            Dim fila = dgvMembresias.SelectedRows(0)
            disponible = Convert.ToString(fila.Cells("estado").Value) = "Activa" AndAlso
                         Convert.ToDateTime(fila.Cells("fecha_inicio").Value).Date <= Date.Today AndAlso
                         Convert.ToDateTime(fila.Cells("fecha_vencimiento").Value).Date >= Date.Today AndAlso
                         PermisosReportes.PuedeVer("R02", Sesion.Rol)
        End If
        btnConstancia.Enabled = disponible
        btnPaginaConstancia.Enabled = disponible
        btnImprimirConstancia.Enabled = disponible
    End Sub

    Private Sub btnRecibosMembresia_Click(sender As Object, e As EventArgs) Handles btnRecibosMembresia.Click
        Try
            PermisosReportes.Exigir("R01")
            Using formulario As New frmRecibos(MembresiaSeleccionada())
                formulario.ShowDialog(Me)
            End Using
            If Sesion.IdUsuario <= 0 Then Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Recibos de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If TypeOf ex Is UnauthorizedAccessException OrElse Sesion.IdUsuario <= 0 Then Close()
        End Try
    End Sub

    Private Sub PrepararConstancia()
        Dim id = MembresiaSeleccionada()
        Dim datos = ConstanciaDAO.Obtener(id)
        If impresorConstancia IsNot Nothing AndAlso idConstancia = id Then
            impresorConstancia.Datos = datos
            Return
        End If
        ppdConstancia.Document = Nothing
        psdPaginaConstancia.Document = Nothing
        prdConstancia.Document = Nothing
        If impresorConstancia IsNot Nothing Then impresorConstancia.Dispose()
        idConstancia = id
        impresorConstancia = New ImpresorConstancia(datos, My.Resources.logo_uni_azul)
        AddHandler impresorConstancia.Documento.BeginPrint, AddressOf ValidarConstancia
        ppdConstancia.Document = impresorConstancia.Documento
        psdPaginaConstancia.Document = impresorConstancia.Documento
        prdConstancia.Document = impresorConstancia.Documento
    End Sub

    Private Sub ValidarConstancia(sender As Object, e As System.Drawing.Printing.PrintEventArgs)
        errorConstancia = ""
        Try
            impresorConstancia.Datos = ConstanciaDAO.Obtener(idConstancia)
        Catch ex As Exception
            e.Cancel = True
            errorConstancia = DescribirErrorConstancia(ex)
        End Try
    End Sub

    Private Sub btnConstancia_Click(sender As Object, e As EventArgs) Handles btnConstancia.Click
        Try
            PrepararConstancia()
            errorConstancia = ""
            ppdConstancia.PrintPreviewControl.InvalidatePreview()
            ppdConstancia.ShowDialog(Me)
            If errorConstancia <> "" Then MessageBox.Show(errorConstancia, "Constancia")
            If Sesion.IdUsuario <= 0 Then Close()
        Catch ex As Exception
            MostrarErrorConstancia(ex)
        End Try
    End Sub

    Private Sub btnPaginaConstancia_Click(sender As Object, e As EventArgs) Handles btnPaginaConstancia.Click
        Try
            PrepararConstancia()
            psdPaginaConstancia.ShowDialog(Me)
        Catch ex As Exception
            MostrarErrorConstancia(ex)
        End Try
    End Sub

    Private Sub btnImprimirConstancia_Click(sender As Object, e As EventArgs) Handles btnImprimirConstancia.Click
        Try
            PrepararConstancia()
            If prdConstancia.ShowDialog(Me) <> DialogResult.OK Then Return
            errorConstancia = ""
            impresorConstancia.Documento.Print()
            If errorConstancia <> "" Then
                MessageBox.Show(errorConstancia, "No se imprimió la constancia")
                If Sesion.IdUsuario <= 0 Then Close()
            Else
                MessageBox.Show("La constancia se envió a la impresora seleccionada.", "Constancia")
            End If
        Catch ex As Exception
            MostrarErrorConstancia(ex)
        End Try
    End Sub

    Private Shared Function DescribirErrorConstancia(ex As Exception) As String
        If TypeOf ex Is System.IO.FileNotFoundException Then
            Return "Falta configurar conexion.reportes.local.json con el usuario gym_reportes."
        End If
        If TypeOf ex Is MySqlConnector.MySqlException Then
            Dim fallo = DirectCast(ex, MySqlConnector.MySqlException)
            If fallo.Number = 1146 OrElse fallo.Number = 1054 OrElse fallo.Number = 1356 Then
                Return "La vista vw_saldos_membresia todavía no está lista. Revisa las columnas necesarias para R02."
            End If
            Return "No se pudo consultar la base de reportes. Revisa la conexión y los permisos de gym_reportes."
        End If
        If TypeOf ex Is System.Drawing.Printing.InvalidPrinterException Then
            Return "No hay una impresora disponible. Selecciona una impresora válida."
        End If
        Return ex.Message
    End Function

    Private Sub MostrarErrorConstancia(ex As Exception)
        MessageBox.Show(DescribirErrorConstancia(ex), "Constancia de membresía", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If TypeOf ex Is UnauthorizedAccessException OrElse Sesion.IdUsuario <= 0 Then Close()
    End Sub

End Class
