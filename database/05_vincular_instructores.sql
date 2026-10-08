-- Ejecutar con una cuenta administradora después de los fragmentos 01, 02 y 04.
-- Conserva los registros. Si hay IDs de instructor inexistentes, corregirlos antes.
USE gimnasio_db;

SET @fk_existe = (
    SELECT COUNT(*) FROM information_schema.TABLE_CONSTRAINTS
    WHERE CONSTRAINT_SCHEMA = 'gimnasio_db'
      AND TABLE_NAME = 'usuarios'
      AND CONSTRAINT_NAME = 'fk_usuarios_instructores'
);
SET @sql_fk = IF(@fk_existe = 0,
    'ALTER TABLE usuarios ADD CONSTRAINT fk_usuarios_instructores
     FOREIGN KEY (id_instructor) REFERENCES instructores(IdInstructor)
     ON DELETE RESTRICT ON UPDATE RESTRICT',
    'SELECT 1');
PREPARE stmt FROM @sql_fk;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
