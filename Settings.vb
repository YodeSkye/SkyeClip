
Imports System.ComponentModel.Design.ObjectSelectorEditor
Imports Skye.UI

Public Class Settings

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private suppressPageSelection As Boolean = False
    Private ProfileItemMove As ListViewItem 'Item being moved in the profile list

    ' Form Events
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipSettings)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged

        ILPageSelector.Images.Add(My.Resources.ImageSettings48)
        ILPageSelector.Images.Add(My.Resources.ImageApp48)
        ILPageSelector.Images.Add(My.Resources.ImageHotKeys48)
        ILPageSelector.Images.Add(My.Resources.ImageBackup48)
        ILPageSelector.Images.Add(My.Resources.ImageProfiles48)

        LVPageSelector.Items.Add(New ListViewItem("General", 0))
        LVPageSelector.Items.Add(New ListViewItem("Clips", 1))
        LVPageSelector.Items.Add(New ListViewItem("Hot Keys", 2))
        LVPageSelector.Items.Add(New ListViewItem("Backup", 3))
        LVPageSelector.Items.Add(New ListViewItem("Profiles", 4))
        LVPageSelector.Items(0).Selected = True

        PanelGeneral.Dock = DockStyle.Fill
        PanelClips.Dock = DockStyle.Fill
        PanelHotKeys.Dock = DockStyle.Fill
        PanelBackup.Dock = DockStyle.Fill
        PanelProfiles.Dock = DockStyle.Fill

        CMTxtBox.Font = App.MenuFont
        Dim bstr As String = TipSettings.GetText(BtnBackupNow)
        bstr &= App.UserPath
        TipSettings.SetText(BtnBackupNow, bstr)
        bstr = TipSettings.GetText(CoBoxAutoBackupFrequency)
        bstr &= App.UserPath
        TipSettings.SetText(CoBoxAutoBackupFrequency, bstr)
        LVProfiles.Columns(0).Width = LVProfiles.Width - SystemInformation.VerticalScrollBarWidth

        'Settings
        LoadSettings()

    End Sub
    Private Sub Settings_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LVPageSelector.Focus()
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
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
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
    Private Sub LVPageSelector_MouseDown(sender As Object, e As MouseEventArgs) Handles LVPageSelector.MouseDown
        ' Find the item under the mouse
        suppressPageSelection = True
        Dim info = LVPageSelector.HitTest(e.Location)
        Dim item = info.Item
        If item Is Nothing Then Return

        ' Ensure it becomes selected (for visual feedback)
        item.Selected = True
        Dim selectedSource As String = item.Text

        SetPage(selectedSource)
        suppressPageSelection = False
    End Sub
    Private Sub LVPageSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVPageSelector.SelectedIndexChanged
        If suppressPageSelection OrElse LVPageSelector.SelectedItems.Count = 0 Then Return
        Dim selectedSource As String = LVPageSelector.SelectedItems(0).Text
        SetPage(LVPageSelector.SelectedItems(0).Text)
    End Sub
    Private Sub LVProfiles_BeforeEdit(item As ListViewItem, subItemIndex As Integer, ByRef cancel As Boolean) Handles LVProfiles.BeforeEdit
        item.SubItems(subItemIndex).Tag = item.SubItems(subItemIndex).Text
    End Sub
    Private Sub LVProfiles_SubItemEdited(item As ListViewItem, subItemIndex As Integer, newValue As String) Handles LVProfiles.SubItemEdited

        ' Only enforce uniqueness on the Name column
        If subItemIndex <> 0 Then Exit Sub

        Dim trimmed = newValue.Trim()

        ' Empty names are not allowed
        If trimmed = String.Empty Then
            item.SubItems(subItemIndex).Text = item.SubItems(subItemIndex).Tag.ToString()
            'App.Tray.ShowToast("Profile names cannot be empty.")
            TipSettings.HideDelay = 4000
            TipSettings.ShowTooltipAtCursor("Profile names cannot be empty.", My.Resources.ImageProfiles16)
            TipSettings.HideDelay = 500
            Exit Sub
        End If

        ' Check for duplicates
        For Each li As ListViewItem In LVProfiles.Items
            If li IsNot item Then
                If String.Equals(li.SubItems(0).Text.Trim(), trimmed, StringComparison.OrdinalIgnoreCase) Then
                    ' Restore old value (stored in Tag)
                    item.SubItems(subItemIndex).Text = item.SubItems(subItemIndex).Tag.ToString()
                    'App.Tray.ShowToast("Profile names must be unique.")
                    TipSettings.HideDelay = 4000
                    TipSettings.ShowTooltipAtCursor("Profile names must be unique.", My.Resources.ImageProfiles16)
                    TipSettings.HideDelay = 500
                    Exit Sub
                End If
            End If
        Next

        ' If valid, update the Tag to the new value
        item.SubItems(subItemIndex).Tag = trimmed
    End Sub
    Private Sub LVProfiles_AfterEdit(item As ListViewItem, subItemIndex As Integer, newValue As String) Handles LVProfiles.AfterEdit
        SaveProfiles()
    End Sub
    Private Sub LVProfiles_MouseDown(sender As Object, e As MouseEventArgs) Handles LVProfiles.MouseDown
        If e.Clicks = 1 Then ProfileItemMove = LVProfiles.GetItemAt(e.X, e.Y)
    End Sub
    Private Sub LVProfiles_MouseMove(sender As Object, e As MouseEventArgs) Handles LVProfiles.MouseMove
        If ProfileItemMove IsNot Nothing Then
            Cursor = Cursors.Hand
            Dim lastItemBottom = Math.Min(e.Y, LVProfiles.Items(LVProfiles.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVProfiles.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing Then
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    LVProfiles.LineBefore = itemover.Index
                    LVProfiles.LineAfter = -1
                Else
                    LVProfiles.LineBefore = -1
                    LVProfiles.LineAfter = itemover.Index
                End If
                LVProfiles.Invalidate()
            End If
        End If
    End Sub
    Private Sub LVProfiles_MouseUp(sender As Object, e As MouseEventArgs) Handles LVProfiles.MouseUp
        If ProfileItemMove IsNot Nothing Then
            Dim lastItemBottom = Math.Min(e.Y, LVProfiles.Items(LVProfiles.Items.Count - 1).GetBounds(ItemBoundsPortion.Entire).Bottom - 1)
            Dim itemover = LVProfiles.GetItemAt(0, lastItemBottom)
            If itemover IsNot Nothing And itemover IsNot ProfileItemMove Then
                Dim insertbefore As Boolean
                Dim rc = itemover.GetBounds(ItemBoundsPortion.Entire)
                If e.Y < rc.Top + rc.Height / 2 Then
                    insertbefore = True
                Else
                    insertbefore = False
                End If
                LVProfiles.Items.Remove(ProfileItemMove)
                If Not ProfileItemMove.Index = itemover.Index Then
                    If insertbefore Then
                        LVProfiles.Items.Insert(itemover.Index, ProfileItemMove)
                    Else
                        LVProfiles.Items.Insert(itemover.Index + 1, ProfileItemMove)
                    End If
                End If
            End If
            ProfileItemMove = Nothing
            SaveProfiles()
            App.Tray.RefreshMenu()
        End If
        ProfileItemMove = Nothing
        Cursor = Cursors.Default
        LVProfiles.LineBefore = -1
        LVProfiles.LineAfter = -1
        LVProfiles.Invalidate()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnPurgeNow_Click(sender As Object, e As EventArgs) Handles BtnPurgeNow.Click
        Dim cutoff = DateTime.Now.AddDays(-App.Settings.PurgeDays)
        App.Tray.repo.PurgeClips(cutoff)
        App.Tray.RefreshMenu()
    End Sub
    Private Sub BtnBackupNow_Click(sender As Object, e As EventArgs) Handles BtnBackupNow.Click
        App.BackupManual()
    End Sub
    Private Sub BtnRestoreNow_Click(sender As Object, e As EventArgs) Handles BtnRestoreNow.Click
        Using dlg As New OpenFileDialog()
            dlg.Title = "Select a SkyeClip Backup"
            dlg.InitialDirectory = App.UserPath
            dlg.Filter = "SkyeClip Backups (*.db)|*.db"
            dlg.RestoreDirectory = True

            If dlg.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            End If

            Dim backupPath As String = dlg.FileName

            ' ---------------------------------------------------------
            ' 1. Ask if user wants to back up current DB first
            ' ---------------------------------------------------------
            Dim backupFirst = MessageBox.Show("Would you like to create a backup of your current clipboard history before restoring?",
                                "Backup Before Restore",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)

            If backupFirst = DialogResult.Yes Then BackupManual()

            ' ---------------------------------------------------------
            ' 2. Confirm destructive restore
            ' ---------------------------------------------------------
            Dim msg As String = "Restoring a backup will replace your current database. This action cannot be undone." & vbCrLf & vbCrLf & "Are you sure you want to continue?"
            Dim result = MessageBox.Show(msg,
                                     "Restore Backup",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Warning)
            If result <> DialogResult.Yes Then Exit Sub

            App.RestoreBackup(backupPath)

        End Using
    End Sub
    Private Sub BtnAddProfile_Click(sender As Object, e As EventArgs) Handles BtnAddProfile.Click
        Dim newid As Integer = App.Settings.GetNextProfileID
        Dim lvi As New ListViewItem("Profile " & newid.ToString) With {.Tag = newid}
        LVProfiles.Items.Add(lvi)
        SaveProfiles()
        App.Tray.RefreshMenu()
    End Sub
    Private Sub BtnRemoveProfile_Click(sender As Object, e As EventArgs) Handles BtnRemoveProfile.Click
        If LVProfiles.SelectedItems.Count = 0 Then Exit Sub
        Dim lvi As ListViewItem = LVProfiles.SelectedItems(0)
        Dim profileID As Integer = CInt(lvi.Tag)

        ' Delete profile's scratch pad text file if it exists
        Dim filePath = App.GetScratchPadProfiledPath(profileID)
        If IO.File.Exists(filePath) Then
            IO.File.Delete(filePath)
        End If

        ' Delete the profile and refresh
        App.Settings.DeleteProfile(profileID)
        LVProfiles.Items.Remove(lvi)

        ' If the deleted profile was the last used one, reset last used profile ID to 0 (no profile)
        If App.Settings.LastUsedProfileID = profileID Then
            App.Settings.LastUsedProfileID = 0
        End If

        ' If there are no profiles left, disable profile mode
        If App.Settings.Profiles.Count = 0 Then
            ChkBoxUseProfiles.Checked = False
            App.Settings.UseProfiles = False
            SetUseProfiles()
            App.Tray.RefreshMenu()
        End If

    End Sub
    Private Sub CoBoxAutoBackupFrequency_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CoBoxAutoBackupFrequency.SelectionChangeCommitted
        Dim item = CType(CoBoxAutoBackupFrequency.SelectedItem, App.AutoBackupFrequencyEnumItem)
        App.Settings.AutoBackup = item.Value
    End Sub
    Private Sub ChkBoxAutoPurgeBackups_Click(sender As Object, e As EventArgs) Handles ChkBoxAutoPurgeBackups.Click
        App.Settings.AutoBackupPurge = ChkBoxAutoPurgeBackups.Checked
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
        CMTxtBox.ShortcutKeys(CType(sender, System.Windows.Forms.TextBox), e)
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
    Private Sub CoBoxTheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxTheme.SelectedIndexChanged
        Dim selected = CoBoxTheme.SelectedItem.ToString
        App.Settings.ThemeName = selected
        If Not App.Settings.ThemeAuto Then
            SetTheme(GetTheme(selected))
            ApplyThemeToAllOpenForms()
        End If
    End Sub
    Private Sub ChkBoxThemeAuto_Click(sender As Object, e As EventArgs) Handles ChkBoxThemeAuto.Click
        App.Settings.ThemeAuto = ChkBoxThemeAuto.Checked
        SetThemesList()
        If App.Settings.ThemeAuto Then
            ' Auto mode: sync to Windows immediately
            Skye.UI.ThemeManager.SetTheme(DetectWindowsTheme())
        Else
            ' Manual mode: apply whatever theme the user selected
            Skye.UI.ThemeManager.SetTheme(Skye.UI.SkyeThemes.GetTheme(App.Settings.ThemeName))
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
        App.Settings.BlinkOnNewClip = ChkBoxBlinkOnNewClip.Checked
    End Sub
    Private Sub ChkBoxNotifyOnNewClip_Click(sender As Object, e As EventArgs) Handles ChkBoxNotifyOnNewClip.Click
        App.Settings.NotifyOnNewClip = ChkBoxNotifyOnNewClip.Checked
    End Sub
    Private Sub ChkBoxPlaySoundWithNotify_Click(sender As Object, e As EventArgs) Handles ChkBoxPlaySoundWithNotify.Click
        App.Settings.PlaySoundWithNotify = ChkBoxPlaySoundWithNotify.Checked
    End Sub
    Private Sub ChkBoxShowOpenSourceApp_Click(sender As Object, e As EventArgs) Handles ChkBoxShowOpenSourceApp.Click
        App.Settings.ShowOpenSourceApp = Not App.Settings.ShowOpenSourceApp
    End Sub
    Private Sub ChkBoxKeepScratchPadText_Click(sender As Object, e As EventArgs) Handles ChkBoxKeepScratchPadText.Click
        App.Settings.ScratchPadKeepText = Not App.Settings.ScratchPadKeepText
        App.FrmScratchPad?.UpdateUI()
    End Sub
    Private Sub ChkBoxUseProfiles_Click(sender As Object, e As EventArgs) Handles ChkBoxUseProfiles.Click
        If ChkBoxUseProfiles.Checked Then
            If App.Settings.Profiles Is Nothing OrElse App.Settings.Profiles.Count = 0 Then
                TipSettings.HideDelay = 4000
                TipSettings.ShowTooltipAtCursor("You must create at least one profile before enabling Profile Mode.", My.Resources.ImageProfiles16)
                TipSettings.HideDelay = 500
                ChkBoxUseProfiles.Checked = False
                Exit Sub
            End If
        End If
        If App.FrmScratchPad IsNot Nothing Then App.FrmScratchPad?.SaveScratchPad()
        App.Settings.UseProfiles = Not App.Settings.UseProfiles
        If App.FrmScratchPad Is Nothing Then App.LoadScratchPadText()
        SetUseProfiles()
        App.FrmScratchPad?.UpdateUI()
    End Sub

    ' Methods
    Friend Sub LoadSettings()
        Text = "Settings for " & GetAppTitle()
        If App.Settings.UseProfiles Then Text &= " (" & App.Settings.GetProfileName(App.Settings.CurrentProfileID) & " Profile)"
        ChkBoxThemeAuto.Checked = App.Settings.ThemeAuto
        CoBoxTheme.Items.Clear()
        For Each theme In SkyeThemes.AllThemes
            CoBoxTheme.Items.Add(theme.Name)
        Next
        CoBoxTheme.SelectedItem = App.Settings.ThemeName
        SetThemesList()
        ChkBoxAutoStartWithWindows.Checked = App.Settings.AutoStartWithWindows
        TxtBoxMaxClips.Text = App.Settings.MaxClips.ToString
        TxtBoxMaxClipPreviewLength.Text = App.Settings.MaxClipPreviewLength.ToString
        ChkBoxBlinkOnNewClip.Checked = App.Settings.BlinkOnNewClip
        ChkBoxNotifyOnNewClip.Checked = App.Settings.NotifyOnNewClip
        ChkBoxPlaySoundWithNotify.Checked = App.Settings.PlaySoundWithNotify
        ChkBoxShowOpenSourceApp.Checked = App.Settings.ShowOpenSourceApp
        SetKeepText()
        TxtBoxHotKeyToggleFavorite.Text = FormatHotKey(App.Settings.HotKeys.ToggleFavorite)
        TxtBoxHotKeyShowViewer.Text = FormatHotKey(App.Settings.HotKeys.ShowViewer)
        TxtBoxHotKeyShowScratchPad.Text = FormatHotKey(App.Settings.HotKeys.ShowScratchPad)
        TxtBoxPurgeDays.Text = App.Settings.PurgeDays.ToString
        ChkBoxAutoPurge.Checked = App.Settings.AutoPurge
        CoBoxAutoBackupFrequency.Items.Clear()
        For Each freq As AutoBackupFrequency In [Enum].GetValues(Of AutoBackupFrequency)()
            CoBoxAutoBackupFrequency.Items.Add(New App.AutoBackupFrequencyEnumItem With {
                .Value = freq,
                .Text = freq.ToString().Replace("_", " ")
            })
        Next
        For Each item As App.AutoBackupFrequencyEnumItem In CoBoxAutoBackupFrequency.Items
            If item.Value = App.Settings.AutoBackup Then
                CoBoxAutoBackupFrequency.SelectedItem = item
                Exit For
            End If
        Next
        ChkBoxAutoPurgeBackups.Checked = App.Settings.AutoBackupPurge
        ChkBoxUseProfiles.Checked = App.Settings.UseProfiles
        LVProfiles.Items.Clear()
        For Each p As App.Settings.Profile In App.Settings.Profiles
            Dim lvi As New ListViewItem(p.Name) With {.Tag = p.ID}
            LVProfiles.Items.Add(lvi)
        Next

        If App.Settings.UseProfiles Then
            LblThemeBadge.Visible = True
            LblThemeAutoBadge.Visible = True
            LblMaxClipsBadge.Visible = True
            LblMaxClipPreviewLengthBadge.Visible = True
            LblBlinkOnNewClipBadge.Visible = True
            LblNotifyOnNewClipBadge.Visible = True
            LblPlaySoundWithNotifyBadge.Visible = True
        Else
            LblThemeBadge.Visible = False
            LblThemeAutoBadge.Visible = False
            LblMaxClipsBadge.Visible = False
            LblMaxClipPreviewLengthBadge.Visible = False
            LblBlinkOnNewClipBadge.Visible = False
            LblNotifyOnNewClipBadge.Visible = False
            LblPlaySoundWithNotifyBadge.Visible = False
        End If

    End Sub
    Private Sub SetPage(page As String)
        PanelGeneral.Enabled = False
        PanelClips.Enabled = False
        PanelHotKeys.Enabled = False
        PanelBackup.Enabled = False
        PanelProfiles.Enabled = False
        Select Case page
            Case "General"
                PanelGeneral.Enabled = True
                PanelGeneral.BringToFront()
            Case "Clips"
                PanelClips.Enabled = True
                PanelClips.BringToFront()
            Case "Hot Keys"
                PanelHotKeys.Enabled = True
                PanelHotKeys.BringToFront()
            Case "Backup"
                PanelBackup.Enabled = True
                PanelBackup.BringToFront()
            Case "Profiles"
                PanelProfiles.Enabled = True
                PanelProfiles.BringToFront()
        End Select
    End Sub
    Private Function FormatHotKey(k As Keys) As String
        Dim parts As New List(Of String)

        If (k And Keys.Control) = Keys.Control Then parts.Add("Ctrl")
        If (k And Keys.Shift) = Keys.Shift Then parts.Add("Shift")
        If (k And Keys.Alt) = Keys.Alt Then parts.Add("Alt")

        Dim keyOnly = k And Not Keys.Modifiers
        parts.Add(keyOnly.ToString())

        Return String.Join("+", parts)
    End Function
    Private Sub SetUseProfiles()
        If App.Settings.ThemeAuto Then
            Skye.UI.ThemeManager.SetTheme(DetectWindowsTheme())
        Else
            SetTheme(GetTheme(App.Settings.ThemeName))
            ApplyThemeToAllOpenForms()
        End If
        LoadSettings() 'Reload settings to update badges visibility
    End Sub
    Friend Sub SetKeepText()
        ChkBoxKeepScratchPadText.Checked = App.Settings.ScratchPadKeepText
    End Sub
    Private Sub SetThemesList()
        If App.Settings.ThemeAuto Then
            CoBoxTheme.Enabled = False
        Else
            CoBoxTheme.Enabled = True
        End If
    End Sub
    Private Sub SaveProfiles()
        Dim newList As New List(Of App.Settings.Profile)

        For Each item As ListViewItem In LVProfiles.Items
            Dim id As Integer = CInt(item.Tag)
            ' Try to find an existing profile
            Dim p = App.Settings.Profiles.FirstOrDefault(Function(x) x.ID = id)
            If p Is Nothing Then
                ' This is a NEW profile
                p = New App.Settings.Profile With {
                    .ID = id,
                    .Name = item.Text
                }
            Else
                ' Existing profile: update name in case it changed
                p.Name = item.Text
            End If
            newList.Add(p)
        Next

        App.Settings.Profiles = newList
        App.Settings.Save()
    End Sub
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

End Class
