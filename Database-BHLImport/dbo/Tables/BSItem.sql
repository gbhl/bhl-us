CREATE TABLE [dbo].[BSItem] (
    [ItemID]           INT      IDENTITY (1, 1) NOT NULL,
    [BHLItemID]        INT      NULL,
    [ItemStatusID]     INT      CONSTRAINT [DF_BSItem_IsLoaded] DEFAULT ((10)) NOT NULL,
    [CreationDate]     DATETIME CONSTRAINT [DF_BSItem_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME CONSTRAINT [DF_BSItem_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BSItem] PRIMARY KEY CLUSTERED ([ItemID] ASC),
    CONSTRAINT [FK_BSItem_BSItemStatus] FOREIGN KEY ([ItemStatusID]) REFERENCES [dbo].[BSItemStatus] ([ItemStatusID])
);

