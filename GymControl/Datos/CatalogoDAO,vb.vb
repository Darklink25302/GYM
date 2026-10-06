Imports MySqlConnector
Imports System.IO
Imports System.Text.Json

Public Class CatalogoDAO

    ' Método para obtener la cadena de conexión
    Private Shared Function ObtenerCadenaConexion() As String
        Try
            Dim ruta As String = Path.Combine(Application.StartupPath, "conexion.json")
            If Not File.Exists(ruta) Then
                ruta = Path.Combine(Application.StartupPath, "conexion.ejemplo.json")
            End If
            Dim json As String = File.ReadAllText(ruta)
            Dim config = JsonSerializer.Deserialize(Of Dictionary(Of String, Object))(json)
            Return $"Server={config("Servidor")};Port={config("Puerto")};Database={config("BaseDatos")};Uid={config("Usuario")};Pwd={config("Clave")};"
        Catch ex As Exception
            Throw New Exception("Error al leer la configuración de conexión: " & ex.Message)
        End Try
    End Function

    ' ================================================
    ' ACTIVIDADES
    ' ================================================

    ' Insertar una nueva actividad
    Public Shared Function InsertarActividad(nombre As String, descripcion As String) As Boolean
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "INSERT INTO actividades (Nombre, Descripcion, Activo) VALUES (@Nombre, @Descripcion, 1)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", nombre)
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al insertar actividad: " & ex.Message)
            End Try
        End Using
    End Function

    ' Obtener todas las actividades (solo activas)
    Public Shared Function ObtenerActividades() As List(Of Actividad)
        Dim lista As New List(Of Actividad)
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM actividades WHERE Activo = 1 ORDER BY Nombre"
                Using cmd As New MySqlCommand(query, conn)
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
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
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
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "INSERT INTO salas (Nombre, Capacidad, Activo) VALUES (@Nombre, @Capacidad, 1)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", nombre)
                    cmd.Parameters.AddWithValue("@Capacidad", capacidad)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al insertar sala: " & ex.Message)
            End Try
        End Using
    End Function

    ' Obtener todas las salas (solo activas)
    Public Shared Function ObtenerSalas() As List(Of Sala)
        Dim lista As New List(Of Sala)
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM salas WHERE Activo = 1 ORDER BY Nombre"
                Using cmd As New MySqlCommand(query, conn)
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
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
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