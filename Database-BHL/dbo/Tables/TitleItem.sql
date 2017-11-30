CREATE TABLE [dbo].[TitleItem] (
    [TitleItemID]        INT      IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT      NOT NULL,
    [ItemID]             INT      NOT NULL,
    [ItemSequence]       SMALLINT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_TitleItem_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_TitleItem_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_TitleItem_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_TitleItem_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleItem] PRIMARY KEY CLUSTERED ([TitleItemID] ASC),
    CONSTRAINT [FK_TitleItem_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]),
    CONSTRAINT [FK_TitleItem_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TitleItem]
    ON [dbo].[TitleItem]([TitleID] ASC, [ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TitleItemItemID]
    ON [dbo].[TitleItem]([ItemID] ASC)
    INCLUDE([TitleID], [ItemSequence]);


GO
