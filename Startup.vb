
Imports System.Threading

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
        If App.Settings.ScratchPadKeepText AndAlso IO.File.Exists(App.ScratchPadPath) Then
            App.ScratchPadText = IO.File.ReadAllText(App.ScratchPadPath)
        Else
            App.ScratchPadText = String.Empty
        End If

        ' Start the application
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        App.Tray = New TrayAppContext()
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
