CREATE TABLE dbo.OAIRecordCreator
	(
	OAIRecordCreatorID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecordCreator PRIMARY KEY CLUSTERED,
	OAIRecordID int NOT NULL,
	CreatorType nvarchar(50) NOT NULL DEFAULT(''),
	FullName nvarchar(300) NOT NULL DEFAULT(''),
	Dates nvarchar(50) NOT NULL DEFAULT(''),
	StartDate nvarchar(25) NOT NULL DEFAULT(''),
	EndDate nvarchar(25) NOT NULL DEFAULT(''),
	ProductionAuthorID int NULL,
	CreationDate datetime NOT NULL DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT FK_OAIRecordCreator_OAIRecord
		FOREIGN KEY (OAIRecordID) REFERENCES dbo.OAIRecord(OAIRecordID)
	)
GO