CREATE TABLE [reqlog].[RequestType] (
    [RequestTypeID]   INT          NOT NULL,
    [RequestTypeName] VARCHAR (50) NOT NULL,
    [ApplicationID]   INT          NOT NULL,
    CONSTRAINT [PK_RequestType] PRIMARY KEY CLUSTERED ([RequestTypeID] ASC)
)
GO
ALTER TABLE [reqlog].[RequestType] ADD  CONSTRAINT [DF_RequestType_ApplicationID]  DEFAULT ((1)) FOR [ApplicationID]
GO
ALTER TABLE [reqlog].[RequestType] WITH NOCHECK
    ADD CONSTRAINT [FK_RequestType_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [reqlog].[Application] ([ApplicationID]);
GO
ALTER TABLE [reqlog].[RequestType] CHECK CONSTRAINT [FK_RequestType_Application]
GO
