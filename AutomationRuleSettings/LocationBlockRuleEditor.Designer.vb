<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LocationBlockRuleEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        BtnSave = New Button()
        CoBoxTargetName = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        Label2 = New Skye.UI.Label()
        TxtBoxDescription = New TextBox()
        SuspendLayout()
        ' 
        ' BtnSave
        ' 
        BtnSave.Image = My.Resources.Resources.ImageOK16
        BtnSave.ImageAlign = ContentAlignment.MiddleLeft
        BtnSave.Location = New Point(3, 214)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(409, 36)
        BtnSave.TabIndex = 4
        BtnSave.Text = "Save Rule"
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' CoBoxTargetName
        ' 
        CoBoxTargetName.FormattingEnabled = True
        CoBoxTargetName.Location = New Point(3, 72)
        CoBoxTargetName.Name = "CoBoxTargetName"
        CoBoxTargetName.Size = New Size(190, 30)
        CoBoxTargetName.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(3, 52)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Label3.Text = "Target Name"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(3, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(140, 23)
        Label2.TabIndex = 11
        Label2.Text = "Description"
        ' 
        ' TxtBoxDescription
        ' 
        TxtBoxDescription.Location = New Point(3, 123)
        TxtBoxDescription.Name = "TxtBoxDescription"
        TxtBoxDescription.Size = New Size(409, 29)
        TxtBoxDescription.TabIndex = 12
        ' 
        ' LocationBlockRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TxtBoxDescription)
        Controls.Add(Label2)
        Controls.Add(CoBoxTargetName)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "LocationBlockRuleEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxTargetName As Skye.UI.ComboBox
    Friend WithEvents Label3 As Skye.UI.Label
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents TxtBoxDescription As TextBox

End Class
