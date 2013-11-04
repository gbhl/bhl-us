CREATE TABLE dbo.OAIRecordStatus
	(
	OAIRecordStatusID int NOT NULL CONSTRAINT PK_OAIRecordStatus PRIMARY KEY CLUSTERED,
	RecordStatus nvarchar(30) NOT NULL DEFAULT(''),
	StatusDescription nvarchar(400) NOT NULL DEFAULT('')
	)
GO