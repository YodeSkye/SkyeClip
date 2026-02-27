<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KeywordRuleEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        components = New ComponentModel.Container()
        Label1 = New Skye.UI.Label()
        TxtBoxKeyword = New TextBox()
        Label2 = New Skye.UI.Label()
        CoBoxAction = New Skye.UI.ComboBox()
        BtnSave = New Button()
        Tip = New Skye.UI.ToolTipEX(components)
        TipError = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' Label1
        ' 
        TipError.SetImage(Label1, Nothing)
        Tip.SetImage(Label1, Nothing)
        Label1.Location = New Point(123, 38)
        Label1.Name = "Label1"
        Label1.Size = New Size(78, 23)
        Label1.TabIndex = 0
        Tip.SetText(Label1, Nothing)
        Label1.Text = "Keyword"
        TipError.SetText(Label1, Nothing)
        ' 
        ' TxtBoxKeyword
        ' 
        TipError.SetImage(TxtBoxKeyword, Nothing)
        Tip.SetImage(TxtBoxKeyword, Nothing)
        TxtBoxKeyword.Location = New Point(123, 60)
        TxtBoxKeyword.Name = "TxtBoxKeyword"
        TxtBoxKeyword.Size = New Size(169, 29)
        TxtBoxKeyword.TabIndex = 1
        Tip.SetText(TxtBoxKeyword, "Enter a Keyword.")
        TipError.SetText(TxtBoxKeyword, Nothing)
        ' 
        ' Label2
        ' 
        TipError.SetImage(Label2, Nothing)
        Tip.SetImage(Label2, Nothing)
        Label2.Location = New Point(123, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(66, 23)
        Label2.TabIndex = 2
        Tip.SetText(Label2, Nothing)
        Label2.Text = "Action"
        TipError.SetText(Label2, Nothing)
        ' 
        ' CoBoxAction
        ' 
        CoBoxAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAction.FormattingEnabled = True
        Tip.SetImage(CoBoxAction, Nothing)
        TipError.SetImage(CoBoxAction, Nothing)
        CoBoxAction.ItemHeight = 26
        CoBoxAction.Location = New Point(123, 125)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(169, 32)
        CoBoxAction.TabIndex = 3
        TipError.SetText(CoBoxAction, Nothing)
        Tip.SetText(CoBoxAction, "Select an Action.")
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
        ' KeywordRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(BtnSave)
        Controls.Add(CoBoxAction)
        Controls.Add(Label2)
        Controls.Add(TxtBoxKeyword)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "KeywordRuleEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxKeyword As TextBox
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents TipError As Skye.UI.ToolTipEX
    Friend WithEvents Tip As Skye.UI.ToolTipEX

End Class
