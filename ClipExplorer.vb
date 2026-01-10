
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Skye.UI
Imports SkyeClip.ClipRepository

Public Class ClipExplorer

    ' Declarations
    Private _searchText As String = String.Empty
    Private _searchFavoritesOnly As Boolean = False
    Private _searchDays As Integer = 0 ' 0 means all days
    Private _searchMode As App.TextSearchMode
    Private _searchCache As New Dictionary(Of Integer, String)
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
        LoadClips()
    End Sub

    ' Handlers
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipClipExplorer)
    End Sub

    ' Control Events
    Private Sub DGV_SelectionChanged(sender As Object, e As EventArgs) Handles DGV.SelectionChanged
        If DGV.SelectedRows.Count = 0 Then
            RTB.Text = String.Empty
            Return
        End If

        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)

        Dim formats = App.Tray.repo.GetClipFormats(clipId)
        Dim preview = BuildPreviewText(formats)

        RTB.Text = preview
    End Sub
    Private Sub DGV_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            DGV.ClearSelection()
            DGV.Rows(e.RowIndex).Selected = True
        End If
    End Sub
    Private Sub CMClipActions_Opening(sender As Object, e As CancelEventArgs) Handles CMClipActions.Opening
        If DGV.SelectedRows.Count = 0 Then
            e.Cancel = True
            Return
        End If

        Dim row = DGV.SelectedRows(0)
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
        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.Tray.repo.RestoreClip(clipId)
        LoadClips()
    End Sub
    Private Sub CMICAFavorite_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAFavorite.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return
        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.Tray.repo.ToggleFavorite(clipId)
        LoadClips()
        App.Tray.RefreshMenu()
    End Sub
    Private Sub CMICAClipViewer_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAClipViewer.MouseDown
        App.HideClipViewer()
        If DGV.SelectedRows.Count = 0 Then Return
        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.ShowClipViewer(clipId, Nothing, Nothing, True)
    End Sub
    Private Sub CMICAScratchPad_MouseDown(sender As Object, e As MouseEventArgs) Handles CMICAScratchPad.MouseDown
        If DGV.SelectedRows.Count = 0 Then Return
        Dim row = DGV.SelectedRows(0)
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
        Dim row = DGV.SelectedRows(0)
        Dim clipId = CInt(row.Cells("Id").Value)
        App.Tray.repo.DeleteClip(clipId)
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

        Return "(no text formats)"
    End Function
    Private Function GetCachedSearchText(clipId As Integer) As String
        Dim cached As String = Nothing
        If _searchCache.TryGetValue(clipId, cached) Then
            Return cached
        End If

        Dim text = App.Tray.repo.GetSearchableText(clipId, _searchMode)
        _searchCache(clipId) = text
        Return text
    End Function

End Class
