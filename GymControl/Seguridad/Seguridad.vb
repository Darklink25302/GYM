Imports System.Security.Cryptography

Public NotInheritable Class Seguridad

    Private Const IteracionesNuevas As Integer = 600000
    Private Const BytesSal As Integer = 16
    Private Const BytesHash As Integer = 32

    Private Sub New()
    End Sub

    Public Shared Function CrearHash(
        contrasena As String
    ) As (Hash As String, Sal As String, Iteraciones As Integer)

        If String.IsNullOrEmpty(contrasena) Then
            Throw New ArgumentException("La contraseña no puede estar vacía.")
        End If

        Dim sal = RandomNumberGenerator.GetBytes(BytesSal)
        Dim hash = Rfc2898DeriveBytes.Pbkdf2(
            contrasena, sal, IteracionesNuevas,
            HashAlgorithmName.SHA256, BytesHash
        )

        Return (
            Convert.ToBase64String(hash),
            Convert.ToBase64String(sal),
            IteracionesNuevas
        )
    End Function

    Public Shared Function VerificarContrasena(
        contrasena As String,
        hashGuardado As String,
        salGuardada As String,
        iteraciones As Integer
    ) As Boolean

        If String.IsNullOrEmpty(contrasena) OrElse
           String.IsNullOrEmpty(hashGuardado) OrElse
           String.IsNullOrEmpty(salGuardada) OrElse
           iteraciones <= 0 Then
            Return False
        End If

        Try
            Dim sal = Convert.FromBase64String(salGuardada)
            Dim hashOriginal = Convert.FromBase64String(hashGuardado)

            If sal.Length <> BytesSal OrElse
               hashOriginal.Length <> BytesHash Then
                Return False
            End If

            Dim hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                contrasena, sal, iteraciones,
                HashAlgorithmName.SHA256, BytesHash
            )

            Return CryptographicOperations.FixedTimeEquals(
                hashOriginal, hashCalculado
            )
        Catch ex As FormatException
            Return False
        End Try
    End Function

End Class