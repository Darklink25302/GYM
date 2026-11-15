Option Strict On

Imports System.Drawing.Printing
Imports System.Globalization

Public Class ImpresorConstancia
    Implements IDisposable

    Public Property Datos As DatosConstancia
    Public ReadOnly Property Documento As PrintDocument
    Private ReadOnly logo As Image
    Private ReadOnly cultura As New CultureInfo("es-NI")

    Public Sub New(datosIniciales As DatosConstancia, imagenLogo As Image)
        If datosIniciales Is Nothing Then Throw New ArgumentNullException(NameOf(datosIniciales))
        If imagenLogo Is Nothing Then Throw New ArgumentException("Agrega el logo del gimnasio a los recursos.")
        Datos = datosIniciales
        logo = imagenLogo
        Documento = New PrintDocument()
        Documento.DocumentName = "Constancia de membresía"
        Documento.OriginAtMargins = False
        Documento.DefaultPageSettings.PaperSize = New PaperSize("Carta", 850, 1100)
        Documento.DefaultPageSettings.Margins = New Margins(75, 75, 75, 75)
        AddHandler Documento.QueryPageSettings, AddressOf ConfigurarPagina
        AddHandler Documento.PrintPage, AddressOf ImprimirPagina
    End Sub

    Private Sub ConfigurarPagina(sender As Object, e As QueryPageSettingsEventArgs)
        e.PageSettings.Landscape = False
        e.PageSettings.PaperSize = New PaperSize("Carta", 850, 1100)
    End Sub

    Private Sub ImprimirPagina(sender As Object, e As PrintPageEventArgs)
        e.Graphics.PageUnit = GraphicsUnit.Millimeter
        e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX * 0.254F, -e.PageSettings.HardMarginY * 0.254F)
        Dim area As New RectangleF(e.MarginBounds.X * 0.254F, e.MarginBounds.Y * 0.254F,
                                  e.MarginBounds.Width * 0.254F, e.MarginBounds.Height * 0.254F)
        If Dibujar(e.Graphics, area, True) > area.Bottom Then
            Throw New ArgumentException("El contenido no cabe. Reduce los márgenes en Configurar página.")
        End If
        Dibujar(e.Graphics, area, False)
        e.HasMorePages = False
    End Sub

    Public Function Dibujar(grafico As Graphics, area As RectangleF, Optional soloMedir As Boolean = False) As Single
        If Datos Is Nothing OrElse Datos.IdMembresia <= 0 Then Throw New ArgumentException("Faltan los datos de la membresía.")
        If Datos.Estado <> "Activa" OrElse Datos.Inicio.Date > Date.Today OrElse Datos.Vence.Date < Date.Today Then
            Throw New ArgumentException("Solo se puede emitir la constancia de una membresía vigente.")
        End If
        If area.Width < 120 OrElse area.Height < 170 Then Throw New ArgumentException("Los márgenes dejan muy poco espacio para la constancia.")
        grafico.PageUnit = GraphicsUnit.Millimeter
        Dim y = area.Top
        Dim anchoLogo = Math.Min(35.0F, 22.0F * CSng(logo.Width) / CSng(logo.Height))
        Dim altoLogo = anchoLogo * CSng(logo.Height) / CSng(logo.Width)
        If Not soloMedir Then grafico.DrawImage(logo, area.Left + (area.Width - anchoLogo) / 2, y, anchoLogo, altoLogo)
        y += altoLogo + 4
        Using normal As New Font("Arial", 11), negrita As New Font("Arial", 11, FontStyle.Bold), titulo As New Font("Arial", 17, FontStyle.Bold)
            y = Texto(grafico, "GIMNASIO TITÁN", negrita, area, y, True, soloMedir)
            y = Texto(grafico, "CONSTANCIA", titulo, area, y, True, soloMedir) + 7
            Dim parrafo = "El Gimnasio Titán hace constar que " & Datos.Socio &
                          ", con cédula " & Datos.Cedula & ", cuenta con una membresía vigente del plan " &
                          Datos.Plan & ", desde el " & Datos.Inicio.ToString("dd/MM/yyyy") &
                          " hasta el " & Datos.Vence.ToString("dd/MM/yyyy") & "."
            y = Texto(grafico, parrafo, normal, area, y, False, soloMedir) + 6
            y = Fila(grafico, "Membresía", Datos.IdMembresia.ToString(), normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Plan", Datos.Plan, normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Período", Datos.Inicio.ToString("dd/MM/yyyy") & " al " & Datos.Vence.ToString("dd/MM/yyyy"), normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Estado", Datos.Estado, normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Precio pactado", "C$ " & Datos.Precio.ToString("N2", cultura), normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Abonado", "C$ " & Datos.Pagado.ToString("N2", cultura), normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Saldo pendiente", "C$ " & Datos.Saldo.ToString("N2", cultura), normal, negrita, area, y, soloMedir)
            y = Fila(grafico, "Estado de pagos", If(Datos.Saldo > 0, "Con saldo pendiente", "Sin saldo pendiente"), normal, negrita, area, y, soloMedir)
            y += 8
            y = Texto(grafico, "Se extiende la presente " & FechaEnLetras.Convertir(Date.Today) & ".", normal, area, y, False, soloMedir)
            y += 20
            If Not soloMedir Then
                Using linea As New Pen(Color.Black, 0.2F)
                    grafico.DrawLine(linea, area.Left + area.Width * 0.2F, y, area.Left + area.Width * 0.8F, y)
                End Using
            End If
            y += 2
            y = Texto(grafico, "Firma autorizada y sello", normal, area, y, True, soloMedir)
        End Using
        Return y
    End Function

    Private Function Texto(grafico As Graphics, contenido As String, fuente As Font, area As RectangleF, y As Single, centrar As Boolean, medir As Boolean) As Single
        Using formato As New StringFormat()
            formato.Alignment = If(centrar, StringAlignment.Center, StringAlignment.Near)
            Dim alto = grafico.MeasureString(contenido, fuente, New SizeF(area.Width, 10000), formato).Height + 1
            If Not medir Then grafico.DrawString(contenido, fuente, Brushes.Black, New RectangleF(area.Left, y, area.Width, alto), formato)
            Return y + alto
        End Using
    End Function

    Private Function Fila(grafico As Graphics, etiqueta As String, valor As String, normal As Font, negrita As Font, area As RectangleF, y As Single, medir As Boolean) As Single
        Dim izquierda = area.Width * 0.32F
        Dim derecha = area.Width - izquierda
        Dim alto = Math.Max(grafico.MeasureString(etiqueta, negrita, CInt(izquierda - 4)).Height,
                            grafico.MeasureString(valor, normal, CInt(derecha - 4)).Height) + 4
        If Not medir Then
            Using linea As New Pen(Color.Gray, 0.2F)
                grafico.DrawRectangle(linea, area.Left, y, izquierda, alto)
                grafico.DrawRectangle(linea, area.Left + izquierda, y, derecha, alto)
            End Using
            grafico.DrawString(etiqueta, negrita, Brushes.Black, New RectangleF(area.Left + 2, y + 2, izquierda - 4, alto - 3))
            grafico.DrawString(valor, normal, Brushes.Black, New RectangleF(area.Left + izquierda + 2, y + 2, derecha - 4, alto - 3))
        End If
        Return y + alto
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        Documento.Dispose()
    End Sub
End Class
