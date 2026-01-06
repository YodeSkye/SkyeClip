<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DevTools
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DevTools))
        SplitContainerClips = New SplitContainer()
        LblClips = New Label()
        DGVClips = New DataGridView()
        LblClipFormats = New Label()
        DGVClipFormats = New DataGridView()
        PanelToolbox = New Panel()
        BtnDeleteFormat = New Button()
        BtnDeleteClip = New Button()
        BtnSaveFormats = New Button()
        BtnSaveClips = New Button()
        BtnRefresh = New Button()
        BtnViewFormatData = New Button()
        CType(SplitContainerClips, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerClips.Panel1.SuspendLayout()
        SplitContainerClips.Panel2.SuspendLayout()
        SplitContainerClips.SuspendLayout()
        CType(DGVClips, ComponentModel.ISupportInitialize).BeginInit()
        CType(DGVClipFormats, ComponentModel.ISupportInitialize).BeginInit()
        PanelToolbox.SuspendLayout()
        SuspendLayout()
        ' 
        ' SplitContainerClips
        ' 
        SplitContainerClips.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SplitContainerClips.Location = New Point(0, 0)
        SplitContainerClips.Margin = New Padding(4)
        SplitContainerClips.Name = "SplitContainerClips"
        SplitContainerClips.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainerClips.Panel1
        ' 
        SplitContainerClips.Panel1.Controls.Add(LblClips)
        SplitContainerClips.Panel1.Controls.Add(DGVClips)
        ' 
        ' SplitContainerClips.Panel2
        ' 
        SplitContainerClips.Panel2.Controls.Add(LblClipFormats)
        SplitContainerClips.Panel2.Controls.Add(DGVClipFormats)
        SplitContainerClips.Size = New Size(1029, 567)
        SplitContainerClips.SplitterDistance = 352
        SplitContainerClips.SplitterWidth = 6
        SplitContainerClips.TabIndex = 0
        ' 
        ' LblClips
        ' 
        LblClips.AutoSize = True
        LblClips.Location = New Point(218, 5)
        LblClips.Margin = New Padding(4, 0, 4, 0)
        LblClips.Name = "LblClips"
        LblClips.Size = New Size(83, 21)
        LblClips.TabIndex = 0
        LblClips.Text = "Clips Table"
        ' 
        ' DGVClips
        ' 
        DGVClips.AllowUserToAddRows = False
        DGVClips.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVClips.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVClips.Dock = DockStyle.Fill
        DGVClips.Location = New Point(0, 0)
        DGVClips.Margin = New Padding(4)
        DGVClips.Name = "DGVClips"
        DGVClips.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVClips.Size = New Size(1029, 352)
        DGVClips.TabIndex = 1
        ' 
        ' LblClipFormats
        ' 
        LblClipFormats.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        LblClipFormats.AutoSize = True
        LblClipFormats.Location = New Point(4, 181)
        LblClipFormats.Margin = New Padding(4, 0, 4, 0)
        LblClipFormats.Name = "LblClipFormats"
        LblClipFormats.Size = New Size(137, 21)
        LblClipFormats.TabIndex = 0
        LblClipFormats.Text = "Clip Formats Table"
        ' 
        ' DGVClipFormats
        ' 
        DGVClipFormats.AllowUserToAddRows = False
        DGVClipFormats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVClipFormats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVClipFormats.Dock = DockStyle.Fill
        DGVClipFormats.Location = New Point(0, 0)
        DGVClipFormats.Margin = New Padding(4)
        DGVClipFormats.Name = "DGVClipFormats"
        DGVClipFormats.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVClipFormats.Size = New Size(1029, 209)
        DGVClipFormats.TabIndex = 1
        ' 
        ' PanelToolbox
        ' 
        PanelToolbox.Controls.Add(BtnViewFormatData)
        PanelToolbox.Controls.Add(BtnDeleteFormat)
        PanelToolbox.Controls.Add(BtnDeleteClip)
        PanelToolbox.Controls.Add(BtnSaveFormats)
        PanelToolbox.Controls.Add(BtnSaveClips)
        PanelToolbox.Controls.Add(BtnRefresh)
        PanelToolbox.Dock = DockStyle.Bottom
        PanelToolbox.Location = New Point(0, 566)
        PanelToolbox.Margin = New Padding(4)
        PanelToolbox.Name = "PanelToolbox"
        PanelToolbox.Size = New Size(1029, 64)
        PanelToolbox.TabIndex = 2
        ' 
        ' BtnDeleteFormat
        ' 
        BtnDeleteFormat.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnDeleteFormat.Location = New Point(896, 17)
        BtnDeleteFormat.Margin = New Padding(4)
        BtnDeleteFormat.Name = "BtnDeleteFormat"
        BtnDeleteFormat.Size = New Size(116, 32)
        BtnDeleteFormat.TabIndex = 4
        BtnDeleteFormat.Text = "Delete Format"
        BtnDeleteFormat.UseVisualStyleBackColor = True
        ' 
        ' BtnDeleteClip
        ' 
        BtnDeleteClip.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnDeleteClip.Location = New Point(792, 17)
        BtnDeleteClip.Margin = New Padding(4)
        BtnDeleteClip.Name = "BtnDeleteClip"
        BtnDeleteClip.Size = New Size(96, 32)
        BtnDeleteClip.TabIndex = 3
        BtnDeleteClip.Text = "Delete Clip"
        BtnDeleteClip.UseVisualStyleBackColor = True
        ' 
        ' BtnSaveFormats
        ' 
        BtnSaveFormats.Location = New Point(239, 17)
        BtnSaveFormats.Margin = New Padding(4)
        BtnSaveFormats.Name = "BtnSaveFormats"
        BtnSaveFormats.Size = New Size(121, 32)
        BtnSaveFormats.TabIndex = 2
        BtnSaveFormats.Text = "Save Formats"
        BtnSaveFormats.UseVisualStyleBackColor = True
        ' 
        ' BtnSaveClips
        ' 
        BtnSaveClips.Location = New Point(141, 17)
        BtnSaveClips.Margin = New Padding(4)
        BtnSaveClips.Name = "BtnSaveClips"
        BtnSaveClips.Size = New Size(96, 32)
        BtnSaveClips.TabIndex = 1
        BtnSaveClips.Text = "Save Clips"
        BtnSaveClips.UseVisualStyleBackColor = True
        ' 
        ' BtnRefresh
        ' 
        BtnRefresh.Location = New Point(15, 17)
        BtnRefresh.Margin = New Padding(4)
        BtnRefresh.Name = "BtnRefresh"
        BtnRefresh.Size = New Size(96, 32)
        BtnRefresh.TabIndex = 0
        BtnRefresh.Text = "Refresh"
        BtnRefresh.UseVisualStyleBackColor = True
        ' 
        ' BtnViewFormatData
        ' 
        BtnViewFormatData.Location = New Point(388, 17)
        BtnViewFormatData.Name = "BtnViewFormatData"
        BtnViewFormatData.Size = New Size(163, 32)
        BtnViewFormatData.TabIndex = 5
        BtnViewFormatData.Text = "View Format Data"
        BtnViewFormatData.UseVisualStyleBackColor = True
        ' 
        ' DevTools
        ' 
        AutoScaleDimensions = New SizeF(9F, 21F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1029, 630)
        Controls.Add(SplitContainerClips)
        Controls.Add(PanelToolbox)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        Name = "DevTools"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dev Tools (USE CAREFULLY)"
        SplitContainerClips.Panel1.ResumeLayout(False)
        SplitContainerClips.Panel1.PerformLayout()
        SplitContainerClips.Panel2.ResumeLayout(False)
        SplitContainerClips.Panel2.PerformLayout()
        CType(SplitContainerClips, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerClips.ResumeLayout(False)
        CType(DGVClips, ComponentModel.ISupportInitialize).EndInit()
        CType(DGVClipFormats, ComponentModel.ISupportInitialize).EndInit()
        PanelToolbox.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents SplitContainerClips As SplitContainer
    Friend WithEvents DGVClips As DataGridView
    Friend WithEvents LblClips As Label
    Friend WithEvents LblClipFormats As Label
    Friend WithEvents DGVClipFormats As DataGridView
    Friend WithEvents PanelToolbox As Panel
    Friend WithEvents BtnDeleteFormat As Button
    Friend WithEvents BtnDeleteClip As Button
    Friend WithEvents BtnSaveFormats As Button
    Friend WithEvents BtnSaveClips As Button
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents BtnViewFormatData As Button
End Class
