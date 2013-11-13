@ECHO OFF

IF "%1" == "" GOTO MISSINGPARAM
IF "%2" == "" GOTO MISSINGPARAM

@ECHO ON

:CREATEDB

REM --------------------------------------
REM  Create Database
REM --------------------------------------
sqlcmd -E -S %1 -Q "CREATE DATABASE [%2]"
sqlcmd -E -S %1 -Q "ALTER DATABASE [%2] MODIFY FILE ( NAME = N'%2', SIZE = 512000KB , FILEGROWTH = 10%)"

REM --------------------------------------
REM  Build Schemas
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "Security\Schemas\audit.sql"

REM --------------------------------------
REM  Build Tables
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "audit\Tables\AuditBasic.sql"

REM --------------------------------------
REM  Add Synonyms
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "audit\Synonyms\AuditBasicArchive.sql"

GOTO DONE

:MISSINGPARAM

@ECHO OFF

ECHO.
ECHO Usage:
ECHO. 
ECHO BHLAuditArchiveDBBuildScript SERVERNAME DATABASENAME
ECHO.
ECHO SERVERNAME is the name of the database server
ECHO DATABASENAME is the name of the database to be created
ECHO.
ECHO Example: BHLAuditArchiveDBBuildScript localhost BHLAuditArchive
ECHO.
ECHO It is highly recommended that "BHLAuditArchive" be used as the database name.
ECHO.

@ECHO ON

:DONE

