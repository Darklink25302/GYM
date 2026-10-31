Public Class frmExportarArchivos


    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        ' 1. Validar que se seleccione una opción
        If Not rdbCartera.Checked AndAlso Not rdbInformeMensual.Checked AndAlso Not rdbLibroAnual.Checked Then
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
            End If

            ' 3. Mostrar el diálogo para elegir la ruta
            If dialog.ShowDialog() = DialogResult.OK Then
                Try
                    ' Deshabilitar botón mientras genera
                    btnGenerar.Enabled = False
                    btnGenerar.Text = "Generando..."

                    ' AQUÍ CONECTAREMOS TUS CLASES MÁS ADELANTE
                    If rdbLibroAnual.Checked Then
                        ' Llamada a tu clase R09
                        Dim generador As New ExcelLibroAnual()
                        ' Por ahora le pasamos un DataTable vacío
                        generador.Generar(CInt(nudAnios.Value), New DataTable(), dialog.FileName)

                    ElseIf rdbInformeMensual.Checked Then
                        MessageBox.Show("Aquí llamaremos a PdfInformeMensual (R08) pronto.")
                    End If

                    MessageBox.Show("Reporte generado con éxito en: " & dialog.FileName, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Si marcó "Abrir al terminar", abrimos el archivo
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