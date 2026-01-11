
Imports Skye.UI

Public Class Log

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private LogSearchTitle As String
    Private DeleteLogConfirm As Boolean = False
    Private WithEvents TimerDeleteLog As New Timer

    ' Form Events
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipAlert)
        Skye.UI.ThemeManager.ApplyToTooltip(TipLog)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        Text = App.GetAppTitle + " Log"
        RTBCMLog.Font = App.MenuFont
        LogSearchTitle = TxBxSearch.Text
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
        If Not TxBxSearch.Focused Then
            If e.KeyData = Keys.Escape Then Me.Close()
        End If
    End Sub
    Private Sub Log_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ToggleMaximized()
    End Sub

    ' Control Events
    Private Sub RTBLog_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles RTBLog.PreviewKeyDown
        RTBCMLog.ShortcutKeys(CType(sender, RichTextBox), e)
    End Sub
    Private Sub LBLLogInfo_DoubleClick(sender As Object, e As EventArgs) Handles LBLLogInfo.DoubleClick
        App.OpenFileLocation(App.LogPath)
    End Sub
    Private Sub BTNOK_Click(sender As Object, e As EventArgs) Handles BTNOK.Click
        Close()
    End Sub
    Private Sub BTNRefreshLog_Click(sender As Object, e As EventArgs) Handles BTNRefreshLog.Click
        SetDeleteLogConfirm(True)
        App.ShowLog(True)
    End Sub
    Private Sub BTNDeleteLog_Click(sender As Object, e As EventArgs) Handles BTNDeleteLog.Click
        If DeleteLogConfirm Then
            App.DeleteLog()
        End If
        SetDeleteLogConfirm()
    End Sub
    Private Sub TxBxSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxBxSearch.KeyPress
        Select Case e.KeyChar
            Case Convert.ToChar(Keys.Escape)
                ResetRTBLogFind()
                RTBLog.Focus()
                e.Handled = True
        End Select
    End Sub
    Private Sub TxBxSearch_Enter(sender As Object, e As EventArgs) Handles TxBxSearch.Enter
        If TxBxSearch.Text = LogSearchTitle Then TxBxSearch.ResetText()
    End Sub
    Private Sub TxBxSearch_Leave(sender As Object, e As EventArgs) Handles TxBxSearch.Leave
        TxBxSearch.Text = LogSearchTitle
        TxBxSearch.ForeColor = Skye.UI.ThemeManager.CurrentTheme.TextFore
    End Sub
    Private Sub TxBxSearch_TextChanged(sender As Object, e As EventArgs) Handles TxBxSearch.TextChanged
        If TxBxSearch.Text Is String.Empty Or RTBLog.Focused Then
            ResetRTBLogFind()
        ElseIf TxBxSearch.Text.Length <= 4 Then
            TxBxSearch.ForeColor = Skye.UI.ThemeManager.CurrentTheme.TextFore
            ResetRTBLogFind()
        ElseIf Not TxBxSearch.Text = LogSearchTitle AndAlso TxBxSearch.Text.Length > 4 AndAlso IsHandleCreated Then
            'Debug.Print("Searching Log...")
            LblStatus.Visible = True
            LblStatus.Refresh()
            Dim foundindex As Integer
            Dim searchtext As String = RTBLog.Text
            ResetRTBLogFind()
            'Try To Find First Occurrence
            foundindex = searchtext.IndexOf(TxBxSearch.Text, 0, StringComparison.CurrentCultureIgnoreCase)
            If foundindex < 0 Then
                TxBxSearch.ForeColor = Color.Red
            Else
                TxBxSearch.ForeColor = Skye.UI.ThemeManager.CurrentTheme.TextFore
                RTBLog.Select(foundindex, 0)
                RTBLog.ScrollToCaret()
            End If
            Do Until foundindex < 0
                'Highlight Current Match
                RTBLog.SelectionStart = foundindex
                RTBLog.SelectionLength = TxBxSearch.Text.Length
                RTBLog.SelectionBackColor = Color.FromArgb(180, 210, 255)
                RTBLog.SelectionColor = Color.Black
                'Try To Find Next Occurrence
                foundindex = searchtext.IndexOf(TxBxSearch.Text, foundindex + TxBxSearch.Text.Length, StringComparison.CurrentCultureIgnoreCase)
            Loop
            LblStatus.Visible = False
        End If
    End Sub

    ' Handlers
    Private Sub TimerDeleteLog_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerDeleteLog.Tick
        SetDeleteLogConfirm()
    End Sub
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipAlert)
        Skye.UI.ThemeManager.ApplyToTooltip(TipLog)
    End Sub

    ' Methods
    Private Sub ToggleMaximized()
        Select Case WindowState
            Case FormWindowState.Normal, FormWindowState.Minimized
                WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                WindowState = FormWindowState.Normal
        End Select
    End Sub
    Private Sub ResetRTBLogFind()
        RTBLog.SelectAll()
        RTBLog.SelectionBackColor = RTBLog.BackColor
        RTBLog.SelectionColor = RTBLog.ForeColor
        RTBLog.DeselectAll()
        RTBLog.SelectionStart = RTBLog.TextLength
        RTBLog.SelectionLength = 0
        RTBLog.ScrollToCaret()
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
