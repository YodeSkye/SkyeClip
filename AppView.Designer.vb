<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        BtnSettings = New Button()
        BtnLog = New Button()
        BtnAbout = New Button()
        BtnHelp = New Button()
        TipAppView = New Skye.UI.ToolTipEX(components)
        BtnExit = New Button()
        BtnExport = New Button()
        BtnImport = New Button()
        SuspendLayout()
        ' 
        ' BtnSettings
        ' 
        BtnSettings.Image = My.Resources.Resources.ImageSettings32
        TipAppView.SetImage(BtnSettings, My.Resources.Resources.ImageSettings16)
        BtnSettings.Location = New Point(12, 14)
        BtnSettings.Name = "BtnSettings"
        BtnSettings.Size = New Size(48, 48)
        BtnSettings.TabIndex = 0
        TipAppView.SetText(BtnSettings, "Settings")
        BtnSettings.UseVisualStyleBackColor = True
        ' 
        ' BtnLog
        ' 
        BtnLog.Image = My.Resources.Resources.ImageLog32
        TipAppView.SetImage(BtnLog, My.Resources.Resources.ImageLog16)
        BtnLog.Location = New Point(12, 175)
        BtnLog.Name = "BtnLog"
        BtnLog.Size = New Size(48, 48)
        BtnLog.TabIndex = 1
        TipAppView.SetText(BtnLog, "Log")
        BtnLog.UseVisualStyleBackColor = True
        ' 
        ' BtnAbout
        ' 
        BtnAbout.Image = My.Resources.Resources.ImageAbout32
        TipAppView.SetImage(BtnAbout, My.Resources.Resources.ImageAbout16)
        BtnAbout.Location = New Point(12, 283)
        BtnAbout.Name = "BtnAbout"
        BtnAbout.Size = New Size(48, 48)
        BtnAbout.TabIndex = 3
        TipAppView.SetText(BtnAbout, "About")
        BtnAbout.UseVisualStyleBackColor = True
        ' 
        ' BtnHelp
        ' 
        BtnHelp.Image = My.Resources.Resources.ImageHelp32
        TipAppView.SetImage(BtnHelp, My.Resources.Resources.ImageHelp16)
        BtnHelp.Location = New Point(12, 229)
        BtnHelp.Name = "BtnHelp"
        BtnHelp.Size = New Size(48, 48)
        BtnHelp.TabIndex = 2
        TipAppView.SetText(BtnHelp, "Help")
        BtnHelp.UseVisualStyleBackColor = True
        ' 
        ' TipAppView
        ' 
        TipAppView.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAppView.ShadowThickness = 0
        ' 
        ' BtnExit
        ' 
        BtnExit.Image = My.Resources.Resources.ImageExit32
        TipAppView.SetImage(BtnExit, My.Resources.Resources.ImageAbout16)
        BtnExit.Location = New Point(12, 337)
        BtnExit.Name = "BtnExit"
        BtnExit.Size = New Size(48, 48)
        BtnExit.TabIndex = 4
        TipAppView.SetText(BtnExit, "Exit (Right-Click to Restart App)")
        BtnExit.UseVisualStyleBackColor = True
        ' 
        ' BtnExport
        ' 
        BtnExport.Image = My.Resources.Resources.ImageExport32
        TipAppView.SetImage(BtnExport, My.Resources.Resources.ImageExport16)
        BtnExport.Location = New Point(12, 121)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New Size(48, 48)
        BtnExport.TabIndex = 5
        TipAppView.SetText(BtnExport, "Export This Profile's Clips" & vbCrLf & "RightClick To Export All Clips")
        BtnExport.UseVisualStyleBackColor = True
        ' 
        ' BtnImport
        ' 
        BtnImport.Image = My.Resources.Resources.ImageImport32
        TipAppView.SetImage(BtnImport, My.Resources.Resources.ImageImport16)
        BtnImport.Location = New Point(12, 68)
        BtnImport.Name = "BtnImport"
        BtnImport.Size = New Size(48, 48)
        BtnImport.TabIndex = 6
        TipAppView.SetText(BtnImport, "Import")
        BtnImport.UseVisualStyleBackColor = True
        ' 
        ' AppView
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(72, 397)
        ControlBox = False
        Controls.Add(BtnImport)
        Controls.Add(BtnExport)
        Controls.Add(BtnExit)
        Controls.Add(BtnAbout)
        Controls.Add(BtnHelp)
        Controls.Add(BtnLog)
        Controls.Add(BtnSettings)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        TipAppView.SetImage(Me, Nothing)
        KeyPreview = True
        MaximizeBox = False
        MinimizeBox = False
        Name = "AppView"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        TipAppView.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnSettings As Button
    Friend WithEvents BtnLog As Button
    Friend WithEvents BtnAbout As Button
    Friend WithEvents BtnHelp As Button
    Friend WithEvents TipAppView As Skye.UI.ToolTipEX
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnExport As Button
    Friend WithEvents BtnImport As Button
End Class
