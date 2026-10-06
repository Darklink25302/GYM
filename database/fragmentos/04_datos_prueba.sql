-- Datos ficticios opcionales. Usar una sola vez sobre una base de pruebas vacía.
-- Ejecutar después de 04_instructores_horarios.sql.
USE gimnasio_db;

INSERT INTO instructores (Nombre, Apellido, Especialidad, Telefono, Email) VALUES
('Ana', 'Prueba', 'Yoga', '00000000', 'ana@example.com'),
('Luis', 'Prueba', 'Spinning', '00000001', 'luis@example.com');
INSERT INTO actividades (Nombre, Descripcion) VALUES
('Yoga', 'Actividad ficticia de prueba'),
('Spinning', 'Actividad ficticia de prueba');
INSERT INTO salas (Nombre, Capacidad) VALUES ('Sala A', 20), ('Sala B', 15);

-- Registrar horarios desde el formulario para demostrar las validaciones.
