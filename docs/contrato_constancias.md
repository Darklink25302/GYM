# Constancia de membresía vigente R02

Administrador y Recepcionista pueden abrir Reportes > R02, o entrar directamente
a Membresías y pagos. Seleccionan una fila de la tabla y utilizan Vista previa
constancia, Configurar página o Imprimir constancia.

La fila debe estar Activa y la fecha actual debe estar entre inicio y vencimiento,
incluidos ambos días. La validación se repite en la consulta y al comenzar la
impresión, incluso desde la barra de la vista previa. No se exige saldo cero:
la constancia informa lo pactado, lo abonado y el saldo actual.

## Datos que entrega Persona 2

ConstanciaDAO usa ConexionBD.CrearConexionReportes y solo consulta
vw_saldos_membresia con el usuario gym_reportes. La vista necesita estos alias:

| Columna | Significado |
| --- | --- |
| id_membresia | Identificador de la membresía |
| socio | Nombre completo |
| cedula | Cédula del socio |
| plan | Nombre del tipo de membresía |
| inicio | Fecha de inicio |
| vence | Fecha de vencimiento |
| estado | Estado de la membresía: Activa, Cancelada, etc. |
| precio_pactado | Total acordado, tomado de la membresía |
| pagado | Suma de pagos no anulados; cero cuando no hay pagos |
| saldo | precio_pactado menos pagado |

Debe existir una sola fila por membresía, incluso cuando no tenga pagos.
precio_pactado, pagado y saldo son DECIMAL, sin valores nulos.
gym_reportes necesita SELECT sobre la vista, sin acceso directo a tablas
ni permisos de escritura. No se crea otra tabla para la constancia.

El JSON privado conexion.reportes.local.json se configura localmente con el
servidor, puerto, base, usuario gym_reportes y su contraseña. Se copia al compilar
y está excluido de Git. No se utiliza una conexión alternativa con root.

## Archivos de esta parte

- Modelos/DatosConstancia.vb: datos que se imprimen.
- Datos/ConstanciaDAO.vb: permiso y consulta parametrizada de la membresía.
- Utilidades/FechaEnLetras.vb: día, mes y año escritos en español.
- Reportes/Impresion/ImpresorConstancia.vb: dibujo, sin consultas ni controles.
- Formularios/frmMembresiasPagos.vb y su diseñador: selección y diálogos.
- Formularios/frmCentroReportes.vb: entrada a R02.
- My Project/Resources.resx y Recursos: logos azul y blanco del HTML del profesor.

La impresión es Carta vertical y usa los márgenes configurados. Si los márgenes
dejan demasiado poco espacio, se avisa en lugar de recortar el contenido. Las
fuentes y los trazos se liberan con Using; el formulario libera su documento al
cerrarse o desecharse. El logo compartido de los recursos no se desecha.

## Comprobaciones

Se realizaron 45 comprobaciones contra el proyecto, con configuración ficticia
y un servidor MariaDB separado de la instalación del usuario. Incluyen los
cuatro roles, fechas límite, estados no vigentes, pagos anulados, consulta con
parámetros, nombres largos, márgenes, una página Carta, carga del formulario,
cancelación de una membresía y desactivación de una cuenta después de preparar
el documento. También se generaron muestras de R01 y R02 con PrintDocument.

Las muestras no sustituyen la prueba de integración: debe repetirse el recorrido
con las vistas definitivas del equipo. Persona 3 conserva el registro de pagos
y debe conectar la impresión automática de R01 según contrato_recibos.md.
