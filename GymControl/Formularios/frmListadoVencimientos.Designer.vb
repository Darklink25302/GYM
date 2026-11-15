<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmListadoVencimientos
    Inherits Form
    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If impresor IsNot Nothing Then impresor.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        dgvVencimientos = New DataGridView()
        lblEstado = New Label()
        btnVistaPrevia = New Button()
        btnImprimir = New Button()
        btnCerrar = New Button()
        CType(dgvVencimientos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        dgvVencimientos.Name = "dgvVencimientos"
        dgvVencimientos.Location = New Point(20, 65)
        dgvVencimientos.Size = New Size(1060, 510)
        dgvVencimientos.ReadOnly = True
        dgvVencimientos.MultiSelect = False
        dgvVencimientos.AllowUserToAddRows = False
        dgvVencimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvVencimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVencimientos.RowHeadersVisible = False
        Controls.Add(dgvVencimientos)
        lblEstado.Name = "lblEstado"
        lblEstado.Location = New Point(20, 15)
        lblEstado.Size = New Size(1060, 40)
        lblEstado.Text = "Listado de vencimientos"
        Controls.Add(lblEstado)
        btnVistaPrevia.Name = "btnVistaPrevia"
        btnVistaPrevia.Location = New Point(20, 600)
        btnVistaPrevia.Size = New Size(170, 36)
        btnVistaPrevia.Text = "Vista previa"
        Controls.Add(btnVistaPrevia)
        btnImprimir.Name = "btnImprimir"
        btnImprimir.Location = New Point(205, 600)
        btnImprimir.Size = New Size(150, 36)
        btnImprimir.Text = "Imprimir"
        Controls.Add(btnImprimir)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Location = New Point(930, 600)
        btnCerrar.Size = New Size(150, 36)
        btnCerrar.Text = "Cerrar"
        Controls.Add(btnCerrar)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1100, 660)
        MinimumSize = New Size(1116, 699)
        Name = "frmListadoVencimientos"
        Text = "GymControl · Vencimientos"
        StartPosition = FormStartPosition.CenterParent
        CType(dgvVencimientos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents dgvVencimientos As DataGridView
    Friend WithEvents lblEstado As Label
    Friend WithEvents btnVistaPrevia As Button
    Friend WithEvents btnImprimir As Button
    Friend WithEvents btnCerrar As Button
End Class
