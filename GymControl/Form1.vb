Public Class Form1

    Private Async Sub Form1_Shown(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Shown

        Try
            Using conexion = ConexionBD.CrearConexion()
                Await conexion.OpenAsync()
                MessageBox.Show("Conexión a MariaDB correcta.")
            End Using
        Catch ex As Exception
            MessageBox.Show("Error de conexión: " & ex.Message)
        End Try

    End Sub

End Class