USE gimnasio_db;

ALTER TABLE pagos MODIFY COLUMN fecha_pago DATETIME NOT NULL;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS metodo_pago VARCHAR(20) NULL;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS id_usuario INT UNSIGNED NULL;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS anulado BOOLEAN NOT NULL DEFAULT FALSE;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS motivo_anulacion VARCHAR(255) NULL;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS fecha_anulacion DATETIME NULL;
ALTER TABLE pagos ADD COLUMN IF NOT EXISTS id_usuario_anula INT UNSIGNED NULL;
ALTER TABLE pagos ADD INDEX IF NOT EXISTS ix_pago_membresia_fecha (id_membresia, fecha_pago, id_pago);

-- Los datos históricos desconocidos se conservan como NULL.
-- No se asigna un método, una hora ni un registrador inventado a pagos anteriores.
