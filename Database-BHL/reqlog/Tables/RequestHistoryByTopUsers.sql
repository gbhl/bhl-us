CREATE TABLE [reqlog].[RequestHistoryByTopUsers] (
    [RequestHistoryByTopUsers] INT          IDENTITY (1, 1) NOT NULL,
    [ApplicationID]            INT          NOT NULL,
    [RequestDate]              DATETIME     NOT NULL,
    [RankForDate]              INT          NOT NULL,
    [IPAddress]                VARCHAR (15) NULL,
    [UserID]                   INT          NULL,
    [NumRequests]              INT          NOT NULL,
    CONSTRAINT [PK_RequestHistoryByTopUsers] PRIMARY KEY CLUSTERED ([RequestHistoryByTopUsers] ASC)
)
GO
ALTER TABLE [reqlog].[RequestHistoryByTopUsers] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestHistoryByTopUsers_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [reqlog].[Application] ([ApplicationID]);
GO
ALTER TABLE [reqlog].[RequestHistoryByTopUsers] CHECK CONSTRAINT [FK_RequestHistoryByTopUsers_Application]
GO
