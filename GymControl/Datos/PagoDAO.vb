Imports MySqlConnector

Public Class PagoDAO

    Public Function Insertar(pago As Pago) As Boolean
        If pago Is Nothing Then
            Throw New ArgumentNullException(NameOf(pago))
        End If

        If pago.IdMembresia <= 0 Then
            Throw New ArgumentException("Seleccioná una membresía válida.")
        End If

        If pago.Monto <= 0D Then
            Throw New ArgumentException("El monto del pago debe ser mayor que cero.")
        End If

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
    Public Function ConsultarPorMembresia(idMembresia As Integer) As DataTable

        If idMembresia <= 0 Then
            Throw New ArgumentException("La membresía no es válida.")
        End If

        Dim tabla As New DataTable()

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
                "SELECT id_membresia, fecha_pago, monto " &
                "FROM pagos " &
                "WHERE id_membresia = @id " &
                "ORDER BY fecha_pago DESC"

            Using cmd As New MySqlCommand(sql, conexion)
                cmd.Parameters.AddWithValue("@id", idMembresia)

                Using lector = cmd.ExecuteReader()
                    tabla.Load(lector)
                End Using
            End Using
        End Using

        Return tabla

    End Function

    Public Function VerificarColumnasAnulacion() As String

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
            "SELECT COLUMN_NAME " &
            "FROM INFORMATION_SCHEMA.COLUMNS " &
            "WHERE TABLE_SCHEMA = DATABASE() " &
            "AND TABLE_NAME = 'pagos'"

            Using cmd As New MySqlCommand(sql, conexion)
                Using lector = cmd.ExecuteReader()

                    Dim columnas As New List(Of String)

                    While lector.Read()
                        columnas.Add(lector.GetString(0))
                    End While

                    Return String.Join(", ", columnas)

                End Using
            End Using
        End Using

    End Function


End Class