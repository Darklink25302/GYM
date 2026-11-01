Option Strict On
Option Infer On

Public Class frmCentroReportes
    Public Sub New()
        InitializeComponent()
        If Sesion.IdUsuario <= 0 Then
            Throw New UnauthorizedAccessException("Debes iniciar sesión para consultar reportes.")
        End If

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

        Select Case Sesion.Rol
            Case "Administrador", "Recepcionista"
                AgregarReporte(impresion, "R01", "Recibo de pago")
                AgregarReporte(impresion, "R02", "Constancia de membresía")
                AgregarReporte(impresion, "R03", "Listado de vencimientos")
                If Sesion.Rol = "Administrador" Then
                    AgregarReporte(visor, "R04", "Ingresos por período")
                End If
                AgregarReporte(visor, "R05", "Estado de cuenta")
                AgregarReporte(visor, "R06", "Horario semanal")
                If Sesion.Rol = "Administrador" Then
                    AgregarReporte(archivos, "R07", "Cartera de saldos")
                    AgregarReporte(archivos, "R08", "Informe mensual")
                    AgregarReporte(archivos, "R09", "Libro de ingresos anual")
                End If
            Case "Instructor"
                AgregarReporte(visor, "R06", "Mi horario semanal")
            Case "Socio"
                AgregarReporte(visor, "R05", "Mi estado de cuenta")
                AgregarReporte(visor, "R06", "Horario semanal")
            Case Else
                Throw New UnauthorizedAccessException("Tu rol no tiene acceso a los reportes.")
        End Select

        If impresion.Nodes.Count > 0 Then tvReportes.Nodes.Add(impresion)
        If visor.Nodes.Count > 0 Then tvReportes.Nodes.Add(visor)
        If archivos.Nodes.Count > 0 Then tvReportes.Nodes.Add(archivos)
        tvReportes.ExpandAll()
        tvReportes.SelectedNode = tvReportes.Nodes(0).Nodes(0)
    End Sub

    Private Sub AgregarReporte(grupo As TreeNode, codigo As String, titulo As String)
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
        lblNombreReporte.Text = e.Node.Text
        lblEstadoReporte.Text = "Este reporte todavía no está disponible. Podrás generarlo cuando se complete su integración."
        tslEstado.Text = codigo & " · Pendiente de integración"

        Select Case codigo
            Case "R01"
                lblDescripcion.Text = "Recibo de un pago en ticket de 80 mm, con monto, método y saldo pendiente."
                lblSeleccion.Text = "Selecciona el pago en Membresías y pagos para imprimir o reimprimir su recibo."
                pnlSeleccion.Visible = True
                btnVistaPrevia.Visible = True
                btnImprimir.Visible = True
            Case "R02"
                lblDescripcion.Text = "Constancia de una membresía vigente en tamaño Carta, con los datos del socio y del plan."
                lblSeleccion.Text = "Selecciona una membresía vigente en Membresías y pagos para emitir la constancia."
                pnlSeleccion.Visible = True
                btnVistaPrevia.Visible = True
                btnImprimir.Visible = True
            Case "R03"
                lblDescripcion.Text = "Membresías próximas a vencer o vencidas, con teléfono y saldo para contactar al socio."
                pnlDias.Visible = True
                btnVistaPrevia.Visible = True
                btnImprimir.Visible = True
            Case "R04"
                lblDescripcion.Text = "Ingresos del período por día y método de pago. Los pagos anulados se informan por separado."
                pnlFechas.Visible = True
                btnVistaPrevia.Visible = True
                btnExportarPdf.Visible = True
                btnExportarExcel.Visible = True
            Case "R05"
                lblDescripcion.Text = "Membresías, pagos y saldo del socio. Los pagos anulados se muestran sin sumarlos a lo abonado."
                pnlSocio.Visible = True
                lblSocioSesion.Visible = Sesion.Rol = "Socio"
                cboSocio.Visible = Sesion.Rol <> "Socio"
                If Sesion.Rol = "Socio" Then
                    If Sesion.IdSocio.HasValue Then
                        lblSocioSesion.Text = "Tu socio vinculado: " & Sesion.IdSocio.Value.ToString()
                    Else
                        lblSocioSesion.Text = "Un administrador debe vincular tu cuenta con un socio."
                    End If
                End If
                btnVistaPrevia.Visible = True
                btnExportarPdf.Visible = True
            Case "R06"
                lblDescripcion.Text = "Clases semanales por día y hora, con instructor, actividad y sala."
                pnlHorario.Visible = True
                lblInstructorSesion.Visible = Sesion.Rol = "Instructor"
                cboInstructor.Visible = Sesion.Rol <> "Instructor"
                If Sesion.Rol = "Instructor" Then
                    If Sesion.IdInstructor.HasValue Then
                        lblInstructorSesion.Text = "Tu instructor vinculado: " & Sesion.IdInstructor.Value.ToString()
                    Else
                        lblInstructorSesion.Text = "Un administrador debe vincular tu cuenta con un instructor."
                    End If
                End If
                btnVistaPrevia.Visible = True
                btnExportarPdf.Visible = True
            Case "R07"
                lblDescripcion.Text = "Libro Excel con saldos pendientes, categorías de atraso, filtros y fórmulas."
                pnlCartera.Visible = True
                btnExportarExcel.Visible = True
            Case "R08"
                lblDescripcion.Text = "Informe PDF con indicadores del mes, ingresos por plan, saldos y horas de clase."
                pnlMes.Visible = True
                btnExportarPdf.Visible = True
            Case "R09"
                lblDescripcion.Text = "Libro Excel con las doce hojas mensuales y un resumen anual que se recalcula con fórmulas."
                pnlAnio.Visible = True
                btnExportarExcel.Visible = True
        End Select
    End Sub

    Private Sub OcultarParametros()
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

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
