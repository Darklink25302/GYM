Option Strict On
Option Infer On

Public Class frmCentroReportes
    Public Sub New()
        InitializeComponent()
        If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then Return
        If Sesion.IdUsuario <= 0 Then
            Throw New UnauthorizedAccessException("Debes iniciar sesión para consultar reportes.")
        End If

        PermisosReportes.ExigirCentro()
        dtpDesde.Value = New Date(Date.Today.Year, Date.Today.Month, 1)
        dtpCuentaDesde.Value = New Date(Date.Today.Year, 1, 1)
        nudAnio.Maximum = Date.Today.Year
        nudAnio.Value = Date.Today.Year
        tslUsuario.Text = Sesion.NombreUsuario & " · " & Sesion.Rol
        CargarReportes()
    End Sub

    Private Sub CargarReportes()
        Dim impresion As New TreeNode("Impresión directa")
        Dim visor As New TreeNode("Reportes en visor")
        Dim archivos As New TreeNode("Archivos Excel y PDF")

        AgregarReporte(impresion, "R01", "Recibo de pago")
        AgregarReporte(impresion, "R02", "Constancia de membresía")
        AgregarReporte(impresion, "R03", "Listado de vencimientos")
        AgregarReporte(visor, "R04", "Ingresos por período")
        AgregarReporte(visor, "R05", "Estado de cuenta")
        AgregarReporte(visor, "R06", "Horario semanal")
        AgregarReporte(archivos, "R07", "Cartera de saldos")
        AgregarReporte(archivos, "R08", "Informe mensual")
        AgregarReporte(archivos, "R09", "Libro de ingresos anual")

        If impresion.Nodes.Count > 0 Then tvReportes.Nodes.Add(impresion)
        If visor.Nodes.Count > 0 Then tvReportes.Nodes.Add(visor)
        If archivos.Nodes.Count > 0 Then tvReportes.Nodes.Add(archivos)
        tvReportes.ExpandAll()
        tvReportes.SelectedNode = tvReportes.Nodes(0).Nodes(0)
    End Sub

    Private Sub AgregarReporte(grupo As TreeNode, codigo As String, titulo As String)
        If Not PermisosReportes.PuedeVer(codigo, Sesion.Rol) Then Return
        Dim nodo As New TreeNode(codigo & " · " & titulo)
        nodo.Tag = codigo
        grupo.Nodes.Add(nodo)
    End Sub

    Private Sub tvReportes_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvReportes.AfterSelect
        OcultarParametros()
        If e.Node Is Nothing OrElse e.Node.Tag Is Nothing Then
            lblNombreReporte.Text = "Selecciona un reporte"
            lblDescripcion.Text = "Elige una opción dentro de esta categoría."
            lblEstadoReporte.Text = ""
            tslEstado.Text = "Selecciona un reporte"
            Return
        End If

        Dim codigo = CStr(e.Node.Tag)
        Try
            PermisosReportes.Exigir(codigo)
        Catch ex As Exception
            lblEstadoReporte.Text = ex.Message
            If Sesion.IdUsuario <= 0 Then Close()
            Return
        End Try
        lblNombreReporte.Text = e.Node.Text
        lblEstadoReporte.Text = "Este reporte todavía no está disponible. Podrás generarlo cuando se complete su integración."
        tslEstado.Text = codigo & " · Pendiente de integración"

        Try
            Select Case codigo
                Case "R01"
                    lblDescripcion.Text="Recibo de pago de 80 mm, con saldo, monto en letras y marca de anulación."
                    lblSeleccion.Text="Abre el listado, selecciona un pago y utiliza Vista previa o Imprimir."
                    pnlSeleccion.Visible=True
                    btnVistaPrevia.Visible=True : btnImprimir.Visible=True
                Case "R02"
                    lblDescripcion.Text="Constancia de membresía vigente en papel Carta con márgenes configurables."
                    lblSeleccion.Text="Selecciona una membresía vigente en Membresías y pagos para emitir la constancia."
                    pnlSeleccion.Visible=True
                    btnVistaPrevia.Visible=True : btnImprimir.Visible=True
                Case "R03"
                    lblDescripcion.Text="Listado paginado de vencimientos con teléfono, saldo y espacio para registrar la llamada."
                    pnlDias.Visible=True
                    btnVistaPrevia.Visible=True : btnImprimir.Visible=True
                Case "R04"
                    lblDescripcion.Text="Ingresos por día y método de pago, con subtotales y resumen de anulados."
                    pnlFechas.Visible=True
                    btnVistaPrevia.Visible=True : btnExportarPdf.Visible=True : btnExportarExcel.Visible=True
                Case "R05"
                    lblDescripcion.Text="Estado de cuenta por membresía. Los pagos anulados no reducen el saldo."
                    pnlSocio.Visible=True
                    cboSocio.DisplayMember="socio"
                    cboSocio.ValueMember="id_socio"
                    cboSocio.DataSource=ReporteDAO.Socios()
                    cboSocio.Enabled=Sesion.Rol<>"Socio"
                    cboSocio.Visible=Sesion.Rol<>"Socio"
                    lblSocioSesion.Visible=Sesion.Rol="Socio"
                    lblSocioSesion.Text="Tu socio vinculado: " & Sesion.IdSocio.GetValueOrDefault().ToString()
                    btnVistaPrevia.Visible=True : btnExportarPdf.Visible=True
                Case "R06"
                    lblDescripcion.Text="Matriz de lunes a sábado con horas semanales por instructor y sala."
                    pnlHorario.Visible=True
                    Dim instructores=ReporteDAO.Instructores()
                    Dim fila=instructores.NewRow()
                    fila("id_instructor")=0 : fila("instructor")="Todos"
                    instructores.Rows.InsertAt(fila,0)
                    cboInstructor.DisplayMember="instructor" : cboInstructor.ValueMember="id_instructor" : cboInstructor.DataSource=instructores
                    Dim salas=ReporteDAO.Salas()
                    fila=salas.NewRow()
                    fila("id_sala")=0 : fila("sala")="Todas"
                    salas.Rows.InsertAt(fila,0)
                    cboSala.DisplayMember="sala" : cboSala.ValueMember="id_sala" : cboSala.DataSource=salas
                    cboSala.Enabled=True
                    cboInstructor.Enabled=Sesion.Rol<>"Instructor"
                    cboInstructor.Visible=Sesion.Rol<>"Instructor"
                    lblInstructorSesion.Visible=Sesion.Rol="Instructor"
                    lblInstructorSesion.Text="Tu instructor vinculado: " & Sesion.IdInstructor.GetValueOrDefault().ToString()
                    btnVistaPrevia.Visible=True : btnExportarPdf.Visible=True
                Case "R07"
                    lblDescripcion.Text="Cartera Excel con fórmulas, categorías, autofiltro y totales visibles."
                    pnlCartera.Visible=True : btnExportarExcel.Visible=True
                Case "R08"
                    lblDescripcion.Text="Informe mensual con indicadores reales, ingresos, saldos y horas de clase."
                    pnlMes.Visible=True : btnExportarPdf.Visible=True
                Case "R09"
                    lblDescripcion.Text="Libro Excel con doce meses y resumen recalculable; los anulados se excluyen de los totales."
                    pnlAnio.Visible=True : btnExportarExcel.Visible=True
            End Select
            btnVistaPrevia.Enabled=btnVistaPrevia.Visible
            btnImprimir.Enabled=btnImprimir.Visible
            btnExportarPdf.Enabled=btnExportarPdf.Visible
            btnExportarExcel.Enabled=btnExportarExcel.Visible
            lblEstadoReporte.Text="Disponible. Elige los parámetros y abre el reporte."
            tslEstado.Text=codigo & " · Disponible"
        Catch ex As Exception
            lblEstadoReporte.Text=ex.Message
            If Sesion.IdUsuario<=0 Then Close()
        End Try
    End Sub

    Private Sub OcultarParametros()
        btnVistaPrevia.Enabled = False
        btnImprimir.Enabled = False
        btnExportarPdf.Enabled = False
        btnExportarExcel.Enabled = False
        pnlSeleccion.Visible = False
        pnlDias.Visible = False
        pnlFechas.Visible = False
        pnlSocio.Visible = False
        pnlHorario.Visible = False
        pnlCartera.Visible = False
        pnlMes.Visible = False
        pnlAnio.Visible = False
        btnVistaPrevia.Visible = False
        btnImprimir.Visible = False
        btnExportarPdf.Visible = False
        btnExportarExcel.Visible = False
    End Sub

    Private Sub AbrirRecibos()
        Try
            If tvReportes.SelectedNode Is Nothing OrElse CStr(tvReportes.SelectedNode.Tag) <> "R01" Then Return
            PermisosReportes.Exigir("R01")
            Using formulario As New frmRecibos()
                formulario.ShowDialog(Me)
            End Using
        Catch ex As Exception
            lblEstadoReporte.Text = ex.Message
            If Sesion.IdUsuario <= 0 Then Close()
        End Try
    End Sub

    Private Sub btnVistaPrevia_Click(sender As Object, e As EventArgs) Handles btnVistaPrevia.Click
        AbrirReporteSeleccionado()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        AbrirReporteSeleccionado()
    End Sub

    Private Sub frmCentroReportes_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Try
            PermisosReportes.ExigirCentro()
        Catch ex As Exception
            lblEstadoReporte.Text = ex.Message
            If Sesion.IdUsuario <= 0 OrElse TypeOf ex Is UnauthorizedAccessException Then Close()
            OcultarParametros()
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
    Private Sub AbrirReporteSeleccionado()
        If tvReportes.SelectedNode Is Nothing OrElse tvReportes.SelectedNode.Tag Is Nothing Then Return
        Dim codigo=CStr(tvReportes.SelectedNode.Tag)
        Try
            PermisosReportes.Exigir(codigo)
            Select Case codigo
                Case "R01"
                    AbrirRecibos()
                Case "R02"
                    Using formulario As New frmMembresiasPagos()
                        formulario.ShowDialog(Me)
                    End Using
                Case "R03"
                    Using formulario As New frmListadoVencimientos(CInt(nudDiasAdelante.Value),CInt(nudDiasAtras.Value),chkSoloConSaldo.Checked,chkHorizontal.Checked)
                        formulario.ShowDialog(Me)
                    End Using
                Case "R04", "R05", "R06"
                    Dim socio=0
                    Dim instructor=0
                    Dim sala=0
                    Dim desde=dtpDesde.Value.Date
                    If codigo="R05" Then
                        desde=dtpCuentaDesde.Value.Date
                        If Sesion.Rol="Socio" Then
                            socio=PermisosReportes.ObtenerSocioPermitido(0)
                        Else
                            If cboSocio.SelectedValue Is Nothing Then Throw New ArgumentException("Selecciona un socio.")
                            socio=Convert.ToInt32(cboSocio.SelectedValue)
                        End If
                    End If
                    If codigo="R06" Then
                        If cboInstructor.SelectedValue IsNot Nothing Then instructor=Convert.ToInt32(cboInstructor.SelectedValue)
                        instructor=PermisosReportes.ObtenerInstructorPermitido(instructor)
                        If cboSala.SelectedValue IsNot Nothing Then sala=Convert.ToInt32(cboSala.SelectedValue)
                    End If
                    Using formulario As New frmVisorReportes(codigo,desde,dtpHasta.Value.Date,CStr(cboMetodo.SelectedItem),socio,instructor,sala)
                        formulario.ShowDialog(Me)
                    End Using
                Case "R07", "R08", "R09"
                    Using formulario As New frmExportarArchivos(codigo,dtpMes.Value,CInt(nudAnio.Value),chkIncluirAlDia.Checked)
                        formulario.ShowDialog(Me)
                    End Using
            End Select
            If Sesion.IdUsuario<=0 Then Close()
        Catch ex As Exception
            lblEstadoReporte.Text=ex.Message
            If Sesion.IdUsuario<=0 Then Close()
        End Try
    End Sub

    Private Sub btnExportarPdf_Click(sender As Object,e As EventArgs) Handles btnExportarPdf.Click,btnExportarExcel.Click
        AbrirReporteSeleccionado()
    End Sub
End Class
