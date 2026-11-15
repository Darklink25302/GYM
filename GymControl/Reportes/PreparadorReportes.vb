Option Strict On

Imports Microsoft.Reporting.WinForms

Public Class PreparadorReportes
    Public Shared Function Crear(codigo As String, desde As Date, hasta As Date, metodo As String, idSocio As Integer, Optional idInstructor As Integer = 0, Optional idSala As Integer = 0) As LocalReport
        Dim reporte As New LocalReport()
        Try
            Cargar(reporte,codigo,desde,hasta,metodo,idSocio,idInstructor,idSala)
            Return reporte
        Catch
            reporte.Dispose()
            Throw
        End Try
    End Function

    Public Shared Sub Cargar(reporte As LocalReport, codigo As String, desde As Date, hasta As Date, metodo As String, idSocio As Integer, idInstructor As Integer, idSala As Integer)
        PermisosReportes.Exigir(codigo)
        Dim archivo As String
        Select Case codigo
            Case "R04" : archivo="R04_Ingresos.rdlc"
            Case "R05" : archivo="R05_EstadoCuenta.rdlc"
            Case "R06" : archivo="R06_Horario.rdlc"
            Case Else : Throw New ArgumentException("El reporte no utiliza el visor RDLC.")
        End Select
        reporte.DataSources.Clear()
        Using definicion=GetType(PreparadorReportes).Assembly.GetManifestResourceStream("GymControl." & archivo)
            If definicion Is Nothing Then Throw New InvalidOperationException("No se encontró el diseño del reporte.")
            reporte.LoadReportDefinition(definicion)
        End Using
        Dim titulo=""
        Dim subtitulo=""
        Dim parametros As New List(Of ReportParameter)
        Select Case codigo
            Case "R04"
                Dim todos=ReporteDAO.Ingresos(desde,hasta,metodo,"R04")
                Dim anulados As Integer=0
                Dim montoAnulado As Decimal=0
                For Each fila As DataRow In todos.Rows
                    If Convert.ToBoolean(fila("anulado")) Then
                        anulados+=1
                        montoAnulado+=Convert.ToDecimal(fila("monto"))
                    End If
                Next
                Dim vista As New DataView(todos)
                vista.RowFilter="anulado=FALSE"
                reporte.DataSources.Add(New ReportDataSource("DsIngresos",vista.ToTable()))
                titulo="R04 · Ingresos por período"
                subtitulo=desde.ToString("dd/MM/yyyy") & " al " & hasta.ToString("dd/MM/yyyy") & " · Método: " & metodo
                parametros.Add(New ReportParameter("Anulados","Pagos anulados en el período: " & anulados.ToString() & " · Importe: C$ " & montoAnulado.ToString("N2")))
            Case "R05"
                idSocio=PermisosReportes.ObtenerSocioPermitido(idSocio)
                Dim socios=ReporteDAO.Socios()
                Dim nombre=""
                For Each socio As DataRow In socios.Rows
                    If Convert.ToInt32(socio("id_socio"))=idSocio Then nombre=CStr(socio("socio"))
                Next
                If nombre="" Then Throw New ArgumentException("El socio no está disponible.")
                Dim tabla=ReporteDAO.EstadoCuenta(idSocio,desde)
                Dim saldos=ReporteDAO.SaldosSocio(idSocio)
                Dim saldo As Decimal=0
                For Each fila As DataRow In saldos.Rows
                    saldo+=Convert.ToDecimal(fila("saldo"))
                Next
                reporte.DataSources.Add(New ReportDataSource("DsCuenta",tabla))
                titulo="R05 · Estado de cuenta"
                subtitulo=nombre & " · Detalle desde " & desde.ToString("dd/MM/yyyy") & ". Los saldos incluyen todo el historial válido."
                parametros.Add(New ReportParameter("SaldoTotal","Saldo total del socio: C$ " & saldo.ToString("N2")))
                PermisosReportes.ObtenerSocioPermitido(idSocio)
            Case "R06"
                idInstructor=PermisosReportes.ObtenerInstructorPermitido(idInstructor)
                Dim horario=ReporteDAO.Horario(idInstructor,idSala)
                reporte.DataSources.Add(New ReportDataSource("DsHorario",MatrizHorario(horario)))
                reporte.DataSources.Add(New ReportDataSource("DsOcupacion",Ocupacion(horario)))
                titulo="R06 · Horario semanal y ocupación"
                subtitulo="Lunes a sábado · " & If(idInstructor=0,"Todos los instructores","Instructor vinculado: " & idInstructor.ToString()) &
                    " · " & If(idSala=0,"Todas las salas","Sala: " & idSala.ToString())
                PermisosReportes.ObtenerInstructorPermitido(idInstructor)
        End Select
        parametros.Add(New ReportParameter("Titulo",titulo))
        parametros.Add(New ReportParameter("Subtitulo",subtitulo))
        parametros.Add(New ReportParameter("Usuario",Sesion.NombreUsuario & " · " & Date.Now.ToString("dd/MM/yyyy HH:mm")))
        reporte.SetParameters(parametros)
        PermisosReportes.Exigir(codigo)
    End Sub

    Public Shared Function MatrizHorario(horario As DataTable) As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("hora",GetType(String))
        tabla.Columns.Add("dia",GetType(Integer))
        tabla.Columns.Add("nombre_dia",GetType(String))
        tabla.Columns.Add("detalle",GetType(String))
        Dim dias={"Lunes","Martes","Miércoles","Jueves","Viernes","Sábado"}
        Dim horas=horario.DefaultView.ToTable(True,"hora_inicio")
        If horas.Rows.Count=0 Then horas.Rows.Add("Sin clases")
        For Each hora As DataRow In horas.Rows
            For dia=1 To 6
                Dim texto=""
                For Each fila As DataRow In horario.Rows
                    If CStr(fila("hora_inicio"))=CStr(hora("hora_inicio")) AndAlso Convert.ToInt32(fila("dia_semana"))=dia Then
                        If texto<>"" Then texto &= Environment.NewLine & Environment.NewLine
                        texto &= CStr(fila("actividad")) & " (" & CStr(fila("hora_inicio")) & "–" & CStr(fila("hora_fin")) & ")" & Environment.NewLine &
                            CStr(fila("instructor")) & Environment.NewLine & CStr(fila("sala"))
                    End If
                Next
                tabla.Rows.Add(CStr(hora("hora_inicio")),dia,dias(dia-1),texto)
            Next
        Next
        Return tabla
    End Function

    Public Shared Function Ocupacion(horario As DataTable) As DataTable
        Dim tabla As New DataTable()
        tabla.Columns.Add("tipo",GetType(String))
        tabla.Columns.Add("nombre",GetType(String))
        tabla.Columns.Add("horas",GetType(Decimal))
        For Each campo In New String(){"instructor","sala"}
            Dim nombres=horario.DefaultView.ToTable(True,campo)
            For Each nombre As DataRow In nombres.Rows
                Dim minutos As Decimal=0
                For Each fila As DataRow In horario.Rows
                    If CStr(fila(campo))=CStr(nombre(campo)) Then minutos+=Convert.ToDecimal(fila("minutos"))
                Next
                tabla.Rows.Add(If(campo="instructor","Instructor","Sala"),CStr(nombre(campo)),minutos/60D)
            Next
        Next
        Return tabla
    End Function
End Class
