Public Class DatosRecibo
    Public Property IdPago As Integer
    Public Property IdMembresia As Integer
    Public Property Fecha As Date
    Public Property Socio As String = ""
    Public Property Cedula As String = ""
    Public Property Plan As String = ""
    Public Property Inicio As Date
    Public Property Vence As Date
    Public Property Monto As Decimal
    Public Property Metodo As String = ""
    Public Property Saldo As Decimal
    Public Property Anulado As Boolean
    Public Property Usuario As String = ""
End Class
