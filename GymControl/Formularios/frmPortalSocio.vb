Option Strict On

Public Class frmPortalSocio
    Private cargando As Boolean
    Private movimientos As DataTable

    Private Sub frmPortalSocio_Load(sender As Object,e As EventArgs) Handles MyBase.Load
        Try
            PermisosReportes.Exigir("R05")
            cargando=True
            cboSocio.DisplayMember="socio"
            cboSocio.ValueMember="id_socio"
            cboSocio.DataSource=ReporteDAO.Socios()
            cboSocio.Enabled=Sesion.Rol<>"Socio"
            cargando=False
            Consultar()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Portal del socio")
            Close()
        End Try
    End Sub

    Private Function SocioSeleccionado() As Integer
        If Sesion.Rol="Socio" Then Return PermisosReportes.ObtenerSocioPermitido(0)
        If cboSocio.SelectedValue Is Nothing Then Throw New ArgumentException("Selecciona un socio.")
        Return PermisosReportes.ObtenerSocioPermitido(Convert.ToInt32(cboSocio.SelectedValue))
    End Function

    Private Sub Consultar() Handles btnConsultar.Click
        If cargando Then Return
        Try
            Dim id=SocioSeleccionado()
            Dim saldos=ReporteDAO.SaldosSocio(id)
            movimientos=ReporteDAO.EstadoCuenta(id,New Date(1900,1,1))
            dgvMembresias.DataSource=saldos
            dgvMembresias.Columns("id_socio").Visible=False
            Dim saldo As Decimal=0
            For Each fila As DataRow In saldos.Rows
                saldo+=Convert.ToDecimal(fila("saldo"))
            Next
            lblSaldo.Text="Saldo pendiente: C$ " & saldo.ToString("N2")
            dgvMembresias.ClearSelection()
            dgvPagos.DataSource=Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Portal del socio")
            If Sesion.IdUsuario<=0 OrElse TypeOf ex Is UnauthorizedAccessException Then Close()
        End Try
    End Sub

    Private Sub SeleccionarMembresia() Handles dgvMembresias.SelectionChanged
        dgvPagos.DataSource=Nothing
        If movimientos Is Nothing OrElse dgvMembresias.SelectedRows.Count<>1 Then Return
        Try
            Dim socioPermitido=SocioSeleccionado()
            If Convert.ToInt32(dgvMembresias.SelectedRows(0).Cells("id_socio").Value)<>socioPermitido Then
                Throw New UnauthorizedAccessException("Actualiza la consulta del socio antes de continuar.")
            End If
            Dim id=Convert.ToInt32(dgvMembresias.SelectedRows(0).Cells("id_membresia").Value)
            Dim vista As New DataView(movimientos)
            vista.RowFilter="id_membresia=" & id.ToString() & " AND id_pago IS NOT NULL"
            dgvPagos.DataSource=vista.ToTable(False,"id_pago","fecha_pago","metodo_pago","monto","anulado")
        Catch ex As Exception
            If Sesion.IdUsuario<=0 Then Close()
        End Try
    End Sub

    Private Sub btnEstadoCuenta_Click(sender As Object,e As EventArgs) Handles btnEstadoCuenta.Click
        Try
            Using formulario As New frmVisorReportes("R05",New Date(Date.Today.Year,1,1),Date.Today,"Todos",SocioSeleccionado())
                formulario.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Estado de cuenta")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object,e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
