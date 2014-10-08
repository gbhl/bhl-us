CREATE TABLE dbo.OAIRecordSubject
	(
	OAIRecordSubjectID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecordSubject PRIMARY KEY CLUSTERED,
	OAIRecordID int NOT NULL,
	Keyword nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL DEFAULT(''),
	ProductionKeywordID int NULL,
	CreationDate datetime NOT NULL DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT FK_OAIRecordSubject_OAIRecord
		FOREIGN KEY (OAIRecordID) REFERENCES dbo.OAIRecord(OAIRecordID)
	)
GO
CREATE NONCLUSTERED INDEX [IX_OAIRecordSubject_OAIRecordID] ON [dbo].[OAIRecordSubject]
(
	[OAIRecordID] ASC
)
INCLUDE ([OAIRecordSubjectID], [Keyword]) 
GO
