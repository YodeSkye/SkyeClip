
Imports Skye.UI

Public Class ChangeLog

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    'Form Events
    Private Sub ChangeLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = "What's New In " & App.GetAppTitle
        RTBoxChangeLog.Rtf = My.Resources.ChangeLogRT
        If Skye.UI.ThemeManager.CurrentTheme IsNot Skye.UI.SkyeThemes.Light Then Skye.UI.ThemeManager.ApplyTheme(Me)
    End Sub
    Private Sub ChangeLog_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub ChangeLog_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub ChangeLog_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub ChangeLog_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub ChangeLogKey_Down(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub

    'Procedures
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
