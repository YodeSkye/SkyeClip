
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Skye.UI
Imports SkyeClip.ClipRepository

Public Class ClipExplorer

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private _searchText As String = String.Empty
    Private _searchFavoritesOnly As Boolean = False
    Private _searchDays As Integer = 0 ' 0 means all days
    Private _searchMode As App.TextSearchMode
    Private _searchCache As New Dictionary(Of Integer, String)
    Private _primaryRowIndex As Integer = -1
    Private Class ClipLoadResult
        Public Property Rows As List(Of Object())
        Public Property TotalCount As Integer
        Public Property FilteredCount As Integer
    End Class

    ' Form Events
    Private Sub ClipExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipClipExplorer)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        Text = App.GetAppTitle() & " " & Text
        RadBtnPlainText.Checked = True
        CMClipActions.Font = App.MenuFont
        CMTxtBox.Font = App.MenuFont
        If App.Settings.ClipExplorerSize.Height >= 0 Then Size = App.Settings.ClipExplorerSize
        If App.Settings.ClipExplorerLocation.Y >= 0 Then Location = App.Settings.ClipExplorerLocation
        LoadClips()
    End Sub
    Private Sub ClipExplorer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        App.Settings.Save()
    End Sub
    Private Sub ClipExplorer_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PanelCE.MouseDown, StatusStripCE.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            ElseIf cSender Is PanelCE Then
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 5, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 5)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub ClipExplorer_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PanelCE.MouseMove, StatusStripCE.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.Settings.ClipExplorerLocation = Location
        End If
    End Sub
    Private Sub ClipExplorer_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PanelCE.MouseUp, StatusStripCE.MouseUp
        mMove = False
    End Sub
    Private Sub ClipExplorer_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.Settings.ClipExplorerLocation = Location
        End If
    End Sub
    Private Sub ClipExplorer_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.Settings.ClipExplorerSize = Size
        End If
    End Sub

    ' Handlers
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipClipExplorer)
    End Sub

    ' Control Events
    Private Sub DGV_SelectionChanged(sender As Object, e As EventArgs) Handles DGV.SelectionChanged
        If DGV.SelectedRows.Count = 0 Then
            RTB.Text = String.Empty
            RTB.BringToFront()
            Return
        End If

        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)
        Dim formats = App.Tray.repo.GetClipFormats(clipId)

        ' Detect FileDrop
        Dim fileDrop = formats.FirstOrDefault(Function(f) f.FormatName = "FileDrop")
        If fileDrop IsNot Nothing Then
            ' Show FileDrop preview
            Dim entries = App.ParseFileDrop(fileDrop.DataBytes)
            ShowFileDrop(entries)
            LVFileDrop.BringToFront()
            Return
        End If

        ' Otherwise show text/HTML/RTF preview
        Dim preview = BuildPreviewText(formats)
        RTB.Text = preview
        RTB.BringToFront()
    End Sub
    Private Sub CMClipActions_Opening(sender As Object, e As CancelEventArgs) Handles CMClipActions.Opening
        If DGV.SelectedRows.Count = 0 Then
            e.Cancel = True
            Return
        End If

        Dim pt = DGV.PointToClient(Cursor.Position)
        Dim hit = DGV.HitTest(pt.X, pt.Y)
        If hit.RowIndex < 0 Then
            e.Cancel = True
            Return
        End If
        _primaryRowIndex = hit.RowIndex

        ' Ensure primary row is selected (but don't break multiselect)
        If Not DGV.Rows(_primaryRowIndex).Selected Then
            DGV.Rows(_primaryRowIndex).Selected = True
        End If

        Dim row = DGV.Rows(_primaryRowIndex)
        Dim clipId = CInt(row.Cells("Id").Value)
        Dim clip = App.Tray.repo.GetClipById(clipId)

        ' --- Favorite toggle ---
        CMICAFavorite.Checked = clip.IsFavorite
        CMICAFavorite.Text = If(clip.IsFavorite, "Unfavorite", "Favorite")

        ' --- Open Source App ---
        If App.Settings.ShowOpenSourceApp Then
            CMICAOpenSourceApp.Visible = True
            If App.IsLegitimateSourceApp(clip.SourceAppPath) Then
                CMICAOpenSourceApp.Enabled = True
                Using ms As New MemoryStream(clip.SourceAppIcon)
                    CMICAOpenSourceApp.Image = Image.FromStream(ms)
                End Using
                CMICAOpenSourceApp.Tag = clip.SourceAppPath
            Else
                CMICAOpenSourceApp.Enabled = False
                CMICAOpenSourceApp.Image = Nothing
            End If
        Else
            CMICAOpenSourceApp.Visible = False
        End If
    End Sub
    Private Sub CMICAUseClip_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAUseClip.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return
        If _primaryRowIndex < 0 Then Return
        If _primaryRowIndex >= DGV.Rows.Count Then Return

        Dim row = DGV.Rows(_primaryRowIndex)
        Dim clipId = CInt(row.Cells("Id").Value)

        App.Tray.repo.RestoreClip(clipId)
        LoadClips()

    End Sub
    Private Sub CMICAFavorite_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAFavorite.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return

        ' Determine the intended action based on menu text
        Dim doFavorite As Boolean = (CMICAFavorite.Text = "Favorite")

        ' Apply to ALL selected clips
        For Each row As DataGridViewRow In DGV.SelectedRows
            Dim clipId = CInt(row.Cells("Id").Value)
            App.Tray.repo.SetFavorite(clipId, doFavorite)
        Next

        LoadClips()
        App.Tray.RefreshMenu()

    End Sub
    Private Sub CMICAClipViewer_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAClipViewer.MouseDown
        App.HideClipViewer()
        If DGV.SelectedRows.Count = 0 Then Return
        If _primaryRowIndex < 0 Then Return
        If _primaryRowIndex >= DGV.Rows.Count Then Return

        Dim row = DGV.Rows(_primaryRowIndex)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.ShowClipViewer(clipId, Nothing, Nothing, True)

    End Sub
    Private Sub CMICAScratchPad_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAScratchPad.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return
        If _primaryRowIndex < 0 Then Return
        If _primaryRowIndex >= DGV.Rows.Count Then Return

        Dim row = DGV.Rows(_primaryRowIndex)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.ShowScratchPad(clipId)

    End Sub
    Private Sub CMICAOpenSourceApp_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAOpenSourceApp.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return

        Dim item = DirectCast(sender, ToolStripMenuItem)
        Dim exePath = TryCast(item.Tag, String)
        If String.IsNullOrWhiteSpace(exePath) Then Exit Sub
        Try
            Process.Start(exePath)
        Catch
            App.WriteToLog("Unable to Open the Source Application: " & exePath)
        End Try

    End Sub
    Private Sub CMICADelete_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICADelete.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return

        For Each row As DataGridViewRow In DGV.SelectedRows
            Dim clipId = CInt(row.Cells("Id").Value)
            App.Tray.repo.DeleteClip(clipId)
        Next

        LoadClips()
        App.Tray.RefreshMenu()

    End Sub
    Private Sub BtnClearSearch_Click(sender As Object, e As EventArgs) Handles BtnClearSearch.Click
        _searchText = String.Empty
        TxtBoxSearch.Text = String.Empty
        _searchFavoritesOnly = False
        ChkBoxFavorites.Checked = False
        _searchDays = 0
        TxtBoxDays.Text = String.Empty
        RadBtnPlainText.Checked = True
        LoadClips()
    End Sub
    Private Sub TxtBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBoxSearch.KeyDown, TxtBoxDays.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            Validate()
        End If
    End Sub
    Private Sub TxtBoxNumbersOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TxtBoxDays.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then e.Handled = True
    End Sub
    Private Sub TxtBoxSearch_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtBoxSearch.PreviewKeyDown, TxtBoxDays.PreviewKeyDown
        CMTxtBox.ShortcutKeys(CType(sender, TextBox), e)
    End Sub
    Private Sub TxtBoxSearch_Validated(sender As Object, e As EventArgs) Handles TxtBoxSearch.Validated
        _searchText = TxtBoxSearch.Text.Trim
        LoadClips()
    End Sub
    Private Sub ChkBoxFavorites_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxFavorites.CheckedChanged
        _searchFavoritesOnly = ChkBoxFavorites.Checked
        LoadClips()
    End Sub
    Private Sub TxtBoxDays_Validated(sender As Object, e As EventArgs) Handles TxtBoxDays.Validated
        TxtBoxDays.SelectAll()
        If Integer.TryParse(TxtBoxDays.Text.Trim, _searchDays) = False Then _searchDays = 0
        LoadClips()
    End Sub
    Private Sub RadBtnText_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnPlainText.CheckedChanged, RadBtnRTF.CheckedChanged, RadBtnHTML.CheckedChanged, RadBtnAllText.CheckedChanged
        If RadBtnPlainText.Checked Then
            _searchMode = App.TextSearchMode.PlainText
        ElseIf RadBtnRTF.Checked Then
            _searchMode = App.TextSearchMode.RichText
        ElseIf RadBtnHTML.Checked Then
            _searchMode = App.TextSearchMode.HTMLText
        ElseIf RadBtnAllText.Checked Then
            _searchMode = App.TextSearchMode.AllText
        End If
        _searchCache.Clear()
        LoadClips()
        Debug.WriteLine("Search Mode: " & _searchMode.ToString)
    End Sub

    ' Methods
    Private Async Sub LoadClips()
        TSSLabelStatus.Text = "Working..."
        TSSLabelStatus.ForeColor = Color.Red
        StatusStripCE.Refresh()

        Dim result = Await Task.Run(Function() BuildClipList())

        ' Form might be closing — bail out safely
        If Me.IsDisposed OrElse DGV.IsDisposed OrElse DGV.Columns.Count = 0 Then Return

        DGV.Rows.Clear()
        For Each r In result.Rows
            DGV.Rows.Add(r)
        Next

        TSSLabelStatus.Text = $"Showing {result.FilteredCount} of {result.TotalCount} Clips"
        TSSLabelStatus.ResetForeColor()
    End Sub
    Private Function BuildClipList() As ClipLoadResult
        Dim result As New ClipLoadResult()
        Dim rows As New List(Of Object())()

        Dim allClips = App.Tray.repo.GetAllClips()
        result.TotalCount = allClips.Count

        Dim filtered = allClips

        If _searchFavoritesOnly Then
            filtered = filtered.Where(Function(c) c.IsFavorite).ToList()
        End If

        If _searchDays > 0 Then
            Dim cutoff = DateTime.UtcNow.AddDays(-_searchDays)
            filtered = filtered.Where(Function(c) c.CreatedAt >= cutoff).ToList()
        End If

        If _searchText <> "" Then
            filtered = filtered.Where(Function(c)
                                          Dim text = GetCachedSearchText(c.Id)
                                          Return text.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                                      End Function).ToList()
        End If

        result.FilteredCount = filtered.Count

        ' Build rows
        For Each c In filtered
            Dim iconImg As Image = Nothing
            If c.SourceAppIcon IsNot Nothing AndAlso c.SourceAppIcon.Length > 0 Then
                Using ms As New MemoryStream(c.SourceAppIcon)
                    iconImg = Image.FromStream(ms)
                End Using
            End If

            rows.Add({
            c.Id,
            c.Preview,
            c.CreatedAt.ToString("g"),
            c.LastUsedAt.ToString("g"),
            c.SourceAppName,
            iconImg,
            c.IsFavorite
        })
        Next

        result.Rows = rows
        Return result
    End Function
    Private Function BuildPreviewText(formats As List(Of ClipData)) As String
        ' 1) Unicode text
        Dim uni = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_UNICODETEXT)
        If uni IsNot Nothing Then
            Return NormalizeUnicodeText(uni.DataBytes)
        End If

        ' 2) ANSI text
        Dim ansi = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_TEXT)
        If ansi IsNot Nothing Then
            Return Encoding.Default.GetString(ansi.DataBytes)
        End If

        ' 3) RTF → plain text
        Dim rtf = formats.FirstOrDefault(Function(f) f.FormatName.Contains("rtf", StringComparison.OrdinalIgnoreCase))
        If rtf IsNot Nothing Then
            Dim rtfString = Encoding.ASCII.GetString(rtf.DataBytes)
            Return App.NormalizeRtf(rtfString)
        End If

        ' 4) HTML → fragment
        Dim html = formats.FirstOrDefault(Function(f) f.FormatName.Contains("html", StringComparison.OrdinalIgnoreCase))
        If html IsNot Nothing Then
            Dim htmlString = Encoding.UTF8.GetString(html.DataBytes)
            Return ClipRepository.ExtractHtmlFragment(htmlString)
        End If

        Return "< No Text Formats >"
    End Function
    Private Sub ShowFileDrop(entries As List(Of FileDropEntry))
        ' Clear old UI content
        LVFileDrop.Items.Clear()
        ILFileDrop.Images.Clear()

        ' Compute totals
        Dim count = entries.Count
        Dim totalBytes As Long = entries.
        Where(Function(e) File.Exists(e.FullPath)).Sum(Function(e) New FileInfo(e.FullPath).Length)
        Dim totalSizeText = Skye.Common.FormatFileSize(totalBytes, Skye.Common.FormatFileSizeUnits.Auto)

        ' Update column headers
        LVFileDrop.Columns(1).Text = $"Name ({count})"
        LVFileDrop.Columns(2).Text = $"Size ({totalSizeText})"

        For Each e In entries
            Dim iconIndex As Integer = -1

            ' Add icon if available
            If e.Icon IsNot Nothing Then
                ILFileDrop.Images.Add(e.Icon)
                iconIndex = ILFileDrop.Images.Count - 1
            End If

            ' Build ListView item
            Dim item As New ListViewItem("", iconIndex)
            item.SubItems.Add(e.FileName)
            item.SubItems.Add(e.SizeText)
            item.Tag = e.FullPath

            LVFileDrop.Items.Add(item)
        Next

        ' Show the ListView preview
        LVFileDrop.Visible = True
        LVFileDrop.BringToFront()
    End Sub
    Private Function GetCachedSearchText(clipId As Integer) As String
        Dim cached As String = Nothing
        If _searchCache.TryGetValue(clipId, cached) Then
            Return cached
        End If

        Dim text = App.Tray.repo.GetSearchableText(clipId, _searchMode)
        _searchCache(clipId) = text
        Return text
    End Function
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
