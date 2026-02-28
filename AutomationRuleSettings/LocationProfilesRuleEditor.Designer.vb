<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LocationProfilesRuleEditor
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
        BtnSave = New Button()
        CoBoxTargetName = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        CoBoxProfile = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        Tip = New Skye.UI.ToolTipEX(components)
        TipError = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' BtnSave
        ' 
        TipError.SetImage(BtnSave, Nothing)
        BtnSave.Image = My.Resources.Resources.ImageOK16
        Tip.SetImage(BtnSave, My.Resources.Resources.ImageOK16)
        BtnSave.ImageAlign = ContentAlignment.MiddleLeft
        BtnSave.Location = New Point(3, 214)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(409, 36)
        BtnSave.TabIndex = 100
        Tip.SetText(BtnSave, "Save this Rule.")
        TipError.SetText(BtnSave, Nothing)
        BtnSave.Text = "Save Rule"
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' CoBoxTargetName
        ' 
        CoBoxTargetName.FormattingEnabled = True
        Tip.SetImage(CoBoxTargetName, Nothing)
        TipError.SetImage(CoBoxTargetName, Nothing)
        CoBoxTargetName.Location = New Point(112, 68)
        CoBoxTargetName.Name = "CoBoxTargetName"
        CoBoxTargetName.Size = New Size(190, 30)
        CoBoxTargetName.TabIndex = 5
        TipError.SetText(CoBoxTargetName, Nothing)
        Tip.SetText(CoBoxTargetName, "Name of the current Windows network (SSID or network profile name).")
        ' 
        ' Label3
        ' 
        TipError.SetImage(Label3, Nothing)
        Tip.SetImage(Label3, Nothing)
        Label3.Location = New Point(112, 48)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Tip.SetText(Label3, Nothing)
        Label3.Text = "Target Name"
        TipError.SetText(Label3, Nothing)
        ' 
        ' CoBoxProfile
        ' 
        CoBoxProfile.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxProfile.FormattingEnabled = True
        Tip.SetImage(CoBoxProfile, Nothing)
        TipError.SetImage(CoBoxProfile, Nothing)
        CoBoxProfile.Location = New Point(112, 122)
        CoBoxProfile.Name = "CoBoxProfile"
        CoBoxProfile.Size = New Size(190, 30)
        CoBoxProfile.TabIndex = 7
        TipError.SetText(CoBoxProfile, Nothing)
        Tip.SetText(CoBoxProfile, "Select a Profile.")
        ' 
        ' Label4
        ' 
        TipError.SetImage(Label4, Nothing)
        Tip.SetImage(Label4, Nothing)
        Label4.Location = New Point(112, 102)
        Label4.Name = "Label4"
        Label4.Size = New Size(100, 23)
        Label4.TabIndex = 8
        Tip.SetText(Label4, Nothing)
        Label4.Text = "Profile"
        TipError.SetText(Label4, Nothing)
        ' 
        ' Tip
        ' 
        Tip.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.ShadowThickness = 0
        ' 
        ' TipError
        ' 
        TipError.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipError.HideDelay = 4000
        TipError.ShadowThickness = 0
        ' 
        ' LocationProfilesRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(CoBoxProfile)
        Controls.Add(Label4)
        Controls.Add(CoBoxTargetName)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "LocationProfilesRuleEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxTargetName As Skye.UI.ComboBox
    Friend WithEvents Label3 As Skye.UI.Label
    Friend WithEvents CoBoxProfile As Skye.UI.ComboBox
    Friend WithEvents Label4 As Skye.UI.Label
    Friend WithEvents TipError As Skye.UI.ToolTipEX
    Friend WithEvents Tip As Skye.UI.ToolTipEX

End Class
