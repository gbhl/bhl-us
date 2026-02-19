@ECHO OFF

REM --------------------------------
REM Read and validate parameters
REM /ITEM:{0} /DOWNLOAD:{1} /UPLOAD:{2} /PUBLISH:{3} /QUIET:{4}
REM --------------------------------

IF "%1"=="" GOTO MISSINGPARAM
IF "%2"=="" GOTO MISSINGPARAM
IF "%3"=="" GOTO MISSINGPARAM
IF "%4"=="" GOTO MISSINGPARAM
IF "%5"=="" GOTO MISSINGPARAM

REM --------------------------------
REM Execute IAHarvest.exe once or twice, depending on the specified parameters.
REM --------------------------------
IF /I "%2"=="/DOWNLOAD:TRUE" CALL IAHarvest.exe %1 %2 /UPLOAD:False /PUBLISH:False %5
IF /I "%3"=="/UPLOAD:TRUE" CALL IAHarvest.exe %1 /DOWNLOAD:False %3 /PUBLISH:False %5
IF /I "%4"=="/PUBLISH:TRUE" CALL IAHarvest.exe %1 /DOWNLOAD:False /UPLOAD:False %4 %5

GOTO DONE

:MISSINGPARAM

ECHO.
ECHO Usage:
ECHO.
ECHO IAHarvest2Step.bat /ITEM:^<ITEM^> /DOWNLOAD:^<DOWNLOAD^> /UPLOAD:^<UPLOAD^> /PUBLISH:^<PUBLISH^> /QUIET:^<QUIET^>
ECHO.
ECHO ITEM is the identifier of the Item in the BHLImport.dbo.IAItem table.
ECHO.
ECHO DOWNLOAD indicates whether or not to download the Item's files from Internet Archive.
ECHO       "True" to download
ECHO       "False" to skip the download
ECHO.
ECHO UPLOAD indicates whether or not to stage the Item's metadata for publishing to BHL.
ECHO       "True" to stage
ECHO       "False" to not stage
ECHO.
ECHO PUBLISH indicates whether or not to publish the Item to BHL.
ECHO       "True" to publish
ECHO       "False" to not publish
ECHO.
ECHO QUIET indicates whether or not to send an email after a successful execution.
ECHO		"True" to not send emails after success (i.e. only send emails after failures)
ECHO		"False" to always send emails
ECHO.

:DONE
