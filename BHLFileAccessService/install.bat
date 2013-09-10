call "C:\Program Files\Microsoft Visual Studio 8\Common7\Tools\vsvars32.bat"
installutil.exe "MOBOT.FileAccessService.exe"

net start "MOBOT FileAccess Service"

pause