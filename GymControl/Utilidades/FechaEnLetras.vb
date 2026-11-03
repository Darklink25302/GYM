Option Strict On

Public Module FechaEnLetras
    Public Function Convertir(fecha As Date) As String
        Dim meses = {"enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"}
        Dim dia = Numero(fecha.Day)
        If fecha.Day = 1 Then
            Return "al día uno del mes de " & meses(fecha.Month - 1) & " del año " & Numero(fecha.Year)
        End If
        Return "a los " & dia & " días del mes de " & meses(fecha.Month - 1) & " del año " & Numero(fecha.Year)
    End Function

    Private Function Numero(valor As Integer) As String
        Dim miles = valor \ 1000
        Dim resto = valor Mod 1000
        Dim texto = ""
        If miles = 1 Then texto = "mil"
        If miles > 1 Then texto = MenorDeMil(miles) & " mil"
        If resto > 0 Then texto &= " " & MenorDeMil(resto)
        Return texto.Trim()
    End Function

    Private Function MenorDeMil(valor As Integer) As String
        Dim primeros = {"cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte", "veintiuno", "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve"}
        If valor < 30 Then Return primeros(valor)
        Dim decenas = {"", "", "", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"}
        If valor < 100 Then
            Return decenas(valor \ 10) & If(valor Mod 10 = 0, "", " y " & primeros(valor Mod 10))
        End If
        If valor = 100 Then Return "cien"
        Dim centenas = {"", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"}
        Dim texto = centenas(valor \ 100)
        Dim resto = valor Mod 100
        If resto = 0 Then Return texto
        If resto < 30 Then Return texto & " " & primeros(resto)
        Return texto & " " & decenas(resto \ 10) & If(resto Mod 10 = 0, "", " y " & primeros(resto Mod 10))
    End Function
End Module
