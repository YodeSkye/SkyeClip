<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Log
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Log))
        RTBCMLog = New Skye.UI.RichTextBoxContextMenu()
        BTNOK = New Button()
        BTNDeleteLog = New Button()
        LBLLogInfo = New Skye.UI.Label()
        TipLog = New Skye.UI.ToolTipEX(components)
        LogViewer = New Skye.UI.Log.LogViewerControl()
        TipAlert = New Skye.UI.ToolTipEX(components)
        SuspendLayout()
        ' 
        ' RTBCMLog
        ' 
        RTBCMLog.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLog.SetImage(RTBCMLog, Nothing)
        TipAlert.SetImage(RTBCMLog, Nothing)
        RTBCMLog.Name = "RTBCMLog"
        RTBCMLog.Size = New Size(129, 148)
        TipLog.SetText(RTBCMLog, Nothing)
        TipAlert.SetText(RTBCMLog, Nothing)
        ' 
        ' BTNOK
        ' 
        BTNOK.Anchor = AnchorStyles.Bottom
        TipAlert.SetImage(BTNOK, Nothing)
        BTNOK.Image = My.Resources.Resources.ImageOK
        TipLog.SetImage(BTNOK, Nothing)
        BTNOK.Location = New Point(368, 383)
        BTNOK.Name = "BTNOK"
        BTNOK.Size = New Size(64, 64)
        BTNOK.TabIndex = 2
        TipLog.SetText(BTNOK, "Close Window")
        TipAlert.SetText(BTNOK, Nothing)
        BTNOK.UseVisualStyleBackColor = True
        ' 
        ' BTNDeleteLog
        ' 
        BTNDeleteLog.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TipAlert.SetImage(BTNDeleteLog, Nothing)
        BTNDeleteLog.Image = My.Resources.Resources.ImageDeleteLog32
        TipLog.SetImage(BTNDeleteLog, Nothing)
        BTNDeleteLog.Location = New Point(12, 399)
        BTNDeleteLog.Name = "BTNDeleteLog"
        BTNDeleteLog.Size = New Size(48, 48)
        BTNDeleteLog.TabIndex = 3
        TipLog.SetText(BTNDeleteLog, "Delete Log")
        TipAlert.SetText(BTNDeleteLog, Nothing)
        BTNDeleteLog.UseVisualStyleBackColor = True
        ' 
        ' LBLLogInfo
        ' 
        LBLLogInfo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LBLLogInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TipAlert.SetImage(LBLLogInfo, Nothing)
        TipLog.SetImage(LBLLogInfo, Nothing)
        LBLLogInfo.Location = New Point(12, 352)
        LBLLogInfo.Name = "LBLLogInfo"
        LBLLogInfo.Size = New Size(776, 23)
        LBLLogInfo.TabIndex = 5
        TipLog.SetText(LBLLogInfo, Nothing)
        LBLLogInfo.Text = "Log Info"
        TipAlert.SetText(LBLLogInfo, Nothing)
        LBLLogInfo.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' TipLog
        ' 
        TipLog.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLog.ShadowThickness = 0
        ' 
        ' LogViewer
        ' 
        LogViewer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LogViewer.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipLog.SetImage(LogViewer, Nothing)
        TipAlert.SetImage(LogViewer, Nothing)
        LogViewer.Location = New Point(0, 0)
        LogViewer.Margin = New Padding(4)
        LogViewer.Name = "LogViewer"
        LogViewer.Size = New Size(800, 344)
        LogViewer.TabIndex = 6
        TipLog.SetText(LogViewer, Nothing)
        TipAlert.SetText(LogViewer, Nothing)
        ' 
        ' TipAlert
        ' 
        TipAlert.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipAlert.HideDelay = 5000
        TipAlert.ShadowThickness = 0
        ' 
        ' Log
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(800, 459)
        Controls.Add(LogViewer)
        Controls.Add(LBLLogInfo)
        Controls.Add(BTNDeleteLog)
        Controls.Add(BTNOK)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        TipAlert.SetImage(Me, Nothing)
        TipLog.SetImage(Me, Nothing)
        KeyPreview = True
        MinimumSize = New Size(400, 300)
        Name = "Log"
        StartPosition = FormStartPosition.CenterScreen
        TipLog.SetText(Me, Nothing)
        TipAlert.SetText(Me, Nothing)
        ResumeLayout(False)
    End Sub
    Friend WithEvents BTNOK As Button
    Friend WithEvents BTNDeleteLog As Button
    Friend WithEvents RTBCMLog As Skye.UI.RichTextBoxContextMenu
    Friend WithEvents LBLLogInfo As Skye.UI.Label
    Friend WithEvents TipLog As Skye.UI.ToolTipEX
    Friend WithEvents TipAlert As Skye.UI.ToolTipEX
    Friend WithEvents LogViewerControl1 As Skye.UI.Log.LogViewerControl
    Friend WithEvents LogViewer As Skye.UI.Log.LogViewerControl
End Class
