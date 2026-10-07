Imports System.IO
Imports System.Text.Json
Imports MySqlConnector

Public Class ConexionBD

    Public Shared Function CrearConexion() As MySqlConnection
        Dim ruta = Path.Combine(
            AppContext.BaseDirectory,
            "conexion.local.json"
        )

        If Not File.Exists(ruta) Then
            Throw New FileNotFoundException(
                "Falta el archivo conexion.local.json."
            )
        End If

        Using documento = JsonDocument.Parse(File.ReadAllText(ruta))
            Dim datos = documento.RootElement

            Dim opciones As New MySqlConnectionStringBuilder With {
                .Server = datos.GetProperty("Servidor").GetString(),
                .Port = datos.GetProperty("Puerto").GetUInt32(),
                .Database = datos.GetProperty("BaseDatos").GetString(),
                .UserID = datos.GetProperty("Usuario").GetString(),
                .Password = datos.GetProperty("Clave").GetString()
            }

            Return New MySqlConnection(opciones.ConnectionString)
        End Using
    End Function

End Class