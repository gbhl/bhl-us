CREATE TABLE dbo.SearchIndexQueueLog (
	SearchIndexQueueLogID INT IDENTITY(1,1) NOT NULL,
	LastAuditBasicID INT NOT NULL,
	LastAuditDate DATETIME NOT NULL,
	NumberQueued INT NOT NULL,
	LogDate DATETIME CONSTRAINT [DF_SearchIndexQueueLog_LogDate] DEFAULT GETDATE() NOT NULL,
	CONSTRAINT [PK_SearchIndexQueueLog] PRIMARY KEY CLUSTERED ([SearchIndexQueueLogID] ASC)
)
GO
