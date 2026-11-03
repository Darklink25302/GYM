Option Strict On
Option Infer On

Imports System.Drawing.Printing
Imports System.Globalization

Public Class ImpresorRecibo
    Implements IDisposable

    Public Property Datos As DatosRecibo
    Public ReadOnly Property Documento As PrintDocument
    Private ReadOnly cultura As New CultureInfo("es-NI")

    Public Sub New(datosIniciales As DatosRecibo)
        If datosIniciales Is Nothing Then Throw New ArgumentNullException(NameOf(datosIniciales))
        Datos = datosIniciales
        Documento = New PrintDocument()
        Documento.DocumentName = "Recibo GymControl"
        Documento.OriginAtMargins = False
        Documento.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
        Documento.DefaultPageSettings.PaperSize = New PaperSize("Ticket 80 mm", 315, 650)
        AddHandler Documento.QueryPageSettings, AddressOf ConfigurarPagina
        AddHandler Documento.PrintPage, AddressOf ImprimirPagina
    End Sub

    Private Sub ConfigurarPagina(sender As Object, e As QueryPageSettingsEventArgs)
        Using imagen As New Bitmap(1, 1)
            Using grafico = Graphics.FromImage(imagen)
                Dim alto = Dibujar(grafico, True)
                e.PageSettings.PaperSize = New PaperSize("Ticket 80 mm", 315, CInt(Math.Ceiling(alto * 100.0F / 25.4F)))
                e.PageSettings.Landscape = False
            End Using
        End Using
    End Sub

    Private Sub ImprimirPagina(sender As Object, e As PrintPageEventArgs)
        e.Graphics.PageUnit = GraphicsUnit.Millimeter
        e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX * 0.254F, -e.PageSettings.HardMarginY * 0.254F)
        Dibujar(e.Graphics)
        e.HasMorePages = False
    End Sub

    ' Devuelve la altura en milímetros; el mismo cálculo sirve para medir y dibujar.
    Public Function Dibujar(grafico As Graphics, Optional soloMedir As Boolean = False) As Single
        If Datos Is Nothing OrElse Datos.IdPago <= 0 OrElse Datos.Monto <= 0 Then
            Throw New ArgumentException("El recibo necesita un pago válido.")
        End If
        grafico.PageUnit = GraphicsUnit.Millimeter
        Dim y As Single = 4
        Using normal As New Font("Arial", 9.0F), negrita As New Font("Arial", 9.0F, FontStyle.Bold), titulo As New Font("Arial", 12.0F, FontStyle.Bold)
            y = Linea(grafico, "GIMNASIO TITÁN", titulo, y, True, soloMedir)
            y = Linea(grafico, "Recibo de pago", normal, y, True, soloMedir)
            y = Linea(grafico, "Recibo N.º " & Datos.IdPago.ToString("D6"), negrita, y, False, soloMedir)
            Dim fecha = Datos.Fecha.ToString("dd/MM/yyyy HH:mm", cultura)
            y = Linea(grafico, "Fecha: " & fecha, normal, y, False, soloMedir)
            y += 2
            y = Linea(grafico, "Socio: " & Datos.Socio, normal, y, False, soloMedir)
            y = Linea(grafico, "Cédula: " & Datos.Cedula, normal, y, False, soloMedir)
            y = Linea(grafico, "Plan: " & Datos.Plan, normal, y, False, soloMedir)
            y = Linea(grafico, Datos.Inicio.ToString("dd/MM/yyyy", cultura) & " al " & Datos.Vence.ToString("dd/MM/yyyy", cultura), normal, y, False, soloMedir)
            y += 2
            If Not soloMedir Then
                Using trazo As New Pen(Color.Black, 0.2F)
                    grafico.DrawLine(trazo, 4, y, 76, y)
                End Using
            End If
            y += 2
            y = Linea(grafico, "Abono (" & Datos.Metodo & "): C$ " & Datos.Monto.ToString("N2", cultura), negrita, y, False, soloMedir)
            y = Linea(grafico, NumeroALetras.Convertir(Datos.Monto), normal, y, False, soloMedir)
            y = Linea(grafico, "Saldo después del pago: C$ " & Datos.Saldo.ToString("N2", cultura), negrita, y, False, soloMedir)
            If Datos.Anulado Then y = Linea(grafico, "Estado: ANULADO", negrita, y, False, soloMedir)
            y += 2
            y = Linea(grafico, "Atendió: " & Datos.Usuario, normal, y, False, soloMedir)
            y = Linea(grafico, "¡Gracias por entrenar con nosotros!", normal, y, True, soloMedir)
        End Using
        If Datos.Anulado AndAlso Not soloMedir Then
            Dim estado = grafico.Save()
            grafico.TranslateTransform(40, y / 2)
            grafico.RotateTransform(-35)
            Using fuente As New Font("Arial", 26.0F, FontStyle.Bold), pincel As New SolidBrush(Color.FromArgb(100, 180, 0, 0)), formato As New StringFormat()
                formato.Alignment = StringAlignment.Center
                grafico.DrawString("ANULADO", fuente, pincel, 0, 0, formato)
            End Using
            grafico.Restore(estado)
        End If
        Return y + 4
    End Function

    Private Function Linea(grafico As Graphics, texto As String, fuente As Font, y As Single, centrar As Boolean, soloMedir As Boolean) As Single
        Using formato As New StringFormat()
            formato.Alignment = If(centrar, StringAlignment.Center, StringAlignment.Near)
            Dim alto = grafico.MeasureString(texto, fuente, New SizeF(72, 1000), formato).Height
            If Not soloMedir Then grafico.DrawString(texto, fuente, Brushes.Black, New RectangleF(4, y, 72, alto + 1), formato)
            Return y + alto + 1
        End Using
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        Documento.Dispose()
    End Sub
End Class
