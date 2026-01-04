
Imports System.Data.SQLite
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Skye.UI

Friend Class ScratchPad

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private _clipID As Integer = -1

    ' Form Events
    Private Sub ScratchPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = App.GetAppTitle & " " & Text
        If App.Settings.ScratchPadLocation.Y >= 0 Then Me.Location = App.Settings.ScratchPadLocation
        If App.Settings.ScratchPadSize.Height >= 0 Then Me.Size = App.Settings.ScratchPadSize
        ChkBoxKeepText.Checked = App.Settings.ScratchPadKeepText
        If Not String.IsNullOrWhiteSpace(App.ScratchPadText) Then
            RTB.Rtf = App.ScratchPadText
        End If
    End Sub
    Friend Overloads Sub Show(ClipID As Integer)
        _clipID = ClipID
        MyBase.Show()
        BringToFront()

        Dim dt = LoadFormatsForClip(ClipID)
        Dim best = PickBestFormat(dt)
        If best IsNot Nothing Then ShowClipData(best)

    End Sub
    Private Sub ScratchPad_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If App.Settings.ScratchPadKeepText Then
            App.ScratchPadText = RTB.Rtf
            IO.File.WriteAllText(App.ScratchPadPath, RTB.Rtf)
        Else
            App.ScratchPadText = String.Empty
            If IO.File.Exists(App.ScratchPadPath) Then
                IO.File.Delete(App.ScratchPadPath)
            End If
        End If
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

    ' Control Events
    Private Sub ChkBoxKeepText_Click(sender As Object, e As EventArgs) Handles ChkBoxKeepText.Click
        App.Settings.ScratchPadKeepText = ChkBoxKeepText.Checked
    End Sub
    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click

    End Sub

    ' Methods
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
            "Rich Text Format",
            "UnicodeText",
            "Text",
            "HTML Format"
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
            Case "HTML Format"
                ShowHtml(bytes)
            Case "Rich Text Format"
                ShowRtf(bytes)
            Case "UnicodeText"
                ShowUnicode(bytes)
            Case "Text"
                ShowAnsi(bytes)
            Case Else
                ShowRaw(bytes, format)
        End Select

    End Sub
    Private Sub ShowRtf(bytes As Byte())
        Dim rtf As String = System.Text.Encoding.UTF8.GetString(bytes)
        RTB.Select(RTB.TextLength, 0)
        RTB.SelectedRtf = rtf
    End Sub
    Private Sub ShowUnicode(bytes As Byte())
        Dim txt As String = System.Text.Encoding.Unicode.GetString(bytes)
        RTB.Text &= txt
    End Sub
    Private Sub ShowAnsi(bytes As Byte())
        Dim txt As String = System.Text.Encoding.Default.GetString(bytes)
        RTB.Text = txt
    End Sub
    Private Sub ShowHtml(bytes As Byte())
        Dim raw = Encoding.UTF8.GetString(bytes)
        Dim plain = Regex.Replace(raw, "<.*?>", "")
        RTB.Text &= plain
    End Sub
    Private Sub ShowRaw(bytes As Byte(), format As String)
        RTB.Text &= $"[{format}] {bytes.Length} bytes"
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
