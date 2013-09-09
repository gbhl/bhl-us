CREATE TABLE [dbo].[BSItemStatus] (
    [ItemStatusID]     INT             NOT NULL,
    [Status]           NVARCHAR (30)   CONSTRAINT [DF_BSItemStatus_Status] DEFAULT ('') NOT NULL,
    [Description]      NVARCHAR (4000) CONSTRAINT [DF_BSItemStatus_Description] DEFAULT ('') NOT NULL,
    [CreationDate]     DATETIME        CONSTRAINT [DF_BSItemStatus_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_BSItemStatus_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BSItemStatus] PRIMARY KEY CLUSTERED ([ItemStatusID] ASC)
);

