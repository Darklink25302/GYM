Option Strict On
Option Infer On

Imports System.Drawing.Printing
Imports System.IO
Imports MySqlConnector

Public Class frmRecibos
    Private filtroMembresia As Integer
    Private idPagoActual As Integer
    Private impresor As ImpresorRecibo
    Private errorImpresion As String = ""

    Public Sub New()
        InitializeComponent()
        dtpDesde.Value = New Date(Date.Today.Year, Date.Today.Month, 1)
    End Sub

    Public Sub New(idMembresia As Integer)
        Me.New()
        If idMembresia < 0 Then Throw New ArgumentException("La membresía no es válida.")
        filtroMembresia = idMembresia
    End Sub

    Private Sub frmRecibos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarPagos()
    End Sub

    Private Sub CargarPagos()
        dgvPagos.DataSource = Nothing
        btnVistaPrevia.Enabled = False
        btnImprimir.Enabled = False
        UseWaitCursor = True
        btnCargar.Enabled = False
        Try
            dgvPagos.DataSource = ReciboDAO.ListarPagos(dtpDesde.Value, dtpHasta.Value, txtBuscar.Text, filtroMembresia)
            dgvPagos.ClearSelection()
            lblEstado.Text = dgvPagos.Rows.Count.ToString() & " pagos. Selecciona uno para imprimir o reimprimir su recibo."
            If dgvPagos.Rows.Count = 0 Then lblEstado.Text = "No hay pagos que coincidan con estos filtros."
        Catch ex As Exception
            MostrarError(ex)
        Finally
            btnCargar.Enabled = True
            UseWaitCursor = False
        End Try
    End Sub

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        CargarPagos()
    End Sub

    Private Sub dgvPagos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPagos.SelectionChanged
        Dim seleccionado = dgvPagos.SelectedRows.Count = 1
        btnVistaPrevia.Enabled = seleccionado
        btnImprimir.Enabled = seleccionado
    End Sub

    Private Sub dgvPagos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvPagos.CellFormatting
        If dgvPagos.Columns(e.ColumnIndex).DataPropertyName = "anulado" AndAlso e.Value IsNot Nothing AndAlso e.Value IsNot DBNull.Value Then
            Dim anulado = Convert.ToBoolean(e.Value)
            e.Value = If(anulado, "ANULADO", "Vigente")
            e.FormattingApplied = True
            If anulado Then e.CellStyle.ForeColor = Color.DarkRed
        End If
    End Sub

    Private Function PagoSeleccionado() As Integer
        If dgvPagos.SelectedRows.Count <> 1 Then Throw New ArgumentException("Selecciona un pago.")
        Return Convert.ToInt32(dgvPagos.SelectedRows(0).Cells("colRecibo").Value)
    End Function

    Private Sub PrepararDocumento(idPago As Integer)
        Dim datos = ReciboDAO.Obtener(idPago)
        ppdVista.Document = Nothing
        prdImpresora.Document = Nothing
        If impresor IsNot Nothing Then impresor.Dispose()
        idPagoActual = idPago
        errorImpresion = ""
        impresor = New ImpresorRecibo(datos)
        AddHandler impresor.Documento.BeginPrint, AddressOf ValidarImpresion
        ppdVista.Document = impresor.Documento
        prdImpresora.Document = impresor.Documento
    End Sub

    Private Sub ValidarImpresion(sender As Object, e As PrintEventArgs)
        errorImpresion = ""
        Try
            ' Se consulta de nuevo al imprimir, incluso desde la barra de la vista previa.
            impresor.Datos = ReciboDAO.Obtener(idPagoActual)
        Catch ex As Exception
            e.Cancel = True
            errorImpresion = DescribirError(ex)
            lblEstado.Text = errorImpresion
        End Try
    End Sub

    Private Sub btnVistaPrevia_Click(sender As Object, e As EventArgs) Handles btnVistaPrevia.Click
        Try
            PrepararDocumento(PagoSeleccionado())
            ppdVista.PrintPreviewControl.InvalidatePreview()
            ppdVista.ShowDialog(Me)
            If errorImpresion <> "" Then Throw New InvalidOperationException(errorImpresion)
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            PrepararDocumento(PagoSeleccionado())
            If prdImpresora.ShowDialog(Me) <> DialogResult.OK Then Return
            impresor.Documento.Print()
            If errorImpresion <> "" Then Throw New InvalidOperationException(errorImpresion)
            lblEstado.Text = "El recibo se envió a la impresora seleccionada."
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Public Shared Function ImprimirPago(idPago As Integer, propietario As IWin32Window) As Boolean
        Try
            Using formulario As New frmRecibos()
                formulario.PrepararDocumento(idPago)
                formulario.impresor.Documento.Print()
                If formulario.errorImpresion <> "" Then Throw New InvalidOperationException(formulario.errorImpresion)
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show(propietario, "No se pudo imprimir el recibo. El registro del pago no se ha modificado. " & DescribirError(ex), "Recibo de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Shared Function DescribirError(ex As Exception) As String
        If TypeOf ex Is FileNotFoundException Then
            Return "Falta configurar conexion.reportes.local.json con el usuario gym_reportes."
        End If
        If TypeOf ex Is MySqlException Then
            Dim fallo = DirectCast(ex, MySqlException)
            If fallo.Number = 1146 OrElse fallo.Number = 1054 OrElse fallo.Number = 1356 Then
                Return "Las vistas de reportes todavía no están listas para consultar los recibos."
            End If
            Return "No se pudo consultar la base de reportes. Revisa la conexión y los permisos de gym_reportes."
        End If
        If TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ArgumentException OrElse TypeOf ex Is InvalidOperationException Then Return ex.Message
        If TypeOf ex Is InvalidPrinterException Then Return "No hay una impresora disponible. Selecciona una impresora válida."
        Return "No se pudo preparar el recibo. Revisa la conexión, los datos y la impresora."
    End Function

    Private Sub MostrarError(ex As Exception)
        lblEstado.Text = DescribirError(ex)
        If TypeOf ex Is UnauthorizedAccessException Then
            dgvPagos.DataSource = Nothing
            btnVistaPrevia.Enabled = False
            btnImprimir.Enabled = False
            Close()
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
