Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure
Imports System.Data

Public Class PdfHorario

    Public Sub Generar(ruta As String)

        QuestPDF.Settings.License = LicenseType.Community
        QuestPDF.Settings.UseSystemFonts = True

        ' Datos de prueba (Mock)
        Dim dt As New DataTable()
        dt.Columns.Add("hora_inicio")
        dt.Columns.Add("dia")
        dt.Columns.Add("actividad")
        dt.Columns.Add("instructor")
        dt.Columns.Add("sala")
        dt.Columns.Add("duracion_min", GetType(Integer))

        dt.Rows.Add("08:00", "Lunes", "Spinning", "Juan", "Sala 1", 60)
        dt.Rows.Add("08:00", "Miércoles", "Yoga", "Ana", "Sala 2", 90)
        dt.Rows.Add("10:00", "Lunes", "Pesas", "Carlos", "Sala 3", 60)

        Document.Create(Sub(container)
                            container.Page(Sub(page)
                                               page.Size(PageSizes.Letter.Landscape())
                                               page.Margin(1.5F, Unit.Centimetre)
                                               page.DefaultTextStyle(TextStyle.Default.FontSize(10).FontFamily("Arial"))

                                               page.Header().Text("HORARIO SEMANAL Y OCUPACIÓN DE SALAS").FontSize(16).Bold().AlignCenter()

                                               page.Content().PaddingVertical(15).Column(Sub(col)

                                                                                             ' --- PRIMERA TABLA: MATRIZ DEL HORARIO ---
                                                                                             col.Item().Text("Matriz de Horarios").FontSize(12).Bold()
                                                                                             col.Item().PaddingTop(5).Table(Sub(table)
                                                                                                                                table.ColumnsDefinition(Sub(columns)
                                                                                                                                                            columns.RelativeColumn(1.5F)
                                                                                                                                                            columns.RelativeColumn(1)
                                                                                                                                                            columns.RelativeColumn(2.5F)
                                                                                                                                                            columns.RelativeColumn(1.5F)
                                                                                                                                                            columns.RelativeColumn(1)
                                                                                                                                                        End Sub)

                                                                                                                                table.Header(Sub(header)
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Hora").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Día").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Actividad").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Instructor").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Sala").Bold()
                                                                                                                                             End Sub)

                                                                                                                                For Each row As DataRow In dt.Rows
                                                                                                                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("hora_inicio").ToString())
                                                                                                                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("dia").ToString())
                                                                                                                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("actividad").ToString())
                                                                                                                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("instructor").ToString())
                                                                                                                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(row("sala").ToString())
                                                                                                                                Next
                                                                                                                            End Sub)

                                                                                             ' --- SEGUNDA TABLA: HORAS POR INSTRUCTOR Y SALA ---
                                                                                             col.Item().PaddingTop(30).Text("Ocupación Semanal (Horas)").FontSize(12).Bold()
                                                                                             col.Item().PaddingTop(5).Table(Sub(table)
                                                                                                                                table.ColumnsDefinition(Sub(columns)
                                                                                                                                                            columns.RelativeColumn(2)
                                                                                                                                                            columns.RelativeColumn(1)
                                                                                                                                                        End Sub)

                                                                                                                                table.Header(Sub(header)
                                                                                                                                                 header.Cell().Background(Colors.Blue.Lighten4).Padding(5).Text("Instructor / Sala").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Blue.Lighten4).Padding(5).Text("Total Horas").Bold()
                                                                                                                                             End Sub)

                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("Instructor: Juan")
                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("1.0 h")

                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("Instructor: Ana")
                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("1.5 h")

                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("Sala 1")
                                                                                                                                table.Cell().BorderBottom(1).Padding(5).Text("1.0 h")
                                                                                                                            End Sub)

                                                                                         End Sub)

                                               page.Footer().AlignCenter().Text(Sub(x)
                                                                                    x.Span("Página ")
                                                                                    x.CurrentPageNumber()
                                                                                End Sub)
                                           End Sub)
                        End Sub).GeneratePdf(ruta)

    End Sub

End Class