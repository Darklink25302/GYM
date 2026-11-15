# GymControl: reportes

Aplicación de escritorio para socios, membresías, pagos, usuarios y horarios. El Centro de reportes reúne nueve reportes con permisos según el rol.

## Requisitos

- Visual Studio 2026, con Desarrollo de escritorio de .NET y SDK .NET 10.
- MariaDB 12.3 y HeidiSQL para ejecutar los scripts con una cuenta administradora.
- Windows y una impresora instalada. Las muestras usan Microsoft Print to PDF.
- Paquetes NuGet: MySqlConnector 2.6.2, ReportViewerCore.WinForms 15.1.33, ClosedXML 0.105.1 y QuestPDF 2026.9.1.

## Base y conexión

1. Ejecutar `database/gimnasio_db.sql` como administrador. Crea las tablas, amplía pagos y crea las cuatro vistas. Puede actualizar la base anterior sin borrar sus registros. No es necesario volver a ejecutar los fragmentos por separado.
2. Crear una cuenta de aplicación con SELECT, INSERT, UPDATE y DELETE sobre `gimnasio_db.*`, sin usar root en el programa.
3. Crear la cuenta `gym_reportes`, con una contraseña propia, y ejecutar `database/07_permisos_reportes.sql`. El script usa el host `localhost`: si la cuenta se creó como `127.0.0.1`, cambiar ese host en las cinco instrucciones. La cuenta debe ser exclusiva de este proyecto, sin permisos globales ni roles heredados.
4. Copiar `GymControl/conexion.ejemplo.json` a `GymControl/conexion.local.json` y completar servidor, puerto, base, usuario y clave de la cuenta de aplicación.
5. Copiar `GymControl/conexion.reportes.ejemplo.json` a `GymControl/conexion.reportes.local.json` y completar los datos de `gym_reportes`. Ambas conexiones deben apuntar a la misma base.
6. Abrir `GymControl/GymControl.slnx`, restaurar NuGet, compilar e iniciar con F5. Los archivos locales se copian a la salida al compilar y están excluidos de Git.
7. En una instalación nueva, crear el primer administrador desde la pantalla inicial. Después puede crear las otras cuentas. Vincular las cuentas Socio e Instructor a su registro correspondiente.

Las vistas están también en `database/reportes_vistas.sql` para consultarlas o recrearlas por separado. Los pagos antiguos conservan método y registrador sin especificar; no se inventan esos datos.

## Demostración con datos ficticios

`database/gimnasio_demo.sql` prepara una base independiente llamada `gimnasio_demo`. Ejecutarlo una sola vez, con esa base vacía; no usarlo para actualizar datos reales. Incluye 30 socios, 43 membresías, 83 pagos en seis meses, tres métodos de pago, cinco anulados y 20 horarios. Los registros adicionales permiten demostrar pagos, anulaciones y membresías sin pagos.

Para probarla, cambiar ambas conexiones locales a `gimnasio_demo` y conceder a sus cuentas los mismos permisos indicados arriba sobre esa base. El script de permisos debe usar `USE gimnasio_demo` y `gimnasio_demo.` en los GRANT. No publica contraseñas de cuentas MariaDB.

| Cuenta del programa | Rol | Contraseña de demostración |
|---|---|---|
| demo_admin | Administrador | DemoGym2026! |
| demo_recepcion | Recepcionista | DemoGym2026! |
| demo_instructor | Instructor vinculado al instructor 1 | DemoGym2026! |
| demo_socio | Socio vinculado al socio 1 | DemoGym2026! |

Estas cuentas corresponden exclusivamente a la base ficticia. No son credenciales de la instalación personal.

## Reportes

| Código | Reporte | Acceso | Formato | Reparto acordado |
|---|---|---|---|---|
| R01 | Recibo, monto en letras y marca de anulado | Administrador, Recepcionista | Impresión de 80 mm | Persona 1 |
| R02 | Constancia de membresía vigente | Administrador, Recepcionista | Impresión Carta | Persona 1 |
| R03 | Vencimientos, saldo y control de llamadas | Administrador, Recepcionista | Impresión paginada | Persona 2 |
| R04 | Ingresos por día y método, resumen de anulados | Administrador | RDLC, PDF, Excel | Persona 3 |
| R05 | Estado de cuenta por membresía | Administrador, Recepcionista, Socio propio | RDLC y PDF | Persona 3 |
| R06 | Horario semanal y ocupación por instructor/sala | Todos; Instructor solo sus clases | RDLC y PDF | Persona 4 |
| R07 | Cartera, fórmulas, categorías y totales filtrados | Administrador | Excel | Persona 2 |
| R08 | Informe mensual, planes, saldos y horas | Administrador | PDF | Persona 4 |
| R09 | Doce meses y resumen anual con fórmulas | Administrador | Excel | Persona 4 |

La integración final completa los módulos pendientes y conserva los avances útiles de las ramas.

## Uso y comprobaciones

- Registrar una membresía y su primer abono desde Membresías y pagos. El registro es conjunto: si falla el pago, no queda una membresía a medias.
- Seleccionar una membresía para consultar pagos, agregar un abono o anularlo con motivo. No se aceptan importes mayores que el saldo. El recibo usa el ID confirmado del pago.
- Abrir Reportes desde el menú. Elegir un reporte, ajustar sus parámetros y usar Vista previa, Imprimir o Exportar. El portal del socio también permite abrir su estado de cuenta.
- R05 muestra el saldo con todo el historial válido; el filtro Desde limita el detalle visible de pagos. Anular conserva el registro y restaura el saldo.
- R08 separa los ingresos e inscripciones del mes de los saldos y membresías vigentes al momento de generar el informe.
- En R09 solo se protege la hoja Resumen; los meses quedan disponibles para la demostración de recálculo. Los archivos exportados son copias, no modifican MariaDB.

Resultados de comprobación y limitaciones: `docs/pruebas_integracion.md`. Muestras ficticias de los nueve reportes: `docs/muestras/`.

## Entrega

Falta que el equipo grabe el video de 6 a 10 minutos, agregue su enlace aquí y confirme el número de equipo y la carpeta del repositorio del curso. No se incluye un enlace inventado. Guion sugerido: `docs/guion_demostracion.md`.

Antes de publicar: revisar los cambios, excluir contraseñas y archivos locales, hacer commit en la rama de integración y abrir el pull request que corresponda al flujo indicado por el profesor.
