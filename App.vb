
Imports System.IO
Imports System.IO.Compression
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports Skye.UI
Imports SkyeClip.ClipRepository

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
    Public Class FileDropEntry
        Public Property FullPath As String
        Public Property FileName As String
        Public Property SizeText As String
        Public Property Icon As Icon
    End Class
    Friend Class Hash
        Public Const CurrentHashVersion As Integer = 1
    End Class
    Friend SuppressNextClipboardEvent As Boolean = False
    Friend ReadOnly AdjustScreenBoundsNormalWindow As Byte = 8 'AdjustScreenBoundsNormalWindow is the number of pixels to adjust the screen bounds for normal windows.
    Friend ReadOnly AdjustScreenBoundsDialogWindow As Byte = 10 'AdjustScreenBoundsDialogWindow is the number of pixels to adjust the screen bounds for dialog windows.
    Friend ReadOnly CBEmptyString As String = "< Clipboard Empty >"
    Friend ReadOnly CBNoPreviewString As String = "< No Preview Available >"
    Friend ReadOnly CBUnknownFormatString As String = "< Unknown Format >"
    Friend ReadOnly CBRTFSuffix As String = " <RTF>"
    Friend ReadOnly CBHTMLSuffix As String = " <HTML>"
    Friend ReadOnly MenuFont As New Font("Segoe UI", 10) 'SystemFonts.MenuFont.FontFamily ' MenuFont is the font used for context menus.
    Friend ReadOnly AttributionMicrosoft As String = "https://www.microsoft.com" 'AttributionMicrosoft is the URL for Microsoft, which provides various APIs and libraries used in the application.
    Friend ReadOnly AttributionSQLite As String = "https://www.sqlite.org/index.html" 'AttributionSQLite is the URL for SQLite, which provides database functionality in the application.
    Friend ReadOnly AttributionIcons8 As String = "https://icons8.com/" 'AttributionIcons8 is the URL for Icons8, which provides icons used in the application.
    Friend ReadOnly SponsorGitHub As String = "https://github.com/sponsors/YodeSkye" 'SponsorGitHub is the URL for the GitHub Sponsors page of the application's developer.
    Friend ReadOnly SponsorPayPal As String = "https://www.paypal.com/donate/?hosted_button_id=RVH5T9H69G6CS" 'SponsorPayPal is the URL for the PayPal donation page for the application's developer.
    Friend ScratchPadText As String = String.Empty
    Friend Property ChangeLogLastVersionShown As String = String.Empty
    Friend Property CBLivePreview As String
    Friend Property AppHandle As IntPtr
    Friend Enum AutoBackupFrequency
        Never
        Every_Day
        Every_Other_Day
        Every_Three_Days
        Every_Week
        Every_Other_Week
        Every_Month
    End Enum
    Friend Class AutoBackupFrequencyEnumItem
        Public Property Value As AutoBackupFrequency
        Public Property Text As String
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class
    Friend WithEvents MaintenanceTimer As New Timer() With {
        .Interval = 60000, ' 1 minute
        .Enabled = False
    }

    ' Paths
    Friend ReadOnly UserPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Skye\" 'UserPath is the base path for user-specific files.
#If DEBUG Then
    Private ReadOnly devFileTag As String = "DEV"
    Friend ReadOnly LogPath As String = IO.Path.GetTempPath & GetAssemblyName() & "LogDEV.txt" 'LogPath is the path to the log file.
    Friend ReadOnly DBPath As String = UserPath & Application.ProductName & "ClipboardDEV.db" 'DatabasePath is the path to the SQLite database file.
    Friend ReadOnly ScratchPadPath As String = UserPath & Application.ProductName & "ScratchPadDEV.rtf" 'ScratchPadPath is the path to the ScratchPad KeepText RTF file.
#Else
    Private ReadOnly devFileTag As String = String.Empty
    Friend ReadOnly LogPath As String = IO.Path.GetTempPath & GetAssemblyName() & "Log.txt" 'LogPath is the path to the log file.
    Friend ReadOnly DBPath As String = UserPath & Application.ProductName & "Clipboard.db" 'DatabasePath is the path to the SQLite database file.
    Friend ReadOnly ScratchPadPath As String = UserPath & Application.ProductName & "ScratchPad.rtf" 'ScratchPadPath is the path to the ScratchPad KeepText RTF file.
#End If
    Friend ReadOnly DBConnectionString As String = "Data Source=" & DBPath & ";Version=3;"

    ' Settings
    Friend Class Settings
        Friend Shared ThemeName As String ' the name of the current theme
        Friend Shared ThemeAuto As Boolean ' whether to auto-switch theme based on system settings
        Friend Shared AutoBackup As AutoBackupFrequency ' frequency of automatic backups
        Friend Shared LastAutoBackup As DateTime ' the last date when automatic backup was performed
        Friend Shared AutoBackupPurge As Boolean ' whether to auto-purge old backups
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
        Friend Shared ClipExplorerLocation As Point ' location of the Clip Explorer window
        Friend Shared ClipExplorerSize As Size ' size of the Clip Explorer window
        Friend Shared LastUpdateCheck As DateTime ' the last date when update check was performed
        Friend Shared LatestKnownVersion As String ' the latest known version
        Private Shared _currentProfileID As Integer ' the ID of the currently active profile, used when profiles are enabled. 0 is the default profile.
        Friend Shared Property CurrentProfileID As Integer ' the ID of the currently active profile, used when profiles are enabled. 0 is the default profile.
            Get
                Return _currentProfileID
            End Get
            Set(value As Integer)
                If _currentProfileID <> value Then
                    Debug.Print("Current Profile changed from " & _currentProfileID.ToString & " to " & value.ToString)
                    _currentProfileID = value
                    Skye.Common.RegistryHelper.SetInt("CurrentProfileID", _currentProfileID)
                    Tray.RefreshMenu()
                End If
            End Set
        End Property
        Private Shared _nextProfileID As Integer ' the next profile ID to assign when creating a new profile
        Private Shared _useProfiles As Boolean ' whether to use profiles, which allow for separate clip histories and some settings per profile
        Friend Shared Property UseProfiles As Boolean
            Get
                Return _useProfiles
            End Get
            Set(value As Boolean)
                If _useProfiles <> value Then
                    'Debug.Print("UseProfiles changed from " & _useProfiles.ToString & " to " & value.ToString)
                    _useProfiles = value
                    Skye.Common.RegistryHelper.SetBool("UseProfiles", _useProfiles)
                    If _useProfiles Then
                        ' Turning profiles ON
                        If LastUsedProfileID > 0 Then
                            CurrentProfileID = LastUsedProfileID
                        ElseIf Profiles.Count > 0 Then
                            CurrentProfileID = Profiles(0).ID
                        Else
                            CurrentProfileID = 0
                        End If
                    Else
                        ' Turning profiles OFF
                        LastUsedProfileID = CurrentProfileID
                        Skye.Common.RegistryHelper.SetInt("LastUsedProfileID", LastUsedProfileID)
                        CurrentProfileID = 0
                    End If
                    Tray.RefreshMenu()
                End If
            End Set
        End Property
        Private Shared LastUsedProfileID As Integer
        Friend Shared Profiles As List(Of Profile) ' the list of profiles, used when profiles are enabled. The default profile with ID 0 is not stored in this list but is implicitly available.

        Friend Class HotKeys
            Friend Shared ToggleFavorite As Keys
            Friend Shared ShowViewer As Keys
            Friend Shared ShowScratchPad As Keys
            Friend Shared DevTools As Keys = Keys.Control Or Keys.Shift Or Keys.D 'Not a Saved Setting
        End Class
        Friend Class Profile
            Public Property ID As Integer ' unique identifier for the profile
            Public Property Order As Integer ' sort order of the profile, used only for registry storage and retrieval, do not use for display, rely on the natural sort order of the list.
            Public Property Name As String ' name of the profile
        End Class

        Friend Shared Sub Load()
            Dim starttime As TimeSpan = DateTime.Now.TimeOfDay

            ThemeName = Skye.Common.RegistryHelper.GetString("ThemeName", "Dark")
            ThemeAuto = Skye.Common.RegistryHelper.GetBool("ThemeAuto", False)
            AutoBackup = CType(Skye.Common.RegistryHelper.GetInt("AutoBackup", CInt(AutoBackupFrequency.Never)), AutoBackupFrequency)
            LastAutoBackup = Skye.Common.RegistryHelper.GetDateTime("LastAutoBackup", DateTime.MinValue)
            AutoBackupPurge = Skye.Common.RegistryHelper.GetBool("AutoBackupPurge", True)
            ChangeLogLastVersionShown = Skye.Common.RegistryHelper.GetString("ChangeLogLastVersionShown", String.Empty)
            AutoStartWithWindows = Skye.Common.RegistryHelper.GetBool("AutoStartWithWindows", False)
            MaxClips = Skye.Common.RegistryHelper.GetInt("MaxClips", 30)
            MaxClipPreviewLength = Skye.Common.RegistryHelper.GetInt("MaxClipPreviewLength", 60)
            BlinkOnNewClip = Skye.Common.RegistryHelper.GetBool("BlinkOnNewClip", True)
            NotifyOnNewClip = Skye.Common.RegistryHelper.GetBool("NotifyOnNewClip", True)
            PlaySoundWithNotify = Skye.Common.RegistryHelper.GetBool("PlaySoundWithNotify", False)
            AutoPurge = Skye.Common.RegistryHelper.GetBool("AutoPurge", False)
            LastPurgeDate = Skye.Common.RegistryHelper.GetDateTime("LastPurgeDate", DateTime.MinValue)
            PurgeDays = Skye.Common.RegistryHelper.GetInt("PurgeDays", 30)
            ShowOpenSourceApp = Skye.Common.RegistryHelper.GetBool("ShowOpenSourceApp", False)
            Dim x As Integer = Skye.Common.RegistryHelper.GetInt("ScratchPadLocationX", -AdjustScreenBoundsNormalWindow - 1)
            Dim y As Integer = Skye.Common.RegistryHelper.GetInt("ScratchPadLocationY", -1)
            ScratchPadLocation = New Point(x, y)
            Dim w As Integer = Skye.Common.RegistryHelper.GetInt("ScratchPadSizeW", -1)
            Dim h As Integer = Skye.Common.RegistryHelper.GetInt("ScratchPadSizeH", -1)
            ScratchPadSize = New Size(w, h)
            ScratchPadKeepText = Skye.Common.RegistryHelper.GetBool("ScratchPadKeepText", False)
            x = Skye.Common.RegistryHelper.GetInt("ClipExplorerLocationX", -AdjustScreenBoundsNormalWindow - 1)
            y = Skye.Common.RegistryHelper.GetInt("ClipExplorerLocationY", -1)
            ClipExplorerLocation = New Point(x, y)
            w = Skye.Common.RegistryHelper.GetInt("ClipExplorerSizeW", -1)
            h = Skye.Common.RegistryHelper.GetInt("ClipExplorerSizeH", -1)
            ClipExplorerSize = New Size(w, h)
            LastUpdateCheck = Skye.Common.RegistryHelper.GetDateTime("LastUpdateCheck", DateTime.MinValue)
            LatestKnownVersion = Skye.Common.RegistryHelper.GetString("LatestKnownVersion", String.Empty)

            ' Profiles
            _currentProfileID = Skye.Common.RegistryHelper.GetInt("CurrentProfileID", 0)
            _nextProfileID = Skye.Common.RegistryHelper.GetInt("NextProfileID", 1)
            _useProfiles = Skye.Common.RegistryHelper.GetBool("UseProfiles", False)
            LastUsedProfileID = Skye.Common.RegistryHelper.GetInt("LastUsedProfileID", 0)
            Dim valueNames = Skye.Common.RegistryHelper.GetValuesWithPrefix("Profile")
            Dim profiles As New List(Of Profile)
            For Each key In valueNames
                If key.StartsWith("Profile") AndAlso key.EndsWith("ID") Then
                    ' Extract the numeric ID between "Profile" and "ID"
                    Dim idPart = key.Substring("Profile".Length, key.Length - "Profile".Length - "ID".Length)
                    Dim id As Integer
                    If Integer.TryParse(idPart, id) Then
                        ' Load order (default to 9999 for old installs)
                        Dim orderKey = "Profile" & id & "Order"
                        Dim order = Skye.Common.RegistryHelper.GetInt(orderKey, 9999)
                        ' Load name
                        Dim nameKey = "Profile" & id & "Name"
                        Dim name = Skye.Common.RegistryHelper.GetString(nameKey, "")
                        ' Reconstruct the profile object
                        Dim p As New Profile With {
                            .ID = id,
                            .Order = order,
                            .Name = name
                        }
                        profiles.Add(p)
                    End If
                End If
            Next
            ' Sort by saved order
            profiles = profiles.OrderBy(Function(p) p.Order).ToList()
            Settings.Profiles = profiles

            ' HotKeys
            HotKeys.ToggleFavorite = CType(Skye.Common.RegistryHelper.GetInt("HotKeyToggleFavorite", CInt(Keys.F)), Keys)
            HotKeys.ShowViewer = CType(Skye.Common.RegistryHelper.GetInt("HotKeyShowViewer", CInt(Keys.V)), Keys)
            HotKeys.ShowScratchPad = CType(Skye.Common.RegistryHelper.GetInt("HotKeyShowScratchPad", CInt(Keys.S)), Keys)

            WriteToLog("Settings Loaded (" & Skye.Common.GenerateLogTime(starttime, DateTime.Now.TimeOfDay, True) & ")")
        End Sub
        Friend Shared Sub Save()
            Dim starttime As TimeSpan = DateTime.Now.TimeOfDay
            Skye.Common.RegistryHelper.SetString("ThemeName", ThemeName)
            Skye.Common.RegistryHelper.SetBool("ThemeAuto", ThemeAuto)
            Skye.Common.RegistryHelper.SetInt("AutoBackup", CInt(AutoBackup))
            Skye.Common.RegistryHelper.SetDateTime("LastAutoBackup", LastAutoBackup)
            Skye.Common.RegistryHelper.SetBool("AutoBackupPurge", AutoBackupPurge)
            Skye.Common.RegistryHelper.SetString("ChangeLogLastVersionShown", ChangeLogLastVersionShown)
            Skye.Common.RegistryHelper.SetBool("AutoStartWithWindows", AutoStartWithWindows)
            Skye.Common.RegistryHelper.SetInt("MaxClips", MaxClips)
            Skye.Common.RegistryHelper.SetInt("MaxClipPreviewLength", MaxClipPreviewLength)
            Skye.Common.RegistryHelper.SetBool("BlinkOnNewClip", BlinkOnNewClip)
            Skye.Common.RegistryHelper.SetBool("NotifyOnNewClip", NotifyOnNewClip)
            Skye.Common.RegistryHelper.SetBool("PlaySoundWithNotify", PlaySoundWithNotify)
            Skye.Common.RegistryHelper.SetBool("AutoPurge", AutoPurge)
            Skye.Common.RegistryHelper.SetDateTime("LastPurgeDate", LastPurgeDate)
            Skye.Common.RegistryHelper.SetInt("PurgeDays", PurgeDays)
            Skye.Common.RegistryHelper.SetBool("ShowOpenSourceApp", ShowOpenSourceApp)
            Skye.Common.RegistryHelper.SetInt("ScratchPadLocationX", ScratchPadLocation.X)
            Skye.Common.RegistryHelper.SetInt("ScratchPadLocationY", ScratchPadLocation.Y)
            Skye.Common.RegistryHelper.SetInt("ScratchPadSizeW", ScratchPadSize.Width)
            Skye.Common.RegistryHelper.SetInt("ScratchPadSizeH", ScratchPadSize.Height)
            Skye.Common.RegistryHelper.SetBool("ScratchPadKeepText", ScratchPadKeepText)
            Skye.Common.RegistryHelper.SetInt("ClipExplorerLocationX", ClipExplorerLocation.X)
            Skye.Common.RegistryHelper.SetInt("ClipExplorerLocationY", ClipExplorerLocation.Y)
            Skye.Common.RegistryHelper.SetInt("ClipExplorerSizeW", ClipExplorerSize.Width)
            Skye.Common.RegistryHelper.SetInt("ClipExplorerSizeH", ClipExplorerSize.Height)
            Skye.Common.RegistryHelper.SetDateTime("LastUpdateCheck", LastUpdateCheck)
            Skye.Common.RegistryHelper.SetString("LatestKnownVersion", LatestKnownVersion)

            ' Profiles
            Skye.Common.RegistryHelper.SetInt("CurrentProfileID", _currentProfileID)
            Skye.Common.RegistryHelper.SetInt("NextProfileID", _nextProfileID)
            Skye.Common.RegistryHelper.SetBool("UseProfiles", _useProfiles)
            Skye.Common.RegistryHelper.SetInt("LastUsedProfileID", LastUsedProfileID)
            Dim order As Integer = 0
            For Each profile In Profiles
                profile.Order = order
                Skye.Common.RegistryHelper.SetInt("Profile" & profile.ID & "ID", profile.ID)
                Skye.Common.RegistryHelper.SetInt("Profile" & profile.ID & "Order", profile.Order)
                Skye.Common.RegistryHelper.SetString("Profile" & profile.ID & "Name", profile.Name)
                order += 1
            Next

            ' HotKeys
            Skye.Common.RegistryHelper.SetInt("HotKeyToggleFavorite", CInt(HotKeys.ToggleFavorite))
            Skye.Common.RegistryHelper.SetInt("HotKeyShowViewer", CInt(HotKeys.ShowViewer))
            Skye.Common.RegistryHelper.SetInt("HotKeyShowScratchPad", CInt(HotKeys.ShowScratchPad))
            WriteToLog("Settings Saved (" & Skye.Common.GenerateLogTime(starttime, DateTime.Now.TimeOfDay, True) & ")")
        End Sub
        Friend Shared Function GetNextProfileID() As Integer
            Dim id = _nextProfileID
            _nextProfileID += 1
            Skye.Common.RegistryHelper.SetInt("NextProfileID", _nextProfileID)
            Return id
        End Function
        Public Shared Function GetProfileName(id As Integer) As String
            If id = 0 Then Return "No Profile"
            Dim p = Settings.Profiles.FirstOrDefault(Function(x) x.ID = id)
            Return If(p IsNot Nothing, p.Name, "Unknown Profile")
        End Function
        Friend Shared Function DeleteProfile(profileID As Integer) As Boolean
            Try
                ' Remove from registry
                Skye.Common.RegistryHelper.DeleteValue("Profile" & profileID & "ID")
                Skye.Common.RegistryHelper.DeleteValue("Profile" & profileID & "Order")
                Skye.Common.RegistryHelper.DeleteValue("Profile" & profileID & "Name")
                ' Remove from in-memory list
                Dim profileToRemove = Profiles.FirstOrDefault(Function(p) p.ID = profileID)
                If profileToRemove IsNot Nothing Then
                    Profiles.Remove(profileToRemove)
                End If
                ' If the deleted profile was the current one, switch to first in list or default
                If CurrentProfileID = profileID Then
                    CurrentProfileID = If(Profiles.Count > 0, Profiles(0).ID, 0)
                    If CurrentProfileID = 0 Then _useProfiles = False
                End If
                Return True
            Catch ex As Exception
                App.WriteToLog("Error Deleting Profile ID " & profileID.ToString & ": " & ex.Message)
                Return False
            End Try
        End Function

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

    ' HANDLERS
    Private Sub MaintenanceTimer_Tick(sender As Object, e As EventArgs) Handles MaintenanceTimer.Tick

        ' ---------------------------------------------------------
        ' Auto Purge old clips once per day
        ' ---------------------------------------------------------
        Try
            If App.Settings.AutoPurge Then
                If Date.Today > App.Settings.LastPurgeDate Then
                    Dim cutoff = DateTime.Now.AddDays(-App.Settings.PurgeDays)

                    Tray.repo.PurgeClips(cutoff)
                    App.Settings.LastPurgeDate = Date.Today
                    App.Settings.Save()

                    App.Tray.ShowToast("Daily purge completed")
                End If
            End If
        Catch ex As Exception
            WriteToLog("Auto Clip Purge Execution Error: " & ex.Message)
        End Try


        ' ---------------------------------------------------------
        ' Auto Backup based on frequency enum
        ' ---------------------------------------------------------
        Try
            Dim freq = App.Settings.AutoBackup
            If freq <> AutoBackupFrequency.Never Then
                Dim last = App.Settings.LastAutoBackup
                Dim nextTime = GetNextAutoBackupTime(last, freq)
                If DateTime.Now >= nextTime Then

                    BackupAuto()
                    App.Settings.LastAutoBackup = DateTime.Now
                    App.Settings.Save()

                End If
            End If
        Catch ex As Exception
            App.WriteToLog("Auto Backup Execution Error: " & ex.Message)
        End Try

    End Sub

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
        Dim finalX As Integer
        If menuBounds = Nothing Then
            finalX = Cursor.Position.X - vw
        Else
            finalX = menuBounds.Left - vw
        End If
        ' Clamp horizontally
        If finalX < screenBounds.Left Then
            finalX = screenBounds.Left
        End If

        ' Align vertically with the hovered item
        Dim finalY As Integer
        If hovered Is Nothing Then
            finalY = Cursor.Position.Y
        Else
            Dim itemScreen = hovered.GetCurrentParent().PointToScreen(hovered.Bounds.Location)
            finalY = itemScreen.Y
        End If
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

    ' EXPORT TO FILE
    Friend Sub ExportClipToFile(clipId As Integer)
        Dim formats = App.Tray.repo.GetClipFormats(clipId)
        If formats Is Nothing OrElse formats.Count = 0 Then
            MessageBox.Show("This clip has no exportable formats.")
            Exit Sub
        End If

        Dim exportOptions = BuildExportOptions(formats)
        If exportOptions.Count = 0 Then
            MessageBox.Show("No supported export formats found.")
            Exit Sub
        End If

        Dim sfd As New SaveFileDialog With {
            .Title = "Export Clip",
            .Filter = BuildFilterString(exportOptions),
            .FileName = "Clip_" & clipId
        }

        If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

        Dim chosenExt = Path.GetExtension(sfd.FileName).ToLower()
        ExportByExtension(formats, chosenExt, sfd.FileName)
    End Sub
    Private Function BuildExportOptions(formats As List(Of ClipData)) As Dictionary(Of String, String)
        Dim opts As New Dictionary(Of String, String)

        If HasFormat(formats, "HTML Format") Then
            opts(".html") = "HTML File (*.html)|*.html"
        End If

        If HasFormat(formats, "Rich Text Format") Then
            opts(".rtf") = "Rich Text (*.rtf)|*.rtf"
        End If

        If HasFormat(formats, "UnicodeText") OrElse HasFormat(formats, "Text") Then
            opts(".txt") = "Plain Text (*.txt)|*.txt"
        End If

        If HasFormat(formats, "PNG") Then
            opts(".png") = "PNG Image (*.png)|*.png"
        End If

        If HasFormat(formats, "JFIF") OrElse HasFormat(formats, "JPEG") Then
            opts(".jpg") = "JPEG Image (*.jpg)|*.jpg"
        End If

        If HasFormat(formats, "Bitmap") Then
            opts(".bmp") = "Bitmap Image (*.bmp)|*.bmp"
        End If

        If formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_DIB OrElse
                          f.FormatId = Skye.WinAPI.CF_DIBV5 OrElse
                          f.FormatName.Contains("DIB", StringComparison.OrdinalIgnoreCase)) Then
            opts(".jpg") = "JPEG Image (*.jpg)|*.jpg"
            opts(".png") = "PNG Image (*.png)|*.png"
            opts(".bmp") = "Bitmap Image (*.bmp)|*.bmp"
        End If

        If HasFormat(formats, "FileDrop") Then
            opts(".zip") = "ZIP Archive (*.zip)|*.zip"
        End If

        If opts.Count = 0 Then
            opts(".bin") = "Binary Data (*.bin)|*.bin"
        End If

        Return opts
    End Function
    Private Function BuildFilterString(opts As Dictionary(Of String, String)) As String
        Return String.Join("|", opts.Values)
    End Function
    Private Sub ExportByExtension(formats As List(Of ClipData), ext As String, outPath As String)
        Select Case ext
            Case ".html"
                File.WriteAllBytes(outPath, GetFormatBytes(formats, "HTML Format"))
            Case ".rtf"
                File.WriteAllBytes(outPath, GetFormatBytes(formats, "Rich Text Format"))
            Case ".txt"
                File.WriteAllBytes(outPath, GetBestTextBytes(formats))
            Case ".png"
                Dim img = ResolveImageBytes(formats)
                If img.bytes Is Nothing Then Exit Select

                If img.kind = "png" Then
                    File.WriteAllBytes(outPath, img.bytes)
                Else
                    Using bmp = If(img.kind = "dib",
                    DIBToBitmap(img.bytes),
                    New Bitmap(New MemoryStream(img.bytes)))
                        Using safeBmp = NormalizeBitmap(bmp)
                            safeBmp.Save(outPath, Imaging.ImageFormat.Png)
                        End Using
                    End Using
                End If
            Case ".jpg"
                Dim img = ResolveImageBytes(formats)
                If img.bytes Is Nothing Then Exit Select

                If img.kind = "jpg" Then
                    File.WriteAllBytes(outPath, img.bytes)
                Else
                    Using bmp = If(img.kind = "dib",
                    DIBToBitmap(img.bytes),
                    New Bitmap(New MemoryStream(img.bytes)))
                        Using safeBmp = NormalizeBitmap(bmp)
                            safeBmp.Save(outPath, Imaging.ImageFormat.Jpeg)
                        End Using
                    End Using
                End If
            Case ".bmp"
                Dim img = ResolveImageBytes(formats)
                If img.bytes Is Nothing Then Exit Select

                If img.kind = "bmp" Then
                    File.WriteAllBytes(outPath, img.bytes)
                Else
                    Using bmp = If(img.kind = "dib",
                    DIBToBitmap(img.bytes),
                    New Bitmap(New MemoryStream(img.bytes)))
                        Using safeBmp = NormalizeBitmap(bmp)
                            safeBmp.Save(outPath, Imaging.ImageFormat.Bmp)
                        End Using
                    End Using
                End If
            Case ".zip"
                Task.Run(Sub()
                             Try
                                 ExportFileDropAsZip(formats, outPath)
                                 App.Tray.ShowToast("Export Complete, Your ZIP File Is Ready.")
                             Catch ex As Exception
                                 App.Tray.ShowToast("Clip Export Failed.")
                                 App.WriteToLog("Clip Export Failed: " & ex.Message)
                             End Try
                         End Sub)
            Case ".bin"
                File.WriteAllBytes(outPath, formats(0).DataBytes)
        End Select
    End Sub
    Private Function HasFormat(formats As List(Of ClipData), name As String) As Boolean
        Return formats.Any(Function(f) f.FormatName.Equals(name, StringComparison.OrdinalIgnoreCase))
    End Function
    Private Function GetFormatBytes(formats As List(Of ClipData), name As String) As Byte()
        Dim f = formats.FirstOrDefault(Function(x) x.FormatName.Equals(name, StringComparison.OrdinalIgnoreCase))
        If f Is Nothing Then Return Nothing
        Return f.DataBytes
    End Function
    Private Function GetBestTextBytes(formats As List(Of ClipData)) As Byte()
        Dim uni = formats.FirstOrDefault(Function(f) f.FormatName = "UnicodeText")
        If uni IsNot Nothing Then Return uni.DataBytes

        Dim txt = formats.FirstOrDefault(Function(f) f.FormatName = "Text")
        If txt IsNot Nothing Then Return txt.DataBytes

        Return Encoding.UTF8.GetBytes("") ' fallback
    End Function
    Private Function ResolveImageBytes(formats As List(Of ClipData)) As (kind As String, bytes As Byte())
        ' 1. Prefer real PNG
        Dim png = formats.FirstOrDefault(Function(f) f.FormatName.Contains("PNG", StringComparison.OrdinalIgnoreCase))
        If png IsNot Nothing Then Return ("png", png.DataBytes)

        ' 2. Prefer real JPEG/JFIF
        Dim jpg = formats.FirstOrDefault(Function(f) f.FormatName.Contains("JFIF", StringComparison.OrdinalIgnoreCase) _
                                   OrElse f.FormatName.Contains("JPEG", StringComparison.OrdinalIgnoreCase))
        If jpg IsNot Nothing Then Return ("jpg", jpg.DataBytes)

        ' 3. Prefer real BMP
        Dim bmp = formats.FirstOrDefault(Function(f) f.FormatName.Contains("Bitmap", StringComparison.OrdinalIgnoreCase))
        If bmp IsNot Nothing Then Return ("bmp", bmp.DataBytes)

        ' 4. Fall back to DIB / DIBV5
        Dim dib = formats.FirstOrDefault(Function(f) _
        f.FormatName.Contains("DIB", StringComparison.OrdinalIgnoreCase) _
        OrElse f.FormatId = Skye.WinAPI.CF_DIB _
        OrElse f.FormatId = Skye.WinAPI.CF_DIBV5)

        If dib IsNot Nothing Then Return ("dib", dib.DataBytes)

        ' 5. Nothing found
        Return (Nothing, Nothing)
    End Function
    Friend Function DIBToBitmap(dibBytes As Byte()) As Bitmap
        Dim headerSize As Integer = BitConverter.ToInt32(dibBytes, 0)

        ' Validate header
        If headerSize < 40 OrElse headerSize > dibBytes.Length Then
            Throw New Exception("Invalid DIB header.")
        End If

        ' Pixel data offset = 14 + headerSize
        Dim pixelOffset As Integer = 14 + headerSize

        Using ms As New MemoryStream()
            ' Build BMP header
            Dim fileHeader(13) As Byte

            ' Signature "BM"
            fileHeader(0) = &H42
            fileHeader(1) = &H4D

            ' File size
            Dim fileSize As Integer = dibBytes.Length + 14
            BitConverter.GetBytes(fileSize).CopyTo(fileHeader, 2)

            ' Reserved (4 bytes) = 0

            ' Pixel data offset
            BitConverter.GetBytes(pixelOffset).CopyTo(fileHeader, 10)

            ' Write BMP header + DIB
            ms.Write(fileHeader, 0, fileHeader.Length)
            ms.Write(dibBytes, 0, dibBytes.Length)
            ms.Position = 0

            Return CType(Bitmap.FromStream(ms), Bitmap)
        End Using
    End Function
    Private Function NormalizeBitmap(bmp As Bitmap) As Bitmap
        Dim safeBmp As New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format24bppRgb)
        Using g = Graphics.FromImage(safeBmp)
            g.DrawImage(bmp, 0, 0)
        End Using
        Return safeBmp
    End Function
    Private Sub ExportFileDropAsZip(formats As List(Of ClipData), outPath As String)
        Dim fd = formats.FirstOrDefault(Function(f) f.FormatName = "FileDrop")
        If fd Is Nothing Then Exit Sub

        Dim paths = DecodeFileDrop(fd.DataBytes)
        If paths Is Nothing OrElse paths.Length = 0 Then Exit Sub

        If File.Exists(outPath) Then File.Delete(outPath)

        Using zip As ZipArchive = ZipFile.Open(outPath, ZipArchiveMode.Create)
            For Each p In paths
                If File.Exists(p) Then
                    zip.CreateEntryFromFile(p, Path.GetFileName(p))
                ElseIf Directory.Exists(p) Then
                    AddFolderToZip(zip, p, Path.GetFileName(p))
                End If
            Next
        End Using
    End Sub
    Private Sub AddFolderToZip(zip As ZipArchive, folderPath As String, entryRoot As String)
        ' Add an empty directory entry (optional but nice)
        zip.CreateEntry(entryRoot & "/")

        For Each file In Directory.GetFiles(folderPath)
            zip.CreateEntryFromFile(file, entryRoot & "/" & Path.GetFileName(file))
        Next

        For Each d In Directory.GetDirectories(folderPath)
            AddFolderToZip(zip, d, entryRoot & "/" & Path.GetFileName(d))
        Next
    End Sub

    ' BACKUP SYSTEM
    Friend Sub BackupManual()
        Dim fileName As String = $"{Application.ProductName}{devFileTag}_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db"
        Dim path As String = IO.Path.Combine(App.UserPath, fileName)
        Try
            File.Copy(App.DBPath, path, overwrite:=True)
            Tray.ShowToast("Backup Completed Successfully")
            WriteToLog("Manual Backup Successful: " & path)
        Catch ex As Exception
            WriteToLog("Manual Backup Failed: " & path & vbCr & ex.Message)
        End Try
    End Sub
    Friend Sub BackupAuto()
        Dim fileName As String = $"{Application.ProductName}{devFileTag}_AutoBackup_{DateTime.Now:yyyyMMdd_HHmmss}.db"
        Dim path As String = IO.Path.Combine(App.UserPath, fileName)
        Try
            File.Copy(App.DBPath, path, overwrite:=True)
            If App.Settings.AutoBackupPurge Then
                PurgeOldAutoBackups()
            End If
            WriteToLog("Auto Backup Successful: " & path)
        Catch ex As Exception
            WriteToLog("Auto Backup Failed: " & path & vbCr & ex.Message)
        End Try
    End Sub
    Private Sub PurgeOldAutoBackups()
        Try
            Dim files = Directory.GetFiles(App.UserPath, $"{Application.ProductName}{devFileTag}_AutoBackup_*.db").OrderByDescending(Function(f) File.GetCreationTime(f)).ToList()
            If files.Count > 10 Then
                For Each f In files.Skip(10)
                    File.Delete(f)
                Next
                WriteToLog("Auto Backup Purge Completed.")
            End If
        Catch ex As Exception
            WriteToLog("Auto Backup Purge Failed." & vbCr & ex.Message)
        End Try
    End Sub
    Friend Sub RestoreBackup(backupPath As String)
        Try

            ' 1. Overwrite current database
            File.Copy(backupPath, App.DBPath, overwrite:=True)

            ' 2. Recreate the repository so migrations run
            Tray.repo = New ClipRepository()
            Tray.UpdateUI(False)

            Tray.ShowToast("Backup Restored Successfully")
            WriteToLog("Backup successfully restored from: " & backupPath)
        Catch ex As Exception
            WriteToLog("Failed to restore backup from: " & backupPath & vbCr & ex.Message)
        End Try
    End Sub
    Private Function GetNextAutoBackupTime(last As DateTime, freq As AutoBackupFrequency) As DateTime
        Select Case freq
            Case AutoBackupFrequency.Never
                Return DateTime.MaxValue

            Case AutoBackupFrequency.Every_Day
                Return last.AddDays(1)

            Case AutoBackupFrequency.Every_Other_Day
                Return last.AddDays(2)

            Case AutoBackupFrequency.Every_Three_Days
                Return last.AddDays(3)

            Case AutoBackupFrequency.Every_Week
                Return last.AddDays(7)

            Case AutoBackupFrequency.Every_Other_Week
                Return last.AddDays(14)

            Case AutoBackupFrequency.Every_Month
                Return last.AddMonths(1)

            Case Else
                Return DateTime.MaxValue
        End Select
    End Function

    ' METHODS
    Friend Function BuildLiveClipboardPreview() As String
        Dim data = Clipboard.GetDataObject()
        If data Is Nothing OrElse data.GetFormats().Length = 0 Then
            Return App.CBEmptyString
        End If

        Dim formats = ClipboardWin32.CaptureFormatsFromClipboard()
        ' If Win32 capture sees nothing, but DataObject had formats,
        ' treat it as unknown / unsupported, NOT clipboard empty.
        If formats Is Nothing OrElse formats.Count = 0 Then
            Return App.CBUnknownFormatString
        End If

        Return BuildPreviewFromFormats(formats)

        'Dim formats = ClipboardWin32.CaptureFormatsFromClipboard()
        'Return BuildPreviewFromFormats(formats)

        'Dim data = Clipboard.GetDataObject()
        'If data Is Nothing OrElse data.GetFormats().Length = 0 Then
        '    Return CBEmptyString
        'End If

        'Dim hasRtf As Boolean = data.GetDataPresent(DataFormats.Rtf)
        'Dim hasHtml As Boolean = data.GetDataPresent(DataFormats.Html)

        '' --- TEXT PREVIEW ---
        'If data.GetDataPresent(DataFormats.UnicodeText) Then
        '    Dim raw = CStr(data.GetData(DataFormats.UnicodeText))

        '    Dim t = raw.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ")
        '    t = System.Text.RegularExpressions.Regex.Replace(t, "\s+", " ").Trim()
        '    t = Skye.Common.Trunc(t, App.Settings.MaxClipPreviewLength)

        '    If String.IsNullOrWhiteSpace(t) Then
        '        t = "< No Preview >"
        '    End If

        '    ' Append format tags
        '    If hasRtf Then
        '        t &= CBRTFSuffix
        '    ElseIf hasHtml Then
        '        t &= CBHTMLSuffix
        '    End If

        '    Return t
        'End If

        '' --- IMAGE ---
        'If data.GetDataPresent(DataFormats.Bitmap) Then
        '    Return "< Image >"
        'End If

        '' --- FILE DROP ---
        'If data.GetDataPresent(DataFormats.FileDrop) Then
        '    Return BuildFileDropPreview()
        'End If

        'Return "< Unknown Format >"
    End Function
    Friend Function BuildPreviewFromFormats(formats As List(Of ClipData)) As String
        If formats Is Nothing OrElse formats.Count = 0 Then
            Return App.CBNoPreviewString
        End If

        ' 1. TEXT (Unicode)
        Dim uni = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_UNICODETEXT)
        If uni IsNot Nothing Then
            Return BuildTextPreview(formats, uni)
        End If

        ' 2. FILE DROP
        Dim filedrop = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_HDROP)
        If filedrop IsNot Nothing Then
            Return BuildFileDropPreview()
        End If

        ' 3. IMAGE
        Dim dib = formats.FirstOrDefault(Function(f) f.FormatId = Skye.WinAPI.CF_DIB OrElse f.FormatId = Skye.WinAPI.CF_DIBV5)
        If dib IsNot Nothing Then
            Return "< Image >"
        End If

        ' 4. FALLBACK: we have data, but nothing we know how to preview
        Dim f0 = formats.First()
        Return If(String.IsNullOrWhiteSpace(f0.FormatName), App.CBUnknownFormatString, f0.FormatName)


        'Dim f0 = formats.First()
        'Return If(String.IsNullOrWhiteSpace(f0.FormatName), $"Format {f0.FormatId}", f0.FormatName)
    End Function
    Private Function BuildTextPreview(formats As List(Of ClipData), uni As ClipData) As String
        Dim s = Encoding.Unicode.GetString(Skye.Common.TrimUnicodeNull(uni.DataBytes))

        ' Normalize whitespace
        s = s.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ")
        s = System.Text.RegularExpressions.Regex.Replace(s, "\s+", " ").TrimEnd()

        If String.IsNullOrWhiteSpace(s) Then
            Return "< No Preview >"
        End If

        ' Truncate
        Dim preview = Skye.Common.Trunc(s, App.Settings.MaxClipPreviewLength)

        ' Detect RTF/HTML
        'Dim hasRtf As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_RTF OrElse (f.FormatName & "").ToLower().Contains("rtf"))
        'Dim hasHtml As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_HTML OrElse (f.FormatName & "").ToLower().Contains("html"))
        Dim hasRtf As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_RTF OrElse (f.FormatName & "").Contains("rtf", StringComparison.OrdinalIgnoreCase))
        Dim hasHtml As Boolean = formats.Any(Function(f) f.FormatId = Skye.WinAPI.CF_HTML OrElse (f.FormatName & "").Contains("html", StringComparison.OrdinalIgnoreCase))
        If hasRtf Then preview &= App.CBRTFSuffix
        If hasHtml Then preview &= App.CBHTMLSuffix

        Return preview
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
            Return $"< 1 File >"
        Else
            Return $"< {count} Files >"
        End If
    End Function
    Private Function DecodeFileDrop(bytes As Byte()) As String()
        ' Convert raw bytes to UTF-16 string
        Dim txt As String = System.Text.Encoding.Unicode.GetString(bytes)

        ' Remove trailing nulls (there can be MANY)
        txt = txt.TrimEnd(ControlChars.NullChar)

        ' Split on single null
        Dim parts = txt.Split(ControlChars.NullChar)

        ' Filter out garbage entries
        Dim files = parts.
        Where(Function(p) Not String.IsNullOrWhiteSpace(p)).
        Where(Function(p) p.IndexOfAny(Path.GetInvalidPathChars()) = -1).
        ToArray()

        Return files
    End Function
    Private Function GetFolderIcon(path As String) As Icon
        Dim shinfo As New Skye.WinAPI.SHFILEINFO()
        Dim hImg = Skye.WinAPI.SHGetFileInfo(path, 0, shinfo, Marshal.SizeOf(shinfo), Skye.WinAPI.SHGFI_ICON Or Skye.WinAPI.SHGFI_SMALLICON)
        If hImg <> IntPtr.Zero AndAlso shinfo.hIcon <> IntPtr.Zero Then
            Return Icon.FromHandle(shinfo.hIcon)
        End If
        Return Nothing
    End Function
    Friend Function ParseFileDrop(bytes As Byte()) As List(Of FileDropEntry)
        Dim files = DecodeFileDrop(bytes)
        Dim list As New List(Of FileDropEntry)

        For Each path In files
            Dim entry As New FileDropEntry With {
            .FullPath = path,
            .FileName = IO.Path.GetFileName(path)
        }

            If File.Exists(path) Then
                Dim size = New FileInfo(path).Length
                entry.SizeText = Skye.Common.FormatFileSize(size, Skye.Common.FormatFileSizeUnits.Auto)
            Else
                entry.SizeText = ""
            End If

            Try
                If Directory.Exists(path) Then
                    entry.Icon = GetFolderIcon(path)
                ElseIf File.Exists(path) Then
                    entry.Icon = Icon.ExtractAssociatedIcon(path)
                End If
            Catch
            End Try

            list.Add(entry)
        Next

        Return list
    End Function
    Public Async Function ComputeFolderSizesAsync(entries As List(Of FileDropEntry)) As Task(Of Long)
        Dim folderPaths = entries.
            Where(Function(e) Directory.Exists(e.FullPath)).
            Select(Function(e) e.FullPath).
            ToList()

        Dim total As Long = 0

        For Each folder In folderPaths
            total += Await Task.Run(Function() GetFolderSize(folder))
        Next

        Return total
    End Function
    Private Function GetFolderSize(path As String) As Long
        Dim total As Long = 0

        Try
            For Each file In Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                Try
                    total += New FileInfo(file).Length
                Catch
                End Try
            Next
        Catch
        End Try

        Return total
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
    Friend Function GetFullVersion() As String
        Dim ver = Assembly.GetExecutingAssembly().GetName().Version
        GetFullVersion = ver.Major.ToString & "." & ver.Minor.ToString & "." & ver.Build.ToString
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
        Dim value As String = Skye.Common.RegistryHelper.GetStringFromHKCU(runKey, "SkyeClip", String.Empty)
        Return Not String.IsNullOrEmpty(value)
    End Function
    Friend Sub SetAutoStart()
        If Settings.AutoStartWithWindows Then
            Dim runKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
            Dim exePath As String = """" & Application.ExecutablePath & """"
            Skye.Common.RegistryHelper.SetStringInHKCU(runKey, "SkyeClip", exePath)
        Else
            Dim runKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
            Skye.Common.RegistryHelper.DeleteValueInHKCU(runKey, "SkyeClip")
        End If
    End Sub
    Friend Function DetectWindowsTheme() As SkyeTheme
        Const keyPath As String = "Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"
        Using key = Registry.CurrentUser.OpenSubKey(keyPath)
            Dim light As Integer = CInt(key.GetValue("AppsUseLightTheme", 1))
            If light = 1 Then
                Return Skye.UI.SkyeThemes.Light
            Else
                Return Skye.UI.SkyeThemes.Dark
            End If
        End Using
    End Function
    Friend Sub CheckForUpdatesIfNeeded()
        Dim last = App.Settings.LastUpdateCheck.Date
        Dim today = Date.Today

        If last = today Then
            ' Already checked today — use cached version
            Exit Sub
        End If

        ' Not checked today — fetch fresh version
        Dim latest = FetchLatestVersion()
        If latest IsNot Nothing Then
            App.Settings.LatestKnownVersion = latest
            App.Settings.LastUpdateCheck = DateTime.Now
            App.Settings.Save()
        End If

    End Sub
    Private Function FetchLatestVersion() As String
        Try
            Using client As New Net.Http.HttpClient()
                client.DefaultRequestHeaders.UserAgent.ParseAdd("SkyeClip")
                Dim versionText As String = client.GetStringAsync("https://raw.githubusercontent.com/yodeskye/SkyeClip/master/publishedversion.txt").Result
                Debug.Print("Fetched Latest Version: " & versionText.Trim())
                Return versionText.Trim()
            End Using
        Catch
            Return Nothing
        End Try
    End Function
    Friend Function IsNewerVersion(latest As String) As Boolean
        Try
            Dim vCurrent As New Version(GetFullVersion())
            Dim vLatest As New Version(latest)
            Debug.Print("Comparing Versions: Current=" & vCurrent.ToString() & " Latest=" & vLatest.ToString())
            Return vLatest > vCurrent
        Catch
            Return False
        End Try
    End Function

End Module
