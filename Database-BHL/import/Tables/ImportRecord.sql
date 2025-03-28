﻿CREATE TABLE import.ImportRecord
	(
	ImportRecordID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecord PRIMARY KEY,
	ImportFileID int NOT NULL,
	ImportRecordStatusID int NOT NULL,
	TitleID int NULL,
	ItemID int NULL,
	SegmentID int NULL,
	ImportSegmentID int NULL,
	Genre nvarchar(50) NOT NULL CONSTRAINT DF_ImportRecord_Genre DEFAULT(''),
	Title nvarchar(2000) NOT NULL CONSTRAINT DF_ImportRecord_Title DEFAULT(''),
	TranslatedTitle nvarchar(2000) NOT NULL CONSTRAINT DF_ImportRecord_TranslatedTitle DEFAULT(''),
	JournalTitle nvarchar(2000) NOT NULL CONSTRAINT DF_ImportRecord_JournalTitle DEFAULT(''),
	Volume nvarchar(100) NOT NULL CONSTRAINT DF_ImportRecord_Volume DEFAULT(''),
	Series nvarchar(100) NOT NULL CONSTRAINT DF_ImportRecord_Series DEFAULT(''),
	Issue nvarchar(100) NOT NULL CONSTRAINT DF_ImportRecord_Issue DEFAULT(''),
	Edition nvarchar(400) NOT NULL CONSTRAINT DF_ImportRecord_Edition DEFAULT(''),
	PublicationDetails nvarchar(400) NOT NULL CONSTRAINT DF_ImportRecord_PublicationDetails DEFAULT(''),
	PublisherName nvarchar(250) NOT NULL CONSTRAINT DF_ImportRecord_PublisherName DEFAULT(''),
	PublisherPlace nvarchar(150) NOT NULL CONSTRAINT DF_ImportRecord_PublisherPlace DEFAULT(''),
	Year nvarchar(20) NOT NULL CONSTRAINT DF_ImportRecord_Year DEFAULT(''),
	StartYear smallint NULL,
	EndYear smallint NULL,
	Language nvarchar(30) NULL,
	Summary nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_Summary DEFAULT(''),
	Notes nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_Notes DEFAULT(''),
	Rights nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_Rights DEFAULT(''),
	DueDiligence nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_DueDiligence DEFAULT(''),
	CopyrightStatus nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_CopyrightStatus DEFAULT(''),
	License nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecord_License DEFAULT(''),
	LicenseUrl nvarchar(200) NOT NULL CONSTRAINT DF_ImportRecord_LicenseUrl DEFAULT(''),
	PageRange nvarchar(50) NOT NULL CONSTRAINT DF_ImportRecord_PageRange DEFAULT(''),
	StartPage nvarchar(20) NOT NULL CONSTRAINT DF_ImportRecord_StartPage DEFAULT(''),
	StartPageID int NULL,
	EndPage nvarchar(20) NOT NULL CONSTRAINT DF_ImportRecord_EndPage DEFAULT(''),
	EndPageID int NULL,
	Url nvarchar(200) NOT NULL CONSTRAINT DF_ImportRecord_Url DEFAULT(''),
	DownloadUrl nvarchar(200) NOT NULL CONSTRAINT DF_ImportRecord_DownloadUrl DEFAULT(''),
	DOI nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_DOI DEFAULT(''),
	ISSN nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_ISSN DEFAULT(''),
	ISBN nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_ISBN DEFAULT(''),
	OCLC nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_OCLC DEFAULT(''),
	LCCN nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_LCCN DEFAULT(''),
	ARK nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_ARK DEFAULT(''),
	Biostor nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_Biostor DEFAULT(''),
	JSTOR nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_JSTOR DEFAULT(''),
	TL2 nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_TL2 DEFAULT(''),
	Wikidata nvarchar(125) NOT NULL CONSTRAINT DF_ImportRecord_Wikidata DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecord_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecord_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecord_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecord_LastModifiedUserID DEFAULT(1),
	CONSTRAINT FK_ImportRecord_ImportFile FOREIGN KEY (ImportFileID) 
			REFERENCES import.ImportFile (ImportFileID),
	CONSTRAINT FK_ImportFile_ImportRecordStatus FOREIGN KEY (ImportRecordStatusID) 
			REFERENCES import.ImportRecordStatus (ImportRecordStatusID)
	)
GO

CREATE NONCLUSTERED INDEX IX_ImportRecord_ImportFileID ON [import].[ImportRecord] 
(
	ImportFileID
)
INCLUDE 
(
	ImportRecordID,ImportRecordStatusID,Title,JournalTitle,Volume,Issue,[Year],StartPage,ItemID
)
GO
