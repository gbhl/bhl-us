@ECHO OFF

IF "%1" == "" GOTO MISSINGPARAM
IF "%2" == "" GOTO MISSINGPARAM
IF "%3" == "" GOTO MISSINGPARAM

@ECHO ON

IF "%~3" == "structure" GOTO CREATEDB
IF "%~3" == "all" GOTO CREATEDB

GOTO DATALOAD

:CREATEDB

REM --------------------------------------
REM  Create Database
REM --------------------------------------
sqlcmd -E -S %1 -Q "CREATE DATABASE [%2]"
sqlcmd -E -S %1 -Q "ALTER DATABASE [%2] MODIFY FILE ( NAME = N'%2', SIZE = 512000KB , FILEGROWTH = 10%)"

REM --------------------------------------
REM  Build Tables
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Collection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\Item.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ExportScanListAuthor.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ExportScanListDates.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ExportScanListItem.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ExportScanListOCLC.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\ItemCollection.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\MarcControl.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\MarcDataField.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\MarcSubField.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\RptByCategory.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\RptBySubject.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\RptCombined.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SubjectExclusion.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Tables\SubjectSample.sql"

REM --------------------------------------
REM  Add Synonyms
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Synonyms\BHLItem.sql"

REM --------------------------------------
REM  Build Functions
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnFilterString.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Functions\fnMarcStringForMarcDataField.sql"

REM --------------------------------------
REM  Build Views
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -I -i "dbo\Views\vwMarcControl.sql"
sqlcmd -E -S %1 -d %2 -I -i "dbo\Views\vwMarcDataField.sql"
sqlcmd -E -S %1 -d %2 -I -i "dbo\Views\vwMarcDetail.sql"

:DATALOAD

IF "%~3" == "data" GOTO INSERTDATA
IF "%~3" == "all" GOTO INSERTDATA

GOTO RESUMESTRUCTURE

:INSERTDATA

REM --------------------------------------
REM  Insert Data
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "data\dbo.SubjectExclusion.Table.sql"
sqlcmd -E -S %1 -d %2 -i "data\dbo.SubjectSample.Table.sql"

:RESUMESTRUCTURE

IF "%~3" == "structure" GOTO CREATESP
IF "%~3" == "all" GOTO CREATESP

GOTO DONE

:CREATESP

REM --------------------------------------
REM  Build Stored Procedures
REM --------------------------------------
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CollectionDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CollectionInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CollectionSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CollectionSelectByCollectionName.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\CollectionUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\DoAnalysis.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ExportScanList.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\GetNewSubjectSample.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemCollectionDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemCollectionInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemCollectionSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemCollectionUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectByIdentifier.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectForXMLDownload.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemSelectNextStartDate.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\ItemUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcControlDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcControlInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcControlSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcControlUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcDataFieldDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcDataFieldInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcDataFieldSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcDataFieldUpdateAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcSubFieldDeleteAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcSubFieldInsertAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcSubFieldSelectAuto.sql"
sqlcmd -E -S %1 -d %2 -i "dbo\Stored Procedures\MarcSubFieldUpdateAuto.sql"

GOTO DONE

:MISSINGPARAM

@ECHO OFF

ECHO.
ECHO Usage:
ECHO. 
ECHO IAAnalysisDBBuildScript SERVERNAME DATABASENAME DATAORSTRUCTURE
ECHO.
ECHO SERVERNAME is the name of the database server
ECHO DATABASENAME is the name of the database to be created
ECHO DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the struture and add the data.
ECHO.
ECHO Example: IAAnalysisDBBuildScript localhost IAAnalysis all
ECHO.
ECHO It is highly recommended that "IAAnalysis" be used as the database name.
ECHO.

@ECHO ON

:DONE

