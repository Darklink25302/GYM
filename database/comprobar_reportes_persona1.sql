-- Abrir la conexión gym_reportes y ejecutar en gimnasio_db.
-- Estas comprobaciones no crean ni modifican datos o permisos.

USE gimnasio_db;

SELECT CURRENT_USER() AS cuenta_actual,
       IF(SUBSTRING_INDEX(CURRENT_USER(), '@', 1) = 'gym_reportes',
          'OK', 'Usar la conexion gym_reportes') AS comprobacion_cuenta;

SHOW GRANTS FOR CURRENT_USER;

-- R01: comprobar nombres de columnas sin recuperar datos personales.
SELECT id_pago, id_membresia, fecha, socio, cedula, plan,
       metodo_pago, monto, anulado, usuario
FROM vw_ingresos_detalle
LIMIT 0;

-- R01 y R02: una vista de saldos con los campos necesarios.
SELECT id_membresia, socio, cedula, plan, inicio, vence, estado,
       precio_pactado, pagado, saldo
FROM vw_saldos_membresia
LIMIT 0;
