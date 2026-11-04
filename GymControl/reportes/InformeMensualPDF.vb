Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure

Public Class InformeMensualPDF

    Public Sub Generar(mes As Integer, anio As Integer, ruta As String)

        QuestPDF.Settings.License = LicenseType.Community
        QuestPDF.Settings.UseSystemFonts = True

        ' Datos de prueba (Mock)
        Dim sociosActivos As Integer = 45
        Dim nuevasMembresias As Integer = 12
        Dim ingresosMes As Decimal = 15400.5D
        Dim saldoPorCobrar As Decimal = 2350D

        Document.Create(Sub(container)
                            container.Page(Sub(page)
                                               page.Size(PageSizes.Letter)
                                               page.Margin(2, Unit.Centimetre)
                                               page.DefaultTextStyle(TextStyle.Default.FontSize(10).FontFamily("Arial"))

                                               ' ENCABEZADO
                                               page.Header().Column(Sub(col)
                                                                        col.Item().Row(Sub(row)
                                                                                           row.RelativeItem().Text("GYMCONTROL").FontSize(20).Bold().FontColor(Colors.Blue.Darken2)
                                                                                           row.RelativeItem().AlignRight().Text($"Informe Mensual - {mes}/{anio}")
                                                                                       End Sub)
                                                                        col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2)
                                                                    End Sub)

                                               ' CONTENIDO
                                               page.Content().PaddingVertical(15).Column(Sub(col)

                                                                                             ' A. INDICADORES
                                                                                             col.Item().Row(Sub(row)
                                                                                                                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(Sub(c)
                                                                                                                                                                                                      c.Item().Text("Socios Activos").FontSize(10).FontColor(Colors.Grey.Darken1)
                                                                                                                                                                                                      c.Item().Text(sociosActivos.ToString()).FontSize(16).Bold()
                                                                                                                                                                                                  End Sub)
                                                                                                                row.ConstantItem(10)
                                                                                                                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(Sub(c)
                                                                                                                                                                                                      c.Item().Text("Nuevas Membresías").FontSize(10).FontColor(Colors.Grey.Darken1)
                                                                                                                                                                                                      c.Item().Text(nuevasMembresias.ToString()).FontSize(16).Bold()
                                                                                                                                                                                                  End Sub)
                                                                                                                row.ConstantItem(10)
                                                                                                                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(Sub(c)
                                                                                                                                                                                                      c.Item().Text("Ingresos del Mes").FontSize(10).FontColor(Colors.Grey.Darken1)
                                                                                                                                                                                                      c.Item().Text(ingresosMes.ToString("C2")).FontSize(16).Bold().FontColor(Colors.Green.Darken2)
                                                                                                                                                                                                  End Sub)
                                                                                                                row.ConstantItem(10)
                                                                                                                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(Sub(c)
                                                                                                                                                                                                      c.Item().Text("Saldo por Cobrar").FontSize(10).FontColor(Colors.Grey.Darken1)
                                                                                                                                                                                                      c.Item().Text(saldoPorCobrar.ToString("C2")).FontSize(16).Bold().FontColor(Colors.Red.Darken2)
                                                                                                                                                                                                  End Sub)
                                                                                                            End Sub)

                                                                                             ' B. TABLA 1
                                                                                             col.Item().PaddingTop(20).Text("Ingresos por tipo de membresía").FontSize(12).Bold()
                                                                                             col.Item().PaddingTop(5).Table(Sub(table)
                                                                                                                                table.ColumnsDefinition(Sub(columns)
                                                                                                                                                            columns.RelativeColumn(3)
                                                                                                                                                            columns.RelativeColumn(2)
                                                                                                                                                            columns.RelativeColumn(2)
                                                                                                                                                        End Sub)
                                                                                                                                table.Header(Sub(header)
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Tipo").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Cantidad").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Total").Bold()
                                                                                                                                             End Sub)
                                                                                                                                table.Cell().Padding(5).Text("Mensual")
                                                                                                                                table.Cell().Padding(5).Text("15")
                                                                                                                                table.Cell().Padding(5).Text("12,000.00")
                                                                                                                                table.Cell().Padding(5).Text("Trimestral")
                                                                                                                                table.Cell().Padding(5).Text("5")
                                                                                                                                table.Cell().Padding(5).Text("3,400.50")
                                                                                                                            End Sub)

                                                                                             ' C. TABLA 2
                                                                                             col.Item().PaddingTop(20).Text("Top 10 Saldos Más Altos").FontSize(12).Bold()
                                                                                             col.Item().PaddingTop(5).Table(Sub(table)
                                                                                                                                table.ColumnsDefinition(Sub(columns)
                                                                                                                                                            columns.RelativeColumn(1)
                                                                                                                                                            columns.RelativeColumn(3)
                                                                                                                                                            columns.RelativeColumn(2)
                                                                                                                                                        End Sub)
                                                                                                                                table.Header(Sub(header)
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("#").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Socio").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Saldo").Bold()
                                                                                                                                             End Sub)
                                                                                                                                table.Cell().Padding(5).Text("1")
                                                                                                                                table.Cell().Padding(5).Text("María López")
                                                                                                                                table.Cell().Padding(5).Text("C$ 800.00")
                                                                                                                            End Sub)

                                                                                             ' D. TABLA 3
                                                                                             col.Item().PaddingTop(20).Text("Horas de Clase por Instructor").FontSize(12).Bold()
                                                                                             col.Item().PaddingTop(5).Table(Sub(table)
                                                                                                                                table.ColumnsDefinition(Sub(columns)
                                                                                                                                                            columns.RelativeColumn(3)
                                                                                                                                                            columns.RelativeColumn(2)
                                                                                                                                                        End Sub)
                                                                                                                                table.Header(Sub(header)
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Instructor").Bold()
                                                                                                                                                 header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Horas").Bold()
                                                                                                                                             End Sub)
                                                                                                                                table.Cell().Padding(5).Text("Juan Pérez")
                                                                                                                                table.Cell().Padding(5).Text("12.5 h")
                                                                                                                            End Sub)

                                                                                         End Sub)

                                               ' PIE
                                               page.Footer().AlignCenter().Text(Sub(x)
                                                                                    x.Span("Página ")
                                                                                    x.CurrentPageNumber()
                                                                                    x.Span(" de ")
                                                                                    x.TotalPages()
                                                                                End Sub)
                                           End Sub)
                        End Sub).GeneratePdf(ruta)

    End Sub

End Class