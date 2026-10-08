Option Strict On
Option Infer On

Imports System.Data
Imports MySqlConnector

Public NotInheritable Class UsuarioDAO
    Private Const MaximoIntentos As Integer = 3
    ' Comprueba una clave también cuando el usuario no existe.
    Private Shared ReadOnly HashFicticio As (Hash As String, Sal As String, Iteraciones As Integer) =
        Seguridad.CrearHash(Guid.NewGuid().ToString("N"))

    Private Sub New()
    End Sub

    Public Shared Function NecesitaAdministradorInicial() As Boolean
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand("SELECT COUNT(*) FROM usuarios", conexion)
                Return Convert.ToInt64(comando.ExecuteScalar()) = 0
            End Using
        End Using
    End Function

    Public Shared Function ObtenerRoles() As DataTable
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using comando As New MySqlCommand(
                "SELECT id_rol, nombre FROM roles ORDER BY id_rol", conexion)
                Using lector = comando.ExecuteReader()
                    Dim tabla As New DataTable()
                    tabla.Load(lector)
                    Return tabla
                End Using
            End Using
        End Using
    End Function

    ' Crea el administrador solo si todavía no hay usuarios.
    Public Shared Function CrearPrimerAdministrador(
        nombreUsuario As String, contrasena As String
    ) As Integer
        Dim nombre = ValidarNombre(nombreUsuario)
        If String.IsNullOrEmpty(contrasena) OrElse contrasena.Length < 12 Then
            Throw New ArgumentException("Usa una contrasena de al menos 12 caracteres.")
        End If
        Dim datos = Seguridad.CrearHash(contrasena)

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                ' Evita crear dos primeros administradores al mismo tiempo.
                Dim idRol As Integer
                Using comando As New MySqlCommand(
                    "SELECT id_rol FROM roles WHERE nombre = @rol FOR UPDATE",
                    conexion, transaccion)
                    comando.Parameters.AddWithValue("@rol", "Administrador")
                    Dim valor = comando.ExecuteScalar()
                    If valor Is Nothing OrElse valor Is DBNull.Value Then
                        Throw New InvalidOperationException("Falta el rol Administrador. Ejecuta el script de seguridad.")
                    End If
                    idRol = Convert.ToInt32(valor)
                End Using
                Using comando As New MySqlCommand(
                    "SELECT COUNT(*) FROM usuarios", conexion, transaccion)
                    If Convert.ToInt64(comando.ExecuteScalar()) <> 0 Then
                        Throw New InvalidOperationException("La base ya tiene usuarios. Usa la administracion de usuarios.")
                    End If
                End Using
                Using comando As New MySqlCommand(
                    "INSERT INTO usuarios " &
                    "(nombre_usuario, clave_hash, clave_sal, iteraciones, id_rol) " &
                    "VALUES (@nombre, @hash, @sal, @iteraciones, @rol)",
                    conexion, transaccion)
                    comando.Parameters.AddWithValue("@nombre", nombre)
                    comando.Parameters.AddWithValue("@hash", datos.Hash)
                    comando.Parameters.AddWithValue("@sal", datos.Sal)
                    comando.Parameters.AddWithValue("@iteraciones", datos.Iteraciones)
                    comando.Parameters.AddWithValue("@rol", idRol)
                    comando.ExecuteNonQuery()
                    Dim id = Convert.ToInt32(comando.LastInsertedId)
                    transaccion.Commit()
                    Return id
                End Using
            End Using
        End Using
    End Function

    ' Verifica la contraseña y guarda el resultado del acceso.
    Public Shared Function Autenticar(
        nombreUsuario As String, contrasena As String
    ) As ResultadoAutenticacion
        Dim nombre = If(nombreUsuario, "").Trim()
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                Dim usuarioEncontrado As Usuario = Nothing
                Dim hash As String = Nothing
                Dim sal As String = Nothing
                Dim iteraciones As Integer

                Using comando As New MySqlCommand(
                    "SELECT u.id_usuario, u.nombre_usuario, u.id_rol, r.nombre AS rol, " &
                    "u.id_socio, u.id_instructor, u.activo, u.bloqueado, " &
                    "u.intentos_fallidos, u.clave_hash, u.clave_sal, u.iteraciones " &
                    "FROM usuarios u INNER JOIN roles r ON r.id_rol = u.id_rol " &
                    "WHERE u.nombre_usuario = @nombre FOR UPDATE",
                    conexion, transaccion)
                    comando.Parameters.AddWithValue("@nombre", nombre)
                    Using lector = comando.ExecuteReader()
                        If lector.Read() Then
                            usuarioEncontrado = New Usuario With {
                                .IdUsuario = Convert.ToInt32(lector("id_usuario")),
                                .NombreUsuario = Convert.ToString(lector("nombre_usuario")),
                                .IdRol = Convert.ToInt32(lector("id_rol")),
                                .Rol = Convert.ToString(lector("rol")),
                                .IdSocio = EnteroOpcional(lector("id_socio")),
                                .IdInstructor = EnteroOpcional(lector("id_instructor")),
                                .Activo = Convert.ToBoolean(lector("activo")),
                                .Bloqueado = Convert.ToBoolean(lector("bloqueado")),
                                .IntentosFallidos = Convert.ToInt32(lector("intentos_fallidos"))
                            }
                            hash = Convert.ToString(lector("clave_hash"))
                            sal = Convert.ToString(lector("clave_sal"))
                            iteraciones = Convert.ToInt32(lector("iteraciones"))
                        End If
                    End Using
                End Using

                Dim claveCorrecta As Boolean
                If usuarioEncontrado Is Nothing Then
                    claveCorrecta = Seguridad.VerificarContrasena(
                        contrasena, HashFicticio.Hash, HashFicticio.Sal, HashFicticio.Iteraciones)
                    BitacoraDAO.Registrar(conexion, transaccion, Nothing, nombre,
                        "FALLIDO", "Credenciales rechazadas")
                    transaccion.Commit()
                    Return New ResultadoAutenticacion(EstadoAutenticacion.Rechazado)
                End If

                claveCorrecta = Seguridad.VerificarContrasena(contrasena, hash, sal, iteraciones)
                If Not usuarioEncontrado.Activo OrElse usuarioEncontrado.Bloqueado Then
                    Dim estado = EstadoAutenticacion.Rechazado
                    Dim resultado = "FALLIDO"
                    If usuarioEncontrado.Bloqueado Then
                        estado = EstadoAutenticacion.Bloqueado
                        resultado = "BLOQUEADO"
                    End If
                    BitacoraDAO.Registrar(conexion, transaccion, usuarioEncontrado.IdUsuario,
                        nombre, resultado, "Cuenta no habilitada para iniciar sesion")
                    transaccion.Commit()
                    Return New ResultadoAutenticacion(estado)
                End If

                If Not claveCorrecta Then
                    Dim intentos = Math.Min(MaximoIntentos, usuarioEncontrado.IntentosFallidos + 1)
                    Dim bloquear = intentos >= MaximoIntentos
                    ActualizarIntentos(conexion, transaccion, usuarioEncontrado.IdUsuario, intentos, bloquear)
                    Dim resultado = "FALLIDO"
                    Dim detalle = "Credenciales rechazadas"
                    If bloquear Then
                        resultado = "BLOQUEADO"
                        detalle = "Bloqueo por tres intentos fallidos"
                    End If
                    BitacoraDAO.Registrar(conexion, transaccion, usuarioEncontrado.IdUsuario,
                        nombre, resultado, detalle)
                    transaccion.Commit()
                    If bloquear Then
                        Return New ResultadoAutenticacion(EstadoAutenticacion.Bloqueado)
                    Else
                        Return New ResultadoAutenticacion(EstadoAutenticacion.Rechazado)
                    End If
                End If

                ActualizarIntentos(conexion, transaccion, usuarioEncontrado.IdUsuario, 0, False)
                BitacoraDAO.Registrar(conexion, transaccion, usuarioEncontrado.IdUsuario,
                    nombre, "EXITOSO", "Inicio de sesion correcto")
                transaccion.Commit()
                usuarioEncontrado.IntentosFallidos = 0
                Return New ResultadoAutenticacion(EstadoAutenticacion.Exitoso, usuarioEncontrado)
            End Using
        End Using
    End Function

    Private Shared Sub ActualizarIntentos(
        conexion As MySqlConnection, transaccion As MySqlTransaction,
        idUsuario As Integer, intentos As Integer, bloquear As Boolean
    )
        Using comando As New MySqlCommand(
            "UPDATE usuarios SET intentos_fallidos = @intentos, bloqueado = @bloqueado " &
            "WHERE id_usuario = @id", conexion, transaccion)
            comando.Parameters.AddWithValue("@intentos", intentos)
            comando.Parameters.AddWithValue("@bloqueado", bloquear)
            comando.Parameters.AddWithValue("@id", idUsuario)
            comando.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Function EnteroOpcional(valor As Object) As Integer?
        If valor Is DBNull.Value Then Return Nothing
        Return Convert.ToInt32(valor)
    End Function

    Private Shared Function ValidarNombre(nombreUsuario As String) As String
        Dim nombre = If(nombreUsuario, "").Trim()
        If nombre.Length = 0 OrElse nombre.Length > 50 Then
            Throw New ArgumentException("El usuario debe tener entre 1 y 50 caracteres.")
        End If
        Return nombre
    End Function

    Public Shared Function ListarUsuarios() As DataTable
        ComprobarSesionAdministrador()
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                PrepararAdministracion(conexion, transaccion)
                Using comando As New MySqlCommand(
                    "SELECT u.id_usuario, u.nombre_usuario, u.id_rol, r.nombre AS rol, " &
                    "u.activo, u.bloqueado, u.intentos_fallidos, u.id_socio, u.id_instructor " &
                    "FROM usuarios u INNER JOIN roles r ON r.id_rol = u.id_rol " &
                    "ORDER BY u.nombre_usuario", conexion, transaccion)
                    Dim tabla As New DataTable()
                    Using lector = comando.ExecuteReader()
                        tabla.Load(lector)
                    End Using
                    transaccion.Commit()
                    Return tabla
                End Using
            End Using
        End Using
    End Function

    Public Shared Function GuardarUsuario(usuario As Usuario, contrasena As String) As Integer
        ComprobarSesionAdministrador()
        If usuario Is Nothing Then Throw New ArgumentException("Faltan los datos del usuario.")
        Dim nombre = ValidarNombre(usuario.NombreUsuario)
        If usuario.IdUsuario < 0 OrElse usuario.IdRol <= 0 Then
            Throw New ArgumentException("Selecciona un rol válido.")
        End If
        If usuario.IdSocio.HasValue AndAlso usuario.IdSocio.Value <= 0 Then
            Throw New ArgumentException("El ID del socio debe ser positivo.")
        End If
        If usuario.IdInstructor.HasValue AndAlso usuario.IdInstructor.Value <= 0 Then
            Throw New ArgumentException("El ID del instructor debe ser positivo.")
        End If

        Dim hash As String = ""
        Dim sal As String = ""
        Dim iteraciones As Integer
        If usuario.IdUsuario = 0 Then
            ValidarContrasenaNueva(contrasena)
            Dim datos = Seguridad.CrearHash(contrasena)
            hash = datos.Hash
            sal = datos.Sal
            iteraciones = datos.Iteraciones
        End If

        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                Dim idAdministrador = PrepararAdministracion(conexion, transaccion)
                Dim nombreRol As String
                Using comando As New MySqlCommand(
                    "SELECT nombre FROM roles WHERE id_rol = @rol", conexion, transaccion)
                    comando.Parameters.AddWithValue("@rol", usuario.IdRol)
                    Dim valor = comando.ExecuteScalar()
                    If valor Is Nothing Then Throw New ArgumentException("El rol seleccionado no existe.")
                    nombreRol = Convert.ToString(valor)
                End Using

                Dim idSocio As Object = DBNull.Value
                Dim idInstructor As Object = DBNull.Value
                If nombreRol = "Socio" AndAlso usuario.IdSocio.HasValue Then
                    idSocio = usuario.IdSocio.Value
                End If
                If nombreRol = "Instructor" AndAlso usuario.IdInstructor.HasValue Then
                    idInstructor = usuario.IdInstructor.Value
                End If

                If idSocio IsNot DBNull.Value Then
                    Using comando As New MySqlCommand("SELECT COUNT(*) FROM socios WHERE id_socio = @id", conexion, transaccion)
                        comando.Parameters.AddWithValue("@id", idSocio)
                        If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
                            Throw New ArgumentException("El socio indicado no existe.")
                        End If
                    End Using
                End If
                If idInstructor IsNot DBNull.Value Then
                    Using comando As New MySqlCommand("SELECT COUNT(*) FROM instructores WHERE IdInstructor = @id", conexion, transaccion)
                        comando.Parameters.AddWithValue("@id", idInstructor)
                        If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
                            Throw New ArgumentException("El instructor indicado no existe.")
                        End If
                    End Using
                End If

                Dim consulta As String
                If usuario.IdUsuario = 0 Then
                    consulta = "INSERT INTO usuarios " &
                        "(nombre_usuario, id_rol, activo, id_socio, id_instructor, clave_hash, clave_sal, iteraciones) " &
                        "VALUES (@nombre, @rol, @activo, @socio, @instructor, @hash, @sal, @iteraciones)"
                Else
                    ComprobarEdicionAdministrador(conexion, transaccion, usuario, idAdministrador)
                    consulta = "UPDATE usuarios SET nombre_usuario = @nombre, id_rol = @rol, " &
                        "activo = @activo, id_socio = @socio, id_instructor = @instructor " &
                        "WHERE id_usuario = @id"
                End If

                Using comando As New MySqlCommand(consulta, conexion, transaccion)
                    comando.Parameters.AddWithValue("@nombre", nombre)
                    comando.Parameters.AddWithValue("@rol", usuario.IdRol)
                    comando.Parameters.AddWithValue("@activo", usuario.Activo)
                    comando.Parameters.AddWithValue("@socio", idSocio)
                    comando.Parameters.AddWithValue("@instructor", idInstructor)
                    If usuario.IdUsuario = 0 Then
                        comando.Parameters.AddWithValue("@hash", hash)
                        comando.Parameters.AddWithValue("@sal", sal)
                        comando.Parameters.AddWithValue("@iteraciones", iteraciones)
                    Else
                        comando.Parameters.AddWithValue("@id", usuario.IdUsuario)
                    End If
                    comando.ExecuteNonQuery()
                    Dim id = usuario.IdUsuario
                    If id = 0 Then id = Convert.ToInt32(comando.LastInsertedId)
                    transaccion.Commit()
                    Return id
                End Using
            End Using
        End Using
    End Function

    Public Shared Sub DesbloquearUsuario(idUsuario As Integer)
        ComprobarSesionAdministrador()
        If idUsuario <= 0 Then Throw New ArgumentException("Selecciona un usuario.")
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                PrepararAdministracion(conexion, transaccion)
                ComprobarUsuarioExistente(conexion, transaccion, idUsuario)
                ActualizarIntentos(conexion, transaccion, idUsuario, 0, False)
                transaccion.Commit()
            End Using
        End Using
    End Sub

    Public Shared Sub RestablecerContrasena(idUsuario As Integer, contrasena As String)
        ComprobarSesionAdministrador()
        If idUsuario <= 0 Then Throw New ArgumentException("Selecciona un usuario.")
        ValidarContrasenaNueva(contrasena)
        Dim datos = Seguridad.CrearHash(contrasena)
        Using conexion = ConexionBD.CrearConexion()
            conexion.Open()
            Using transaccion = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
                PrepararAdministracion(conexion, transaccion)
                ComprobarUsuarioExistente(conexion, transaccion, idUsuario)
                Using comando As New MySqlCommand(
                    "UPDATE usuarios SET clave_hash = @hash, clave_sal = @sal, iteraciones = @iteraciones " &
                    "WHERE id_usuario = @id", conexion, transaccion)
                    comando.Parameters.AddWithValue("@hash", datos.Hash)
                    comando.Parameters.AddWithValue("@sal", datos.Sal)
                    comando.Parameters.AddWithValue("@iteraciones", datos.Iteraciones)
                    comando.Parameters.AddWithValue("@id", idUsuario)
                    comando.ExecuteNonQuery()
                End Using
                transaccion.Commit()
            End Using
        End Using
    End Sub

    Private Shared Sub ComprobarSesionAdministrador()
        If Sesion.IdUsuario <= 0 OrElse Sesion.Rol <> "Administrador" Then
            Throw New UnauthorizedAccessException("Solo el administrador puede gestionar usuarios.")
        End If
    End Sub

    Private Shared Function PrepararAdministracion(
        conexion As MySqlConnection, transaccion As MySqlTransaction
    ) As Integer
        ' Coordina los cambios para conservar un administrador habilitado.
        Dim idRol As Integer
        Using comando As New MySqlCommand(
            "SELECT id_rol FROM roles WHERE nombre = @rol FOR UPDATE", conexion, transaccion)
            comando.Parameters.AddWithValue("@rol", "Administrador")
            Dim valor = comando.ExecuteScalar()
            If valor Is Nothing Then Throw New InvalidOperationException("Falta el rol Administrador.")
            idRol = Convert.ToInt32(valor)
        End Using
        Using comando As New MySqlCommand(
            "SELECT id_usuario FROM usuarios WHERE id_usuario = @id AND id_rol = @rol " &
            "AND activo = TRUE AND bloqueado = FALSE FOR UPDATE", conexion, transaccion)
            comando.Parameters.AddWithValue("@id", Sesion.IdUsuario)
            comando.Parameters.AddWithValue("@rol", idRol)
            If comando.ExecuteScalar() Is Nothing Then
                Throw New UnauthorizedAccessException("Tu cuenta ya no tiene permiso para gestionar usuarios.")
            End If
        End Using
        Return idRol
    End Function

    Private Shared Sub ComprobarEdicionAdministrador(
        conexion As MySqlConnection, transaccion As MySqlTransaction,
        usuario As Usuario, idAdministrador As Integer
    )
        ComprobarUsuarioExistente(conexion, transaccion, usuario.IdUsuario)
        If usuario.IdUsuario = Sesion.IdUsuario AndAlso
            (Not usuario.Activo OrElse usuario.IdRol <> idAdministrador) Then
            Throw New InvalidOperationException("No puedes inactivar tu propia cuenta ni quitarte el rol de administrador.")
        End If
        If usuario.Activo AndAlso usuario.IdRol = idAdministrador Then Return

        Dim esAdministrador As Boolean
        Using comando As New MySqlCommand(
            "SELECT id_rol FROM usuarios WHERE id_usuario = @id", conexion, transaccion)
            comando.Parameters.AddWithValue("@id", usuario.IdUsuario)
            esAdministrador = Convert.ToInt32(comando.ExecuteScalar()) = idAdministrador
        End Using
        If Not esAdministrador Then Return
        Using comando As New MySqlCommand(
            "SELECT COUNT(*) FROM usuarios WHERE id_rol = @rol AND activo = TRUE " &
            "AND bloqueado = FALSE AND id_usuario <> @id", conexion, transaccion)
            comando.Parameters.AddWithValue("@rol", idAdministrador)
            comando.Parameters.AddWithValue("@id", usuario.IdUsuario)
            If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
                Throw New InvalidOperationException("Debe quedar al menos un administrador habilitado.")
            End If
        End Using
    End Sub

    Private Shared Sub ComprobarUsuarioExistente(
        conexion As MySqlConnection, transaccion As MySqlTransaction, idUsuario As Integer
    )
        Using comando As New MySqlCommand(
            "SELECT id_usuario FROM usuarios WHERE id_usuario = @id FOR UPDATE", conexion, transaccion)
            comando.Parameters.AddWithValue("@id", idUsuario)
            If comando.ExecuteScalar() Is Nothing Then
                Throw New InvalidOperationException("El usuario seleccionado ya no existe.")
            End If
        End Using
    End Sub

    Private Shared Sub ValidarContrasenaNueva(contrasena As String)
        If String.IsNullOrEmpty(contrasena) OrElse contrasena.Length < 12 Then
            Throw New ArgumentException("La contraseña debe tener al menos 12 caracteres.")
        End If
    End Sub

End Class
