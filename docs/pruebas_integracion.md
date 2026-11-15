# Pruebas de integración

Comprobación del 15 de noviembre de 2026, en Windows, .NET 10 y MariaDB 12.3.3.

- Compilación de la solución: sin errores ni advertencias.
- 48 comprobaciones correctas en una base aislada de datos ficticios.
- Instalación completa del esquema, cuatro vistas y permisos de la cuenta de lectura.
- Membresía y primer abono conjuntos, ID confirmado del recibo, pago adicional y hora/usuario de registro.
- Rechazo de sobrepagos, incluso con dos pagos simultáneos; anulación sin borrado y rechazo de una segunda anulación.
- R05 conserva las cuatro membresías y los siete registros del socio de prueba, incluida una membresía sin pagos y un pago anulado.
- Socio limitado a su estado de cuenta; Instructor limitado a sus horarios; cuenta de reportes sin acceso directo a tablas.
- Exportación real de los tres RDLC; R04 también admite un período sin pagos.
- Cartera Excel con tabla, fórmula de saldo y SUBTOTAL; libro anual con doce meses, totales coincidentes con MariaDB y resumen protegido.
- Informe mensual con datos consultados de la base.
- Listado de cien filas en varias páginas y reimpresión con paginación reiniciada.
- Generación de recibo, recibo anulado y constancia mediante PrintDocument.

Las muestras se generaron con datos ficticios y se revisaron visualmente. No representan cobros reales. Microsoft Print to PDF coloca el recibo en una hoja Carta; no se comprobó una impresora térmica física.

En la instalación local se ejecutó la actualización después de guardar un respaldo. Los registros de las seis tablas que ya tenían datos coinciden con el respaldo. Se comprobó la lectura de las cuatro vistas y el rechazo del acceso directo a pagos desde gym_reportes.

Antes de entregar, el equipo debe grabar su demostración y confirmar los datos pendientes de EQUIPO.md y el destino del repositorio del curso.
