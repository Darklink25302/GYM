# Datos y conexiones del recibo R01

El recibo se consulta desde Reportes > R01. Primero se busca y selecciona un pago;
después se abre la vista previa o se elige una impresora. Reimprimir no registra
otro pago ni cambia la base.

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

Para consultar recibos de una membresía seleccionada:

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
ni revierte un pago confirmado. Estos botones y el CheckBox se conectan en el
formulario de Persona 3 cuando complete el registro de pagos.

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

PrintDocument crea una página con papel personalizado de 315 centésimas de
pulgada de ancho, aproximadamente 80 mm, y altura calculada según el contenido.
La vista previa y la impresión usan el mismo dibujo. El controlador de cada
impresora debe admitir ese tamaño; Microsoft Print to PDF puede guardarlo en
papel Carta aunque la vista previa muestre el ticket de 80 mm.
