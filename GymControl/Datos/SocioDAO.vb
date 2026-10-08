Imports MySqlConnector

Public Class SocioDAO

    Public Function Listar() As List(Of Socio)

        Dim lista As New List(Of Socio)

        Try
            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "SELECT * FROM socios WHERE activo = 1"

                Using cmd As New MySqlCommand(sql, conexion)

                    Using lector = cmd.ExecuteReader()

                        While lector.Read()

                            Dim socio As New Socio()

                            socio.IdSocio = Convert.ToInt32(lector("id_socio"))
                            socio.Cedula = lector("cedula").ToString()
                            socio.Nombres = lector("nombres").ToString()
                            socio.Apellidos = lector("apellidos").ToString()
                            socio.Telefono = lector("telefono").ToString()
                            socio.Correo = lector("correo").ToString()
                            socio.Direccion = lector("direccion").ToString()

                            If Not IsDBNull(lector("fecha_nacimiento")) Then
                                socio.FechaNacimiento =
                                    Convert.ToDateTime(lector("fecha_nacimiento"))
                            End If

                            socio.Activo = Convert.ToBoolean(lector("activo"))

                            lista.Add(socio)

                        End While

                    End Using

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al cargar los socios: " & ex.Message,
                ex
            )

        End Try

        Return lista

    End Function

    Public Function Insertar(socio As Socio) As Boolean

        Try
            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "INSERT INTO socios " &
                    "(cedula, nombres, apellidos, telefono, correo, direccion, fecha_nacimiento, activo) " &
                    "VALUES " &
                    "(@cedula, @nombres, @apellidos, @telefono, @correo, @direccion, @fecha_nacimiento, 1)"

                Using cmd As New MySqlCommand(sql, conexion)

                    cmd.Parameters.AddWithValue("@cedula", socio.Cedula)
                    cmd.Parameters.AddWithValue("@nombres", socio.Nombres)
                    cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos)
                    cmd.Parameters.AddWithValue("@telefono", socio.Telefono)
                    cmd.Parameters.AddWithValue("@correo", socio.Correo)
                    cmd.Parameters.AddWithValue("@direccion", socio.Direccion)

                    If socio.FechaNacimiento.HasValue Then
                        cmd.Parameters.AddWithValue(
                            "@fecha_nacimiento",
                            socio.FechaNacimiento.Value
                        )
                    Else
                        cmd.Parameters.AddWithValue(
                            "@fecha_nacimiento",
                            DBNull.Value
                        )
                    End If

                    Return cmd.ExecuteNonQuery() > 0

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al registrar el socio: " & ex.Message,
                ex
            )

        End Try

    End Function

    Public Function Actualizar(socio As Socio) As Boolean

        Try
            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "UPDATE socios SET " &
                    "cedula = @cedula, " &
                    "nombres = @nombres, " &
                    "apellidos = @apellidos, " &
                    "telefono = @telefono, " &
                    "correo = @correo, " &
                    "direccion = @direccion, " &
                    "fecha_nacimiento = @fecha_nacimiento " &
                    "WHERE id_socio = @id_socio"

                Using cmd As New MySqlCommand(sql, conexion)

                    cmd.Parameters.AddWithValue("@cedula", socio.Cedula)
                    cmd.Parameters.AddWithValue("@nombres", socio.Nombres)
                    cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos)
                    cmd.Parameters.AddWithValue("@telefono", socio.Telefono)
                    cmd.Parameters.AddWithValue("@correo", socio.Correo)
                    cmd.Parameters.AddWithValue("@direccion", socio.Direccion)

                    If socio.FechaNacimiento.HasValue Then
                        cmd.Parameters.AddWithValue(
                            "@fecha_nacimiento",
                            socio.FechaNacimiento.Value
                        )
                    Else
                        cmd.Parameters.AddWithValue(
                            "@fecha_nacimiento",
                            DBNull.Value
                        )
                    End If

                    cmd.Parameters.AddWithValue("@id_socio", socio.IdSocio)

                    Return cmd.ExecuteNonQuery() > 0

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al actualizar el socio: " & ex.Message,
                ex
            )

        End Try

    End Function

    Public Function Inactivar(idSocio As Integer) As Boolean

        Try
            Using conexion = ConexionBD.CrearConexion()

                conexion.Open()

                Dim sql As String =
                    "UPDATE socios SET activo = 0 " &
                    "WHERE id_socio = @id_socio"

                Using cmd As New MySqlCommand(sql, conexion)

                    cmd.Parameters.AddWithValue("@id_socio", idSocio)

                    Return cmd.ExecuteNonQuery() > 0

                End Using

            End Using

        Catch ex As Exception

            Throw New Exception(
                "Error al inactivar el socio: " & ex.Message,
                ex
            )

        End Try

    End Function

End Class