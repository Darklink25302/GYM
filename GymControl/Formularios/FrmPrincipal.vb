Option Strict On
Option Infer On

Public Class FrmPrincipal
    Public Sub New()
        InitializeComponent()
        If Sesion.IdUsuario <= 0 Then
            Throw New InvalidOperationException("Debes iniciar sesión para abrir esta ventana.")
        End If
        ActualizarMenu()
    End Sub

    Private Sub ActualizarMenu()
        lblBienvenida.Text = "Bienvenido, " & Sesion.NombreUsuario
        lblRol.Text = "Rol: " & Sesion.Rol
        btnUsuarios.Visible = Sesion.Rol = "Administrador"
        btnSocios.Visible = Sesion.Rol = "Administrador" OrElse Sesion.Rol = "Recepcionista"
        btnTiposMembresia.Visible = Sesion.Rol = "Administrador"
        btnMembresias.Visible = Sesion.Rol = "Administrador" OrElse Sesion.Rol = "Recepcionista"
        btnInstructores.Visible = Sesion.Rol = "Administrador"
        btnActividades.Visible = Sesion.Rol = "Administrador"
        btnSalas.Visible = Sesion.Rol = "Administrador"
        btnHorarios.Visible = Sesion.Rol = "Administrador" OrElse Sesion.Rol = "Instructor"
        btnHorarios.Text = If(Sesion.Rol = "Instructor", "Mis horarios", "Horarios")
        lblAviso.Text = ""
        If Sesion.Rol = "Socio" Then
            lblAviso.Text = "El portal del socio todavía está pendiente."
        End If
    End Sub

    Private Sub MostrarError(fallo As Exception)
        If TypeOf fallo Is UnauthorizedAccessException OrElse TypeOf fallo Is ArgumentException Then
            MessageBox.Show(fallo.Message, "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            MessageBox.Show("No se pudo abrir el módulo. Comprueba la conexión y las tablas de la base de datos.", "GymControl", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        If Sesion.IdUsuario <= 0 Then Close()
    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click
        Try
            PermisosMenu.Exigir("Administrador")
            Using formulario As New FrmUsuarios()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnSocios_Click(sender As Object, e As EventArgs) Handles btnSocios.Click
        Try
            PermisosMenu.Exigir("Administrador", "Recepcionista")
            Using formulario As New frmSocios()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnTiposMembresia_Click(sender As Object, e As EventArgs) Handles btnTiposMembresia.Click
        Try
            PermisosMenu.Exigir("Administrador")
            Using formulario As New frmTiposMembresia()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnMembresias_Click(sender As Object, e As EventArgs) Handles btnMembresias.Click
        Try
            PermisosMenu.Exigir("Administrador", "Recepcionista")
            Using formulario As New frmMembresiasPagos()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnInstructores_Click(sender As Object, e As EventArgs) Handles btnInstructores.Click
        Try
            PermisosMenu.Exigir("Administrador")
            Using formulario As New frmInstructores()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnActividades_Click(sender As Object, e As EventArgs) Handles btnActividades.Click
        Try
            PermisosMenu.Exigir("Administrador")
            Using formulario As New frmActividades()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnSalas_Click(sender As Object, e As EventArgs) Handles btnSalas.Click
        Try
            PermisosMenu.Exigir("Administrador")
            Using formulario As New frmSalas()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnHorarios_Click(sender As Object, e As EventArgs) Handles btnHorarios.Click
        Try
            PermisosMenu.Exigir("Administrador", "Instructor")
            If Sesion.Rol = "Instructor" AndAlso Not Sesion.IdInstructor.HasValue Then
                Throw New ArgumentException("Un administrador debe vincular tu cuenta con un instructor.")
            End If
            Using formulario As New frmHorarios()
                formulario.ShowDialog(Me)
            End Using
            ActualizarMenu()
        Catch ex As Exception
            MostrarError(ex)
        End Try
    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Close()
    End Sub

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Sesion.Limpiar()
    End Sub
End Class
