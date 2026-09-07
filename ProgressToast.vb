
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class ProgressToast
    Inherits Form

    Private Enum DWMWINDOWATTRIBUTE
        DWMWA_WINDOW_CORNER_PREFERENCE = 33
    End Enum
    Private Enum DWM_WINDOW_CORNER_PREFERENCE
        DWMWCP_DEFAULT = 0
        DWMWCP_DONOTROUND = 1
        DWMWCP_ROUND = 2       ' Standard Win11 rounded corners
        DWMWCP_ROUNDSMALL = 3  ' Slightly smaller corner radius
    End Enum
    Private LblTitle As Label
    Private LblStatus As Label
    Private ProgressBarToast As ProgressBar

    Protected Overrides ReadOnly Property CreateParams As CreateParams ' Adds native Windows drop shadow
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Friend Sub New(title As String)
        InitializeToastUI(title)
    End Sub
    Protected Overrides Sub OnHandleCreated(e As EventArgs) ' Tells Windows 11 DWM to round the window corners
        MyBase.OnHandleCreated(e)
        Try
            Dim preference As Integer = CInt(DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND)
            Skye.WinAPI.DwmSetWindowAttribute(Me.Handle, CInt(DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE), preference, Marshal.SizeOf(preference))
        Catch
            ' Graceful fallback on older Windows versions
        End Try
    End Sub
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        PositionBottomRight()
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs) ' Optional: Draws a 1px border so borderless toast doesn't blend into white backgrounds
        MyBase.OnPaint(e)
        Using p As New Pen(Color.FromArgb(220, 220, 220), 1)
            e.Graphics.DrawRectangle(p, 0, 0, Me.Width - 1, Me.Height - 1)
        End Using
    End Sub

    Private Sub InitializeToastUI(title As String)
        ' Window Setup
        Me.FormBorderStyle = FormBorderStyle.None
        Me.ControlBox = False
        Me.StartPosition = FormStartPosition.Manual
        Me.Size = New Size(340, 95)
        Me.ShowInTaskbar = False
        Me.TopMost = True

        ' App Icon or Title
        LblTitle = New Label With {
            .Text = title,
            .Font = New Font(App.MenuFont, FontStyle.Bold),
            .Location = New Point(12, 10),
            .Size = New Size(316, 20),
            .AutoEllipsis = True
        }

        ' Real-time Status Text
        LblStatus = New Label With {
            .Text = "Preparing...",
            .Font = App.MenuFont,
            .Location = New Point(12, 32),
            .Size = New Size(316, 22),
            .AutoEllipsis = True
        }

        ' Progress Bar
        ProgressBarToast = New ProgressBar With {
            .Location = New Point(12, 60),
            .Size = New Size(312, 18),
            .Style = ProgressBarStyle.Blocks,
            .Minimum = 0,
            .Maximum = 100,
            .Value = 0
        }

        Me.Controls.AddRange({LblTitle, LblStatus, ProgressBarToast})
        Skye.UI.ThemeManager.ApplyTheme(Me)
    End Sub
    Private Sub PositionBottomRight()
        ' Places window 20px off the bottom-right edge of the primary working area (above taskbar)
        Dim wa = Screen.PrimaryScreen.WorkingArea
        Dim x = wa.Right - Me.Width - 20
        Dim y = wa.Bottom - Me.Height - 20
        Me.Location = New Point(x, y)
    End Sub

    ''' <summary>
    ''' Safely updates the status label and progress bar from any thread.
    ''' </summary>
    Friend Sub UpdateProgress(info As App.ProgressInfo)
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() UpdateProgress(info))
            Return
        End If

        If Not String.IsNullOrEmpty(info.Message) Then
            LblStatus.Text = info.Message
        End If

        If info.TotalCount > 0 Then
            Dim percentage As Integer = CInt((info.CurrentIndex / info.TotalCount) * 100)
            ProgressBarToast.Value = Math.Min(100, Math.Max(0, percentage))
        End If
    End Sub

End Class
