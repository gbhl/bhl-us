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

ALTER TABLE servlog.ServiceLog WITH NOCHECK
    ADD CONSTRAINT FK_ServLogServiceLog_Service FOREIGN KEY (ServiceID) REFERENCES servlog.[Service] (ServiceID);
GO

ALTER TABLE servlog.ServiceLog WITH NOCHECK
    ADD CONSTRAINT FK_ServLogServiceLog_Severity FOREIGN KEY (SeverityID) REFERENCES servlog.Severity (SeverityID);
GO

CREATE NONCLUSTERED INDEX IDX_ServLogServiceLog_CreationDate ON [servlog].[ServiceLog] 
(
	[CreationDate] ASC
)
INCLUDE
(
	[ServiceID],
	[SeverityID]
)
GO
