# Preparación del módulo de reportes

## Paquetes instalados

El proyecto conserva .NET 10 y MySqlConnector. Se agregaron:

| Paquete | Versión |
| --- | --- |
| ReportViewerCore.WinForms | 15.1.33 |
| ClosedXML | 0.105.1 |
| QuestPDF | 2026.9.1 |

La licencia Community de QuestPDF se establece en el evento Startup de
ApplicationEvents.vb, antes de abrir el login. Se usa para este proyecto académico.
No se cambió el formulario de inicio ni se agregó otro Sub Main.

## Conexión de lectura

`ConexionBD.CrearConexion()` sigue leyendo `conexion.local.json` para los módulos
existentes. `ConexionBD.CrearConexionReportes()` lee un archivo separado,
`conexion.reportes.local.json`, y exige el usuario `gym_reportes`.

Cuando Persona 2 entregue el usuario y las vistas:

1. Copiar `GymControl/conexion.reportes.ejemplo.json` en la misma carpeta con el
   nombre `conexion.reportes.local.json`.
2. Ajustar servidor, puerto y base a la instalación local. El puerto del ejemplo
   es orientativo; no cambiar el de la conexión existente.
3. Escribir la contraseña local de `gym_reportes`. No usar root ni gymcontrol_app.
4. Persona 2 debe conceder SELECT solo sobre las vistas de reportes y comprobar
   sus permisos en MariaDB. Usar ese nombre no concede permisos automáticamente.
5. Compilar. El archivo privado se copia al directorio de ejecución y está
   excluido de Git.

Esta entrega no crea usuarios, vistas ni modifica datos de MariaDB. La conexión
real de reportes depende de la entrega de Persona 2. No se reutilizan las claves
de la conexión principal ni se hace una conexión alternativa con más permisos.

## Centro de reportes

El botón Reportes de FrmPrincipal abre `frmCentroReportes`. Se muestran los
reportes permitidos en el catálogo del profesor: Administrador R01 a R09;
Recepcionista R01, R02, R03, R05 y R06; Instructor R06; Socio R05 y R06.

El Centro permite recorrer el catálogo y ver los parámetros de cada reporte.
Los selectores de socios, instructores y salas todavía no se llenan porque
dependen de la capa de consultas de Persona 2. Socio e Instructor no pueden
escoger otro identificador en sus reportes restringidos.

Los botones de impresión y exportación permanecen deshabilitados y se informa
que la generación está pendiente. No se crean archivos vacíos ni se muestran
mensajes de generación exitosa. Cada dueño conectará sus reportes cuando existan;
Persona 1 conserva los archivos del Centro y del menú principal.

## Cómo probar esta parte

1. Abrir `GymControl/GymControl.slnx`, fuera de Backup.
2. Compilar con Ctrl+Mayús+B y ejecutar con F5.
3. Iniciar sesión y pulsar Reportes. Revisar los títulos, descripciones y filtros.
4. Cambiar de reporte y seleccionar una categoría: no deben quedar parámetros
   visibles del reporte anterior. Cerrar el Centro debe regresar al menú.
5. Repetir con las cuatro cuentas de prueba para comprobar su catálogo.
6. Confirmar que los botones pendientes no intentan imprimir ni exportar.

Los PDF y Excel finales, las clases de impresión R01 y R02 y la conexión con los
reportes de los compañeros corresponden a las siguientes tareas del plan.
