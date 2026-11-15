Option Strict On

Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure
Imports System.IO
Imports System.Globalization

Public Class PdfInformeMensual
    Private Shared Sub Tabla(contenedor As IContainer,titulo As String,datos As DataTable,campos As String(),nombres As String())
        contenedor.Column(Sub(col)
            col.Item().PaddingTop(12).Text(titulo).Bold().FontSize(12)
            col.Item().PaddingTop(5).Table(Sub(tabla)
                tabla.ColumnsDefinition(Sub(columnas)
                    For Each campo In campos
                        columnas.RelativeColumn()
                    Next
                End Sub)
                tabla.Header(Sub(cabecera)
                    For Each nombre In nombres
                        cabecera.Cell().Background(Colors.Blue.Lighten4).Padding(5).Text(nombre).Bold()
                    Next
                End Sub)
                If datos.Rows.Count=0 Then tabla.Cell().ColumnSpan(CUInt(campos.Length)).Padding(8).Text("Sin registros para este período.")
                For Each fila As DataRow In datos.Rows
                    For Each campo In campos
                        Dim valor=If(fila.IsNull(campo),"",Convert.ToString(fila(campo)))
                        If TypeOf fila(campo) Is Decimal Then valor=Convert.ToDecimal(fila(campo)).ToString("N2",CultureInfo.GetCultureInfo("es-NI"))
                        tabla.Cell().BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(valor)
                    Next
                Next
            End Sub)
        End Sub)
    End Sub

    Public Sub Generar(mes As Integer,anio As Integer,indicadores As DataTable,ingresos As DataTable,saldos As DataTable,horas As DataTable,usuario As String,ruta As String)
        Dim fecha As New Date(anio,mes,1)
        Dim dato=indicadores.Rows(0)
        Dim logo As Byte()
        Using memoria As New MemoryStream()
            My.Resources.logo_uni_azul.Save(memoria,System.Drawing.Imaging.ImageFormat.Png)
            logo=memoria.ToArray()
        End Using
        Document.Create(Sub(documento)
            documento.Page(Sub(pagina)
                pagina.Size(PageSizes.Letter)
                pagina.Margin(35)
                pagina.DefaultTextStyle(Function(estilo) estilo.FontSize(10).FontFamily("Lato"))
                pagina.Header().Row(Sub(fila)
                    fila.ConstantItem(65).Height(40).Image(logo).FitArea()
                    fila.RelativeItem().PaddingLeft(12).Column(Sub(col)
                        col.Item().Text("GIMNASIO TITÁN").Bold().FontSize(18)
                        col.Item().Text("R08 · Informe gerencial · " & fecha.ToString("MMMM yyyy",CultureInfo.GetCultureInfo("es-NI")))
                        col.Item().Text("Generado: " & Date.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(8)
                    End Sub)
                End Sub)
                pagina.Content().PaddingTop(15).Column(Sub(col)
                    col.Item().Row(Sub(fila)
                        Dim etiquetas={"Socios activos","Nuevas membresías","Ingresos del mes","Saldo actual por cobrar"}
                        Dim valores={Convert.ToInt32(dato("activos")).ToString(),Convert.ToInt32(dato("nuevas")).ToString(),
                            "C$ " & Convert.ToDecimal(dato("ingresos")).ToString("N2"),"C$ " & Convert.ToDecimal(dato("saldo")).ToString("N2")}
                        For i=0 To 3
                            Dim etiqueta=etiquetas(i)
                            Dim valor=valores(i)
                            fila.RelativeItem().Border(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(6).Column(Sub(caja)
                                caja.Item().Text(etiqueta).FontSize(8)
                                caja.Item().Text(valor).Bold().FontSize(12)
                            End Sub)
                        Next
                    End Sub)
                    col.Item().PaddingTop(6).Text("Ingresos y altas corresponden al mes; socios activos y saldos corresponden al día de generación. Las horas indican la programación semanal vigente.").FontSize(8)
                    Tabla(col.Item(),"Ingresos por tipo de membresía",ingresos,New String(){"plan","cantidad","total"},New String(){"Plan","Pagos","Total C$"})
                    Tabla(col.Item(),"Diez saldos más altos por socio",saldos,New String(){"socio","saldo"},New String(){"Socio","Saldo C$"})
                    Tabla(col.Item(),"Horas semanales de clase por instructor",horas,New String(){"instructor","horas"},New String(){"Instructor","Horas"})
                End Sub)
                pagina.Footer().AlignCenter().Text(Sub(texto)
                    texto.Span(usuario & " · Página ")
                    texto.CurrentPageNumber()
                    texto.Span(" de ")
                    texto.TotalPages()
                End Sub)
            End Sub)
        End Sub).GeneratePdf(ruta)
    End Sub
End Class
