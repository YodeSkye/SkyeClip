
Friend Class AppView

    ' Declarations
    Private fadeInTimer As Timer
    Private fadeOutTimer As Timer
    Private Const FadeStep As Double = 0.08 ' adjust for speed

    ' From Events
    Private Sub AppView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opacity = 0

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
    Private Sub BtnLog_Click(sender As Object, e As EventArgs) Handles BtnLog.Click
        App.ShowLog()
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
