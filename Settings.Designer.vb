<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
        BtnOK = New Button()
        LblMaxClips = New Label()
        TxtBoxMaxClips = New TextBox()
        CMTxtBox = New Skye.UI.TextBoxContextMenu()
        TxtBoxMaxClipPreviewLength = New TextBox()
        LblMaxClipPreviewLength = New Label()
        TipSettings = New Skye.UI.ToolTipEX(components)
        ChkBoxBlinkOnNewClip = New CheckBox()
        ChkBoxNotifyOnNewClip = New CheckBox()
        TxtBoxHotKeyToggleFavorite = New TextBox()
        LblHotKeyToggleFavorite = New Label()
        LblHotKeys = New Label()
        TxtBoxHotKeyShowViewer = New TextBox()
        LblHotKeyShowViewer = New Label()
        LblPurgeDays1 = New Label()
        LblPurgeDays2 = New Label()
        TxtBoxPurgeDays = New TextBox()
        ChkBoxAutoPurge = New CheckBox()
        BtnPurgeNow = New Button()
        ChkBoxShowOpenSourceApp = New CheckBox()
        ChkBoxKeepScratchPadText = New CheckBox()
        TxtBoxHotKeyShowScratchPad = New TextBox()
        LblHotKeyShowScratchPad = New Label()
        ChkBoxPlaySoundWithNotify = New CheckBox()
        ChkBoxAutoStartWithWindows = New CheckBox()
        ChkBoxThemeAuto = New CheckBox()
        LblTheme = New Label()
        CoBoxTheme = New Skye.UI.ComboBox()
        CoBoxAutoBackupFrequency = New Skye.UI.ComboBox()
        LblAutoBackup = New Label()
        ChkBoxAutoPurgeBackups = New CheckBox()
        BtnBackupNow = New Button()
        BtnRestoreNow = New Button()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipSettings.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(346, 469)
        BtnOK.Margin = New Padding(4)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 1100
        TipSettings.SetText(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblMaxClips
        ' 
        LblMaxClips.AutoSize = True
        TipSettings.SetImage(LblMaxClips, Nothing)
        LblMaxClips.Location = New Point(10, 7)
        LblMaxClips.Name = "LblMaxClips"
        LblMaxClips.Size = New Size(77, 21)
        LblMaxClips.TabIndex = 1
        LblMaxClips.Text = "Max Clips"
        TipSettings.SetText(LblMaxClips, Nothing)
        ' 
        ' TxtBoxMaxClips
        ' 
        TxtBoxMaxClips.ContextMenuStrip = CMTxtBox
        TipSettings.SetImage(TxtBoxMaxClips, Nothing)
        TxtBoxMaxClips.Location = New Point(12, 28)
        TxtBoxMaxClips.Name = "TxtBoxMaxClips"
        TxtBoxMaxClips.ShortcutsEnabled = False
        TxtBoxMaxClips.Size = New Size(60, 29)
        TxtBoxMaxClips.TabIndex = 10
        TipSettings.SetText(TxtBoxMaxClips, "Maximum number of clips to show in the menu.")
        TxtBoxMaxClips.TextAlign = HorizontalAlignment.Center
        ' 
        ' CMTxtBox
        ' 
        TipSettings.SetImage(CMTxtBox, Nothing)
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.Size = New Size(138, 176)
        TipSettings.SetText(CMTxtBox, Nothing)
        ' 
        ' TxtBoxMaxClipPreviewLength
        ' 
        TxtBoxMaxClipPreviewLength.ContextMenuStrip = CMTxtBox
        TipSettings.SetImage(TxtBoxMaxClipPreviewLength, Nothing)
        TxtBoxMaxClipPreviewLength.Location = New Point(12, 89)
        TxtBoxMaxClipPreviewLength.Name = "TxtBoxMaxClipPreviewLength"
        TxtBoxMaxClipPreviewLength.ShortcutsEnabled = False
        TxtBoxMaxClipPreviewLength.Size = New Size(60, 29)
        TxtBoxMaxClipPreviewLength.TabIndex = 20
        TipSettings.SetText(TxtBoxMaxClipPreviewLength, "Maximum text length of each clip in the menu.")
        TxtBoxMaxClipPreviewLength.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblMaxClipPreviewLength
        ' 
        LblMaxClipPreviewLength.AutoSize = True
        TipSettings.SetImage(LblMaxClipPreviewLength, Nothing)
        LblMaxClipPreviewLength.Location = New Point(10, 68)
        LblMaxClipPreviewLength.Name = "LblMaxClipPreviewLength"
        LblMaxClipPreviewLength.Size = New Size(181, 21)
        LblMaxClipPreviewLength.TabIndex = 3
        LblMaxClipPreviewLength.Text = "Max Clip Preview Length"
        TipSettings.SetText(LblMaxClipPreviewLength, Nothing)
        ' 
        ' TipSettings
        ' 
        TipSettings.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipSettings.ShadowThickness = 0
        ' 
        ' ChkBoxBlinkOnNewClip
        ' 
        ChkBoxBlinkOnNewClip.AutoSize = True
        TipSettings.SetImage(ChkBoxBlinkOnNewClip, Nothing)
        ChkBoxBlinkOnNewClip.Location = New Point(317, 43)
        ChkBoxBlinkOnNewClip.Name = "ChkBoxBlinkOnNewClip"
        ChkBoxBlinkOnNewClip.RightToLeft = RightToLeft.Yes
        ChkBoxBlinkOnNewClip.Size = New Size(155, 25)
        ChkBoxBlinkOnNewClip.TabIndex = 30
        TipSettings.SetText(ChkBoxBlinkOnNewClip, "Blink the tray icon several times when the clipboard changes.")
        ChkBoxBlinkOnNewClip.Text = "Blink On New Clip"
        ChkBoxBlinkOnNewClip.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxNotifyOnNewClip
        ' 
        ChkBoxNotifyOnNewClip.AutoSize = True
        TipSettings.SetImage(ChkBoxNotifyOnNewClip, Nothing)
        ChkBoxNotifyOnNewClip.Location = New Point(308, 65)
        ChkBoxNotifyOnNewClip.Name = "ChkBoxNotifyOnNewClip"
        ChkBoxNotifyOnNewClip.RightToLeft = RightToLeft.Yes
        ChkBoxNotifyOnNewClip.Size = New Size(164, 25)
        ChkBoxNotifyOnNewClip.TabIndex = 35
        TipSettings.SetText(ChkBoxNotifyOnNewClip, "Show a notification toast when the clipboard changes.")
        ChkBoxNotifyOnNewClip.Text = "Notify On New Clip"
        ChkBoxNotifyOnNewClip.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHotKeyToggleFavorite
        ' 
        TxtBoxHotKeyToggleFavorite.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyToggleFavorite, My.Resources.Resources.ImageFavorites16)
        TxtBoxHotKeyToggleFavorite.Location = New Point(12, 211)
        TxtBoxHotKeyToggleFavorite.Name = "TxtBoxHotKeyToggleFavorite"
        TxtBoxHotKeyToggleFavorite.ReadOnly = True
        TxtBoxHotKeyToggleFavorite.ShortcutsEnabled = False
        TxtBoxHotKeyToggleFavorite.Size = New Size(144, 29)
        TxtBoxHotKeyToggleFavorite.TabIndex = 50
        TipSettings.SetText(TxtBoxHotKeyToggleFavorite, "Key or Key Combination to use on the tray menu to Favorite a clip.")
        TxtBoxHotKeyToggleFavorite.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHotKeyToggleFavorite
        ' 
        TipSettings.SetImage(LblHotKeyToggleFavorite, Nothing)
        LblHotKeyToggleFavorite.Location = New Point(10, 190)
        LblHotKeyToggleFavorite.Name = "LblHotKeyToggleFavorite"
        LblHotKeyToggleFavorite.Size = New Size(146, 24)
        LblHotKeyToggleFavorite.TabIndex = 7
        LblHotKeyToggleFavorite.Text = "Toggle Favorite"
        TipSettings.SetText(LblHotKeyToggleFavorite, Nothing)
        LblHotKeyToggleFavorite.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblHotKeys
        ' 
        LblHotKeys.Font = New Font("Segoe UI", 12F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        TipSettings.SetImage(LblHotKeys, Nothing)
        LblHotKeys.Location = New Point(12, 166)
        LblHotKeys.Name = "LblHotKeys"
        LblHotKeys.Size = New Size(144, 24)
        LblHotKeys.TabIndex = 9
        LblHotKeys.Text = "HotKeys"
        TipSettings.SetText(LblHotKeys, Nothing)
        LblHotKeys.TextAlign = ContentAlignment.TopCenter
        ' 
        ' TxtBoxHotKeyShowViewer
        ' 
        TxtBoxHotKeyShowViewer.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyShowViewer, My.Resources.Resources.imageClipViewer16)
        TxtBoxHotKeyShowViewer.Location = New Point(12, 264)
        TxtBoxHotKeyShowViewer.Name = "TxtBoxHotKeyShowViewer"
        TxtBoxHotKeyShowViewer.ReadOnly = True
        TxtBoxHotKeyShowViewer.ShortcutsEnabled = False
        TxtBoxHotKeyShowViewer.Size = New Size(144, 29)
        TxtBoxHotKeyShowViewer.TabIndex = 60
        TipSettings.SetText(TxtBoxHotKeyShowViewer, "Key or Key Combination to use on the tray menu to show the Clip Viewer.")
        TxtBoxHotKeyShowViewer.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHotKeyShowViewer
        ' 
        TipSettings.SetImage(LblHotKeyShowViewer, Nothing)
        LblHotKeyShowViewer.Location = New Point(10, 243)
        LblHotKeyShowViewer.Name = "LblHotKeyShowViewer"
        LblHotKeyShowViewer.Size = New Size(146, 24)
        LblHotKeyShowViewer.TabIndex = 10
        LblHotKeyShowViewer.Text = "Show Viewer"
        TipSettings.SetText(LblHotKeyShowViewer, Nothing)
        LblHotKeyShowViewer.TextAlign = ContentAlignment.TopCenter
        ' 
        ' LblPurgeDays1
        ' 
        LblPurgeDays1.AutoSize = True
        TipSettings.SetImage(LblPurgeDays1, Nothing)
        LblPurgeDays1.Location = New Point(10, 395)
        LblPurgeDays1.Name = "LblPurgeDays1"
        LblPurgeDays1.Size = New Size(170, 21)
        LblPurgeDays1.TabIndex = 12
        LblPurgeDays1.Text = "Purge Clips Older Than"
        TipSettings.SetText(LblPurgeDays1, Nothing)
        LblPurgeDays1.TextAlign = ContentAlignment.TopRight
        ' 
        ' LblPurgeDays2
        ' 
        LblPurgeDays2.AutoSize = True
        TipSettings.SetImage(LblPurgeDays2, Nothing)
        LblPurgeDays2.Location = New Point(232, 395)
        LblPurgeDays2.Name = "LblPurgeDays2"
        LblPurgeDays2.Size = New Size(44, 21)
        LblPurgeDays2.TabIndex = 13
        LblPurgeDays2.Text = "Days"
        TipSettings.SetText(LblPurgeDays2, Nothing)
        ' 
        ' TxtBoxPurgeDays
        ' 
        TipSettings.SetImage(TxtBoxPurgeDays, Nothing)
        TxtBoxPurgeDays.Location = New Point(176, 392)
        TxtBoxPurgeDays.Name = "TxtBoxPurgeDays"
        TxtBoxPurgeDays.Size = New Size(58, 29)
        TxtBoxPurgeDays.TabIndex = 150
        TipSettings.SetText(TxtBoxPurgeDays, "Purge Clips Older than x Days")
        TxtBoxPurgeDays.TextAlign = HorizontalAlignment.Center
        ' 
        ' ChkBoxAutoPurge
        ' 
        ChkBoxAutoPurge.AutoSize = True
        TipSettings.SetImage(ChkBoxAutoPurge, Nothing)
        ChkBoxAutoPurge.Location = New Point(325, 396)
        ChkBoxAutoPurge.Name = "ChkBoxAutoPurge"
        ChkBoxAutoPurge.Size = New Size(109, 25)
        ChkBoxAutoPurge.TabIndex = 170
        TipSettings.SetText(ChkBoxAutoPurge, "Auto-Purge Clips Once Each Day on Startup.")
        ChkBoxAutoPurge.Text = "Auto-Purge"
        ChkBoxAutoPurge.UseVisualStyleBackColor = True
        ' 
        ' BtnPurgeNow
        ' 
        TipSettings.SetImage(BtnPurgeNow, Nothing)
        BtnPurgeNow.Location = New Point(155, 424)
        BtnPurgeNow.Name = "BtnPurgeNow"
        BtnPurgeNow.Size = New Size(99, 32)
        BtnPurgeNow.TabIndex = 160
        BtnPurgeNow.TabStop = False
        TipSettings.SetText(BtnPurgeNow, "Purge Clips Now")
        BtnPurgeNow.Text = "Purge Now"
        BtnPurgeNow.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxShowOpenSourceApp
        ' 
        ChkBoxShowOpenSourceApp.AutoSize = True
        TipSettings.SetImage(ChkBoxShowOpenSourceApp, Nothing)
        ChkBoxShowOpenSourceApp.Location = New Point(278, 118)
        ChkBoxShowOpenSourceApp.Name = "ChkBoxShowOpenSourceApp"
        ChkBoxShowOpenSourceApp.RightToLeft = RightToLeft.Yes
        ChkBoxShowOpenSourceApp.Size = New Size(194, 25)
        ChkBoxShowOpenSourceApp.TabIndex = 40
        TipSettings.SetText(ChkBoxShowOpenSourceApp, "Controls whether ""Open Source App"" appears as an Option in the Clip Context Menu.")
        ChkBoxShowOpenSourceApp.Text = "Show Open Source App"
        ChkBoxShowOpenSourceApp.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxKeepScratchPadText
        ' 
        ChkBoxKeepScratchPadText.AutoSize = True
        TipSettings.SetImage(ChkBoxKeepScratchPadText, Nothing)
        ChkBoxKeepScratchPadText.Location = New Point(295, 149)
        ChkBoxKeepScratchPadText.Name = "ChkBoxKeepScratchPadText"
        ChkBoxKeepScratchPadText.RightToLeft = RightToLeft.Yes
        ChkBoxKeepScratchPadText.Size = New Size(177, 25)
        ChkBoxKeepScratchPadText.TabIndex = 45
        TipSettings.SetText(ChkBoxKeepScratchPadText, "Keep the contents of the Scratch Pad between sessions?")
        ChkBoxKeepScratchPadText.Text = "Keep Scratch Pad Text"
        ChkBoxKeepScratchPadText.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHotKeyShowScratchPad
        ' 
        TxtBoxHotKeyShowScratchPad.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyShowScratchPad, My.Resources.Resources.imageScratchPad16)
        TxtBoxHotKeyShowScratchPad.Location = New Point(12, 317)
        TxtBoxHotKeyShowScratchPad.Name = "TxtBoxHotKeyShowScratchPad"
        TxtBoxHotKeyShowScratchPad.ReadOnly = True
        TxtBoxHotKeyShowScratchPad.ShortcutsEnabled = False
        TxtBoxHotKeyShowScratchPad.Size = New Size(144, 29)
        TxtBoxHotKeyShowScratchPad.TabIndex = 65
        TipSettings.SetText(TxtBoxHotKeyShowScratchPad, "Key or Key Combination to use on the tray menu to show the clipboard Scratch Pad.")
        TxtBoxHotKeyShowScratchPad.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHotKeyShowScratchPad
        ' 
        TipSettings.SetImage(LblHotKeyShowScratchPad, Nothing)
        LblHotKeyShowScratchPad.Location = New Point(10, 296)
        LblHotKeyShowScratchPad.Name = "LblHotKeyShowScratchPad"
        LblHotKeyShowScratchPad.Size = New Size(146, 24)
        LblHotKeyShowScratchPad.TabIndex = 1101
        LblHotKeyShowScratchPad.Text = "Show Scratch Pad"
        TipSettings.SetText(LblHotKeyShowScratchPad, Nothing)
        LblHotKeyShowScratchPad.TextAlign = ContentAlignment.TopCenter
        ' 
        ' ChkBoxPlaySoundWithNotify
        ' 
        ChkBoxPlaySoundWithNotify.AutoSize = True
        TipSettings.SetImage(ChkBoxPlaySoundWithNotify, Nothing)
        ChkBoxPlaySoundWithNotify.Location = New Point(281, 87)
        ChkBoxPlaySoundWithNotify.Name = "ChkBoxPlaySoundWithNotify"
        ChkBoxPlaySoundWithNotify.RightToLeft = RightToLeft.Yes
        ChkBoxPlaySoundWithNotify.Size = New Size(191, 25)
        ChkBoxPlaySoundWithNotify.TabIndex = 36
        TipSettings.SetText(ChkBoxPlaySoundWithNotify, "Play a Sound with the Notification Toast.")
        ChkBoxPlaySoundWithNotify.Text = "Play Sound With Notify"
        ChkBoxPlaySoundWithNotify.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxAutoStartWithWindows
        ' 
        ChkBoxAutoStartWithWindows.AutoSize = True
        TipSettings.SetImage(ChkBoxAutoStartWithWindows, Nothing)
        ChkBoxAutoStartWithWindows.Location = New Point(266, 12)
        ChkBoxAutoStartWithWindows.Name = "ChkBoxAutoStartWithWindows"
        ChkBoxAutoStartWithWindows.RightToLeft = RightToLeft.Yes
        ChkBoxAutoStartWithWindows.Size = New Size(206, 25)
        ChkBoxAutoStartWithWindows.TabIndex = 29
        TipSettings.SetText(ChkBoxAutoStartWithWindows, "Auto-Start the App with Windows.")
        ChkBoxAutoStartWithWindows.Text = "Auto-Start With Windows"
        ChkBoxAutoStartWithWindows.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxThemeAuto
        ' 
        ChkBoxThemeAuto.AutoSize = True
        TipSettings.SetImage(ChkBoxThemeAuto, Nothing)
        ChkBoxThemeAuto.Location = New Point(256, 290)
        ChkBoxThemeAuto.Name = "ChkBoxThemeAuto"
        ChkBoxThemeAuto.Size = New Size(205, 25)
        ChkBoxThemeAuto.TabIndex = 110
        TipSettings.SetText(ChkBoxThemeAuto, "Automatically set the theme to match current windows settings.")
        ChkBoxThemeAuto.Text = "Auto Sync With Windows"
        ChkBoxThemeAuto.UseVisualStyleBackColor = True
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        TipSettings.SetImage(LblTheme, Nothing)
        LblTheme.Location = New Point(256, 242)
        LblTheme.Name = "LblTheme"
        LblTheme.Size = New Size(57, 21)
        LblTheme.TabIndex = 1104
        LblTheme.Text = "Theme"
        TipSettings.SetText(LblTheme, Nothing)
        ' 
        ' CoBoxTheme
        ' 
        CoBoxTheme.DrawMode = DrawMode.OwnerDrawFixed
        CoBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTheme.FormattingEnabled = True
        TipSettings.SetImage(CoBoxTheme, Nothing)
        CoBoxTheme.Location = New Point(256, 263)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(216, 30)
        CoBoxTheme.TabIndex = 100
        TipSettings.SetText(CoBoxTheme, "Select a Theme.")
        ' 
        ' CoBoxAutoBackupFrequency
        ' 
        CoBoxAutoBackupFrequency.DrawMode = DrawMode.OwnerDrawFixed
        CoBoxAutoBackupFrequency.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAutoBackupFrequency.FormattingEnabled = True
        TipSettings.SetImage(CoBoxAutoBackupFrequency, Nothing)
        CoBoxAutoBackupFrequency.Location = New Point(526, 118)
        CoBoxAutoBackupFrequency.Name = "CoBoxAutoBackupFrequency"
        CoBoxAutoBackupFrequency.Size = New Size(216, 30)
        CoBoxAutoBackupFrequency.TabIndex = 1105
        TipSettings.SetText(CoBoxAutoBackupFrequency, "Select a backup frequency.")
        ' 
        ' LblAutoBackup
        ' 
        TipSettings.SetImage(LblAutoBackup, Nothing)
        LblAutoBackup.Location = New Point(526, 97)
        LblAutoBackup.Name = "LblAutoBackup"
        LblAutoBackup.Size = New Size(216, 21)
        LblAutoBackup.TabIndex = 1106
        LblAutoBackup.Text = "Auto Backup:"
        TipSettings.SetText(LblAutoBackup, Nothing)
        LblAutoBackup.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ChkBoxAutoPurgeBackups
        ' 
        ChkBoxAutoPurgeBackups.AutoSize = True
        TipSettings.SetImage(ChkBoxAutoPurgeBackups, Nothing)
        ChkBoxAutoPurgeBackups.Location = New Point(550, 149)
        ChkBoxAutoPurgeBackups.Name = "ChkBoxAutoPurgeBackups"
        ChkBoxAutoPurgeBackups.RightToLeft = RightToLeft.Yes
        ChkBoxAutoPurgeBackups.Size = New Size(168, 25)
        ChkBoxAutoPurgeBackups.TabIndex = 1107
        TipSettings.SetText(ChkBoxAutoPurgeBackups, "Automatically purge older auto-saved backups, keeping the most recent 10. Manually saved backups are never deleted.")
        ChkBoxAutoPurgeBackups.Text = "Auto Purge Backups"
        ChkBoxAutoPurgeBackups.UseVisualStyleBackColor = True
        ' 
        ' BtnBackupNow
        ' 
        TipSettings.SetImage(BtnBackupNow, Nothing)
        BtnBackupNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnBackupNow.Location = New Point(550, 12)
        BtnBackupNow.Name = "BtnBackupNow"
        BtnBackupNow.Size = New Size(194, 32)
        BtnBackupNow.TabIndex = 1108
        TipSettings.SetText(BtnBackupNow, "Backup the clip database now.")
        BtnBackupNow.Text = "Backup Now"
        BtnBackupNow.TextAlign = ContentAlignment.MiddleRight
        BtnBackupNow.UseVisualStyleBackColor = True
        ' 
        ' BtnRestoreNow
        ' 
        TipSettings.SetImage(BtnRestoreNow, Nothing)
        BtnRestoreNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnRestoreNow.Location = New Point(550, 50)
        BtnRestoreNow.Name = "BtnRestoreNow"
        BtnRestoreNow.Size = New Size(194, 32)
        BtnRestoreNow.TabIndex = 1109
        TipSettings.SetText(BtnRestoreNow, "Restore a backup file and overwrite the current clip database.")
        BtnRestoreNow.Text = "Restore From Backup"
        BtnRestoreNow.TextAlign = ContentAlignment.MiddleRight
        BtnRestoreNow.UseVisualStyleBackColor = True
        ' 
        ' Settings
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(756, 546)
        Controls.Add(BtnRestoreNow)
        Controls.Add(BtnBackupNow)
        Controls.Add(ChkBoxAutoPurgeBackups)
        Controls.Add(CoBoxAutoBackupFrequency)
        Controls.Add(LblAutoBackup)
        Controls.Add(CoBoxTheme)
        Controls.Add(ChkBoxAutoStartWithWindows)
        Controls.Add(ChkBoxPlaySoundWithNotify)
        Controls.Add(TxtBoxHotKeyShowScratchPad)
        Controls.Add(LblHotKeyShowScratchPad)
        Controls.Add(ChkBoxKeepScratchPadText)
        Controls.Add(ChkBoxShowOpenSourceApp)
        Controls.Add(BtnPurgeNow)
        Controls.Add(ChkBoxAutoPurge)
        Controls.Add(TxtBoxPurgeDays)
        Controls.Add(LblPurgeDays2)
        Controls.Add(LblPurgeDays1)
        Controls.Add(TxtBoxHotKeyShowViewer)
        Controls.Add(LblHotKeyShowViewer)
        Controls.Add(LblHotKeys)
        Controls.Add(TxtBoxHotKeyToggleFavorite)
        Controls.Add(LblHotKeyToggleFavorite)
        Controls.Add(ChkBoxNotifyOnNewClip)
        Controls.Add(ChkBoxBlinkOnNewClip)
        Controls.Add(TxtBoxMaxClipPreviewLength)
        Controls.Add(LblMaxClipPreviewLength)
        Controls.Add(TxtBoxMaxClips)
        Controls.Add(BtnOK)
        Controls.Add(LblMaxClips)
        Controls.Add(ChkBoxThemeAuto)
        Controls.Add(LblTheme)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipSettings.SetImage(Me, Nothing)
        Margin = New Padding(4)
        MaximizeBox = False
        Name = "Settings"
        StartPosition = FormStartPosition.CenterScreen
        TipSettings.SetText(Me, Nothing)
        Text = "Settings"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnOK As Button
    Friend WithEvents LblMaxClips As Label
    Friend WithEvents TxtBoxMaxClips As TextBox
    Friend WithEvents TxtBoxMaxClipPreviewLength As TextBox
    Friend WithEvents LblMaxClipPreviewLength As Label
    Friend WithEvents TipSettings As Skye.UI.ToolTipEX
    Friend WithEvents ChkBoxBlinkOnNewClip As CheckBox
    Friend WithEvents ChkBoxNotifyOnNewClip As CheckBox
    Friend WithEvents CMTxtBox As Skye.UI.TextBoxContextMenu
    Friend WithEvents TxtBoxHotKeyToggleFavorite As TextBox
    Friend WithEvents LblHotKeyToggleFavorite As Label
    Friend WithEvents LblHotKeys As Label
    Friend WithEvents TxtBoxHotKeyShowViewer As TextBox
    Friend WithEvents LblHotKeyShowViewer As Label
    Friend WithEvents LblPurgeDays1 As Label
    Friend WithEvents LblPurgeDays2 As Label
    Friend WithEvents TxtBoxPurgeDays As TextBox
    Friend WithEvents ChkBoxAutoPurge As CheckBox
    Friend WithEvents BtnPurgeNow As Button
    Friend WithEvents ChkBoxShowOpenSourceApp As CheckBox
    Friend WithEvents ChkBoxKeepScratchPadText As CheckBox
    Friend WithEvents TxtBoxHotKeyShowScratchPad As TextBox
    Friend WithEvents LblHotKeyShowScratchPad As Label
    Friend WithEvents ChkBoxPlaySoundWithNotify As CheckBox
    Friend WithEvents ChkBoxAutoStartWithWindows As CheckBox
    Friend WithEvents ChkBoxThemeAuto As CheckBox
    Friend WithEvents LblTheme As Label
    Friend WithEvents CoBoxTheme As Skye.UI.ComboBox
    Friend WithEvents CoBoxAutoBackupFrequency As Skye.UI.ComboBox
    Friend WithEvents LblAutoBackup As Label
    Friend WithEvents ChkBoxAutoPurgeBackups As CheckBox
    Friend WithEvents BtnBackupNow As Button
    Friend WithEvents BtnRestoreNow As Button
End Class
