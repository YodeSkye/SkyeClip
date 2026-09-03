
Imports Skye.UI

Friend Class AppView

    ' Declarations
    Private Const FadeStep As Double = 0.08 ' adjust for speed
    Private fadeInTimer As Timer
    Private fadeOutTimer As Timer
    Private _suppressHideOnDeactivate As Boolean = False

    ' From Events
    Private Sub AppView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0

        Skye.UI.ThemeManager.RegisterComponent(TipAppView)
        Skye.UI.ThemeManager.ApplyTheme(Me)
        If App.Settings.UseProfiles Then
            TipAppView.SetText(BtnImport, "Import Clips Into Current Profile")
            TipAppView.SetText(BtnExport, "Export This Profile's Clips" & Environment.NewLine & "Right-Click To Export All Clips")
        Else
            TipAppView.SetText(BtnImport, "Import Clips Into Root Profile")
            TipAppView.SetText(BtnExport, "Export All Clips")
        End If
        TipAppView.SetText(BtnImport, TipAppView.GetText(BtnImport) & Environment.NewLine & "Right-Click To Import To Top Of List")

        'Ensure the form is fully on-screen
        Dim wa = Screen.FromPoint(Location).WorkingArea
        Dim x = Left
        Dim y = Top
        If x < wa.Left Then x = wa.Left
        If x + Width > wa.Right Then x = wa.Right - Width
        If y < wa.Top Then y = wa.Top
        If y + Height > wa.Bottom Then y = wa.Bottom - Height
        Location = New Point(x, y)

        fadeInTimer = New Timer With {.Interval = 15}
        AddHandler fadeInTimer.Tick, AddressOf FadeIn_Tick
        fadeOutTimer = New Timer With {.Interval = 15}
        AddHandler fadeOutTimer.Tick, AddressOf FadeOut_Tick
    End Sub
    Friend Sub ShowAtScreenPoint(screenPos As Point)
        Dim working = Screen.FromPoint(screenPos).WorkingArea
        Dim x As Integer = screenPos.X
        Dim y As Integer = screenPos.Y - Me.Height
        ' Clamp horizontally
        If x + Me.Width > working.Right Then x = working.Right - Me.Width
        If x < working.Left Then x = working.Left
        ' Clamp vertically
        If y < working.Top Then y = working.Top
        Location = New Point(x, y)
        Show()
        fadeOutTimer?.Stop()
        fadeInTimer.Start()
    End Sub
    Private Sub AppView_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ' If a modal dialog or popup is active, DO NOT hide the form!
        If _suppressHideOnDeactivate Then Return

        If Not IsDisposed Then
            fadeInTimer?.Stop()
            fadeOutTimer.Start()
        End If

    End Sub
    Private Sub AppView_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.D Then
            App.ShowDevTools()
        End If
    End Sub

    ' Control Events
    Private Sub BtnSettings_Click(sender As Object, e As EventArgs) Handles BtnSettings.Click
        App.ShowSettings()
    End Sub
    Private Sub BtnImport_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnImport.MouseDown
        If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then Return
        _suppressHideOnDeactivate = True
        Dim bringToTop As Boolean = (e.Button = MouseButtons.Right)

        Try
            ' Resolve Target Profile
            Dim targetProfileId As Integer = 0
            If App.Settings.UseProfiles Then
                targetProfileId = App.Settings.CurrentProfileID
                Dim profileName As String = App.Settings.GetProfileName(targetProfileId)
                Dim dialogResult = MessageBox.Show(
                    $"Import clips into profile '{profileName}'?",
                    "Confirm Import Target",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                )
                If dialogResult <> DialogResult.OK Then Return
            End If
            ' Select File
            Using ofd As New OpenFileDialog()
                ofd.Filter = "ZIP Archives (*.zip)|*.zip|All Files (*.*)|*.*"
                ofd.Title = If(bringToTop, "Import Clips (Bring to Top)", "Import Clips (Keep Timestamps)")
                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    Dim importResult = App.Tray.repo.ImportPackage(ofd.FileName, targetProfileId, bringToTop)
                    If importResult.Success Then
                        App.Tray.RefreshMenu()
                        ' Signal ClipExplorer to reload grid
                        'App.Events.RaiseClipsImported()
                        App.Tray.ShowToast($"Import complete!{Environment.NewLine}" & $"• Imported: {importResult.ImportedCount}{Environment.NewLine}" & $"• Skipped (Duplicates): {importResult.SkippedDuplicates}")
                    Else
                        App.Tray.ShowToast($"Import failed: {importResult.ErrorMessage}")
                    End If
                End If
            End Using
        Finally
            ' 2. Unfreeze Deactivate handler after dialog closes
            _suppressHideOnDeactivate = False
        Me.Hide()
        End Try
    End Sub
    Private Sub BtnExport_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnExport.MouseDown
        If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then Return
        ' 1. Freeze the Deactivate handler so AppView stays visible
        _suppressHideOnDeactivate = True

        Try
            Using sfd As New SaveFileDialog()
                sfd.Filter = "ZIP Archives (*.zip)|*.zip|All Files (*.*)|*.*"
                sfd.DefaultExt = "zip"
                sfd.AddExtension = True
                If Not App.Settings.UseProfiles Then
                    sfd.Title = "Export All Clips"
                    sfd.FileName = $"SkyeClip_Export_All_{DateTime.Now:yyyyMMdd_HHmmss}.zip"
                Else
                    If e.Button = MouseButtons.Left Then
                        sfd.Title = "Export Current Profile Clips"
                        sfd.FileName = $"SkyeClip_Export_Profile_{App.Settings.CurrentProfileID}_{DateTime.Now:yyyyMMdd_HHmmss}.zip"
                    Else
                        sfd.Title = "Export ALL Clips (All Profiles)"
                        sfd.FileName = $"SkyeClip_Export_All_{DateTime.Now:yyyyMMdd_HHmmss}.zip"
                    End If
                End If
                If sfd.ShowDialog(Me) = DialogResult.OK Then
                    If Not App.Settings.UseProfiles Then
                        Tray.repo.ExportAll(sfd.FileName)
                    Else
                        If e.Button = MouseButtons.Left Then
                            Tray.repo.ExportProfile(App.Settings.CurrentProfileID, sfd.FileName)
                        Else
                            Tray.repo.ExportAll(sfd.FileName)
                        End If
                    End If
                    App.Tray.ShowToast("Export completed successfully!")
                End If
            End Using
        Finally
            ' 2. Unfreeze Deactivate handler after dialog closes
            _suppressHideOnDeactivate = False
            Me.Hide()
        End Try

    End Sub
    Private Sub BtnLog_Click(sender As Object, e As EventArgs) Handles BtnLog.Click
        ShowLog()
    End Sub
    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        App.ShowHelp()
    End Sub
    Private Sub BtnAbout_Click(sender As Object, e As EventArgs) Handles BtnAbout.Click
        App.ShowAbout()
    End Sub
    Private Sub BtnExit_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnExit.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                ExitApp()
            Case MouseButtons.Right
                RestartApp()
        End Select
    End Sub

    ' Handlers
    Private Sub FadeIn_Tick(sender As Object, e As EventArgs)
        If Opacity < 1 Then
            Opacity += FadeStep
        Else
            fadeInTimer.Stop()
        End If
    End Sub
    Private Sub FadeOut_Tick(sender As Object, e As EventArgs)
        If Opacity > 0 Then
            Opacity -= FadeStep
        Else
            fadeOutTimer.Stop()
            Close()
        End If
    End Sub

End Class
