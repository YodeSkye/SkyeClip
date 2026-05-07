
Imports Skye.UI

Public Class Log

    ' DECLARATIONS
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private DeleteLogConfirm As Boolean = False
    Private WithEvents TimerDeleteLog As New Timer

    ' FORM EVENTS
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Skye.UI.ThemeManager.RegisterComponent(TipLog)
        Skye.UI.ThemeManager.RegisterComponent(TipAlert)
        Skye.UI.ThemeManager.ApplyTheme(Me)

        Text = App.GetAppTitle + " Log"
        LBLLogInfo.Text = Skye.Common.Log.LogFilePath
        RTBCMLog.Font = App.MenuFont
        TimerDeleteLog.Interval = 5000
    End Sub
    Private Sub Log_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        App.FrmLog?.Dispose()
        App.FrmLog = Nothing
    End Sub
    Private Sub Log_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LBLLogInfo.MouseDown
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
    Private Sub Log_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LBLLogInfo.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Log_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LBLLogInfo.MouseUp
        mMove = False
    End Sub
    Private Sub Log_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then CheckMove(Location)
    End Sub
    Private Sub Log_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub
    Private Sub Log_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    ' CONTROL EVENTS
    Private Sub LBLLogInfo_DoubleClick(sender As Object, e As EventArgs) Handles LBLLogInfo.DoubleClick
        Skye.Common.Log.OpenLocation()
    End Sub
    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        Close()
    End Sub
    Private Sub BTNRefreshLog_Click(sender As Object, e As EventArgs)
        SetDeleteLogConfirm(True)
        ShowLog(True)
    End Sub
    Private Sub BTNDeleteLog_Click(sender As Object, e As EventArgs) Handles BTNDeleteLog.Click
        If DeleteLogConfirm Then
            Skye.Common.Log.Clear()
            LogViewer.RefreshContent()
        End If
        SetDeleteLogConfirm()
    End Sub

    ' HANDLERS
    Private Sub TimerDeleteLog_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerDeleteLog.Tick
        SetDeleteLogConfirm()
    End Sub

    ' METHODS
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub SetDeleteLogConfirm(Optional forcereset As Boolean = False)
        If DeleteLogConfirm Or forcereset Then
            TimerDeleteLog.Stop()
            DeleteLogConfirm = False
            Me.BTNDeleteLog.ResetBackColor()
            TipAlert.HideTooltip()
        Else
            DeleteLogConfirm = True
            Me.BTNDeleteLog.BackColor = Color.Red
            TipLog.HideTooltip()
            TipAlert.ShowTooltipAtCursor("Are You Sure?", SystemIcons.Error.ToBitmap)
            TimerDeleteLog.Start()
        End If
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
