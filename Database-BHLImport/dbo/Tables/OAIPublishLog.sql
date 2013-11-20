CREATE TABLE dbo.OAIPublishLog
	(
	OAIPublishLogID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIPublishLog PRIMARY KEY,
	PublishDate datetime NOT NULL CONSTRAINT DF_OAIPublishLog_PublishDate DEFAULT (GETDATE()),
	HarvestLogID int NULL,
	PublishResult nvarchar(50) NOT NULL CONSTRAINT DF_OAIPublishLog_PublishResult DEFAULT(''),
	TotalInsert int NOT NULL CONSTRAINT DF_OAIPublishLog_TotalInsert DEFAULT(0),
	TotalUpdate int NOT NULL CONSTRAINT DF_OAIPublishLog_TotalUpdate DEFAULT(0),
	TotalDelete int NOT NULL CONSTRAINT DF_OAIPublishLog_TotalDelete DEFAULT(0)
	)
GO