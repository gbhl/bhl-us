CREATE TABLE [dbo].[NamePage] (
    [NamePageID]         INT      IDENTITY (1, 1) NOT NULL,
    [NameID]             INT      NOT NULL,
    [PageID]             INT      NOT NULL,
    [NameSourceID]       INT      NOT NULL,
    [IsFirstOccurrence]  SMALLINT CONSTRAINT [DF_NamePage_IsFirstOccurrence] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_NamePage_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_NamePage_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      NULL,
    [LastModifiedUserID] INT      NULL,
    CONSTRAINT [PK_NamePage] PRIMARY KEY CLUSTERED ([NamePageID] ASC),
    CONSTRAINT [FK_NamePage_Name] FOREIGN KEY ([NameID]) REFERENCES [dbo].[Name] ([NameID]),
    CONSTRAINT [FK_NamePage_NameSource] FOREIGN KEY ([NameSourceID]) REFERENCES [dbo].[NameSource] ([NameSourceID]),
    CONSTRAINT [FK_NamePage_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID])
);


GO
CREATE NONCLUSTERED INDEX [IX_NamePage_NameID]
    ON [dbo].[NamePage]([NameID] ASC)
    INCLUDE([PageID]);


GO
CREATE NONCLUSTERED INDEX [IX_NamePage_PageID]
    ON [dbo].[NamePage]([PageID] ASC);

