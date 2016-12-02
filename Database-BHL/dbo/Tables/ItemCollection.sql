CREATE TABLE [dbo].[ItemCollection] (
    [ItemCollectionID] INT      IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT      NOT NULL,
    [CollectionID]     INT      NOT NULL,
    [CreationDate]     DATETIME CONSTRAINT [DF_ItemCollection_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemCollection] PRIMARY KEY CLUSTERED ([ItemCollectionID] ASC),
    CONSTRAINT [FK_ItemCollection_Collection] FOREIGN KEY ([CollectionID]) REFERENCES [dbo].[Collection] ([CollectionID]),
    CONSTRAINT [FK_ItemCollection_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ItemCollection_ItemIDCollectionID]
    ON [dbo].[ItemCollection]([ItemID] ASC, [CollectionID] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_ItemCollection_CollectionID] 
	ON [dbo].[ItemCollection] ([CollectionID] ASC);
GO


