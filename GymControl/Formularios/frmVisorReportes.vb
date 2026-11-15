Option Strict On

Imports Microsoft.Reporting.WinForms
Imports System.IO

Public Class frmVisorReportes
    Private visor As ReportViewer
    Private codigo As String
    Private desde As Date
    Private hasta As Date
    Private metodo As String
    Private idSocio As Integer
    Private idInstructor As Integer
    Private idSala As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(reporte As String, fechaDesde As Date, fechaHasta As Date, metodoPago As String, socio As Integer, Optional instructor As Integer=0, Optional sala As Integer=0)
        Me.New()
        codigo=reporte : desde=fechaDesde : hasta=fechaHasta : metodo=metodoPago
        idSocio=socio : idInstructor=instructor : idSala=sala
    End Sub

    Private Sub frmVisorReportes_Load(sender As Object,e As EventArgs) Handles MyBase.Load
        Try
            ValidarAcceso()
            visor=New ReportViewer()
            visor.Dock=DockStyle.Fill
            visor.ProcessingMode=ProcessingMode.Local
            visor.ShowExportButton=False
            visor.ShowRefreshButton=False
            pnlVisor.Controls.Add(visor)
            AddHandler visor.PrintingBegin, AddressOf ValidarImpresion
            AddHandler visor.ReportError, AddressOf ErrorDelReporte
            Actualizar()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Visor de reportes")
            Close()
        End Try
    End Sub

    Private Sub ValidarAcceso()
        PermisosReportes.Exigir(codigo)
        If codigo="R05" Then idSocio=PermisosReportes.ObtenerSocioPermitido(idSocio)
        If codigo="R06" Then idInstructor=PermisosReportes.ObtenerInstructorPermitido(idInstructor)
    End Sub

    Private Sub Actualizar()
        ValidarAcceso()
        PreparadorReportes.Cargar(visor.LocalReport,codigo,desde,hasta,metodo,idSocio,idInstructor,idSala)
        visor.RefreshReport()
    End Sub

    Private Sub btnActualizar_Click(sender As Object,e As EventArgs) Handles btnActualizar.Click
        Try
            Actualizar()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"No se pudo actualizar")
            If Sesion.IdUsuario<=0 Then Close()
        End Try
    End Sub

    Private Sub Exportar(formato As String,extension As String)
        Try
            ValidarAcceso()
            Using dialogo As New SaveFileDialog()
                dialogo.Filter=extension.ToUpperInvariant() & "|*." & extension
                dialogo.FileName=codigo & "_" & Date.Today.ToString("yyyyMMdd") & "." & extension
                If dialogo.ShowDialog(Me)<>DialogResult.OK Then Return
                Using reporte=PreparadorReportes.Crear(codigo,desde,hasta,metodo,idSocio,idInstructor,idSala)
                    Dim contenido=reporte.Render(formato)
                    ValidarAcceso()
                    File.WriteAllBytes(dialogo.FileName,contenido)
                End Using
                lblEstado.Text="Archivo guardado: " & dialogo.FileName
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message,"No se pudo exportar")
            If Sesion.IdUsuario<=0 Then Close()
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object,e As EventArgs) Handles btnPDF.Click
        Exportar("PDF","pdf")
    End Sub

    Private Sub btnExcel_Click(sender As Object,e As EventArgs) Handles btnExcel.Click
        Exportar("EXCELOPENXML","xlsx")
    End Sub

    Private Sub ValidarImpresion(sender As Object,e As ReportPrintEventArgs)
        Try
            ValidarAcceso()
        Catch ex As Exception
            e.Cancel=True
            MessageBox.Show(ex.Message,"Impresión cancelada")
        End Try
    End Sub

    Private Sub ErrorDelReporte(sender As Object,e As ReportErrorEventArgs)
        e.Handled=True
        lblEstado.Text=e.Exception.Message
    End Sub

    Private Sub btnCerrar_Click(sender As Object,e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
