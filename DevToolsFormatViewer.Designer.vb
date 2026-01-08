<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DevToolsFormatViewer
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
        RTB = New Skye.UI.RichTextBox()
        SuspendLayout()
        ' 
        ' RTB
        ' 
        RTB.BorderStyle = BorderStyle.None
        RTB.Dock = DockStyle.Fill
        RTB.Font = New Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        RTB.Location = New Point(0, 0)
        RTB.Name = "RTB"
        RTB.ReadOnly = True
        RTB.ScrollBars = RichTextBoxScrollBars.ForcedBoth
        RTB.Size = New Size(554, 631)
        RTB.TabIndex = 0
        RTB.Text = ""
        ' 
        ' DevToolsFormatViewer
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(554, 631)
        Controls.Add(RTB)
        Name = "DevToolsFormatViewer"
        StartPosition = FormStartPosition.CenterParent
        Text = "DevToolsFormatViewer"
        ResumeLayout(False)
    End Sub

    Friend WithEvents RTB As Skye.UI.RichTextBox
End Class
