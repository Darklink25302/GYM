# Pruebas de seguridad y usuarios

## Comprobaciones realizadas

- Restauración desde una copia limpia con los paquetes instalados y compilación completa: 0 errores y 0 advertencias.
- 39 pruebas de datos usando una instancia temporal de MariaDB y el script `database/fragmentos/01_seguridad.sql`.
- 47 comprobaciones de formularios, permisos, inicio y cierre de sesión: 5 de arranque y salida, 9 de login y preparación inicial, y 33 de usuarios y roles.
- Acceso, altas, edición, desbloqueo, cambio de contraseña y listado usando una cuenta de MariaDB con permisos SELECT, INSERT, UPDATE y DELETE.
- En el equipo del proyecto se confirmó el inicio de sesión del primer administrador y la creación de otra cuenta desde Usuarios.

Las pruebas de datos se hicieron con cuentas ficticias en una base temporal. No se utilizaron las credenciales ni los datos de la base local del proyecto.

## Casos verificados

1. Crear el primer administrador y rechazar un segundo intento de preparación inicial.
2. Crear y editar usuarios sin cambiar su contraseña al guardar otros datos.
3. Rechazar nombres duplicados, roles inexistentes y contraseñas cortas.
4. Rechazar el acceso de usuarios inactivos.
5. Bloquear una cuenta tras tres fallos y conservar el bloqueo aunque se escriba la contraseña correcta.
6. Desbloquear la cuenta y reiniciar sus intentos.
7. Restablecer una contraseña con nueva sal; rechazar la anterior y aceptar la nueva.
8. Conservar en la bitácora éxitos, fallos, bloqueos y nombres de usuarios inexistentes.
9. Respetar los vínculos de socio e instructor según el rol asignado.
10. Rechazar la administración de usuarios desde los roles Recepcionista, Instructor y Socio.
11. Verificar el permiso del administrador en la base, aunque el rol de la sesión local se altere.
12. Impedir que el administrador inhabilite su propia cuenta o se quite el rol.
13. Limpiar la selección para crear un usuario nuevo y cargar los datos al elegir una fila.
14. Iniciar la aplicación desde FrmLogin, cargar sus recursos y limpiar la sesión al cerrar la ventana principal o salir del login.
15. Impedir el cierre mientras se completa una operación en curso.

## Prueba manual completa para repetir en el equipo

1. Ejecutar con F5. La aplicación debe abrir FrmLogin; iniciar sesión como administrador.
2. Abrir **Administrar usuarios**.
3. Pulsar **Nuevo**, crear un usuario de prueba con rol Recepcionista y una contraseña de al menos 12 caracteres.
4. Cerrar sesión y comprobar el acceso con ese usuario. No debe aparecer la administración de usuarios.
5. Volver al login y escribir tres contraseñas incorrectas para ese usuario de prueba. La cuenta debe quedar bloqueada.
6. Entrar como administrador, seleccionar la cuenta bloqueada y pulsar **Desbloquear**.
7. Verificar que la cuenta puede entrar de nuevo y que sus intentos vuelven a cero.
8. Desde el administrador, escribir y confirmar una contraseña nueva para esa cuenta y pulsar **Restablecer contraseña**. Verificar que la anterior no funciona y que la nueva sí.
9. Seleccionar esa cuenta, desmarcar **Activo** y pulsar **Guardar**. Verificar que ya no puede entrar. Marcar Activo y guardar para reactivarla.
10. Repetir la comprobación de acceso y permisos para Instructor y Socio cuando existan sus registros asociados.

## Integración pendiente

- Conectar desde la ventana principal los módulos de los otros integrantes según los permisos de cada rol.
- Acordar y agregar las claves foráneas de `usuarios.id_socio` y `usuarios.id_instructor` cuando estén disponibles las tablas correspondientes.
- Verificar el proyecto desde un clon actualizado con la configuración local de un compañero.
- Revisar e integrar los cambios en `main`.
