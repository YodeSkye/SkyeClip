
Imports System.Runtime.InteropServices
Imports System.Text
Imports SkyeClip.ClipRepository

Module ClipboardWin32

    ' Declarations
    Private ReadOnly _cache As New Dictionary(Of String, UInteger)(StringComparer.Ordinal)

    ' Methods
    Private Function TryOpenClipboard(hwnd As IntPtr, Optional retries As Integer = 5) As Boolean
        For i As Integer = 1 To retries
            If Skye.WinAPI.OpenClipboard(hwnd) Then Return True
            Threading.Thread.Sleep(10) ' wait 10ms before retry
        Next
        Return False
    End Function
    Public Function GetId(name As String) As UInteger
        If String.IsNullOrEmpty(name) Then Return 0UI
        Dim id As UInteger = 0UI
        If _cache.TryGetValue(name, id) Then Return id
        id = Skye.WinAPI.RegisterClipboardFormat(name)
        If id = 0UI Then
            ' Registration failed (should be rare); still store 0 to avoid repeated calls
            _cache(name) = 0UI
            Return 0UI
        End If
        _cache(name) = id
        Return id
    End Function
    Friend Function CaptureFormatsFromClipboard() As List(Of ClipData)
        Dim all As New List(Of ClipData)

        ' Text formats (Unicode, ANSI, OEM, RTF, HTML, CSV)
        all.AddRange(CaptureTextFormats())

        ' File drop (CF_HDROP)
        all.AddRange(CaptureFileDrop())

        ' Image formats (CF_DIB, CF_DIBV5)
        all.AddRange(CaptureImageFormats())

        Return all
    End Function
    Public Function CaptureTextFormats() As List(Of ClipData)
        Dim list As New List(Of ClipData)

        If Not TryOpenClipboard(App.AppHandle) Then
            'Debug.Print("CaptureTextFormats: OpenClipboard failed after retries")
            Return list
        End If

        Try
            Dim formatsToGrab As UInteger() = {Skye.WinAPI.CF_UNICODETEXT, Skye.WinAPI.CF_TEXT, Skye.WinAPI.CF_OEMTEXT}
            For Each fmt In formatsToGrab
                Dim hData As IntPtr = Skye.WinAPI.GetClipboardData(fmt)
                If hData = IntPtr.Zero Then Continue For

                Dim size As Integer = Skye.WinAPI.GlobalSize(hData)
                Dim cb As Integer = size
                Dim pData As IntPtr = Skye.WinAPI.GlobalLock(hData)
                If pData = IntPtr.Zero Then Continue For

                Try
                    Dim bytes As Byte()
                    If cb > 0 Then
                        bytes = New Byte(cb - 1) {}
                        Marshal.Copy(pData, bytes, 0, cb)
                    Else
                        bytes = ReadUntilTerminator(fmt, pData)
                    End If

                    Dim name As String = ""
                    Select Case fmt
                        Case Skye.WinAPI.CF_UNICODETEXT : name = "UnicodeText"
                        Case Skye.WinAPI.CF_TEXT : name = "Text"
                        Case Skye.WinAPI.CF_OEMTEXT : name = "OEMText"
                    End Select
                    list.Add(New ClipData With {
                        .FormatId = fmt,
                        .FormatName = name,
                        .DataBytes = bytes
                    })

                Finally
                    Skye.WinAPI.GlobalUnlock(hData)
                End Try
            Next

            ' Optional: registered text formats (RTF/HTML/CSV)
            Dim names = New String() {"Rich Text Format", "HTML Format", "Csv"}
            For Each name In names
                Dim regId = Skye.WinAPI.RegisterClipboardFormat(name)
                If regId = 0UI Then Continue For

                Dim hData As IntPtr = Skye.WinAPI.GetClipboardData(regId)
                If hData = IntPtr.Zero Then Continue For

                Dim size As Integer = Skye.WinAPI.GlobalSize(hData)
                Dim cb As Integer = size
                Dim pData As IntPtr = Skye.WinAPI.GlobalLock(hData)
                If pData = IntPtr.Zero Then Continue For

                Try
                    Dim bytes As Byte()
                    If cb > 0 Then
                        bytes = New Byte(cb - 1) {}
                        Marshal.Copy(pData, bytes, 0, cb)
                    Else
                        bytes = ReadAnsiUntilNull(pData)
                    End If

                    list.Add(New ClipData With {
                    .FormatId = regId,
                    .FormatName = name,
                    .DataBytes = bytes
                })
                Finally
                    Skye.WinAPI.GlobalUnlock(hData)
                End Try
            Next
        Finally
            Skye.WinAPI.CloseClipboard()
        End Try

        'Debug.Print($"CaptureTextFormats: Captured {list.Count} formats")
        Return list
    End Function
    Public Function CaptureFileDrop() As List(Of ClipData)
        Dim list As New List(Of ClipData)

        If Not TryOpenClipboard(App.AppHandle) Then
            'Debug.Print("CaptureFileDrop: OpenClipboard failed")
            Return list
        End If

        Try
            Dim hData As IntPtr = Skye.WinAPI.GetClipboardData(Skye.WinAPI.CF_HDROP)
            If hData = IntPtr.Zero Then Return list

            Dim size As Integer = Skye.WinAPI.GlobalSize(hData)
            If size <= 0 Then Return list

            Dim pData As IntPtr = Skye.WinAPI.GlobalLock(hData)
            If pData = IntPtr.Zero Then Return list

            Try
                ' Read UTF-16 string list until double null
                Dim bytes(size - 1) As Byte
                Marshal.Copy(pData, bytes, 0, size)
                list.Add(New ClipData With {
                .FormatId = Skye.WinAPI.CF_HDROP,
                .FormatName = "FileDrop",
                .DataBytes = bytes
            })
            Finally
                Skye.WinAPI.GlobalUnlock(hData)
            End Try
        Finally
            Skye.WinAPI.CloseClipboard()
        End Try

        'Debug.Print($"CaptureFileDrop: Captured {list.Count} formats")
        Return list
    End Function
    Public Function CaptureImageFormats() As List(Of ClipData)
        Dim list As New List(Of ClipData)

        If Not TryOpenClipboard(App.AppHandle) Then
            'Debug.Print("CaptureImageFormats: OpenClipboard failed")
            Return list
        End If

        Try
            Dim formatsToGrab As UInteger() = {Skye.WinAPI.CF_DIB, Skye.WinAPI.CF_DIBV5}

            For Each fmt In formatsToGrab
                Dim hData As IntPtr = Skye.WinAPI.GetClipboardData(fmt)
                If hData = IntPtr.Zero Then Continue For

                Dim size As Integer = Skye.WinAPI.GlobalSize(hData)
                If size <= 0 Then Continue For

                Dim pData As IntPtr = Skye.WinAPI.GlobalLock(hData)
                If pData = IntPtr.Zero Then Continue For

                Try
                    Dim bytes(size - 1) As Byte
                    Marshal.Copy(pData, bytes, 0, size)

                    list.Add(New ClipData With {
                    .FormatId = fmt,
                    .FormatName = If(fmt = Skye.WinAPI.CF_DIB, "CF_DIB", "CF_DIBV5"),
                    .DataBytes = bytes
                })

                Finally
                    Skye.WinAPI.GlobalUnlock(hData)
                End Try
            Next

        Finally
            Skye.WinAPI.CloseClipboard()
        End Try

        'Debug.Print($"CaptureImageFormats: Captured {list.Count} formats")
        Return list
    End Function
    Public Function ReadUntilTerminator(fmt As UInteger, pData As IntPtr) As Byte()
        If fmt = Skye.WinAPI.CF_UNICODETEXT Then
            ' Read UTF-16LE until 0x0000
            Dim bytes As New List(Of Byte)
            Dim offset As Integer = 0
            While True
                Dim b0 = Marshal.ReadByte(pData, offset)
                Dim b1 = Marshal.ReadByte(pData, offset + 1)
                bytes.Add(b0) : bytes.Add(b1)
                offset += 2
                If b0 = 0 AndAlso b1 = 0 Then Exit While
            End While
            Return bytes.ToArray()
        Else
            Return ReadAnsiUntilNull(pData)
        End If
    End Function
    Public Function ReadAnsiUntilNull(pData As IntPtr) As Byte()
        Dim bytes As New List(Of Byte)
        Dim offset As Integer = 0
        While True
            Dim b = Marshal.ReadByte(pData, offset)
            bytes.Add(b)
            offset += 1
            If b = 0 Then Exit While
        End While
        Return bytes.ToArray()
    End Function
    Public Function OrderRank(cd As ClipData) As Integer
        Select Case cd.FormatId
            Case Skye.WinAPI.CF_UNICODETEXT : Return 0
            Case Skye.WinAPI.CF_TEXT, Skye.WinAPI.CF_OEMTEXT : Return 1
            Case Else : Return 2 ' registered
        End Select
    End Function
    Public Function EnsureAnsiNull(bytes As Byte()) As Byte()
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return New Byte() {0}
        If bytes(bytes.Length - 1) = 0 Then Return bytes
        Dim resized(bytes.Length) As Byte
        Buffer.BlockCopy(bytes, 0, resized, 0, bytes.Length)
        Return resized
    End Function
    Public Function EnsureUnicodeNull(bytes As Byte()) As Byte()
        If bytes Is Nothing OrElse bytes.Length < 2 Then Return New Byte() {0, 0}
        Dim last0 = bytes(bytes.Length - 2)
        Dim last1 = bytes(bytes.Length - 1)
        If last0 = 0 AndAlso last1 = 0 Then Return bytes
        Dim resized(bytes.Length + 1) As Byte
        Buffer.BlockCopy(bytes, 0, resized, 0, bytes.Length)
        Return resized
    End Function

End Module
