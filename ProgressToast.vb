
Imports System.Drawing
Imports System.Windows.Forms

Public Class ProgressToast
    Inherits Form

    Private LblTitle As Label
    Private LblStatus As Label
    Private ProgressBarToast As ProgressBar

    Friend Sub New(title As String)
        InitializeToastUI(title)
    End Sub
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        PositionBottomRight()
    End Sub

    Private Sub InitializeToastUI(title As String)
        ' Window Setup
        Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
        Me.ControlBox = False
        Me.StartPosition = FormStartPosition.Manual
        Me.Size = New Size(340, 95)
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.BackColor = Color.FromArgb(245, 245, 245)

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
            .Size = New Size(300, 18),
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
