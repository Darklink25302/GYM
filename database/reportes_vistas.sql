USE gimnasio_db;

CREATE OR REPLACE SQL SECURITY DEFINER VIEW vw_ingresos_detalle AS
SELECT p.id_pago, m.id_membresia, s.id_socio,
       p.fecha_pago AS fecha, CONCAT(s.nombres, ' ', s.apellidos) AS socio,
       s.cedula, s.telefono, t.nombre AS plan,
       COALESCE(p.metodo_pago, 'Sin registrar') AS metodo_pago,
       p.monto, p.anulado, COALESCE(u.nombre_usuario, 'Sin registrar') AS usuario
FROM pagos p
JOIN membresias m ON m.id_membresia=p.id_membresia
JOIN socios s ON s.id_socio=m.id_socio
JOIN tipos_membresia t ON t.id_tipo_membresia=m.id_tipo_membresia
LEFT JOIN usuarios u ON u.id_usuario=p.id_usuario;

CREATE OR REPLACE SQL SECURITY DEFINER VIEW vw_saldos_membresia AS
SELECT m.id_membresia, s.id_socio, CONCAT(s.nombres, ' ', s.apellidos) AS socio,
       s.cedula, s.telefono, t.nombre AS plan,
       m.fecha_inicio AS inicio, m.fecha_vencimiento AS vence, m.estado,
       m.total AS precio_pactado, COALESCE(p.pagado,0) AS pagado,
       m.total-COALESCE(p.pagado,0) AS saldo
FROM membresias m
JOIN socios s ON s.id_socio=m.id_socio
JOIN tipos_membresia t ON t.id_tipo_membresia=m.id_tipo_membresia
LEFT JOIN (SELECT id_membresia, SUM(monto) AS pagado FROM pagos
           WHERE anulado=FALSE GROUP BY id_membresia) p ON p.id_membresia=m.id_membresia;

CREATE OR REPLACE SQL SECURITY DEFINER VIEW vw_estado_cuenta AS
SELECT s.id_socio, CONCAT(s.nombres,' ',s.apellidos) AS socio, s.cedula,
       s.telefono, s.activo AS socio_activo,
       m.id_membresia, t.nombre AS plan, m.fecha_inicio AS inicio,
       m.fecha_vencimiento AS vence, m.estado, m.total AS precio_pactado,
       p.id_pago, p.fecha_pago, COALESCE(p.monto,0) AS monto,
       COALESCE(p.metodo_pago,'Sin registrar') AS metodo_pago,
       COALESCE(p.anulado,FALSE) AS anulado
FROM socios s
LEFT JOIN membresias m ON m.id_socio=s.id_socio
LEFT JOIN tipos_membresia t ON t.id_tipo_membresia=m.id_tipo_membresia
LEFT JOIN pagos p ON p.id_membresia=m.id_membresia;

CREATE OR REPLACE SQL SECURITY DEFINER VIEW vw_horario_semanal AS
SELECT h.IdHorario AS id_horario, h.IdInstructor AS id_instructor, h.IdSala AS id_sala,
       FIELD(h.DiaSemana,'Lunes','Martes','Miércoles','Jueves','Viernes','Sábado','Domingo') AS dia_semana,
       h.DiaSemana AS nombre_dia, h.HoraInicio AS hora_inicio, h.HoraFin AS hora_fin,
       TIME_TO_SEC(TIMEDIFF(h.HoraFin,h.HoraInicio)) DIV 60 AS minutos,
       a.Nombre AS actividad, CONCAT(i.Nombre,' ',i.Apellido) AS instructor, s.Nombre AS sala
FROM horarios h
JOIN instructores i ON i.IdInstructor=h.IdInstructor
JOIN actividades a ON a.IdActividad=h.IdActividad
JOIN salas s ON s.IdSala=h.IdSala
WHERE h.Activo=TRUE AND i.Activo=TRUE AND a.Activo=TRUE AND s.Activo=TRUE;
