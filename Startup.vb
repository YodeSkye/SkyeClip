
Imports System.Threading
Imports Skye.UI

Module Startup

    Private appmutex As Mutex

    <STAThread>
    Friend Sub Main()

        ' Single-Instance check
#If DEBUG Then
        Const MutexName As String = "SkyeClip_SingleInstanceDEV"
#Else
        Const MutexName As String = "SkyeClip_SingleInstance"
#End If
        Dim createdNew As Boolean
        appmutex = New Mutex(True, MutexName, createdNew)
        If Not createdNew Then Return 'Another instance is already running → just exit

        ' Initialize the application
        App.WriteToLog(GetAssemblyName() & " Started...")
        App.Settings.Load()
        ' Get Theme
        If App.Settings.ThemeAuto Then
            Skye.UI.ThemeManager.SetTheme(DetectWindowsTheme())
        Else
            Skye.UI.ThemeManager.CurrentTheme = Skye.UI.SkyeThemes.GetTheme(App.Settings.ThemeName)
        End If
        ' Check autostart setting
        Dim autostart As Boolean = App.IsAutoStartEnabled
        If autostart <> App.Settings.AutoStartWithWindows Then
            App.Settings.AutoStartWithWindows = autostart
            App.Settings.Save()
        End If
        ' Load scratchpad text if setting enabled
        If App.Settings.ScratchPadKeepText AndAlso IO.File.Exists(App.ScratchPadPath) Then
            App.ScratchPadText = IO.File.ReadAllText(App.ScratchPadPath)
        Else
            App.ScratchPadText = String.Empty
        End If

        ' Start the application
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        App.Tray = New TrayAppContext()
        AddHandler Skye.UI.ThemeManager.ThemeChanged, AddressOf App.Tray.OnThemeChanged
        Application.Run(App.Tray)

        ' Application is closing
        App.WriteToLog("..." & GetAssemblyName() & " Closed")

    End Sub

    Friend Sub ExitApp()
        Application.Exit()
    End Sub
    Friend Sub RestartApp()
        appmutex.ReleaseMutex()
        appmutex.Dispose()
        appmutex = Nothing
        Application.Restart()
    End Sub

End Module
