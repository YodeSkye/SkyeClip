
Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Microsoft.Win32

Friend Module App

    ' DECLARATIONS
    Friend Enum TextSearchMode
        PlainText
        RichText
        HTMLText
        AllText
    End Enum
    Friend Class CommonAction
        Public Property Text As String
        Public Property Image As Image
        Public Property Handler As MouseEventHandler
    End Class
    Friend ReadOnly AdjustScreenBoundsNormalWindow As Byte = 8 'AdjustScreenBoundsNormalWindow is the number of pixels to adjust the screen bounds for normal windows.
    Friend ReadOnly AdjustScreenBoundsDialogWindow As Byte = 10 'AdjustScreenBoundsDialogWindow is the number of pixels to adjust the screen bounds for dialog windows.
    Friend ReadOnly CBEmptyString As String = "< Clipboard Empty >"
    Friend ReadOnly CBRTFSuffix As String = " <RTF>"
    Friend ReadOnly CBHTMLSuffix As String = " <HTML>"
    Friend ReadOnly MenuFont As New Font(SystemFonts.MenuFont.FontFamily, 10)
    Friend ReadOnly AttributionMicrosoft As String = "https://www.microsoft.com" 'AttributionMicrosoft is the URL for Microsoft, which provides various APIs and libraries used in the application.
    Friend ReadOnly AttributionSQLite As String = "https://www.sqlite.org/index.html" 'AttributionSQLite is the URL for SQLite, which provides database functionality in the application.
    Friend ReadOnly AttributionIcons8 As String = "https://icons8.com/" 'AttributionIcons8 is the URL for Icons8, which provides icons used in the application.
    Friend ReadOnly SponsorGitHub As String = "https://github.com/sponsors/YodeSkye" 'SponsorGitHub is the URL for the GitHub Sponsors page of the application's developer.
    Friend ReadOnly SponsorPayPal As String = "https://www.paypal.com/donate/?hosted_button_id=RVH5T9H69G6CS" 'SponsorPayPal is the URL for the PayPal donation page for the application's developer.
    Friend ScratchPadText As String = String.Empty
    Friend Property ChangeLogLastVersionShown As String = String.Empty
    Friend Property CBLivePreview As String
    Friend Property AppHandle As IntPtr

    ' Paths
    Friend ReadOnly UserPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Skye\" 'UserPath is the base path for user-specific files.
#If DEBUG Then
    Friend ReadOnly LogPath As String = IO.Path.GetTempPath & GetAssemblyName() & "LogDEV.txt" 'LogPath is the path to the log file.
    Friend ReadOnly DBPath As String = UserPath & Application.ProductName & "ClipboardDEV.db" 'DatabasePath is the path to the SQLite database file.
    Friend ReadOnly ScratchPadPath As String = UserPath & Application.ProductName & "ScratchPadDEV.rtf" 'ScratchPadPath is the path to the ScratchPad KeepText RTF file.
#Else
    Friend ReadOnly LogPath As String = IO.Path.GetTempPath & GetAssemblyName() & "Log.txt" 'LogPath is the path to the log file.
    Friend ReadOnly DBPath As String = UserPath & Application.ProductName & "Clipboard.db" 'DatabasePath is the path to the SQLite database file.
    Friend ReadOnly ScratchPadPath As String = UserPath & Application.ProductName & "ScratchPad.rtf" 'ScratchPadPath is the path to the ScratchPad KeepText RTF file.
#End If
    Friend ReadOnly DBConnectionString As String = "Data Source=" & DBPath & ";Version=3;"

    ' Settings
    Friend Class Settings
        Friend Shared AutoStartWithWindows As Boolean ' whether to auto-start with Windows
        Friend Shared MaxClips As Integer ' maximum number of clipboard entries to show
        Friend Shared MaxClipPreviewLength As Integer ' in characters
        Friend Shared BlinkOnNewClip As Boolean ' whether to blink the tray icon on new clipboard entry
        Friend Shared NotifyOnNewClip As Boolean ' whether to show a notification on new clipboard entry
        Friend Shared PlaySoundWithNotify As Boolean ' whether to play a sound with notification
        Friend Shared AutoPurge As Boolean ' whether to automatically purge old clipboard entries when older than a certain date
        Friend Shared LastPurgeDate As DateTime ' the last date when automatic purge was performed
        Friend Shared PurgeDays As Integer ' number of days after which clipboard entries are purged
        Friend Shared ShowOpenSourceApp As Boolean ' whether to show the source application on clip context menu
        Friend Shared ScratchPadLocation As Point ' location of the ScratchPad window
        Friend Shared ScratchPadSize As Size ' size of the ScratchPad window
        Friend Shared ScratchPadKeepText As Boolean ' whether to keep text in the ScratchPad
        Friend Class HotKeys
            Friend Shared ToggleFavorite As Keys
            Friend Shared ShowViewer As Keys
            Friend Shared ShowScratchPad As Keys
            Friend Shared DevTools As Keys = Keys.Control Or Keys.Shift Or Keys.D 'Not a Saved Setting
        End Class

        Friend Shared Sub Load()
            Dim starttime As TimeSpan = DateTime.Now.TimeOfDay
            ChangeLogLastVersionShown = RegistryHelper.GetString("ChangeLogLastVersionShown", String.Empty)
            AutoStartWithWindows = RegistryHelper.GetBool("AutoStartWithWindows", False)
            MaxClips = RegistryHelper.GetInt("MaxClips", 30)
            MaxClipPreviewLength = RegistryHelper.GetInt("MaxClipPreviewLength", 60)
            BlinkOnNewClip = RegistryHelper.GetBool("BlinkOnNewClip", True)
            NotifyOnNewClip = RegistryHelper.GetBool("NotifyOnNewClip", True)
            PlaySoundWithNotify = RegistryHelper.GetBool("PlaySoundWithNotify", False)
            AutoPurge = RegistryHelper.GetBool("AutoPurge", False)
            LastPurgeDate = RegistryHelper.GetDateTime("LastPurgeDate", DateTime.MinValue)
            PurgeDays = RegistryHelper.GetInt("PurgeDays", 30)
            ShowOpenSourceApp = RegistryHelper.GetBool("ShowOpenSourceApp", False)
            Dim x As Integer = RegistryHelper.GetInt("ScratchPadLocationX", -AdjustScreenBoundsNormalWindow - 1)
            Dim y As Integer = RegistryHelper.GetInt("ScratchPadLocationY", -1)
            ScratchPadLocation = New Point(x, y)
            Dim w As Integer = RegistryHelper.GetInt("ScratchPadSizeW", -1)
            Dim h As Integer = RegistryHelper.GetInt("ScratchPadSizeH", -1)
            ScratchPadSize = New Size(w, h)
            ScratchPadKeepText = RegistryHelper.GetBool("ScratchPadKeepText", False)
            HotKeys.ToggleFavorite = CType(RegistryHelper.GetInt("HotKeyToggleFavorite", CInt(Keys.Space)), Keys)
            HotKeys.ShowViewer = CType(RegistryHelper.GetInt("HotKeyShowViewer", CInt(Keys.V)), Keys)
            HotKeys.ShowScratchPad = CType(RegistryHelper.GetInt("HotKeyShowScratchPad", CInt(Keys.S)), Keys)
            WriteToLog("Settings Loaded (" & Skye.Common.GenerateLogTime(starttime, DateTime.Now.TimeOfDay, True) & ")")
        End Sub
        Friend Shared Sub Save()
            Dim starttime As TimeSpan = DateTime.Now.TimeOfDay
            RegistryHelper.SetString("ChangeLogLastVersionShown", ChangeLogLastVersionShown)
            RegistryHelper.SetBool("AutoStartWithWindows", AutoStartWithWindows)
            RegistryHelper.SetInt("MaxClips", MaxClips)
            RegistryHelper.SetInt("MaxClipPreviewLength", MaxClipPreviewLength)
            RegistryHelper.SetBool("BlinkOnNewClip", BlinkOnNewClip)
            RegistryHelper.SetBool("NotifyOnNewClip", NotifyOnNewClip)
            RegistryHelper.SetBool("PlaySoundWithNotify", PlaySoundWithNotify)
            RegistryHelper.SetBool("AutoPurge", AutoPurge)
            RegistryHelper.SetDateTime("LastPurgeDate", LastPurgeDate)
            RegistryHelper.SetInt("PurgeDays", PurgeDays)
            RegistryHelper.SetBool("ShowOpenSourceApp", ShowOpenSourceApp)
            RegistryHelper.SetInt("ScratchPadLocationX", ScratchPadLocation.X)
            RegistryHelper.SetInt("ScratchPadLocationY", ScratchPadLocation.Y)
            RegistryHelper.SetInt("ScratchPadSizeW", ScratchPadSize.Width)
            RegistryHelper.SetInt("ScratchPadSizeH", ScratchPadSize.Height)
            RegistryHelper.SetBool("ScratchPadKeepText", ScratchPadKeepText)
            RegistryHelper.SetInt("HotKeyToggleFavorite", CInt(HotKeys.ToggleFavorite))
            RegistryHelper.SetInt("HotKeyShowViewer", CInt(HotKeys.ShowViewer))
            RegistryHelper.SetInt("HotKeyShowScratchPad", CInt(HotKeys.ShowScratchPad))
            WriteToLog("Settings Saved (" & Skye.Common.GenerateLogTime(starttime, DateTime.Now.TimeOfDay, True) & ")")
        End Sub

    End Class

    ' WinForms
    Friend Tray As TrayAppContext
    Friend CMTray As ContextMenuStrip
    Private FrmClipExplorer As ClipExplorer
    Private FrmClipViewer As ClipViewer
    Friend FmrScratchPad As ScratchPad
    Private FrmAppView As AppView
    Private FrmSettings As SkyeClip.Settings
    Friend FrmLog As Log
    Private FrmHelp As Help
    Private FrmAbout As About
    Private FrmChangeLog As ChangeLog
    Private FrmDevTools As DevTools

    ' FORMS
    Friend Sub ShowClipExplorer()
        If FrmClipExplorer Is Nothing OrElse FrmClipExplorer.IsDisposed Then
            FrmClipExplorer = New ClipExplorer()
            FrmClipExplorer.Show()
        Else
            FrmClipExplorer.BringToFront()
            FrmClipExplorer.Focus()
        End If
    End Sub
    Friend Sub HideClipExplorer()
        If FrmClipExplorer IsNot Nothing AndAlso Not FrmClipExplorer.IsDisposed Then
            FrmClipExplorer.Close()
        End If
    End Sub
    Friend Sub ShowClipViewer(clipID As Integer, menuBounds As Rectangle, hovered As ToolStripMenuItem, focus As Boolean)
        If FrmClipViewer Is Nothing OrElse FrmClipViewer.IsDisposed Then FrmClipViewer = New ClipViewer()

        Dim screenBounds As Rectangle = Screen.FromPoint(Cursor.Position).WorkingArea
        Dim vw = FrmClipViewer.Width
        Dim vh = FrmClipViewer.Height

        ' Always place viewer to the LEFT of the main menu
        Dim finalX As Integer = menuBounds.Left - vw
        If finalX < screenBounds.Left Then
            finalX = screenBounds.Left
        End If

        ' Align vertically with the hovered item
        Dim itemScreen = hovered.GetCurrentParent().PointToScreen(hovered.Bounds.Location)
        Dim finalY As Integer = itemScreen.Y

        ' Clamp vertically
        If finalY + vh > screenBounds.Bottom Then finalY = screenBounds.Bottom - vh
        If finalY < screenBounds.Top Then finalY = screenBounds.Top

        FrmClipViewer.ShowAtScreenPoint(clipID, New Point(finalX, finalY))
        If focus Then FrmClipViewer.Focus()
    End Sub
    Friend Sub HideClipViewer()
        If FrmClipViewer IsNot Nothing AndAlso Not FrmClipViewer.IsDisposed Then
            If Not FrmClipViewer.Bounds.Contains(Cursor.Position) Then
                FrmClipViewer.fadeInTimer.Stop()
                FrmClipViewer.fadeOutTimer.Stop()
                FrmClipViewer.Close()
            End If
        End If
    End Sub
    Friend Sub ShowScratchPad(ClipID As Integer)
        If FmrScratchPad Is Nothing OrElse FmrScratchPad.IsDisposed Then
            FmrScratchPad = New ScratchPad
        End If
        FmrScratchPad.Show(ClipID)
    End Sub
    Friend Sub HideScratchPad()
        If FmrScratchPad IsNot Nothing AndAlso Not FmrScratchPad.IsDisposed Then
            If Not FmrScratchPad.Bounds.Contains(Cursor.Position) Then
                FmrScratchPad.Close()
            End If
        End If
    End Sub
    Friend Sub ShowAppView()
        If FrmAppView Is Nothing OrElse FrmAppView.IsDisposed Then
            FrmAppView = New AppView()
        End If
        Dim screenPos As Point = Cursor.Position
        FrmAppView.ShowAtScreenPoint(screenPos)
    End Sub
    Friend Sub HideAppView()
        If FrmAppView IsNot Nothing AndAlso Not FrmAppView.IsDisposed Then
            FrmAppView.Close()
        End If
    End Sub
    Friend Sub ShowSettings()
        If FrmSettings Is Nothing OrElse FrmSettings.IsDisposed Then
            FrmSettings = New SkyeClip.Settings()
            FrmSettings.Show()
        Else
            FrmSettings.BringToFront()
            FrmSettings.Focus()
        End If
    End Sub
    Friend Sub HideSettings()
        If FrmSettings IsNot Nothing AndAlso Not FrmSettings.IsDisposed Then
            FrmSettings.Close()
        End If
    End Sub
    Friend Sub ShowLog(Optional refresh As Boolean = False)
        If refresh OrElse FrmLog IsNot Nothing Then
            FrmLog.BringToFront()
            FrmLog.Focus()
        Else
            FrmLog = New Log
            FrmLog.Show()
        End If
        Dim logtext As String = String.Empty
        Dim lines As Integer = 0
        FrmLog.RTBLog.Clear()
        Try
            logtext = IO.File.ReadAllText(LogPath)
        Catch
        End Try
        FrmLog.RTBLog.AppendText(logtext)
        FrmLog.LBLLogInfo.Text = LogPath
        If FrmLog.RTBLog.Lines.Length > 0 AndAlso FrmLog.RTBLog.Lines(0).Length > 0 Then lines = FrmLog.RTBLog.GetLineFromCharIndex(FrmLog.RTBLog.Text.Length)
        FrmLog.LBLLogInfo.Text += " (" + lines.ToString + IIf(lines = 1, " Line", " Lines").ToString + ")"
        If lines > 0 Then
            FrmLog.BTNDeleteLog.Visible = True
            FrmLog.BTNRefreshLog.Visible = True
            FrmLog.RTBLog.ScrollToCaret()
        Else
            FrmLog.BTNDeleteLog.Visible = False
        End If
        FrmLog.BTNOK.Select()
    End Sub
    Friend Sub HideLog()
        If FrmLog IsNot Nothing AndAlso Not FrmLog.IsDisposed Then
            FrmLog.Close()
        End If
    End Sub
    Friend Sub ShowHelp(Optional scrollto As Integer = 0)
        If FrmHelp Is Nothing OrElse FrmHelp.IsDisposed Then
            FrmHelp = New Help()
            FrmHelp.Show()
            FrmHelp.RTxBxHelp.SelectionStart = scrollto
            FrmHelp.RTxBxHelp.ScrollToCaret()
            FrmHelp.RTxBxHelp.Invalidate()
        Else
            FrmHelp.BringToFront()
            FrmHelp.Focus()
        End If
    End Sub
    Friend Sub HideHelp()
        If FrmHelp IsNot Nothing AndAlso Not FrmHelp.IsDisposed Then
            FrmHelp.Close()
        End If
    End Sub
    Friend Sub ShowAbout()
        If FrmAbout Is Nothing OrElse FrmAbout.IsDisposed Then
            FrmAbout = New About()
            FrmAbout.Show()
        Else
            FrmAbout.BringToFront()
            FrmAbout.Focus()
        End If
    End Sub
    Friend Sub HideAbout()
        If FrmAbout IsNot Nothing AndAlso Not FrmAbout.IsDisposed Then
            FrmAbout.Close()
        End If
    End Sub
    Friend Sub ShowChangeLog()
        If FrmChangeLog Is Nothing OrElse FrmChangeLog.IsDisposed Then
            FrmChangeLog = New ChangeLog()
            FrmChangeLog.Show()
        Else
            FrmChangeLog.BringToFront()
            FrmChangeLog.Focus()
        End If
    End Sub
    Friend Sub HideChangeLog()
        If FrmChangeLog IsNot Nothing AndAlso Not FrmChangeLog.IsDisposed Then
            FrmChangeLog.Close()
        End If
    End Sub

    Friend Sub ShowDevTools()
        If FrmDevTools Is Nothing OrElse FrmDevTools.IsDisposed Then
            FrmDevTools = New DevTools With {.WindowState = FormWindowState.Maximized}
            FrmDevTools.Show()
        Else
            If FrmDevTools.WindowState = FormWindowState.Minimized Then FrmDevTools.WindowState = FormWindowState.Maximized
            FrmDevTools.BringToFront()
        End If
    End Sub
    Friend Sub HideDevTools()
        If FrmDevTools IsNot Nothing AndAlso Not FrmDevTools.IsDisposed Then
            FrmDevTools.Close()
        End If
    End Sub

    ' METHODS
    Friend Function BuildLiveClipboardPreview() As String
        Dim data = Clipboard.GetDataObject()
        If data Is Nothing OrElse data.GetFormats().Length = 0 Then
            Return CBEmptyString
        End If

        Dim hasRtf As Boolean = data.GetDataPresent(DataFormats.Rtf)
        Dim hasHtml As Boolean = data.GetDataPresent(DataFormats.Html)

        ' --- TEXT PREVIEW ---
        If data.GetDataPresent(DataFormats.UnicodeText) Then
            Dim raw = CStr(data.GetData(DataFormats.UnicodeText))

            Dim t = raw.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ")
            t = System.Text.RegularExpressions.Regex.Replace(t, "\s+", " ").Trim()
            t = Skye.Common.Trunc(t, App.Settings.MaxClipPreviewLength)

            If String.IsNullOrWhiteSpace(t) Then
                t = "< No Preview >"
            End If

            ' Append format tags
            If hasRtf Then
                t &= CBRTFSuffix
            ElseIf hasHtml Then
                t &= CBHTMLSuffix
            End If

            Return t
        End If

        ' --- IMAGE ---
        If data.GetDataPresent(DataFormats.Bitmap) Then
            Return "< Image >"
        End If

        ' --- FILE DROP ---
        If data.GetDataPresent(DataFormats.FileDrop) Then
            Return BuildFileDropPreview()
        End If

        Return "< Unknown Format >"
    End Function
    Friend Function BuildFileDropPreview() As String
        Dim files As String() = Array.Empty(Of String)()

        If Clipboard.TryGetData(DataFormats.FileDrop, files) Then
            If files Is Nothing Then
                files = Array.Empty(Of String)()
            End If
        End If

        Dim count = files.Length
        If count = 0 Then
            Return "< Empty File Drop >"
        ElseIf count = 1 Then
            Return "< 1 File >"
        Else
            Return $"< {count} Files >"
        End If
    End Function
    Friend Function GetSourceAppInfo() As (AppName As String, IconBytes As Byte(), ExePath As String)
        Dim hwnd = Skye.WinAPI.GetForegroundWindow()
        Dim pid As UInteger
        Dim result As UInteger = Skye.WinAPI.GetWindowThreadProcessId(hwnd, pid)
        If pid = 0 Then Return ("Unknown", Nothing, Nothing)

        Try
            Dim proc = Process.GetProcessById(CInt(pid))
            Dim exePath As String = Nothing

            Try
                exePath = proc.MainModule.FileName
            Catch
                ' Some protected processes throw here
                exePath = Nothing
            End Try

            ' Friendly name
            Dim appName As String = "Unknown"

            If Not String.IsNullOrWhiteSpace(exePath) Then
                appName = Path.GetFileNameWithoutExtension(exePath)

                Dim verInfo = FileVersionInfo.GetVersionInfo(exePath)
                If Not String.IsNullOrWhiteSpace(verInfo.ProductName) Then
                    appName = verInfo.ProductName
                End If
            End If

            ' Extract icon
            Dim iconBytes As Byte() = Nothing

            If Not String.IsNullOrWhiteSpace(exePath) Then
                Try
                    Dim ico = Icon.ExtractAssociatedIcon(exePath)
                    If ico IsNot Nothing Then
                        Using ms As New MemoryStream()
                            ico.ToBitmap().Save(ms, Imaging.ImageFormat.Png)
                            iconBytes = ms.ToArray()
                        End Using
                    End If
                Catch
                    ' Ignore icon extraction failures
                End Try
            End If

            Return (appName, iconBytes, exePath)

        Catch
            Return ("Unknown", Nothing, Nothing)
        End Try
    End Function
    Friend Function GetAssemblyName() As String
        Dim asm = Assembly.GetExecutingAssembly()
        Dim name = asm.GetName().Name

        If Not String.IsNullOrEmpty(name) Then
            Return name
        End If

        ' Fallback: use the EXE filename
        Return IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    End Function
    Friend Function GetAppTitle() As String
        Dim asm = Assembly.GetExecutingAssembly()
        Dim attr = asm.GetCustomAttribute(Of AssemblyTitleAttribute)()

        If attr IsNot Nothing AndAlso Not String.IsNullOrEmpty(attr.Title) Then
            Return attr.Title
        End If

        ' Fallback: use the EXE filename
        Return IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    End Function
    Friend Function GetAppDescription() As String
        Dim asm = Assembly.GetExecutingAssembly()
        Dim attr = asm.GetCustomAttribute(Of AssemblyDescriptionAttribute)()

        If attr IsNot Nothing AndAlso Not String.IsNullOrEmpty(attr.Description) Then
            Return attr.Description
        End If

        ' Fallback: use the EXE filename
        Return IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    End Function
    Friend Function GetSimpleVersion() As String
        Dim ver = Assembly.GetExecutingAssembly().GetName().Version
        GetSimpleVersion = ver.Major.ToString & "." & ver.Minor.ToString
    End Function
    Friend Function IsLegitimateSourceApp(path As String) As Boolean
        If String.IsNullOrWhiteSpace(path) Then Return False
        If Not File.Exists(path) Then Return False

        Dim p = path.ToLower()

        ' Exclude temp EXEs
        If p.Contains("\appdata\local\temp\") Then Return False

        ' Exclude system processes
        Dim win = Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToLower()
        If p.StartsWith(win & "\system32") Then Return False
        If p.StartsWith(win & "\syswow64") Then Return False

        ' Exclude UWP apps
        If p.Contains("\windowsapps\") Then Return False

        Return True
    End Function
    Friend Function NormalizeRtf(rtf As String) As String
        If String.IsNullOrWhiteSpace(rtf) Then
            Return String.Empty
        End If

        Dim cleaned As String = rtf

        ' Remove volatile RTF header metadata (generator, build, timestamps)
        cleaned = Regex.Replace(cleaned,
                            "\\\*?\\generator[^;]*;",
                            "",
                            RegexOptions.IgnoreCase)

        cleaned = Regex.Replace(cleaned,
                            "\\\*?\\creatim[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        cleaned = Regex.Replace(cleaned,
                            "\\\*?\\revtim[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        ' Remove font table (not needed for search)
        cleaned = Regex.Replace(cleaned,
                            "{\\fonttbl[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        ' Remove color table (not needed for search)
        cleaned = Regex.Replace(cleaned,
                            "{\\colortbl[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        ' Remove stylesheet blocks
        cleaned = Regex.Replace(cleaned,
                            "{\\stylesheet[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        ' Remove picture data (huge blobs)
        cleaned = Regex.Replace(cleaned,
                            "{\\pict[^}]*}",
                            "",
                            RegexOptions.IgnoreCase)

        ' Collapse excessive whitespace
        cleaned = Regex.Replace(cleaned,
                            "\s+",
                            " ",
                            RegexOptions.Multiline)

        Return cleaned.Trim()
    End Function
    Friend Sub WriteToLog(message As String)
        If String.IsNullOrWhiteSpace(message) Then Exit Sub
        RotateLogIfNeeded()
        Try
            Dim line As String = $"{Date.Now:yyyy/MM/dd @ HH:mm:ss} --> {message}{Environment.NewLine}"
            IO.File.AppendAllText(LogPath, line)
        Catch ' Logging must never crash the app. Swallowing exceptions here is intentional.
        End Try
    End Sub
    Private Sub RotateLogIfNeeded()
        Try
            Dim fi As New IO.FileInfo(LogPath)
            If fi.Exists AndAlso fi.Length >= 1_000_000 Then
                Dim timestamp As String = Date.Now.ToString("yyyyMMdd@HHmmss")
                Dim backupPath As String = IO.Path.Combine(fi.DirectoryName, $"{IO.Path.GetFileNameWithoutExtension(LogPath)}_Backup@{timestamp}{fi.Extension}")
                IO.File.Move(LogPath, backupPath)
            End If
        Catch ' If rotation fails, ignore — logging must never break the app.
        End Try
    End Sub
    Friend Sub DeleteLog()
        If IO.File.Exists(LogPath) Then IO.File.Delete(LogPath)
        ShowLog(True)
    End Sub
    Friend Sub OpenFileLocation(filename As String)
        Dim psi As New ProcessStartInfo("EXPLORER.EXE") With {
                .Arguments = "/SELECT," + """" + filename + """"}
        Try
            Process.Start(psi)
            WriteToLog("File Location Opened (" + filename + ")")
        Catch ex As Exception
            WriteToLog("Error Opening File Location (" + filename + ")" + vbCr + ex.Message)
        End Try
    End Sub
    Friend Function ResizeImage(src As Image, size As Integer) As Image
        Dim bmp As New Bitmap(size, size)
        Using g = Graphics.FromImage(bmp)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.DrawImage(src, New Rectangle(0, 0, size, size))
        End Using
        Return bmp
    End Function
    Friend Function IsAutoStartEnabled() As Boolean
        Dim runKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
        Dim value As String = RegistryHelper.GetStringFromHKCU(runKey, "SkyeClip", String.Empty)
        Return Not String.IsNullOrEmpty(value)
    End Function
    Friend Sub SetAutoStart()
        If Settings.AutoStartWithWindows Then
            Dim runKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
            Dim exePath As String = """" & Application.ExecutablePath & """"
            RegistryHelper.SetStringInHKCU(runKey, "SkyeClip", exePath)
        Else
            Dim runKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
            RegistryHelper.DeleteValueInHKCU(runKey, "SkyeClip")
        End If
    End Sub

    Private Class RegistryHelper
#If DEBUG Then
        Private Shared ReadOnly BaseKey As String = "Software\" & GetAssemblyName() & "DEV"
#Else
        Private Shared ReadOnly BaseKey As String = "Software\" & GetAssemblyName()
#End If

        Public Shared Function GetInt(name As String, defaultValue As Integer) As Integer
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Return CInt(key.GetValue(name, defaultValue))
            End Using
        End Function
        Public Shared Sub SetInt(name As String, value As Integer)
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, value, RegistryValueKind.DWord)
            End Using
        End Sub

        Public Shared Function GetBool(name As String, defaultValue As Boolean) As Boolean
            Return GetInt(name, If(defaultValue, 1, 0)) = 1
        End Function
        Public Shared Sub SetBool(name As String, value As Boolean)
            SetInt(name, If(value, 1, 0))
        End Sub

        Public Shared Function GetString(name As String, defaultValue As String) As String
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Return CStr(key.GetValue(name, defaultValue))
            End Using
        End Function
        Public Shared Sub SetString(name As String, value As String)
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, value, RegistryValueKind.String)
            End Using
        End Sub

        Public Shared Function GetDateTime(name As String, defaultValue As DateTime) As DateTime
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Dim obj = key.GetValue(name, Nothing)

                If TypeOf obj Is Long Then
                    Return New DateTime(CType(obj, Long))
                End If

                Return defaultValue
            End Using
        End Function
        Public Shared Sub SetDateTime(name As String, value As DateTime)
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, value.Ticks, RegistryValueKind.QWord)
            End Using
        End Sub

        Public Shared Function GetBytes(name As String) As Byte()
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Dim obj = key.GetValue(name)
                If TypeOf obj Is Byte() Then
                    Return CType(obj, Byte())
                End If
                Return Nothing
            End Using
        End Function
        Public Shared Sub SetBytes(name As String, data As Byte())
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, data, RegistryValueKind.Binary)
            End Using
        End Sub

        Public Shared Function GetLong(name As String, defaultValue As Long) As Long
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Dim obj = key.GetValue(name, Nothing)
                If TypeOf obj Is Long Then
                    Return CType(obj, Long)
                End If
                Return defaultValue
            End Using
        End Function
        Public Shared Sub SetLong(name As String, value As Long)
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, value, RegistryValueKind.QWord)
            End Using
        End Sub

        Public Shared Function GetStringArray(name As String, defaultValue As String()) As String()
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                Dim obj = key.GetValue(name, Nothing)
                If TypeOf obj Is String() Then
                    Return CType(obj, String())
                End If
                Return defaultValue
            End Using
        End Function
        Public Shared Sub SetStringArray(name As String, values As String())
            Using key = Registry.CurrentUser.CreateSubKey(BaseKey)
                key.SetValue(name, values, RegistryValueKind.MultiString)
            End Using
        End Sub

        Public Shared Function GetStringFromHKCU(subKey As String, valueName As String, defaultValue As String) As String
            Using key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey, writable:=False)
                If key Is Nothing Then Return defaultValue
                Dim v = key.GetValue(valueName)
                If v Is Nothing Then Return defaultValue
                Return CStr(v)
            End Using
        End Function
        Public Shared Sub SetStringInHKCU(subKey As String, valueName As String, valueData As String)
            Using key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKey)
                key.SetValue(valueName, valueData, Microsoft.Win32.RegistryValueKind.String)
            End Using
        End Sub
        Public Shared Sub DeleteValueInHKCU(subKey As String, valueName As String)
            Using key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey, writable:=True)
                key?.DeleteValue(valueName, throwOnMissingValue:=False)
            End Using
        End Sub

    End Class

End Module
