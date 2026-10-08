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