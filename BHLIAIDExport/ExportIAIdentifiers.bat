﻿BHLIAIDExport

@ECHO OFF
cd identifiers
@ECHO ON

git commit -a -m "Automatic update"

@ECHO OFF
REM Set the environment variable so that the "git push" command will find the SSH key
SET HOME=D:\Services\BHLIAIDExport
@ECHO ON

git push origin master
