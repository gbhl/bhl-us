CREATE TABLE dbo.OAIRecordRelatedTitle
	(
	OAIRecordRelatedTitleID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecordRelatedTitle PRIMARY KEY CLUSTERED,
	OAIRecordID int NOT NULL,
	TitleType nvarchar(50) NOT NULL DEFAULT(''),
	Title nvarchar(300) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL DEFAULT(''),
	ProductionEntityType nvarchar(15) NULL,
	ProductionEntityID int NULL,
	CreationDate datetime NOT NULL DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT FK_OAIRecordRelatedTitle_OAIRecord
		FOREIGN KEY (OAIRecordID) REFERENCES dbo.OAIRecord(OAIRecordID)
	)
GO