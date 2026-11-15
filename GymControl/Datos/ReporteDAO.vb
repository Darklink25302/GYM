Option Strict On

Imports MySqlConnector

Public Class ReporteDAO
    Private Shared Function Consultar(sql As String, ParamArray parametros As MySqlParameter()) As DataTable
        Dim tabla As New DataTable()
        Using conexion = ConexionBD.CrearConexionReportes()
            conexion.Open()
            Using comando As New MySqlCommand(sql, conexion)
                comando.Parameters.AddRange(parametros)
                Using adaptador As New MySqlDataAdapter(comando)
                    adaptador.MissingSchemaAction=MissingSchemaAction.Add
                    adaptador.Fill(tabla)
                End Using
            End Using
        End Using
        Return tabla
    End Function

    Public Shared Function Socios() As DataTable
        PermisosReportes.Exigir("R05")
        Dim id = If(Sesion.Rol = "Socio", Sesion.IdSocio.GetValueOrDefault(), 0)
        Return Consultar("SELECT DISTINCT id_socio, socio FROM vw_estado_cuenta " &
                         "WHERE (@id=0 OR id_socio=@id) ORDER BY socio", New MySqlParameter("@id", id))
    End Function

    Public Shared Function SaldosSocio(idSocio As Integer) As DataTable
        idSocio = PermisosReportes.ObtenerSocioPermitido(idSocio)
        Return Consultar("SELECT * FROM vw_saldos_membresia WHERE id_socio=@id ORDER BY inicio DESC, id_membresia DESC",
                         New MySqlParameter("@id", idSocio))
    End Function

    Public Shared Function EstadoCuenta(idSocio As Integer, desde As Date) As DataTable
        idSocio = PermisosReportes.ObtenerSocioPermitido(idSocio)
        Return Consultar("SELECT *, CASE WHEN fecha_pago>=@desde THEN 1 ELSE 0 END AS mostrar_pago " &
                         "FROM vw_estado_cuenta WHERE id_socio=@id AND id_membresia IS NOT NULL " &
                         "ORDER BY inicio, id_membresia, fecha_pago, id_pago",
                         New MySqlParameter("@id", idSocio), New MySqlParameter("@desde", desde.Date))
    End Function

    Public Shared Function Ingresos(desde As Date, hasta As Date, metodo As String, codigo As String) As DataTable
        If codigo <> "R04" AndAlso codigo <> "R08" AndAlso codigo <> "R09" Then Throw New ArgumentException("Reporte de ingresos inválido.")
        PermisosReportes.Exigir(codigo)
        If desde.Date > hasta.Date Then Throw New ArgumentException("La fecha inicial no puede superar la final.")
        If metodo <> "Todos" AndAlso metodo <> "Efectivo" AndAlso metodo <> "Tarjeta" AndAlso metodo <> "Transferencia" Then
            Throw New ArgumentException("Selecciona un método de pago válido.")
        End If
        Return Consultar("SELECT *, DATE(fecha) AS dia FROM vw_ingresos_detalle " &
                         "WHERE fecha>=@desde AND fecha<@hasta AND (@metodo='Todos' OR metodo_pago=@metodo) " &
                         "ORDER BY fecha, metodo_pago, id_pago",
                         New MySqlParameter("@desde", desde.Date), New MySqlParameter("@hasta", hasta.Date.AddDays(1)),
                         New MySqlParameter("@metodo", metodo))
    End Function

    Public Shared Function Vencimientos(adelante As Integer, atras As Integer, soloSaldo As Boolean) As DataTable
        PermisosReportes.Exigir("R03")
        If adelante < 0 OrElse atras < 0 OrElse adelante > 3650 OrElse atras > 3650 Then Throw New ArgumentException("Revisa los días de vencimiento.")
        Return Consultar("SELECT *, DATEDIFF(vence,CURRENT_DATE()) AS dias FROM vw_saldos_membresia " &
                         "WHERE estado<>'Cancelada' AND vence BETWEEN @desde AND @hasta " &
                         "AND (@saldo=FALSE OR saldo>0) ORDER BY vence, socio, id_membresia",
                         New MySqlParameter("@desde", Date.Today.AddDays(-atras)), New MySqlParameter("@hasta", Date.Today.AddDays(adelante)),
                         New MySqlParameter("@saldo", soloSaldo))
    End Function

    Public Shared Function Cartera(incluirAlDia As Boolean) As DataTable
        PermisosReportes.Exigir("R07")
        Return Consultar("SELECT * FROM vw_saldos_membresia WHERE @todos=TRUE OR saldo>0 ORDER BY vence,socio,id_membresia",
                         New MySqlParameter("@todos", incluirAlDia))
    End Function

    Public Shared Function Horario(idInstructor As Integer, idSala As Integer) As DataTable
        idInstructor = PermisosReportes.ObtenerInstructorPermitido(idInstructor)
        If idSala < 0 Then Throw New ArgumentException("Sala inválida.")
        Return Consultar("SELECT id_horario,id_instructor,id_sala,dia_semana,nombre_dia," &
                         "TIME_FORMAT(hora_inicio,'%H:%i') AS hora_inicio,TIME_FORMAT(hora_fin,'%H:%i') AS hora_fin," &
                         "minutos,actividad,instructor,sala FROM vw_horario_semanal " &
                         "WHERE dia_semana<=6 AND (@instructor=0 OR id_instructor=@instructor) " &
                         "AND (@sala=0 OR id_sala=@sala) ORDER BY hora_inicio,dia_semana,id_horario",
                         New MySqlParameter("@instructor", idInstructor), New MySqlParameter("@sala", idSala))
    End Function

    Public Shared Function Instructores() As DataTable
        Dim id = PermisosReportes.ObtenerInstructorPermitido(0)
        Return Consultar("SELECT DISTINCT id_instructor,instructor FROM vw_horario_semanal " &
                         "WHERE @id=0 OR id_instructor=@id ORDER BY instructor", New MySqlParameter("@id", id))
    End Function

    Public Shared Function Salas() As DataTable
        Dim id = PermisosReportes.ObtenerInstructorPermitido(0)
        Return Consultar("SELECT DISTINCT id_sala,sala FROM vw_horario_semanal " &
                         "WHERE @id=0 OR id_instructor=@id ORDER BY sala", New MySqlParameter("@id", id))
    End Function

    Public Shared Function Indicadores(mes As Integer, anio As Integer) As DataTable
        PermisosReportes.Exigir("R08")
        Dim desde As New Date(anio, mes, 1)
        Return Consultar("SELECT (SELECT COUNT(DISTINCT id_socio) FROM vw_estado_cuenta WHERE socio_activo=TRUE) AS activos," &
                         "(SELECT COUNT(*) FROM vw_saldos_membresia WHERE inicio>=@desde AND inicio<@hasta) AS nuevas," &
                         "(SELECT COALESCE(SUM(monto),0) FROM vw_ingresos_detalle WHERE anulado=FALSE AND fecha>=@desde AND fecha<@hasta) AS ingresos," &
                         "(SELECT COALESCE(SUM(GREATEST(saldo,0)),0) FROM vw_saldos_membresia) AS saldo",
                         New MySqlParameter("@desde", desde), New MySqlParameter("@hasta", desde.AddMonths(1)))
    End Function

    Public Shared Function IngresosPorPlan(mes As Integer, anio As Integer) As DataTable
        PermisosReportes.Exigir("R08")
        Dim desde As New Date(anio, mes, 1)
        Return Consultar("SELECT plan,COUNT(*) AS cantidad,SUM(monto) AS total FROM vw_ingresos_detalle " &
                         "WHERE anulado=FALSE AND fecha>=@desde AND fecha<@hasta GROUP BY plan ORDER BY total DESC",
                         New MySqlParameter("@desde", desde), New MySqlParameter("@hasta", desde.AddMonths(1)))
    End Function

    Public Shared Function SaldosMayores() As DataTable
        PermisosReportes.Exigir("R08")
        Return Consultar("SELECT socio,SUM(saldo) AS saldo FROM vw_saldos_membresia GROUP BY id_socio,socio HAVING SUM(saldo)>0 ORDER BY saldo DESC LIMIT 10")
    End Function

    Public Shared Function HorasPorInstructor() As DataTable
        PermisosReportes.Exigir("R08")
        Return Consultar("SELECT instructor,SUM(minutos)/60.0 AS horas FROM vw_horario_semanal WHERE dia_semana<=6 GROUP BY id_instructor,instructor ORDER BY instructor")
    End Function
End Class
