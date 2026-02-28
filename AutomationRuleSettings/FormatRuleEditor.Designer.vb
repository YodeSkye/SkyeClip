<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormatRuleEditor
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
        CoBoxFormatName = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        CoBoxAction = New Skye.UI.ComboBox()
        Label5 = New Skye.UI.Label()
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
        ' CoBoxFormatName
        ' 
        CoBoxFormatName.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxFormatName.FormattingEnabled = True
        Tip.SetImage(CoBoxFormatName, Nothing)
        TipError.SetImage(CoBoxFormatName, Nothing)
        CoBoxFormatName.Location = New Point(116, 58)
        CoBoxFormatName.Name = "CoBoxFormatName"
        CoBoxFormatName.Size = New Size(183, 30)
        CoBoxFormatName.TabIndex = 7
        TipError.SetText(CoBoxFormatName, Nothing)
        Tip.SetText(CoBoxFormatName, "Select a Format Name." & vbCrLf & "This is the name Windows uses to differentiate clip types.")
        ' 
        ' Label4
        ' 
        TipError.SetImage(Label4, Nothing)
        Tip.SetImage(Label4, Nothing)
        Label4.Location = New Point(116, 38)
        Label4.Name = "Label4"
        Label4.Size = New Size(115, 23)
        Label4.TabIndex = 8
        Tip.SetText(Label4, Nothing)
        Label4.Text = "Format Name"
        TipError.SetText(Label4, Nothing)
        ' 
        ' CoBoxAction
        ' 
        CoBoxAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAction.FormattingEnabled = True
        Tip.SetImage(CoBoxAction, Nothing)
        TipError.SetImage(CoBoxAction, Nothing)
        CoBoxAction.Location = New Point(116, 113)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(183, 30)
        CoBoxAction.TabIndex = 9
        TipError.SetText(CoBoxAction, Nothing)
        Tip.SetText(CoBoxAction, "Select an Action.")
        ' 
        ' Label5
        ' 
        TipError.SetImage(Label5, Nothing)
        Tip.SetImage(Label5, Nothing)
        Label5.Location = New Point(116, 93)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 23)
        Label5.TabIndex = 10
        Tip.SetText(Label5, Nothing)
        Label5.Text = "Action"
        TipError.SetText(Label5, Nothing)
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
        ' FormatRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(CoBoxAction)
        Controls.Add(Label5)
        Controls.Add(CoBoxFormatName)
        Controls.Add(Label4)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "FormatRuleEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxFormatName As Skye.UI.ComboBox
    Friend WithEvents Label4 As Skye.UI.Label
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents Label5 As Skye.UI.Label
    Friend WithEvents TipError As Skye.UI.ToolTipEX
    Friend WithEvents Tip As Skye.UI.ToolTipEX

End Class
