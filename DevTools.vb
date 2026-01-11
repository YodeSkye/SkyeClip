
Imports System.Data.SQLite

Public Class DevTools

    ' Declarations
    Private ReadOnly MyConnectionString As String = "Data Source=" & DBPath & ";Version=3;"

    ' Form Events
    Private Sub DevTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
        Text = GetAppTitle() & " " & Text
        LblClips.Text &= " (" & GetClipsCount.ToString & " records)"
        LblClipFormats.Text &= " (" & GetFormatsCount.ToString & " records)"
        LoadClips()
    End Sub

    ' Control Events
    Private Sub DGVClips_SelectionChanged(sender As Object, e As EventArgs) Handles DGVClips.SelectionChanged
        LoadFormatsForSelectedClip()
    End Sub
    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        LoadClips()
    End Sub
    Private Sub BtnSaveClips_Click(sender As Object, e As EventArgs) Handles BtnSaveClips.Click
        Dim dt As DataTable = CType(DGVClips.DataSource, DataTable)

        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Dim da As New SQLiteDataAdapter("SELECT * FROM Clips", conn)
            Dim cb As New SQLiteCommandBuilder(da)

            da.Update(dt)
        End Using

        LoadClips()
        App.Tray.RefreshMenu()
    End Sub
    Private Sub BtnSaveFormats_Click(sender As Object, e As EventArgs) Handles BtnSaveFormats.Click
        Dim dt As DataTable = CType(DGVClipFormats.DataSource, DataTable)

        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Dim da As New SQLiteDataAdapter("SELECT * FROM ClipFormats", conn)
            Dim cb As New SQLiteCommandBuilder(da)

            da.Update(dt)
        End Using

        LoadFormatsForSelectedClip()
    End Sub
    Private Sub BtnViewFormatData_Click(sender As Object, e As EventArgs) Handles BtnViewFormatData.Click
        If DGVClipFormats.SelectedRows.Count = 0 Then Exit Sub

        Dim id As Integer = CInt(DGVClipFormats.SelectedRows(0).Cells("ID").Value)
        Dim formatName As String = CStr(DGVClipFormats.SelectedRows(0).Cells("FormatName").Value)

        Dim bytes = LoadFormatBlobData(id)

        Dim viewer As New DevToolsFormatViewer()
        viewer.SetData(formatName, bytes)
        viewer.Show(Me)
    End Sub
    Private Sub BtnDeleteClip_Click(sender As Object, e As EventArgs) Handles BtnDeleteClip.Click
        If DGVClips.SelectedRows.Count = 0 Then Exit Sub

        Dim id As Integer = CInt(DGVClips.SelectedRows(0).Cells("ID").Value)

        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            ' delete formats first
            Dim cmd1 As New SQLiteCommand("DELETE FROM ClipFormats WHERE EntryID=@id", conn)
            cmd1.Parameters.AddWithValue("@id", id)
            cmd1.ExecuteNonQuery()

            ' delete clip
            Dim cmd2 As New SQLiteCommand("DELETE FROM Clips WHERE ID=@id", conn)
            cmd2.Parameters.AddWithValue("@id", id)
            cmd2.ExecuteNonQuery()
        End Using

        LoadClips()
        App.Tray.RefreshMenu()
    End Sub
    Private Sub BtnDeleteFormat_Click(sender As Object, e As EventArgs) Handles BtnDeleteFormat.Click
        If DGVClipFormats.SelectedRows.Count = 0 Then Exit Sub

        Dim id As Integer = CInt(DGVClipFormats.SelectedRows(0).Cells("ID").Value)

        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Dim cmd As New SQLiteCommand("DELETE FROM ClipFormats WHERE ID=@id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
        End Using

        LoadFormatsForSelectedClip()
    End Sub

    ' Methods
    Private Sub LoadClips()
        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Dim da As New SQLiteDataAdapter("SELECT * FROM Clips ORDER BY ID DESC", conn)
            Dim dt As New DataTable()
            da.Fill(dt)

            DGVClips.DataSource = dt
        End Using
    End Sub
    Private Sub LoadFormatsForSelectedClip()
        If DGVClips.SelectedRows.Count = 0 Then
            DGVClipFormats.DataSource = Nothing
            Return
        End If

        Dim clipId As Integer = CInt(DGVClips.SelectedRows(0).Cells("ID").Value)

        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Dim da As New SQLiteDataAdapter(
                "SELECT ID, EntryID, FormatID, FormatName FROM ClipFormats WHERE EntryID = @id ORDER BY ID",
                conn
            )
            da.SelectCommand.Parameters.AddWithValue("@id", clipId)

            Dim dt As New DataTable()
            da.Fill(dt)

            DGVClipFormats.DataSource = dt
            If DGVClipFormats.Columns.Contains("Data") Then
                DGVClipFormats.Columns("Data").Visible = False
            End If
        End Using
    End Sub
    Private Shared Function GetClipsCount() As Integer
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM Clips;", conn)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function
    Private Shared Function GetFormatsCount() As Integer
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM ClipFormats;", conn)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function
    Private Function LoadFormatBlobData(formatId As Integer) As Byte()
        Using conn As New SQLiteConnection(MyConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("SELECT Data FROM ClipFormats WHERE ID=@id", conn)
                cmd.Parameters.AddWithValue("@id", formatId)

                Dim result = cmd.ExecuteScalar()
                If result Is DBNull.Value OrElse result Is Nothing Then
                    Return Nothing
                End If

                Return CType(result, Byte())
            End Using
        End Using
    End Function

End Class
