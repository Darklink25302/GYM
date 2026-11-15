Option Strict On

Public Class frmMembresiasPagos
    Private cargando As Boolean

    Private Sub frmMembresiasPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            PermisosMenu.Exigir("Administrador", "Recepcionista")
            cargando = True
            cboMetodo.Items.AddRange(New Object() {"Efectivo", "Tarjeta", "Transferencia"})
            cboMetodo.SelectedIndex = 0
            Dim socios As New SocioDAO()
            cboSocio.DisplayMember = "NombreCompleto"
            cboSocio.ValueMember = "IdSocio"
            cboSocio.DataSource = socios.Listar()
            Dim tipos As New TipoMembresiaDAO()
            cboTipoMembresia.DisplayMember = "nombre"
            cboTipoMembresia.ValueMember = "id_tipo_membresia"
            Dim vista As New DataView(tipos.Listar())
            vista.RowFilter = "activo=1"
            cboTipoMembresia.DataSource = vista
            dtpFechaInicio.Value = Date.Today
            cargando = False
            ActualizarPlan()
            CargarMembresias()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Membresías y pagos")
            Close()
        End Try
    End Sub

    Private Sub ActualizarPlan() Handles cboTipoMembresia.SelectedIndexChanged, dtpFechaInicio.ValueChanged
        If cargando Then Return
        Dim fila = TryCast(cboTipoMembresia.SelectedItem, DataRowView)
        If fila Is Nothing Then Return
        txtTotal.Text = Convert.ToDecimal(fila("precio")).ToString("N2")
        dtpFechaVencimiento.Value = dtpFechaInicio.Value.Date.AddDays(Convert.ToInt32(fila("duracion_dias")))
    End Sub

    Private Sub CargarMembresias()
        cargando = True
        Try
            Dim dao As New MembresiaDAO()
            dgvMembresias.DataSource = dao.Listar()
            dgvMembresias.Columns("id_membresia").Visible = False
            If dgvMembresias.Columns.Contains("id_socio") Then dgvMembresias.Columns("id_socio").Visible = False
            For Each nombre In New String() {"total", "abonado", "saldo"}
                dgvMembresias.Columns(nombre).DefaultCellStyle.Format = "N2"
            Next
            dgvMembresias.ClearSelection()
            dgvPagos.DataSource = Nothing
        Finally
            cargando = False
            ActualizarSeleccion()
        End Try
    End Sub

    Private Sub ActualizarSeleccion() Handles dgvMembresias.SelectionChanged
        If cargando Then Return
        ActualizarBotonesConstancia()
        btnRegistrarPago.Enabled = dgvMembresias.SelectedRows.Count = 1
        dgvPagos.DataSource = Nothing
        btnAnularPago.Enabled = False
        If dgvMembresias.SelectedRows.Count <> 1 Then Return
        Try
            Dim dao As New PagoDAO()
            dgvPagos.DataSource = dao.ConsultarPorMembresia(MembresiaSeleccionada())
            dgvPagos.Columns("id_pago").HeaderText = "Recibo"
            dgvPagos.Columns("monto").DefaultCellStyle.Format = "N2"
            dgvPagos.Columns("fecha_pago").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            dgvPagos.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Pagos de la membresía")
        End Try
    End Sub

    Private Sub SeleccionarPago() Handles dgvPagos.SelectionChanged
        btnAnularPago.Enabled = False
        If dgvPagos.SelectedRows.Count = 1 Then
            btnAnularPago.Enabled = Not Convert.ToBoolean(dgvPagos.SelectedRows(0).Cells("anulado").Value)
        End If
    End Sub

    Private Sub btnRegistrarMembresia_Click(sender As Object, e As EventArgs) Handles btnRegistrarMembresia.Click
        Try
            If cboSocio.SelectedValue Is Nothing OrElse cboTipoMembresia.SelectedValue Is Nothing Then Throw New ArgumentException("Selecciona un socio y un plan.")
            Dim total As Decimal
            Dim abono As Decimal = 0
            If Not Decimal.TryParse(txtTotal.Text, total) Then Throw New ArgumentException("El precio no es válido.")
            If txtPrimerAbono.Text.Trim() <> "" AndAlso Not Decimal.TryParse(txtPrimerAbono.Text, abono) Then Throw New ArgumentException("El primer abono no es válido.")
            Dim membresia As New Membresia With {.IdSocio=Convert.ToInt32(cboSocio.SelectedValue),.IdTipoMembresia=Convert.ToInt32(cboTipoMembresia.SelectedValue),
                .FechaInicio=dtpFechaInicio.Value.Date,.FechaVencimiento=dtpFechaVencimiento.Value.Date,.Total=total,.Estado="Activa"}
            Dim pago As New Pago With {.Monto=abono,.MetodoPago=CStr(cboMetodo.SelectedItem)}
            Dim dao As New PagoDAO()
            Dim id = dao.RegistrarMembresia(membresia,pago)
            CargarMembresias()
            MessageBox.Show("Membresía " & id.ToString() & " registrada correctamente.", "GymControl")
            ImprimirSiCorresponde(pago.IdPago)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "No se pudo registrar la membresía")
        End Try
    End Sub

    Private Sub btnRegistrarPago_Click(sender As Object, e As EventArgs) Handles btnRegistrarPago.Click
        Try
            Dim id = MembresiaSeleccionada()
            Dim entrada = InputBox("Escribe el monto del abono en córdobas.", "Registrar pago")
            If String.IsNullOrWhiteSpace(entrada) Then Return
            Dim monto As Decimal
            If Not Decimal.TryParse(entrada,monto) Then Throw New ArgumentException("El monto no es válido.")
            If MessageBox.Show("¿Registrar C$ " & monto.ToString("N2") & " mediante " & CStr(cboMetodo.SelectedItem) & "?", "Confirmar pago", MessageBoxButtons.YesNo) <> DialogResult.Yes Then Return
            Dim pago As New Pago With {.IdMembresia=id,.Monto=monto,.MetodoPago=CStr(cboMetodo.SelectedItem)}
            Dim dao As New PagoDAO()
            Dim idPago = dao.InsertarYObtenerId(pago)
            CargarMembresias()
            ImprimirSiCorresponde(idPago)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "No se pudo registrar el pago")
        End Try
    End Sub

    Private Sub ImprimirSiCorresponde(idPago As Integer)
        If idPago > 0 AndAlso chkImprimirAlRegistrar.Checked Then frmRecibos.ImprimirPago(idPago,Me)
    End Sub

    Private Sub btnAnularPago_Click(sender As Object, e As EventArgs) Handles btnAnularPago.Click
        Try
            If dgvPagos.SelectedRows.Count <> 1 Then Throw New ArgumentException("Selecciona un pago.")
            Dim motivo = InputBox("Escribe el motivo de anulación.","Anular pago")
            If String.IsNullOrWhiteSpace(motivo) Then Return
            If MessageBox.Show("El pago se conservará marcado como ANULADO. ¿Continuar?","Confirmar",MessageBoxButtons.YesNo)<>DialogResult.Yes Then Return
            Dim dao As New PagoDAO()
            dao.Anular(Convert.ToInt32(dgvPagos.SelectedRows(0).Cells("id_pago").Value),motivo)
            CargarMembresias()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"No se pudo anular el pago")
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object,e As EventArgs) Handles btnLimpiar.Click
        txtPrimerAbono.Clear()
        dtpFechaInicio.Value=Date.Today
        ActualizarPlan()
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

    Private Sub ActualizarBotonesConstancia()
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
