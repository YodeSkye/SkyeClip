
Imports Skye.UI

Friend Class AppView

    ' Declarations
    Private fadeInTimer As Timer
    Private fadeOutTimer As Timer
    Private Const FadeStep As Double = 0.08 ' adjust for speed
    Private _suppressHideOnDeactivate As Boolean = False

    ' From Events
    Private Sub AppView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0

        Skye.UI.ThemeManager.RegisterComponent(TipAppView)
        Skye.UI.ThemeManager.ApplyTheme(Me)
        'Skye.UI.ThemeManager.ApplyToTooltip(TipAppView)
        'AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged

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

        Dim bringToTop As Boolean = (e.Button = MouseButtons.Right)

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
            ofd.Filter = "SkyeClip Packages (*.skyeclip;*.zip)|*.skyeclip;*.zip|All Files (*.*)|*.*"
            ofd.Title = If(bringToTop, "Import Clips (Bring to Top)", "Import Clips (Keep Timestamps)")

            If ofd.ShowDialog() = DialogResult.OK Then
                Dim importResult = App.Tray.repo.ImportPackage(ofd.FileName, targetProfileId, bringToTop)

                If importResult.Success Then
                    MessageBox.Show(
                        $"Import complete!{Environment.NewLine}" &
                        $"• Imported: {importResult.ImportedCount}{Environment.NewLine}" &
                        $"• Skipped (Duplicates): {importResult.SkippedDuplicates}",
                        "Import Results",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )
                    ' Signal ClipExplorer to reload grid
                    'App.Events.RaiseClipsImported()
                Else
                    MessageBox.Show($"Import failed: {importResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Using
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

                If e.Button = MouseButtons.Left Then
                    sfd.Title = "Export Current Profile"
                    sfd.FileName = $"SkyeClip_Profile_{App.Settings.GetProfileName(App.Settings.CurrentProfileID)}_{DateTime.Now:yyyyMMdd}.zip"
                Else
                    sfd.Title = "Export All Clips"
                    sfd.FileName = $"SkyeClip_All_{DateTime.Now:yyyyMMdd}.zip"
                End If

                ' Pass 'Me' (the form) as the owner window explicitly
                If sfd.ShowDialog(Nothing) = DialogResult.OK Then
                    If e.Button = MouseButtons.Left Then
                        Tray.repo.ExportProfile(App.Settings.CurrentProfileID, sfd.FileName)
                    Else
                        Tray.repo.ExportAll(sfd.FileName)
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
