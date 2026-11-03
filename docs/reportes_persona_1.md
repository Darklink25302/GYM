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

R01 abre la ventana de recibos, donde se busca un pago, se previsualiza y se
imprime. Requiere la conexión y las vistas de reportes indicadas en
`docs/contrato_recibos.md`. Los demás reportes permanecen deshabilitados hasta
su integración. Persona 1 conserva los archivos del Centro y del menú principal.

Los permisos ahora se verifican también contra la cuenta vigente en MariaDB,
al seleccionar un reporte, consultar un recibo e iniciar su impresión. Los
compañeros tienen disponibles los controles de alcance de socio e instructor
en PermisosReportes para sus propias consultas.

## Cómo probar esta parte

1. Abrir `GymControl/GymControl.slnx`, fuera de Backup.
2. Compilar con Ctrl+Mayús+B y ejecutar con F5.
3. Iniciar sesión y pulsar Reportes. Revisar los títulos, descripciones y filtros.
4. Cambiar de reporte y seleccionar una categoría: no deben quedar parámetros
   visibles del reporte anterior. Cerrar el Centro debe regresar al menú.
5. Repetir con las cuatro cuentas de prueba para comprobar su catálogo.
6. Entrar a R01 y buscar los pagos. Sin conexión de lectura o vistas, la ventana
   debe explicar qué falta. Con la base de reportes lista, seleccionar un pago,
   abrir la vista previa y reimprimir. Comprobar saldo, monto en letras y ANULADO.
7. Confirmar que los demás reportes pendientes no intentan imprimir ni exportar.

La constancia R02 y la integración con los reportes de los compañeros siguen
pendientes. Persona 3 puede conectar el recibo al registro de pagos con el
método compartido explicado en el contrato de recibos.
