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
