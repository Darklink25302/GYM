Option Strict On

Imports System.Diagnostics
Imports System.IO

Public Class frmExportarArchivos
    Private codigoInicial As String="R07"
    Private mesInicial As Date=Date.Today
    Private anioInicial As Integer=Date.Today.Year
    Private incluirInicial As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(codigo As String,mes As Date,anio As Integer,incluirAlDia As Boolean)
        Me.New()
        codigoInicial=codigo : mesInicial=mes : anioInicial=anio : incluirInicial=incluirAlDia
    End Sub

    Private Sub frmExportarArchivos_Load(sender As Object,e As EventArgs) Handles MyBase.Load
        Try
            PermisosMenu.Exigir("Administrador")
            dtpMes.Value=mesInicial
            nudAnio.Value=anioInicial
            chkIncluirAlDia.Checked=incluirInicial
            rdbCartera.Checked=codigoInicial="R07"
            rdbInformeMensual.Checked=codigoInicial="R08"
            rdbLibroAnual.Checked=codigoInicial="R09"
            ActualizarParametros()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Exportar reportes")
            Close()
        End Try
    End Sub

    Private Function Codigo() As String
        If rdbCartera.Checked Then Return "R07"
        If rdbInformeMensual.Checked Then Return "R08"
        If rdbLibroAnual.Checked Then Return "R09"
        Throw New ArgumentException("Selecciona un reporte.")
    End Function

    Private Sub ActualizarParametros() Handles rdbCartera.CheckedChanged,rdbInformeMensual.CheckedChanged,rdbLibroAnual.CheckedChanged
        dtpMes.Enabled=rdbInformeMensual.Checked
        nudAnio.Enabled=rdbLibroAnual.Checked
        chkIncluirAlDia.Enabled=rdbCartera.Checked
        txtRuta.Clear()
    End Sub

    Private Function ElegirRuta() As Boolean
        Using dialogo As New SaveFileDialog()
            Dim extension=If(Codigo()="R08","pdf","xlsx")
            dialogo.Filter=extension.ToUpperInvariant() & "|*." & extension
            dialogo.FileName=Codigo() & "_" & If(Codigo()="R08",dtpMes.Value.ToString("yyyyMM"),If(Codigo()="R09",nudAnio.Value.ToString(),Date.Today.ToString("yyyyMMdd"))) & "." & extension
            If dialogo.ShowDialog(Me)<>DialogResult.OK Then Return False
            txtRuta.Text=dialogo.FileName
            Return True
        End Using
    End Function

    Private Sub btnExaminar_Click(sender As Object,e As EventArgs) Handles btnExaminar.Click
        Try
            ElegirRuta()
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Guardar reporte")
        End Try
    End Sub

    Private Sub btnGenerar_Click(sender As Object,e As EventArgs) Handles btnGenerar.Click
        Try
            Dim reporte=Codigo()
            PermisosReportes.Exigir(reporte)
            If txtRuta.Text="" AndAlso Not ElegirRuta() Then Return
            btnGenerar.Enabled=False
            UseWaitCursor=True
            Select Case reporte
                Case "R07"
                    Dim datos=ReporteDAO.Cartera(chkIncluirAlDia.Checked)
                    PermisosReportes.Exigir("R07")
                    Dim generador As New ExcelCartera()
                    generador.Generar(datos,txtRuta.Text)
                Case "R08"
                    Dim mes=dtpMes.Value.Month
                    Dim anio=dtpMes.Value.Year
                    Dim indicadores=ReporteDAO.Indicadores(mes,anio)
                    Dim ingresos=ReporteDAO.IngresosPorPlan(mes,anio)
                    Dim saldos=ReporteDAO.SaldosMayores()
                    Dim horas=ReporteDAO.HorasPorInstructor()
                    PermisosReportes.Exigir("R08")
                    Dim generador As New PdfInformeMensual()
                    generador.Generar(mes,anio,indicadores,ingresos,saldos,horas,Sesion.NombreUsuario,txtRuta.Text)
                Case "R09"
                    Dim anio=CInt(nudAnio.Value)
                    Dim datos=ReporteDAO.Ingresos(New Date(anio,1,1),New Date(anio,12,31),"Todos","R09")
                    PermisosReportes.Exigir("R09")
                    Dim generador As New ExcelLibroAnual()
                    generador.Generar(anio,datos,txtRuta.Text)
            End Select
            lblEstado.Text="Guardado correctamente: " & txtRuta.Text
            If chkAbrirAlTerminar.Checked Then Process.Start(New ProcessStartInfo(txtRuta.Text) With {.UseShellExecute=True})
        Catch ex As Exception
            MessageBox.Show(ex.Message,"No se pudo generar el reporte")
            If Sesion.IdUsuario<=0 Then Close()
        Finally
            btnGenerar.Enabled=True
            UseWaitCursor=False
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object,e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
