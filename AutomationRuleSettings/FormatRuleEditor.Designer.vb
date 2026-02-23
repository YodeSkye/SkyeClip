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
        BtnSave = New Button()
        CoBoxFormatName = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        CoBoxAction = New Skye.UI.ComboBox()
        Label5 = New Skye.UI.Label()
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
        ' CoBoxFormatName
        ' 
        CoBoxFormatName.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxFormatName.FormattingEnabled = True
        CoBoxFormatName.Location = New Point(116, 58)
        CoBoxFormatName.Name = "CoBoxFormatName"
        CoBoxFormatName.Size = New Size(183, 30)
        CoBoxFormatName.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(116, 38)
        Label4.Name = "Label4"
        Label4.Size = New Size(115, 23)
        Label4.TabIndex = 8
        Label4.Text = "Format Name"
        ' 
        ' CoBoxAction
        ' 
        CoBoxAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAction.FormattingEnabled = True
        CoBoxAction.Location = New Point(116, 113)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(183, 30)
        CoBoxAction.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(116, 93)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 23)
        Label5.TabIndex = 10
        Label5.Text = "Action"
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
        Name = "FormatRuleEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents CoBoxFormatName As Skye.UI.ComboBox
    Friend WithEvents Label4 As Skye.UI.Label
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents Label5 As Skye.UI.Label

End Class
