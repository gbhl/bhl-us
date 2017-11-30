CREATE TABLE [dbo].[DOIStatus] (
    [DOIStatusID]          INT             NOT NULL,
    [DOIStatusName]        NVARCHAR (30)   CONSTRAINT [DF_DOIStatus_DOIStatusName] DEFAULT ('') NOT NULL,
    [DOIStatusDescription] NVARCHAR (2000) CONSTRAINT [DF_DOIStatus_DOIStatusDescription] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_DOIStatus] PRIMARY KEY CLUSTERED ([DOIStatusID] ASC)
);


GO
