CREATE TABLE [reqlog].[RequestLog] (
    [RequestLogID]  INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationID] INT            NOT NULL,
    [IPAddress]     VARCHAR (15)   NULL,
    [UserID]        INT            NULL,
    [CreationDate]  DATETIME       NOT NULL,
    [RequestTypeID] INT            NOT NULL,
    [Detail]        VARCHAR (2000) NULL,
    CONSTRAINT [PK_RequestLog] PRIMARY KEY CLUSTERED ([RequestLogID] ASC)
)
GO
ALTER TABLE [reqlog].[RequestLog] ADD  CONSTRAINT [DF_RequestLog_RequestDateTime]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [reqlog].[RequestLog] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestLog_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [reqlog].[Application] ([ApplicationID]);
GO
ALTER TABLE [reqlog].[RequestLog] CHECK CONSTRAINT [FK_RequestLog_Application]
GO
ALTER TABLE [reqlog].[RequestLog] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestLog_RequestType] FOREIGN KEY ([RequestTypeID]) REFERENCES [reqlog].[RequestType] ([RequestTypeID]);
GO
ALTER TABLE [reqlog].[RequestLog] CHECK CONSTRAINT [FK_RequestLog_RequestType]
GO
