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
