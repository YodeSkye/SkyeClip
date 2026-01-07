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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClipExplorer))
        StatusStripCE = New StatusStrip()
        PanelCE = New Panel()
        SplitContainerCE = New SplitContainer()
        CType(SplitContainerCE, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainerCE.SuspendLayout()
        SuspendLayout()
        ' 
        ' StatusStripCE
        ' 
        StatusStripCE.Location = New Point(0, 428)
        StatusStripCE.Name = "StatusStripCE"
        StatusStripCE.Size = New Size(800, 22)
        StatusStripCE.TabIndex = 0
        ' 
        ' PanelCE
        ' 
        PanelCE.BorderStyle = BorderStyle.FixedSingle
        PanelCE.Dock = DockStyle.Top
        PanelCE.Location = New Point(0, 0)
        PanelCE.Name = "PanelCE"
        PanelCE.Size = New Size(800, 59)
        PanelCE.TabIndex = 1
        ' 
        ' SplitContainerCE
        ' 
        SplitContainerCE.Dock = DockStyle.Fill
        SplitContainerCE.Location = New Point(0, 59)
        SplitContainerCE.Name = "SplitContainerCE"
        SplitContainerCE.Size = New Size(800, 369)
        SplitContainerCE.SplitterDistance = 506
        SplitContainerCE.TabIndex = 2
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
        Name = "ClipExplorer"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        Text = "Clip Explorer"
        CType(SplitContainerCE, ComponentModel.ISupportInitialize).EndInit()
        SplitContainerCE.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStripCE As StatusStrip
    Friend WithEvents PanelCE As Panel
    Friend WithEvents SplitContainerCE As SplitContainer
End Class
