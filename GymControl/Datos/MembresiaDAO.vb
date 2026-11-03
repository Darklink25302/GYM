Imports MySqlConnector

Public Class MembresiaDAO

    Public Function Insertar(membresia As Membresia) As Integer

        Try

            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "INSERT INTO membresias " &
                    "(id_socio, id_tipo_membresia, fecha_inicio, fecha_vencimiento, total, estado) " &
                    "VALUES " &
                    "(@id_socio, @id_tipo_membresia, @fecha_inicio, @fecha_vencimiento, @total, @estado); " &
                    "SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conexion)

                    cmd.Parameters.AddWithValue(
                        "@id_socio",
                        membresia.IdSocio
                    )

                    cmd.Parameters.AddWithValue(
                        "@id_tipo_membresia",
                        membresia.IdTipoMembresia
                    )

                    cmd.Parameters.AddWithValue(
                        "@fecha_inicio",
                        membresia.FechaInicio
                    )

                    cmd.Parameters.AddWithValue(
                        "@fecha_vencimiento",
                        membresia.FechaVencimiento
                    )

                    cmd.Parameters.AddWithValue(
                        "@total",
                        membresia.Total
                    )

                    cmd.Parameters.AddWithValue(
                        "@estado",
                        membresia.Estado
                    )

                    Return Convert.ToInt32(
                        cmd.ExecuteScalar()
                    )

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al registrar la membresía: " &
                ex.Message,
                ex
            )

        End Try

    End Function
    Public Function Listar() As DataTable

        Dim tabla As New DataTable()

        Try

            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "SELECT " &
                    "m.id_membresia, " &
                    "CONCAT(s.nombres, ' ', s.apellidos) AS socio, " &
                    "tm.nombre AS tipo_membresia, " &
                    "m.fecha_inicio, " &
                    "m.fecha_vencimiento, " &
                    "m.total, " &
                    "COALESCE(SUM(p.monto), 0) AS abonado, " &
                    "(m.total - COALESCE(SUM(p.monto), 0)) AS saldo, " &
                    "m.estado " &
                    "FROM membresias m " &
                    "INNER JOIN socios s ON s.id_socio = m.id_socio " &
                    "INNER JOIN tipos_membresia tm " &
                    "ON tm.id_tipo_membresia = m.id_tipo_membresia " &
                    "LEFT JOIN pagos p ON p.id_membresia = m.id_membresia " &
                    "GROUP BY " &
                    "m.id_membresia, " &
                    "s.nombres, s.apellidos, " &
                    "tm.nombre, " &
                    "m.fecha_inicio, " &
                    "m.fecha_vencimiento, " &
                    "m.total, " &
                    "m.estado " &
                    "ORDER BY m.id_membresia DESC"

                Using cmd As New MySqlConnector.MySqlCommand(sql, conexion)

                    Using adaptador As New MySqlConnector.MySqlDataAdapter(cmd)

                        adaptador.Fill(tabla)

                    End Using

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al cargar las membresías: " & ex.Message,
                ex
            )

        End Try

        Return tabla

    End Function


End Class
