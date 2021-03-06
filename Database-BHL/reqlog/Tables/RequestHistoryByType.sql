CREATE TABLE [reqlog].[RequestHistoryByType] (
    [RequestHistoryByTypeID] INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationID]          INT      NOT NULL,
    [RequestDate]            DATETIME NOT NULL,
    [RequestTypeID]          INT      NOT NULL,
    [NumRequests]            INT      NOT NULL,
    CONSTRAINT [PK_RequestHistoryByType] PRIMARY KEY CLUSTERED ([RequestHistoryByTypeID] ASC)
)
GO
ALTER TABLE [reqlog].[RequestHistoryByType] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestHistoryByType_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [reqlog].[Application] ([ApplicationID]);
GO
ALTER TABLE [reqlog].[RequestHistoryByType] CHECK CONSTRAINT [FK_RequestHistoryByType_Application]
GO
ALTER TABLE [reqlog].[RequestHistoryByType] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestHistoryByType_RequestType] FOREIGN KEY ([RequestTypeID]) REFERENCES [reqlog].[RequestType] ([RequestTypeID]);
GO
ALTER TABLE [reqlog].[RequestHistoryByType] CHECK CONSTRAINT [FK_RequestHistoryByType_RequestType]
GO
