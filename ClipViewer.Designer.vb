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
        RTFBox = New RichTextBox()
        LVFileDrop = New ListView()
        FileIcon = New ColumnHeader()
        FileName = New ColumnHeader()
        FileSize = New ColumnHeader()
        ILFileDrop = New ImageList(components)
        WebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        CType(PicBox, ComponentModel.ISupportInitialize).BeginInit()
        CType(WebView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtBox
        ' 
        TxtBox.BorderStyle = BorderStyle.None
        TxtBox.Dock = DockStyle.Fill
        TxtBox.Location = New Point(0, 0)
        TxtBox.Multiline = True
        TxtBox.Name = "TxtBox"
        TxtBox.ReadOnly = True
        TxtBox.Size = New Size(584, 284)
        TxtBox.TabIndex = 0
        ' 
        ' PicBox
        ' 
        PicBox.Dock = DockStyle.Fill
        PicBox.Location = New Point(0, 0)
        PicBox.Name = "PicBox"
        PicBox.Size = New Size(584, 284)
        PicBox.SizeMode = PictureBoxSizeMode.Zoom
        PicBox.TabIndex = 1
        PicBox.TabStop = False
        ' 
        ' RTFBox
        ' 
        RTFBox.BorderStyle = BorderStyle.None
        RTFBox.Dock = DockStyle.Fill
        RTFBox.Location = New Point(0, 0)
        RTFBox.Name = "RTFBox"
        RTFBox.ReadOnly = True
        RTFBox.Size = New Size(584, 284)
        RTFBox.TabIndex = 2
        RTFBox.Text = ""
        ' 
        ' LVFileDrop
        ' 
        LVFileDrop.Columns.AddRange(New ColumnHeader() {FileIcon, FileName, FileSize})
        LVFileDrop.Dock = DockStyle.Fill
        LVFileDrop.HeaderStyle = ColumnHeaderStyle.Nonclickable
        LVFileDrop.Location = New Point(0, 0)
        LVFileDrop.Name = "LVFileDrop"
        LVFileDrop.Size = New Size(584, 284)
        LVFileDrop.SmallImageList = ILFileDrop
        LVFileDrop.TabIndex = 3
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
        FileSize.Width = 100
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
        WebView.Location = New Point(0, 0)
        WebView.Name = "WebView"
        WebView.Size = New Size(584, 284)
        WebView.TabIndex = 4
        WebView.ZoomFactor = 1R
        ' 
        ' ClipViewer
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(584, 284)
        ControlBox = False
        Controls.Add(WebView)
        Controls.Add(LVFileDrop)
        Controls.Add(TxtBox)
        Controls.Add(RTFBox)
        Controls.Add(PicBox)
        Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        KeyPreview = True
        Margin = New Padding(4)
        Name = "ClipViewer"
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Show
        StartPosition = FormStartPosition.Manual
        CType(PicBox, ComponentModel.ISupportInitialize).EndInit()
        CType(WebView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBox As TextBox
    Friend WithEvents PicBox As PictureBox
    Friend WithEvents RTFBox As RichTextBox
    Friend WithEvents LVFileDrop As ListView
    Friend WithEvents FileIcon As ColumnHeader
    Friend WithEvents FileName As ColumnHeader
    Friend WithEvents FileSize As ColumnHeader
    Friend WithEvents ILFileDrop As ImageList
    Friend WithEvents WebView As Microsoft.Web.WebView2.WinForms.WebView2
End Class
