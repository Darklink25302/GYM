# Datos y conexiones del recibo R01

El recibo se consulta desde Reportes > R01. Primero se busca y selecciona un pago;
después se abre la vista previa o se elige una impresora. Reimprimir no registra
otro pago ni cambia la base.

También se puede seleccionar una fila en Membresías y pagos y pulsar Recibos de
esta membresía. Ese botón abre solamente sus pagos y ajusta las fechas desde el
primero hasta el último, incluidos los recibos anulados. Así se encuentran los
pagos antiguos sin tener que ampliar manualmente el filtro del mes actual.
Las membresías vencidas o canceladas conservan acceso a sus recibos; la condición
de vigencia se exige para R02, no para reimprimir pagos registrados.

## Vistas que entrega Persona 2

ReciboDAO utiliza CrearConexionReportes y consulta estas dos vistas. Los alias
deben coincidir con esta lista; se pueden agregar más columnas para otros reportes.

| Vista | Columnas necesarias para R01 |
| --- | --- |
| vw_ingresos_detalle | id_pago, id_membresia, fecha, socio, cedula, plan, metodo_pago, monto, anulado, usuario |
| vw_saldos_membresia | id_membresia, inicio, vence, precio_pactado |

- `fecha` debe conservar la fecha y hora real del pago; `inicio` y `vence` son
  las fechas de la membresía.
- `monto` y `precio_pactado` son DECIMAL. El precio pactado sale de membresias.total,
  no del precio actual del catálogo.
- `anulado` es un booleano y la vista conserva también los pagos anulados.
- `usuario` identifica a quien registró el pago; no es quien lo reimprime.
- La vista de saldos devuelve una sola fila por membresía, incluso sin pagos.
- La cuenta gym_reportes necesita SELECT sobre las dos vistas. No necesita
  acceso directo a tablas ni permisos de escritura.

La primera etapa publicada todavía no guarda método, anulación, usuario ni
hora de cada pago. Personas 2 y 3 deben acordar esos campos y completar su
registro. Este cambio no ejecuta migraciones ni asigna datos inventados a los
pagos anteriores. Si falta la conexión o las vistas, la ventana avisa y no imprime.

## Saldo del recibo

Se resta al precio pactado la suma de los pagos no anulados hasta la fecha y
hora del pago elegido. Si dos pagos tienen el mismo instante se ordenan por
id_pago. Los pagos posteriores no alteran el saldo de un recibo anterior.

El cálculo utiliza el estado de anulación actual. Al reimprimir un pago anulado,
se muestra ANULADO y ese abono no participa en el saldo. No se guarda una copia
histórica separada del saldo.

## Lo que conecta Persona 3 en su formulario

El botón de consulta manual ya está conectado en frmMembresiasPagos. Si Persona 3
necesita abrir el mismo listado desde otro lugar, puede usar:

```vb
Using formulario As New frmRecibos(idMembresia)
    formulario.ShowDialog(Me)
End Using
```

Para imprimir automáticamente después de confirmar un pago:

```vb
If chkImprimirAlRegistrar.Checked Then
    frmRecibos.ImprimirPago(idPagoGuardado, Me)
End If
```

`idPagoGuardado` debe ser el identificador que devuelve la inserción confirmada.
No calcular MAX(id_pago) ni volver a insertar para conseguir un identificador.
Si no se puede imprimir, el método devuelve False y muestra un aviso; no repite
ni revierte un pago confirmado. El CheckBox de impresión automática y la llamada
después del pago siguen pendientes del registro que completa Persona 3.
El PagoDAO actual devuelve Boolean: todavía no entrega el identificador del pago.

## Comprobar la instalación

Cuando Persona 2 entregue las vistas y el usuario, abrir en HeidiSQL la conexión
de gym_reportes y ejecutar `database/comprobar_reportes_persona1.sql`.
El script muestra la cuenta y sus permisos, y comprueba los nombres de columnas
de las dos vistas sin recuperar filas personales. No crea ni modifica nada.
Un error de vista o columna indica que falta ajustar el contrato; un error de
acceso requiere revisar los permisos de esa cuenta.

El script no demuestra por sí solo que los saldos sean correctos ni reemplaza
las pruebas de pagos parciales, anulaciones y membresías sin pagos.

## Permisos comunes

- Los nuevos formularios y consultas deben llamar a PermisosReportes.Exigir
  con el código R01 a R09 antes de obtener datos o generar documentos.
- R05 usa ObtenerSocioPermitido: para Socio toma el vínculo vigente de su cuenta
  y rechaza un identificador ajeno.
- R06 usa ObtenerInstructorPermitido: para Instructor toma su vínculo vigente;
  los roles que pueden consultar todos aceptan cero como filtro general.
- La autorización comprueba en la base que la cuenta siga activa, no bloqueada
  y con el mismo rol. Si el rol cambió se exige iniciar sesión otra vez.
- R01 vuelve a consultar permiso y pago en BeginPrint, también si se imprime
  desde la barra de la vista previa. Así se detecta una anulación o desactivación
  ocurrida después de seleccionar el recibo.

## Comprobaciones realizadas

Se utilizaron datos ficticios y un servidor MariaDB separado de la base local.
Se comprobaron los cuatro roles, vínculos ausentes y cambiados, cuentas bloqueadas
y desactivadas, cambio de rol, fechas, consultas parametrizadas, pagos parciales,
saldo pagado, anulaciones, montos en letras y nombres largos.

La integración del botón de membresías pasó 33 comprobaciones adicionales con
datos ficticios: historial anterior al mes actual, filtro por membresía,
ausencia de pagos, separación entre R01 y R02 para estados no vigentes,
permisos por rol, revocación de cuenta y script de comprobación con gym_reportes.
Consultar el historial no insertó pagos ni membresías.

PrintDocument crea una página con papel personalizado de 315 centésimas de
pulgada de ancho, aproximadamente 80 mm, y altura calculada según el contenido.
La vista previa y la impresión usan el mismo dibujo. El controlador de cada
impresora debe admitir ese tamaño; Microsoft Print to PDF puede guardarlo en
papel Carta aunque la vista previa muestre el ticket de 80 mm.
