<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TimeRuleEditor
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
        CoBoxProfile = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        DTPStartTime = New DateTimePicker()
        Label1 = New Skye.UI.Label()
        DTPEndTime = New DateTimePicker()
        Label2 = New Skye.UI.Label()
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
        ' CoBoxProfile
        ' 
        CoBoxProfile.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxProfile.FormattingEnabled = True
        Tip.SetImage(CoBoxProfile, Nothing)
        TipError.SetImage(CoBoxProfile, Nothing)
        CoBoxProfile.Location = New Point(116, 153)
        CoBoxProfile.Name = "CoBoxProfile"
        CoBoxProfile.Size = New Size(183, 30)
        CoBoxProfile.TabIndex = 30
        TipError.SetText(CoBoxProfile, Nothing)
        Tip.SetText(CoBoxProfile, "Select a Profile.")
        ' 
        ' Label4
        ' 
        TipError.SetImage(Label4, Nothing)
        Tip.SetImage(Label4, Nothing)
        Label4.Location = New Point(116, 133)
        Label4.Name = "Label4"
        Label4.Size = New Size(153, 23)
        Label4.TabIndex = 8
        Tip.SetText(Label4, Nothing)
        Label4.Text = "Change Profile To:"
        TipError.SetText(Label4, Nothing)
        ' 
        ' DTPStartTime
        ' 
        DTPStartTime.Format = DateTimePickerFormat.Time
        TipError.SetImage(DTPStartTime, Nothing)
        Tip.SetImage(DTPStartTime, Nothing)
        DTPStartTime.Location = New Point(157, 28)
        DTPStartTime.Name = "DTPStartTime"
        DTPStartTime.ShowUpDown = True
        DTPStartTime.Size = New Size(100, 29)
        DTPStartTime.TabIndex = 9
        Tip.SetText(DTPStartTime, "Enter the Start Time.")
        TipError.SetText(DTPStartTime, Nothing)
        ' 
        ' Label1
        ' 
        TipError.SetImage(Label1, Nothing)
        Tip.SetImage(Label1, Nothing)
        Label1.Location = New Point(157, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(100, 23)
        Label1.TabIndex = 10
        Tip.SetText(Label1, Nothing)
        Label1.Text = "Start Time"
        TipError.SetText(Label1, Nothing)
        ' 
        ' DTPEndTime
        ' 
        DTPEndTime.Format = DateTimePickerFormat.Time
        TipError.SetImage(DTPEndTime, Nothing)
        Tip.SetImage(DTPEndTime, Nothing)
        DTPEndTime.Location = New Point(157, 79)
        DTPEndTime.Name = "DTPEndTime"
        DTPEndTime.ShowUpDown = True
        DTPEndTime.Size = New Size(100, 29)
        DTPEndTime.TabIndex = 11
        Tip.SetText(DTPEndTime, "Enter the End Time")
        TipError.SetText(DTPEndTime, Nothing)
        ' 
        ' Label2
        ' 
        TipError.SetImage(Label2, Nothing)
        Tip.SetImage(Label2, Nothing)
        Label2.Location = New Point(157, 59)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 23)
        Label2.TabIndex = 12
        Tip.SetText(Label2, Nothing)
        Label2.Text = "End Time"
        TipError.SetText(Label2, Nothing)
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
        ' TimeRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(DTPEndTime)
        Controls.Add(Label2)
        Controls.Add(DTPStartTime)
        Controls.Add(Label1)
        Controls.Add(CoBoxProfile)
        Controls.Add(Label4)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "TimeRuleEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxProfile As Skye.UI.ComboBox
    Friend WithEvents Label4 As Skye.UI.Label
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents DTPStartTime As DateTimePicker
    Friend WithEvents DTPEndTime As DateTimePicker
    Friend WithEvents TipError As Skye.UI.ToolTipEX
    Friend WithEvents Tip As Skye.UI.ToolTipEX

End Class
