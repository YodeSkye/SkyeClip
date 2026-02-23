<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ActiveAppRuleBlockEditor
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
        ' ActiveAppRuleBlockEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TxtBoxExitDescription)
        Controls.Add(TxtBoxEnterDescription)
        Controls.Add(Label2)
        Controls.Add(BtnSave)
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(Label1)
        Controls.Add(Label6)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "ActiveAppRuleBlockEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents BtnSave As Button
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents TxtBoxEnterDescription As TextBox
    Friend WithEvents TxtBoxExitDescription As TextBox
    Friend WithEvents Label6 As Skye.UI.Label

End Class
