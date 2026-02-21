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
        Label1 = New Skye.UI.Label()
        TxtBoxKeyword = New TextBox()
        Label2 = New Skye.UI.Label()
        CoBoxAction = New Skye.UI.ComboBox()
        BtnSave = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(75, 38)
        Label1.Name = "Label1"
        Label1.Size = New Size(78, 23)
        Label1.TabIndex = 0
        Label1.Text = "Keyword"
        ' 
        ' TxtBoxKeyword
        ' 
        TxtBoxKeyword.Location = New Point(75, 60)
        TxtBoxKeyword.Name = "TxtBoxKeyword"
        TxtBoxKeyword.Size = New Size(265, 29)
        TxtBoxKeyword.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(75, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(66, 23)
        Label2.TabIndex = 2
        Label2.Text = "Action"
        ' 
        ' CoBoxAction
        ' 
        CoBoxAction.DrawMode = DrawMode.OwnerDrawFixed
        CoBoxAction.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxAction.FormattingEnabled = True
        CoBoxAction.Location = New Point(75, 125)
        CoBoxAction.Name = "CoBoxAction"
        CoBoxAction.Size = New Size(169, 30)
        CoBoxAction.TabIndex = 3
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
        ' KeywordRuleEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(BtnSave)
        Controls.Add(CoBoxAction)
        Controls.Add(Label2)
        Controls.Add(TxtBoxKeyword)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "KeywordRuleEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxKeyword As TextBox
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents CoBoxAction As Skye.UI.ComboBox
    Friend WithEvents BtnSave As Button

End Class
