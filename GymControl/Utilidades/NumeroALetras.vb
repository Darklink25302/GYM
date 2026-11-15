Option Strict On
Option Infer On

Public Module NumeroALetras
    Public Function Convertir(monto As Decimal) As String
        If monto < 0D Then Throw New ArgumentException("El monto no puede ser negativo.")
        monto = Decimal.Round(monto, 2, MidpointRounding.AwayFromZero)
        If monto < 0D OrElse monto > 99999999.99D Then
            Throw New ArgumentException("El monto debe estar entre 0 y 99,999,999.99 córdobas.")
        End If
        Dim entero = CInt(Decimal.Truncate(monto))
        Dim centavos = CInt((monto - entero) * 100D)
        Dim texto = Apocopar(EnteroEnLetras(entero))
        If entero >= 1000000 AndAlso entero Mod 1000000 = 0 Then texto &= " de"
        texto &= If(entero = 1, " córdoba", " córdobas")
        If centavos = 0 Then Return texto & " netos"
        Return texto & " con " & Apocopar(EnteroEnLetras(centavos)) & If(centavos = 1, " centavo", " centavos")
    End Function

    Private Function Apocopar(texto As String) As String
        If texto.EndsWith("veintiuno", StringComparison.Ordinal) Then Return texto.Substring(0, texto.Length - 9) & "veintiún"
        If texto.EndsWith("uno", StringComparison.Ordinal) Then Return texto.Substring(0, texto.Length - 3) & "un"
        Return texto
    End Function

    Private Function MenorDeCien(numero As Integer) As String
        Dim unidades = {"cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno", "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve"}
        If numero < 30 Then Return unidades(numero)
        Dim decenas = {"", "", "", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"}
        Return decenas(numero \ 10) & If(numero Mod 10 = 0, "", " y " & unidades(numero Mod 10))
    End Function

    Private Function MenorDeMil(numero As Integer) As String
        If numero < 100 Then Return MenorDeCien(numero)
        If numero = 100 Then Return "cien"
        Dim centenas = {"", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"}
        Return centenas(numero \ 100) & If(numero Mod 100 = 0, "", " " & MenorDeCien(numero Mod 100))
    End Function

    Private Function EnteroEnLetras(numero As Integer) As String
        If numero = 0 Then Return "cero"
        Dim millones = numero \ 1000000
        Dim miles = (numero \ 1000) Mod 1000
        Dim resto = numero Mod 1000
        Dim texto = ""
        If millones > 0 Then
            texto = If(millones = 1, "un millón", Apocopar(MenorDeMil(millones)) & " millones")
        End If
        If miles > 0 Then
            texto &= " " & If(miles = 1, "mil", Apocopar(MenorDeMil(miles)) & " mil")
        End If
        If resto > 0 Then texto &= " " & MenorDeMil(resto)
        Return texto.Trim()
    End Function
End Module
