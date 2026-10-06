Imports MySqlConnector

Public Class HorarioDAO

    Private Shared Sub Validar(horario As Horario)
        If horario Is Nothing Then Throw New ArgumentException("Faltan los datos del horario.")
        If horario.IdInstructor <= 0 OrElse horario.IdActividad <= 0 OrElse horario.IdSala <= 0 Then
            Throw New ArgumentException("Selecciona instructor, actividad y sala.")
        End If
        Dim dias As String() = {"Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"}
        If Array.IndexOf(dias, horario.DiaSemana) < 0 Then
            Throw New ArgumentException("Selecciona un día de la semana.")
        End If
        If horario.HoraInicio < TimeSpan.Zero OrElse horario.HoraFin >= TimeSpan.FromDays(1) OrElse
           horario.HoraFin <= horario.HoraInicio Then
            Throw New ArgumentException("La hora final debe ser mayor que la inicial, dentro del mismo día.")
        End If
    End Sub

    Public Shared Function ObtenerTodos() As List(Of Horario)
        ExigirConsultaHorarios()
        Dim lista As New List(Of Horario)
        Dim sql = "SELECT h.*, CONCAT(i.Nombre, ' ', i.Apellido) AS Instructor, " &
                  "a.Nombre AS Actividad, s.Nombre AS Sala FROM horarios h " &
                  "INNER JOIN instructores i ON i.IdInstructor=h.IdInstructor " &
                  "INNER JOIN actividades a ON a.IdActividad=h.IdActividad " &
                  "INNER JOIN salas s ON s.IdSala=h.IdSala "
        ' El instructor no puede elegir el ID de otra persona.
        If Sesion.Rol = "Instructor" Then
            sql &= "WHERE h.IdInstructor=@IdInstructor AND h.Activo=1 "
        End If
        sql &= "ORDER BY FIELD(h.DiaSemana,'Lunes','Martes','Miércoles','Jueves','Viernes','Sábado','Domingo'), h.HoraInicio"
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand(sql, conexion)
                If Sesion.Rol = "Instructor" Then comando.Parameters.AddWithValue("@IdInstructor", Sesion.IdInstructor.Value)
                Using lector = comando.ExecuteReader()
                    While lector.Read()
                        lista.Add(New Horario With {
                            .IdHorario = lector.GetInt32("IdHorario"),
                            .IdInstructor = lector.GetInt32("IdInstructor"),
                            .IdActividad = lector.GetInt32("IdActividad"),
                            .IdSala = lector.GetInt32("IdSala"),
                            .DiaSemana = lector.GetString("DiaSemana"),
                            .HoraInicio = lector.GetTimeSpan("HoraInicio"),
                            .HoraFin = lector.GetTimeSpan("HoraFin"),
                            .Activo = lector.GetBoolean("Activo"),
                            .NombreInstructor = lector.GetString("Instructor"),
                            .NombreActividad = lector.GetString("Actividad"),
                            .NombreSala = lector.GetString("Sala")
                        })
                    End While
                End Using
            End Using
        End Using
        Return lista
    End Function

    Public Shared Function Guardar(horario As Horario) As Boolean
        ExigirAdministrador()
        Validar(horario)
        If horario.IdHorario < 0 Then Throw New ArgumentException("El identificador del horario no es válido.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                Try
                    If horario.IdHorario > 0 Then
                        Using comando As New MySqlCommand("SELECT Activo FROM horarios WHERE IdHorario=@Id FOR UPDATE", conexion, transaccion)
                            comando.Parameters.AddWithValue("@Id", horario.IdHorario)
                            Dim estado = comando.ExecuteScalar()
                            If estado Is Nothing OrElse Not Convert.ToBoolean(estado) Then
                                Throw New ArgumentException("El horario no existe o está inactivo.")
                            End If
                        End Using
                    End If

                    ' Bloqueamos los recursos siempre en el mismo orden.
                    ' Otra estación espera antes de comprobar y guardar sus horarios.
                    Using comando As New MySqlCommand("SELECT Activo FROM instructores WHERE IdInstructor=@Id FOR UPDATE", conexion, transaccion)
                        comando.Parameters.AddWithValue("@Id", horario.IdInstructor)
                        Dim estado = comando.ExecuteScalar()
                        If estado Is Nothing OrElse Not Convert.ToBoolean(estado) Then
                            Throw New ArgumentException("El instructor no existe o está inactivo.")
                        End If
                    End Using
                    Using comando As New MySqlCommand("SELECT Activo FROM salas WHERE IdSala=@Id FOR UPDATE", conexion, transaccion)
                        comando.Parameters.AddWithValue("@Id", horario.IdSala)
                        Dim estado = comando.ExecuteScalar()
                        If estado Is Nothing OrElse Not Convert.ToBoolean(estado) Then
                            Throw New ArgumentException("La sala no existe o está inactiva.")
                        End If
                    End Using
                    Using comando As New MySqlCommand("SELECT Activo FROM actividades WHERE IdActividad=@Id FOR UPDATE", conexion, transaccion)
                        comando.Parameters.AddWithValue("@Id", horario.IdActividad)
                        Dim estado = comando.ExecuteScalar()
                        If estado Is Nothing OrElse Not Convert.ToBoolean(estado) Then
                            Throw New ArgumentException("La actividad no existe o está inactiva.")
                        End If
                    End Using

                    Dim choques = "SELECT COUNT(*) FROM horarios WHERE Activo=1 AND DiaSemana=@Dia " &
                                 "AND IdHorario<>@IdHorario AND (IdInstructor=@Instructor OR IdSala=@Sala) " &
                                 "AND @Inicio<HoraFin AND @Fin>HoraInicio"
                    Using comando As New MySqlCommand(choques, conexion, transaccion)
                        comando.Parameters.AddWithValue("@Dia", horario.DiaSemana)
                        comando.Parameters.AddWithValue("@IdHorario", horario.IdHorario)
                        comando.Parameters.AddWithValue("@Instructor", horario.IdInstructor)
                        comando.Parameters.AddWithValue("@Sala", horario.IdSala)
                        comando.Parameters.AddWithValue("@Inicio", horario.HoraInicio)
                        comando.Parameters.AddWithValue("@Fin", horario.HoraFin)
                        If Convert.ToInt32(comando.ExecuteScalar()) > 0 Then
                            Throw New ArgumentException("El horario choca con otro del mismo instructor o de la misma sala.")
                        End If
                    End Using

                    Dim sql As String
                    If horario.IdHorario = 0 Then
                        sql = "INSERT INTO horarios (IdInstructor, IdActividad, IdSala, DiaSemana, HoraInicio, HoraFin, Activo) " &
                              "VALUES (@Instructor, @Actividad, @Sala, @Dia, @Inicio, @Fin, 1)"
                    Else
                        sql = "UPDATE horarios SET IdInstructor=@Instructor, IdActividad=@Actividad, IdSala=@Sala, " &
                              "DiaSemana=@Dia, HoraInicio=@Inicio, HoraFin=@Fin WHERE IdHorario=@IdHorario"
                    End If
                    Using comando As New MySqlCommand(sql, conexion, transaccion)
                        comando.Parameters.AddWithValue("@Instructor", horario.IdInstructor)
                        comando.Parameters.AddWithValue("@Actividad", horario.IdActividad)
                        comando.Parameters.AddWithValue("@Sala", horario.IdSala)
                        comando.Parameters.AddWithValue("@Dia", horario.DiaSemana)
                        comando.Parameters.AddWithValue("@Inicio", horario.HoraInicio)
                        comando.Parameters.AddWithValue("@Fin", horario.HoraFin)
                        If horario.IdHorario > 0 Then comando.Parameters.AddWithValue("@IdHorario", horario.IdHorario)
                        comando.ExecuteNonQuery()
                        If horario.IdHorario = 0 Then horario.IdHorario = CInt(comando.LastInsertedId)
                    End Using
                    transaccion.Commit()
                    Return True
                Catch
                    transaccion.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Public Shared Function Inactivar(idHorario As Integer) As Boolean
        ExigirAdministrador()
        If idHorario <= 0 Then Throw New ArgumentException("Selecciona un horario.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand("UPDATE horarios SET Activo=0 WHERE IdHorario=@Id", conexion)
                comando.Parameters.AddWithValue("@Id", idHorario)
                Return comando.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

End Class
