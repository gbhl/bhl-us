CREATE TABLE dbo.OAIRecord
	(
	OAIRecordID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecord PRIMARY KEY CLUSTERED,
	HarvestLogID int NOT NULL,
	OAIIdentifier nvarchar(100) NOT NULL DEFAULT(''),
	OAIDateStamp nvarchar(30) NOT NULL DEFAULT(''),
	OAIStatus nvarchar(20) NOT NULL DEFAULT(''),
	RecordType nvarchar(20) NOT NULL DEFAULT(''),
	Title nvarchar(2000) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL DEFAULT(''),
	ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
	Contributor nvarchar(200) NOT NULL DEFAULT(''),
	[Date] nvarchar(20) NOT NULL DEFAULT(''),
	[Language] nvarchar(30) NOT NULL DEFAULT(''),
	Publisher nvarchar(250) NOT NULL DEFAULT(''),
	PublicationPlace nvarchar(150) NOT NULL DEFAULT(''),
	PublicationDate nvarchar(100) NOT NULL DEFAULT(''),
	Edition nvarchar(450) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	Issue nvarchar(100) NOT NULL DEFAULT(''),
	StartPage nvarchar(20) NOT NULL DEFAULT(''),
	EndPage nvarchar(20) NOT NULL DEFAULT(''),
	CallNumber nvarchar(100) NOT NULL DEFAULT(''),
	Issn nvarchar(125) NOT NULL DEFAULT(''),
	Isbn nvarchar(125) NOT NULL DEFAULT(''),
	Lccn nvarchar(125) NOT NULL DEFAULT(''),
	Doi nvarchar(50) NOT NULL DEFAULT(''),
	Url nvarchar(200) NOT NULL DEFAULT(''),
	OAIRecordStatusID int NOT NULL,
	ProductionTitleID int NULL,
	ProductionItemID int NULL,
	ProductionSegmentID int NULL,
	CreationDate datetime NOT NULL DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT FK_OAIRecord_OAIHarvestLog 
		FOREIGN KEY (HarvestLogID) REFERENCES dbo.OAIHarvestLog(HarvestLogID),
	CONSTRAINT FK_OAIRecord_OAIRecordStatus
		FOREIGN KEY (OAIRecordStatusID)	REFERENCES dbo.OAIRecordStatus(OAIRecordStatusID)
	)
GO

CREATE NONCLUSTERED INDEX [IX_OAIRecord_HarvestLogID] ON [dbo].[OAIRecord]
(
	[HarvestLogID] ASC
)
INCLUDE ([OAIRecordID]) 
GO

CREATE NONCLUSTERED INDEX [IX_OAIRecord_RecordStatusSegment] ON [dbo].[OAIRecord]
(
	[OAIRecordStatusID] ASC,
	[ProductionSegmentID] ASC,
	[RecordType] ASC
)
INCLUDE ([OAIRecordID], [HarvestLogID]) 
GO

CREATE NONCLUSTERED INDEX [IX_OAIRecord_RecordStatusTitleItem] ON [dbo].[OAIRecord]
(
	[OAIRecordStatusID] ASC,
	[ProductionTitleID] ASC,
	[ProductionItemID] ASC,
	[RecordType] ASC
)
INCLUDE ([OAIRecordID], [HarvestLogID]) 
GO

CREATE NONCLUSTERED INDEX [IX_OAIRecord_OAIIdentifierDateStamp]
	ON [dbo].[OAIRecord] ([OAIIdentifier], [OAIDateStamp]);
GO

