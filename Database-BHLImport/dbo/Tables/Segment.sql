CREATE TABLE dbo.Segment (
	SegmentID int IDENTITY(1,1) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID int NULL,
	BarCode nvarchar(40) NOT NULL,
	SequenceOrder smallint NOT NULL CONSTRAINT DF_Segment_SequenceOrder DEFAULT ((1)),
	SegmentStatusID int NOT NULL,
	SegmentGenreID int NOT NULL,
	Title nvarchar(2000) NOT NULL CONSTRAINT DF_Segment_Title DEFAULT (''),
	TranslatedTitle nvarchar(2000) NOT NULL CONSTRAINT DF_Segment_TranslatedTitle DEFAULT (''),
	SortTitle nvarchar(2000) NOT NULL CONSTRAINT DF_Segment_SortTitle DEFAULT (''),
	ContainerTitle nvarchar(2000) NOT NULL CONSTRAINT DF_Segment_ContainerTitle DEFAULT (''),
 	PublicationDetails nvarchar(400) NOT NULL CONSTRAINT DF_Segment_PublicationDetails DEFAULT (''),
	PublisherName nvarchar(250) NOT NULL CONSTRAINT DF_Segment_PublisherName DEFAULT (''),
	PublisherPlace nvarchar(150) NOT NULL CONSTRAINT DF_Segment_PublisherPlace DEFAULT (''),
	Notes nvarchar(max) NOT NULL CONSTRAINT DF_Segment_Notes DEFAULT (''),
	Summary nvarchar(max) NOT NULL CONSTRAINT DF_Segment_Summary DEFAULT (''),
	Volume nvarchar(100) NOT NULL CONSTRAINT DF_Segment_Volume DEFAULT (''),
	Series nvarchar(100) NOT NULL CONSTRAINT DF_Segment_Series DEFAULT (''),
	Issue nvarchar(100) NOT NULL CONSTRAINT DF_Segment_Issue DEFAULT (''),
	Edition nvarchar(400) NOT NULL CONSTRAINT DF_Segment_Edition DEFAULT (''),
	[Date] nvarchar(20) NOT NULL CONSTRAINT DF_Segment_Date DEFAULT (''),
	PageRange nvarchar(50) NOT NULL CONSTRAINT DF_Segment_PageRange DEFAULT (''),
	StartPageNumber nvarchar(20) NOT NULL CONSTRAINT DF_Segment_StartPageNumber DEFAULT (''),
	EndPageNumber nvarchar(20) NOT NULL CONSTRAINT DF_Segment_EndPageNumber DEFAULT (''),
	StartPageID int NULL,
	InstitutionCode nvarchar(10) NULL,
	LanguageCode nvarchar(10) NULL,
	[Url] nvarchar(200) NOT NULL CONSTRAINT DF_Segment_Url DEFAULT (''),
	DownloadUrl nvarchar(200) NOT NULL CONSTRAINT DF_Segment_DownloadUrl DEFAULT (''),
	RightsStatus nvarchar(500) NOT NULL CONSTRAINT DF_Segment_RightsStatus DEFAULT (''),
	RightsStatement nvarchar(500) NOT NULL CONSTRAINT DF_Segment_RightsStatement DEFAULT (''),
	LicenseName nvarchar(200) NOT NULL CONSTRAINT DF_Segment_LicenseName DEFAULT (''),
	LicenseUrl nvarchar(200) NOT NULL CONSTRAINT DF_Segment_LicenseUrl DEFAULT (''),
	ProductionDate datetime NULL,
	CreationDate datetime NOT NULL CONSTRAINT DF_Segment_CreationDate DEFAULT (getdate()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_Segment_LastModifiedDate DEFAULT (getdate()),
	CONSTRAINT PK_Segment PRIMARY KEY CLUSTERED ( SegmentID ASC )
)
GO

ALTER TABLE dbo.Segment ADD CONSTRAINT FK_Segment_ImportSource 
	FOREIGN KEY(ImportSourceID) REFERENCES dbo.ImportSource (ImportSourceID)
GO

ALTER TABLE dbo.Segment ADD CONSTRAINT FK_Segment_ImportStatus 
	FOREIGN KEY(ImportStatusID) REFERENCES dbo.ImportStatus (ImportStatusID)
GO
