Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure
Imports System.Data

Public Class PdfHorario

    Public Sub Generar(ruta As String)

        QuestPDF.Settings.License = LicenseType.Community

        ' Datos de prueba (Mock) para que veas la tabla funcionando
        Dim dt As New DataTable()
        dt.Columns.Add("hora_inicio")
        dt.Columns.Add("dia")
        dt.Columns.Add("actividad")
        dt.Columns.Add("instructor")
        dt.Columns.Add("sala")

        ' Insertamos filas de ejemplo
        dt.Rows.Add("08:00", "Lunes", "Spinning", "Juan", "Sala 1")
        dt.Rows.Add("08:00", "Miércoles", "Yoga", "Ana", "Sala 2")
        dt.Rows.Add("10:00", "Lunes", "Pesas", "Carlos", "Sala 3")

        Document.Create(Sub(container)
                            container.Page(Sub(page)
                                               page.Size(PageSizes.Letter.Landscape()) ' Horizontal para que quepa la tabla
                                               page.Margin(2, Unit.Centimetre)
                                               page.DefaultTextStyle(TextStyle.Default.FontSize(10))

                                               page.Header().Text("HORARIO SEMANAL").FontSize(18).Bold().AlignCenter()

                                               page.Content().PaddingVertical(20).Table(Sub(table)
                                                                                            table.ColumnsDefinition(Sub(columns)
                                                                                                                        columns.RelativeColumn(1.5F) ' Hora
                                                                                                                        columns.RelativeColumn(1) ' Día
                                                                                                                        columns.RelativeColumn(2.5F) ' Actividad
                                                                                                                        columns.RelativeColumn(1.5F) ' Instructor
                                                                                                                        columns.RelativeColumn(1) ' Sala
                                                                                                                    End Sub)

                                                                                            ' Encabezados
                                                                                            table.Header(Sub(header)
                                                                                                             header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Hora").Bold()
                                                                                                             header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Día").Bold()
                                                                                                             header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Actividad").Bold()
                                                                                                             header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Instructor").Bold()
                                                                                                             header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Sala").Bold()
                                                                                                         End Sub)

                                                                                            ' Filas
                                                                                            For Each row As DataRow In dt.Rows
                                                                                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("hora_inicio").ToString())
                                                                                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("dia").ToString())
                                                                                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("actividad").ToString())
                                                                                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("instructor").ToString())
                                                                                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("sala").ToString())
                                                                                            Next
                                                                                        End Sub)

                                               page.Footer().AlignCenter().Text(Sub(x)
                                                                                    x.Span("Página ")
                                                                                    x.CurrentPageNumber()
                                                                                End Sub)
                                           End Sub)
                        End Sub).GeneratePdf(ruta)

    End Sub

End Class