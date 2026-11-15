Option Strict On

Imports System.Drawing.Printing

Public Class frmListadoVencimientos
    Private adelante As Integer
    Private atras As Integer
    Private soloSaldo As Boolean
    Private horizontal As Boolean
    Private impresor As ImpresorVencimientos
    Private errorImpresion As String=""

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(diasAdelante As Integer,diasAtras As Integer,conSaldo As Boolean,hojaHorizontal As Boolean)
        Me.New()
        adelante=diasAdelante : atras=diasAtras : soloSaldo=conSaldo : horizontal=hojaHorizontal
    End Sub

    Private Sub frmListadoVencimientos_Load(sender As Object,e As EventArgs) Handles MyBase.Load
        Try
            Dim tabla=ReporteDAO.Vencimientos(adelante,atras,soloSaldo)
            dgvVencimientos.DataSource=tabla.DefaultView.ToTable(False,"socio","telefono","plan","vence","dias","saldo")
            dgvVencimientos.Columns("saldo").DefaultCellStyle.Format="N2"
            dgvVencimientos.Columns("vence").DefaultCellStyle.Format="dd/MM/yyyy"
            lblEstado.Text=tabla.Rows.Count.ToString() & " membresías · Próximos " & adelante.ToString() & " días / vencidas hasta " & atras.ToString() & " días."
            impresor=New ImpresorVencimientos(tabla,horizontal)
            AddHandler impresor.Documento.BeginPrint,AddressOf Validar
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Vencimientos")
            Close()
        End Try
    End Sub

    Private Sub Validar(sender As Object,e As PrintEventArgs)
        errorImpresion=""
        Try
            impresor.Datos=ReporteDAO.Vencimientos(adelante,atras,soloSaldo)
        Catch ex As Exception
            e.Cancel=True
            errorImpresion=ex.Message
        End Try
    End Sub

    Private Sub btnVistaPrevia_Click(sender As Object,e As EventArgs) Handles btnVistaPrevia.Click
        Try
            Using vista As New PrintPreviewDialog()
                vista.Document=impresor.Documento
                vista.UseAntiAlias=True
                vista.WindowState=FormWindowState.Maximized
                vista.ShowDialog(Me)
            End Using
            If errorImpresion<>"" Then MessageBox.Show(errorImpresion,"Impresión cancelada")
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Vista previa")
        End Try
    End Sub

    Private Sub btnImprimir_Click(sender As Object,e As EventArgs) Handles btnImprimir.Click
        Try
            Using dialogo As New PrintDialog()
                dialogo.Document=impresor.Documento
                dialogo.UseEXDialog=True
                dialogo.AllowSomePages=False
                If dialogo.ShowDialog(Me)<>DialogResult.OK Then Return
                impresor.Documento.Print()
            End Using
            If errorImpresion<>"" Then MessageBox.Show(errorImpresion,"Impresión cancelada")
        Catch ex As Exception
            MessageBox.Show(ex.Message,"Impresión")
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object,e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
