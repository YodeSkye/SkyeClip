
Imports System.IO

Friend Class TrayAppContext
    Inherits ApplicationContext

    ' Declarations
    Private ReadOnly NIClipboard As NotifyIcon
    Private ReadOnly Watcher As ClipboardWatcher
    Private ReadOnly clipboardTimer As System.Windows.Forms.Timer
    Private ReadOnly commonActions As New List(Of CommonAction) From {
        New CommonAction With {
            .Text = "Clear Clipboard",
            .Handler = AddressOf OnClearClipboard_MouseDown,
            .Image = My.Resources.IconApp.ToBitmap
        },
        New CommonAction With {
            .Text = "Scratch Pad",
            .Handler = AddressOf OnScratchPad_MouseDown,
            .Image = My.Resources.imageScratchPad16
        },
        New CommonAction With {
            .Text = "App View",
            .Handler = AddressOf OnAppView_MouseDown,
            .Image = My.Resources.ImageSettings16
        }}
    Friend ReadOnly repo As ClipRepository
    Private ReadOnly ClipCM As New ContextMenuStrip()
    Private ClipCMCurrentClipId As Integer

    ' Blink Notification
    Private ReadOnly blinkTimer As Timer
    Private Const MaxBlinks As Integer = 8 ' 6 toggles = 3 full blinks
    Private blinkState As Boolean
    Private blinkCount As Integer

    ' Filters
    Private suppressClose As Boolean = False
    Private lastToastTime As DateTime = DateTime.MinValue
    Private ReadOnly toastCooldownMs As Integer = 100
    Private lastHash As String = Nothing
    Private lastLength As Integer = -1
    Private lastClipTime As DateTime = DateTime.MinValue
    Private Const duplicateThresholdMs As Integer = 5000
    Private lastClearTime As DateTime = DateTime.MinValue
    Private Const clearSuppressionMs As Integer = 300

    ' Constructor
    Friend Sub New()

        ' Show ChangeLog if new version
        If App.GetSimpleVersion() <> ChangeLogLastVersionShown Then
            ChangeLogLastVersionShown = App.GetSimpleVersion()
            App.Settings.Save()
            App.ShowChangeLog()
        End If

        ' Instantiate repository once, pointing to your database file
        repo = New ClipRepository()

        ' Auto-purge old clips once per day
        If App.Settings.AutoPurge Then
            If Date.Today > App.Settings.LastPurgeDate Then
                Dim cutoff = DateTime.Now.AddDays(-App.Settings.PurgeDays)
                repo.PurgeClips(cutoff)
                App.Settings.LastPurgeDate = Date.Today
                App.Settings.Save()
            End If
        End If

        ' Set initial live clipboard preview
        App.CBLivePreview = App.BuildLiveClipboardPreview()

        ' Tray icon setup
        NIClipboard = New NotifyIcon With {
            .Icon = My.Resources.IconApp,
            .Visible = True,
            .Text = App.GetAppTitle}
        BuildMenu()

        ' Clipboard watcher
        Watcher = New ClipboardWatcher()
        App.AppHandle = Watcher.Handle
        AddHandler Watcher.ClipboardChanged, AddressOf OnClipboardChanged

        ' Clipboard debounce & stabilization timer
        clipboardTimer = New Timer With {.Interval = 30}
        AddHandler clipboardTimer.Tick, AddressOf OnClipboardStabilized

        ' Blink Timer for Notifications
        blinkTimer = New Timer With {.Interval = 250} ' milliseconds per toggle
        AddHandler blinkTimer.Tick, AddressOf BlinkTimer_Tick

        ' Clip Context Menu
        Dim cmi As ToolStripMenuItem
        cmi = New ToolStripMenuItem("Preview", My.Resources.IconApp.ToBitmap, AddressOf OnClipCMPreviewClick) With {.Name = "Preview"}
        ClipCM.Items.Add(cmi)
        ClipCM.Items.Add(New ToolStripSeparator())
        cmi = New ToolStripMenuItem("Favorite", My.Resources.ImageFavorites16, AddressOf OnClipCMFavorite) With {.Name = "Favorite"}
        ClipCM.Items.Add(cmi)
        ClipCM.Items.Add("View Clip", My.Resources.imageClipViewer16, AddressOf OnClipCMViewClip)
        ClipCM.Items.Add("Send To Scratch Pad", My.Resources.imageScratchPad16, AddressOf OnCLipCMScratchPad)
        cmi = New ToolStripMenuItem("Open Source App", Nothing, AddressOf OnCLipCMOpenSourceApp) With {.Name = "OpenSourceApp"}
        ClipCM.Items.Add(cmi)
        ClipCM.Items.Add(New ToolStripSeparator())
        ClipCM.Items.Add("Delete", My.Resources.ImageClearRemoveDelete16, AddressOf OnClipCMDelete)

    End Sub

    ' Handlers
    Private Sub OnClipboardChanged()

        'Debug.Print("=== CLIPBOARD EVENT === " & DateTime.Now.ToString("HH:mm:ss.fff"))
        'Dim data = Clipboard.GetDataObject()
        'If data Is Nothing Then
        '    Debug.Print("Clipboard empty or unavailable")
        'Else
        '    For Each fmt In data.GetFormats()
        '        Debug.Print("Format: " & fmt)
        '    Next
        'End If

        clipboardTimer.Stop()
        clipboardTimer.Start()

    End Sub
    Private Sub OnClipboardStabilized(sender As Object, e As EventArgs)
        clipboardTimer.Stop()

        'Debug.Print("=== STABILIZED === " & DateTime.Now.ToString("HH:mm:ss.fff"))

        ' Filtering
        Dim now = DateTime.Now
        Dim signatureBytes = GetClipboardSignature()
        If ShouldIgnoreClip(signatureBytes, now) Then Return

        ' Database
        App.CBLivePreview = App.BuildLiveClipboardPreview()
        repo.SaveClip()

        ' Menu
        BuildMenu()

        ' Notifications
        If App.Settings.BlinkOnNewClip Then StartBlink()
        If App.Settings.NotifyOnNewClip Then
            Dim now2 = DateTime.Now
            If (now2 - lastToastTime).TotalMilliseconds > toastCooldownMs Then 'Toast cooldown to prevent real clip toast spam
                lastToastTime = now2
                Dim toast As New Skye.UI.ToastOptions With {
                .Title = App.GetAppTitle,
                .Message = App.CBLivePreview,
                .Icon = My.Resources.IconApp,
                .Duration = 4000,
                .PlaySound = True}
                Skye.UI.Toast.ShowToast(toast)
            End If
        End If
        NIClipboard.Text = App.GetAppTitle & vbCrLf & App.CBLivePreview

    End Sub
    Private Sub OnClipSelected(sender As Object, e As MouseEventArgs)
        Dim item = TryCast(sender, ToolStripMenuItem)
        If item Is Nothing OrElse item.Tag Is Nothing Then Exit Sub

        Select Case e.Button
            Case MouseButtons.Left
                Dim clipID As Integer
                If Not Integer.TryParse(item.Tag.ToString(), clipID) Then Exit Sub
                repo.RestoreClip(clipID)
                BuildMenu()
            Case MouseButtons.Right
                If Integer.TryParse(item.Tag.ToString(), ClipCMCurrentClipId) Then
                    ClipCM.Items("Preview").Text = item.Text
                    Dim favItem = DirectCast(ClipCM.Items("Favorite"), ToolStripMenuItem)
                    favItem.Checked = item.Checked
                    If favItem.Checked Then
                        favItem.Text = "Unfavorite"
                    Else
                        favItem.Text = "Favorite"
                    End If
                    If App.Settings.ShowOpenSourceApp Then
                        Dim clip = repo.GetClipById(ClipCMCurrentClipId)
                        Dim openItem = DirectCast(ClipCM.Items("OpenSourceApp"), ToolStripMenuItem)
                        openItem.Visible = True
                        If App.IsLegitimateSourceApp(clip.SourceAppPath) Then
                            openItem.Enabled = True
                            Using ms As New MemoryStream(clip.SourceAppIcon)
                                openItem.Image = Image.FromStream(ms)
                            End Using
                            openItem.Tag = clip.SourceAppPath
                        Else
                            openItem.Enabled = False
                            openItem.Image = Nothing
                        End If
                    Else
                        Dim openItem = DirectCast(ClipCM.Items("OpenSourceApp"), ToolStripMenuItem)
                        openItem.Visible = False
                    End If
                    ClipCM.Show(Cursor.Position)
                End If
        End Select

    End Sub
    Private Sub OnMenuClosing(sender As Object, e As ToolStripDropDownClosingEventArgs)
        'Debug.WriteLine(suppressClose.ToString & " - " & e.CloseReason.ToString)
        If suppressClose AndAlso e.CloseReason = ToolStripDropDownCloseReason.AppClicked Then
            e.Cancel = True
        End If
        App.HideClipViewer()
        suppressClose = False
    End Sub
    Private Sub OnMenuKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = App.Settings.HotKeys.ToggleFavorite Then
            Dim cms = DirectCast(sender, ContextMenuStrip)
            Dim pos = cms.PointToClient(Cursor.Position)
            Dim item As ToolStripItem = cms.GetItemAt(pos)
            Dim hovered As ToolStripMenuItem = TryCast(item, ToolStripMenuItem)
            If hovered IsNot Nothing AndAlso hovered.Tag IsNot Nothing Then
                Dim clipID As Integer
                If Integer.TryParse(hovered.Tag.ToString(), clipID) Then
                    repo.ToggleFavorite(clipID)
                    Dim newState = Not hovered.Checked
                    hovered.Checked = newState
                    UpdateFavoriteItemCheckedState(App.CMTray, clipID, newState)
                    RefreshFavoritesMenu(App.CMTray, repo, AddressOf OnClipSelected, AddressOf OnMenuKeyDown)
                End If
            End If
        ElseIf e.KeyCode = App.Settings.HotKeys.ShowViewer Then
            e.SuppressKeyPress = True
            suppressClose = True
            Dim cms = DirectCast(sender, ContextMenuStrip)
            App.HideClipViewer()
            Dim pos = cms.PointToClient(Cursor.Position)
            Dim item = cms.GetItemAt(pos)
            If TypeOf item Is ToolStripMenuItem AndAlso item.Tag IsNot Nothing Then
                Dim hovered As ToolStripMenuItem = DirectCast(item, ToolStripMenuItem)
                Dim clipID As Integer
                If Integer.TryParse(hovered.Tag.ToString(), clipID) Then
                    App.ShowClipViewer(clipID, App.CMTray.Bounds, hovered, False)
                End If
            End If
        ElseIf e.KeyCode = App.Settings.HotKeys.ShowScratchPad Then
            Dim cms = DirectCast(sender, ContextMenuStrip)
            Dim pos = cms.PointToClient(Cursor.Position)
            Dim item = cms.GetItemAt(pos)
            If TypeOf item Is ToolStripMenuItem AndAlso item.Tag IsNot Nothing Then
                Dim hovered As ToolStripMenuItem = DirectCast(item, ToolStripMenuItem)
                Dim clipID As Integer
                If Integer.TryParse(hovered.Tag.ToString(), clipID) Then
                    App.ShowScratchPad(clipID)
                End If
            End If
        ElseIf HotKeyMatches(e, App.Settings.HotKeys.DevTools) Then
            App.ShowDevTools()
        End If
    End Sub
    Private Sub OnClearClipboard_MouseDown(sender As Object, e As MouseEventArgs)
        Select Case e.Button
            Case MouseButtons.Left
                Clipboard.Clear()
                App.CBLivePreview = App.BuildLiveClipboardPreview()
                RefreshMenu()
            Case MouseButtons.Right
        End Select
    End Sub
    Private Sub OnScratchPad_MouseDown(sender As Object, e As MouseEventArgs)
        Select Case e.Button
            Case MouseButtons.Left
                App.ShowScratchPad(-1)
            Case MouseButtons.Right
        End Select
    End Sub
    Private Sub OnAppView_MouseDown(sender As Object, e As MouseEventArgs)
        Select Case e.Button
            Case MouseButtons.Left
                Dim screenPos As Point = Cursor.Position
                App.ShowAppView()
            Case MouseButtons.Right
                Dim screenPos As Point = Cursor.Position
                App.ShowAppView()
        End Select
    End Sub
    Private Sub OnClipCMPreviewClick(sender As Object, e As EventArgs)
        repo.RestoreClip(ClipCMCurrentClipId)
        RefreshMenu()
    End Sub
    Private Sub OnClipCMFavorite(sender As Object, e As EventArgs)
        repo.ToggleFavorite(ClipCMCurrentClipId)
        RefreshMenu()
    End Sub
    Private Sub OnClipCMViewClip(sender As Object, e As EventArgs)
        App.HideClipViewer()
        App.ShowClipViewer(ClipCMCurrentClipId, ClipCM.Bounds, DirectCast(ClipCM.Items(2), ToolStripMenuItem), True)
    End Sub
    Private Sub OnCLipCMScratchPad(sender As Object, e As EventArgs)
        App.ShowScratchPad(ClipCMCurrentClipId)
    End Sub
    Private Sub OnCLipCMOpenSourceApp(sender As Object, e As EventArgs)
        Dim item = DirectCast(sender, ToolStripMenuItem)
        Dim exePath = TryCast(item.Tag, String)
        If String.IsNullOrWhiteSpace(exePath) Then Exit Sub
        Try
            Process.Start(exePath)
        Catch
            App.WriteToLog("Unable to Open the Source Application: " & exePath)
        End Try
    End Sub
    Private Sub OnClipCMDelete(sender As Object, e As EventArgs)
        repo.DeleteClip(ClipCMCurrentClipId)
        RefreshMenu()
    End Sub
    Private Sub BlinkTimer_Tick(sender As Object, e As EventArgs)
        blinkState = Not blinkState

        If blinkState Then
            NIClipboard.Icon = My.Resources.IconAppNotify ' your highlight icon
        Else
            NIClipboard.Icon = My.Resources.IconApp
        End If

        blinkCount += 1
        If blinkCount >= MaxBlinks Then
            StopBlink()
        End If
    End Sub

    ' Methods
    Private Sub BuildMenu()
        App.CMTray = MenuBuilder.BuildMenu(repo, commonActions, AddressOf OnClipSelected, AddressOf OnMenuKeyDown)
        NIClipboard.ContextMenuStrip = App.CMTray
        AddHandler NIClipboard.ContextMenuStrip.KeyDown, AddressOf OnMenuKeyDown
        AddHandler NIClipboard.ContextMenuStrip.Closing, AddressOf OnMenuClosing
    End Sub
    Friend Sub RefreshMenu()
        BuildMenu()
    End Sub
    Friend Shared Sub RefreshFavoritesMenu(menu As ContextMenuStrip, repo As ClipRepository, clickHandler As MouseEventHandler, keyHandler As KeyEventHandler)
        Dim favorites = repo.GetFavoriteClips(App.Settings.MaxClips)

        ' Find the anchor separator before Common Actions
        Dim anchorIndex As Integer = menu.Items.IndexOfKey("CommonActionsSeparator")
        If anchorIndex = -1 Then Exit Sub

        ' Find existing Favorites menu (if any)
        Dim favMenu = menu.Items.
        OfType(Of ToolStripMenuItem)().
        FirstOrDefault(Function(i) i.Name = "FavoritesMenu")

        ' If no favorites exist, remove the menu if present
        If favorites.Count = 0 Then
            If favMenu IsNot Nothing Then
                ' ⭐ CLOSE THE DROPDOWN IF IT IS OPEN (this prevents the lingering ghost menu)
                If favMenu.DropDown.Visible Then
                    favMenu.DropDown.Close()
                End If
                ' Remove the separator above it too
                Dim favIndex = menu.Items.IndexOf(favMenu)
                If favIndex > 0 AndAlso TypeOf menu.Items(favIndex - 1) Is ToolStripSeparator Then
                    menu.Items.RemoveAt(favIndex - 1)
                End If
                menu.Items.Remove(favMenu)
            End If
            Exit Sub
        End If

        ' If favorites exist but menu doesn't, create it
        If favMenu Is Nothing Then
            favMenu = MenuBuilder.BuildFavoritesMenu(repo, clickHandler, keyHandler)
            favMenu.Name = "FavoritesMenu"
            ' Insert separator + menu before Common Actions
            menu.Items.Insert(anchorIndex, favMenu)
            menu.Items.Insert(anchorIndex, New ToolStripSeparator())
            Exit Sub
        End If

        ' Otherwise refresh the existing dropdown
        Dim newFavMenu = MenuBuilder.BuildFavoritesMenu(repo, clickHandler, keyHandler)

        favMenu.DropDown.Items.Clear()
        For Each src As ToolStripMenuItem In newFavMenu.DropDown.Items
            Dim clone As New ToolStripMenuItem(src.Text) With {
                .Tag = src.Tag,
                .Checked = src.Checked,
                .Image = src.Image}
            AddHandler clone.MouseDown, clickHandler
            favMenu.DropDown.Items.Add(clone)
        Next
    End Sub
    Friend Shared Sub UpdateFavoriteItemCheckedState(menu As ContextMenuStrip, clipID As Integer, isFavorite As Boolean)
        For Each item As ToolStripItem In menu.Items
            Dim mi = TryCast(item, ToolStripMenuItem)
            If mi IsNot Nothing AndAlso mi.Tag IsNot Nothing Then
                Dim id As Integer
                If Integer.TryParse(mi.Tag.ToString(), id) AndAlso id = clipID Then
                    mi.Checked = isFavorite
                    Exit Sub
                End If
            End If
        Next
    End Sub
    Private Shared Function HotKeyMatches(e As KeyEventArgs, hotkey As Keys) As Boolean
        Dim mods = hotkey And Keys.Modifiers
        Dim key = hotkey And Not Keys.Modifiers
        Return e.Modifiers = mods AndAlso e.KeyCode = key
    End Function
    Private Shared Function GetClipboardSignature() As Byte()
        If Clipboard.ContainsFileDropList() Then
            Dim files = Clipboard.GetFileDropList()
            Dim fileList As String = String.Join("|", files.Cast(Of String)())
            Return Text.Encoding.UTF8.GetBytes(fileList)
        ElseIf Clipboard.ContainsImage() Then
            Dim img = Clipboard.GetImage()
            Using ms As New MemoryStream()
                img.Save(ms, Imaging.ImageFormat.Png)
                Return ms.ToArray()
            End Using
        ElseIf Clipboard.ContainsText() Then
            Dim txt = Clipboard.GetText()
            Return Text.Encoding.UTF8.GetBytes(txt)
        End If
        Return Nothing 'unsupported format
    End Function
    Private Function ShouldIgnoreClip(signatureBytes As Byte(), now As DateTime) As Boolean

        ' Empty clipboard filter
        If signatureBytes Is Nothing OrElse signatureBytes.Length = 0 Then
            'Debug.Print("CLIPFILTER --> Empty clipboard detected")
            'App.WriteToLog("CLIPFILTER --> Empty clipboard detected")

            Dim dt = (now - lastClearTime).TotalMilliseconds

            ' Empty clipboard is a real event unless it happens right after a clear
            If dt < clearSuppressionMs Then
                'Debug.Print("CLIPFILTER --> SUPPRESSED: empty clipboard")
                'App.WriteToLog($"CLIPFILTER --> SUPPRESSED: empty clipboard (dt={dt}ms)")
                Return True   ' suppress noise clears
            End If

            ' Accept empty clipboard as a real event
            lastHash = ""
            lastLength = 0
            lastClipTime = now
            lastClearTime = now

            'Debug.Print("CLIPFILTER --> ACCEPTED: empty clipboard")
            'App.WriteToLog("CLIPFILTER --> ACCEPTED: empty clipboard")
            Return False
        End If

        ' Post-clear suppression
        Dim dtClear = (now - lastClearTime).TotalMilliseconds
        If dtClear < clearSuppressionMs Then
            'Debug.Print("CLIPFILTER --> SUPPRESSED: post-clear window")
            'App.WriteToLog($"CLIPFILTER --> SUPPRESSED: post-clear window (dt={dtClear}ms)")
            Return True
        End If

        ' Hash duplicate suppression
        Dim newHash = ComputeHash(signatureBytes)
        If newHash = lastHash Then
            Dim dtDup = (now - lastClipTime).TotalMilliseconds
            If dtDup < duplicateThresholdMs Then
                'Debug.Print("CLIPFILTER --> SUPPRESSED: duplicate")
                'App.WriteToLog($"CLIPFILTER --> SUPPRESSED: duplicate (dt={dtDup}ms)")
                Return True
            End If
        End If

        ' Accept this clip → update state
        lastHash = newHash
        lastLength = signatureBytes.Length
        lastClipTime = now

        'Debug.Print("CLIPFILTER --> ACCEPTED: clip (len={signatureBytes.Length})")
        'App.WriteToLog($"CLIPFILTER --> ACCEPTED: clip (len={signatureBytes.Length})")
        Return False
    End Function
    Private Shared Function ComputeHash(data As Byte()) As String
        Dim hash = Security.Cryptography.SHA256.HashData(data)
        Return Convert.ToBase64String(hash)
    End Function
    Private Sub StartBlink()
        blinkCount = 0
        blinkState = False
        blinkTimer.Start()
    End Sub
    Private Sub StopBlink()
        blinkTimer.Stop()
        NIClipboard.Icon = My.Resources.IconApp
    End Sub

End Class
