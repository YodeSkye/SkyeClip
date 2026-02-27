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
        components = New ComponentModel.Container()
        BtnSave = New Button()
        TxtBoxTargetProcess = New TextBox()
        CoBoxTargetProcess = New Skye.UI.ComboBox()
        Label2 = New Skye.UI.Label()
        Tip = New Skye.UI.ToolTipEX(components)
        TipError = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' BtnSave
        ' 
        TipError.SetImage(BtnSave, Nothing)
        BtnSave.Image = My.Resources.Resources.ImageOK16
        Tip.SetImage(BtnSave, Nothing)
        BtnSave.ImageAlign = ContentAlignment.MiddleLeft
        BtnSave.Location = New Point(3, 214)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New Size(409, 36)
        BtnSave.TabIndex = 100
        Tip.SetText(BtnSave, Nothing)
        TipError.SetText(BtnSave, Nothing)
        BtnSave.Text = "Save Rule"
        BtnSave.UseVisualStyleBackColor = True
        ' 
        ' TxtBoxTargetProcess
        ' 
        TxtBoxTargetProcess.BorderStyle = BorderStyle.None
        TipError.SetImage(TxtBoxTargetProcess, Nothing)
        Tip.SetImage(TxtBoxTargetProcess, Nothing)
        TxtBoxTargetProcess.Location = New Point(113, 107)
        TxtBoxTargetProcess.Name = "TxtBoxTargetProcess"
        TxtBoxTargetProcess.Size = New Size(179, 22)
        TxtBoxTargetProcess.TabIndex = 14
        Tip.SetText(TxtBoxTargetProcess, Nothing)
        TipError.SetText(TxtBoxTargetProcess, Nothing)
        ' 
        ' CoBoxTargetProcess
        ' 
        CoBoxTargetProcess.DropDownStyle = ComboBoxStyle.DropDownList
        CoBoxTargetProcess.FormattingEnabled = True
        Tip.SetImage(CoBoxTargetProcess, Nothing)
        TipError.SetImage(CoBoxTargetProcess, Nothing)
        CoBoxTargetProcess.ItemHeight = 24
        CoBoxTargetProcess.Location = New Point(109, 103)
        CoBoxTargetProcess.Name = "CoBoxTargetProcess"
        CoBoxTargetProcess.Size = New Size(197, 30)
        CoBoxTargetProcess.TabIndex = 15
        CoBoxTargetProcess.TabStop = False
        TipError.SetText(CoBoxTargetProcess, Nothing)
        Tip.SetText(CoBoxTargetProcess, Nothing)
        ' 
        ' Label2
        ' 
        TipError.SetImage(Label2, Nothing)
        Tip.SetImage(Label2, Nothing)
        Label2.Location = New Point(109, 81)
        Label2.Name = "Label2"
        Label2.Size = New Size(123, 23)
        Label2.TabIndex = 13
        Tip.SetText(Label2, Nothing)
        Label2.Text = "Target Process"
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
        ' ActiveAppRuleBlockEditor
        ' 
        AutoScaleMode = AutoScaleMode.None
        Controls.Add(TxtBoxTargetProcess)
        Controls.Add(CoBoxTargetProcess)
        Controls.Add(Label2)
        Controls.Add(BtnSave)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Tip.SetImage(Me, Nothing)
        TipError.SetImage(Me, Nothing)
        Name = "ActiveAppRuleBlockEditor"
        Size = New Size(415, 253)
        TipError.SetText(Me, Nothing)
        Tip.SetText(Me, Nothing)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents BtnSave As Button
    Friend WithEvents TxtBoxTargetProcess As TextBox
    Friend WithEvents CoBoxTargetProcess As Skye.UI.ComboBox
    Friend WithEvents Label2 As Skye.UI.Label
    Friend WithEvents Tip As Skye.UI.ToolTipEX
    Friend WithEvents TipError As Skye.UI.ToolTipEX

End Class
