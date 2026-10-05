Option Strict On
Option Infer On

Imports System.Threading.Tasks
Imports MySqlConnector

Public Class FrmUsuarios
    Private idSeleccionado As Integer
    Private procesando As Boolean
    Private cargando As Boolean

    Public Sub New()
        InitializeComponent()
        If Sesion.IdUsuario <= 0 OrElse Sesion.Rol <> "Administrador" Then
            Throw New UnauthorizedAccessException("Solo el administrador puede abrir Usuarios.")
        End If
    End Sub

    Private Async Sub FrmUsuarios_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await CargarUsuariosAsync()
    End Sub

    Private Async Function CargarUsuariosAsync(Optional idElegido As Integer = 0) As Task(Of Boolean)
        EstablecerOcupado(True)
        cargando = True
        Try
            Dim roles = Await Task.Run(Function() UsuarioDAO.ObtenerRoles())
            Dim usuarios = Await Task.Run(Function() UsuarioDAO.ListarUsuarios())
            cmbRol.DisplayMember = "nombre"
            cmbRol.ValueMember = "id_rol"
            cmbRol.DataSource = roles
            dgvUsuarios.DataSource = usuarios
            LimpiarCampos()
            cargando = False
            If idElegido > 0 Then
                For Each fila As DataGridViewRow In dgvUsuarios.Rows
                    If Convert.ToInt32(fila.Cells("id_usuario").Value) = idElegido Then
                        dgvUsuarios.CurrentCell = fila.Cells("nombre_usuario")
                        fila.Selected = True
                        MostrarSeleccion()
                        Exit For
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            MostrarError(ex)
            Return False
        Finally
            cargando = False
            EstablecerOcupado(False)
        End Try
    End Function

    Private Sub dgvUsuarios_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUsuarios.SelectionChanged
        If Not cargando AndAlso Not procesando Then MostrarSeleccion()
    End Sub

    Private Sub MostrarSeleccion()
        If dgvUsuarios.SelectedRows.Count = 0 Then Return
        Dim fila = dgvUsuarios.SelectedRows(0)
        idSeleccionado = Convert.ToInt32(fila.Cells("id_usuario").Value)
        txtUsuario.Text = Convert.ToString(fila.Cells("nombre_usuario").Value)
        cmbRol.SelectedValue = Convert.ToInt32(fila.Cells("id_rol").Value)
        chkActivo.Checked = Convert.ToBoolean(fila.Cells("activo").Value)
        nudSocio.Value = 0
        nudInstructor.Value = 0
        If Not Convert.IsDBNull(fila.Cells("id_socio").Value) Then
            nudSocio.Value = Convert.ToDecimal(fila.Cells("id_socio").Value)
        End If
        If Not Convert.IsDBNull(fila.Cells("id_instructor").Value) Then
            nudInstructor.Value = Convert.ToDecimal(fila.Cells("id_instructor").Value)
        End If
        txtContrasena.Clear()
        txtConfirmacion.Clear()
        lblEstado.Text = "Usuario seleccionado. Puedes editar sus datos o restablecer su contraseña."
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        LimpiarCampos()
        txtUsuario.Select()
    End Sub

    Private Sub LimpiarCampos()
        idSeleccionado = 0
        dgvUsuarios.ClearSelection()
        dgvUsuarios.CurrentCell = Nothing
        txtUsuario.Clear()
        cmbRol.SelectedIndex = -1
        chkActivo.Checked = True
        nudSocio.Value = 0
        nudInstructor.Value = 0
        txtContrasena.Clear()
        txtConfirmacion.Clear()
        lblEstado.Text = "Para crear un usuario, elige su rol y escribe una contraseña. Para inactivarlo, desmarca Activo y guarda."
    End Sub

    Private Sub cmbRol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRol.SelectedIndexChanged
        Dim rol As String = ""
        If cmbRol.SelectedItem IsNot Nothing Then
            rol = Convert.ToString(DirectCast(cmbRol.SelectedItem, DataRowView)("nombre"))
        End If
        nudSocio.Enabled = rol = "Socio"
        nudInstructor.Enabled = rol = "Instructor"
        If rol <> "Socio" Then nudSocio.Value = 0
        If rol <> "Instructor" Then nudInstructor.Value = 0
    End Sub

    Private Function ContrasenaValida() As Boolean
        If txtContrasena.Text.Length < 12 Then
            lblEstado.Text = "La contraseña debe tener al menos 12 caracteres."
            Return False
        End If
        If Not String.Equals(txtContrasena.Text, txtConfirmacion.Text, StringComparison.Ordinal) Then
            lblEstado.Text = "Las contraseñas no coinciden."
            Return False
        End If
        Return True
    End Function

    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If procesando Then Return
        If txtUsuario.Text.Trim().Length = 0 OrElse cmbRol.SelectedIndex < 0 Then
            lblEstado.Text = "Escribe el usuario y selecciona un rol."
            Return
        End If
        If idSeleccionado = 0 AndAlso Not ContrasenaValida() Then Return
        If idSeleccionado > 0 AndAlso (txtContrasena.Text.Length > 0 OrElse txtConfirmacion.Text.Length > 0) Then
            lblEstado.Text = "Para cambiar la contraseña de este usuario, pulsa Restablecer contraseña."
            Return
        End If
        Dim usuario As New Usuario With {
            .IdUsuario = idSeleccionado,
            .NombreUsuario = txtUsuario.Text.Trim(),
            .IdRol = Convert.ToInt32(cmbRol.SelectedValue),
            .Activo = chkActivo.Checked
        }
        If nudSocio.Value > 0 Then usuario.IdSocio = Decimal.ToInt32(nudSocio.Value)
        If nudInstructor.Value > 0 Then usuario.IdInstructor = Decimal.ToInt32(nudInstructor.Value)
        Dim clave = txtContrasena.Text
        Dim idGuardado As Integer
        EstablecerOcupado(True)
        Try
            idGuardado = Await Task.Run(Function() UsuarioDAO.GuardarUsuario(usuario, clave))
            If idGuardado = Sesion.IdUsuario Then Sesion.NombreUsuario = usuario.NombreUsuario
        Catch ex As Exception
            MostrarError(ex)
        Finally
            txtContrasena.Clear()
            txtConfirmacion.Clear()
            EstablecerOcupado(False)
        End Try
        If idGuardado > 0 Then
            If Await CargarUsuariosAsync(idGuardado) Then lblEstado.Text = "Datos guardados."
        End If
    End Sub

    Private Async Sub btnDesbloquear_Click(sender As Object, e As EventArgs) Handles btnDesbloquear.Click
        If procesando Then Return
        If idSeleccionado = 0 Then
            lblEstado.Text = "Selecciona un usuario de la lista."
            Return
        End If
        Dim id = idSeleccionado
        Dim actualizado As Boolean
        EstablecerOcupado(True)
        Try
            Await Task.Run(Sub() UsuarioDAO.DesbloquearUsuario(id))
            actualizado = True
        Catch ex As Exception
            MostrarError(ex)
        Finally
            EstablecerOcupado(False)
        End Try
        If actualizado Then
            If Await CargarUsuariosAsync(id) Then lblEstado.Text = "Usuario desbloqueado. Sus intentos vuelven a cero."
        End If
    End Sub

    Private Async Sub btnRestablecer_Click(sender As Object, e As EventArgs) Handles btnRestablecer.Click
        If procesando Then Return
        If idSeleccionado = 0 Then
            lblEstado.Text = "Selecciona un usuario de la lista."
            Return
        End If
        If Not ContrasenaValida() Then Return
        Dim id = idSeleccionado
        Dim clave = txtContrasena.Text
        Dim actualizado As Boolean
        EstablecerOcupado(True)
        Try
            Await Task.Run(Sub() UsuarioDAO.RestablecerContrasena(id, clave))
            actualizado = True
        Catch ex As Exception
            MostrarError(ex)
        Finally
            txtContrasena.Clear()
            txtConfirmacion.Clear()
            EstablecerOcupado(False)
        End Try
        If actualizado Then
            If Await CargarUsuariosAsync(id) Then lblEstado.Text = "Contraseña restablecida. Si la cuenta está bloqueada, usa Desbloquear."
        End If
    End Sub

    Private Async Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If Not procesando Then Await CargarUsuariosAsync()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub

    Private Sub EstablecerOcupado(ocupado As Boolean)
        procesando = ocupado
        pnlContenido.Enabled = Not ocupado
        UseWaitCursor = ocupado
    End Sub

    Private Sub MostrarError(ex As Exception)
        If TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ArgumentException OrElse TypeOf ex Is InvalidOperationException Then
            lblEstado.Text = ex.Message
        ElseIf TypeOf ex Is MySqlException AndAlso DirectCast(ex, MySqlException).Number = 1062 Then
            lblEstado.Text = "Ya existe un usuario con ese nombre."
        Else
            lblEstado.Text = "No se pudo completar la operación. Comprueba la conexión, los permisos y los datos."
        End If
    End Sub

    Private Sub FrmUsuarios_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If procesando Then e.Cancel = True
    End Sub
End Class
