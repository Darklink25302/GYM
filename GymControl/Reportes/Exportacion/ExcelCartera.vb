Option Strict On

Imports ClosedXML.Excel

Public Class ExcelCartera
    Public Sub Generar(datos As DataTable,ruta As String)
        Using libro As New XLWorkbook()
            libro.CalculateMode=XLCalculateMode.Auto
            Dim hoja=libro.Worksheets.Add("Cartera")
            hoja.Cell(1,1).Value="GIMNASIO TITÁN · Cartera de saldos · " & Date.Today.ToString("dd/MM/yyyy")
            hoja.Range(1,1,1,11).Merge().Style.Font.SetBold().Font.SetFontSize(15)
            Dim titulos={"Cédula","Socio","Teléfono","Plan","Inicio","Vence","Precio","Pagado","Saldo","Días vencido","Categoría"}
            For i=0 To titulos.Length-1
                hoja.Cell(3,i+1).Value=titulos(i)
            Next
            Dim fila=4
            For Each dato As DataRow In datos.Rows
                hoja.Cell(fila,1).Value=Convert.ToString(dato("cedula"))
                hoja.Cell(fila,2).Value=CStr(dato("socio"))
                hoja.Cell(fila,3).Value=Convert.ToString(dato("telefono"))
                hoja.Cell(fila,4).Value=CStr(dato("plan"))
                hoja.Cell(fila,5).Value=Convert.ToDateTime(dato("inicio"))
                hoja.Cell(fila,6).Value=Convert.ToDateTime(dato("vence"))
                hoja.Cell(fila,7).Value=Convert.ToDecimal(dato("precio_pactado"))
                hoja.Cell(fila,8).Value=Convert.ToDecimal(dato("pagado"))
                hoja.Cell(fila,9).FormulaA1="G" & fila.ToString() & "-H" & fila.ToString()
                hoja.Cell(fila,10).FormulaA1="MAX(0,TODAY()-F" & fila.ToString() & ")"
                hoja.Cell(fila,11).FormulaA1="IF(OR(I" & fila.ToString() & "<=0,J" & fila.ToString() & "=0),""Al día"",IF(J" & fila.ToString() & "<=30,""1–30"",IF(J" & fila.ToString() & "<=60,""31–60"",""Más de 60"")))"
                fila+=1
            Next
            If fila=4 Then
                hoja.Cell(4,1).Value="Sin registros"
                fila=5
            End If
            Dim ultima=fila-1
            Dim tabla=hoja.Range(3,1,ultima,11).CreateTable("CarteraDatos")
            tabla.Theme=XLTableTheme.TableStyleMedium2
            hoja.Cell(fila,2).Value="TOTAL VISIBLE"
            For i=7 To 9
                Dim letra=hoja.Cell(1,i).Address.ColumnLetter
                hoja.Cell(fila,i).FormulaA1="SUBTOTAL(109," & letra & "4:" & letra & ultima.ToString() & ")"
            Next
            hoja.Range(fila,1,fila,11).Style.Font.Bold=True
            hoja.Range(4,5,ultima,6).Style.DateFormat.Format="dd/mm/yyyy"
            hoja.Range(4,7,fila,9).Style.NumberFormat.Format="""C$ ""#,##0.00"
            hoja.Range(4,10,ultima,10).AddConditionalFormat().ColorScale().LowestValue(XLColor.LightGreen).Midpoint(XLCFContentType.Number,"30",XLColor.Yellow).HighestValue(XLColor.Red)
            hoja.Range(4,9,ultima,9).AddConditionalFormat().DataBar(XLColor.CornflowerBlue)
            hoja.SheetView.FreezeRows(3)
            hoja.Columns().AdjustToContents()
            hoja.Column(2).Width=35
            Dim resumen=libro.Worksheets.Add("Resumen")
            resumen.Cell(1,1).Value="Resumen por categoría"
            resumen.Cell(1,1).Style.Font.Bold=True
            resumen.Cell(3,1).Value="Categoría"
            resumen.Cell(3,2).Value="Membresías"
            resumen.Cell(3,3).Value="Saldo C$"
            Dim categorias={"Al día","1–30","31–60","Más de 60"}
            For i=0 To 3
                Dim r=i+4
                resumen.Cell(r,1).Value=categorias(i)
                resumen.Cell(r,2).FormulaA1="COUNTIF(Cartera!K4:K" & ultima.ToString() & ",A" & r.ToString() & ")"
                resumen.Cell(r,3).FormulaA1="SUMIF(Cartera!K4:K" & ultima.ToString() & ",A" & r.ToString() & ",Cartera!I4:I" & ultima.ToString() & ")"
            Next
            resumen.Range(4,3,7,3).Style.NumberFormat.Format="""C$ ""#,##0.00"
            resumen.Columns().AdjustToContents()
            libro.SaveAs(ruta)
        End Using
    End Sub
End Class
