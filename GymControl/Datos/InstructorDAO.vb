Imports MySqlConnector
Imports System.IO
Imports System.Text.Json

Public Class InstructorDAO

    ' Método para obtener la cadena de conexión desde el archivo JSON
    Private Shared Function ObtenerCadenaConexion() As String
        Try
            ' Asegúrate de que el archivo se llame conexion.json (sin .ejemplo)
            Dim ruta As String = Path.Combine(Application.StartupPath, "conexion.json")
            If Not File.Exists(ruta) Then
                ruta = Path.Combine(Application.StartupPath, "conexion.ejemplo.json")
            End If

            Dim json As String = File.ReadAllText(ruta)
            Dim config = JsonSerializer.Deserialize(Of Dictionary(Of String, Object))(json)

            Dim servidor As String = config("Servidor").ToString()
            Dim puerto As String = config("Puerto").ToString()
            Dim baseDatos As String = config("BaseDatos").ToString()
            Dim usuario As String = config("Usuario").ToString()
            Dim clave As String = config("Clave").ToString()

            Return $"Server={servidor};Port={puerto};Database={baseDatos};Uid={usuario};Pwd={clave};"
        Catch ex As Exception
            Throw New Exception("Error al leer la configuración de conexión: " & ex.Message)
        End Try
    End Function

    ' 1. ALTA: Insertar un nuevo instructor
    Public Shared Function Insertar(instructor As Instructor) As Boolean
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "INSERT INTO instructores (Nombre, Apellido, Especialidad, Telefono, Email, Activo) " &
                                      "VALUES (@Nombre, @Apellido, @Especialidad, @Telefono, @Email, @Activo)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", instructor.Nombre)
                    cmd.Parameters.AddWithValue("@Apellido", instructor.Apellido)
                    cmd.Parameters.AddWithValue("@Especialidad", instructor.Especialidad)
                    cmd.Parameters.AddWithValue("@Telefono", instructor.Telefono)
                    cmd.Parameters.AddWithValue("@Email", instructor.Email)
                    cmd.Parameters.AddWithValue("@Activo", instructor.Activo)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al insertar instructor: " & ex.Message)
            End Try
        End Using
    End Function

    ' 2. CONSULTA: Obtener todos los instructores
    Public Shared Function ObtenerTodos() As List(Of Instructor)
        Dim lista As New List(Of Instructor)
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM instructores ORDER BY Apellido, Nombre"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            lista.Add(New Instructor With {
                                .IdInstructor = Convert.ToInt32(reader("IdInstructor")),
                                .Nombre = reader("Nombre").ToString(),
                                .Apellido = reader("Apellido").ToString(),
                                .Especialidad = reader("Especialidad").ToString(),
                                .Telefono = reader("Telefono").ToString(),
                                .Email = reader("Email").ToString(),
                                .Activo = Convert.ToBoolean(reader("Activo"))
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception("Error al consultar instructores: " & ex.Message)
            End Try
        End Using
        Return lista
    End Function

    ' 3. ACTUALIZACIÓN: Modificar un instructor existente
    Public Shared Function Actualizar(instructor As Instructor) As Boolean
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "UPDATE instructores SET Nombre=@Nombre, Apellido=@Apellido, Especialidad=@Especialidad, " &
                                      "Telefono=@Telefono, Email=@Email WHERE IdInstructor=@IdInstructor"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdInstructor", instructor.IdInstructor)
                    cmd.Parameters.AddWithValue("@Nombre", instructor.Nombre)
                    cmd.Parameters.AddWithValue("@Apellido", instructor.Apellido)
                    cmd.Parameters.AddWithValue("@Especialidad", instructor.Especialidad)
                    cmd.Parameters.AddWithValue("@Telefono", instructor.Telefono)
                    cmd.Parameters.AddWithValue("@Email", instructor.Email)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al actualizar instructor: " & ex.Message)
            End Try
        End Using
    End Function

    ' 4. INACTIVACIÓN: Cambiar Activo a False (no borrar)
    Public Shared Function Inactivar(idInstructor As Integer) As Boolean
        Using conn As New MySqlConnection(ObtenerCadenaConexion())
            Try
                conn.Open()
                Dim query As String = "UPDATE instructores SET Activo = 0 WHERE IdInstructor = @IdInstructor"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdInstructor", idInstructor)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al inactivar instructor: " & ex.Message)
            End Try
        End Using
    End Function

End Class