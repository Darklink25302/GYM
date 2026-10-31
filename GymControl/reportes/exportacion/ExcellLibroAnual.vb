Imports ClosedXML.Excel
Imports System.Data

Public Class ExcelLibroAnual

    ''' <summary>
    ''' Genera el libro anual R09 con 12 hojas mensuales + Resumen.
    ''' </summary>
    ''' <param name="anio">Año del libro (ej. 2026)</param>
    ''' <param name="pagos">DataTable con las columnas: Fecha, Recibo, Socio, Plan, Metodo, Monto, Anulado</param>
    ''' <param name="ruta">Ruta donde se guardará el archivo .xlsx</param>
    Public Sub Generar(anio As Integer, pagos As DataTable, ruta As String)
        ' 1. Crear el libro de Excel
        Using libro As New XLWorkbook()

            ' 2. Crear las 12 hojas
            Dim meses As String() = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                                     "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

            ' Primero creamos la hoja Resumen para que quede de primera
            Dim hojaResumen = libro.Worksheets.Add("Resumen")

            ' Creamos las hojas de los meses y les ponemos sus encabezados
            For i As Integer = 0 To 11
                Dim hojaMes = libro.Worksheets.Add(meses(i))

                ' Título y encabezados
                hojaMes.Cell(1, 1).Value = $"Ingresos - {meses(i)} {anio}"
                hojaMes.Cell(1, 1).Style.Font.Bold = True
                hojaMes.Cell(1, 1).Style.Font.FontSize = 14

                hojaMes.Cell(3, 1).Value = "Fecha"
                hojaMes.Cell(3, 2).Value = "Recibo"
                hojaMes.Cell(3, 3).Value = "Socio"
                hojaMes.Cell(3, 4).Value = "Plan"
                hojaMes.Cell(3, 5).Value = "Método"
                hojaMes.Cell(3, 6).Value = "Monto"
                hojaMes.Cell(3, 7).Value = "Anulado"

                ' Estilo de encabezados
                Dim rangoEncabezado = hojaMes.Range(3, 1, 3, 7)
                rangoEncabezado.Style.Font.Bold = True
                rangoEncabezado.Style.Fill.BackgroundColor = XLColor.LightGray
                rangoEncabezado.Style.Border.OutsideBorder = XLBorderStyleValues.Thin

                ' Formato de moneda para la columna Monto
                hojaMes.Column(6).Style.NumberFormat.Format = "$ #,##0.00"
            Next

            ' 3. Llenar los datos de prueba (Mock) por ahora
            ' NOTA: Cuando tengamos ReporteDAO, leeremos el DataTable "pagos" real. 
            ' Por ahora, simulamos que hay un pago en Enero y uno en Febrero.

            ' Simulamos pago en Enero
            Dim hojaEnero = libro.Worksheet("Enero")
            hojaEnero.Cell(4, 1).Value = New DateTime(anio, 1, 15)
            hojaEnero.Cell(4, 2).Value = "000123"
            hojaEnero.Cell(4, 3).Value = "Juan Pérez"
            hojaEnero.Cell(4, 4).Value = "Mensual"
            hojaEnero.Cell(4, 5).Value = "Efectivo"
            hojaEnero.Cell(4, 6).Value = 800
            hojaEnero.Cell(4, 7).Value = "No"

            ' Simulamos pago en Febrero
            Dim hojaFebrero = libro.Worksheet("Febrero")
            hojaFebrero.Cell(4, 1).Value = New DateTime(anio, 2, 10)
            hojaFebrero.Cell(4, 2).Value = "000124"
            hojaFebrero.Cell(4, 3).Value = "María López"
            hojaFebrero.Cell(4, 4).Value = "Trimestral"
            hojaFebrero.Cell(4, 5).Value = "Tarjeta"
            hojaFebrero.Cell(4, 6).Value = 2200
            hojaFebrero.Cell(4, 7).Value = "No"

            ' 4. Armar la hoja Resumen con fórmulas
            hojaResumen.Cell(1, 1).Value = $"Resumen Anual de Ingresos - {anio}"
            hojaResumen.Cell(1, 1).Style.Font.Bold = True
            hojaResumen.Cell(1, 1).Style.Font.FontSize = 16

            ' Encabezados del resumen
            hojaResumen.Cell(3, 1).Value = "Mes"
            hojaResumen.Cell(3, 2).Value = "Efectivo"
            hojaResumen.Cell(3, 3).Value = "Tarjeta"
            hojaResumen.Cell(3, 4).Value = "Transferencia"
            hojaResumen.Cell(3, 5).Value = "Total Válido"

            Dim rangoResumen = hojaResumen.Range(3, 1, 3, 5)
            rangoResumen.Style.Font.Bold = True
            rangoResumen.Style.Fill.BackgroundColor = XLColor.LightBlue

            ' Fórmulas para cada mes en el Resumen
            For i As Integer = 0 To 11
                Dim fila As Integer = 4 + i
                Dim nombreMes As String = meses(i)

                hojaResumen.Cell(fila, 1).Value = nombreMes

                ' Fórmulas SUMIFS. En ClosedXML se usan en inglés con comas.
                ' Estructura: SUMIFS(rango_suma, rango_criterio1, criterio1, rango_criterio2, criterio2)
                ' Rango de suma: Columna F (Monto) de la hoja del mes
                ' Criterio 1: Columna E (Método) = "Efectivo"
                ' Criterio 2: Columna G (Anulado) = "No"

                hojaResumen.Cell(fila, 2).FormulaA1 = $"SUMIFS('{nombreMes}'!F:F, '{nombreMes}'!E:E, ""Efectivo"", '{nombreMes}'!G:G, ""No"")"
                hojaResumen.Cell(fila, 3).FormulaA1 = $"SUMIFS('{nombreMes}'!F:F, '{nombreMes}'!E:E, ""Tarjeta"", '{nombreMes}'!G:G, ""No"")"
                hojaResumen.Cell(fila, 4).FormulaA1 = $"SUMIFS('{nombreMes}'!F:F, '{nombreMes}'!E:E, ""Transferencia"", '{nombreMes}'!G:G, ""No"")"
                hojaResumen.Cell(fila, 5).FormulaA1 = $"SUM('{nombreMes}'!F:F) - SUMIFS('{nombreMes}'!F:F, '{nombreMes}'!G:G, ""Si"")"
            Next

            ' Formato de moneda para todo el resumen
            hojaResumen.Range(4, 2, 15, 5).Style.NumberFormat.Format = "$ #,##0.00"

            ' Fila de Total Anual
            hojaResumen.Cell(16, 1).Value = "TOTAL ANUAL"
            hojaResumen.Cell(16, 1).Style.Font.Bold = True
            hojaResumen.Cell(16, 2).FormulaA1 = "SUM(B4:B15)"
            hojaResumen.Cell(16, 3).FormulaA1 = "SUM(C4:C15)"
            hojaResumen.Cell(16, 4).FormulaA1 = "SUM(D4:D15)"
            hojaResumen.Cell(16, 5).FormulaA1 = "SUM(E4:E15)"

            Dim rangoTotal = hojaResumen.Range(16, 1, 16, 5)
            rangoTotal.Style.Font.Bold = True
            rangoTotal.Style.Border.TopBorder = XLBorderStyleValues.Double

            ' Ajustar el tamaño de las columnas
            hojaResumen.Columns().AdjustToContents()

            ' 5. Guardar el archivo
            libro.SaveAs(ruta)

        End Using
    End Sub

End Class