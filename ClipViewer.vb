
Imports System.Data.SQLite
Imports System.IO
Imports System.Runtime.InteropServices
Imports Skye.WinAPI

Friend Class ClipViewer

    ' Declarations
    Friend fadeInTimer As Timer
    Friend fadeOutTimer As Timer
    Private Const FadeStep As Double = 0.08 ' adjust for speed
    Private Browser As New WebBrowser
    Private _clipID As Integer
    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean
        Get
            Return True
        End Get
    End Property

    ' Form Events
    Public Sub New()
        InitializeComponent()
        Opacity = 0
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipClipViewer)

        fadeInTimer = New Timer With {.Interval = 15}
        AddHandler fadeInTimer.Tick, AddressOf FadeIn_Tick
        fadeOutTimer = New Timer With {.Interval = 15}
        AddHandler fadeOutTimer.Tick, AddressOf FadeOut_Tick

    End Sub
    Private Async Sub ClipViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)

        ' Initialize WebView
        Try
            Await WebView.EnsureCoreWebView2Async()
            If WebView.CoreWebView2 IsNot Nothing Then
                With WebView.CoreWebView2.Settings
                    .AreDefaultScriptDialogsEnabled = False
                    .AreDevToolsEnabled = False
                    .IsStatusBarEnabled = False
                End With
                WebView.DefaultBackgroundColor = Color.Transparent
            End If
        Catch ex As Exception
        End Try

        Dim emptyMenu As New ContextMenuStrip()
        TxtBox.ContextMenuStrip = emptyMenu

    End Sub
    Friend Sub ShowAtScreenPoint(clipID As Integer, screenPos As System.Drawing.Point)
        _clipID = clipID
        Location = New System.Drawing.Point(screenPos.X, screenPos.Y)
        Show()

        ' Load formats
        Dim dt = LoadFormatsForClip(clipID)
        Dim best = PickBestFormat(dt)
        If best IsNot Nothing Then ShowClipData(best)

        fadeOutTimer?.Stop()
        fadeInTimer.Start()
    End Sub
    Private Sub ClipViewer_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        StartFadeOut()
    End Sub
    Private Sub ClipViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then StartFadeOut()
    End Sub

    ' Control Events
    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Close()
        App.ExportClipToFile(_clipID)
    End Sub

    ' Handlers
    Private Sub FadeIn_Tick(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Me.Disposing Then
            fadeInTimer.Stop()
            fadeOutTimer.Stop()
            Return
        End If
        If Opacity < 1 Then
            Opacity += FadeStep
        Else
            fadeInTimer.Stop()
        End If
    End Sub
    Private Sub FadeOut_Tick(sender As Object, e As EventArgs)
        If Me.IsDisposed OrElse Me.Disposing Then
            fadeInTimer.Stop()
            fadeOutTimer.Stop()
            Return
        End If
        If Opacity > 0 Then
            Opacity -= FadeStep
        Else
            fadeOutTimer.Stop()
            Close()
        End If
    End Sub

    ' Methods
    Private Sub StartFadeOut()
        If Not IsDisposed Then
            fadeInTimer?.Stop()
            fadeOutTimer.Start()
        End If
    End Sub
    Private Function LoadFormatsForClip(clipID As Integer) As DataTable
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Dim da As New SQLiteDataAdapter(
                "SELECT FormatName, Data FROM ClipFormats WHERE EntryID = @id ORDER BY ID",
                conn
            )
            da.SelectCommand.Parameters.AddWithValue("@id", clipID)

            Dim dt As New DataTable()
            da.Fill(dt)
            Return dt
        End Using
    End Function
    Private Function PickBestFormat(dt As DataTable) As DataRow
        Dim priority = {
            "HTML Format",
            "Rich Text Format",
            "Bitmap",
            "PNG",
            "JFIF",
            "UnicodeText",
            "Text"
        }

        For Each p In priority
            Dim rows = dt.Select("FormatName = '" & p & "'")
            If rows.Length > 0 Then Return rows(0)
        Next

        ' fallback: first available format
        If dt.Rows.Count > 0 Then Return dt.Rows(0)

        Return Nothing
    End Function
    Private Sub ShowClipData(row As DataRow)
        Dim format As String = CStr(row("FormatName"))
        Dim bytes As Byte() = CType(row("Data"), Byte())

        Select Case format
            Case "FileDrop"
                ShowFileDrop(bytes)
            Case "HTML Format"
                ShowHtml(bytes)
            Case "Rich Text Format"
                ShowRtf(bytes)
            Case "CF_DIB", "CF_DIBV5"
                ShowImage(bytes)
            Case "UnicodeText"
                ShowUnicode(bytes)
            Case "Text"
                ShowAnsi(bytes)
            Case Else
                ShowRaw(bytes, format)
        End Select
        BtnExport.BringToFront()

    End Sub
    Private Function ExtractHtmlFragment(rawHtml As String) As String
        Const startTag As String = "<!--StartFragment-->"
        Const endTag As String = "<!--EndFragment-->"

        Dim startIndex As Integer = rawHtml.IndexOf(startTag)
        Dim endIndex As Integer = rawHtml.IndexOf(endTag)

        If startIndex >= 0 AndAlso endIndex > startIndex Then
            startIndex += startTag.Length
            Return rawHtml.Substring(startIndex, endIndex - startIndex)
        End If

        ' If no markers found, return the whole HTML
        Return rawHtml
    End Function
    Private Sub ShowFileDrop(bytes As Byte())

        Dim entries = App.ParseFileDrop(bytes)

        LVFileDrop.Items.Clear()
        ILFileDrop.Images.Clear()

        ' Compute file-only size first (instant)
        Dim fileBytes As Long = entries.Where(Function(e) File.Exists(e.FullPath)).Sum(Function(e) New FileInfo(e.FullPath).Length)
        Dim count = entries.Count
        Dim totalSizeText = Skye.Common.FormatFileSize(fileBytes, Skye.Common.FormatFileSizeUnits.Auto)

        ' Update headers immediately
        LVFileDrop.Columns(1).Text = $"Name ({count})"
        LVFileDrop.Columns(2).Text = $"Size ({totalSizeText})"

        ' Populate list
        For Each e In entries
            Dim iconIndex As Integer = -1
            If e.Icon IsNot Nothing Then
                ILFileDrop.Images.Add(e.Icon)
                iconIndex = ILFileDrop.Images.Count - 1
            End If
            Dim item As New ListViewItem("", iconIndex)
            item.SubItems.Add(e.FileName)
            item.SubItems.Add(e.SizeText)
            item.Tag = e.FullPath
            LVFileDrop.Items.Add(item)
        Next

        LVFileDrop.Visible = True
        LVFileDrop.BringToFront()

        ' Compute folder sizes asynchronously
        UpdateFolderSizesAsync(entries, fileBytes)

    End Sub
    Private Async Sub UpdateFolderSizesAsync(entries As List(Of FileDropEntry), fileBytes As Long)

        Dim folderBytes = Await App.ComputeFolderSizesAsync(entries)
        Dim totalBytes = fileBytes + folderBytes
        Dim totalSizeText = Skye.Common.FormatFileSize(totalBytes, Skye.Common.FormatFileSizeUnits.Auto)

        App.Tray.RunOnUI(Sub()
                             LVFileDrop.Columns(2).Text = $"Size ({totalSizeText})"
                         End Sub)

    End Sub
    Private Sub ShowHtml(bytes As Byte())
        Dim rawhtml As String = System.Text.Encoding.UTF8.GetString(bytes)
        Dim fragment As String = ExtractHtmlFragment(rawhtml)
        ' Wrap in minimal HTML so WebView2 renders cleanly
        Dim wrappedHtml As String =
            "<html><body style='margin:0;padding:0;background:" &
            ColorTranslator.ToHtml(Me.BackColor) & ";'>" &
            fragment &
            "</body></html>"

        If WebView.CoreWebView2 Is Nothing Then
            ' Delay until ready
            AddHandler WebView.CoreWebView2InitializationCompleted,
                Sub()
                    WebView.CoreWebView2?.NavigateToString(wrappedHtml)
                    WebView.Visible = True
                    WebView.BringToFront()
                    BtnExport.BringToFront()
                End Sub
            Return
        End If
        ' If it's already initialized, load immediately
        WebView.CoreWebView2?.NavigateToString(wrappedHtml)
        WebView.Visible = True
        WebView.BringToFront()

    End Sub
    Private Sub ShowRtf(bytes As Byte())
        Dim rtf As String = System.Text.Encoding.UTF8.GetString(bytes)
        RTFBox.Rtf = rtf
        RTFBox.Visible = True
        RTFBox.BringToFront()
    End Sub
    Private Sub ShowUnicode(bytes As Byte())
        Dim txt As String = System.Text.Encoding.Unicode.GetString(bytes)
        TxtBox.Text = txt
        TxtBox.Visible = True
        TxtBox.BringToFront()
    End Sub
    Private Sub ShowAnsi(bytes As Byte())
        Dim txt As String = System.Text.Encoding.Default.GetString(bytes)
        TxtBox.Text = txt
        TxtBox.Visible = True
        TxtBox.BringToFront()
    End Sub
    Private Sub ShowRaw(bytes As Byte(), format As String)
        TxtBox.Text = $"[{format}] {bytes.Length} bytes"
        TxtBox.Visible = True
        TxtBox.BringToFront()
    End Sub
    Private Sub ShowImage(dibBytes As Byte())
        Dim bmpBytes = WrapDibAsBmp(dibBytes)

        Using ms As New MemoryStream(bmpBytes)
            Dim img As Image = Image.FromStream(ms)
            PicBox.Image = img
        End Using

        PicBox.Visible = True
        PicBox.BringToFront()
    End Sub
    Private Function WrapDibAsBmp(dib() As Byte) As Byte()
        ' BITMAPFILEHEADER is 14 bytes
        Dim header(13) As Byte

        ' bfType = "BM"
        header(0) = &H42
        header(1) = &H4D

        ' bfOffBits = 14 + biSize (DWORD at offset 0 of DIB)
        Dim biSize As Integer = BitConverter.ToInt32(dib, 0)
        Dim bfOffBits As Integer = 14 + biSize
        Buffer.BlockCopy(BitConverter.GetBytes(bfOffBits), 0, header, 10, 4)

        ' bfSize = total file size = header + dib length
        Dim bfSize As Integer = 14 + dib.Length
        Buffer.BlockCopy(BitConverter.GetBytes(bfSize), 0, header, 2, 4)

        ' Combine header + dib
        Dim result(header.Length + dib.Length - 1) As Byte
        Buffer.BlockCopy(header, 0, result, 0, header.Length)
        Buffer.BlockCopy(dib, 0, result, header.Length, dib.Length)

        Return result
    End Function

End Class
