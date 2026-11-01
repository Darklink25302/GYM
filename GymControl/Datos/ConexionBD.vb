Imports System.IO
Imports System.Text.Json
Imports MySqlConnector

Public Class ConexionBD

    Public Shared Function CrearConexion() As MySqlConnection
        Return LeerConexion("conexion.local.json")
    End Function

    Public Shared Function CrearConexionReportes() As MySqlConnection
        Dim conexion = LeerConexion("conexion.reportes.local.json")
        Dim opciones As New MySqlConnectionStringBuilder(conexion.ConnectionString)

        If opciones.UserID <> "gym_reportes" Then
            conexion.Dispose()
            Throw New InvalidOperationException("Los reportes deben usar el usuario gym_reportes.")
        End If

        Return conexion
    End Function

    Private Shared Function LeerConexion(nombreArchivo As String) As MySqlConnection
        Dim ruta = Path.Combine(
            AppContext.BaseDirectory,
            nombreArchivo
        )

        If Not File.Exists(ruta) Then
            Throw New FileNotFoundException(
                "Falta el archivo " & nombreArchivo & "."
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
