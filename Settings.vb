
Imports Skye.UI

Public Class Settings

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    ' Form Events
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipSettings)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        Text = "Settings for " & GetAppTitle()

        'Settings
        CoBoxTheme.Items.Clear()
        For Each theme In SkyeThemes.AllThemes
            CoBoxTheme.Items.Add(theme.Name)
        Next
        CoBoxTheme.SelectedItem = App.Settings.ThemeName
        ChkBoxThemeAuto.Checked = App.Settings.ThemeAuto
        ChkBoxAutoStartWithWindows.Checked = App.Settings.AutoStartWithWindows
        TxtBoxMaxClips.Text = App.Settings.MaxClips.ToString
        TxtBoxMaxClipPreviewLength.Text = App.Settings.MaxClipPreviewLength.ToString
        ChkBoxBlinkOnNewClip.Checked = App.Settings.BlinkOnNewClip
        ChkBoxNotifyOnNewClip.Checked = App.Settings.NotifyOnNewClip
        ChkBoxPlaySoundWithNotify.Checked = App.Settings.PlaySoundWithNotify
        ChkBoxShowOpenSourceApp.Checked = App.Settings.ShowOpenSourceApp
        ChkBoxKeepScratchPadText.Checked = App.Settings.ScratchPadKeepText
        TxtBoxHotKeyToggleFavorite.Text = FormatHotKey(App.Settings.HotKeys.ToggleFavorite)
        TxtBoxHotKeyShowViewer.Text = FormatHotKey(App.Settings.HotKeys.ShowViewer)
        TxtBoxHotKeyShowScratchPad.Text = FormatHotKey(App.Settings.HotKeys.ShowScratchPad)
        TxtBoxPurgeDays.Text = App.Settings.PurgeDays.ToString
        ChkBoxAutoPurge.Checked = App.Settings.AutoPurge

    End Sub
    Private Sub Settings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        App.Settings.Save()
    End Sub
    Private Sub Settings_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 9, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 9)
            End If
        End If
    End Sub
    Private Sub Settings_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub Settings_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub Settings_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub Settings_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Close()
    End Sub

    ' Handlers
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipSettings)
    End Sub

    ' Control Events
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnPurgeNow_Click(sender As Object, e As EventArgs) Handles BtnPurgeNow.Click
        Dim cutoff = DateTime.Now.AddDays(-App.Settings.PurgeDays)
        App.Tray.repo.PurgeClips(cutoff)
        App.Tray.RefreshMenu()
    End Sub
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxMaxClips.KeyDown, TxtBoxMaxClipPreviewLength.KeyDown, TxtBoxPurgeDays.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxMaxClips.KeyPress, TxtBoxMaxClipPreviewLength.KeyPress, TxtBoxPurgeDays.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxMaxClips.PreviewKeyDown, TxtBoxMaxClipPreviewLength.PreviewKeyDown, TxtBoxPurgeDays.PreviewKeyDown
        CMTxtBox.ShortcutKeys(CType(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxMaxClips_Validated(sender As Object, e As EventArgs) Handles TxtBoxMaxClips.Validated
        If Not String.IsNullOrEmpty(TxtBoxMaxClips.Text) Then
            Dim interval As UShort
            Try
                interval = CUShort(Val(TxtBoxMaxClips.Text))
                If interval < 1 Then
                    interval = 1
                ElseIf interval > 200 Then
                    interval = 200
                End If
            Catch
                interval = 30
            End Try
            App.Settings.MaxClips = interval
            TxtBoxMaxClips.Text = interval.ToString
            TxtBoxMaxClips.SelectAll()
            App.Tray.RefreshMenu()
        End If
    End Sub
    Private Sub TxtBoxMaxClipPreviewLength_Validated(sender As Object, e As EventArgs) Handles TxtBoxMaxClipPreviewLength.Validated
        If Not String.IsNullOrEmpty(TxtBoxMaxClipPreviewLength.Text) Then
            Dim interval As UShort
            Try
                interval = CUShort(Val(TxtBoxMaxClipPreviewLength.Text))
                If interval < 5 Then
                    interval = 5
                ElseIf interval > 500 Then
                    interval = 500
                End If
            Catch
                interval = 60
            End Try
            App.Settings.MaxClipPreviewLength = interval
            TxtBoxMaxClipPreviewLength.Text = interval.ToString
            TxtBoxMaxClipPreviewLength.SelectAll()
        End If
    End Sub
    Private Sub TxtBoxHotKeyToggleFavorite_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxHotKeyToggleFavorite.KeyDown
        e.SuppressKeyPress = True
        Dim mods As Keys = e.Modifiers
        Dim key As Keys = e.KeyCode
        If key = Keys.ControlKey OrElse key = Keys.ShiftKey OrElse key = Keys.Menu Then Return 'Ignore modifier-only presses
        Dim combo As Keys = mods Or key
        TxtBoxHotKeyToggleFavorite.Text = FormatHotKey(combo)
        App.Settings.HotKeys.ToggleFavorite = combo
    End Sub
    Private Sub TxtBoxHotKeyShowViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxHotKeyShowViewer.KeyDown
        e.SuppressKeyPress = True
        Dim mods = e.Modifiers
        Dim key = e.KeyCode
        If key = Keys.ControlKey OrElse key = Keys.ShiftKey OrElse key = Keys.Menu Then Return 'Ignore modifier-only presses
        Dim combo = mods Or key
        TxtBoxHotKeyShowViewer.Text = FormatHotKey(combo)
        App.Settings.HotKeys.ShowViewer = combo
    End Sub
    Private Sub TxtBoxHotKeyShowScratchPad_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxHotKeyShowScratchPad.KeyDown
        e.SuppressKeyPress = True
        Dim mods = e.Modifiers
        Dim key = e.KeyCode
        If key = Keys.ControlKey OrElse key = Keys.ShiftKey OrElse key = Keys.Menu Then Return 'Ignore modifier-only presses
        Dim combo = mods Or key
        TxtBoxHotKeyShowScratchPad.Text = FormatHotKey(combo)
        App.Settings.HotKeys.ShowScratchPad = combo
    End Sub
    Private Sub TxtBoxPurgeDays_Validated(sender As Object, e As EventArgs) Handles TxtBoxPurgeDays.Validated
        If Not String.IsNullOrEmpty(TxtBoxPurgeDays.Text) Then
            Dim interval As UShort
            Try
                interval = CUShort(Val(TxtBoxPurgeDays.Text))
                If interval < 1 Then
                    interval = 1
                ElseIf interval > 2000 Then
                    interval = 2000
                End If
            Catch
                interval = 30
            End Try
            App.Settings.PurgeDays = interval
            TxtBoxPurgeDays.Text = interval.ToString
            TxtBoxPurgeDays.SelectAll()
        End If
    End Sub
    Private Sub ChkBoxAutoStartWithWindows_Click(sender As Object, e As EventArgs) Handles ChkBoxAutoStartWithWindows.Click
        App.Settings.AutoStartWithWindows = Not App.Settings.AutoStartWithWindows
        App.SetAutoStart()
    End Sub
    Private Sub ChkBoxAutoPurge_Click(sender As Object, e As EventArgs) Handles ChkBoxAutoPurge.Click
        App.Settings.AutoPurge = Not App.Settings.AutoPurge
    End Sub
    Private Sub ChkBoxBlinkOnNewClip_Click(sender As Object, e As EventArgs) Handles ChkBoxBlinkOnNewClip.Click
        App.Settings.BlinkOnNewClip = Not App.Settings.BlinkOnNewClip
    End Sub
    Private Sub ChkBoxNotifyOnNewClip_Click(sender As Object, e As EventArgs) Handles ChkBoxNotifyOnNewClip.Click
        App.Settings.NotifyOnNewClip = Not App.Settings.NotifyOnNewClip
    End Sub
    Private Sub ChkBoxPlaySoundWithNotify_Click(sender As Object, e As EventArgs) Handles ChkBoxPlaySoundWithNotify.Click
        App.Settings.PlaySoundWithNotify = Not App.Settings.PlaySoundWithNotify
    End Sub
    Private Sub ChkBoxShowOpenSourceApp_Click(sender As Object, e As EventArgs) Handles ChkBoxShowOpenSourceApp.Click
        App.Settings.ShowOpenSourceApp = Not App.Settings.ShowOpenSourceApp
    End Sub
    Private Sub ChkBoxKeepScratchPadText_Click(sender As Object, e As EventArgs) Handles ChkBoxKeepScratchPadText.Click
        App.Settings.ScratchPadKeepText = Not App.Settings.ScratchPadKeepText
    End Sub

    ' Methods
    Private Function FormatHotKey(k As Keys) As String
        Dim parts As New List(Of String)

        If (k And Keys.Control) = Keys.Control Then parts.Add("Ctrl")
        If (k And Keys.Shift) = Keys.Shift Then parts.Add("Shift")
        If (k And Keys.Alt) = Keys.Alt Then parts.Add("Alt")

        Dim keyOnly = k And Not Keys.Modifiers
        parts.Add(keyOnly.ToString())

        Return String.Join("+", parts)
    End Function
    Private Sub CheckMove(ByRef location As Point)
        Dim wa As Rectangle = Screen.FromPoint(location).WorkingArea
        If location.X + Me.Width > wa.Right Then
            location.X = wa.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        End If
        If location.X < wa.Left Then
            location.X = wa.Left - App.AdjustScreenBoundsDialogWindow
        End If
        If location.Y + Me.Height > wa.Bottom Then
            location.Y = wa.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        End If
        If location.Y < wa.Top Then
            location.Y = wa.Top
        End If
    End Sub

    Private Sub ChkBoxThemeAuto_Click(sender As Object, e As EventArgs) Handles ChkBoxThemeAuto.Click
        'App.Settings.FollowWindowsTheme = chkFollowWindows.Checked
        'App.Settings.Save()

        'If chkFollowWindows.Checked Then
        '    ' Immediately sync to Windows theme
        '    Dim isDark = DetectWindowsDarkMode()
        '    Dim themeName = If(isDark, "Dark", "Light")

        '    App.Settings.ThemeName = themeName
        '    App.Settings.Save()

        '    ThemeManager.SetTheme(SkyeThemes.GetTheme(themeName))
        'End If
    End Sub

    Private Sub CoBoxTheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxTheme.SelectedIndexChanged
        Dim selected = CoBoxTheme.SelectedItem.ToString()
        App.Settings.ThemeName = selected
        If Not ChkBoxThemeAuto.Checked Then
            Skye.UI.ThemeManager.SetTheme(SkyeThemes.GetTheme(selected))
            Skye.UI.ThemeManager.ApplyThemeToAllOpenForms()
        End If
    End Sub
End Class
