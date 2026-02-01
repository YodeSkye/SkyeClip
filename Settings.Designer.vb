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
        PanelPageSelector = New Panel()
        LVPageSelector = New ListView()
        ILPageSelector = New ImageList(components)
        PanelGeneral = New Panel()
        PanelClips = New Panel()
        PanelHotKeys = New Panel()
        PanelBackup = New Panel()
        PanelControls = New Panel()
        PanelPageSelector.SuspendLayout()
        PanelGeneral.SuspendLayout()
        PanelClips.SuspendLayout()
        PanelHotKeys.SuspendLayout()
        PanelBackup.SuspendLayout()
        PanelControls.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipSettings.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(163, 14)
        BtnOK.Margin = New Padding(4)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 1000
        TipSettings.SetText(BtnOK, Nothing)
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' LblMaxClips
        ' 
        LblMaxClips.AutoSize = True
        TipSettings.SetImage(LblMaxClips, Nothing)
        LblMaxClips.Location = New Point(14, 8)
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
        TxtBoxMaxClips.Location = New Point(16, 29)
        TxtBoxMaxClips.Name = "TxtBoxMaxClips"
        TxtBoxMaxClips.ShortcutsEnabled = False
        TxtBoxMaxClips.Size = New Size(75, 29)
        TxtBoxMaxClips.TabIndex = 100
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
        TxtBoxMaxClipPreviewLength.Location = New Point(116, 29)
        TxtBoxMaxClipPreviewLength.Name = "TxtBoxMaxClipPreviewLength"
        TxtBoxMaxClipPreviewLength.ShortcutsEnabled = False
        TxtBoxMaxClipPreviewLength.Size = New Size(73, 29)
        TxtBoxMaxClipPreviewLength.TabIndex = 110
        TipSettings.SetText(TxtBoxMaxClipPreviewLength, "Maximum text length of each clip in the menu.")
        TxtBoxMaxClipPreviewLength.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblMaxClipPreviewLength
        ' 
        LblMaxClipPreviewLength.AutoSize = True
        TipSettings.SetImage(LblMaxClipPreviewLength, Nothing)
        LblMaxClipPreviewLength.Location = New Point(114, 8)
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
        ChkBoxBlinkOnNewClip.Location = New Point(16, 71)
        ChkBoxBlinkOnNewClip.Name = "ChkBoxBlinkOnNewClip"
        ChkBoxBlinkOnNewClip.Size = New Size(155, 25)
        ChkBoxBlinkOnNewClip.TabIndex = 120
        TipSettings.SetText(ChkBoxBlinkOnNewClip, "Blink the tray icon several times when the clipboard changes.")
        ChkBoxBlinkOnNewClip.Text = "Blink On New Clip"
        ChkBoxBlinkOnNewClip.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxNotifyOnNewClip
        ' 
        ChkBoxNotifyOnNewClip.AutoSize = True
        TipSettings.SetImage(ChkBoxNotifyOnNewClip, Nothing)
        ChkBoxNotifyOnNewClip.Location = New Point(16, 95)
        ChkBoxNotifyOnNewClip.Name = "ChkBoxNotifyOnNewClip"
        ChkBoxNotifyOnNewClip.Size = New Size(164, 25)
        ChkBoxNotifyOnNewClip.TabIndex = 130
        TipSettings.SetText(ChkBoxNotifyOnNewClip, "Show a notification toast when the clipboard changes.")
        ChkBoxNotifyOnNewClip.Text = "Notify On New Clip"
        ChkBoxNotifyOnNewClip.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHotKeyToggleFavorite
        ' 
        TxtBoxHotKeyToggleFavorite.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyToggleFavorite, My.Resources.Resources.ImageFavorites16)
        TxtBoxHotKeyToggleFavorite.Location = New Point(124, 96)
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
        LblHotKeyToggleFavorite.Location = New Point(122, 75)
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
        LblHotKeys.Location = New Point(124, 51)
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
        TxtBoxHotKeyShowViewer.Location = New Point(124, 149)
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
        LblHotKeyShowViewer.Location = New Point(122, 128)
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
        LblPurgeDays1.Location = New Point(12, 158)
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
        LblPurgeDays2.Location = New Point(234, 158)
        LblPurgeDays2.Name = "LblPurgeDays2"
        LblPurgeDays2.Size = New Size(44, 21)
        LblPurgeDays2.TabIndex = 13
        LblPurgeDays2.Text = "Days"
        TipSettings.SetText(LblPurgeDays2, Nothing)
        ' 
        ' TxtBoxPurgeDays
        ' 
        TipSettings.SetImage(TxtBoxPurgeDays, Nothing)
        TxtBoxPurgeDays.Location = New Point(178, 155)
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
        ChkBoxAutoPurge.Location = New Point(16, 178)
        ChkBoxAutoPurge.Name = "ChkBoxAutoPurge"
        ChkBoxAutoPurge.Size = New Size(109, 25)
        ChkBoxAutoPurge.TabIndex = 145
        TipSettings.SetText(ChkBoxAutoPurge, "Auto-Purge Clips Once Each Day on Startup.")
        ChkBoxAutoPurge.Text = "Auto-Purge"
        ChkBoxAutoPurge.UseVisualStyleBackColor = True
        ' 
        ' BtnPurgeNow
        ' 
        TipSettings.SetImage(BtnPurgeNow, Nothing)
        BtnPurgeNow.Location = New Point(157, 187)
        BtnPurgeNow.Name = "BtnPurgeNow"
        BtnPurgeNow.Size = New Size(99, 32)
        BtnPurgeNow.TabIndex = 160
        TipSettings.SetText(BtnPurgeNow, "Purge Clips Now")
        BtnPurgeNow.Text = "Purge Now"
        BtnPurgeNow.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxShowOpenSourceApp
        ' 
        ChkBoxShowOpenSourceApp.AutoSize = True
        TipSettings.SetImage(ChkBoxShowOpenSourceApp, Nothing)
        ChkBoxShowOpenSourceApp.Location = New Point(16, 166)
        ChkBoxShowOpenSourceApp.Name = "ChkBoxShowOpenSourceApp"
        ChkBoxShowOpenSourceApp.Size = New Size(194, 25)
        ChkBoxShowOpenSourceApp.TabIndex = 50
        TipSettings.SetText(ChkBoxShowOpenSourceApp, "Controls whether ""Open Source App"" appears as an Option in the Clip Context Menu.")
        ChkBoxShowOpenSourceApp.Text = "Show Open Source App"
        ChkBoxShowOpenSourceApp.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxKeepScratchPadText
        ' 
        ChkBoxKeepScratchPadText.AutoSize = True
        TipSettings.SetImage(ChkBoxKeepScratchPadText, Nothing)
        ChkBoxKeepScratchPadText.Location = New Point(16, 197)
        ChkBoxKeepScratchPadText.Name = "ChkBoxKeepScratchPadText"
        ChkBoxKeepScratchPadText.Size = New Size(177, 25)
        ChkBoxKeepScratchPadText.TabIndex = 60
        TipSettings.SetText(ChkBoxKeepScratchPadText, "Keep the contents of the Scratch Pad between sessions?")
        ChkBoxKeepScratchPadText.Text = "Keep Scratch Pad Text"
        ChkBoxKeepScratchPadText.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHotKeyShowScratchPad
        ' 
        TxtBoxHotKeyShowScratchPad.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyShowScratchPad, My.Resources.Resources.imageScratchPad16)
        TxtBoxHotKeyShowScratchPad.Location = New Point(124, 202)
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
        LblHotKeyShowScratchPad.Location = New Point(122, 181)
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
        ChkBoxPlaySoundWithNotify.Location = New Point(16, 119)
        ChkBoxPlaySoundWithNotify.Name = "ChkBoxPlaySoundWithNotify"
        ChkBoxPlaySoundWithNotify.Size = New Size(191, 25)
        ChkBoxPlaySoundWithNotify.TabIndex = 140
        TipSettings.SetText(ChkBoxPlaySoundWithNotify, "Play a Sound with the Notification Toast.")
        ChkBoxPlaySoundWithNotify.Text = "Play Sound With Notify"
        ChkBoxPlaySoundWithNotify.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxAutoStartWithWindows
        ' 
        ChkBoxAutoStartWithWindows.AutoSize = True
        TipSettings.SetImage(ChkBoxAutoStartWithWindows, Nothing)
        ChkBoxAutoStartWithWindows.Location = New Point(16, 135)
        ChkBoxAutoStartWithWindows.Name = "ChkBoxAutoStartWithWindows"
        ChkBoxAutoStartWithWindows.Size = New Size(206, 25)
        ChkBoxAutoStartWithWindows.TabIndex = 40
        TipSettings.SetText(ChkBoxAutoStartWithWindows, "Auto-Start the App with Windows.")
        ChkBoxAutoStartWithWindows.Text = "Auto-Start With Windows"
        ChkBoxAutoStartWithWindows.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxThemeAuto
        ' 
        ChkBoxThemeAuto.AutoSize = True
        TipSettings.SetImage(ChkBoxThemeAuto, Nothing)
        ChkBoxThemeAuto.Location = New Point(16, 60)
        ChkBoxThemeAuto.Name = "ChkBoxThemeAuto"
        ChkBoxThemeAuto.Size = New Size(205, 25)
        ChkBoxThemeAuto.TabIndex = 20
        TipSettings.SetText(ChkBoxThemeAuto, "Automatically set the theme to match current windows settings.")
        ChkBoxThemeAuto.Text = "Auto Sync With Windows"
        ChkBoxThemeAuto.UseVisualStyleBackColor = True
        ' 
        ' LblTheme
        ' 
        LblTheme.AutoSize = True
        TipSettings.SetImage(LblTheme, Nothing)
        LblTheme.Location = New Point(16, 12)
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
        CoBoxTheme.Location = New Point(16, 33)
        CoBoxTheme.Name = "CoBoxTheme"
        CoBoxTheme.Size = New Size(216, 30)
        CoBoxTheme.TabIndex = 10
        TipSettings.SetText(CoBoxTheme, "Select a Theme.")
        ' 
        ' CoBoxAutoBackupFrequency
        ' 
        CoBoxAutoBackupFrequency.DrawMode = DrawMode.OwnerDrawFixed
        CoBoxAutoBackupFrequency.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAutoBackupFrequency.FormattingEnabled = True
        TipSettings.SetImage(CoBoxAutoBackupFrequency, My.Resources.Resources.ImageBackup16)
        CoBoxAutoBackupFrequency.Location = New Point(15, 163)
        CoBoxAutoBackupFrequency.Name = "CoBoxAutoBackupFrequency"
        CoBoxAutoBackupFrequency.Size = New Size(216, 30)
        CoBoxAutoBackupFrequency.TabIndex = 340
        TipSettings.SetText(CoBoxAutoBackupFrequency, "Select a backup frequency." & vbCrLf & "Automatic Backups are stored in ")
        ' 
        ' LblAutoBackup
        ' 
        TipSettings.SetImage(LblAutoBackup, Nothing)
        LblAutoBackup.Location = New Point(15, 142)
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
        TipSettings.SetImage(ChkBoxAutoPurgeBackups, My.Resources.Resources.ImageBackup16)
        ChkBoxAutoPurgeBackups.Location = New Point(39, 194)
        ChkBoxAutoPurgeBackups.Name = "ChkBoxAutoPurgeBackups"
        ChkBoxAutoPurgeBackups.RightToLeft = RightToLeft.Yes
        ChkBoxAutoPurgeBackups.Size = New Size(168, 25)
        ChkBoxAutoPurgeBackups.TabIndex = 350
        TipSettings.SetText(ChkBoxAutoPurgeBackups, "Automatically purge older auto-saved backups, keeping the most recent 10." & vbCrLf & "Manually saved backups are never deleted.")
        ChkBoxAutoPurgeBackups.Text = "Auto Purge Backups"
        ChkBoxAutoPurgeBackups.UseVisualStyleBackColor = True
        ' 
        ' BtnBackupNow
        ' 
        BtnBackupNow.Image = My.Resources.Resources.ImageBackup16
        TipSettings.SetImage(BtnBackupNow, My.Resources.Resources.ImageBackup16)
        BtnBackupNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnBackupNow.Location = New Point(13, 16)
        BtnBackupNow.Name = "BtnBackupNow"
        BtnBackupNow.Size = New Size(194, 32)
        BtnBackupNow.TabIndex = 300
        TipSettings.SetText(BtnBackupNow, "Backup the clip database now." & vbCrLf & "Backups are located in ")
        BtnBackupNow.Text = "Backup Now"
        BtnBackupNow.TextAlign = ContentAlignment.MiddleRight
        BtnBackupNow.UseVisualStyleBackColor = True
        ' 
        ' BtnRestoreNow
        ' 
        BtnRestoreNow.Image = My.Resources.Resources.ImageRestore16
        TipSettings.SetImage(BtnRestoreNow, My.Resources.Resources.ImageRestore16)
        BtnRestoreNow.ImageAlign = ContentAlignment.MiddleLeft
        BtnRestoreNow.Location = New Point(13, 54)
        BtnRestoreNow.Name = "BtnRestoreNow"
        BtnRestoreNow.Size = New Size(194, 32)
        BtnRestoreNow.TabIndex = 310
        TipSettings.SetText(BtnRestoreNow, "Restore a backup file and overwrite the current clip database.")
        BtnRestoreNow.Text = "Restore From Backup"
        BtnRestoreNow.TextAlign = ContentAlignment.MiddleRight
        BtnRestoreNow.UseVisualStyleBackColor = True
        ' 
        ' PanelPageSelector
        ' 
        PanelPageSelector.Controls.Add(LVPageSelector)
        PanelPageSelector.Dock = DockStyle.Left
        TipSettings.SetImage(PanelPageSelector, Nothing)
        PanelPageSelector.Location = New Point(0, 0)
        PanelPageSelector.Name = "PanelPageSelector"
        PanelPageSelector.Size = New Size(93, 361)
        PanelPageSelector.TabIndex = 1110
        TipSettings.SetText(PanelPageSelector, Nothing)
        ' 
        ' LVPageSelector
        ' 
        LVPageSelector.AutoArrange = False
        LVPageSelector.BorderStyle = BorderStyle.FixedSingle
        LVPageSelector.Dock = DockStyle.Fill
        LVPageSelector.HeaderStyle = ColumnHeaderStyle.None
        TipSettings.SetImage(LVPageSelector, Nothing)
        LVPageSelector.LargeImageList = ILPageSelector
        LVPageSelector.Location = New Point(0, 0)
        LVPageSelector.MultiSelect = False
        LVPageSelector.Name = "LVPageSelector"
        LVPageSelector.Scrollable = False
        LVPageSelector.ShowGroups = False
        LVPageSelector.Size = New Size(93, 361)
        LVPageSelector.TabIndex = 0
        LVPageSelector.TabStop = False
        TipSettings.SetText(LVPageSelector, Nothing)
        LVPageSelector.UseCompatibleStateImageBehavior = False
        ' 
        ' ILPageSelector
        ' 
        ILPageSelector.ColorDepth = ColorDepth.Depth32Bit
        ILPageSelector.ImageSize = New Size(48, 48)
        ILPageSelector.TransparentColor = Color.Transparent
        ' 
        ' PanelGeneral
        ' 
        PanelGeneral.AutoSizeMode = AutoSizeMode.GrowAndShrink
        PanelGeneral.Controls.Add(CoBoxTheme)
        PanelGeneral.Controls.Add(LblTheme)
        PanelGeneral.Controls.Add(ChkBoxThemeAuto)
        PanelGeneral.Controls.Add(ChkBoxAutoStartWithWindows)
        PanelGeneral.Controls.Add(ChkBoxShowOpenSourceApp)
        PanelGeneral.Controls.Add(ChkBoxKeepScratchPadText)
        PanelGeneral.Dock = DockStyle.Fill
        TipSettings.SetImage(PanelGeneral, Nothing)
        PanelGeneral.Location = New Point(93, 0)
        PanelGeneral.Name = "PanelGeneral"
        PanelGeneral.Size = New Size(391, 270)
        PanelGeneral.TabIndex = 1111
        TipSettings.SetText(PanelGeneral, Nothing)
        ' 
        ' PanelClips
        ' 
        PanelClips.Controls.Add(TxtBoxMaxClipPreviewLength)
        PanelClips.Controls.Add(LblMaxClips)
        PanelClips.Controls.Add(TxtBoxMaxClips)
        PanelClips.Controls.Add(LblMaxClipPreviewLength)
        PanelClips.Controls.Add(TxtBoxPurgeDays)
        PanelClips.Controls.Add(LblPurgeDays1)
        PanelClips.Controls.Add(LblPurgeDays2)
        PanelClips.Controls.Add(ChkBoxAutoPurge)
        PanelClips.Controls.Add(BtnPurgeNow)
        PanelClips.Controls.Add(ChkBoxPlaySoundWithNotify)
        PanelClips.Controls.Add(ChkBoxNotifyOnNewClip)
        PanelClips.Controls.Add(ChkBoxBlinkOnNewClip)
        PanelClips.Dock = DockStyle.Fill
        TipSettings.SetImage(PanelClips, Nothing)
        PanelClips.Location = New Point(0, 0)
        PanelClips.Name = "PanelClips"
        PanelClips.Size = New Size(484, 361)
        PanelClips.TabIndex = 1112
        TipSettings.SetText(PanelClips, Nothing)
        ' 
        ' PanelHotKeys
        ' 
        PanelHotKeys.Controls.Add(LblHotKeys)
        PanelHotKeys.Controls.Add(TxtBoxHotKeyToggleFavorite)
        PanelHotKeys.Controls.Add(TxtBoxHotKeyShowViewer)
        PanelHotKeys.Controls.Add(TxtBoxHotKeyShowScratchPad)
        PanelHotKeys.Controls.Add(LblHotKeyShowViewer)
        PanelHotKeys.Controls.Add(LblHotKeyToggleFavorite)
        PanelHotKeys.Controls.Add(LblHotKeyShowScratchPad)
        PanelHotKeys.Dock = DockStyle.Fill
        TipSettings.SetImage(PanelHotKeys, Nothing)
        PanelHotKeys.Location = New Point(0, 0)
        PanelHotKeys.Name = "PanelHotKeys"
        PanelHotKeys.Size = New Size(484, 361)
        PanelHotKeys.TabIndex = 1113
        TipSettings.SetText(PanelHotKeys, Nothing)
        ' 
        ' PanelBackup
        ' 
        PanelBackup.AutoSizeMode = AutoSizeMode.GrowAndShrink
        PanelBackup.Controls.Add(BtnBackupNow)
        PanelBackup.Controls.Add(LblAutoBackup)
        PanelBackup.Controls.Add(CoBoxAutoBackupFrequency)
        PanelBackup.Controls.Add(ChkBoxAutoPurgeBackups)
        PanelBackup.Controls.Add(BtnRestoreNow)
        PanelBackup.Dock = DockStyle.Fill
        TipSettings.SetImage(PanelBackup, Nothing)
        PanelBackup.Location = New Point(0, 0)
        PanelBackup.Name = "PanelBackup"
        PanelBackup.Size = New Size(484, 361)
        PanelBackup.TabIndex = 1114
        TipSettings.SetText(PanelBackup, Nothing)
        ' 
        ' PanelControls
        ' 
        PanelControls.Controls.Add(BtnOK)
        PanelControls.Dock = DockStyle.Bottom
        TipSettings.SetImage(PanelControls, Nothing)
        PanelControls.Location = New Point(93, 270)
        PanelControls.Name = "PanelControls"
        PanelControls.Size = New Size(391, 91)
        PanelControls.TabIndex = 1115
        TipSettings.SetText(PanelControls, Nothing)
        ' 
        ' Settings
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(484, 361)
        Controls.Add(PanelGeneral)
        Controls.Add(PanelControls)
        Controls.Add(PanelPageSelector)
        Controls.Add(PanelClips)
        Controls.Add(PanelHotKeys)
        Controls.Add(PanelBackup)
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
        PanelPageSelector.ResumeLayout(False)
        PanelGeneral.ResumeLayout(False)
        PanelGeneral.PerformLayout()
        PanelClips.ResumeLayout(False)
        PanelClips.PerformLayout()
        PanelHotKeys.ResumeLayout(False)
        PanelHotKeys.PerformLayout()
        PanelBackup.ResumeLayout(False)
        PanelBackup.PerformLayout()
        PanelControls.ResumeLayout(False)
        ResumeLayout(False)
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
    Friend WithEvents PanelPageSelector As Panel
    Friend WithEvents LVPageSelector As ListView
    Friend WithEvents ILPageSelector As ImageList
    Friend WithEvents PanelGeneral As Panel
    Friend WithEvents PanelClips As Panel
    Friend WithEvents PanelHotKeys As Panel
    Friend WithEvents PanelBackup As Panel
    Friend WithEvents PanelControls As Panel
End Class
