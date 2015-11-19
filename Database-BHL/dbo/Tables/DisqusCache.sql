CREATE TABLE [dbo].[DisqusCache] (
    [DisqusCacheID]	INT	IDENTITY (1, 1) NOT NULL,
    [ItemID] INT NOT NULL,
    [PageID] INT NOT NULL,
    [Count] INT NOT NULL,
	[CreationDate]	DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_DisqusCache] PRIMARY KEY CLUSTERED ([DisqusCacheID] ASC),
    CONSTRAINT [FK_DisqusCache_ItemID] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]),
    CONSTRAINT [KF_DisqusCache_PageID] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID])
);