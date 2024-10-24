CREATE TABLE [reqlog].[RequestHistory] (
    [RequestHistoryID] INT      IDENTITY (1, 1) NOT NULL,
    [ApplicationID]    INT      NOT NULL,
    [RequestDate]      DATETIME NOT NULL,
    [NumRequests]      INT      NOT NULL,
    CONSTRAINT [PK_RequestHistory] PRIMARY KEY CLUSTERED ([RequestHistoryID] ASC)
)
GO
ALTER TABLE [reqlog].[RequestHistory] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestHistory_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [reqlog].[Application] ([ApplicationID]);


GO
ALTER TABLE [reqlog].[RequestHistory] CHECK CONSTRAINT [FK_RequestHistory_Application]
GO
