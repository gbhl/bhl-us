CREATE TABLE [dbo].[ItemCollection] (
    [ItemID]       INT      NOT NULL,
    [CollectionID] INT      NOT NULL,
    [CreationDate] DATETIME CONSTRAINT [DF__ItemColle__Creat__1920BF5C] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemSet] PRIMARY KEY CLUSTERED ([ItemID] ASC, [CollectionID] ASC),
    CONSTRAINT [FK_ItemCollection_Collection] FOREIGN KEY ([CollectionID]) REFERENCES [dbo].[Collection] ([CollectionID]),
    CONSTRAINT [FK_ItemCollection_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
)
WITH (DATA_COMPRESSION = PAGE);
