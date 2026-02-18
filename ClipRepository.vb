
Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Friend Class ClipRepository

    ' Declarations
    Friend Class BasicClipInfo
        Public Property Id As Integer
        Public Property Preview As String
        Public Property LastUsedAt As DateTime
        Public Property SourceAppIcon As Byte()
        Public Property IsFavorite As Boolean
    End Class
    Friend Class FullClipInfo
        Public Property Id As Integer
        Public Property ProfileID As Integer
        Public Property Preview As String
        Public Property SourceAppName As String
        Public Property SourceAppPath As String
        Public Property SourceAppIcon As Byte()
        Public Property IsFavorite As Boolean
        Public Property LastUsedAt As DateTime
    End Class
    Friend Class ExplorerClipInfo
        Public Property Id As Integer
        Public Property ProfileID As Integer
        Public Property Preview As String
        Public Property CreatedAt As DateTime
        Public Property LastUsedAt As DateTime
        Public Property SourceAppName As String
        Public Property SourceAppPath As String
        Public Property SourceAppIcon As Byte()
        Public Property IsFavorite As Boolean
    End Class
    Friend Class ClipData
        Public Property FormatId As UInteger
        Public Property FormatName As String
        Public Property DataBytes As Byte()
    End Class

    ' Constructor
    Friend Sub New()
        EnsureSchema()
    End Sub
    Private Shared Sub EnsureSchema()
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Clips
            Dim createClipsCmd As New SQLiteCommand("
            CREATE TABLE IF NOT EXISTS Clips (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProfileID INTEGER NOT NULL DEFAULT 0,
                Preview TEXT,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                LastUsedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                AggregateHash TEXT NOT NULL,
                HashVersion INTEGER NOT NULL DEFAULT 1,
                SourceAppName TEXT,
                SourceAppPath TEXT,
                SourceAppIcon BLOB,
                IsFavorite INTEGER DEFAULT 0
            )", conn)
            createClipsCmd.ExecuteNonQuery()

            ' ClipFormats
            Dim createFormatsCmd As New SQLiteCommand("
            CREATE TABLE IF NOT EXISTS ClipFormats (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EntryId INTEGER NOT NULL,
                FormatId INTEGER NOT NULL,
                FormatName TEXT,
                Data BLOB NOT NULL
            )", conn)
            createFormatsCmd.ExecuteNonQuery()

            ' Meta table
            Dim createMetaCmd As New SQLiteCommand("
            CREATE TABLE IF NOT EXISTS Meta (
                [Key] TEXT PRIMARY KEY,
                [Value] TEXT NOT NULL
            )", conn)
            createMetaCmd.ExecuteNonQuery()

            ' Run migrations (adds HashVersion if old DB, etc.)
            RunMigrations(conn)

            ' Indexes
            Dim idxCreatedCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_clips_created ON Clips(CreatedAt)", conn)
            idxCreatedCmd.ExecuteNonQuery()

            Dim idxLastUsedCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_clips_lastused ON Clips(LastUsedAt)", conn)
            idxLastUsedCmd.ExecuteNonQuery()

            ' Drop old unique index if it exists (release DBs only)
            Dim dropOldIdx As New SQLiteCommand("DROP INDEX IF EXISTS idx_clips_hash", conn)
            dropOldIdx.ExecuteNonQuery()

            ' Composite unique index on (AggregateHash, HashVersion)
            Dim idxHashCmd As New SQLiteCommand("CREATE UNIQUE INDEX IF NOT EXISTS idx_clips_hash ON Clips(AggregateHash, HashVersion, ProfileID)", conn)
            idxHashCmd.ExecuteNonQuery()

            Dim indexCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_ClipFormats_EntryId ON ClipFormats(EntryId)", conn)
            indexCmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Database Functions
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub SaveClip()

        ' 1) Capture formats
        Dim formats = CaptureTextFormats()
        formats.AddRange(CaptureFileDrop())
        formats.AddRange(CaptureImageFormats())
        If formats Is Nothing OrElse formats.Count = 0 Then Exit Sub

        ' 2) Build preview
        Dim preview As String = BuildPreviewFromFormats(formats)

        ' 3) Hash
        Dim formatsForHash = FilterFormatsForHash(formats)
        Dim aggHash As String = ComputeAggregateHash(formatsForHash)
        Dim nowVal As DateTime = DateTime.UtcNow
        Dim hashVersion As Integer = App.Hash.CurrentHashVersion

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Check for existing clip in SAME hash version
            Dim checkCmd As New SQLiteCommand("
                SELECT Id FROM Clips
                WHERE AggregateHash=@hash
                  AND HashVersion=@hv
                  AND ProfileID=@pid", conn)
            checkCmd.Parameters.AddWithValue("@hash", aggHash)
            checkCmd.Parameters.AddWithValue("@hv", hashVersion)
            checkCmd.Parameters.AddWithValue("@pid", App.Settings.CurrentProfileID)
            Dim existingIdObj = checkCmd.ExecuteScalar()

            Dim entryId As Integer

            If existingIdObj IsNot Nothing AndAlso Not DBNull.Value.Equals(existingIdObj) Then
                ' Duplicate → promote
                entryId = Convert.ToInt32(existingIdObj)
                Dim updCmd As New SQLiteCommand("
                UPDATE Clips SET LastUsedAt=@now WHERE Id=@id", conn)
                updCmd.Parameters.AddWithValue("@now", nowVal)
                updCmd.Parameters.AddWithValue("@id", entryId)
                updCmd.ExecuteNonQuery()
            Else
                ' New clip
                Dim sourceInfo = GetSourceAppInfo()
                Dim insertEntry As New SQLiteCommand("
                INSERT INTO Clips
                    (ProfileID, Preview, CreatedAt, LastUsedAt, AggregateHash, HashVersion,
                     SourceAppName, SourceAppPath, SourceAppIcon)
                VALUES
                    (@pid, @p, @c, @l, @hash, @hv, @app, @apppath, @icon);
                SELECT last_insert_rowid();", conn)

                insertEntry.Parameters.AddWithValue("@pid", App.Settings.CurrentProfileID)
                insertEntry.Parameters.AddWithValue("@p", preview)
                insertEntry.Parameters.AddWithValue("@c", nowVal)
                insertEntry.Parameters.AddWithValue("@l", nowVal)
                insertEntry.Parameters.AddWithValue("@hash", aggHash)
                insertEntry.Parameters.AddWithValue("@hv", hashVersion)
                insertEntry.Parameters.AddWithValue("@app", sourceInfo.AppName)
                insertEntry.Parameters.AddWithValue("@apppath", sourceInfo.ExePath)
                insertEntry.Parameters.Add("@icon", DbType.Binary).Value =
                If(sourceInfo.IconBytes IsNot Nothing, CType(sourceInfo.IconBytes, Object), DBNull.Value)

                entryId = Convert.ToInt32(insertEntry.ExecuteScalar())
                App.WriteToLog("New Clip Saved: ID=" & entryId & " """ & preview & """")

                ' Formats
                For Each cd In formats
                    Dim insertFmt As New SQLiteCommand("
                    INSERT INTO ClipFormats (EntryId, FormatId, FormatName, Data)
                    VALUES (@e, @fid, @fname, @data)", conn)
                    insertFmt.Parameters.AddWithValue("@e", entryId)
                    insertFmt.Parameters.AddWithValue("@fid", cd.FormatId)
                    insertFmt.Parameters.AddWithValue("@fname", If(cd.FormatName, ""))
                    insertFmt.Parameters.Add("@data", DbType.Binary).Value = cd.DataBytes
                    insertFmt.ExecuteNonQuery()
                Next
            End If
        End Using
    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub ToggleFavorite(clipID As Integer)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE Clips SET IsFavorite = CASE IsFavorite WHEN 1 THEN 0 ELSE 1 END WHERE Id=@id", conn)
                cmd.Parameters.AddWithValue("@id", clipID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub SetFavorite(clipID As Integer, isFavorite As Boolean)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE Clips SET IsFavorite = @fav WHERE Id=@id", conn)
                cmd.Parameters.AddWithValue("@fav", If(isFavorite, 1, 0))
                cmd.Parameters.AddWithValue("@id", clipID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Public Function MoveClipToProfile(clipId As Integer, newProfileId As Integer) As Boolean
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' 1. Load the clip's hash + version
            Dim hash As String = Nothing
            Dim hv As Integer = 0

            Using cmd As New SQLiteCommand("
                SELECT AggregateHash, HashVersion
                FROM Clips
                WHERE Id=@id", conn)

                cmd.Parameters.AddWithValue("@id", clipId)

                Using r = cmd.ExecuteReader()
                    If r.Read() Then
                        hash = r.GetString(0)
                        hv = r.GetInt32(1)
                    Else
                        Return False ' clip not found
                    End If
                End Using
            End Using

            ' 2. Check if target profile already contains this clip
            Using checkCmd As New SQLiteCommand("
                SELECT COUNT(*)
                FROM Clips
                WHERE AggregateHash=@hash
                  AND HashVersion=@hv
                  AND ProfileID=@pid", conn)

                checkCmd.Parameters.AddWithValue("@hash", hash)
                checkCmd.Parameters.AddWithValue("@hv", hv)
                checkCmd.Parameters.AddWithValue("@pid", newProfileId)

                Dim exists As Boolean = (CInt(checkCmd.ExecuteScalar()) > 0)
                If exists Then
                    Return False ' duplicate → do not move
                End If
            End Using

            ' 3. Safe to move
            Using upd As New SQLiteCommand("
                UPDATE Clips
                SET ProfileID=@pid
                WHERE Id=@id", conn)

                upd.Parameters.AddWithValue("@pid", newProfileId)
                upd.Parameters.AddWithValue("@id", clipId)
                upd.ExecuteNonQuery()
            End Using

            Return True
        End Using
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub RestoreClip(entryId As Integer)

        ' ---------------------------------------------------------
        ' 1. Mark Suppress var ON for the next clipboard event
        ' ---------------------------------------------------------
        App.SuppressNextClipboardEvent = True
        'Debug.Print("setting App.IsRestoring to True")

        ' ---------------------------------------------------------
        ' 2. Load stored formats for this entry
        ' ---------------------------------------------------------
        Dim formats As New List(Of ClipData)

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
                SELECT FormatId, IFNULL(FormatName,''), Data
                FROM ClipFormats
                WHERE EntryId=@id", conn)

                cmd.Parameters.AddWithValue("@id", entryId)

                Using r = cmd.ExecuteReader()
                    While r.Read()
                        formats.Add(New ClipData With {
                            .FormatId = CUInt(r.GetInt32(0)),
                            .FormatName = r.GetString(1),
                            .DataBytes = DirectCast(r("Data"), Byte())
                        })
                    End While
                End Using
            End Using
        End Using

        If formats.Count = 0 Then Exit Sub

        ' ---------------------------------------------------------
        ' 3. Defensive fixups for text formats
        ' ---------------------------------------------------------
        For Each cd In formats
            If cd.FormatId = Skye.WinAPI.CF_UNICODETEXT Then
                cd.DataBytes = EnsureUnicodeNull(cd.DataBytes)
            ElseIf cd.FormatId = Skye.WinAPI.CF_TEXT OrElse cd.FormatId = Skye.WinAPI.CF_OEMTEXT Then
                cd.DataBytes = EnsureAnsiNull(cd.DataBytes)
            End If
        Next

        ' ---------------------------------------------------------
        ' 4. Open clipboard and replay formats
        ' ---------------------------------------------------------
        If Not Skye.WinAPI.OpenClipboard(App.AppHandle) Then Exit Sub

        Try
            Skye.WinAPI.EmptyClipboard()

            Dim restoredCount As Integer = 0

            ' Replay in ranked order
            For Each cd In formats.OrderBy(Function(f) OrderRank(f))

                Try
                    Dim dataLen As Integer = If(cd.DataBytes?.Length, 0)
                    If dataLen <= 0 OrElse cd.FormatId = Skye.WinAPI.CF_BITMAP Then Continue For

                    ' Allocate movable global memory
                    Dim hMem As IntPtr = Skye.WinAPI.GlobalAlloc(Skye.WinAPI.GMEM_MOVEABLE, CType(dataLen, UIntPtr))
                    If hMem = IntPtr.Zero Then Continue For

                    Dim pMem As IntPtr = Skye.WinAPI.GlobalLock(hMem)
                    If pMem = IntPtr.Zero Then
                        Skye.WinAPI.GlobalFree(hMem)
                        Continue For
                    End If

                    Try
                        Marshal.Copy(cd.DataBytes, 0, pMem, dataLen)
                    Finally
                        Skye.WinAPI.GlobalUnlock(hMem)
                    End Try

                    ' Resolve format ID
                    Dim fmtId As UInteger = cd.FormatId
                    If fmtId = 0UI AndAlso cd.FormatName.Length > 0 Then
                        fmtId = Skye.WinAPI.RegisterClipboardFormat(cd.FormatName)
                    End If
                    If fmtId = 0UI Then
                        Skye.WinAPI.GlobalFree(hMem)
                        Continue For
                    End If

                    ' SetClipboardData transfers ownership on success
                    Dim hRes As IntPtr = Skye.WinAPI.SetClipboardData(fmtId, hMem)
                    If hRes = IntPtr.Zero Then
                        Skye.WinAPI.GlobalFree(hMem)
                    Else
                        restoredCount += 1
                    End If

                Catch
                    ' Skip this format on any error
                End Try

            Next

        Finally
            Skye.WinAPI.CloseClipboard()
        End Try

        ' ---------------------------------------------------------
        ' 5. Promote clip (update LastUsedAt)
        ' ---------------------------------------------------------
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
                UPDATE Clips
                SET LastUsedAt = @now
                WHERE Id = @id", conn)

                cmd.Parameters.AddWithValue("@now", DateTime.UtcNow)
                cmd.Parameters.AddWithValue("@id", entryId)
                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetClipById(clipID As Integer) As FullClipInfo
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Dim cmd As New SQLiteCommand("
            SELECT Id, ProfileID, Preview, SourceAppName, SourceAppPath, SourceAppIcon, IsFavorite, LastUsedAt
            FROM Clips
            WHERE Id = @id", conn)

            cmd.Parameters.AddWithValue("@id", clipID)

            Using reader = cmd.ExecuteReader()
                If reader.Read() Then
                    Dim ci As New FullClipInfo With {
                    .Id = reader.GetInt32(0),
                    .ProfileID = reader.GetInt32(1),
                    .Preview = If(reader.IsDBNull(2), "", reader.GetString(2)),
                    .SourceAppName = If(reader.IsDBNull(3), "", reader.GetString(3)),
                    .SourceAppPath = If(reader.IsDBNull(4), "", reader.GetString(4)),
                    .IsFavorite = (Not reader.IsDBNull(6) AndAlso reader.GetInt32(6) = 1)
                }

                    ' Icon
                    If Not reader.IsDBNull(5) Then
                        Dim length As Integer = CInt(reader.GetBytes(5, 0, Nothing, 0, 0))
                        Dim buffer(length - 1) As Byte
                        reader.GetBytes(5, 0, buffer, 0, length)
                        ci.SourceAppIcon = buffer
                    End If

                    ' LastUsedAt
                    If Not reader.IsDBNull(7) Then
                        ci.LastUsedAt = reader.GetDateTime(7)
                    End If

                    Return ci
                End If
            End Using
        End Using

        Return Nothing
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetRecentClips(limit As Integer) As List(Of BasicClipInfo)
        Dim clips As New List(Of BasicClipInfo)

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Dim sql As String

            If App.Settings.UseProfiles Then
                ' Profiles ON → only show clips from the active profile
                sql = "
                SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
                FROM Clips
                WHERE ProfileID = @pid
                ORDER BY LastUsedAt DESC
                LIMIT @l"
            Else
                ' Profiles OFF → show ALL clips (no filter)
                sql = "
                SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
                FROM Clips
                ORDER BY LastUsedAt DESC
                LIMIT @l"
            End If

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@l", limit)

                If App.Settings.UseProfiles Then
                    cmd.Parameters.AddWithValue("@pid", App.Settings.CurrentProfileID)
                End If

                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim ci As New BasicClipInfo With {
                        .Id = reader.GetInt32(0),
                        .Preview = If(reader.IsDBNull(1), String.Empty, reader.GetString(1))
                    }

                        If Not reader.IsDBNull(2) Then
                            ci.LastUsedAt = reader.GetDateTime(2)
                        End If

                        If Not reader.IsDBNull(3) Then
                            Dim length As Integer = CInt(reader.GetBytes(3, 0, Nothing, 0, 0))
                            Dim buffer(length - 1) As Byte
                            reader.GetBytes(3, 0, buffer, 0, length)
                            ci.SourceAppIcon = buffer
                        End If

                        If Not reader.IsDBNull(4) Then
                            ci.IsFavorite = (reader.GetInt32(4) = 1)
                        End If

                        clips.Add(ci)
                    End While
                End Using
            End Using
        End Using

        Return clips
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetFavoriteClips(limit As Integer) As List(Of BasicClipInfo)
        Dim clips As New List(Of BasicClipInfo)

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Dim sql As String

            If App.Settings.UseProfiles Then
                ' Profiles ON → only favorites from the active profile
                sql = "
                SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
                FROM Clips
                WHERE IsFavorite = 1
                  AND ProfileID = @pid
                ORDER BY LastUsedAt DESC
                LIMIT @l"
            Else
                ' Profiles OFF → show ALL favorites from ALL profiles
                sql = "
                SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
                FROM Clips
                WHERE IsFavorite = 1
                ORDER BY LastUsedAt DESC
                LIMIT @l"
            End If

            Using cmd As New SQLiteCommand(sql, conn)
                cmd.Parameters.AddWithValue("@l", limit)

                If App.Settings.UseProfiles Then
                    cmd.Parameters.AddWithValue("@pid", App.Settings.CurrentProfileID)
                End If

                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim ci As New BasicClipInfo With {
                        .Id = reader.GetInt32(0),
                        .Preview = If(reader.IsDBNull(1), "", reader.GetString(1))
                    }

                        If Not reader.IsDBNull(2) Then
                            ci.LastUsedAt = reader.GetDateTime(2)
                        End If

                        If Not reader.IsDBNull(3) Then
                            Dim length As Integer = CInt(reader.GetBytes(3, 0, Nothing, 0, 0))
                            Dim buffer(length - 1) As Byte
                            reader.GetBytes(3, 0, buffer, 0, length)
                            ci.SourceAppIcon = buffer
                        End If

                        If Not reader.IsDBNull(4) Then
                            ci.IsFavorite = (reader.GetInt32(4) = 1)
                        End If

                        clips.Add(ci)
                    End While
                End Using
            End Using
        End Using

        Return clips
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetAllClips() As List(Of ExplorerClipInfo)
        Dim clips As New List(Of ExplorerClipInfo)

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Dim cmd As New SQLiteCommand("
            SELECT Id, ProfileID, Preview, CreatedAt, LastUsedAt, SourceAppName, SourceAppPath, SourceAppIcon, IsFavorite
            FROM Clips
            ORDER BY LastUsedAt DESC", conn)

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim ci As New ExplorerClipInfo With {
                    .Id = reader.GetInt32(0),
                    .ProfileID = If(reader.IsDBNull(1), 0, reader.GetInt32(1)),
                    .Preview = If(reader.IsDBNull(2), "", reader.GetString(2)),
                    .CreatedAt = If(reader.IsDBNull(3), Date.MinValue, reader.GetDateTime(3)),
                    .LastUsedAt = If(reader.IsDBNull(4), Date.MinValue, reader.GetDateTime(4)),
                    .SourceAppName = If(reader.IsDBNull(5), "", reader.GetString(5)),
                    .SourceAppPath = If(reader.IsDBNull(6), "", reader.GetString(6)),
                    .IsFavorite = (Not reader.IsDBNull(8) AndAlso reader.GetInt32(8) = 1)
                }

                    ' Icon
                    If Not reader.IsDBNull(7) Then
                        Dim length As Integer = CInt(reader.GetBytes(7, 0, Nothing, 0, 0))
                        Dim buffer(length - 1) As Byte
                        reader.GetBytes(7, 0, buffer, 0, length)
                        ci.SourceAppIcon = buffer
                    End If

                    clips.Add(ci)
                End While
            End Using
        End Using

        Return clips
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetClipFormats(entryId As Integer) As List(Of ClipData)
        Dim list As New List(Of ClipData)

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
            SELECT FormatId, IFNULL(FormatName,''), Data
            FROM ClipFormats
            WHERE EntryId=@id", conn)

                cmd.Parameters.AddWithValue("@id", entryId)

                Using r = cmd.ExecuteReader()
                    While r.Read()
                        list.Add(New ClipData With {
                        .FormatId = CUInt(r.GetInt32(0)),
                        .FormatName = r.GetString(1),
                        .DataBytes = DirectCast(r("Data"), Byte())
                    })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetSearchableText(entryId As Integer, mode As App.TextSearchMode) As String
        Dim sb As New StringBuilder()

        ' Load formats for this clip
        Dim formats As New List(Of ClipData)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("
            SELECT FormatId, IFNULL(FormatName,''), Data
            FROM ClipFormats
            WHERE EntryId=@id", conn)

                cmd.Parameters.AddWithValue("@id", entryId)

                Using r = cmd.ExecuteReader()
                    While r.Read()
                        formats.Add(New ClipData With {
                        .FormatId = CUInt(r.GetInt32(0)),
                        .FormatName = r.GetString(1),
                        .DataBytes = DirectCast(r("Data"), Byte())
                    })
                    End While
                End Using
            End Using
        End Using

        For Each f In formats
            Select Case mode

                Case App.TextSearchMode.PlainText
                    If f.FormatId = Skye.WinAPI.CF_UNICODETEXT Then
                        sb.AppendLine(NormalizeUnicodeText(f.DataBytes))
                    ElseIf f.FormatId = Skye.WinAPI.CF_TEXT OrElse f.FormatId = Skye.WinAPI.CF_OEMTEXT Then
                        sb.AppendLine(Encoding.Default.GetString(f.DataBytes))
                    End If

                Case App.TextSearchMode.RichText
                    If f.FormatId = Skye.WinAPI.CF_RTF OrElse f.FormatName.Contains("rtf", StringComparison.OrdinalIgnoreCase) Then
                        Dim rtfString = Encoding.ASCII.GetString(f.DataBytes)
                        Dim plain = Skye.Common.RTFToPlainText(rtfString)
                        sb.AppendLine(plain)
                    End If

                Case App.TextSearchMode.HTMLText
                    If f.FormatId = Skye.WinAPI.CF_HTML OrElse f.FormatName.Contains("html", StringComparison.OrdinalIgnoreCase) Then
                        Dim htmlString = Encoding.UTF8.GetString(f.DataBytes)
                        Dim fragment = ClipRepository.ExtractHtmlFragment(htmlString)
                        Dim plain = Skye.Common.HTMLToPlainText(fragment)
                        sb.AppendLine(plain)
                    End If

                Case App.TextSearchMode.AllText
                    ' Unicode
                    If f.FormatId = Skye.WinAPI.CF_UNICODETEXT Then
                        sb.AppendLine(NormalizeUnicodeText(f.DataBytes))
                    End If

                    ' ANSI
                    If f.FormatId = Skye.WinAPI.CF_TEXT OrElse f.FormatId = Skye.WinAPI.CF_OEMTEXT Then
                        sb.AppendLine(Encoding.Default.GetString(f.DataBytes))
                    End If

                    ' RTF
                    If f.FormatId = Skye.WinAPI.CF_RTF OrElse f.FormatName.Contains("rtf", StringComparison.OrdinalIgnoreCase) Then
                        Dim rtfString = Encoding.ASCII.GetString(f.DataBytes)
                        Dim plain = Skye.Common.RTFToPlainText(rtfString)
                        sb.AppendLine(plain)
                    End If

                    ' HTML
                    If f.FormatId = Skye.WinAPI.CF_HTML OrElse f.FormatName.Contains("html", StringComparison.OrdinalIgnoreCase) Then
                        Dim htmlString = Encoding.UTF8.GetString(f.DataBytes)
                        Dim fragment = ClipRepository.ExtractHtmlFragment(htmlString)
                        Dim plain = Skye.Common.HTMLToPlainText(fragment)
                        sb.AppendLine(plain)
                    End If

            End Select
        Next

        Return sb.ToString().Trim()
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub DeleteClip(clipID As Integer)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            Using tx = conn.BeginTransaction()

                ' Delete all formats for this clip
                Using cmd1 As New SQLiteCommand("
                        DELETE FROM ClipFormats
                        WHERE EntryId = @id;
                    ", conn, tx)
                    cmd1.Parameters.AddWithValue("@id", clipID)
                    cmd1.ExecuteNonQuery()
                End Using

                ' Delete the clip itself
                Using cmd2 As New SQLiteCommand("
                        DELETE FROM Clips
                        WHERE Id = @id;
                    ", conn, tx)
                    cmd2.Parameters.AddWithValue("@id", clipID)
                    cmd2.ExecuteNonQuery()
                    Debug.Print($"Deleted Clip {clipID}")
                    WriteToLog($"Deleted Clip {clipID}")
                End Using

                tx.Commit()
            End Using
        End Using
    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Sub PurgeClips(cutoff As DateTime)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using tx = conn.BeginTransaction()

                ' Delete formats for old, non-favorite clips
                Using cmd1 As New SQLiteCommand("
                        DELETE FROM ClipFormats
                        WHERE EntryId IN (
                            SELECT Id FROM Clips
                            WHERE IsFavorite = 0
                              AND CreatedAt < @cutoff
                        );
                    ", conn, tx)
                    cmd1.Parameters.AddWithValue("@cutoff", cutoff)
                    cmd1.ExecuteNonQuery()
                End Using

                ' Delete the clips themselves
                Using cmd2 As New SQLiteCommand("
                        DELETE FROM Clips
                        WHERE IsFavorite = 0
                          AND CreatedAt < @cutoff;
                    ", conn, tx)
                    cmd2.Parameters.AddWithValue("@cutoff", cutoff)
                    Dim rows = cmd2.ExecuteNonQuery()
                    Debug.Print($"Purge Removed {rows} Clips")
                    WriteToLog($"Purge Removed {rows} Clips")
                End Using

                tx.Commit()
            End Using
        End Using
    End Sub

    ' Methods
    Private Shared Sub RunMigrations(conn As SQLiteConnection)
        ' Source App Pathh on Clips
        EnsureColumn(conn, "Clips", "SourceAppPath", "TEXT")

        ' HashVersion on Clips
        EnsureColumn(conn, "Clips", "HashVersion", "INTEGER NOT NULL DEFAULT 0")

        ' Profile ID on Clips (for multi-profile support)
        EnsureColumn(conn, "Clips", "ProfileID", "INTEGER NOT NULL DEFAULT 0")

        ' Ensure Meta rows
        EnsureMeta(conn, "DatabaseVersion", "1")
        EnsureMeta(conn, "HashVersion", "1")
    End Sub
    Private Shared Sub EnsureColumn(conn As SQLiteConnection, table As String, column As String, definition As String)
        ' Check if column exists
        Using pragma As New SQLiteCommand($"PRAGMA table_info({table});", conn)
            Using reader = pragma.ExecuteReader()
                While reader.Read()
                    If reader("name").ToString().Equals(column, StringComparison.OrdinalIgnoreCase) Then
                        Return ' Column already exists
                    End If
                End While
            End Using
        End Using
        ' Add missing column
        Using alter As New SQLiteCommand($"ALTER TABLE {table} ADD COLUMN {column} {definition};", conn)
            alter.ExecuteNonQuery()
        End Using
    End Sub
    Private Shared Sub EnsureMeta(conn As SQLiteConnection, key As String, defaultValue As String)
        Using cmd As New SQLiteCommand("
        INSERT INTO Meta([Key],[Value])
        SELECT @k, @v
        WHERE NOT EXISTS (SELECT 1 FROM Meta WHERE [Key] = @k);", conn)
            cmd.Parameters.AddWithValue("@k", key)
            cmd.Parameters.AddWithValue("@v", defaultValue)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Friend Shared Function ComputeAggregateHash(formats As List(Of ClipData)) As String
        ' For now, V1 is the only pipeline
        Return ComputeAggregateHashV1(formats)
    End Function
    Private Shared Function ComputeAggregateHashV1(formats As List(Of ClipData)) As String

        'Debug.Print("Hashing formats:")
        'For Each f In formats
        'Debug.Print("  " & f.FormatId & " - " & f.FormatName & " (" & f.DataBytes.Length & " bytes)")
        'Next

        Dim allBytes As New List(Of Byte)
        For Each f In formats
            ' Add format ID to the hash
            allBytes.AddRange(BitConverter.GetBytes(f.FormatId))
            If f.FormatId = Skye.WinAPI.CF_UNICODETEXT Then
                ' NEW: Normalize Unicode text before hashing
                Dim normalized = NormalizeUnicodeText(f.DataBytes)
                allBytes.AddRange(Encoding.UTF8.GetBytes(normalized))
            ElseIf f.FormatId = Skye.WinAPI.CF_RTF Then
                ' Normalize RTF before hashing
                Dim rtfText = Encoding.ASCII.GetString(f.DataBytes)
                Dim normalized = NormalizeRtf(rtfText)
                allBytes.AddRange(Encoding.UTF8.GetBytes(normalized))
            Else
                ' Use raw bytes for everything else
                allBytes.AddRange(f.DataBytes)
            End If
        Next
        Dim hashBytes = SHA256.HashData(allBytes.ToArray())
        Return Convert.ToBase64String(hashBytes)
    End Function
    Private Shared Function FilterFormatsForHash(formats As List(Of ClipData)) As List(Of ClipData)
        Dim importantIds As UInteger() = {
            Skye.WinAPI.CF_UNICODETEXT,
            Skye.WinAPI.CF_RTF,
            Skye.WinAPI.CF_HTML,
            Skye.WinAPI.CF_HDROP,
            Skye.WinAPI.CF_DIB,
            Skye.WinAPI.CF_DIBV5
        }

        Return formats.
            Where(Function(f) importantIds.Contains(f.FormatId)).
            OrderBy(Function(f) f.FormatId).
            ToList()
    End Function
    Friend Shared Function NormalizeUnicodeText(rawBytes As Byte()) As String
        If rawBytes Is Nothing OrElse rawBytes.Length = 0 Then
            Return String.Empty
        End If

        ' Convert bytes → string
        Dim s As String = Encoding.Unicode.GetString(rawBytes)

        ' Remove BOM (U+FEFF)
        If s.Length > 0 AndAlso AscW(s(0)) = &HFEFF Then
            s = s.Substring(1)
        End If

        ' Remove trailing nulls
        s = s.TrimEnd(ChrW(0))

        ' Remove zero-width characters
        Dim zeroWidth() As Integer = {
        &H200B, ' zero-width space
        &H200C, ' zero-width non-joiner
        &H200D, ' zero-width joiner
        &H2060  ' word joiner
    }

        For Each zw In zeroWidth
            s = s.Replace(ChrW(zw), "")
        Next

        ' Normalize Unicode (NFC)
        s = s.Normalize(NormalizationForm.FormC)

        ' Normalize newlines
        s = s.Replace(vbCrLf, vbLf)
        s = s.Replace(vbCr, vbLf)

        ' Collapse whitespace
        s = Regex.Replace(s, "\s+", " ").Trim()

        Return s
    End Function
    Private Shared Function NormalizeRtf(rtf As String) As String
        If String.IsNullOrWhiteSpace(rtf) Then Return String.Empty

        ' Remove generator metadata (varies between apps)
        rtf = Regex.Replace(rtf, "\\\*\\generator[^;]*;", "", RegexOptions.IgnoreCase)

        ' Remove creation/modification timestamps
        rtf = Regex.Replace(rtf, "\\creatim[^}]*}", "", RegexOptions.IgnoreCase)
        rtf = Regex.Replace(rtf, "\\revtim[^}]*}", "", RegexOptions.IgnoreCase)

        ' Remove hidden math / editor metadata
        rtf = Regex.Replace(rtf, "\\\*\\mmathPr[^}]*}", "", RegexOptions.IgnoreCase)

        ' Collapse whitespace
        rtf = Regex.Replace(rtf, "\s+", " ")

        Return rtf.Trim()
    End Function
    Friend Shared Function ExtractHtmlFragment(rawHtml As String) As String
        If String.IsNullOrEmpty(rawHtml) Then
            Return String.Empty
        End If

        Const startTag As String = "<!--StartFragment-->"
        Const endTag As String = "<!--EndFragment-->"

        Dim startIndex As Integer = rawHtml.IndexOf(startTag, StringComparison.OrdinalIgnoreCase)
        Dim endIndex As Integer = rawHtml.IndexOf(endTag, StringComparison.OrdinalIgnoreCase)

        If startIndex >= 0 AndAlso endIndex > startIndex Then
            startIndex += startTag.Length
            Return rawHtml.Substring(startIndex, endIndex - startIndex)
        End If

        ' No markers → return whole HTML
        Return rawHtml
    End Function

End Class
