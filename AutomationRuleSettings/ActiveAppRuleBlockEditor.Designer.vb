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
        ' ActiveAppRuleBlockEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(BtnSave)
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "ActiveAppRuleBlockEditor"
        Size = New Size(415, 253)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Skye.UI.Label
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents BtnSave As Button

End Class
