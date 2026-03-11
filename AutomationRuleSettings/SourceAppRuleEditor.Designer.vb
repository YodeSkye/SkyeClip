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
        components = New ComponentModel.Container()
        BtnSave = New Button()
        CoBoxAction = New Skye.UI.ComboBox()
        Label3 = New Skye.UI.Label()
        TxtBoxTargetProcess = New TextBox()
        CoBoxTargetProcess = New Skye.UI.ComboBox()
        Label1 = New Skye.UI.Label()
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
        ' CoBoxAction
        ' 
        CoBoxAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAction.FormattingEnabled = True
        Tip.SetImage(CoBoxAction, Nothing)
        TipError.SetImage(CoBoxAction, Nothing)
        CoBoxAction.Location = New Point(109, 126)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(197, 30)
        CoBoxAction.TabIndex = 30
        TipError.SetText(CoBoxAction, Nothing)
        Tip.SetText(CoBoxAction, "Select an Action.")
        ' 
        ' Label3
        ' 
        TipError.SetImage(Label3, Nothing)
        Tip.SetImage(Label3, Nothing)
        Label3.Location = New Point(109, 106)
        Label3.Name = "Label3"
        Label3.Size = New Size(100, 23)
        Label3.TabIndex = 6
        Tip.SetText(Label3, Nothing)
        Label3.Text = "Action"
        TipError.SetText(Label3, Nothing)
        ' 
        ' TxtBoxTargetProcess
        ' 
        TxtBoxTargetProcess.BorderStyle = BorderStyle.None
        TipError.SetImage(TxtBoxTargetProcess, Nothing)
        Tip.SetImage(TxtBoxTargetProcess, Nothing)
        TxtBoxTargetProcess.Location = New Point(113, 82)
        TxtBoxTargetProcess.Name = "TxtBoxTargetProcess"
        TxtBoxTargetProcess.Size = New Size(176, 22)
        TxtBoxTargetProcess.TabIndex = 14
        Tip.SetText(TxtBoxTargetProcess, "Enter or select a Target Process." & vbCrLf & "This is the process name associated with an app. May be found in details view of Windows Task Manager or in the properties of any running app.")
        TipError.SetText(TxtBoxTargetProcess, Nothing)
        ' 
        ' CoBoxTargetProcess
        ' 
        CoBoxTargetProcess.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTargetProcess.FormattingEnabled = True
        Tip.SetImage(CoBoxTargetProcess, Nothing)
        TipError.SetImage(CoBoxTargetProcess, Nothing)
        CoBoxTargetProcess.ItemHeight = 24
        CoBoxTargetProcess.Location = New Point(109, 78)
        CoBoxTargetProcess.Name = "CoBoxTargetProcess"
        CoBoxTargetProcess.Size = New Size(197, 30)
        CoBoxTargetProcess.TabIndex = 15
        CoBoxTargetProcess.TabStop = False
        TipError.SetText(CoBoxTargetProcess, Nothing)
        Tip.SetText(CoBoxTargetProcess, Nothing)
        ' 
        ' Label1
        ' 
        TipError.SetImage(Label1, Nothing)
        Tip.SetImage(Label1, Nothing)
        Label1.Location = New Point(109, 56)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 23)
        Label1.TabIndex = 13
        Tip.SetText(Label1, Nothing)
        Label1.Text = "Target Process"
        TipError.SetText(Label1, Nothing)
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
        ' SourceAppRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(CoBoxTargetProcess)
        Controls.Add(Label1)
        Controls.Add(CoBoxAction)
        Controls.Add(Label3)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "SourceAppRuleEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents Label3 As Skye.UI.Label
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents CoBoxTargetProcess As Skye.UI.ComboBox
    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents Tip As Skye.UI.ToolTipEX
    Friend WithEvents TipError As Skye.UI.ToolTipEX

End Class
