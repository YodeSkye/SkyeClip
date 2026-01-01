
Public Class ClipboardWatcher
    Inherits NativeWindow

    Public Event ClipboardChanged()
    Private ReadOnly hiddenForm As Form

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = Skye.WinAPI.WM_CLIPBOARDUPDATE Then
            RaiseEvent ClipboardChanged()
        End If
        MyBase.WndProc(m)
    End Sub
    Public Sub New()
        hiddenForm = New Form With {
            .Visible = False,
            .ShowInTaskbar = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Opacity = 0
        }
        hiddenForm.CreateControl()
        AssignHandle(hiddenForm.Handle)
        Skye.WinAPI.AddClipboardFormatListener(hiddenForm.Handle)
    End Sub

End Class
