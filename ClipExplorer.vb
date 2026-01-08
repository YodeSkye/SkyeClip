Public Class ClipExplorer

    ' Declarations
    Private Enum TextSearchMode
        PlainText
        RichText
        HTMLText
        AllText
    End Enum
    Private _searchText As String = String.Empty
    Private _searchFavoritesOnly As Boolean = False
    Private _searchDays As Integer = 0 ' 0 means all days
    Private _searchMode As TextSearchMode

    ' Form Events
    Private Sub ClipExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = App.GetAppTitle() & " " & Text
        RadBtnPlainText.Checked = True
    End Sub

    ' Control Events
    Private Sub BtnClearSearch_Click(sender As Object, e As EventArgs) Handles BtnClearSearch.Click
        _searchText = String.Empty
        TxtBoxSearch.Text = String.Empty
        _searchFavoritesOnly = False
        ChkBoxFavorites.Checked = False
        _searchDays = 0
        TxtBoxDays.Text = String.Empty
        RadBtnPlainText.Checked = True
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
    End Sub
    Private Sub ChkBoxFavorites_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBoxFavorites.CheckedChanged
        _searchFavoritesOnly = ChkBoxFavorites.Checked
    End Sub
    Private Sub TxtBoxDays_Validated(sender As Object, e As EventArgs) Handles TxtBoxDays.Validated
        TxtBoxDays.SelectAll()
        If Integer.TryParse(TxtBoxDays.Text.Trim, _searchDays) = False Then _searchDays = 0
    End Sub
    Private Sub RadBtnText_CheckedChanged(sender As Object, e As EventArgs) Handles RadBtnPlainText.CheckedChanged, RadBtnRTF.CheckedChanged, RadBtnHTML.CheckedChanged, RadBtnAllText.CheckedChanged
        If RadBtnPlainText.Checked Then
            _searchMode = TextSearchMode.PlainText
        ElseIf RadBtnRTF.Checked Then
            _searchMode = TextSearchMode.RichText
        ElseIf RadBtnHTML.Checked Then
            _searchMode = TextSearchMode.HTMLText
        ElseIf RadBtnAllText.Checked Then
            _searchMode = TextSearchMode.AllText
        End If
        Debug.WriteLine("Search Mode: " & _searchMode.ToString)
    End Sub

End Class
