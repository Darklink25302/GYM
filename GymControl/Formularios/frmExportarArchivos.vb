Imports System.IO
Imports System.Diagnostics

Public Class frmExportarArchivos

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If String.IsNullOrWhiteSpace(txtRuta.Text) Then
            MessageBox.Show("Por favor, seleccione una ruta para guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            If rdbHorario.Checked Then
                Dim generadorHorario As New PdfHorario()
                generadorHorario.Generar(txtRuta.Text)
            ElseIf rdbLibroAnual.Checked Then
                Dim generadorExcel As New ExcelLibroAnual()
                generadorExcel.Generar(CInt(nudAnios.Value), New DataTable(), txtRuta.Text)
            ElseIf rdbInformeMensual.Checked Then
                Dim generadorPdf As New InformeMensualPDF()
                generadorPdf.Generar(11, 2026, txtRuta.Text)
            End If
            MessageBox.Show("Generado con éxito")
            ' Abrir el archivo automáticamente si está marcada la casilla
            If chkAbrirAlTerminar.Checked Then
                Process.Start(New ProcessStartInfo(txtRuta.Text) With {.UseShellExecute = True})
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub nudAnios_ValueChanged(sender As Object, e As EventArgs) Handles nudAnios.ValueChanged

    End Sub

    Private Sub frmExportarArchivos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpMes.Value = DateTime.Now
        nudAnios.Value = DateTime.Now.Year
        'ActualizarRutaSugerida()
        dtpMes.Value = DateTime.Now
        nudAnios.Value = DateTime.Now.Year
        rdbCartera.Checked = True
        ' Ruta por defecto en el escritorio
        txtRuta.Text = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reporte.pdf")
    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        ' Abrir la ventanita para guardar
        If rdbInformeMensual.Checked OrElse rdbHorario.Checked Then
            sfdGuardar.Filter = "Archivos PDF (*.pdf)|*.pdf"
        Else
            sfdGuardar.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
        End If

        If sfdGuardar.ShowDialog() = DialogResult.OK Then
            txtRuta.Text = sfdGuardar.FileName
        End If
    End Sub
End Class