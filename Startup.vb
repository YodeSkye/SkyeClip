
Imports System.Threading
Imports Skye.UI

Module Startup

    Private appmutex As Mutex

    <STAThread>
    Friend Sub Main()

        ' SINGLE INSTANCE CHECK
#If DEBUG Then
        Const MutexName As String = "SkyeClip_SingleInstanceDEV"
#Else
        Const MutexName As String = "SkyeClip_SingleInstance"
#End If
        Dim createdNew As Boolean
        appmutex = New Mutex(True, MutexName, createdNew)
        If Not createdNew Then Return 'Another instance is already running → just exit

        ' INITIALIZE APPLICATION
#If DEBUG Then
        Skye.Common.Log.Initialize(App.GetAssemblyName() & "DEV") ' Use separate log file for debug builds
        Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName + "DEV" ' Use separate registry key for debug builds
#Else
        Skye.Common.Log.Initialize(App.GetAssemblyName()) ' Use standard log file for release builds
        Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName ' Use standard registry key for release builds
#End If
        Skye.Common.Log.Write(GetAssemblyName() & " Started...")
        App.Settings.Load()

        ' Get Theme
        If App.Settings.ThemeAuto Then
            Skye.UI.ThemeManager.SetTheme(App.DetectWindowsTheme())
        Else
            Skye.UI.ThemeManager.CurrentTheme = Skye.UI.SkyeThemes.GetTheme(App.Settings.ThemeName)
        End If

        ' Check autostart setting
        Dim autostart As Boolean = App.IsAutoStartEnabled
        If autostart <> App.Settings.AutoStartWithWindows Then
            App.Settings.AutoStartWithWindows = autostart
            App.Settings.Save()
        End If

        ' Scratch Pad
        App.LoadScratchPadText()
        Text.Encoding.RegisterProvider(Text.CodePagesEncodingProvider.Instance) 'Allows use of Windows-1252 character encoding, needed for Scratch Pad Proper Case function.

        App.WarmUpDataTable() 'Pre-load DataTable to improve performance and stop errors when it's first used in the app.

        ' START APPLICATION
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        App.Tray = New TrayAppContext()
        AddHandler Skye.UI.ThemeManager.ThemeChanged, AddressOf App.Tray.OnThemeChanged
        App.MaintenanceTimer.Start()
        App.AutomationTimer.Start()
        Application.Run(App.Tray)

        ' APPLICATION IS CLOSING
        Skye.Common.Log.Write("..." & GetAssemblyName() & " Closed")

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
