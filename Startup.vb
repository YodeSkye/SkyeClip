
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
        Try
            appmutex = New Mutex(True, MutexName, createdNew)
        Catch ex As Exception
            MessageBox.Show($"Failed to acquire single-instance application lock:" & vbCrLf & ex.Message,
                            $"{Application.ProductName} - Startup Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Return
        End Try
        If Not createdNew Then
            ' Another instance is already running → exit cleanly without leaking mutex handle
            appmutex.Dispose()
            appmutex = Nothing
            Return
        End If

        Try
            ' INITIALIZE APPLICATION LOGGING & REGISTRY
#If DEBUG Then
            Skye.Common.Log.Initialize(App.GetAssemblyName() & "DEV") ' Use separate log file for debug builds
            Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName + "DEV" ' Use separate registry key for debug builds
#Else
            Skye.Common.Log.Initialize(App.GetAssemblyName()) ' Use standard log file for release builds
            Skye.Common.RegistryHelper.BaseKey = "Software\" + App.GetAssemblyName ' Use standard registry key for release builds
#End If
            Skye.Common.Log.Write(GetAssemblyName() & " Started...")

            ' Check for storage lockout
            If String.IsNullOrEmpty(App.UserPath) Then
                MessageBox.Show(
                    $"Critical Error: {Application.ProductName} was unable to access its local storage directory." & vbCrLf & vbCrLf &
                    "This is usually caused by temporary file locks, security software, or folder permission issues." & vbCrLf & vbCrLf &
                    "The application will now exit.",
                    $"{Application.ProductName} - Storage Access Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                )
                ' Cleanly exit Main and hit Finally block instead of abrupt Environment.Exit
                Return
            End If

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
            Text.Encoding.RegisterProvider(Text.CodePagesEncodingProvider.Instance) ' Allows use of Windows-1252 character encoding, needed for Scratch Pad Proper Case function.

            App.WarmUpDataTable() ' Pre-load DataTable to improve performance and stop errors when it's first used in the app.

            ' START APPLICATION
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            App.Tray = New TrayAppContext()
            AddHandler Skye.UI.ThemeManager.ThemeChanged, AddressOf App.Tray.OnThemeChanged

            App.MaintenanceTimer.Start()
            App.AutomationTimer.Start()

            Application.Run(App.Tray)

        Catch ex As Exception
            Try
                Skye.Common.Log.Write($"CRITICAL STARTUP EXCEPTION: {ex}")
            Catch
                ' Logging failure safeguard
            End Try
            MessageBox.Show($"A critical unhandled error occurred during startup:" & vbCrLf & vbCrLf & ex.Message,
                            $"{Application.ProductName} - Fatal Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        Finally
            ' CLEANUP MUTEX AND LOGGING ON EXIT
            If appmutex IsNot Nothing Then
                Try
                    appmutex.ReleaseMutex()
                Catch
                    ' Handle was either not owned or already released
                End Try
                appmutex.Dispose()
                appmutex = Nothing
            End If
            Try
                Skye.Common.Log.Write("..." & GetAssemblyName() & " Closed")
            Catch
            End Try
        End Try
    End Sub

    Friend Sub ExitApp()
        Application.Exit()
    End Sub
    Friend Sub RestartApp()
        If appmutex IsNot Nothing Then
            Try
                appmutex.ReleaseMutex()
            Catch
            End Try
            appmutex.Dispose()
            appmutex = Nothing
        End If
        Application.Restart()
    End Sub

End Module
