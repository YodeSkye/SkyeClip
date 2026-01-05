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
        PanelBottom = New Panel()
        BtnOK = New Button()
        ChkBoxKeepText = New CheckBox()
        BtnExport = New Button()
        TipScratchPad = New Skye.UI.ToolTipEX(components)
        PanelBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' RTB
        ' 
        RTB.BorderStyle = BorderStyle.None
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
        ' PanelBottom
        ' 
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
        ' BtnOK
        ' 
        BtnOK.Anchor = AnchorStyles.Bottom
        BtnOK.Image = My.Resources.Resources.ImageOK
        TipScratchPad.SetImage(BtnOK, My.Resources.Resources.ImageOK16)
        BtnOK.Location = New Point(368, 6)
        BtnOK.Name = "BtnOK"
        BtnOK.Size = New Size(64, 64)
        BtnOK.TabIndex = 22
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
        TipScratchPad.SetText(ChkBoxKeepText, "Keep Text?" & vbCrLf & "Do you wish to save the contents of the ScratchPad for next time?")
        ChkBoxKeepText.UseVisualStyleBackColor = True
        ' 
        ' BtnExport
        ' 
        BtnExport.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnExport.Image = My.Resources.Resources.ImageExport16
        TipScratchPad.SetImage(BtnExport, My.Resources.Resources.ImageExport16)
        BtnExport.Location = New Point(756, 38)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New Size(32, 32)
        BtnExport.TabIndex = 30
        TipScratchPad.SetText(BtnExport, "Export" & vbCrLf & "Saves the contents of the ScratchPad to a file.")
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
        PanelBottom.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RTB As Skye.UI.RichTextBox
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents BtnExport As Button
    Friend WithEvents ChkBoxKeepText As CheckBox
    Friend WithEvents TipScratchPad As Skye.UI.ToolTipEX
    Friend WithEvents BtnOK As Button
End Class
