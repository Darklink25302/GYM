-- DEMOSTRACIÓN: datos ficticios. Ejecutar una sola vez en una base gimnasio_demo vacía.
-- Instalación y actualización de GymControl. Ejecutar como administrador.
-- Conserva los registros existentes; no elimina tablas ni bases.


-- Archivo: 00_crear_base.sql
-- Ejecutar con una cuenta administradora antes de los fragmentos SQL.
CREATE DATABASE IF NOT EXISTS gimnasio_demo
CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE gimnasio_demo;


-- Archivo: fragmentos/01_seguridad.sql
-- GymControl: tablas del modulo de seguridad.
-- Ejecutar con una conexion administradora sobre gimnasio_demo.
-- No elimina bases, tablas ni registros.
-- Este fragmento se integrara despues en database/gimnasio_demo.sql.

USE gimnasio_demo;

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
SHOW TABLES FROM gimnasio_demo;
SELECT id_rol, nombre FROM roles ORDER BY id_rol;


-- Archivo: fragmentos/02_socios_membresias_pagos.sql
-- GymControl: modulo de socios, membresias y pagos.
-- Persona 2.
-- Ejecutar despues de 00_crear_base.sql y 01_seguridad.sql.

USE gimnasio_demo;


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
    WHERE CONSTRAINT_SCHEMA = 'gimnasio_demo'
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

SHOW TABLES FROM gimnasio_demo;

SELECT *
FROM tipos_membresia
ORDER BY id_tipo_membresia;

-- Archivo: fragmentos/04_instructores_horarios.sql
-- Persona 4: instructores, actividades, salas y horarios.
-- Ejecutar sobre una base de pruebas vacía después de 00_crear_base.sql.
USE gimnasio_demo;

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
USE gimnasio_demo;

SET @fk_existe = (
    SELECT COUNT(*) FROM information_schema.TABLE_CONSTRAINTS
    WHERE CONSTRAINT_SCHEMA = 'gimnasio_demo'
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
USE gimnasio_demo;

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
USE gimnasio_demo;

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


DELETE FROM tipos_membresia;
DELETE FROM roles;
/*M!999999\- enable the sandbox mode */ 

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` (`id_rol`, `nombre`) VALUES (1,'Administrador'),
(3,'Instructor'),
(2,'Recepcionista'),
(4,'Socio');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `socios` DISABLE KEYS */;
INSERT INTO `socios` (`id_socio`, `cedula`, `nombres`, `apellidos`, `telefono`, `correo`, `direccion`, `fecha_nacimiento`, `activo`) VALUES (1,'DEMO-001','Socio 01','Demostración','0000-0001',NULL,NULL,NULL,1),
(2,'DEMO-002','Socio 02','Demostración','0000-0002',NULL,NULL,NULL,1),
(3,'DEMO-003','Socio 03','Demostración','0000-0003',NULL,NULL,NULL,1),
(4,'DEMO-004','Socio 04','Demostración','0000-0004',NULL,NULL,NULL,1),
(5,'DEMO-005','Socio 05','Demostración','0000-0005',NULL,NULL,NULL,1),
(6,'DEMO-006','Socio 06','Demostración','0000-0006',NULL,NULL,NULL,1),
(7,'DEMO-007','Socio 07','Demostración','0000-0007',NULL,NULL,NULL,1),
(8,'DEMO-008','Socio 08','Demostración','0000-0008',NULL,NULL,NULL,1),
(9,'DEMO-009','Socio 09','Demostración','0000-0009',NULL,NULL,NULL,1),
(10,'DEMO-010','Socio 10','Demostración','0000-0010',NULL,NULL,NULL,1),
(11,'DEMO-011','Socio 11','Demostración','0000-0011',NULL,NULL,NULL,1),
(12,'DEMO-012','Socio 12','Demostración','0000-0012',NULL,NULL,NULL,1),
(13,'DEMO-013','Socio 13','Demostración','0000-0013',NULL,NULL,NULL,1),
(14,'DEMO-014','Socio 14','Demostración','0000-0014',NULL,NULL,NULL,1),
(15,'DEMO-015','Socio 15','Demostración','0000-0015',NULL,NULL,NULL,1),
(16,'DEMO-016','Socio 16','Demostración','0000-0016',NULL,NULL,NULL,1),
(17,'DEMO-017','Socio 17','Demostración','0000-0017',NULL,NULL,NULL,1),
(18,'DEMO-018','Socio 18','Demostración','0000-0018',NULL,NULL,NULL,1),
(19,'DEMO-019','Socio 19','Demostración','0000-0019',NULL,NULL,NULL,1),
(20,'DEMO-020','Socio 20','Demostración','0000-0020',NULL,NULL,NULL,1),
(21,'DEMO-021','Socio 21','Demostración','0000-0021',NULL,NULL,NULL,1),
(22,'DEMO-022','Socio 22','Demostración','0000-0022',NULL,NULL,NULL,1),
(23,'DEMO-023','Socio 23','Demostración','0000-0023',NULL,NULL,NULL,1),
(24,'DEMO-024','Socio 24','Demostración','0000-0024',NULL,NULL,NULL,1),
(25,'DEMO-025','Socio 25','Demostración','0000-0025',NULL,NULL,NULL,1),
(26,'DEMO-026','Socio 26','Demostración','0000-0026',NULL,NULL,NULL,1),
(27,'DEMO-027','Socio 27','Demostración','0000-0027',NULL,NULL,NULL,1),
(28,'DEMO-028','Socio 28','Demostración','0000-0028',NULL,NULL,NULL,1),
(29,'DEMO-029','Socio 29','Demostración','0000-0029',NULL,NULL,NULL,1),
(30,'DEMO-030','Socio 30','Demostración','0000-0030',NULL,NULL,NULL,1);
/*!40000 ALTER TABLE `socios` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `tipos_membresia` DISABLE KEYS */;
INSERT INTO `tipos_membresia` (`id_tipo_membresia`, `nombre`, `precio`, `duracion_dias`, `activo`) VALUES (1,'Mensual',500.00,30,1),
(2,'Trimestral',1350.00,90,1),
(3,'Semestral',2500.00,180,1);
/*!40000 ALTER TABLE `tipos_membresia` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `instructores` DISABLE KEYS */;
INSERT INTO `instructores` (`IdInstructor`, `Nombre`, `Apellido`, `Especialidad`, `Telefono`, `Email`, `Activo`) VALUES (1,'Instructor 1','Demostración','','','',1),
(2,'Instructor 2','Demostración','','','',1),
(3,'Instructor 3','Demostración','','','',1),
(4,'Instructor 4','Demostración','','','',1);
/*!40000 ALTER TABLE `instructores` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `actividades` DISABLE KEYS */;
INSERT INTO `actividades` (`IdActividad`, `Nombre`, `Descripcion`, `Activo`) VALUES (1,'Actividad 1','',1),
(2,'Actividad 2','',1),
(3,'Actividad 3','',1),
(4,'Actividad 4','',1);
/*!40000 ALTER TABLE `actividades` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `salas` DISABLE KEYS */;
INSERT INTO `salas` (`IdSala`, `Nombre`, `Capacidad`, `Activo`) VALUES (1,'Sala 1',20,1),
(2,'Sala 2',20,1),
(3,'Sala 3',20,1),
(4,'Sala 4',20,1);
/*!40000 ALTER TABLE `salas` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` (`id_usuario`, `nombre_usuario`, `clave_hash`, `clave_sal`, `iteraciones`, `id_rol`, `id_socio`, `id_instructor`, `activo`, `intentos_fallidos`, `bloqueado`, `fecha_creacion`) VALUES (1,'demo_admin','0oQ7AAqhTBtCig6AEn4eFNTqj5yQ8AvycVYMynMOU7I=','6is5jbVqAGNd9Ko47ga3LQ==',600000,1,NULL,NULL,1,0,0,'2026-11-15 10:59:43'),
(2,'demo_recepcion','QFqwO9TprASFjAlApGWPcM4Gx2wqYJEI7RZdntQkaMw=','O+g8jyxG43WIDXVIHF9f4g==',600000,2,NULL,NULL,1,0,0,'2026-11-15 10:59:43'),
(3,'demo_instructor','6vLBAA/SWuVI55LdPdwD89oke47BM9seYU4JQQlhjVI=','SClfP6yD4o80+yqtDDNnrA==',600000,3,NULL,1,1,0,0,'2026-11-15 10:59:44'),
(4,'demo_socio','/zGuYntGFDmcCIxseEviLeFKtbEzo1DMBFVrFPsRB5s=','4e1UX/CAMbvBL90xdYdKkw==',600000,4,1,NULL,1,0,0,'2026-11-15 10:59:44');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `membresias` DISABLE KEYS */;
INSERT INTO `membresias` (`id_membresia`, `id_socio`, `id_tipo_membresia`, `fecha_inicio`, `fecha_vencimiento`, `total`, `estado`) VALUES (1,1,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(2,2,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(3,3,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(4,4,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(5,5,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(6,6,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(7,7,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(8,8,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(9,9,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(10,10,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(11,11,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(12,12,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(13,13,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(14,14,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(15,15,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(16,16,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(17,17,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(18,18,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(19,19,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(20,20,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(21,21,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(22,22,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(23,23,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(24,24,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(25,25,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(26,26,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(27,27,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(28,28,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(29,29,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(30,30,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(31,1,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(32,2,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(33,3,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(34,4,1,'2026-08-05','2026-09-04',1000.00,'Activa'),
(35,5,2,'2026-07-05','2026-10-03',1000.00,'Activa'),
(36,6,3,'2026-06-05','2026-12-02',1000.00,'Activa'),
(37,7,1,'2026-11-05','2026-12-05',1000.00,'Activa'),
(38,8,2,'2026-10-05','2027-01-03',1000.00,'Activa'),
(39,9,3,'2026-09-05','2027-03-04',1000.00,'Activa'),
(40,10,1,'2026-08-05','2026-09-04',1000.00,'Cancelada'),
(41,1,1,'2026-11-15','2026-12-15',800.00,'Activa'),
(42,1,1,'2026-11-15','2026-12-15',800.00,'Activa'),
(43,2,1,'2026-11-15','2026-12-15',1000.00,'Activa');
/*!40000 ALTER TABLE `membresias` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `pagos` DISABLE KEYS */;
INSERT INTO `pagos` (`id_pago`, `id_membresia`, `fecha_pago`, `monto`, `metodo_pago`, `id_usuario`, `anulado`, `motivo_anulacion`, `fecha_anulacion`, `id_usuario_anula`) VALUES (1,1,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(2,1,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(3,2,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(4,2,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(5,3,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(6,3,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(7,4,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(8,4,'2026-08-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(9,5,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(10,5,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(11,6,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(12,6,'2026-06-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(13,7,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(14,7,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(15,8,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(16,8,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(17,9,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(18,9,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(19,10,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(20,10,'2026-08-09 10:00:00',300.00,'Tarjeta',2,1,NULL,NULL,NULL),
(21,11,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(22,11,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(23,12,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(24,12,'2026-06-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(25,13,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(26,13,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(27,14,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(28,14,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(29,15,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(30,15,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(31,16,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(32,16,'2026-08-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(33,17,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(34,17,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(35,18,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(36,18,'2026-06-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(37,19,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(38,19,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(39,20,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(40,20,'2026-10-09 10:00:00',300.00,'Efectivo',2,1,NULL,NULL,NULL),
(41,21,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(42,21,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(43,22,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(44,22,'2026-08-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(45,23,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(46,23,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(47,24,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(48,24,'2026-06-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(49,25,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(50,25,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(51,26,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(52,26,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(53,27,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(54,27,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(55,28,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(56,28,'2026-08-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(57,29,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(58,29,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(59,30,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(60,30,'2026-06-09 10:00:00',300.00,'Transferencia',2,1,NULL,NULL,NULL),
(61,31,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(62,31,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(63,32,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(64,32,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(65,33,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(66,33,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(67,34,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(68,34,'2026-08-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(69,35,'2026-07-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(70,35,'2026-07-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(71,36,'2026-06-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(72,36,'2026-06-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(73,37,'2026-11-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(74,37,'2026-11-09 10:00:00',300.00,'Tarjeta',2,0,NULL,NULL,NULL),
(75,38,'2026-10-07 09:00:00',200.00,'Transferencia',2,0,NULL,NULL,NULL),
(76,38,'2026-10-09 10:00:00',300.00,'Efectivo',2,0,NULL,NULL,NULL),
(77,39,'2026-09-07 09:00:00',200.00,'Tarjeta',2,0,NULL,NULL,NULL),
(78,39,'2026-09-09 10:00:00',300.00,'Transferencia',2,0,NULL,NULL,NULL),
(79,40,'2026-08-07 09:00:00',200.00,'Efectivo',2,0,NULL,NULL,NULL),
(80,40,'2026-08-09 10:00:00',300.00,'Tarjeta',2,1,NULL,NULL,NULL),
(81,41,'2026-11-15 10:59:44',300.00,'Tarjeta',1,0,NULL,NULL,NULL),
(82,41,'2026-11-15 10:59:44',500.00,'Transferencia',2,1,'Anulación de demostración','2026-11-15 10:59:44',2),
(83,43,'2026-11-15 10:59:44',600.00,'Efectivo',1,0,NULL,NULL,NULL);
/*!40000 ALTER TABLE `pagos` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `horarios` DISABLE KEYS */;
INSERT INTO `horarios` (`IdHorario`, `IdInstructor`, `IdActividad`, `IdSala`, `DiaSemana`, `HoraInicio`, `HoraFin`, `Activo`) VALUES (1,1,1,1,'Lunes','08:00:00','09:00:00',1),
(2,2,2,2,'Martes','08:00:00','09:00:00',1),
(3,3,3,3,'Miércoles','08:00:00','09:00:00',1),
(4,4,4,4,'Jueves','08:00:00','09:00:00',1),
(5,1,1,1,'Viernes','08:00:00','09:00:00',1),
(6,2,2,2,'Sábado','08:00:00','09:00:00',1),
(7,3,3,3,'Lunes','10:00:00','11:00:00',1),
(8,4,4,4,'Martes','10:00:00','11:00:00',1),
(9,1,1,1,'Miércoles','10:00:00','11:00:00',1),
(10,2,2,2,'Jueves','10:00:00','11:00:00',1),
(11,3,3,3,'Viernes','10:00:00','11:00:00',1),
(12,4,4,4,'Sábado','10:00:00','11:00:00',1),
(13,1,1,1,'Lunes','12:00:00','13:00:00',1),
(14,2,2,2,'Martes','12:00:00','13:00:00',1),
(15,3,3,3,'Miércoles','12:00:00','13:00:00',1),
(16,4,4,4,'Jueves','12:00:00','13:00:00',1),
(17,1,1,1,'Viernes','12:00:00','13:00:00',1),
(18,2,2,2,'Sábado','12:00:00','13:00:00',1),
(19,3,3,3,'Lunes','14:00:00','15:00:00',1),
(20,4,4,4,'Martes','14:00:00','15:00:00',1);
/*!40000 ALTER TABLE `horarios` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;

SET @OLD_AUTOCOMMIT=@@AUTOCOMMIT, @@AUTOCOMMIT=0;
/*!40000 ALTER TABLE `bitacora_accesos` DISABLE KEYS */;
/*!40000 ALTER TABLE `bitacora_accesos` ENABLE KEYS */;
COMMIT;
SET AUTOCOMMIT=@OLD_AUTOCOMMIT;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;
