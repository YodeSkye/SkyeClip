<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ActiveAppRuleProfilesEditor
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
        components = New ComponentModel.Container()
        Label1 = New Skye.UI.Label()
        TxtBoxTargetProcess = New TextBox()
        BtnSave = New Button()
        CoBoxMode = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        CoBoxEnterProfile = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        CoBoxExitProfile = New Skye.UI.ComboBox()
        Label5 = New Skye.UI.Label()
        Tip = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Tip.SetImage(Label1, Nothing)
        Label1.Location = New Point(3, 103)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 23)
        Label1.TabIndex = 0
        Label1.Text = "Target Process"
        Tip.SetText(Label1, Nothing)
        ' 
        ' TxtBoxTargetProcess
        ' 
        Tip.SetImage(TxtBoxTargetProcess, Nothing)
        TxtBoxTargetProcess.Location = New Point(3, 125)
        TxtBoxTargetProcess.Name = "TxtBoxTargetProcess"
        TxtBoxTargetProcess.Size = New Size(197, 29)
        TxtBoxTargetProcess.TabIndex = 1
        Tip.SetText(TxtBoxTargetProcess, Nothing)
        ' 
        ' BtnSave
        ' 
        BtnSave.Image = My.Resources.Resources.ImageOK16
        Tip.SetImage(BtnSave, Nothing)
        BtnSave.ImageAlign = ContentAlignment.MiddleLeft
        BtnSave.Location = New Point(3, 214)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(409, 36)
        BtnSave.TabIndex = 4
        Tip.SetText(BtnSave, Nothing)
        BtnSave.Text = "Save Rule"
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' CoBoxMode
        ' 
        CoBoxMode.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxMode.FormattingEnabled = True
        Tip.SetImage(CoBoxMode, Nothing)
        CoBoxMode.Location = New Point(3, 70)
        CoBoxMode.Name = "CoBoxMode"
        CoBoxMode.Size = New Size(197, 30)
        CoBoxMode.TabIndex = 5
        Tip.SetText(CoBoxMode, "Rule Mode. Select whether the Rule should check for a running process or the currently active window.")
        ' 
        ' Label3
        ' 
        Tip.SetImage(Label3, Nothing)
        Label3.Location = New Point(3, 50)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Label3.Text = "Mode"
        Tip.SetText(Label3, Nothing)
        ' 
        ' CoBoxEnterProfile
        ' 
        CoBoxEnterProfile.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxEnterProfile.FormattingEnabled = True
        Tip.SetImage(CoBoxEnterProfile, Nothing)
        CoBoxEnterProfile.Location = New Point(229, 70)
        CoBoxEnterProfile.Name = "CoBoxEnterProfile"
        CoBoxEnterProfile.Size = New Size(183, 30)
        CoBoxEnterProfile.TabIndex = 7
        Tip.SetText(CoBoxEnterProfile, Nothing)
        ' 
        ' Label4
        ' 
        Tip.SetImage(Label4, Nothing)
        Label4.Location = New Point(229, 50)
        Label4.Name = "Label4"
        Label4.Size = New Size(100, 23)
        Label4.TabIndex = 8
        Label4.Text = "On Enter"
        Tip.SetText(Label4, Nothing)
        ' 
        ' CoBoxExitProfile
        ' 
        CoBoxExitProfile.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxExitProfile.FormattingEnabled = True
        Tip.SetImage(CoBoxExitProfile, Nothing)
        CoBoxExitProfile.Location = New Point(229, 125)
        CoBoxExitProfile.Name = "CoBoxExitProfile"
        CoBoxExitProfile.Size = New Size(183, 30)
        CoBoxExitProfile.TabIndex = 9
        Tip.SetText(CoBoxExitProfile, Nothing)
        ' 
        ' Label5
        ' 
        Tip.SetImage(Label5, Nothing)
        Label5.Location = New Point(229, 105)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 23)
        Label5.TabIndex = 10
        Label5.Text = "On Exit"
        Tip.SetText(Label5, Nothing)
        ' 
        ' Tip
        ' 
        Tip.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.ShadowThickness = 0
        ' 
        ' ActiveAppRuleProfilesEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(CoBoxExitProfile)
        Controls.Add(Label5)
        Controls.Add(CoBoxEnterProfile)
        Controls.Add(Label4)
        Controls.Add(CoBoxMode)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        Name = "ActiveAppRuleProfilesEditor"
        Size = New Size(415, 253)
        Tip.SetText(Me, Nothing)
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
    Friend WithEvents Tip As Skye.UI.ToolTipEX

End Class
