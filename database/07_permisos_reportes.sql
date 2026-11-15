USE gimnasio_db;

-- Crear gym_reportes con una contraseña local desde HeidiSQL antes de ejecutar.
-- Esta cuenta debe ser exclusiva para este proyecto, sin roles ni permisos globales.
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'gym_reportes'@'localhost';
GRANT SELECT ON gimnasio_db.vw_ingresos_detalle TO 'gym_reportes'@'localhost';
GRANT SELECT ON gimnasio_db.vw_saldos_membresia TO 'gym_reportes'@'localhost';
GRANT SELECT ON gimnasio_db.vw_estado_cuenta TO 'gym_reportes'@'localhost';
GRANT SELECT ON gimnasio_db.vw_horario_semanal TO 'gym_reportes'@'localhost';
SHOW GRANTS FOR 'gym_reportes'@'localhost';
