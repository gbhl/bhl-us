Prerequisites
=============

- SQL Express 2012
- Visual Studio 2012 or later

Notes
=====

- The folder holding the bhl-us source code is referred to throughout this document as &lt;BHLRoot&gt;.
- These instructions assume that the databases will be named "BHL", "BHLImport", "BHLAuditArchive", and "IAAnalysis".

Setup
=====

After downloading the bhl-us source code, do the following to get the web sites and utility applications running.

Database Creation
-----------------

1) Open a Windows command prompt.

2) Make sure that the sqlcmd utility, which is part of the SQL Server client tools, is included in your path.  More information can be found at http://technet.microsoft.com/en-us/library/ms162773.aspx.

3) Navigate to the &lt;BHLRoot&gt;\Database-BHL folder.

4) Run the BHLDBBuildScript.bat batch file.  This will build the primary database.

	Usage:

	BHLDBBuildScript SERVERNAME DATABASENAME FULLTEXTCATALOGFILEPATH ISPRODUCTION DATAORSTRUCTURE

	where 

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "BHL" be used as the database name.
	FULLTEXTCATALOGFILEPATH is the path in which to place the full-text catalog file.  Use quotes around this value if the path contains spaces.
	ISPRODUCTION is true for a production database, and false for a development database.  Auditing triggers are removed from development databases.
	DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the structure and add the data.

	Example: 

	BHLDBBuildScript localhost BHL "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA" false all

5) In the new BHL database, create roles named db\_executor and db\_webuser.

	USE [BHL];
	CREATE ROLE db_executor;
	GRANT EXECUTE TO db_executor;

	CREATE ROLE db_webuser;
	GRANT INSERT ON dbo.PDF TO db_webuser;
	GRANT INSERT ON dbo.PDFPage TO db_webuser;
	GRANT UPDATE ON dbo.Page TO db_webuser;

6) Navigate to the &lt;BHLRoot&gt;\Database-BHLImport folder.

7) Run the BHLImportDBBuildScript.bat batch file.  This will build the database used as a staging area for new material.

	Usage:

	BHLImportDBBuildScript SERVERNAME DATABASENAME DATAORSTRUCTURE

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "BHLImport" be used as the database name.
	DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the structure and add the data.

	Example: 

	BHLImportDBBuildScript localhost BHLImport all

8) Navigate to the &lt;BHLRoot&gt;\Database-BHLAuditArchive folder.

9) Run the BHLAuditArchiveDBBuildScript.bat batch file.  This will build the auditing database.

	Usage:

	BHLAuditArchiveDBBuildScript SERVERNAME DATABASENAME

	where

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "BHLAuditArchive" be used as the database name.

	Example: 

	BHLAuditArchiveDBBuildScript localhost BHLAuditArchive

10) Navigate to the &lt;BHLRoot&gt;\Database-IAAnalysis folder.

11) Run the IAAnalysisDBBuildScript.bat batch file.  This will build the database used to ingest non-biodiversity-collection items from Internet Archive.

	Usage:

	IAAnalysisDBBuildScript SERVERNAME DATABASENAME DATAORSTRUCTURE

	where

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "IAAnalysis" be used as the database name.
	DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the structure and add the data.

	Example: 

	IAAnalysisDBBuildScript localhost IAAnalysis all

12) Create a new SQL Server login named BHLWebUser.  Map it to a user named BHLWebUser in the new BHL database, and assign it to the "db\_executor" and "db\_webuser" database roles.

	USE [master];
	CREATE LOGIN [BHLWebUser] WITH PASSWORD=N'BHLWebUser';

	USE [BHL];
	CREATE USER [BHLWebUser] FOR LOGIN [BHLWebUser] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_executor] ADD MEMBER [BHLWebUser];
	ALTER ROLE [db_webuser] ADD MEMBER [BHLWebUser];

13) Create a new SQL Server login named BHLService.  Map it to a user named BHLService in the BHL, BHLAuditArchive, BHLImport, and IAAnalysis databases.  In each database, assign the new user to the "db\_datareader", "db\_datawriter", and "db\_owner" database roles.

	USE [master];
	CREATE LOGIN [BHLService] WITH PASSWORD=N'BHLService';

	USE [BHL];
	CREATE USER [BHLService] FOR LOGIN [BHLService] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_datareader] ADD MEMBER [BHLService];
	ALTER ROLE [db_datawriter] ADD MEMBER [BHLService];
	ALTER ROLE [db_owner] ADD MEMBER [BHLService];

	USE [BHLAuditArchive];
	CREATE USER [BHLService] FOR LOGIN [BHLService] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_datareader] ADD MEMBER [BHLService];
	ALTER ROLE [db_datawriter] ADD MEMBER [BHLService];
	ALTER ROLE [db_owner] ADD MEMBER [BHLService];

	USE [BHLImport];
	CREATE USER [BHLService] FOR LOGIN [BHLService] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_datareader] ADD MEMBER [BHLService];
	ALTER ROLE [db_datawriter] ADD MEMBER [BHLService];
	ALTER ROLE [db_owner] ADD MEMBER [BHLService];

	USE [IAAnalysis];
	CREATE USER [BHLService] FOR LOGIN [BHLService] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_datareader] ADD MEMBER [BHLService];
	ALTER ROLE [db_datawriter] ADD MEMBER [BHLService];
	ALTER ROLE [db_owner] ADD MEMBER [BHLService];

Application Configuration
-------------------------
	
1) Make copies of the config files as indicated in the following list:

<table>
<tr><th>Original File</th><th>Copy To</th></tr>
<tr><td>&lt;BHLRoot&gt;\BHLAdminWeb\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLAdminWeb\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLApi3Web\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLApi3Web\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLApi3WebTest\App.config.template</td><td>&lt;BHLRoot&gt;\BHLApi3WebTest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLApiDALTest\App.config.template</td><td>&lt;BHLRoot&gt;\BHLApiDALTest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLAuditExport\app.config.template</td><td>&lt;BHLRoot&gt;\BHLAuditExport\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLBibTeXExport\App.config.template</td><td>&lt;BHLRoot&gt;\BHLBibTeXExport\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config.template</td><td>&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLCoreDALTest\App.config.template</td><td>&lt;BHLRoot&gt;\BHLCoreDALTest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLDOIService\app.config.template</td><td>&lt;BHLRoot&gt;\BHLDOIService\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLEndNoteExport\app.config.template</td><td>&lt;BHLRoot&gt;\BHLEndNoteExport\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLFileAccessRemotingUtilities\Remoting.config.template</td><td>&lt;BHLRoot&gt;\BHLFileAccessRemotingUtilities\Remoting.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLFileAccessService\App.config.template</td><td>&lt;BHLRoot&gt;\BHLFileAccessService\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config.template</td><td>&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLImportEFDataModel\App.Config.template</td><td>&lt;BHLRoot&gt;\BHLImportEFDataModel\App.Config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLImportService\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLImportService\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLMETSUpload\app.config.template</td><td>&lt;BHLRoot&gt;\BHLMETSUpload\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLMODSExport\app.config.template</td><td>&lt;BHLRoot&gt;\BHLMODSExport\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config.template</td><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\Remoting.config.template</td><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\Remoting.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config.template</td><td>&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLPDFGenerator\app.config.template</td><td>&lt;BHLRoot&gt;\BHLPDFGenerator\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLService\Remoting.config.template</td><td>&lt;BHLRoot&gt;\BHLService\Remoting.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLService\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLService\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\Remoting.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\Remoting.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\services\Name\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\services\Name\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config.template</td><td>&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAFileGenerator\IAFileGeneratorGUI\App.config.template</td><td>&lt;BHLRoot&gt;\IAFileGenerator\IAFileGeneratorGUI\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAHarvest\App.config.template</td><td>&lt;BHLRoot&gt;\IAHarvest\App.config</td></tr>
</table>

&nbsp;  
2) Make the specified modifications to each of the config files in the following list:

\# = denotes optional modifications that are not required for development installations

&nbsp;  
**WWW.BIODIVERSITYLIBRARY.ORG**

**&lt;BHLRoot&gt;\BHLService\Web.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td>appSettings/UseRemoteFileAccessProvider</td><td>false</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLUSWeb2\Web.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>appSettings/UseRemoteFileAccessProvider</td><td>false</td></tr>
<tr><td># appSettings/TwitterConsumerKey</td><td>Consumer Key for Twitter API</td></tr>
<tr><td># appSettings/TwitterConsumerSecret</td><td>Consumer Secret for Twitter API</td></tr>
<tr><td># appSettings/GeminiURL</td><td>Issue tracking service URL</td></tr>
<tr><td># appSettings/GeminiUser</td><td>Issue tracking service username</td></tr>
<tr><td># appSettings/GeminiPassword</td><td>Issue tracking service password</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td># system.net/mailSettings/smtp/network</td><td>STMP host address, username, and password</td></tr>
</table>

&nbsp;  
**ADMIN.BIODIVERSITYLIBRARY.ORG**

**&lt;BHLRoot&gt;\BHLAdminWeb\Web.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>appSettings/CollectionImageUploadPath</td><td>Path in which to place uploaded images.</td></tr>
<tr><td>appSettings/ItunesImageUploadPath</td><td>Path in which to place uploaded images.</td></tr>
<tr><td>appSettings/AlertMsgPath</td><td>Path in which to place text file with informational messages.</td></tr>
<tr><td>appSettings/MARCUploadPath</td><td>Path in which to place uploaded MARC files.</td></tr>
<tr><td>appSettings/MARCUploadDrive</td><td>Drive letter or server name for MARC uploads.</td></tr>
<tr><td>appSettings/MARCUploadServer</td><td>Server name for MARC uploads.</td></tr>
<tr><td>appSettings/CitationNewPath</td><td>Path for new uploads of citation information.</td></tr>
<tr><td>appSettings/CitationCompletePath</td><td>Path for completed uploads of citation information.</td></tr>
<tr><td>appSettings/CitationErrorPath</td><td>Path for failed uploads of citation information.</td></tr>
<tr><td>appSettings/OCRJobNewPath</td><td>Path for OCR job files.</td></tr>
<tr><td># appSettings/FlickrUserId</td><td>Flickr user identifier.</td></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromName</td><td>Email sender address to use when sending emails.</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>Email sender name to use when sending emails.</td></tr>
<tr><td># appSettings/BHLUserAdminEmailAddress</td><td>Email address of a BHL user administrator.</td></tr>
<tr><td>appSettings/LocalFileFolder</td><td>File folder in which to place new data files ingested from Internet Archive.</td></tr>
<tr><td>appSettings/DOIDepositFileLocation</td><td>Path to CrossRef deposit files.</td></tr>
<tr><td>appSettings/DOISubmitLogFileLocation</td><td>Path to Crossref log files.</td></tr>
<tr><td># appSettings/FlickrKey</td><td>Flickr API key</td></tr>
<tr><td># appSettings/FlickrSecret</td><td>Flickr API secret</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td>connectionStrings/BHLUser</td><td>Connection string for BHL user account database</td></tr>
</table>

&nbsp;  
**API3.BIODIVERSITYLIBRARY.ORG**

**&lt;BHLRoot&gt;\BHLApi3Web\Web.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

&nbsp;  
**DATA IMPORT APPS**

**&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>connectionStrings/BHLImportEntities</td><td>Connection string for BHLImport database</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLImportEFDataModel\App.Config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImportEntities</td><td>Connection string for BHLImport database</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLImportService\Web.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
</table>

**&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/IAAnalysis</td><td>Connection string for IAAnalysis database</td></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
</table>

**&lt;BHLRoot&gt;\IAHarvest\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/LocalFileFolder</td><td>Local folder to hold downloaded files</td></tr>
</table>

&nbsp;  
**UTILITY APPS**

**&lt;BHLRoot&gt;\BHLAuditExport\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLBibTeXExport\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BibTeXTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td><tr><td>appSettings/BibTeXItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLDOIService\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/CrossRefDepositorName</td><td>Depositor name associated with CrossRef account</td></tr>
<tr><td>appSettings/CrossRefDepositorEmail</td><td>Depositor email associated with CrossRef account</td></tr>
<tr><td>appSettings/CrossRefLogin</td><td>Login for CrossRef account</td></tr>
<tr><td>appSettings/CrossRefPassword</td><td>Password for CrossRef account</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLEndNoteExport\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/EndNoteTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/EndNoteSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/FlickrAPIKey</td><td>Flickr API key</td></tr>
<tr><td>appSettings/ImageFileName</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/ImageFolder</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/ImageListFilePath</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/DefaultFilesFolder</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLMETSUpload\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/METSEmail</td><td>Organization email address to place in METS files</td></tr>
<tr><td>appSettings/IAS3AccessKey</td><td>Internet Archive access key</td></tr>
<tr><td>appSettings/IAS3SecretKey</td><td>Internet Archive secret key</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLMODSExport\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>	SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/MODSTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/IAS3AccessKey</td><td>Internet Archive access key</td></tr>
<tr><td>appSettings/IAS3SecretKey</td><td>Internet Archive secret key</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLPDFGenerator\app.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>SMTP host address</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/PdfFilePath</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

**&lt;BHLRoot&gt;\IAFileGenerator\IAFileGeneratorGUI\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>appSettings/AccessKey</td><td>Internet Archive access key</td></tr>
<tr><td>appSettings/SecretKey</td><td>Internet Archive secret key</td></tr>
</table>

&nbsp;  
**TEST PROJECTS**

**&lt;BHLRoot&gt;\BHLApi3WebTest\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLApiDALTest\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

**&lt;BHLRoot&gt;\BHLCoreDALTest\App.config**

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

