CREATE TABLE dbo.OAIPublishError
	(
	OAIPublishErrorID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OAIPublishError PRIMARY KEY,
	OAIRecordID int NULL,
	ErrorDate datetime NOT NULL CONSTRAINT DF_OAIPublishError_ErrorDate DEFAULT (GETDATE()),
	Number int NULL,
	Severity int NULL,
	[State] int NULL,
	[Procedure] nvarchar(126) NULL,
	Line int NULL,
	[Message] nvarchar(4000) NULL
	)
GO