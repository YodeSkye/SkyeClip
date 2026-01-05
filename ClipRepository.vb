
Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Friend Class ClipRepository

    ' Declarations
    Friend Class ClipData
        Public Property FormatId As UInteger
        Public Property FormatName As String
        Public Property DataBytes As Byte()
    End Class
    Friend Class ClipInfo
        Public Property Id As Integer
        Public Property Preview As String
        Public Property LastUsedAt As DateTime ' optional
        Public Property SourceAppIcon As Byte()
        Public Property IsFavorite As Boolean
    End Class

    ' Constructor
    Friend Sub New()
        EnsureSchema()
    End Sub
    Private Shared Sub EnsureSchema()
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Create Clips table if missing
            Dim createClipsCmd As New SQLiteCommand("
            CREATE TABLE IF NOT EXISTS Clips (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Preview TEXT,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                LastUsedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                AggregateHash TEXT NOT NULL,
                SourceAppName TEXT,
                SourceAppIcon BLOB,
                IsFavorite INTEGER DEFAULT 0
            )", conn)
            createClipsCmd.ExecuteNonQuery()

            ' Create ClipFormats table if missing
            Dim createFormatsCmd As New SQLiteCommand("
            CREATE TABLE IF NOT EXISTS ClipFormats (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EntryId INTEGER NOT NULL,
                FormatId INTEGER NOT NULL,
                FormatName TEXT,
                Data BLOB NOT NULL
            )", conn)
            createFormatsCmd.ExecuteNonQuery()

            ' Indexes (safe to re-run)
            Dim idxCreatedCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_clips_created ON Clips(CreatedAt)", conn)
            idxCreatedCmd.ExecuteNonQuery()

            Dim idxLastUsedCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_clips_lastused ON Clips(LastUsedAt)", conn)
            idxLastUsedCmd.ExecuteNonQuery()

            Dim idxHashCmd As New SQLiteCommand("CREATE UNIQUE INDEX IF NOT EXISTS idx_clips_hash ON Clips(AggregateHash)", conn)
            idxHashCmd.ExecuteNonQuery()

            Dim indexCmd As New SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_ClipFormats_EntryId ON ClipFormats(EntryId);", conn)
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
        Dim preview As String = "< No Preview >"
        Dim uni = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_UNICODETEXT)

        If uni IsNot Nothing Then
            Dim s = Encoding.Unicode.GetString(uni.DataBytes)
            Dim escaped = String.Join(" ", s.Select(Function(c) AscW(c).ToString("X4")))
            'Debug.Print("RAW UNICODE CODEPOINTS: " & escaped)
        End If

        Dim filedrop = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_HDROP)
        Dim dib = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_DIB OrElse f.FormatId = Skye.WinAPI.CF_DIBV5)
        If uni IsNot Nothing Then
            Dim s = Encoding.Unicode.GetString(Skye.Common.TrimUnicodeNull(uni.DataBytes))
            s = s.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ")
            s = System.Text.RegularExpressions.Regex.Replace(s, "\s+", " ").Trim()

            If String.IsNullOrWhiteSpace(s) Then
                preview = "< No Preview >"
            Else
                preview = Skye.Common.Trunc(s, App.Settings.MaxClipPreviewLength)
                Dim hasRtf As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_RTF OrElse (f.FormatName & "").ToLower().Contains("rtf", StringComparison.OrdinalIgnoreCase))
                Dim hasHtml As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_HTML OrElse (f.FormatName & "").ToLower().Contains("html", StringComparison.OrdinalIgnoreCase))
                If hasRtf Then preview &= App.CBRTFSuffix
                If hasHtml Then preview &= App.CBHTMLSuffix
            End If

        ElseIf filedrop IsNot Nothing Then
            preview = App.BuildFileDropPreview

        ElseIf dib IsNot Nothing Then
            preview = "< Image >"

        Else
            Dim f0 = formats.First()
            preview = If(String.IsNullOrWhiteSpace(f0.FormatName), $"Format {f0.FormatId}", f0.FormatName)
        End If

        ' 3) Insert or update entry
        'Dim aggHash As String = ComputeAggregateHash(formats)
        Dim formatsForHash = FilterFormatsForHash(formats)
        Dim aggHash As String = ComputeAggregateHash(formatsForHash)
        Dim nowVal As DateTime = DateTime.UtcNow

        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Check for existing clip
            Dim checkCmd As New SQLiteCommand("SELECT Id FROM Clips WHERE AggregateHash=@hash", conn)
            checkCmd.Parameters.AddWithValue("@hash", aggHash)
            Dim existingIdObj = checkCmd.ExecuteScalar()

            Dim entryId As Integer

            If existingIdObj IsNot Nothing AndAlso Not DBNull.Value.Equals(existingIdObj) Then
                ' Duplicate found → update LastUsedAt
                entryId = Convert.ToInt32(existingIdObj)
                Dim updCmd As New SQLiteCommand("UPDATE Clips SET LastUsedAt=@now WHERE Id=@id", conn)
                updCmd.Parameters.AddWithValue("@now", nowVal)
                updCmd.Parameters.AddWithValue("@id", entryId)
                updCmd.ExecuteNonQuery()
            Else
                ' New clip → insert metadata
                Dim sourceInfo = GetSourceAppInfo()
                Dim insertEntry As New SQLiteCommand("
                    INSERT INTO Clips (Preview, CreatedAt, LastUsedAt, AggregateHash, SourceAppName, SourceAppIcon)
                    VALUES (@p, @c, @l, @hash, @app, @icon);
                    SELECT last_insert_rowid();", conn)
                insertEntry.Parameters.AddWithValue("@p", preview)
                insertEntry.Parameters.AddWithValue("@c", nowVal)
                insertEntry.Parameters.AddWithValue("@l", nowVal)
                insertEntry.Parameters.AddWithValue("@hash", aggHash)
                insertEntry.Parameters.AddWithValue("@app", sourceInfo.AppName)
                insertEntry.Parameters.Add("@icon", DbType.Binary).Value = If(sourceInfo.IconBytes IsNot Nothing, CType(sourceInfo.IconBytes, Object), DBNull.Value)
                entryId = Convert.ToInt32(insertEntry.ExecuteScalar())

                ' Insert formats only for new clips
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
    Friend Sub RestoreClip(entryId As Integer)
        ' Load stored formats for this entry
        Dim formats As New List(Of ClipData)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()
            Using cmd As New SQLiteCommand("SELECT FormatId, IFNULL(FormatName,''), Data FROM ClipFormats WHERE EntryId=@id", conn)
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

        If formats.Count = 0 Then
            'Debug.Print($"RestoreClip: No formats found for entry {entryId}")
            Exit Sub
        End If
        'Debug.Print($"RestoreClip: Loaded {formats.Count} formats for entry {entryId}")

        ' Defensive fixups for text formats only
        For Each cd In formats
            If cd.FormatId = Skye.WinAPI.CF_UNICODETEXT Then
                cd.DataBytes = EnsureUnicodeNull(cd.DataBytes)
            ElseIf cd.FormatId = Skye.WinAPI.CF_TEXT OrElse cd.FormatId = Skye.WinAPI.CF_OEMTEXT Then
                cd.DataBytes = EnsureAnsiNull(cd.DataBytes)
            End If
        Next

        ' Replay
        If Not Skye.WinAPI.OpenClipboard(App.AppHandle) Then
            'Debug.Print("RestoreClip: OpenClipboard failed")
            Exit Sub
        End If

        Try
            Skye.WinAPI.EmptyClipboard()

            Dim restoredCount As Integer = 0

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

                    ' Resolve format id
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

            'Debug.Print($"RestoreClip: Restored {restoredCount} formats for entry {entryId}")
        Finally
            Skye.WinAPI.CloseClipboard()
        End Try
    End Sub
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetRecentClips(limit As Integer) As List(Of ClipInfo)
        Dim clips As New List(Of ClipInfo)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Query Clips table ordered by LastUsedAt, include IsFavorite
            Dim cmd As New SQLiteCommand("
            SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
            FROM Clips
            ORDER BY LastUsedAt DESC
            LIMIT @l", conn)

            cmd.Parameters.AddWithValue("@l", limit)

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim ci As New ClipInfo With {
                        .Id = reader.GetInt32(0),
                        .Preview = If(reader.IsDBNull(1), "", reader.GetString(1))
                    }

                    ' LastUsedAt
                    If Not reader.IsDBNull(2) Then
                        ci.LastUsedAt = reader.GetDateTime(2)
                    End If

                    ' SourceAppIcon (stored as BLOB)
                    If Not reader.IsDBNull(3) Then
                        Dim length As Integer = CInt(reader.GetBytes(3, 0, Nothing, 0, 0))
                        Dim buffer(length - 1) As Byte
                        reader.GetBytes(3, 0, buffer, 0, CInt(length))
                        ci.SourceAppIcon = buffer
                    End If

                    ' IsFavorite (INTEGER 0/1 → Boolean)
                    If Not reader.IsDBNull(4) Then
                        ci.IsFavorite = (reader.GetInt32(4) = 1)
                    End If

                    clips.Add(ci)
                End While
            End Using
        End Using
        Return clips
    End Function
    <CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Friend Function GetFavoriteClips(limit As Integer) As List(Of ClipInfo)
        Dim clips As New List(Of ClipInfo)
        Using conn As New SQLiteConnection(App.DBConnectionString)
            conn.Open()

            ' Query Clips table for favorites only, ordered by LastUsedAt
            Dim cmd As New SQLiteCommand("
            SELECT Id, Preview, LastUsedAt, SourceAppIcon, IsFavorite
            FROM Clips
            WHERE IsFavorite = 1
            ORDER BY LastUsedAt DESC
            LIMIT @l", conn)

            cmd.Parameters.AddWithValue("@l", limit)

            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim ci As New ClipInfo With {
                        .Id = reader.GetInt32(0),
                        .Preview = If(reader.IsDBNull(1), "", reader.GetString(1))
                    }

                    ' LastUsedAt
                    If Not reader.IsDBNull(2) Then
                        ci.LastUsedAt = reader.GetDateTime(2)
                    End If

                    ' SourceAppIcon (stored as BLOB)
                    If Not reader.IsDBNull(3) Then
                        Dim length As Integer = CInt(reader.GetBytes(3, 0, Nothing, 0, 0))
                        Dim buffer(length - 1) As Byte
                        reader.GetBytes(3, 0, buffer, 0, CInt(length))
                        ci.SourceAppIcon = buffer
                    End If

                    ' IsFavorite (INTEGER 0/1 → Boolean)
                    If Not reader.IsDBNull(4) Then
                        ci.IsFavorite = (reader.GetInt32(4) = 1)
                    End If

                    clips.Add(ci)
                End While
            End Using
        End Using
        Return clips
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
    'Private Shared Function ComputeAggregateHash(formats As IEnumerable(Of ClipData)) As String
    '    ' Collect all bytes into a single buffer
    '    Using ms As New IO.MemoryStream()
    '        For Each cd In formats.OrderBy(Function(f) f.FormatId)
    '            ms.Write(cd.DataBytes, 0, cd.DataBytes.Length)
    '        Next
    '        Dim hashBytes As Byte() = Security.Cryptography.SHA256.HashData(ms.ToArray())
    '        Return Convert.ToHexString(hashBytes)
    '    End Using
    'End Function
    Private Shared Function ComputeAggregateHash(formats As List(Of ClipData)) As String

        'Debug.Print("Hashing formats:")
        For Each f In formats
            'Debug.Print("  " & f.FormatId & " - " & f.FormatName & " (" & f.DataBytes.Length & " bytes)")
        Next

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

End Class
