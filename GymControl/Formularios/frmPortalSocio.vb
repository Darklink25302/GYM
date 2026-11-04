
Public Class frmPortalSocio

    Private Sub frmPortalSocio_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        Try
            Dim dao As New SocioDAO()
            cboSocio.DataSource = dao.Listar()
            cboSocio.DisplayMember = "Nombres"
            cboSocio.ValueMember = "IdSocio"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnConsultar_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnConsultar.Click

        Try
            If cboSocio.SelectedValue Is Nothing Then Exit Sub

            Dim idSocio As Integer =
                Convert.ToInt32(cboSocio.SelectedValue)

            Dim dao As New MembresiaDAO()
            Dim tabla As DataTable = dao.Listar()

            If Not tabla.Columns.Contains("id_socio") Then
                MessageBox.Show(
                    "La consulta de membresías debe incluir id_socio.")
                Exit Sub
            End If

            Dim vista As New DataView(tabla)
            vista.RowFilter = "id_socio = " & idSocio

            dgvMembresias.DataSource = vista
            dgvPagos.DataSource = Nothing

            Dim saldoTotal As Decimal = 0D

            For Each fila As DataRowView In vista
                If Not IsDBNull(fila("saldo")) Then
                    saldoTotal += Convert.ToDecimal(fila("saldo"))
                End If
            Next

            lblSaldo.Text =
                "Saldo pendiente: C$ " & saldoTotal.ToString("N2")

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub dgvMembresias_SelectionChanged(
        sender As Object,
        e As EventArgs
    ) Handles dgvMembresias.SelectionChanged

        Try
            If dgvMembresias.CurrentRow Is Nothing Then Exit Sub

            Dim id As Integer =
                Convert.ToInt32(
                    dgvMembresias.CurrentRow.Cells("id_membresia").Value)

            Dim dao As New PagoDAO()
            dgvPagos.DataSource = dao.ConsultarPorMembresia(id)

        Catch
            dgvPagos.DataSource = Nothing
        End Try

    End Sub

    Private Sub btnPortalSocio_Click(
    sender As Object,
    e As EventArgs
) Handles btnPortalSocio.Click

        Using formulario As New frmPortalSocio()
            formulario.ShowDialog(Me)
        End Using

    End Sub


End Class
