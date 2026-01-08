<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClipExplorer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClipExplorer))
        StatusStripCE = New StatusStrip()
        TSSLabelCount = New ToolStripStatusLabel()
        PanelCE = New Panel()
        GrpBoxSearch = New GroupBox()
        RadBtnHTML = New RadioButton()
        RadBtnPlainText = New RadioButton()
        RadBtnAllText = New RadioButton()
        RadBtnRTF = New RadioButton()
        TxtBoxDays = New TextBox()
        CMTxtBox = New Skye.UI.TextBoxContextMenu()
        ChkBoxFavorites = New CheckBox()
        BtnClearSearch = New Button()
        TxtBoxSearch = New TextBox()
        SplitContainerCE = New SplitContainer()
        DGV = New DataGridView()
        RTB = New RichTextBox()
        TipClipExplorer = New Skye.UI.ToolTipEX(components)
        StatusStripCE.SuspendLayout()
        PanelCE.SuspendLayout()
        GrpBoxSearch.SuspendLayout()
        CType(SplitContainerCE, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerCE.Panel1.SuspendLayout()
        SplitContainerCE.Panel2.SuspendLayout()
        SplitContainerCE.SuspendLayout()
        CType(DGV, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' StatusStripCE
        ' 
        TipClipExplorer.SetImage(StatusStripCE, Nothing)
        StatusStripCE.Items.AddRange(New ToolStripItem() {TSSLabelCount})
        StatusStripCE.Location = New Point(0, 428)
        StatusStripCE.Name = "StatusStripCE"
        StatusStripCE.Size = New Size(800, 22)
        StatusStripCE.TabIndex = 0
        TipClipExplorer.SetText(StatusStripCE, Nothing)
        ' 
        ' TSSLabelCount
        ' 
        TSSLabelCount.Name = "TSSLabelCount"
        TSSLabelCount.Size = New Size(113, 17)
        TSSLabelCount.Text = "Showing x of y Clips"
        ' 
        ' PanelCE
        ' 
        PanelCE.BorderStyle = BorderStyle.FixedSingle
        PanelCE.Controls.Add(GrpBoxSearch)
        PanelCE.Controls.Add(TxtBoxDays)
        PanelCE.Controls.Add(ChkBoxFavorites)
        PanelCE.Controls.Add(BtnClearSearch)
        PanelCE.Controls.Add(TxtBoxSearch)
        PanelCE.Dock = DockStyle.Top
        PanelCE.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipClipExplorer.SetImage(PanelCE, Nothing)
        PanelCE.Location = New Point(0, 0)
        PanelCE.Name = "PanelCE"
        PanelCE.Size = New Size(800, 55)
        PanelCE.TabIndex = 0
        PanelCE.TabStop = True
        TipClipExplorer.SetText(PanelCE, Nothing)
        ' 
        ' GrpBoxSearch
        ' 
        GrpBoxSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        GrpBoxSearch.Controls.Add(RadBtnHTML)
        GrpBoxSearch.Controls.Add(RadBtnPlainText)
        GrpBoxSearch.Controls.Add(RadBtnAllText)
        GrpBoxSearch.Controls.Add(RadBtnRTF)
        TipClipExplorer.SetImage(GrpBoxSearch, Nothing)
        GrpBoxSearch.Location = New Point(597, -10)
        GrpBoxSearch.Name = "GrpBoxSearch"
        GrpBoxSearch.Size = New Size(200, 63)
        GrpBoxSearch.TabIndex = 5
        GrpBoxSearch.TabStop = False
        TipClipExplorer.SetText(GrpBoxSearch, Nothing)
        ' 
        ' RadBtnHTML
        ' 
        RadBtnHTML.AutoSize = True
        TipClipExplorer.SetImage(RadBtnHTML, Nothing)
        RadBtnHTML.Location = New Point(114, 14)
        RadBtnHTML.Name = "RadBtnHTML"
        RadBtnHTML.Size = New Size(69, 25)
        RadBtnHTML.TabIndex = 8
        RadBtnHTML.TabStop = True
        TipClipExplorer.SetText(RadBtnHTML, Nothing)
        RadBtnHTML.Text = "HTML"
        RadBtnHTML.UseVisualStyleBackColor = True
        ' 
        ' RadBtnPlainText
        ' 
        RadBtnPlainText.AutoSize = True
        TipClipExplorer.SetImage(RadBtnPlainText, Nothing)
        RadBtnPlainText.Location = New Point(16, 14)
        RadBtnPlainText.Name = "RadBtnPlainText"
        RadBtnPlainText.Size = New Size(92, 25)
        RadBtnPlainText.TabIndex = 6
        RadBtnPlainText.TabStop = True
        TipClipExplorer.SetText(RadBtnPlainText, Nothing)
        RadBtnPlainText.Text = "Plain Text"
        RadBtnPlainText.UseVisualStyleBackColor = True
        ' 
        ' RadBtnAllText
        ' 
        RadBtnAllText.AutoSize = True
        TipClipExplorer.SetImage(RadBtnAllText, Nothing)
        RadBtnAllText.Location = New Point(114, 34)
        RadBtnAllText.Name = "RadBtnAllText"
        RadBtnAllText.Size = New Size(76, 25)
        RadBtnAllText.TabIndex = 9
        RadBtnAllText.TabStop = True
        TipClipExplorer.SetText(RadBtnAllText, Nothing)
        RadBtnAllText.Text = "All Text"
        RadBtnAllText.UseVisualStyleBackColor = True
        ' 
        ' RadBtnRTF
        ' 
        RadBtnRTF.AutoSize = True
        TipClipExplorer.SetImage(RadBtnRTF, Nothing)
        RadBtnRTF.Location = New Point(16, 34)
        RadBtnRTF.Name = "RadBtnRTF"
        RadBtnRTF.Size = New Size(53, 25)
        RadBtnRTF.TabIndex = 7
        RadBtnRTF.TabStop = True
        TipClipExplorer.SetText(RadBtnRTF, Nothing)
        RadBtnRTF.Text = "RTF"
        RadBtnRTF.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxDays
        ' 
        TxtBoxDays.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TxtBoxDays.ContextMenuStrip = CMTxtBox
        TipClipExplorer.SetImage(TxtBoxDays, Nothing)
        TxtBoxDays.Location = New Point(508, 11)
        TxtBoxDays.Name = "TxtBoxDays"
        TxtBoxDays.PlaceholderText = "Days"
        TxtBoxDays.ShortcutsEnabled = False
        TxtBoxDays.Size = New Size(56, 29)
        TxtBoxDays.TabIndex = 30
        TipClipExplorer.SetText(TxtBoxDays, "Search Last x Days.")
        TxtBoxDays.TextAlign = HorizontalAlignment.Center
        ' 
        ' CMTxtBox
        ' 
        CMTxtBox.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipClipExplorer.SetImage(CMTxtBox, Nothing)
        CMTxtBox.Name = "CMTxtBox"
        CMTxtBox.Size = New Size(165, 204)
        TipClipExplorer.SetText(CMTxtBox, Nothing)
        ' 
        ' ChkBoxFavorites
        ' 
        ChkBoxFavorites.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ChkBoxFavorites.AutoSize = True
        TipClipExplorer.SetImage(ChkBoxFavorites, Nothing)
        ChkBoxFavorites.Location = New Point(309, 15)
        ChkBoxFavorites.Name = "ChkBoxFavorites"
        ChkBoxFavorites.Size = New Size(179, 25)
        ChkBoxFavorites.TabIndex = 20
        TipClipExplorer.SetText(ChkBoxFavorites, "Only Search through Favorite Clips.")
        ChkBoxFavorites.Text = "Search Only Favorites"
        ChkBoxFavorites.UseVisualStyleBackColor = True
        ' 
        ' BtnClearSearch
        ' 
        BtnClearSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnClearSearch.Image = My.Resources.Resources.ImageClearRemoveDelete16
        TipClipExplorer.SetImage(BtnClearSearch, Nothing)
        BtnClearSearch.Location = New Point(251, 10)
        BtnClearSearch.Name = "BtnClearSearch"
        BtnClearSearch.Size = New Size(32, 32)
        BtnClearSearch.TabIndex = 15
        TipClipExplorer.SetText(BtnClearSearch, "Clear Search and Show All Records.")
        BtnClearSearch.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxSearch
        ' 
        TxtBoxSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBoxSearch.ContextMenuStrip = CMTxtBox
        TipClipExplorer.SetImage(TxtBoxSearch, Nothing)
        TxtBoxSearch.Location = New Point(11, 11)
        TxtBoxSearch.Name = "TxtBoxSearch"
        TxtBoxSearch.PlaceholderText = "Search For"
        TxtBoxSearch.ShortcutsEnabled = False
        TxtBoxSearch.Size = New Size(240, 29)
        TxtBoxSearch.TabIndex = 10
        TipClipExplorer.SetText(TxtBoxSearch, "Search for the entered text.")
        ' 
        ' SplitContainerCE
        ' 
        SplitContainerCE.Dock = DockStyle.Fill
        TipClipExplorer.SetImage(SplitContainerCE, Nothing)
        SplitContainerCE.Location = New Point(0, 55)
        SplitContainerCE.Name = "SplitContainerCE"
        ' 
        ' SplitContainerCE.Panel1
        ' 
        SplitContainerCE.Panel1.Controls.Add(DGV)
        TipClipExplorer.SetImage(SplitContainerCE.Panel1, Nothing)
        TipClipExplorer.SetText(SplitContainerCE.Panel1, Nothing)
        ' 
        ' SplitContainerCE.Panel2
        ' 
        SplitContainerCE.Panel2.Controls.Add(RTB)
        TipClipExplorer.SetImage(SplitContainerCE.Panel2, Nothing)
        TipClipExplorer.SetText(SplitContainerCE.Panel2, Nothing)
        SplitContainerCE.Size = New Size(800, 373)
        SplitContainerCE.SplitterDistance = 506
        SplitContainerCE.TabIndex = 100
        TipClipExplorer.SetText(SplitContainerCE, Nothing)
        ' 
        ' DGV
        ' 
        DGV.BorderStyle = BorderStyle.None
        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV.Dock = DockStyle.Fill
        TipClipExplorer.SetImage(DGV, Nothing)
        DGV.Location = New Point(0, 0)
        DGV.Name = "DGV"
        DGV.Size = New Size(506, 373)
        DGV.TabIndex = 0
        TipClipExplorer.SetText(DGV, Nothing)
        ' 
        ' RTB
        ' 
        RTB.BorderStyle = BorderStyle.None
        RTB.Dock = DockStyle.Fill
        TipClipExplorer.SetImage(RTB, Nothing)
        RTB.Location = New Point(0, 0)
        RTB.Name = "RTB"
        RTB.Size = New Size(290, 373)
        RTB.TabIndex = 0
        RTB.Text = ""
        TipClipExplorer.SetText(RTB, Nothing)
        ' 
        ' TipClipExplorer
        ' 
        TipClipExplorer.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ' 
        ' ClipExplorer
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(SplitContainerCE)
        Controls.Add(PanelCE)
        Controls.Add(StatusStripCE)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipClipExplorer.SetImage(Me, Nothing)
        Name = "ClipExplorer"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        TipClipExplorer.SetText(Me, Nothing)
        Text = "Clip Explorer"
        StatusStripCE.ResumeLayout(False)
        StatusStripCE.PerformLayout()
        PanelCE.ResumeLayout(False)
        PanelCE.PerformLayout()
        GrpBoxSearch.ResumeLayout(False)
        GrpBoxSearch.PerformLayout()
        SplitContainerCE.Panel1.ResumeLayout(False)
        SplitContainerCE.Panel2.ResumeLayout(False)
        CType(SplitContainerCE, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerCE.ResumeLayout(False)
        CType(DGV, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStripCE As StatusStrip
    Friend WithEvents PanelCE As Panel
    Friend WithEvents SplitContainerCE As SplitContainer
    Friend WithEvents TxtBoxSearch As TextBox
    Friend WithEvents BtnClearSearch As Button
    Friend WithEvents TipClipExplorer As Skye.UI.ToolTipEX
    Friend WithEvents ChkBoxFavorites As CheckBox
    Friend WithEvents TxtBoxDays As TextBox
    Friend WithEvents GrpBoxSearch As GroupBox
    Friend WithEvents RadBtnHTML As RadioButton
    Friend WithEvents RadBtnAllText As RadioButton
    Friend WithEvents RadBtnRTF As RadioButton
    Friend WithEvents RadBtnPlainText As RadioButton
    Friend WithEvents CMTxtBox As Skye.UI.TextBoxContextMenu
    Friend WithEvents DGV As DataGridView
    Friend WithEvents RTB As RichTextBox
    Friend WithEvents TSSLabelCount As ToolStripStatusLabel
End Class
