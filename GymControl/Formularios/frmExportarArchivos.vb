Imports System.IO
Imports System.Diagnostics

Public Class frmExportarArchivos

    Private Sub frmExportarArchivos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Llenar el ComboBox de meses
        cmbMes.Items.Clear()
        cmbMes.Items.AddRange(New String() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"})
        cmbMes.SelectedIndex = DateTime.Now.Month - 1
        nudAnios.Value = DateTime.Now.Year
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        ' 1. Validar que se seleccione una opción (Incluyendo el Horario)
        If Not rdbCartera.Checked AndAlso Not rdbInformeMensual.Checked AndAlso Not rdbLibroAnual.Checked AndAlso Not rdbHorario.Checked Then
            MessageBox.Show("Por favor, seleccione un tipo de reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Configurar el SaveFileDialog
        Using dialog As New SaveFileDialog()
            dialog.Title = "Guardar Reporte"

            ' Configurar extensión y nombre según el reporte
            If rdbCartera.Checked Then
                dialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                dialog.FileName = $"Cartera_{DateTime.Now:yyyy-MM-dd}.xlsx"
            ElseIf rdbInformeMensual.Checked Then
                dialog.Filter = "Archivos PDF (*.pdf)|*.pdf"
                dialog.FileName = $"Informe_{nudAnios.Value}-{cmbMes.SelectedIndex + 1:00}.pdf"
            ElseIf rdbLibroAnual.Checked Then
                dialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"
                dialog.FileName = $"Ingresos_{nudAnios.Value}.xlsx"
            ElseIf rdbHorario.Checked Then
                dialog.Filter = "Archivos PDF (*.pdf)|*.pdf"
                dialog.FileName = $"Horario_Semanal.pdf"
            End If

            ' 3. Mostrar el diálogo para elegir la ruta
            If dialog.ShowDialog() = DialogResult.OK Then
                Try
                    ' Deshabilitar botón mientras genera
                    btnGenerar.Enabled = False
                    btnGenerar.Text = "Generando..."

                    ' 4. Ejecutar la generación según el reporte elegido
                    If rdbHorario.Checked Then
                        Dim generadorHorario As New PdfHorario()
                        generadorHorario.Generar(dialog.FileName)
                    ElseIf rdbLibroAnual.Checked Then
                        Dim generadorExcel As New ExcelLibroAnual()
                        generadorExcel.Generar(CInt(nudAnios.Value), New DataTable(), dialog.FileName)
                    ElseIf rdbInformeMensual.Checked Then
                        Dim generadorPdf As New InformeMensualPDF()
                        generadorPdf.Generar(cmbMes.SelectedIndex + 1, CInt(nudAnios.Value), dialog.FileName)
                    ElseIf rdbCartera.Checked Then
                        MessageBox.Show("El reporte de Cartera (R07) se conectará pronto.", "Aviso")
                    End If

                    MessageBox.Show("Reporte generado con éxito en: " & dialog.FileName, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' 5. Si marcó "Abrir al terminar", abrimos el archivo
                    If chkAbrirAlTerminar.Checked Then
                        Process.Start(New ProcessStartInfo(dialog.FileName) With {.UseShellExecute = True})
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error al generar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    ' Restaurar botón
                    btnGenerar.Enabled = True
                    btnGenerar.Text = "Generar y Guardar..."
                End Try
            End If
        End Using
    End Sub

End Class