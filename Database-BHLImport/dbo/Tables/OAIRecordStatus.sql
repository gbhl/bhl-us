CREATE TABLE dbo.OAIRecordStatus
	(
	OAIRecordStatusID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIRecordStatus PRIMARY KEY CLUSTERED,
	RecordStatus nvarchar(30) NOT NULL DEFAULT(''),
	StatusDescription nvarchar(400) NOT NULL DEFAULT('')
	)
GO