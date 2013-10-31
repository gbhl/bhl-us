﻿CREATE TABLE dbo.OAIRecordSubject
	(
	OAIRecordSubjectID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecordSubject PRIMARY KEY CLUSTERED,
	OAIRecordID int NOT NULL,
	Keyword nvarchar(50) NOT NULL DEFAULT(''),
	ProductionKeywordID int NULL,
	CreationDate datetime NOT NULL DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL DEFAULT(GETDATE()),
	CONSTRAINT FK_OAIRecordSubject_OAIRecord
		FOREIGN KEY (OAIRecordID) REFERENCES dbo.OAIRecord(OAIRecordID)
	)
GO
