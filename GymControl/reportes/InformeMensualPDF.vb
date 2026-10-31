Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure

Public Class InformeMensualPDF

    Public Sub Generar(mes As Integer, anio As Integer, ruta As String)

        QuestPDF.Settings.License = LicenseType.Community

        Dim sociosActivos As Integer = 45
        Dim ingresosMes As Decimal = 15400.5D

        Document.Create(Sub(container)
                            container.Page(Sub(page)
                                               page.Size(PageSizes.Letter)
                                               page.Margin(2, Unit.Centimetre)
                                               page.DefaultTextStyle(TextStyle.Default.FontSize(10))

                                               page.Content().Column(Sub(col)
                                                                         col.Item().Text($"INFORME MENSUAL - {mes}/{anio}").FontSize(16).Bold()
                                                                         col.Item().Text($"Socios Activos: {sociosActivos}")
                                                                         col.Item().Text($"Ingresos: {ingresosMes.ToString("C2")}")
                                                                     End Sub)
                                           End Sub)
                        End Sub).GeneratePdf(ruta)

    End Sub

End Class