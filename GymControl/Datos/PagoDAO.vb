Imports MySqlConnector

Public Class PagoDAO

    Public Function Insertar(pago As Pago) As Boolean

        Try

            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "INSERT INTO pagos " &
                    "(id_membresia, fecha_pago, monto) " &
                    "VALUES " &
                    "(@id_membresia, @fecha_pago, @monto)"

                Using cmd As New MySqlCommand(sql, conexion)

                    cmd.Parameters.AddWithValue(
                        "@id_membresia",
                        pago.IdMembresia
                    )

                    cmd.Parameters.AddWithValue(
                        "@fecha_pago",
                        pago.FechaPago
                    )

                    cmd.Parameters.AddWithValue(
                        "@monto",
                        pago.Monto
                    )

                    Return cmd.ExecuteNonQuery() > 0

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al registrar el pago: " & ex.Message,
                ex
            )

        End Try

    End Function

End Class