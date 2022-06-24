CREATE TABLE dbo.OAIRecordCreatorIdentifier
(
	OAIRecordCreatorIdentifierID int IDENTITY(1,1) NOT NULL,
	OAIRecordCreatorID int NOT NULL,
	IdentifierType nvarchar(40) CONSTRAINT DF_OAIRecordCreatorIdentifier_IdentifierType DEFAULT ('') NOT NULL,
	IdentifierValue nvarchar(125) CONSTRAINT DF_OAIRecordCreatorIdentifier_IdentifierValue DEFAULT ('') NOT NULL,
	CreationDate datetime CONSTRAINT DF_OAIRecordCreatorIdentifier_CreationDate DEFAULT (GETDATE()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_OAIRecordCreatorIdentifier_LastMmodifiedDate DEFAULT (GETDATE()) NOT NULL,
	CONSTRAINT PK_OAIRecordCreatorIdentifier PRIMARY KEY CLUSTERED 
	(
		OAIRecordCreatorIdentifierID ASC
	)
)
GO

ALTER TABLE dbo.OAIRecordCreatorIdentifier ADD CONSTRAINT FK_OAIRecordCreatorIdentifier_OAIRecordCreator
	FOREIGN KEY(OAIRecordCreatorID) REFERENCES dbo.OAIRecordCreator (OAIRecordCreatorID)
GO

CREATE NONCLUSTERED INDEX IX_OAIRecordCreatorIdentifier_OAIRecordCreatorID ON dbo.OAIRecordCreatorIdentifier
(
	OAIRecordCreatorID ASC
)
GO
