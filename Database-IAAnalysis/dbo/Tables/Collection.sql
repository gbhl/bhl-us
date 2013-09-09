CREATE TABLE [dbo].[Collection] (
    [CollectionID]   INT            IDENTITY (1, 1) NOT NULL,
    [CollectionName] NVARCHAR (200) CONSTRAINT [DF__Collectio__Colle__7D78A4E7] DEFAULT ('') NOT NULL,
    [CreationDate]   DATETIME       CONSTRAINT [DF__Collectio__Creat__7E6CC920] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Set] PRIMARY KEY CLUSTERED ([CollectionID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Collection_CollectionName]
    ON [dbo].[Collection]([CollectionName] ASC);

