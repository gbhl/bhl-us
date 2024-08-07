CREATE TABLE [reqlog].[RequestLogArchive] (
    [RequestLogID]  INT            NOT NULL,
    [ApplicationID] INT            NOT NULL,
    [IPAddress]     VARCHAR (15)   NULL,
    [UserID]        INT            NULL,
    [CreationDate]  DATETIME       NOT NULL,
    [RequestTypeID] INT            NOT NULL,
    [Detail]        VARCHAR (2000) NULL,
    CONSTRAINT [PK_RequestLogArchive] PRIMARY KEY CLUSTERED ([RequestLogID] ASC)
)
GO
