
Imports System.Data.SQLite
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Skye.UI

Friend Class ScratchPad

    ' Declarations
    Private mMove As Boolean = False
    Private mOffset, mPosition As Point
    Private TitleText, OKText As String
    Private _clipID As Integer = -1

    ' Form Events
    Private Sub ScratchPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Skye.UI.ThemeManager.ApplyToTooltip(TipScratchPad)
        AddHandler ThemeManager.ThemeChanged, AddressOf OnThemeChanged
        TitleText = Text
        OKText = TipScratchPad.GetText(BtnOK)
        UpdateUI()
        If App.Settings.ScratchPadSize.Height >= 0 Then Me.Size = App.Settings.ScratchPadSize
        If App.Settings.ScratchPadLocation.Y >= 0 Then Me.Location = App.Settings.ScratchPadLocation
        CMRTB.Font = App.MenuFont
        If Not String.IsNullOrWhiteSpace(App.ScratchPadText) Then
            RTB.Rtf = App.ScratchPadText
        End If
        AddHandler App.Tray.ProfileChanged, AddressOf OnProfileChanged
    End Sub
    Friend Overloads Sub Show(clipID As Integer)
        _clipID = clipID
        MyBase.Show()
        BringToFront()

        If clipID >= 0 Then
            Dim dt = LoadFormatsForClip(clipID)
            Dim best = PickBestFormat(dt)
            If best IsNot Nothing Then ShowClipData(best)
        End If

    End Sub
    Private Sub ScratchPad_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveScratchPad()
        App.Settings.Save()
    End Sub
    Private Sub ScratchPad_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        App.FrmScratchPad?.Dispose()
        App.FrmScratchPad = Nothing
    End Sub
    Private Sub ScratchPad_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PanelBottom.MouseDown
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
    Private Sub ScratchPad_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PanelBottom.MouseMove
        If mMove Then
            mPosition = MousePosition
            mPosition.Offset(mOffset.X, mOffset.Y)
            CheckMove(mPosition)
            Location = mPosition
            App.Settings.ScratchPadLocation = Location
        End If
    End Sub
    Private Sub ScratchPad_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PanelBottom.MouseUp
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
    Private Sub ScratchPad_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, RTB.KeyDown
        Select Case e.KeyCode
            Case Keys.Z 'Undo & Redo
                If e.Control Then
                    If e.Shift Then
                        Redo()
                    Else
                        Undo()
                    End If
                    e.SuppressKeyPress = True
                End If
            Case Keys.X 'Cut
                If e.Control Then
                    If e.Shift Then
                        CutPlain()
                    Else
                        Cut()
                    End If
                    e.SuppressKeyPress = True
                End If
            Case Keys.C 'Copy
                If e.Control Then
                    If e.Shift Then
                        CopyPlain()
                    Else
                        Copy()
                    End If
                    e.SuppressKeyPress = True
                End If
            Case Keys.V 'Paste
                If e.Control Then
                    If e.Shift Then
                        PastePlain()
                    Else
                        Paste()
                    End If
                    e.SuppressKeyPress = True
                End If
            Case Keys.D 'Delete
                If e.Control Then
                    Delete()
                    e.SuppressKeyPress = True
                End If
            Case Keys.A 'Select All
                If e.Control Then
                    SelectAll()
                    e.SuppressKeyPress = True
                End If
        End Select
    End Sub

    ' Control Events
    Private Sub RTB_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles RTB.PreviewKeyDown

        ' === CTRL ONLY ===
        If e.Control AndAlso Not e.Shift Then
            Select Case e.KeyCode
                Case Keys.Z, Keys.X, Keys.C, Keys.V, Keys.D, Keys.A
                    e.IsInputKey = True
            End Select
        End If

        ' === CTRL + SHIFT ===
        If e.Control AndAlso e.Shift Then
            Select Case e.KeyCode
                Case Keys.Z, Keys.X, Keys.C, Keys.V, Keys.D, Keys.A
                    e.IsInputKey = True
            End Select
        End If

    End Sub
    Private Sub ChkBoxKeepText_Click(sender As Object, e As EventArgs) Handles ChkBoxKeepText.Click
        App.Settings.ScratchPadKeepText = ChkBoxKeepText.Checked
        SetOk()
        App.FrmSettings?.SetKeepText()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Close()
    End Sub
    Private Sub BtnExport_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnExport.MouseDown
        Select Case e.Button
            Case MouseButtons.Left
                Export()
            Case MouseButtons.Right
                Import()
        End Select
    End Sub
    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        App.HideHelp()
        App.ShowHelp(1460)
    End Sub
    Private Sub CMRTB_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CMRTB.Opening
        If RTB.CanUndo OrElse RTB.CanRedo Then
            CMIUndo.Enabled = True
        Else
            CMIUndo.Enabled = False
        End If
        CMICut.Enabled = RTB.SelectedText.Length > 0
        CMICopy.Enabled = RTB.SelectedText.Length > 0
        CMIDelete.Enabled = RTB.SelectedText.Length > 0
        CMIPaste.Enabled = Clipboard.ContainsText()
        If RTB.SelectionLength = 0 Then
            MICase.Enabled = False
        Else
            MICase.Enabled = True
        End If
        If RTB.TextLength = 0 OrElse RTB.SelectionLength = RTB.TextLength Then
            CMISelectAll.Enabled = False
        Else
            CMISelectAll.Enabled = True
        End If
    End Sub
    Private Sub CMIUndo_Click(sender As Object, e As EventArgs) Handles CMIUndo.Click
        Dim shift As Boolean = (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        If shift Then
            Redo()
        Else
            Undo()
        End If
    End Sub
    Private Sub CMICut_Click(sender As Object, e As EventArgs) Handles CMICut.Click
        Dim shift As Boolean = (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        If shift Then
            CutPlain()
        Else
            Cut()
        End If
    End Sub
    Private Sub CMICopy_Click(sender As Object, e As EventArgs) Handles CMICopy.Click
        Dim shift As Boolean = (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        If shift Then
            CopyPlain()
        Else
            Copy()
        End If
    End Sub
    Private Sub CMIPaste_Click(sender As Object, e As EventArgs) Handles CMIPaste.Click
        Dim shift As Boolean = (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        If shift Then
            PastePlain()
        Else
            Paste()
        End If
    End Sub
    Private Sub CMIDelete_Click(sender As Object, e As EventArgs) Handles CMIDelete.Click
        Delete()
    End Sub
    Private Sub CMIToUpperCase_Click(sender As Object, e As EventArgs) Handles CMIToUpperCase.Click
        ApplyUpperCase()
    End Sub
    Private Sub CMIToLowerCase_Click(sender As Object, e As EventArgs) Handles CMIToLowerCase.Click
        ApplyLowerCase()
    End Sub
    Private Sub CMIToProperCase_Click(sender As Object, e As EventArgs) Handles CMIToProperCase.Click
        ApplyProperCase()
    End Sub
    Private Sub CMIToSentenceCase_Click(sender As Object, e As EventArgs) Handles CMIToSentenceCase.Click
        ApplySentenceCase()
    End Sub
    Private Sub CMISelectAll_Click(sender As Object, e As EventArgs) Handles CMISelectAll.Click
        SelectAll()
    End Sub

    ' Handlers
    Private Sub OnProfileChanged()
        UpdateUI()
    End Sub
    Private Sub OnThemeChanged()
        Skye.UI.ThemeManager.ApplyToTooltip(TipScratchPad)
    End Sub

    ' Methods
    Friend Sub SaveScratchPad()
        Dim path = App.GetScratchPadProfiledPath

        If App.Settings.ScratchPadKeepText Then
            App.ScratchPadText = RTB.Rtf
            IO.File.WriteAllText(path, RTB.Rtf)
        Else
            App.ScratchPadText = String.Empty
            If IO.File.Exists(path) Then
                IO.File.Delete(path)
            End If
        End If

    End Sub
    Private Sub Export()
        Dim uiSaveFile As New SaveFileDialog With {
            .Title = "Export Scratch Pad Content",
            .Filter = "Rich Text Format (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|All Files (*.*)|*.*",
            .DefaultExt = "rtf",
            .AddExtension = True,
            .OverwritePrompt = True,
            .SupportMultiDottedExtensions = True,
            .InitialDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)}
        If uiSaveFile.ShowDialog() = DialogResult.OK Then
            Dim ext = Path.GetExtension(uiSaveFile.FileName).ToLowerInvariant()
            Select Case ext
                Case ".txt"
                    RTB.SaveFile(uiSaveFile.FileName, RichTextBoxStreamType.PlainText)
                Case Else
                    RTB.SaveFile(uiSaveFile.FileName, RichTextBoxStreamType.RichText)
            End Select
        End If
    End Sub
    Private Sub Import()
        Dim uiOpenFile As New OpenFileDialog With {
            .Title = "Import Content into Scratch Pad (Clears Current Text)",
            .Filter = "Rich Text Format (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|All Files (*.*)|*.*",
            .DefaultExt = "rtf",
            .AddExtension = True,
            .SupportMultiDottedExtensions = True,
            .InitialDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)}
        If uiOpenFile.ShowDialog() = DialogResult.OK Then
            Dim ext = Path.GetExtension(uiOpenFile.FileName).ToLowerInvariant()
            Select Case ext
                Case ".txt"
                    RTB.LoadFile(uiOpenFile.FileName, RichTextBoxStreamType.PlainText)
                Case Else
                    Try
                        RTB.LoadFile(uiOpenFile.FileName, RichTextBoxStreamType.RichText)
                    Catch ex As Exception
                        WriteToLog("Error Importing File into Scratch Pad: " & ex.Message)
                    End Try
            End Select
        End If
    End Sub
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
            "FileDrop",
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
            Case "FileDrop"
                ShowFileDropAsText(bytes)
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
    Private Sub ShowFileDropAsText(bytes As Byte())

        Dim entries = App.ParseFileDrop(bytes)

        Dim sb As New StringBuilder()
        For Each e In entries
            sb.AppendLine(e.FullPath)
        Next

        ' Show in the Scratch Pad text box
        RTB.Text &= sb.ToString()

    End Sub
    Private Sub ShowRaw(bytes As Byte(), format As String)
        RTB.Text &= $"[{format}] {bytes.Length} bytes"
    End Sub
    Private Sub Undo()
        If RTB.CanUndo Then RTB.Undo()
    End Sub
    Private Sub Redo()
        If RTB.CanRedo Then RTB.Redo()
    End Sub
    Private Sub Cut()
        Copy()
        RTB.SelectedText = String.Empty
    End Sub
    Private Sub CutPlain()
        CopyPlain()
        RTB.SelectedText = String.Empty
    End Sub
    Private Sub Copy()
        If RTB.SelectionLength = 0 Then Return

        Dim data As New DataObject()
        data.SetData(DataFormats.Rtf, RTB.SelectedRtf)
        data.SetData(DataFormats.UnicodeText, RTB.SelectedText)
        Clipboard.SetDataObject(data, True)

    End Sub
    Private Sub CopyPlain()
        If RTB.SelectionLength = 0 Then Return

        ' Clipboard.SetText(RTB.SelectedText, TextDataFormat.UnicodeText)
        Dim data As New DataObject()
        data.SetData(DataFormats.UnicodeText, RTB.SelectedText)
        Clipboard.SetDataObject(data, True)

    End Sub
    Private Sub Paste()
        Dim rtf As String = Nothing

        ' Try to get RTF as a string
        If Clipboard.TryGetData(Of String)(rtf, DataFormats.Rtf) Then
            RTB.SelectedRtf = rtf
            Return
        End If

        ' Old API fallback (required for Office, browsers, etc.)
#Disable Warning WFDEV005
        Dim obj = Clipboard.GetData(DataFormats.Rtf)
#Enable Warning WFDEV005
        If TypeOf obj Is String Then
            RTB.SelectedRtf = DirectCast(obj, String)
            Return
        End If
        If TypeOf obj Is MemoryStream Then
            Using ms = DirectCast(obj, MemoryStream)
                RTB.LoadFile(ms, RichTextBoxStreamType.RichText)
            End Using
            Return
        End If

        ' Fallback: plain text
        If Clipboard.ContainsText() Then
            RTB.SelectedText = Clipboard.GetText(TextDataFormat.UnicodeText)
        End If

    End Sub
    Private Sub PastePlain()
        If Clipboard.ContainsText() Then
            RTB.SelectedText = Clipboard.GetText(TextDataFormat.UnicodeText)
        End If
    End Sub
    Private Sub Delete()
        RTB.SelectedText = String.Empty
    End Sub
    Private Sub ApplyUpperCase()
        RTB.SelectedText = RTB.SelectedText.ToUpperInvariant()
    End Sub
    Private Sub ApplyLowerCase()
        RTB.SelectedText = RTB.SelectedText.ToLowerInvariant()
    End Sub
    Private Sub ApplyProperCase()
        RTB.SelectedText = StrConv(RTB.SelectedText, VbStrConv.ProperCase)
    End Sub
    Private Sub ApplySentenceCase()
        Skye.Common.ToSentenceCasePreserveFormatting(RTB)
    End Sub
    Private Sub SelectAll()
        RTB.SelectAll()
    End Sub
    Friend Sub UpdateUI()
        Text = App.GetAppTitle & " " & TitleText & If(App.Settings.UseProfiles, " (" & App.Settings.Profiles.FirstOrDefault(Function(p) p.ID = App.Settings.CurrentProfileID)?.Name & " Profile)", String.Empty)
        ChkBoxKeepText.Checked = App.Settings.ScratchPadKeepText
        SetOk()
    End Sub
    Private Sub SetOk()
        Select Case App.Settings.ScratchPadKeepText
            Case True
                TipScratchPad.SetText(BtnOK, OKText & " (Saving Text)")
            Case False
                TipScratchPad.SetText(BtnOK, OKText & " (Without Saving Text)")
        End Select
    End Sub
    Private Sub CheckMove(ByRef location As Point)
        If location.X + Me.Width > Screen.PrimaryScreen.WorkingArea.Right Then location.X = Screen.PrimaryScreen.WorkingArea.Right - Me.Width + App.AdjustScreenBoundsNormalWindow
        If location.Y + Me.Height > Screen.PrimaryScreen.WorkingArea.Bottom Then location.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Me.Height + App.AdjustScreenBoundsNormalWindow
        If location.X < Screen.PrimaryScreen.WorkingArea.Left Then location.X = Screen.PrimaryScreen.WorkingArea.Left - App.AdjustScreenBoundsNormalWindow
        If location.Y < App.AdjustScreenBoundsNormalWindow Then location.Y = Screen.PrimaryScreen.WorkingArea.Top
    End Sub

End Class
