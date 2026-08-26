
Imports System.ComponentModel

Public Class ClipboardWatcher
    Inherits NativeWindow
    Implements IDisposable

    Public Event ClipboardChanged()
    Private ReadOnly hiddenForm As Form
    Private disposedValue As Boolean

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
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = Skye.WinAPI.WM_CLIPBOARDUPDATE Then
            RaiseEvent ClipboardChanged()
        End If

        MyBase.WndProc(m)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            ' Clean up unmanaged clipboard listener registration
            If Handle <> IntPtr.Zero Then
                Skye.WinAPI.RemoveClipboardFormatListener(Handle)
                ReleaseHandle()
            End If

            If disposing Then
                ' Clean up managed resources
                hiddenForm?.Dispose()
            End If

            disposedValue = True
        End If
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub

End Class
