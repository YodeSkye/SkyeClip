[Setup]
AppName=SkyeClip
AppVersion=1.1
AppVerName=SkyeClip v1.1
DefaultDirName={commonpf64}\Skye\SkyeClip
ArchitecturesInstallIn64BitMode=x64compatible
DisableProgramGroupPage=yes
OutputDir=.
OutputBaseFilename=SkyeClipSetup

[Files]
Source: "bin\Release\net10.0-windows\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
; Start Menu shortcut (root, no subfolder)
Name: "{commonprograms}\SkyeClip"; Filename: "{app}\SkyeClip.exe"

; Optional desktop shortcut
Name: "{commondesktop}\SkyeClip"; Filename: "{app}\SkyeClip.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"