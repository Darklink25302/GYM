Imports MySqlConnector

Public Class InstructorDAO

    Private Shared Sub Validar(instructor As Instructor)
        If instructor Is Nothing Then Throw New ArgumentException("Faltan los datos del instructor.")
        instructor.Nombre = If(instructor.Nombre, "").Trim()
        instructor.Apellido = If(instructor.Apellido, "").Trim()
        instructor.Especialidad = If(instructor.Especialidad, "").Trim()
        instructor.Telefono = If(instructor.Telefono, "").Trim()
        instructor.Email = If(instructor.Email, "").Trim()
        If instructor.Nombre = "" OrElse instructor.Apellido = "" Then
            Throw New ArgumentException("Nombre y apellido son obligatorios.")
        End If
        If instructor.Nombre.Length > 100 OrElse instructor.Apellido.Length > 100 OrElse
           instructor.Especialidad.Length > 100 OrElse instructor.Telefono.Length > 30 OrElse
           instructor.Email.Length > 150 Then
            Throw New ArgumentException("Uno de los campos supera la longitud permitida.")
        End If
        If instructor.Email <> "" Then
            Dim direccion As System.Net.Mail.MailAddress = Nothing
            If Not System.Net.Mail.MailAddress.TryCreate(instructor.Email, direccion) OrElse
               direccion.Address <> instructor.Email Then
                Throw New ArgumentException("El correo no tiene un formato válido.")
            End If
        End If
    End Sub

    ' 1. ALTA: Insertar un nuevo instructor
    Public Shared Function Insertar(instructor As Instructor) As Boolean
        ExigirAdministrador()
        Validar(instructor)
        Using conn = ConexionBD.CrearConexion()
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
        ExigirAdministrador()
        Dim lista As New List(Of Instructor)
        Using conn = ConexionBD.CrearConexion()
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
        ExigirAdministrador()
        Validar(instructor)
        If instructor.IdInstructor <= 0 Then Throw New ArgumentException("Selecciona un instructor.")
        Using conn = ConexionBD.CrearConexion()
            Try
                conn.Open()
                Dim query As String = "UPDATE instructores SET Nombre=@Nombre, Apellido=@Apellido, Especialidad=@Especialidad, " &
                                      "Telefono=@Telefono, Email=@Email, Activo=@Activo WHERE IdInstructor=@IdInstructor"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@IdInstructor", instructor.IdInstructor)
                    cmd.Parameters.AddWithValue("@Nombre", instructor.Nombre)
                    cmd.Parameters.AddWithValue("@Apellido", instructor.Apellido)
                    cmd.Parameters.AddWithValue("@Especialidad", instructor.Especialidad)
                    cmd.Parameters.AddWithValue("@Telefono", instructor.Telefono)
                    cmd.Parameters.AddWithValue("@Email", instructor.Email)
                    cmd.Parameters.AddWithValue("@Activo", instructor.Activo)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            Catch ex As Exception
                Throw New Exception("Error al actualizar instructor: " & ex.Message)
            End Try
        End Using
    End Function

    ' 4. INACTIVACIÓN: Cambiar Activo a False (no borrar)
    Public Shared Function Inactivar(idInstructor As Integer) As Boolean
        ExigirAdministrador()
        If idInstructor <= 0 Then Throw New ArgumentException("Selecciona un instructor.")
        Using conn = ConexionBD.CrearConexion()
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
