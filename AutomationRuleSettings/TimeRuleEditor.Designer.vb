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
        BtnSave = New Button()
        CoBoxProfile = New Skye.UI.ComboBox()
        Label4 = New Skye.UI.Label()
        DTPStartTime = New DateTimePicker()
        Label1 = New Skye.UI.Label()
        DTPEndTime = New DateTimePicker()
        Label2 = New Skye.UI.Label()
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
        ' CoBoxProfile
        ' 
        CoBoxProfile.FormattingEnabled = True
        CoBoxProfile.Location = New Point(3, 153)
        CoBoxProfile.Name = "CoBoxProfile"
        CoBoxProfile.Size = New Size(183, 30)
        CoBoxProfile.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(3, 133)
        Label4.Name = "Label4"
        Label4.Size = New Size(153, 23)
        Label4.TabIndex = 8
        Label4.Text = "Change Profile To:"
        ' 
        ' DTPStartTime
        ' 
        DTPStartTime.Format = DateTimePickerFormat.Time
        DTPStartTime.Location = New Point(3, 28)
        DTPStartTime.Name = "DTPStartTime"
        DTPStartTime.ShowUpDown = True
        DTPStartTime.Size = New Size(183, 29)
        DTPStartTime.TabIndex = 9
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(3, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(100, 23)
        Label1.TabIndex = 10
        Label1.Text = "Start Time"
        ' 
        ' DTPEndTime
        ' 
        DTPEndTime.Format = DateTimePickerFormat.Time
        DTPEndTime.Location = New Point(3, 79)
        DTPEndTime.Name = "DTPEndTime"
        DTPEndTime.ShowUpDown = True
        DTPEndTime.Size = New Size(183, 29)
        DTPEndTime.TabIndex = 11
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(3, 59)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 23)
        Label2.TabIndex = 12
        Label2.Text = "End Time"
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
        Name = "TimeRuleEditor"
        Size = New Size(415, 253)
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

End Class
