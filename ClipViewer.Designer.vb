<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClipViewer
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
        TxtBox = New TextBox()
        PicBox = New PictureBox()
        LVFileDrop = New ListView()
        FileIcon = New ColumnHeader()
        FileName = New ColumnHeader()
        FileSize = New ColumnHeader()
        ILFileDrop = New ImageList(components)
        WebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        RTFBox = New Skye.UI.RichTextBox()
        BtnExport = New Button()
        TipClipViewer = New Skye.UI.ToolTipEX(components)
        CType(PicBox, ComponentModel.ISupportInitialize).BeginInit()
        CType(WebView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtBox
        ' 
        TxtBox.BorderStyle = BorderStyle.None
        TxtBox.Dock = DockStyle.Fill
        TipClipViewer.SetImage(TxtBox, Nothing)
        TxtBox.Location = New Point(0, 0)
        TxtBox.Multiline = True
        TxtBox.Name = "TxtBox"
        TxtBox.ReadOnly = True
        TxtBox.Size = New Size(584, 284)
        TxtBox.TabIndex = 0
        TipClipViewer.SetText(TxtBox, Nothing)
        ' 
        ' PicBox
        ' 
        PicBox.Dock = DockStyle.Fill
        TipClipViewer.SetImage(PicBox, Nothing)
        PicBox.Location = New Point(0, 0)
        PicBox.Name = "PicBox"
        PicBox.Size = New Size(584, 284)
        PicBox.SizeMode = PictureBoxSizeMode.Zoom
        PicBox.TabIndex = 1
        PicBox.TabStop = False
        TipClipViewer.SetText(PicBox, Nothing)
        ' 
        ' LVFileDrop
        ' 
        LVFileDrop.Columns.AddRange(New ColumnHeader() {FileIcon, FileName, FileSize})
        LVFileDrop.Dock = DockStyle.Fill
        LVFileDrop.HeaderStyle = ColumnHeaderStyle.Nonclickable
        TipClipViewer.SetImage(LVFileDrop, Nothing)
        LVFileDrop.Location = New Point(0, 0)
        LVFileDrop.Name = "LVFileDrop"
        LVFileDrop.Size = New Size(584, 284)
        LVFileDrop.SmallImageList = ILFileDrop
        LVFileDrop.TabIndex = 3
        TipClipViewer.SetText(LVFileDrop, Nothing)
        LVFileDrop.UseCompatibleStateImageBehavior = False
        LVFileDrop.View = View.Details
        LVFileDrop.Visible = False
        ' 
        ' FileIcon
        ' 
        FileIcon.Text = ""
        FileIcon.Width = 24
        ' 
        ' FileName
        ' 
        FileName.Text = "File Name"
        FileName.Width = 360
        ' 
        ' FileSize
        ' 
        FileSize.Text = "Size"
        FileSize.Width = 140
        ' 
        ' ILFileDrop
        ' 
        ILFileDrop.ColorDepth = ColorDepth.Depth32Bit
        ILFileDrop.ImageSize = New Size(16, 16)
        ILFileDrop.TransparentColor = Color.Transparent
        ' 
        ' WebView
        ' 
        WebView.AllowExternalDrop = True
        WebView.CreationProperties = Nothing
        WebView.DefaultBackgroundColor = Color.White
        WebView.Dock = DockStyle.Fill
        TipClipViewer.SetImage(WebView, Nothing)
        WebView.Location = New Point(0, 0)
        WebView.Name = "WebView"
        WebView.Size = New Size(584, 284)
        WebView.TabIndex = 4
        TipClipViewer.SetText(WebView, Nothing)
        WebView.ZoomFactor = 1R
        ' 
        ' RTFBox
        ' 
        RTFBox.BorderStyle = BorderStyle.None
        RTFBox.Dock = DockStyle.Fill
        TipClipViewer.SetImage(RTFBox, Nothing)
        RTFBox.Location = New Point(0, 0)
        RTFBox.Name = "RTFBox"
        RTFBox.ReadOnly = True
        RTFBox.Size = New Size(584, 284)
        RTFBox.TabIndex = 5
        RTFBox.Text = ""
        TipClipViewer.SetText(RTFBox, Nothing)
        ' 
        ' BtnExport
        ' 
        BtnExport.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnExport.Image = My.Resources.Resources.ImageExport16
        TipClipViewer.SetImage(BtnExport, My.Resources.Resources.ImageExport16)
        BtnExport.Location = New Point(548, 248)
        BtnExport.Name = "BtnExport"
        BtnExport.Size = New Size(24, 24)
        BtnExport.TabIndex = 6
        TipClipViewer.SetText(BtnExport, "Export Clip")
        BtnExport.UseVisualStyleBackColor = True
        ' 
        ' TipClipViewer
        ' 
        TipClipViewer.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipClipViewer.ShadowThickness = 0
        ' 
        ' ClipViewer
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(584, 284)
        ControlBox = False
        Controls.Add(BtnExport)
        Controls.Add(RTFBox)
        Controls.Add(PicBox)
        Controls.Add(WebView)
        Controls.Add(LVFileDrop)
        Controls.Add(TxtBox)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TipClipViewer.SetImage(Me, Nothing)
        KeyPreview = True
        Margin = New Padding(4)
        Name = "ClipViewer"
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.Manual
        TipClipViewer.SetText(Me, Nothing)
        CType(PicBox, ComponentModel.ISupportInitialize).EndInit()
        CType(WebView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBox As TextBox
    Friend WithEvents PicBox As PictureBox
    Friend WithEvents LVFileDrop As ListView
    Friend WithEvents FileIcon As ColumnHeader
    Friend WithEvents FileName As ColumnHeader
    Friend WithEvents FileSize As ColumnHeader
    Friend WithEvents ILFileDrop As ImageList
    Friend WithEvents WebView As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents RTFBox As Skye.UI.RichTextBox
    Friend WithEvents BtnExport As Button
    Friend WithEvents TipClipViewer As Skye.UI.ToolTipEX
End Class
