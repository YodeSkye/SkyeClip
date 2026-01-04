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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScratchPad))
        RTB = New Skye.UI.RichTextBox()
        PanelBottom = New Panel()
        SuspendLayout()
        ' 
        ' RTB
        ' 
        RTB.BorderStyle = BorderStyle.None
        RTB.Dock = DockStyle.Fill
        RTB.Location = New Point(0, 0)
        RTB.Name = "RTB"
        RTB.ScrollBars = RichTextBoxScrollBars.ForcedBoth
        RTB.ShortcutsEnabled = False
        RTB.ShowSelectionMargin = True
        RTB.Size = New Size(800, 399)
        RTB.TabIndex = 0
        RTB.Text = ""
        RTB.WordWrap = False
        ' 
        ' PanelBottom
        ' 
        PanelBottom.Dock = DockStyle.Bottom
        PanelBottom.Location = New Point(0, 399)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New Size(800, 51)
        PanelBottom.TabIndex = 1
        ' 
        ' ScratchPad
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(RTB)
        Controls.Add(PanelBottom)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "ScratchPad"
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.CenterScreen
        Text = "ScratchPad"
        ResumeLayout(False)
    End Sub

    Friend WithEvents RTB As Skye.UI.RichTextBox
    Friend WithEvents PanelBottom As Panel
End Class
