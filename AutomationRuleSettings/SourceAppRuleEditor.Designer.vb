<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SourceAppRuleEditor
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
        Label1 = New Skye.UI.Label()
        TxtBoxAppName = New TextBox()
        BtnSave = New Button()
        CoBoxAction = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(112, 53)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 23)
        Label1.TabIndex = 0
        Label1.Text = "App Name"
        ' 
        ' TxtBoxAppName
        ' 
        TxtBoxAppName.Location = New Point(112, 75)
        TxtBoxAppName.Name = "TxtBoxAppName"
        TxtBoxAppName.Size = New Size(190, 29)
        TxtBoxAppName.TabIndex = 1
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
        ' CoBoxAction
        ' 
        CoBoxAction.FormattingEnabled = True
        CoBoxAction.Location = New Point(112, 127)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(190, 30)
        CoBoxAction.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(112, 107)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Label3.Text = "Action"
        ' 
        ' SourceAppRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(CoBoxAction)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Controls.Add(TxtBoxAppName)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "SourceAppRuleEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxAppName As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents Label3 As Skye.UI.Label

End Class
