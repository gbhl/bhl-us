net stop "MOBOT FileAccess Service"


call "C:\Program Files\Microsoft Visual Studio 8\Common7\Tools\vsvars32.bat"
installutil.exe /u "MOBOT.FileAccessService.exe"


pause