# Sesión y permisos de GymControl

## Datos que comparten los módulos

`Sesion` conserva los datos de la cuenta que inició sesión:

| Campo | Uso |
| --- | --- |
| IdUsuario | Identificador de la cuenta de acceso. |
| NombreUsuario | Nombre mostrado en la aplicación. |
| Rol | Administrador, Recepcionista, Instructor o Socio. |
| IdSocio | ID del socio vinculado, o Nothing si no está asignado. |
| IdInstructor | ID del instructor vinculado, o Nothing si no está asignado. |

El login llena estos campos después de un acceso válido. `Sesion.Limpiar()` los borra al cerrar sesión. Los formularios de los demás módulos deben consultar la sesión existente.

## Reparto de acceso según el plan

| Rol | Alcance |
| --- | --- |
| Administrador | Usuarios, desbloqueos y gestión de los módulos. |
| Recepcionista | Socios, membresías y registro de pagos. |
| Instructor | Sus horarios y los datos permitidos. |
| Socio | Sus propios datos, membresías, pagos y saldo. |

La administración de usuarios ya comprueba el permiso al abrir la pantalla y al consultar o guardar datos. Los otros módulos deben comprobar su permiso en las operaciones de datos, además de presentar las opciones correspondientes en la ventana principal.

## Vínculos con socios e instructores

- Persona 2 aporta la tabla `socios` y comunica el ID del socio registrado.
- Persona 4 aporta la tabla `instructores` y comunica el ID del instructor registrado.
- Persona 1 vincula esos registros con la cuenta de acceso desde Usuarios.
- Los identificadores acordados para esas relaciones son INT UNSIGNED.
- Las claves foráneas se incorporarán cuando existan las tablas referenciadas.
- Si el vínculo todavía no existe, el portal o la consulta de horarios debe informar que falta asignarlo. No debe mostrar los registros de otras personas.

## Archivos compartidos

La Persona 1 mantiene `Datos/ConexionBD.vb`, `Seguridad/Seguridad.vb`, `Seguridad/Sesion.vb` y `Formularios/FrmPrincipal.vb`. Los compañeros indican qué formularios debe abrir el menú; los cambios en estos archivos se coordinan antes de integrarlos.
