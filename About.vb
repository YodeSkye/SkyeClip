
Imports System.Reflection
Imports Skye.UI

Public Class About

    'Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point

    'Form Events
    Private Sub AboutLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipAbout)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        Text = "About " + App.GetAppTitle
        LblAbout.Text = App.GetAppDescription
        Dim ver = Assembly.GetExecutingAssembly().GetName().Version
        LblVersion.Text = $"v{ver.Major}.{ver.Minor}.{ver.Build}"
        LLblSponsorGitHub.Image = App.ResizeImage(My.Resources.ImageAttributionGitHub, 32)
        LLblSponsorPayPal.Image = App.ResizeImage(My.Resources.ImageAttributionPayPal, 32)
        BtnOK.Select()
    End Sub
    Private Sub About_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, LblAbout.MouseDown, LblVersion.MouseDown
        Dim cSender As Control
        If e.Button = MouseButtons.Left AndAlso WindowState = FormWindowState.Normal Then
            mMove = True
            cSender = CType(sender, Control)
            If cSender Is Me Then
                mOffset = New Point(-e.X - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            Else
                mOffset = New Point(-e.X - cSender.Left - SystemInformation.FixedFrameBorderSize.Width - 7, -e.Y - cSender.Top - SystemInformation.FixedFrameBorderSize.Height - SystemInformation.CaptionHeight - 7)
            End If
        End If
    End Sub
    Private Sub About_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, LblAbout.MouseMove, LblVersion.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
        End If
    End Sub
    Private Sub About_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, LblAbout.MouseUp, LblVersion.MouseUp
        mMove = False
    End Sub
    Private Sub About_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Visible AndAlso WindowState = FormWindowState.Normal AndAlso Not mMove Then
            CheckMove(Location)
        End If
    End Sub
    Private Sub AboutKeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    'Control Events
    Private Sub BtnOKClick(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
    Private Sub BtnChangeLog_Click(sender As Object, e As EventArgs) Handles BtnChangeLog.Click
        App.ShowChangeLog()
    End Sub
    Private Sub LLblMicrosoft_MouseEnter(sender As Object, e As EventArgs) Handles LLblMicrosoft.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblMicrosoft_MouseLeave(sender As Object, e As EventArgs) Handles LLblMicrosoft.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblMicrosoft_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblMicrosoft.MouseClick
        OpenLink(App.AttributionMicrosoft)
    End Sub
    Private Sub LLblSQLite_MouseEnter(sender As Object, e As EventArgs) Handles LLblSQLite.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblSQLite_MouseLeave(sender As Object, e As EventArgs) Handles LLblSQLite.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblSQLite_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblSQLite.MouseClick
        OpenLink(App.AttributionSQLite)
    End Sub
    Private Sub LLblIcons8_MouseEnter(sender As Object, e As EventArgs) Handles LLblIcons8.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblIcons8_MouseLeave(sender As Object, e As EventArgs) Handles LLblIcons8.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblIcons8_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblIcons8.MouseClick
        OpenLink(App.AttributionIcons8)
    End Sub
    Private Sub LLblSponsorGitHub_MouseEnter(sender As Object, e As EventArgs) Handles LLblSponsorGitHub.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblSponsorGitHub_MouseLeave(sender As Object, e As EventArgs) Handles LLblSponsorGitHub.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblSponsorGitHub_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblSponsorGitHub.MouseClick
        OpenLink(App.SponsorGitHub)
    End Sub
    Private Sub LLblSponsorPayPal_MouseEnter(sender As Object, e As EventArgs) Handles LLblSponsorPayPal.MouseEnter
        Cursor = Cursors.Hand
    End Sub
    Private Sub LLblSponsorPayPal_MouseLeave(sender As Object, e As EventArgs) Handles LLblSponsorPayPal.MouseLeave
        ResetCursor()
    End Sub
    Private Sub LLblSponsorPayPal_MouseClick(sender As Object, e As MouseEventArgs) Handles LLblSponsorPayPal.MouseClick
        OpenLink(App.SponsorPayPal)
    End Sub

    ' Handlers
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipAbout)
    End Sub

    'Procedures
    Private Sub OpenLink(target As String)
        Dim pInfo As New Diagnostics.ProcessStartInfo With {
        .UseShellExecute = True,
        .FileName = target}
        Try
            Diagnostics.Process.Start(pInfo)
        Catch ex As Exception
            WriteToLog("Cannot Open " & target & vbCr & ex.Message)
        End Try
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsDialogWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsDialogWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsDialogWindow
        If location.Y < App.AdjustScreenBoundsDialogWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
