
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
        App.WriteToLog(GetAssemblyName() & " Started...")
#If DEBUG Then
        Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName + "DEV" 'Use separate registry key for debug builds
#Else
        Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName 'Use standard registry key for release builds
#End If
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

        ' START APPLICATION
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        App.Tray = New TrayAppContext()
        AddHandler Skye.UI.ThemeManager.ThemeChanged, AddressOf App.Tray.OnThemeChanged
        App.MaintenanceTimer.Start()


        App.Rules.Add(New AppContextRule With {
            .TargetProcess = "firefox",
            .Mode = AppContextRule.ActivationMode.RunningProcess,
            .OnEnter = Sub(ctx)
                           App.Settings.CurrentProfileID = 63
                       End Sub,
            .OnExit = Sub(ctx)
                          App.Settings.CurrentProfileID = 56
                      End Sub
        })
        App.Rules.Add(New AppContextRule With {
            .TargetProcess = "devenv",
            .Mode = AppContextRule.ActivationMode.ForegroundWindow,
            .OnEnter = Sub(ctx)
                           ctx.BlockCapture = True
                       End Sub,
            .OnExit = Sub(ctx)
                          ctx.BlockCapture = False
                      End Sub
        })
        App.Rules.Add(New TimeRule With {
            .StartTime = TimeSpan.FromHours(9),
            .EndTime = TimeSpan.FromHours(20),
            .TargetProfileID = 63,
            .ApplyProfile = Sub(id)
                                App.Settings.CurrentProfileID = id
                                Debug.WriteLine("TimeRule Activated: " & id)
                            End Sub
        })


        App.AutomationTimer.Start()
        Application.Run(App.Tray)

        ' APPLICATION IS CLOSING
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
