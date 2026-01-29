
Public Class MenuBuilder

    Friend Shared Function BuildMenu(repo As ClipRepository, commonActions As List(Of App.CommonAction), clipClickHandler As MouseEventHandler, menuKeyHandler As KeyEventHandler) As ContextMenuStrip
        Dim menu As New ContextMenuStrip With {
            .Font = App.MenuFont,
            .ShowItemToolTips = False}

        ' ============================================================
        ' --- LIVE CLIPBOARD ITEM (always first) ---
        ' ============================================================
        Dim liveItem As New ToolStripMenuItem(App.CBLivePreview) With {
            .Tag = Nothing,
            .Font = New Font(App.MenuFont, FontStyle.Bold),
            .Image = My.Resources.IconApp.ToBitmap}
        menu.Items.Add(liveItem)
        menu.Items.Add(New ToolStripSeparator())
        ' ============================================================

        ' --- Recents ---
        Dim clips = repo.GetRecentClips(App.Settings.MaxClips)
        If clips Is Nothing OrElse clips.Count = 0 Then
            Dim none As New ToolStripMenuItem("< No Saved Clips >") With {
                .Enabled = False,
                .Font = App.MenuFont}
            menu.Items.Add(none)
        Else
            For Each clip In clips
                Dim previewText = clip.Preview
                'Dim previewText = System.Text.RegularExpressions.Regex.Replace(
                '    rawText.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " "),
                '    "\s+", " ").Trim()
                Dim preview = If(String.IsNullOrWhiteSpace(previewText), "< No Preview >", previewText)

                Dim item As New ToolStripMenuItem(preview) With {
                    .Tag = clip.Id,
                    .Checked = clip.IsFavorite,
                    .Font = App.MenuFont
                }

                'If Not String.Equals(preview, rawText, StringComparison.Ordinal) Then
                '    item.ToolTipText = rawText
                'End If

                If clip.SourceAppIcon IsNot Nothing AndAlso clip.SourceAppIcon.Length > 0 Then
                    Using ms As New IO.MemoryStream(clip.SourceAppIcon)
                        item.Image = Image.FromStream(ms)
                    End Using
                End If

                AddHandler item.MouseDown, clipClickHandler
                menu.Items.Add(item)
            Next
        End If

        ' --- Favorites submenu ---
        Dim favMenu = BuildFavoritesMenu(repo, clipClickHandler, menuKeyHandler)
        If favMenu IsNot Nothing Then
            Dim favSeparator As New ToolStripSeparator() With {
                .Name = "FavoritesSeparator",
                .Font = App.MenuFont}
            menu.Items.Add(favSeparator)
            menu.Items.Add(favMenu)
        End If

        ' --- Common actions ---
        If commonActions IsNot Nothing AndAlso commonActions.Count > 0 Then
            Dim commonSeparator As New ToolStripSeparator() With {.Name = "CommonActionsSeparator"}
            menu.Items.Add(commonSeparator)
            For Each action In commonActions
                Dim cmi As New ToolStripMenuItem(action.Text, action.Image) With {.Font = App.MenuFont}
                AddHandler cmi.MouseDown, action.Handler
                menu.Items.Add(cmi)
            Next
        End If

        Return menu
    End Function
    Friend Shared Function BuildFavoritesMenu(repo As ClipRepository, clipClickHandler As MouseEventHandler, menuKeyHandler As KeyEventHandler) As ToolStripMenuItem
        Dim favorites = repo.GetFavoriteClips(App.Settings.MaxClips)
        If favorites Is Nothing OrElse favorites.Count = 0 Then
            Return Nothing
        End If

        Dim favMenu As New ToolStripMenuItem("Favorites") With {
            .Name = "FavoritesMenu",
            .Font = App.MenuFont,
            .Image = My.Resources.ImageFavorites16,
            .Checked = True}

        Dim favDropDown As New ContextMenuStrip With {
            .Name = "FavoritesDropDown",
            .Font = App.MenuFont,
            .Renderer = New Skye.UI.SkyeMenuRenderer
        }
        AddHandler favDropDown.KeyDown, menuKeyHandler

        For Each fav In favorites
            Dim rawText = fav.Preview
            Dim previewText = System.Text.RegularExpressions.Regex.Replace(
                rawText.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " "),
                "\s+", " ").Trim()
            Dim preview = If(String.IsNullOrWhiteSpace(previewText), "< No Preview >", previewText)
            Dim favItem As New ToolStripMenuItem(preview) With {
                .Tag = fav.Id,
                .Checked = fav.IsFavorite}
            If Not String.Equals(preview, rawText, StringComparison.Ordinal) Then
                favItem.ToolTipText = rawText
            End If
            If fav.SourceAppIcon IsNot Nothing AndAlso fav.SourceAppIcon.Length > 0 Then
                Using ms As New IO.MemoryStream(fav.SourceAppIcon)
                    favItem.Image = Image.FromStream(ms)
                End Using
            Else
                favItem.Image = My.Resources.ImageFavorites16
            End If
            AddHandler favItem.MouseDown, clipClickHandler
            favDropDown.Items.Add(favItem)
        Next

        favMenu.DropDown = favDropDown
        Return favMenu
    End Function

End Class
