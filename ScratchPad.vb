
Friend Class ScratchPad

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    ' Form Events
    Private Sub ScratchPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = App.GetAppTitle & " " & Text
        If App.Settings.ScratchPadLocation.Y >= 0 Then Me.Location = App.Settings.ScratchPadLocation
        If App.Settings.ScratchPadSize.Height >= 0 Then Me.Size = App.Settings.ScratchPadSize
    End Sub
    Private Sub ScratchPad_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        App.Settings.Save()
    End Sub
    Private Sub ScratchPad_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        App.FmrScratchPad?.Dispose()
        App.FmrScratchPad = Nothing
    End Sub
    Private Sub ScratchPad_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FrameBorderSize.Width - 4, -e.Y - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FrameBorderSize.Width - 4, -e.Y - cSender.Top - SystemInformation.FrameBorderSize.Height - SystemInformation.CaptionHeight - 4)
            End If
        End If
    End Sub
    Private Sub ScratchPad_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.Settings.ScratchPadLocation = Location
        End If
    End Sub
    Private Sub ScratchPad_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        mMove = False
    End Sub
    Private Sub ScratchPad_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
            App.Settings.ScratchPadLocation = Location
        End If
    End Sub
    Private Sub ScratchPad_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Visible AndAlso WindowState = FormWindowState.Normal Then
            App.Settings.ScratchPadSize = Me.Size
        End If
    End Sub

    ' Methods
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
