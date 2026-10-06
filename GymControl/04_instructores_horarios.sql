-- =====================================================
-- Script de Instructores, Actividades, Salas y Horarios
-- Persona 4: Instructores y Horarios
-- =====================================================

-- 1. Tabla de Actividades
CREATE TABLE IF NOT EXISTS actividades (
    IdActividad INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    Activo TINYINT(1) DEFAULT 1
);

-- 2. Tabla de Salas
CREATE TABLE IF NOT EXISTS salas (
    IdSala INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Capacidad INT DEFAULT 0,
    Activo TINYINT(1) DEFAULT 1
);

-- 3. Tabla de Horarios
CREATE TABLE IF NOT EXISTS horarios (
    IdHorario INT PRIMARY KEY AUTO_INCREMENT,
    IdInstructor INT NOT NULL,
    IdActividad INT NOT NULL,
    IdSala INT NOT NULL,
    DiaSemana VARCHAR(20) NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    FOREIGN KEY (IdInstructor) REFERENCES instructores(IdInstructor),
    FOREIGN KEY (IdActividad) REFERENCES actividades(IdActividad),
    FOREIGN KEY (IdSala) REFERENCES salas(IdSala)
);

-- 4. Datos de prueba
INSERT INTO actividades (Nombre, Descripcion, Activo) VALUES
('Yoga', 'Clase de yoga relajante', 1),
('Spinning', 'Ciclismo indoor', 1),
('Pesas', 'Entrenamiento de fuerza', 1);

INSERT INTO salas (Nombre, Capacidad, Activo) VALUES
('Sala 1', 20, 1),
('Sala 2', 15, 1),
('Sala de Pesas', 30, 1);