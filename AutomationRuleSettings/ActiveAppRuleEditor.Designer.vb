<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ActiveAppRuleEditor
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
        TxtBoxTargetProcess = New TextBox()
        BtnSave = New Button()
        CoBoxMode = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        CoBoxEnterProfile = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        CoBoxExitProfile = New Skye.UI.ComboBox()
        Label5 = New Skye.UI.Label()
        Label2 = New Skye.UI.Label()
        TxtBoxEnterDescription = New TextBox()
        TxtBoxExitDescription = New TextBox()
        Label6 = New Skye.UI.Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(3, 58)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 23)
        Label1.TabIndex = 0
        Label1.Text = "Target Process"
        ' 
        ' TxtBoxTargetProcess
        ' 
        TxtBoxTargetProcess.Location = New Point(3, 80)
        TxtBoxTargetProcess.Name = "TxtBoxTargetProcess"
        TxtBoxTargetProcess.Size = New Size(190, 29)
        TxtBoxTargetProcess.TabIndex = 1
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
        ' CoBoxMode
        ' 
        CoBoxMode.FormattingEnabled = True
        CoBoxMode.Location = New Point(3, 25)
        CoBoxMode.Name = "CoBoxMode"
        CoBoxMode.Size = New Size(190, 30)
        CoBoxMode.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(3, 5)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Label3.Text = "Mode"
        ' 
        ' CoBoxEnterProfile
        ' 
        CoBoxEnterProfile.FormattingEnabled = True
        CoBoxEnterProfile.Location = New Point(229, 25)
        CoBoxEnterProfile.Name = "CoBoxEnterProfile"
        CoBoxEnterProfile.Size = New Size(183, 30)
        CoBoxEnterProfile.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(229, 5)
        Label4.Name = "Label4"
        Label4.Size = New Size(100, 23)
        Label4.TabIndex = 8
        Label4.Text = "On Enter"
        ' 
        ' CoBoxExitProfile
        ' 
        CoBoxExitProfile.FormattingEnabled = True
        CoBoxExitProfile.Location = New Point(229, 80)
        CoBoxExitProfile.Name = "CoBoxExitProfile"
        CoBoxExitProfile.Size = New Size(183, 30)
        CoBoxExitProfile.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(229, 60)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 23)
        Label5.TabIndex = 10
        Label5.Text = "On Exit"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(3, 112)
        Label2.Name = "Label2"
        Label2.Size = New Size(140, 23)
        Label2.TabIndex = 11
        Label2.Text = "Enter Description"
        ' 
        ' TxtBoxEnterDescription
        ' 
        TxtBoxEnterDescription.Location = New Point(3, 132)
        TxtBoxEnterDescription.Name = "TxtBoxEnterDescription"
        TxtBoxEnterDescription.Size = New Size(409, 29)
        TxtBoxEnterDescription.TabIndex = 12
        ' 
        ' TxtBoxExitDescription
        ' 
        TxtBoxExitDescription.Location = New Point(3, 181)
        TxtBoxExitDescription.Name = "TxtBoxExitDescription"
        TxtBoxExitDescription.Size = New Size(409, 29)
        TxtBoxExitDescription.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(3, 161)
        Label6.Name = "Label6"
        Label6.Size = New Size(140, 23)
        Label6.TabIndex = 13
        Label6.Text = "Exit Description"
        ' 
        ' ActiveAppRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TxtBoxExitDescription)
        Controls.Add(TxtBoxEnterDescription)
        Controls.Add(Label2)
        Controls.Add(CoBoxExitProfile)
        Controls.Add(Label5)
        Controls.Add(CoBoxEnterProfile)
        Controls.Add(Label4)
        Controls.Add(CoBoxMode)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(Label1)
        Controls.Add(Label6)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "ActiveAppRuleEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxMode As Skye.UI.ComboBox
    Friend WithEvents Label3 As Skye.UI.Label
    Friend WithEvents CoBoxEnterProfile As Skye.UI.ComboBox
    Friend WithEvents Label4 As Skye.UI.Label
    Friend WithEvents CoBoxExitProfile As Skye.UI.ComboBox
    Friend WithEvents Label5 As Skye.UI.Label
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents TxtBoxEnterDescription As TextBox
    Friend WithEvents TxtBoxExitDescription As TextBox
    Friend WithEvents Label6 As Skye.UI.Label

End Class
