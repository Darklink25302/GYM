-- Instalación y actualización de GymControl. Ejecutar como administrador.
-- Conserva los registros existentes; no elimina tablas ni bases.


-- Archivo: 00_crear_base.sql
-- Ejecutar con una cuenta administradora antes de los fragmentos SQL.
CREATE DATABASE IF NOT EXISTS gimnasio_db
CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE gimnasio_db;


-- Archivo: fragmentos/01_seguridad.sql
-- GymControl: tablas del modulo de seguridad.
-- Ejecutar con una conexion administradora sobre gimnasio_db.
-- No elimina bases, tablas ni registros.
-- Este fragmento se integrara despues en database/gimnasio_db.sql.

USE gimnasio_db;

CREATE TABLE IF NOT EXISTS roles (
    id_rol TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(30) NOT NULL,
    PRIMARY KEY (id_rol),
    CONSTRAINT uq_roles_nombre UNIQUE (nombre)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS usuarios (
    id_usuario INT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombre_usuario VARCHAR(50) NOT NULL,
    clave_hash VARCHAR(44) CHARACTER SET ascii COLLATE ascii_bin NOT NULL,
    clave_sal VARCHAR(24) CHARACTER SET ascii COLLATE ascii_bin NOT NULL,
    iteraciones INT UNSIGNED NOT NULL,
    id_rol TINYINT UNSIGNED NOT NULL,
    id_socio INT UNSIGNED NULL,
    id_instructor INT UNSIGNED NULL,
    activo BOOLEAN NOT NULL DEFAULT TRUE,
    intentos_fallidos TINYINT UNSIGNED NOT NULL DEFAULT 0,
    bloqueado BOOLEAN NOT NULL DEFAULT FALSE,
    fecha_creacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_usuario),
    CONSTRAINT uq_usuarios_nombre UNIQUE (nombre_usuario),
    CONSTRAINT fk_usuarios_roles FOREIGN KEY (id_rol)
        REFERENCES roles (id_rol)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT ck_usuarios_iteraciones CHECK (iteraciones > 0),
    CONSTRAINT ck_usuarios_intentos CHECK (intentos_fallidos BETWEEN 0 AND 3),
    CONSTRAINT ck_usuarios_activo CHECK (activo IN (0, 1)),
    CONSTRAINT ck_usuarios_bloqueado CHECK (bloqueado IN (0, 1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- id_socio e id_instructor quedan preparados para los otros modulos.
-- Acordar con el equipo sus tipos INT UNSIGNED y agregar las claves
-- foraneas cuando existan las tablas socios e instructores.
-- Las claves de GymControl se guardan como PBKDF2-SHA256 en Base64:
-- hash de 32 bytes (44 caracteres) y sal de 16 bytes (24 caracteres).
-- Usar siempre las iteraciones devueltas por Seguridad.CrearHash.

CREATE TABLE IF NOT EXISTS bitacora_accesos (
    id_acceso BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    id_usuario INT UNSIGNED NULL,
    nombre_usuario_intentado VARCHAR(50) NOT NULL,
    resultado ENUM('EXITOSO', 'FALLIDO', 'BLOQUEADO') NOT NULL,
    fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    detalle VARCHAR(255) NULL,
    PRIMARY KEY (id_acceso),
    KEY ix_bitacora_usuario_fecha (id_usuario, fecha),
    CONSTRAINT fk_bitacora_usuarios FOREIGN KEY (id_usuario)
        REFERENCES usuarios (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- id_usuario puede ser NULL para registrar un usuario inexistente.
-- No guardar contrasenas en detalle ni en nombre_usuario_intentado.

INSERT INTO roles (nombre)
VALUES ('Administrador'), ('Recepcionista'), ('Instructor'), ('Socio')
ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

-- Comprobaciones. Todavia no se crean usuarios de GymControl:
-- el primer Administrador se registrara usando Seguridad.CrearHash.
SHOW TABLES FROM gimnasio_db;
SELECT id_rol, nombre FROM roles ORDER BY id_rol;


-- Archivo: fragmentos/02_socios_membresias_pagos.sql
-- GymControl: modulo de socios, membresias y pagos.
-- Persona 2.
-- Ejecutar despues de 00_crear_base.sql y 01_seguridad.sql.

USE gimnasio_db;


-- =========================================================
-- SOCIOS
-- =========================================================

CREATE TABLE IF NOT EXISTS socios (
    id_socio INT UNSIGNED NOT NULL AUTO_INCREMENT,
    cedula VARCHAR(20) NOT NULL,
    nombres VARCHAR(80) NOT NULL,
    apellidos VARCHAR(80) NOT NULL,
    telefono VARCHAR(20) NULL,
    correo VARCHAR(100) NULL,
    direccion VARCHAR(200) NULL,
    fecha_nacimiento DATE NULL,
    activo BOOLEAN NOT NULL DEFAULT TRUE,

    PRIMARY KEY (id_socio),

    CONSTRAINT uq_socios_cedula
        UNIQUE (cedula),

    CONSTRAINT ck_socios_activo
        CHECK (activo IN (0, 1))

) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;


-- =========================================================
-- TIPOS DE MEMBRESIA
-- =========================================================

CREATE TABLE IF NOT EXISTS tipos_membresia (
    id_tipo_membresia INT UNSIGNED NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(60) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    duracion_dias INT UNSIGNED NOT NULL,
    activo BOOLEAN NOT NULL DEFAULT TRUE,

    PRIMARY KEY (id_tipo_membresia),

    CONSTRAINT uq_tipos_membresia_nombre
        UNIQUE (nombre),

    CONSTRAINT ck_tipo_precio
        CHECK (precio >= 0),

    CONSTRAINT ck_tipo_duracion
        CHECK (duracion_dias > 0),

    CONSTRAINT ck_tipo_activo
        CHECK (activo IN (0, 1))

) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;


-- =========================================================
-- MEMBRESIAS
-- =========================================================

CREATE TABLE IF NOT EXISTS membresias (
    id_membresia INT UNSIGNED NOT NULL AUTO_INCREMENT,
    id_socio INT UNSIGNED NOT NULL,
    id_tipo_membresia INT UNSIGNED NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    estado VARCHAR(20) NOT NULL DEFAULT 'Activa',

    PRIMARY KEY (id_membresia),

    KEY ix_membresias_socio (id_socio),
    KEY ix_membresias_tipo (id_tipo_membresia),

    CONSTRAINT fk_membresias_socios
        FOREIGN KEY (id_socio)
        REFERENCES socios (id_socio)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT,

    CONSTRAINT fk_membresias_tipos
        FOREIGN KEY (id_tipo_membresia)
        REFERENCES tipos_membresia (id_tipo_membresia)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT,

    CONSTRAINT ck_membresia_total
        CHECK (total >= 0),

    CONSTRAINT ck_membresia_fechas
        CHECK (fecha_vencimiento >= fecha_inicio)

) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;


-- =========================================================
-- PAGOS
-- =========================================================

CREATE TABLE IF NOT EXISTS pagos (
    id_pago INT UNSIGNED NOT NULL AUTO_INCREMENT,
    id_membresia INT UNSIGNED NOT NULL,
    fecha_pago DATE NOT NULL,
    monto DECIMAL(10,2) NOT NULL,

    PRIMARY KEY (id_pago),

    KEY ix_pagos_membresia (id_membresia),

    CONSTRAINT fk_pagos_membresias
        FOREIGN KEY (id_membresia)
        REFERENCES membresias (id_membresia)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT,

    CONSTRAINT ck_pago_monto
        CHECK (monto > 0)

) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;


-- =========================================================
-- RELACION USUARIOS - SOCIOS
-- =========================================================
-- La tabla usuarios se crea en 01_seguridad.sql.
-- Aquí agregamos la FK porque socios ya existe.

SET @fk_existe = (
    SELECT COUNT(*)
    FROM information_schema.TABLE_CONSTRAINTS
    WHERE CONSTRAINT_SCHEMA = 'gimnasio_db'
      AND TABLE_NAME = 'usuarios'
      AND CONSTRAINT_NAME = 'fk_usuarios_socios'
);

SET @sql_fk = IF(
    @fk_existe = 0,
    'ALTER TABLE usuarios
     ADD CONSTRAINT fk_usuarios_socios
     FOREIGN KEY (id_socio)
     REFERENCES socios (id_socio)
     ON DELETE RESTRICT
     ON UPDATE RESTRICT',
    'SELECT 1'
);

PREPARE stmt FROM @sql_fk;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;


-- =========================================================
-- DATOS INICIALES DE TIPOS DE MEMBRESIA
-- =========================================================

INSERT INTO tipos_membresia
    (nombre, precio, duracion_dias, activo)
VALUES
    ('Mensual', 500.00, 30, TRUE),
    ('Trimestral', 1350.00, 90, TRUE),
    ('Semestral', 2500.00, 180, TRUE)
ON DUPLICATE KEY UPDATE
    nombre = VALUES(nombre);


-- =========================================================
-- COMPROBACIONES
-- =========================================================

SHOW TABLES FROM gimnasio_db;

SELECT *
FROM tipos_membresia
ORDER BY id_tipo_membresia;

-- Archivo: fragmentos/04_instructores_horarios.sql
-- Persona 4: instructores, actividades, salas y horarios.
-- Ejecutar sobre una base de pruebas vacía después de 00_crear_base.sql.
USE gimnasio_db;

CREATE TABLE IF NOT EXISTS instructores (
    IdInstructor INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Especialidad VARCHAR(100) NOT NULL DEFAULT '',
    Telefono VARCHAR(30) NOT NULL DEFAULT '',
    Email VARCHAR(150) NOT NULL DEFAULT '',
    Activo BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdInstructor),
    CONSTRAINT ck_instructor_nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT ck_instructor_apellido CHECK (CHAR_LENGTH(TRIM(Apellido)) > 0),
    CONSTRAINT ck_instructor_activo CHECK (Activo IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS actividades (
    IdActividad INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NOT NULL DEFAULT '',
    Activo BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdActividad),
    CONSTRAINT ck_actividad_nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT ck_actividad_activo CHECK (Activo IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS salas (
    IdSala INT UNSIGNED NOT NULL AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    Activo BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdSala),
    CONSTRAINT ck_sala_nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT ck_sala_capacidad CHECK (Capacidad > 0),
    CONSTRAINT ck_sala_activo CHECK (Activo IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS horarios (
    IdHorario INT UNSIGNED NOT NULL AUTO_INCREMENT,
    IdInstructor INT UNSIGNED NOT NULL,
    IdActividad INT UNSIGNED NOT NULL,
    IdSala INT UNSIGNED NOT NULL,
    DiaSemana VARCHAR(20) NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Activo BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdHorario),
    KEY ix_horario_instructor (IdInstructor, DiaSemana, Activo, HoraInicio, HoraFin),
    KEY ix_horario_sala (IdSala, DiaSemana, Activo, HoraInicio, HoraFin),
    CONSTRAINT fk_horario_instructor FOREIGN KEY (IdInstructor) REFERENCES instructores(IdInstructor) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT fk_horario_actividad FOREIGN KEY (IdActividad) REFERENCES actividades(IdActividad) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT fk_horario_sala FOREIGN KEY (IdSala) REFERENCES salas(IdSala) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT ck_horario_dia CHECK (DiaSemana IN ('Lunes','Martes','Miércoles','Jueves','Viernes','Sábado','Domingo')),
    CONSTRAINT ck_horario_horas CHECK (HoraInicio >= '00:00:00' AND HoraFin < '24:00:00' AND HoraFin > HoraInicio),
    CONSTRAINT ck_horario_activo CHECK (Activo IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Los choques se comprueban al guardar desde HorarioDAO, dentro de una transacción.
-- No se borran registros con historial; se usa Activo=0.
-- usuarios.id_instructor y instructores.IdInstructor son INT UNSIGNED.
-- Persona 3 incorpora la relación usuarios-instructores al script integrado,
-- después de crear ambas tablas y comprobar los vínculos existentes.


-- Archivo: 05_vincular_instructores.sql
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


-- Archivo: 06_pagos_reportes.sql
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


-- Archivo: reportes_vistas.sql
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

