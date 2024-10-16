CREATE TABLE servlog.ServiceLog
	(
	ServiceLogID int IDENTITY(1,1)
	,ServiceID int NOT NULL
	,SeverityID int NOT NULL
	,ErrorNumber int NULL
	,[Procedure] nvarchar(500) CONSTRAINT DF_ServiceLog_Procedure DEFAULT('') NOT NULL
	,Line int NULL
	,[Message] nvarchar(max) CONSTRAINT DF_ServiceLog_Message DEFAULT('') NOT NULL
	,StackTrace nvarchar(max) CONSTRAINT DF_ServiceLog_StackTrace DEFAULT('') NOT NULL
	,CreationDate datetime CONSTRAINT DF_ServiceLog_CreationDate DEFAULT GETDATE() NOT NULL
    ,CONSTRAINT [PK_ServLogServiceLog] PRIMARY KEY CLUSTERED ([ServiceLogID] ASC)
	)
GO
