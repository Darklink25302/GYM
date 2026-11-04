Imports ClosedXML.Excel
Imports System.Data

Public Class ExcelLibroAnual

    Public Sub Generar(anio As Integer, pagos As DataTable, ruta As String)
        Using libro As New XLWorkbook()
            Dim meses As String() = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                                     "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

            Dim hojaResumen = libro.Worksheets.Add("Resumen")

            For i As Integer = 0 To 11
                Dim hojaMes = libro.Worksheets.Add(meses(i))
                hojaMes.Cell(1, 1).Value = $"Ingresos - {meses(i)} {anio}"
                hojaMes.Cell(1, 1).Style.Font.Bold = True
                hojaMes.Cell(3, 1).Value = "Fecha"
                hojaMes.Cell(3, 6).Value = "Monto"
                hojaMes.Column(6).Style.NumberFormat.Format = "$ #,##0.00"
            Next

            Dim hojaEnero = libro.Worksheet("Enero")
            hojaEnero.Cell(4, 1).Value = New DateTime(anio, 1, 15)
            hojaEnero.Cell(4, 6).Value = 800

            hojaResumen.Cell(1, 1).Value = $"Resumen Anual - {anio}"
            hojaResumen.Cell(1, 1).Style.Font.Bold = True

            hojaResumen.Cell(3, 1).Value = "Mes"
            hojaResumen.Cell(3, 2).Value = "Efectivo"

            For i As Integer = 0 To 11
                Dim fila As Integer = 4 + i
                Dim nombreMes As String = meses(i)
                hojaResumen.Cell(fila, 1).Value = nombreMes
                hojaResumen.Cell(fila, 2).FormulaA1 = $"SUMIFS('{nombreMes}'!F:F, '{nombreMes}'!E:E, ""Efectivo"")"
            Next

            libro.SaveAs(ruta)
        End Using
    End Sub

End Class