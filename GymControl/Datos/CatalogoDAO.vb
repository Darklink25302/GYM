Imports MySqlConnector

Public Class CatalogoDAO

    Private Shared Sub ValidarActividad(nombre As String, descripcion As String)
        If String.IsNullOrWhiteSpace(nombre) OrElse nombre.Trim().Length > 100 Then
            Throw New ArgumentException("El nombre de la actividad es obligatorio y admite hasta 100 caracteres.")
        End If
        If descripcion IsNot Nothing AndAlso descripcion.Length > 255 Then
            Throw New ArgumentException("La descripción admite hasta 255 caracteres.")
        End If
    End Sub

    Private Shared Sub ValidarSala(nombre As String, capacidad As Integer)
        If String.IsNullOrWhiteSpace(nombre) OrElse nombre.Trim().Length > 100 Then
            Throw New ArgumentException("El nombre de la sala es obligatorio y admite hasta 100 caracteres.")
        End If
        If capacidad <= 0 Then Throw New ArgumentException("La capacidad debe ser un número entero mayor que cero.")
    End Sub

    Public Shared Function ActualizarActividad(idActividad As Integer, nombre As String, descripcion As String) As Boolean
        ExigirAdministrador()
        ValidarActividad(nombre, descripcion)
        If idActividad <= 0 Then Throw New ArgumentException("Selecciona una actividad.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand("UPDATE actividades SET Nombre=@Nombre, Descripcion=@Descripcion WHERE IdActividad=@Id", conexion)
                comando.Parameters.AddWithValue("@Id", idActividad)
                comando.Parameters.AddWithValue("@Nombre", nombre.Trim())
                comando.Parameters.AddWithValue("@Descripcion", If(descripcion, "").Trim())
                Return comando.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function ActualizarSala(idSala As Integer, nombre As String, capacidad As Integer) As Boolean
        ExigirAdministrador()
        ValidarSala(nombre, capacidad)
        If idSala <= 0 Then Throw New ArgumentException("Selecciona una sala.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand("UPDATE salas SET Nombre=@Nombre, Capacidad=@Capacidad WHERE IdSala=@Id", conexion)
                comando.Parameters.AddWithValue("@Id", idSala)
                comando.Parameters.AddWithValue("@Nombre", nombre.Trim())
                comando.Parameters.AddWithValue("@Capacidad", capacidad)
                Return comando.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ' ================================================
    ' ACTIVIDADES
    ' ================================================

    ' Insertar una nueva actividad
    Public Shared Function InsertarActividad(nombre As String, descripcion As String) As Boolean
        ExigirAdministrador()
        ValidarActividad(nombre, descripcion)
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "INSERT INTO actividades (Nombre, Descripcion, Activo) VALUES (@Nombre, @Descripcion, 1)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", nombre.Trim())
                    cmd.Parameters.AddWithValue("@Descripcion", If(descripcion, "").Trim())
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al insertar actividad: " & ex.Message)
            End Try
        End Using
    End Function

    ' Consultar actividades; soloActivas=True se usa en horarios
    Public Shared Function ObtenerActividades(Optional soloActivas As Boolean = False) As List(Of Actividad)
        ExigirAdministrador()
        Dim lista As New List(Of Actividad)
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM actividades WHERE (@SoloActivas = 0 OR Activo = 1) ORDER BY Nombre"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SoloActivas", soloActivas)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(New Actividad With {
                                .IdActividad = Convert.ToInt32(reader("IdActividad")),
                                .Nombre = reader("Nombre").ToString(),
                                .Descripcion = reader("Descripcion").ToString(),
                                .Activo = Convert.ToBoolean(reader("Activo"))
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Error al consultar actividades: " & ex.Message)
            End Try
        End Using
        Return lista
    End Function

    ' Inactivar una actividad
    Public Shared Function InactivarActividad(idActividad As Integer) As Boolean
        ExigirAdministrador()
        If idActividad <= 0 Then Throw New ArgumentException("Selecciona una actividad.")
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "UPDATE actividades SET Activo = 0 WHERE IdActividad = @IdActividad"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdActividad", idActividad)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al inactivar actividad: " & ex.Message)
            End Try
        End Using
    End Function

    ' ================================================
    ' SALAS
    ' ================================================

    ' Insertar una nueva sala
    Public Shared Function InsertarSala(nombre As String, capacidad As Integer) As Boolean
        ExigirAdministrador()
        ValidarSala(nombre, capacidad)
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "INSERT INTO salas (Nombre, Capacidad, Activo) VALUES (@Nombre, @Capacidad, 1)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", nombre.Trim())
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al insertar sala: " & ex.Message)
            End Try
        End Using
    End Function

    ' Consultar salas; soloActivas=True se usa en horarios
    Public Shared Function ObtenerSalas(Optional soloActivas As Boolean = False) As List(Of Sala)
        ExigirAdministrador()
        Dim lista As New List(Of Sala)
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM salas WHERE (@SoloActivas = 0 OR Activo = 1) ORDER BY Nombre"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SoloActivas", soloActivas)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(New Sala With {
                                .IdSala = Convert.ToInt32(reader("IdSala")),
                                .Nombre = reader("Nombre").ToString(),
                                .Capacidad = Convert.ToInt32(reader("Capacidad")),
                                .Activo = Convert.ToBoolean(reader("Activo"))
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Error al consultar salas: " & ex.Message)
            End Try
        End Using
        Return lista
    End Function

    ' Inactivar una sala
    Public Shared Function InactivarSala(idSala As Integer) As Boolean
        ExigirAdministrador()
        If idSala <= 0 Then Throw New ArgumentException("Selecciona una sala.")
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "UPDATE salas SET Activo = 0 WHERE IdSala = @IdSala"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdSala", idSala)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al inactivar sala: " & ex.Message)
            End Try
        End Using
    End Function

End Class
