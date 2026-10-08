Imports MySqlConnector

Public Class TipoMembresiaDAO

    Public Function Listar() As DataTable
        Dim tabla As New DataTable()

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
                "SELECT id_tipo_membresia, nombre, precio, duracion_dias, activo " &
                "FROM tipos_membresia"

            Using cmd As New MySqlCommand(sql, conexion)
                Using adaptador As New MySqlDataAdapter(cmd)
                    adaptador.Fill(tabla)
                End Using
            End Using
        End Using

        Return tabla
    End Function

    Public Sub Insertar(tipo As TipoMembresia)

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
                "INSERT INTO tipos_membresia " &
                "(nombre, precio, duracion_dias, activo) " &
                "VALUES (@nombre, @precio, @duracion, @activo)"

            Using cmd As New MySqlCommand(sql, conexion)
                cmd.Parameters.AddWithValue("@nombre", tipo.Nombre)
                cmd.Parameters.AddWithValue("@precio", tipo.Precio)
                cmd.Parameters.AddWithValue("@duracion", tipo.DuracionDias)
                cmd.Parameters.AddWithValue("@activo", tipo.Activo)

                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Sub Actualizar(tipo As TipoMembresia)

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
                "UPDATE tipos_membresia SET " &
                "nombre = @nombre, " &
                "precio = @precio, " &
                "duracion_dias = @duracion, " &
                "activo = @activo " &
                "WHERE id_tipo_membresia = @id"

            Using cmd As New MySqlCommand(sql, conexion)
                cmd.Parameters.AddWithValue("@nombre", tipo.Nombre)
                cmd.Parameters.AddWithValue("@precio", tipo.Precio)
                cmd.Parameters.AddWithValue("@duracion", tipo.DuracionDias)
                cmd.Parameters.AddWithValue("@activo", tipo.Activo)
                cmd.Parameters.AddWithValue("@id", tipo.IdTipoMembresia)

                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Sub Inactivar(id As Integer)

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()

            Dim sql As String =
                "UPDATE tipos_membresia " &
                "SET activo = 0 " &
                "WHERE id_tipo_membresia = @id"

            Using cmd As New MySqlCommand(sql, conexion)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub

End Class