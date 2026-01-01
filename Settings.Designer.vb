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
        SuspendLayout()
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipSettings.SetImage(BtnOK, Nothing)
        BtnOK.Location = New Point(210, 430)
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
        TipSettings.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ' 
        ' ChkBoxBlinkOnNewClip
        ' 
        ChkBoxBlinkOnNewClip.AutoSize = True
        TipSettings.SetImage(ChkBoxBlinkOnNewClip, Nothing)
        ChkBoxBlinkOnNewClip.Location = New Point(317, 12)
        ChkBoxBlinkOnNewClip.Name = "ChkBoxBlinkOnNewClip"
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
        ChkBoxNotifyOnNewClip.Location = New Point(317, 43)
        ChkBoxNotifyOnNewClip.Name = "ChkBoxNotifyOnNewClip"
        ChkBoxNotifyOnNewClip.Size = New Size(164, 25)
        ChkBoxNotifyOnNewClip.TabIndex = 40
        TipSettings.SetText(ChkBoxNotifyOnNewClip, "Show a notification toast when the clipboard changes.")
        ChkBoxNotifyOnNewClip.Text = "Notify On New Clip"
        ChkBoxNotifyOnNewClip.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxHotKeyToggleFavorite
        ' 
        TxtBoxHotKeyToggleFavorite.BorderStyle = BorderStyle.FixedSingle
        TipSettings.SetImage(TxtBoxHotKeyToggleFavorite, Nothing)
        TxtBoxHotKeyToggleFavorite.Location = New Point(171, 202)
        TxtBoxHotKeyToggleFavorite.Name = "TxtBoxHotKeyToggleFavorite"
        TxtBoxHotKeyToggleFavorite.ReadOnly = True
        TxtBoxHotKeyToggleFavorite.ShortcutsEnabled = False
        TxtBoxHotKeyToggleFavorite.Size = New Size(144, 29)
        TxtBoxHotKeyToggleFavorite.TabIndex = 50
        TipSettings.SetText(TxtBoxHotKeyToggleFavorite, "Key or Key Combination to use on the tray menu to favorite a clip.")
        TxtBoxHotKeyToggleFavorite.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHotKeyToggleFavorite
        ' 
        TipSettings.SetImage(LblHotKeyToggleFavorite, Nothing)
        LblHotKeyToggleFavorite.Location = New Point(169, 181)
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
        LblHotKeys.Location = New Point(171, 157)
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
        TipSettings.SetImage(TxtBoxHotKeyShowViewer, Nothing)
        TxtBoxHotKeyShowViewer.Location = New Point(171, 255)
        TxtBoxHotKeyShowViewer.Name = "TxtBoxHotKeyShowViewer"
        TxtBoxHotKeyShowViewer.ReadOnly = True
        TxtBoxHotKeyShowViewer.ShortcutsEnabled = False
        TxtBoxHotKeyShowViewer.Size = New Size(144, 29)
        TxtBoxHotKeyShowViewer.TabIndex = 60
        TipSettings.SetText(TxtBoxHotKeyShowViewer, "Key or Key Combination to use on the tray menu to show the clip viewer.")
        TxtBoxHotKeyShowViewer.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHotKeyShowViewer
        ' 
        TipSettings.SetImage(LblHotKeyShowViewer, Nothing)
        LblHotKeyShowViewer.Location = New Point(169, 234)
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
        LblPurgeDays1.Location = New Point(10, 332)
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
        LblPurgeDays2.Location = New Point(232, 332)
        LblPurgeDays2.Name = "LblPurgeDays2"
        LblPurgeDays2.Size = New Size(44, 21)
        LblPurgeDays2.TabIndex = 13
        LblPurgeDays2.Text = "Days"
        TipSettings.SetText(LblPurgeDays2, Nothing)
        ' 
        ' TxtBoxPurgeDays
        ' 
        TipSettings.SetImage(TxtBoxPurgeDays, Nothing)
        TxtBoxPurgeDays.Location = New Point(176, 329)
        TxtBoxPurgeDays.Name = "TxtBoxPurgeDays"
        TxtBoxPurgeDays.Size = New Size(58, 29)
        TxtBoxPurgeDays.TabIndex = 70
        TipSettings.SetText(TxtBoxPurgeDays, "Purge Clips Older than x Days")
        TxtBoxPurgeDays.TextAlign = HorizontalAlignment.Center
        ' 
        ' ChkBoxAutoPurge
        ' 
        ChkBoxAutoPurge.AutoSize = True
        TipSettings.SetImage(ChkBoxAutoPurge, Nothing)
        ChkBoxAutoPurge.Location = New Point(325, 333)
        ChkBoxAutoPurge.Name = "ChkBoxAutoPurge"
        ChkBoxAutoPurge.Size = New Size(109, 25)
        ChkBoxAutoPurge.TabIndex = 80
        TipSettings.SetText(ChkBoxAutoPurge, "Auto-Purge Clips Once Each Day on Startup.")
        ChkBoxAutoPurge.Text = "Auto-Purge"
        ChkBoxAutoPurge.UseVisualStyleBackColor = True
        ' 
        ' BtnPurgeNow
        ' 
        TipSettings.SetImage(BtnPurgeNow, Nothing)
        BtnPurgeNow.Location = New Point(155, 361)
        BtnPurgeNow.Name = "BtnPurgeNow"
        BtnPurgeNow.Size = New Size(99, 32)
        BtnPurgeNow.TabIndex = 1000
        BtnPurgeNow.TabStop = False
        TipSettings.SetText(BtnPurgeNow, "Purge Clips Now")
        BtnPurgeNow.Text = "Purge Now"
        BtnPurgeNow.UseVisualStyleBackColor = True
        ' 
        ' Settings
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(484, 507)
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
End Class
