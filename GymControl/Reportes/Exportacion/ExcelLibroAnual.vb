Option Strict On

Imports ClosedXML.Excel

Public Class ExcelLibroAnual
    Public Sub Generar(anio As Integer,pagos As DataTable,ruta As String)
        If anio<1900 OrElse anio>9998 Then Throw New ArgumentException("Revisa el año del libro.")
        Using libro As New XLWorkbook()
            libro.CalculateMode=XLCalculateMode.Auto
            Dim resumen=libro.Worksheets.Add("Resumen")
            resumen.Cell(1,1).Value="GIMNASIO TITÁN · Ingresos " & anio.ToString()
            resumen.Range(1,1,1,7).Merge().Style.Font.SetBold().Font.SetFontSize(15)
            Dim meses={"Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"}
            Dim cabecera={"Fecha","Recibo","Socio","Plan","Método","Monto","Anulado"}
            Dim cabResumen={"Mes","Efectivo","Tarjeta","Transferencia","Otros / sin registrar","Total válido","Variación"}
            For i=0 To 6
                resumen.Cell(3,i+1).Value=cabResumen(i)
            Next
            For mes=1 To 12
                Dim nombre=meses(mes-1)
                Dim hoja=libro.Worksheets.Add(nombre)
                hoja.Cell(1,1).Value="Ingresos · " & nombre & " " & anio.ToString()
                hoja.Range(1,1,1,7).Merge().Style.Font.SetBold()
                For i=0 To 6
                    hoja.Cell(3,i+1).Value=cabecera(i)
                Next
                Dim fila=4
                For Each pago As DataRow In pagos.Rows
                    Dim fecha=Convert.ToDateTime(pago("fecha"))
                    If fecha.Year<>anio OrElse fecha.Month<>mes Then Continue For
                    hoja.Cell(fila,1).Value=fecha
                    hoja.Cell(fila,2).Value=Convert.ToInt32(pago("id_pago"))
                    hoja.Cell(fila,3).Value=CStr(pago("socio"))
                    hoja.Cell(fila,4).Value=CStr(pago("plan"))
                    hoja.Cell(fila,5).Value=CStr(pago("metodo_pago"))
                    hoja.Cell(fila,6).Value=Convert.ToDecimal(pago("monto"))
                    hoja.Cell(fila,7).Value=If(Convert.ToBoolean(pago("anulado")),"Sí","No")
                    If Convert.ToBoolean(pago("anulado")) Then
                        hoja.Range(fila,1,fila,7).Style.Font.FontColor=XLColor.Gray
                        hoja.Range(fila,1,fila,7).Style.Font.Strikethrough=True
                    End If
                    fila+=1
                Next
                If fila=4 Then
                    hoja.Cell(4,3).Value="Sin pagos registrados"
                    fila=5
                End If
                Dim ultima=fila-1
                hoja.Range(3,1,ultima,7).CreateTable("PagosMes" & mes.ToString()).Theme=XLTableTheme.TableStyleMedium2
                hoja.Cell(fila,5).Value="TOTAL VÁLIDO"
                hoja.Cell(fila,6).FormulaA1="SUMIF(G4:G" & ultima.ToString() & ",""No"",F4:F" & ultima.ToString() & ")"
                hoja.Range(4,1,ultima,1).Style.DateFormat.Format="dd/mm/yyyy hh:mm"
                hoja.Range(4,6,fila,6).Style.NumberFormat.Format="""C$ ""#,##0.00"
                hoja.Columns().AdjustToContents()
                hoja.Column(3).Width=35
                hoja.SheetView.FreezeRows(3)
                Dim r=mes+3
                resumen.Cell(r,1).Value=nombre
                For i=0 To 2
                    Dim metodo=cabResumen(i+1)
                    resumen.Cell(r,i+2).FormulaA1="SUMIFS('" & nombre & "'!F4:F" & ultima.ToString() & ",'" & nombre & "'!E4:E" & ultima.ToString() & ",""" & metodo & """,'" & nombre & "'!G4:G" & ultima.ToString() & ",""No"")"
                Next
                resumen.Cell(r,6).FormulaA1="'" & nombre & "'!F" & fila.ToString()
                resumen.Cell(r,5).FormulaA1="F" & r.ToString() & "-SUM(B" & r.ToString() & ":D" & r.ToString() & ")"
                If mes>1 Then resumen.Cell(r,7).FormulaA1="IFERROR((F" & r.ToString() & "-F" & (r-1).ToString() & ")/F" & (r-1).ToString() & ",0)"
            Next
            resumen.Cell(16,1).Value="TOTAL ANUAL"
            For i=2 To 6
                Dim letra=resumen.Cell(1,i).Address.ColumnLetter
                resumen.Cell(16,i).FormulaA1="SUM(" & letra & "4:" & letra & "15)"
            Next
            resumen.Range(3,1,3,7).Style.Fill.BackgroundColor=XLColor.LightBlue
            resumen.Range(3,1,3,7).Style.Font.Bold=True
            resumen.Range(16,1,16,7).Style.Font.Bold=True
            resumen.Range(4,2,16,6).Style.NumberFormat.Format="""C$ ""#,##0.00"
            resumen.Range(4,7,15,7).Style.NumberFormat.Format="0.00%"
            resumen.Range(5,7,15,7).AddConditionalFormat().WhenGreaterThan(0).Font.SetFontColor(XLColor.DarkGreen)
            resumen.Range(5,7,15,7).AddConditionalFormat().WhenLessThan(0).Font.SetFontColor(XLColor.Red)
            resumen.Columns().AdjustToContents()
            resumen.Protect()
            libro.SaveAs(ruta)
        End Using
    End Sub
End Class
