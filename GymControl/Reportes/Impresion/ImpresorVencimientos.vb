Option Strict On

Imports System.Drawing.Printing

Public Class ImpresorVencimientos
    Implements IDisposable
    Public Property Datos As DataTable
    Public ReadOnly Property Documento As PrintDocument
    Private indice As Integer
    Private pagina As Integer

    Public Sub New(tabla As DataTable, horizontal As Boolean)
        Datos=tabla
        Documento=New PrintDocument()
        Documento.DocumentName="R03 · Listado de vencimientos"
        Documento.DefaultPageSettings.PaperSize=New PaperSize("Carta",850,1100)
        Documento.DefaultPageSettings.Margins=New Margins(55,55,55,55)
        Documento.DefaultPageSettings.Landscape=horizontal
        AddHandler Documento.BeginPrint,AddressOf Reiniciar
        AddHandler Documento.PrintPage,AddressOf Imprimir
    End Sub

    Private Sub Reiniciar(sender As Object,e As PrintEventArgs)
        indice=0
        pagina=0
    End Sub

    Private Sub Imprimir(sender As Object,e As PrintPageEventArgs)
        pagina+=1
        e.Graphics.PageUnit=GraphicsUnit.Millimeter
        e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX*0.254F,-e.PageSettings.HardMarginY*0.254F)
        Dim area As New RectangleF(e.MarginBounds.X*0.254F,e.MarginBounds.Y*0.254F,e.MarginBounds.Width*0.254F,e.MarginBounds.Height*0.254F)
        Dim proporciones={0.04F,0.23F,0.13F,0.14F,0.1F,0.07F,0.12F,0.17F}
        Dim cabeceras={"N.º","Socio","Teléfono","Plan","Vence","Días","Saldo C$","Llamada"}
        Dim y=area.Top
        Using titulo As New Font("Arial",12,FontStyle.Bold),normal As New Font("Arial",8),negrita As New Font("Arial",8,FontStyle.Bold),linea As New Pen(Color.Gray,0.15F)
            e.Graphics.DrawString("GIMNASIO TITÁN · Vencimientos",titulo,Brushes.Black,area.Left,y)
            y+=8
            e.Graphics.DrawString("Listado para contactar a los socios · " & Date.Today.ToString("dd/MM/yyyy"),normal,Brushes.Black,area.Left,y)
            y+=7
            Dim x=area.Left
            For i=0 To cabeceras.Length-1
                Dim ancho=area.Width*proporciones(i)
                e.Graphics.FillRectangle(Brushes.LightGray,x,y,ancho,10)
                e.Graphics.DrawString(cabeceras(i),negrita,Brushes.Black,New RectangleF(x+1,y+1,ancho-2,9))
                x+=ancho
            Next
            y+=10
            If Datos.Rows.Count=0 Then e.Graphics.DrawString("Sin membresías que coincidan con los filtros.",normal,Brushes.Black,area.Left,y+5)
            While indice<Datos.Rows.Count
                Dim fila=Datos.Rows(indice)
                Dim valores={CStr(indice+1),Convert.ToString(fila("socio")),Convert.ToString(fila("telefono")),CStr(fila("plan")),
                    Convert.ToDateTime(fila("vence")).ToString("dd/MM/yy"),Convert.ToString(fila("dias")),Convert.ToDecimal(fila("saldo")).ToString("N2"),""}
                Dim alto As Single=9
                For i=0 To valores.Length-1
                    alto=Math.Max(alto,e.Graphics.MeasureString(valores(i),normal,CInt(area.Width*proporciones(i)-2)).Height+3)
                Next
                If y+alto>area.Bottom-12 Then
                    e.HasMorePages=True
                    Exit While
                End If
                x=area.Left
                For i=0 To valores.Length-1
                    Dim ancho=area.Width*proporciones(i)
                    If indice Mod 2=1 Then e.Graphics.FillRectangle(Brushes.WhiteSmoke,x,y,ancho,alto)
                    e.Graphics.DrawRectangle(linea,x,y,ancho,alto)
                    e.Graphics.DrawString(valores(i),normal,Brushes.Black,New RectangleF(x+1,y+1,ancho-2,alto-1))
                    x+=ancho
                Next
                y+=alto
                indice+=1
            End While
            e.Graphics.DrawString("Página " & pagina.ToString() & " · " & Date.Now.ToString("dd/MM/yyyy HH:mm"),normal,Brushes.Black,area.Left,area.Bottom-8)
        End Using
        e.HasMorePages=indice<Datos.Rows.Count
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Documento.Dispose()
    End Sub
End Class
