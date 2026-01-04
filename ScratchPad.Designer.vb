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
        RTB.Size = New Size(800, 399)
        RTB.TabIndex = 0
        RTB.Text = ""
        TipScratchPad.SetText(RTB, Nothing)
        RTB.WordWrap = False
        ' 
        ' PanelBottom
        ' 
        PanelBottom.Controls.Add(ChkBoxKeepText)
        PanelBottom.Controls.Add(BtnExport)
        PanelBottom.Dock = DockStyle.Bottom
        TipScratchPad.SetImage(PanelBottom, Nothing)
        PanelBottom.Location = New Point(0, 399)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New Size(800, 51)
        PanelBottom.TabIndex = 1
        TipScratchPad.SetText(PanelBottom, Nothing)
        ' 
        ' ChkBoxKeepText
        ' 
        ChkBoxKeepText.Appearance = Appearance.Button
        ChkBoxKeepText.Image = My.Resources.Resources.ImageKeepText
        TipScratchPad.SetImage(ChkBoxKeepText, Nothing)
        ChkBoxKeepText.Location = New Point(12, 7)
        ChkBoxKeepText.Name = "ChkBoxKeepText"
        ChkBoxKeepText.Size = New Size(32, 32)
        ChkBoxKeepText.TabIndex = 21
        TipScratchPad.SetText(ChkBoxKeepText, "Keep Text?" & vbCrLf & "Do you wish to save the contents of the ScratchPad for next time?")
        ChkBoxKeepText.UseVisualStyleBackColor = True
        ' 
        ' BtnExport
        ' 
        BtnExport.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnExport.Image = My.Resources.Resources.ImageExport16
        TipScratchPad.SetImage(BtnExport, Nothing)
        BtnExport.Location = New Point(756, 7)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New Size(32, 32)
        BtnExport.TabIndex = 20
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
        Name = "ScratchPad"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        TipScratchPad.SetText(Me, Nothing)
        Text = "ScratchPad"
        PanelBottom.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RTB As Skye.UI.RichTextBox
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents BtnExport As Button
    Friend WithEvents ChkBoxKeepText As CheckBox
    Friend WithEvents TipScratchPad As Skye.UI.ToolTipEX
End Class
