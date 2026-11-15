<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVisorReportes
    Inherits Form
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        pnlVisor = New Panel()
        btnActualizar = New Button()
        btnPDF = New Button()
        btnExcel = New Button()
        btnCerrar = New Button()
        lblEstado = New Label()
        SuspendLayout()
        pnlVisor.Name = "pnlVisor"
        pnlVisor.Location = New Point(15, 62)
        pnlVisor.Size = New Size(1150, 620)
        pnlVisor.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Controls.Add(pnlVisor)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Location = New Point(15, 12)
        btnActualizar.Size = New Size(130, 34)
        btnActualizar.Text = "Actualizar"
        Controls.Add(btnActualizar)
        btnPDF.Name = "btnPDF"
        btnPDF.Location = New Point(160, 12)
        btnPDF.Size = New Size(140, 34)
        btnPDF.Text = "Exportar PDF"
        Controls.Add(btnPDF)
        btnExcel.Name = "btnExcel"
        btnExcel.Location = New Point(315, 12)
        btnExcel.Size = New Size(150, 34)
        btnExcel.Text = "Exportar Excel"
        Controls.Add(btnExcel)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Location = New Point(1025, 12)
        btnCerrar.Size = New Size(140, 34)
        btnCerrar.Text = "Cerrar"
        btnCerrar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Controls.Add(btnCerrar)
        lblEstado.Name = "lblEstado"
        lblEstado.Location = New Point(15, 690)
        lblEstado.Size = New Size(1150, 26)
        lblEstado.Text = "La impresión está disponible en la barra del visor."
        lblEstado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Controls.Add(lblEstado)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1180, 730)
        MinimumSize = New Size(1196, 769)
        Name = "frmVisorReportes"
        Text = "GymControl · Visor de reportes"
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents pnlVisor As Panel
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnPDF As Button
    Friend WithEvents btnExcel As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblEstado As Label
End Class
