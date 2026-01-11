Public Class DevToolsFormatViewer

    ' Form Events
    Private Sub DevToolsFormatViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyTheme(Me)
    End Sub

    ' Methods
    Public Sub SetData(formatName As String, bytes As Byte())
        Text = "Format Data Viewer - " & formatName

        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("Format: " & formatName)
        sb.AppendLine("Size: " & If(bytes Is Nothing, "0", bytes.Length.ToString()) & " bytes")
        sb.AppendLine()
        sb.AppendLine("Hex Dump:")
        sb.AppendLine()
        sb.AppendLine(HexDump(bytes))

        RTB.Text = sb.ToString()
    End Sub
    Private Function HexDump(bytes As Byte(), Optional bytesPerLine As Integer = 16) As String
        If bytes Is Nothing Then Return "(no data)"

        Dim sb As New System.Text.StringBuilder()

        For i = 0 To bytes.Length - 1 Step bytesPerLine
            Dim line = bytes.Skip(i).Take(bytesPerLine).ToArray()

            ' Hex section
            sb.Append(i.ToString("X8"))
            sb.Append("  ")

            For Each b In line
                sb.Append(b.ToString("X2"))
                sb.Append(" ")
            Next

            ' Padding for short lines
            Dim missing = bytesPerLine - line.Length
            If missing > 0 Then sb.Append(New String(" "c, missing * 3))

            sb.Append(" ")

            ' ASCII section
            For Each b In line
                Dim ch As Char = If(b >= 32 AndAlso b <= 126, ChrW(b), "."c)
                sb.Append(ch)
            Next

            sb.AppendLine()
        Next

        Return sb.ToString()
    End Function

End Class