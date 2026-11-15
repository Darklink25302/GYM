Option Strict On
Option Explicit On

Namespace My.Resources
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "10.0.0.0"),
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
     Global.Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute(),
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>
    Friend Module Resources
        Private resourceMan As Global.System.Resources.ResourceManager
        Private resourceCulture As Global.System.Globalization.CultureInfo

        Friend ReadOnly Property ResourceManager As Global.System.Resources.ResourceManager
            Get
                If resourceMan Is Nothing Then
                    resourceMan = New Global.System.Resources.ResourceManager("GymControl.Resources", GetType(Resources).Assembly)
                End If
                Return resourceMan
            End Get
        End Property

        Friend Property Culture As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(value As Global.System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property logo_uni_azul As Global.System.Drawing.Bitmap
            Get
                Return DirectCast(ResourceManager.GetObject("logo_uni_azul", resourceCulture), Global.System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property logo_uni_blanco As Global.System.Drawing.Bitmap
            Get
                Return DirectCast(ResourceManager.GetObject("logo_uni_blanco", resourceCulture), Global.System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
