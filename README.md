Prerequisites
=============

- SQL Express 2014 or later
- Visual Studio 2017 or later
- Docker (Optional) - Used to host ElasticSearch and RabbitMQ, which enable full-text search.

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

8) In the new BHLImport database, create a role named db\_webuser.

	USE [BHLImport];
	CREATE ROLE db_webuser;
	GRANT SELECT ON dbo.IAFile TO db_webuser;
	GRANT SELECT ON dbo.IAItem TO db_webuser;

9) Navigate to the &lt;BHLRoot&gt;\Database-BHLAuditArchive folder.

10) Run the BHLAuditArchiveDBBuildScript.bat batch file.  This will build the auditing database.

	Usage:

	BHLAuditArchiveDBBuildScript SERVERNAME DATABASENAME

	where

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "BHLAuditArchive" be used as the database name.

	Example: 

	BHLAuditArchiveDBBuildScript localhost BHLAuditArchive

11) Navigate to the &lt;BHLRoot&gt;\Database-IAAnalysis folder.

12) Run the IAAnalysisDBBuildScript.bat batch file.  This will build the database used to ingest non-biodiversity-collection items from Internet Archive.

	Usage:

	IAAnalysisDBBuildScript SERVERNAME DATABASENAME DATAORSTRUCTURE

	where

	SERVERNAME is the name of the database server
	DATABASENAME is the name of the database.  It is recommended that "IAAnalysis" be used as the database name.
	DATAORSTRUCTURE is "structure" to build the empty database (no data), "data" to add data to an existing database, or "all" to build the structure and add the data.

	Example: 

	IAAnalysisDBBuildScript localhost IAAnalysis all

13) Create a new SQL Server login named BHLWebUser.  Map it to a user named BHLWebUser in the new BHL database, and assign it to the "db\_executor" and "db\_webuser" database roles.

	USE [master];
	CREATE LOGIN [BHLWebUser] WITH PASSWORD=N'BHLWebUser';

	USE [BHL];
	CREATE USER [BHLWebUser] FOR LOGIN [BHLWebUser] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_executor] ADD MEMBER [BHLWebUser];
	ALTER ROLE [db_webuser] ADD MEMBER [BHLWebUser];

14) Map the BHLWebUser login to a user named BHLWebUser in the new BHLImport database, and assign it to the "db\_webuser" database role.

	USE [BHLImport];
	CREATE USER [BHLWebUser] FOR LOGIN [BHLWebUser] WITH DEFAULT_SCHEMA=[dbo];
	ALTER ROLE [db_webuser] ADD MEMBER [BHLWebUser];

15) Create a new SQL Server login named BHLService.  Map it to a user named BHLService in the BHL, BHLAuditArchive, BHLImport, and IAAnalysis databases.  In each database, assign the new user to the "db\_datareader", "db\_datawriter", and "db\_owner" database roles.

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

ElasticSearch Installation and Configuration (Optional)
-------------------------------------------------------
The BHL web site uses ElasticSearch to implement many of its search capabilities.  However, if ElasticSearch is not present, the web site will fall back to a SQL Server search implementation.  The SQL Server search performs basic searches of catalog metadata.  Full-text searches are disabled, faceting is not available, and the “search within a book” feature will not work.  Therefore, for a full-featured BHL implementation, the instructions in this section should be followed.  If a limited search is satisfactory, then this section, the “RabbitMQ” section, and the “Index Data in ElasticSearch” section can be skipped.

1) (If necessary) Download and install Docker from docker.com.  As of January 2019, the product to install is called “Docker Desktop”.

2) Open a Windows command prompt.

3) Get the official ElasticSearch image.
 
	docker pull docker.elastic.co/elasticsearch/elasticsearch:7.9.1
 
4) Start a new ElasticSearch docker container that will be accessible at http://localhost:9200.
 
	docker run -d --name es791 -p 9200:9200 -e "http.host=0.0.0.0" -e "transport.host=127.0.0.1" docker.elastic.co/elasticsearch/elasticsearch:7.9.1
 
5) Locate the "elasticsearch.yml" file within the running Docker container.  The following command should return something like "/usr/share/elasticsearch/config/elasticsearch.yml"
 
	docker exec -it es791 find / -name "elasticsearch.yml"
 
6) Copy the elasticsearch.yml file from the container to the host.
 
	docker cp es791:/usr/share/elasticsearch/config/elasticsearch.yml c:\elasticsearch.yml
 
7) On the host, use a text editor to disable Xpack security by adding the following line to the elasticsearch.yml file:
 
	xpack.security.enabled: false
 
	The file contents should now look something like this:
 
	cluster.name: "docker-cluster"
	network.host: 0.0.0.0
 
	&#35; minimum_master_nodes need to be explicitly set when bound on a public IP
	&#35; set to 1 to allow single node clusters
	&#35; Details: https://github.com/elastic/elasticsearch/pull/17288
	discovery.zen.minimum_master_nodes: 1
 
	xpack.security.enabled: false
 
8) Copy the edited elasticsearch.yml file from the host to the running Docker container.
 
	docker cp c:\elasticsearch.yml es791:/usr/share/elasticsearch/config/elasticsearch.yml

9) Add the ICU analysis plug-in to ElasticSearch to add better support for Unicode characters, including Asian characters.
 
	docker exec -it es791 /usr/share/elasticsearch/bin/elasticsearch-plugin install analysis-icu
 
10) Stop the running container.
 
	docker stop es791
 
11)  Restart the ElasticSearch container.  Note that by using the name (es791) assigned to the container is Step 4, all of the other arguments we specified in Step 4 (-d -e -p <containername>) are used by default.
 
	docker start es791
 
12) Verify the operation of ElasticSearch by opening a browser and navigating to http://localhost:9200.  You should get a response that looks something like this:
 
	{
  	"name" : "90WsOOT",
  	"cluster_name" : "docker-cluster",
  	"cluster_uuid" : "Ok4_vavaTuSxn9qrsAGZwA",
  	"version" : {
    		"number" : "5.4.2",
    		"build_hash" : "f9d9b74",
    		"build_date" : "2017-02-24T17:26:45.835Z",
    		"build_snapshot" : false,
    		"lucene_version" : "6.4.1"
  	},
  	"tagline" : "You Know, for Search"
	}

13) Create new indexes using curl or a comparable tool.

	curl –X PUT http://localhost:9200/items -d @<BHLRoot>\ElasticSearch\items.json –H “Content-Type:application/json”
	curl –X PUT http://localhost:9200/catalog -d @<BHLRoot>\ElasticSearch\catalog.json –H “Content-Type:application/json”
	curl –X PUT http://localhost:9200/authors -d @<BHLRoot>\ElasticSearch\authors.json –H “Content-Type:application/json”
	curl –X PUT http://localhost:9200/keywords -d @<BHLRoot>\ElasticSearch\keywords.json –H “Content-Type:application/json”
	curl –X PUT http://localhost:9200/names -d @<BHLRoot>\ElasticSearch\names.json –H “Content-Type:application/json”
	curl –X PUT http://localhost:9200/pages -d @<BHLRoot>\ElasticSearch\pages.json –H “Content-Type:application/json”

RabbitMQ Installation and Configuration (Optional)
--------------------------------------------------

1) Open a Windows command prompt.

2) Get the official RabbitMQ image
 
	docker pull rabbitmq
 
3) Start a new ElasticSearch docker container that will be accessible at http://localhost:5672.
 
	docker run -d --name rabbit373 --hostname my-rabbit -p 5672:5672 rabbitmq
 
	(RECOMMENDED) To alternately include the RabbitMQ management console, which will be accessible at http://localhost:15672, use this instead:
 
	docker run -d --name rabbit373 --hostname my-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:management
 
4)  the operation of RabbitMQ by opening a browser and navigating to http://localhost:5672.  You should get a response that looks something like this:
 
	AMQP
 
5) Verify the operation of the RabbitMQ management console by opening a browser and navigating to http://localhost:15672.  Use guest/guest as the username/password.
 
	NOTE: To supply a different username and password, change the command that creates a RabbitMQ container with the management console to the following:
 
	docker run -d --name rabbit373mgmt --hostname my-rabbit -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:management

Application Configuration
-------------------------
	
1) Make copies of the config files as indicated in the following list:

<table>
<tr><th>Original File</th><th>Copy To</th></tr>
<tr><td>&lt;BHLRoot&gt;\BHLAdminWeb\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLAdminWeb\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLApiDALTest\App.config.template</td><td>&lt;BHLRoot&gt;\BHLApiDALTest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config.template</td><td>&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLCoreDALTest\App.config.template</td><td>&lt;BHLRoot&gt;\BHLCoreDALTest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLDOIService\app.config.template</td><td>&lt;BHLRoot&gt;\BHLDOIService\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLExportProcessor\App.config.template</td><td>&lt;BHLRoot&gt;\BHLExportProcessor\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLFlickrTagHarvest\app.config.template</td><td>&lt;BHLRoot&gt;\BHLFlickrTagHarvest\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config.template</td><td>&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLMETSUpload\app.config.template</td><td>&lt;BHLRoot&gt;\BHLMETSUpload\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config.template</td><td>&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLOAIHarvester\app.config.template</td><td>&lt;BHLRoot&gt;\BHLOAIHarvester\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLOCRRefresh\app.config.template</td><td>&lt;BHLRoot&gt;\BHLOCRRefresh\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config.template</td><td>&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLPDFGenerator\app.config.template</td><td>&lt;BHLRoot&gt;\BHLPDFGenerator\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.xml.template</td><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.xml</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.xml.template</td><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.Names.xml</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.xml.template</td><td>&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.Full.xml</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLSearchIndexQueueLoad\AppConfig.xml.template</td><td>&lt;BHLRoot&gt;\BHLSearchIndexQueueLoad\AppConfig.xml</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLServerTest\app.config.template</td><td>&lt;BHLRoot&gt;\BHLServerTest\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLTextImportProcessor\app.config.template</td><td>&lt;BHLRoot&gt;\BHLTextImportProcessor\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\ratelimit.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\ratelimit.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\ratelimitwhitelist.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\ratelimitwhitelist.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLUSWeb2\Views\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLUSWeb2\Views\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLWebServiceREST.v1\app.config.template</td><td>&lt;BHLRoot&gt;\BHLWebServiceREST.v1\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\BHLWebServiceREST.v1\Web.config.template</td><td>&lt;BHLRoot&gt;\BHLWebServiceREST.v1\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config.template</td><td>&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAHarvest\App.config.template</td><td>&lt;BHLRoot&gt;\IAHarvest\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\IAHarvestAsync\App.config.template</td><td>&lt;BHLRoot&gt;\IAHarvestAsync\App.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\SearchElasticTest\app.config.template</td><td>&lt;BHLRoot&gt;\SearchElasticTest\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\SiteServiceREST.v1\app.config.template</td><td>&lt;BHLRoot&gt;\SiteServiceREST.v1\app.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\SiteServiceREST.v1\Web.config.template</td><td>&lt;BHLRoot&gt;\SiteServiceREST.v1\Web.config</td></tr>
<tr><td>&lt;BHLRoot&gt;\WDHarvest\App.config.template</td><td>&lt;BHLRoot&gt;\WDHarvest\App.config</td></tr>
</table>

&nbsp;  
2) Make the following modifications to the config files:

\# = denotes optional modifications that are not required for development installations

&nbsp;  
**WWW.BIODIVERSITYLIBRARY.ORG**

The primary web user interface, allowing browsing and searching the collection as well as viewing individual items.

*&lt;BHLRoot&gt;\BHLUSWeb2\ratelimit.config*

This configuration file allows rate limits to be set by IP address, User Agent, and web site endpoint. See the instructions and examples in the ratelimit.config file for more information.

*&lt;BHLRoot&gt;\BHLUSWeb2\ratelimitwhitelist.config*

This configuration file works in tandem with the ratelimit.config file.  It specifies IP addresses, User Agents, and web site endpoints to omit from rate limiting (to be "whitelisted").  See the instructions and examples in the file for more information.

*&lt;BHLRoot&gt;\BHLUSWeb2\Web.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/PdfUrl</td><td>http://SITE_SERVICES_URL/pdf{0}/{1}, where SITE_SERVICES_URL is the URL for a running instance of the SiteServiceREST.v1 project</td></tr>
<tr><td># appSettings/GoogleAnalyticsTrackingID</td><td>Tracking identifier for the site in Google Analytics</td></tr>
<tr><td># appSettings/GeminiURL</td><td>Issue tracking service URL</td></tr>
<tr><td># appSettings/GeminiUser</td><td>Issue tracking service username</td></tr>
<tr><td># appSettings/GeminiPassword</td><td>Issue tracking service password</td></tr>
<tr><td>appSettings/ElasticSearchServerAddress</td><td>Server address for an instance of ElasticSearch</td></tr>
<tr><td>appSettings/SiteServicesUrl</td><td>URL for a running instance of the BHLSiteServiceREST.v1 project</td></tr>
<tr><td># appSettings/FundRaiseUpCampaignCode</td><td>FundraiseUp code for the site</td></tr>
<tr><td># appSettings/TwitterConsumerKey</td><td>Consumer Key for Twitter API</td></tr>
<tr><td># appSettings/TwitterConsumerSecret</td><td>Consumer Secret for Twitter API</td></tr>
<tr><td># appSettings/ReCaptchaSiteKey</td><td>Site key for Google ReCaptcha service</td></tr>
<tr><td># appSettings/ReCaptchaSecretKey</td><td>Secret key for Google ReCaptcha service</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td># connectionStrings/Admin</td><td>Optional connection string for API logging database</td></tr>
<tr><td># system.net/mailSettings/smtp/network</td><td>STMP host address, username, and password</td></tr>
</table>

&nbsp;  
**ADMIN.BIODIVERSITYLIBRARY.ORG**

The administrative user interface.  It requires authorization and authentication, and allows metadata editing, reporting, and system monitoring.

*&lt;BHLRoot&gt;\BHLAdminWeb\Web.config*

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
<tr><td># appSettings/FlickrUserId</td><td>Flickr user identifier.</td></tr>
<tr><td>appSettings/SearchServerAddress</td><td>Server address for an instance of ElasticSearch</td></tr>
<tr><td>appSettings/MessageQueueAdminAddress</td><td>Server address for the administrative interface of an instance of RabbitMQ</td></tr>
<tr><td>appSettings/SiteServicesUrl</td><td>URL for a running instance of the BHLSiteServiceREST.v1 project</td></tr>
<tr><td># appSettings/EmailFromName</td><td>Email sender address to use when sending emails.</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>Email sender name to use when sending emails.</td></tr>
<tr><td># appSettings/BHLUserAdminEmailAddress</td><td>Email address of a BHL user administrator.</td></tr>
<tr><td>appSettings/LocalFileFolder</td><td>File folder in which to place new data files ingested from Internet Archive.</td></tr>
<tr><td># appSettings/FlickrKey</td><td>Flickr API key</td></tr>
<tr><td># appSettings/FlickrSecret</td><td>Flickr API secret</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td>connectionStrings/BHLUser</td><td>Connection string for user account database</td></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
</table>

<br/>

**INTERNAL APIs**

**BHLWebServiceREST.v1**

APIs that support the internal non-web applications.

*&lt;BHLRoot&gt;\BHLWebServiceREST.v1\app.config*<br />
*&lt;BHLRoot&gt;\BHLWebServiceREST.v1\web.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>Name of a SMTP server.</td></tr>
<tr><td>appSettings/DOIDepositFileLocation</td><td>Path to CrossRef deposit files.</td></tr>
<tr><td>appSettings/DOISubmitLogFileLocation</td><td>Path to Crossref log files.</td></tr>
<tr><td>appSettings/OCRJobNewPath</td><td>Path to new OCR job files</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

<br/>

**SiteServiceREST.v1**

APIs that support the primary web UI and the administrative web interface.

*&lt;BHLRoot&gt;\SiteServiceREST.v1\app.config*<br/>
*&lt;BHLRoot&gt;\SiteServiceREST.v1\web.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/SMTPHost</td><td>Name of a SMTP server.</td></tr>
<tr><td># appSettings/SearchServerStatsUrl</td><td>Search server URL for uptime stats</td></tr>
<tr><td>appSettings/DOIDepositFileLocation</td><td>Path to CrossRef deposit files.</td></tr>
<tr><td>appSettings/DOISubmitLogFileLocation</td><td>Path to Crossref log files.</td></tr>
<tr><td>appSettings/OCRJobNewPath</td><td>Path to new OCR job files</td></tr>
<tr><td># appSettings/MQHost</td><td>Server address for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPort</td><td>Server port for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQAPIPort</td><td>Server port for a RabbitMQ API instance</td></tr>
<tr><td># appSettings/MQUsername</td><td>Username to access a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPassword</td><td>Password to access a RabbitMQ instance</td></tr>
<tr><td># appSettings/PregeneratedPdfLocation</td><td>File location of pregenerated article PDFs</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

<br/>

**DATA IMPORT APPS**

**BHLBioStorHarvest**

Service that harvests Segment metadata from APIs that are part of the BioStor platform (https://biostor.org/).

*&lt;BHLRoot&gt;\BHLBioStorHarvest\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project</td></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

<br/>

**BHLFlickrTagHarvest**

Service that examines the BHL Flickr collection (https://www.flickr.com/photos/biodivlibrary/) and downloads new and updated tags and notes into a database.

*&lt;BHLRoot&gt;\BHLFlickrTagHarvest\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/FlickrApiKey</td><td>Flickr API Key</td></tr>
<tr><td>appSettings/BHLFlickrUserID</td><td>Flickr username</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

<br/>

**IAAnalysisHarvest**

Service that obtains identifiers of Internet Archive (IA) items that should be harvested into BHL even though they are not part of the IA "biodiversity" collection.

*&lt;BHLRoot&gt;\IAAnalysisHarvest\App.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/IAAnalysis</td><td>Connection string for IAAnalysis database</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br/>

**IAHarvest**

Service that downloads metadata files for new and updated items hosted at Internet Archive.  It extracts the metadata from the files and adds it to database tables.  From there, it initiates procedures that clean the data and add it to the production database.

*&lt;BHLRoot&gt;\IAHarvest\App.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/LocalFileFolder</td><td>Local folder to hold downloaded files</td></tr>
<tr><td># appSettings/MQAddress</td><td>Server address for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPort</td><td>Server port for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQUser</td><td>Username to access a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPassword</td><td>Password to access a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQQueue</td><td>Name of a RabbitMQ queue for identifiers of new/updated items</td></tr>
<tr><td># appSettings/MQExchange</td><td>Name of a RabbitMQ exchange associated with the queue</td></tr>
<tr><td># appSettings/MQErrorQueue</td><td>Name of a RabbitMQ queue for messages that are not processed successfully</td></tr>
<tr><td># appSettings/MQErrorExchange</td><td>Name of a RabbitMQ exchange associated with the error queue</td></tr>
</table>

<br/>

**IAHarvestAsync**

Service that executes multiple instances of the IAHarvest process at one time, speeding up the overall process of downloading metadata files for new and updated items from Internet Archive.

*&lt;BHLRoot&gt;\IAHarvestAsync\App.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/LocalFileFolder</td><td>Local folder to hold downloaded files</td></tr>
</table>

<br/>

**BHLOAIHarvester**

Service that harvests metadata from OAI-PMH feeds and stores it in a BHL database.  From there, it initiates procedures that clean the data and add it to the production database.

*&lt;BHLRoot&gt;\BHLOAIHarvester\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
</table>

<br/>

**WDHarvest**

Service that downloads persistent identifiers associated with BHL entities in Wikidata.  Identifiers are added to the production database, reports are generated identifying newly added data and potential errors, and stakeholders are notified via email.

*&lt;BHLRoot&gt;\WDHarvest\App.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHLImport</td><td>Connection string for BHLImport database</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td># appSettings/AdminEmailToAddress</td><td>Process administrator recipient of report notifications sent by the process</td></tr>
<tr><td># appSettings/StaffEmailToAddress</td><td>Staff member recipients of report notifications sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

&nbsp;  
**UTILITY APPS**

**BHLDOIService**

Service that submits new and updated DOI metadata to Crossref, and updates the DOI metadata in BHL.

*&lt;BHLRoot&gt;\BHLDOIService\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/CrossRefDepositorName</td><td>Depositor name associated with CrossRef account</td></tr>
<tr><td>appSettings/CrossRefDepositorEmail</td><td>Depositor email associated with CrossRef account</td></tr>
<tr><td>appSettings/CrossRefLogin</td><td>Login for CrossRef account</td></tr>
<tr><td>appSettings/CrossRefPassword</td><td>Password for CrossRef account</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLExportProcessor**

Service that creates BHL data exports in a variety of formats, including BibTeX, MODS, RIS, KBART, and TSV.

*&lt;BHLRoot&gt;\BHLExportProcessor\App.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/RISTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/RISInternalSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/MODSInternalSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalTitleTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalTitleZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalItemTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalItemZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalSegmentTempFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalSegmentFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BibTeXInternalSegmentZipFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVDOIFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVAuthorFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVPageFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVPageNameFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVPartFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVPartAuthorFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVKeywordFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVTitleIdentifierFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalDOIFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalAuthorFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalItemFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalPageFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalPageNameFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalPartFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalPartAuthorFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalKeywordFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalTitleFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalTitleIdentifierFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/TSVInternalAuthorIdentifierFile</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
</table>

<br />

**BHLFlickrThumbGrab**

Service that downloads randomly selectly BHL images from Flickr for display on the BHL home page.

*&lt;BHLRoot&gt;\BHLFlickrThumbGrab\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/FlickrAPIKey</td><td>Flickr API key</td></tr>
<tr><td>appSettings/ImageFileName</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/ImageFolder</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/ImageListFilePath</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/DefaultFilesFolder</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLMETSUpload**

Service that generates METS files for new and modified BHL Items.  The METS files include bibliographic metadata and page-level metadata.  After generation they are uploaded to the item's Internet Archive folder.

*&lt;BHLRoot&gt;\BHLMETSUpload\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/METSEmail</td><td>Organization email address to place in METS files</td></tr>
<tr><td>appSettings/IAS3AccessKey</td><td>Internet Archive access key</td></tr>
<tr><td>appSettings/IAS3SecretKey</td><td>Internet Archive secret key</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLNameFileGenerator**

Service that generates XML files containing the scientific names in an item.  After generation they are uploaded to the item's Internet Archive folder.

*&lt;BHLRoot&gt;\BHLNameFileGenerator\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/IAS3AccessKey</td><td>Internet Archive access key</td></tr>
<tr><td>appSettings/IAS3SecretKey</td><td>Internet Archive secret key</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLOCRRefresh**

Service that downloads the DJVU file for an item from Internet Archive, parses it into individual text files (one per page), and replaces the item's existing page text files on the BHL search/file server.

*&lt;BHLRoot&gt;\BHLOCRRefresh\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/OcrJobNewPath</td><td>Path to new job files</td></tr>
<tr><td>appSettings/OcrJobProcessingPath</td><td>Path to job files being processed</td></tr>
<tr><td>appSettings/OcrJobCompletePath</td><td>Path to complete job files</td></tr>
<tr><td>appSettings/OcrJobErrorPath</td><td>Path to failed job files</td></tr>
<tr><td>appSettings/OcrJobTempPath</td><td>Path to temporary OCR files</td></tr>
<tr><td>appSettings/MQAddress</td><td>Message queue host URL</td></tr>
<tr><td>appSettings/MQPort</td><td>Message queue port</td></tr>
<tr><td>appSettings/MQUser</td><td>Message queue username</td></tr>
<tr><td>appSettings/MQPassword</td><td>Message queue password</td></tr>
<tr><td>appSettings/MQQueue</td><td>Name of message queue for items with updated text</td></tr>
<tr><td>appSettings/MQExchange</td><td>Name of MQ exchange for items with update dtext</td></tr>
<tr><td>appSettings/MQErrorQueue</td><td>Name of error message queue</td></tr>
<tr><td>appSettings/MQErrorExchange</td><td>Name of MQ error exchange</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLPageNameRefresh**

Service that invokes the Global Names gnfinder tool to identify scientific names in page text.  Identified names are added to the BHL database.

*&lt;BHLRoot&gt;\BHLPageNameRefresh\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLPDFGenerator**

Service that fulfills requests for custom PDFs.  Assembles the PDFs, saves them to the BHL search/file server, and emails the requestor a download link.

*&lt;BHLRoot&gt;\BHLPDFGenerator\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/PdfFilePath</td><td>Replace \\SERVER\FOLDER with valid path</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
</table>

<br />

**BHLSearchIndexer**

Service that reads messages from RabbitMQ queues and adds/updates/deletes the corresponding Elasticsearch records.

*&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.xml*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailToAddresses</td><td>Recipients of emails sent by the process (comma-separated)</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/ElasticSearchServerAddress</td><td>Search Server address, including port number</td></tr>
<tr><td>appSettings/MQAddress</td><td>Message queue host URL</td></tr>
<tr><td>appSettings/MQPort</td><td>Message queue port</td></tr>
<tr><td>appSettings/MQUser</td><td>Message queue username</td></tr>
<tr><td>appSettings/MQPassword</td><td>Message queue password</td></tr>
<tr><td>appSettings/MQQueueName</td><td>Name of message queue for items/authors/keywords</td></tr>
<tr><td>appSettings/MQErrorExchangeName</td><td>Name of MQ error exchange for items/authors/keywords</td></tr>
<tr><td>appSettings/MQErrorQueueName</td><td>Name of MQ error queue for items/authors/keywords</td></tr>
<tr><td>appSettings/DocFolder</td><td>Folder for debug output files</td></tr>
<tr><td>appSettings/OCRLocation</td><td>Set to “remote”</td></tr>
<tr><td># connectionStrings/Production</td><td>Connection string for production BHL database</td></tr>
<tr><td>connectionStrings/QA</td><td>Connection string for QA BHL database</td></tr>
</table>

*&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.Names.xml*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailToAddresses</td><td>Recipients of emails sent by the process (comma-separated)</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/ElasticSearchServerAddress</td><td>Search Server address, including port number</td></tr>
<tr><td>appSettings/MQAddress</td><td>Message queue host URL</td></tr>
<tr><td>appSettings/MQPort</td><td>Message queue port</td></tr>
<tr><td>appSettings/MQUser</td><td>Message queue username</td></tr>
<tr><td>appSettings/MQPassword</td><td>Message queue password</td></tr>
<tr><td>appSettings/MQQueueName</td><td>Name of message queue for names</td></tr>
<tr><td>appSettings/MQErrorExchangeName</td><td>Name of MQ error exchange for names</td></tr>
<tr><td>appSettings/MQErrorQueueName</td><td>Name of MQ error queue for names</td></tr>
<tr><td>appSettings/DocFolder</td><td>Folder for debug output files</td></tr>
<tr><td>appSettings/OCRLocation</td><td>Set to “remote”</td></tr>
<tr><td># connectionStrings/Production</td><td>Connection string for production BHL database</td></tr>
<tr><td>connectionStrings/QA</td><td>Connection string for QA BHL database</td></tr>
</table>

*&lt;BHLRoot&gt;\BHLSearchIndexer\AppConfig.Full.xml*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/EmailToAddresses</td><td>Recipients of emails sent by the process (comma-separated)</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>appSettings/ElasticSearchServerAddress</td><td>Search Server address, including port number</td></tr>
<tr><td>appSettings/DocFolder</td><td>Folder for debug output files</td></tr>
<tr><td>appSettings/OCRLocation</td><td>Set to “remote”</td></tr>
<tr><td>appSettings/DoFullIndex</td><td>Set to “true”</td></tr>
<tr><td># connectionStrings/Production</td><td>Connection string for production BHL database</td></tr>
<tr><td>connectionStrings/QA</td><td>Connection string for QA BHL database</td></tr>
</table>

<br />

**BHLSearchIndexQueueLoad**

Service that queries the database to identify recently changed entities (titles, items, segments, authors, keywords, names), and adds messages for each changed entity to RabbitMQ queues.  FOr changed segments, it also adds messages to a RabbitMQ queue for pre-generated PDFs.

*&lt;BHLRoot&gt;\BHLSearchIndexQueueLoad\AppConfig.xml*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>appSettings/MQAddress</td><td>Message queue host URL</td></tr>
<tr><td>appSettings/MQPort</td><td>Message queue port</td></tr>
<tr><td>appSettings/MQUser</td><td>Message queue username</td></tr>
<tr><td>appSettings/MQPassword</td><td>Message queue password</td></tr>
<tr><td>appSettings/MQQueue</td><td>Name of message queue for items/authors/keywords</td></tr>
<tr><td>appSettings/MQExchange</td><td>Name of MQ exchange for items/authors/keywords</td></tr>
<tr><td>appSettings/MQErrorExchange</td><td>Name of MQ error exchange for items/authors/keywords</td></tr>
<tr><td>appSettings/MQErrorQueue</td><td>Name of MQ error queue for items/authors/keywords</td></tr>
<tr><td>appSettings/MQQueueNames</td><td>Name of MQ queue for names</td></tr>
<tr><td>appSettings/MQErrorExchangeNames</td><td>Name of MQ error exchange for names</td></tr>
<tr><td>appSettings/MQErrorQueueNames</td><td>Name of MQ error queue for names</td></tr>
<tr><td>appSettings/MQQueuePDF</td><td>Name of MQ queue for pre-generated PDFs</td></tr>
<tr><td>appSettings/MQErrorExchangePDF</td><td>Name of MQ error exchange for pre-generated PDFs</td></tr>
<tr><td>appSettings/MQErrorQueuePDF</td><td>Name of MQ error queue for pre-generated PDFs</td></tr>
<tr><td># appSettings/EmailToAddresses</td><td>Recipients of emails sent by the process (comma-separated)</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td>connectionStrings/Production</td><td>Connection string for production BHL database</td></tr>
<tr><td># connectionStrings/QA</td><td>Connection string for QA BHL database</td></tr>
</table>

<br />

**BHLTextImportProcessor**

Service that parses uploaded files containing page transcripts and replaces existing page text files on the BHL search/file server.

*&lt;BHLRoot&gt;\BHLTextImportProcessor\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td># appSettings/DebugPath</td><td>Path for debugging output</td></tr>
<tr><td># appSettings/EmailFromAddress</td><td>"From" address for emails sent by the process</td></tr>
<tr><td># appSettings/EmailToAddress</td><td>Recipient of emails sent by the process</td></tr>
<tr><td>appSettings/TextImportFilePath</td><td>URL of location of text import files</td></tr>
<tr><td>appSettings/BHLWSUrl</td><td>URL for a running instance of the BHLWebServiceREST.v1 project.</td></tr>
<tr><td># appSettings/MQAddress</td><td>Server address for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPort</td><td>Server port for a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQUser</td><td>Username to access a RabbitMQ instance</td></tr>
<tr><td># appSettings/MQPassword</td><td>Password to access a RabbitMQ instance</td></tr>
<tr><td>appSettings/MQQueue</td><td>Name of message queue for items with updated text</td></tr>
<tr><td>appSettings/MQExchange</td><td>Name of MQ exchange for items with updated text</td></tr>
<tr><td>appSettings/MQErrorQueue</td><td>Name of error message queue</td></tr>
<tr><td>appSettings/MQErrorExchange</td><td>Name of MQ error exchange</td></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

**TEST PROJECTS**

**BHLAPIDALTest**

Unit tests for API data access methods.

*&lt;BHLRoot&gt;\BHLApiDALTest\testhost.dll.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td>connectionstrings/Admin</td><td>Optional connection string for logging database</td></</table>

<br/>

**BHLCoreDALTest**

Unit tests for core data access methods.

*&lt;BHLRoot&gt;\BHLCoreDALTest\testhost.dll.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
</table>

<br/>

**BHLServerTest**

Unit tests for business rule methods.

*&lt;BHLRoot&gt;\BHLServerTest\testhost.dll.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>connectionStrings/BHL</td><td>Connection string for BHL database</td></tr>
<tr><td>connectionstrings/Admin</td><td>Optional connection string for logging database</td></tr>
</table>

<br/>

**SearchElasticTest**

Unit tests for methods that interact with ElasticSearch.

*&lt;BHLRoot&gt;\SearchElasticTest\app.config*

<table>
<tr><th>Element</th><th>Value</th></tr>
<tr><td>appSettings/ElasticSearchServerAddress</td><td>Server address for the ElasticSearch instance</td></tr>
</table>

Index Data in ElasticSearch (Optional)
--------------------------------------

1) Open the BHLUtility solution in Visual Studio.
2) Build the BHLSearchIndexer project.
3) Make sure you have completed the configuration within the AppConfig.Full.xml file.
4) Run the BHLSearchIndex project, specifying “AppConfig.Full.xml” as the application argument.
