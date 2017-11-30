CREATE TABLE [dbo].[TitleCollection] (
    [TitleCollectionID] INT      IDENTITY (1, 1) NOT NULL,
    [TitleID]           INT      NOT NULL,
    [CollectionID]      INT      NOT NULL,
    [CreationDate]      DATETIME CONSTRAINT [DF_TitleCollection_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TitleCollection] PRIMARY KEY CLUSTERED ([TitleCollectionID] ASC),
    CONSTRAINT [FK_TitleCollection_Collection] FOREIGN KEY ([CollectionID]) REFERENCES [dbo].[Collection] ([CollectionID]),
    CONSTRAINT [FK_TitleCollection_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleCollection_ItemIDCollectionID]
    ON [dbo].[TitleCollection]([TitleID] ASC, [CollectionID] ASC);


GO
