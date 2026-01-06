<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScratchPad
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScratchPad))
        RTB = New Skye.UI.RichTextBox()
        CMRTB = New ContextMenuStrip(components)
        CMIUndo = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        CMICut = New ToolStripMenuItem()
        CMICopy = New ToolStripMenuItem()
        CMIPaste = New ToolStripMenuItem()
        CMIDelete = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        CMISelectAll = New ToolStripMenuItem()
        PanelBottom = New Panel()
        BtnHelp = New Button()
        BtnOK = New Button()
        ChkBoxKeepText = New CheckBox()
        BtnExport = New Button()
        TipScratchPad = New Skye.UI.ToolTipEX(components)
        CMRTB.SuspendLayout()
        PanelBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' RTB
        ' 
        RTB.BorderStyle = BorderStyle.None
        RTB.ContextMenuStrip = CMRTB
        RTB.Dock = DockStyle.Fill
        TipScratchPad.SetImage(RTB, Nothing)
        RTB.Location = New Point(0, 0)
        RTB.Name = "RTB"
        RTB.ScrollBars = RichTextBoxScrollBars.ForcedBoth
        RTB.ShortcutsEnabled = False
        RTB.ShowSelectionMargin = True
        RTB.Size = New Size(800, 368)
        RTB.TabIndex = 0
        RTB.Text = ""
        TipScratchPad.SetText(RTB, Nothing)
        RTB.WordWrap = False
        ' 
        ' CMRTB
        ' 
        TipScratchPad.SetImage(CMRTB, Nothing)
        CMRTB.Items.AddRange(New ToolStripItem() {CMIUndo, ToolStripSeparator1, CMICut, CMICopy, CMIPaste, CMIDelete, ToolStripSeparator2, CMISelectAll})
        CMRTB.Name = "CMRTB"
        CMRTB.Size = New Size(142, 148)
        TipScratchPad.SetText(CMRTB, Nothing)
        ' 
        ' CMIUndo
        ' 
        CMIUndo.Image = My.Resources.Resources.ImageEditUndo16
        CMIUndo.Name = "CMIUndo"
        CMIUndo.Size = New Size(141, 22)
        CMIUndo.Text = "Undo / Redo"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(138, 6)
        ' 
        ' CMICut
        ' 
        CMICut.Image = My.Resources.Resources.ImageEditCut16
        CMICut.Name = "CMICut"
        CMICut.Size = New Size(141, 22)
        CMICut.Text = "Cut"
        ' 
        ' CMICopy
        ' 
        CMICopy.Image = My.Resources.Resources.ImageEditCopy16
        CMICopy.Name = "CMICopy"
        CMICopy.Size = New Size(141, 22)
        CMICopy.Text = "Copy"
        ' 
        ' CMIPaste
        ' 
        CMIPaste.Image = My.Resources.Resources.ImageEditPaste16
        CMIPaste.Name = "CMIPaste"
        CMIPaste.Size = New Size(141, 22)
        CMIPaste.Text = "Paste"
        ' 
        ' CMIDelete
        ' 
        CMIDelete.Image = My.Resources.Resources.ImageEditDelete16
        CMIDelete.Name = "CMIDelete"
        CMIDelete.Size = New Size(141, 22)
        CMIDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(138, 6)
        ' 
        ' CMISelectAll
        ' 
        CMISelectAll.Image = My.Resources.Resources.ImageEditSelectAll16
        CMISelectAll.Name = "CMISelectAll"
        CMISelectAll.Size = New Size(141, 22)
        CMISelectAll.Text = "Select All"
        ' 
        ' PanelBottom
        ' 
        PanelBottom.Controls.Add(BtnHelp)
        PanelBottom.Controls.Add(BtnOK)
        PanelBottom.Controls.Add(ChkBoxKeepText)
        PanelBottom.Controls.Add(BtnExport)
        PanelBottom.Dock = DockStyle.Bottom
        TipScratchPad.SetImage(PanelBottom, Nothing)
        PanelBottom.Location = New Point(0, 368)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New Size(800, 82)
        PanelBottom.TabIndex = 1
        TipScratchPad.SetText(PanelBottom, Nothing)
        ' 
        ' BtnHelp
        ' 
        BtnHelp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnHelp.Image = My.Resources.Resources.ImageHelp16
        TipScratchPad.SetImage(BtnHelp, My.Resources.Resources.ImageExport16)
        BtnHelp.Location = New Point(756, 38)
        BtnHelp.Name = "BtnHelp"
        BtnHelp.Size = New Size(32, 32)
        BtnHelp.TabIndex = 40
        TipScratchPad.SetText(BtnHelp, "Help")
        BtnHelp.UseVisualStyleBackColor = True
        ' 
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipScratchPad.SetImage(BtnOK, My.Resources.Resources.ImageOK16)
        BtnOK.Location = New Point(368, 6)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 30
        TipScratchPad.SetText(BtnOK, "Close Scratch Pad")
        BtnOK.UseVisualStyleBackColor = True
        ' 
        ' ChkBoxKeepText
        ' 
        ChkBoxKeepText.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ChkBoxKeepText.Appearance = Appearance.Button
        ChkBoxKeepText.Image = My.Resources.Resources.ImageKeepText16
        TipScratchPad.SetImage(ChkBoxKeepText, My.Resources.Resources.ImageKeepText16)
        ChkBoxKeepText.Location = New Point(12, 38)
        ChkBoxKeepText.Name = "ChkBoxKeepText"
        ChkBoxKeepText.Size = New Size(32, 32)
        ChkBoxKeepText.TabIndex = 10
        TipScratchPad.SetText(ChkBoxKeepText, "Keep Text?" & vbCrLf & "Do you wish to save the contents of the Scratch Pad for next time?")
        ChkBoxKeepText.UseVisualStyleBackColor = True
        ' 
        ' BtnExport
        ' 
        BtnExport.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnExport.Image = My.Resources.Resources.ImageExport16
        TipScratchPad.SetImage(BtnExport, My.Resources.Resources.ImageExport16)
        BtnExport.Location = New Point(50, 38)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New Size(32, 32)
        BtnExport.TabIndex = 20
        TipScratchPad.SetText(BtnExport, "Export" & vbCrLf & "Saves the contents of the Scratch Pad to a file." & vbCrLf & "Right-Click = Load the contents of a file to the Scratch Pad (Clears Current Text)." & vbCrLf & "Both plain text and rich text files are supported.")
        BtnExport.UseVisualStyleBackColor = True
        ' 
        ' TipScratchPad
        ' 
        TipScratchPad.Font = New Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ' 
        ' ScratchPad
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(RTB)
        Controls.Add(PanelBottom)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipScratchPad.SetImage(Me, Nothing)
        KeyPreview = True
        Name = "ScratchPad"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        TipScratchPad.SetText(Me, Nothing)
        Text = "Scratch Pad"
        CMRTB.ResumeLayout(False)
        PanelBottom.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RTB As Skye.UI.RichTextBox
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents BtnExport As Button
    Friend WithEvents ChkBoxKeepText As CheckBox
    Friend WithEvents TipScratchPad As Skye.UI.ToolTipEX
    Friend WithEvents BtnOK As Button
    Friend WithEvents CMRTB As ContextMenuStrip
    Friend WithEvents CMIUndo As ToolStripMenuItem
    Friend WithEvents CMICut As ToolStripMenuItem
    Friend WithEvents CMICopy As ToolStripMenuItem
    Friend WithEvents CMIPaste As ToolStripMenuItem
    Friend WithEvents CMIDelete As ToolStripMenuItem
    Friend WithEvents CMISelectAll As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents BtnHelp As Button
End Class
