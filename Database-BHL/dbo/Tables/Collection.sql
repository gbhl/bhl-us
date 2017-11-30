CREATE TABLE [dbo].[Collection] (
    [CollectionID]          INT             IDENTITY (1, 1) NOT NULL,
    [CollectionName]        NVARCHAR (50)   CONSTRAINT [DF_Collection_CollectionName] DEFAULT ('') NOT NULL,
    [CollectionDescription] NVARCHAR (4000) CONSTRAINT [DF_Collection_CollectionDescription] DEFAULT ('') NOT NULL,
    [CollectionURL]         NVARCHAR (50)   CONSTRAINT [DF_Collection_CollectionURL] DEFAULT ('') NOT NULL,
    [HtmlContent]           NVARCHAR (MAX)  CONSTRAINT [DF_Collection_HtmlContent] DEFAULT ('') NOT NULL,
    [CanContainTitles]      SMALLINT        CONSTRAINT [DF_Collection_CanContainTitles] DEFAULT ((0)) NOT NULL,
    [CanContainItems]       SMALLINT        CONSTRAINT [DF_Collection_CanContainItems] DEFAULT ((0)) NOT NULL,
    [InstitutionCode]       NVARCHAR (10)   NULL,
    [LanguageCode]          NVARCHAR (10)   NULL,
    [Active]                SMALLINT        CONSTRAINT [DF_Collection_Active] DEFAULT ((1)) NOT NULL,
    [CreationDate]          DATETIME        CONSTRAINT [DF_Collection_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]      DATETIME        CONSTRAINT [DF_Collection_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CollectionTarget]      NVARCHAR (30)   CONSTRAINT [DF_Collection_CollectionTargetID] DEFAULT (N'BHL') NOT NULL,
    [ITunesImageURL]        NVARCHAR (100)  CONSTRAINT [DF_Collection_iTunesImageURL] DEFAULT ('') NOT NULL,
    [ITunesURL]             NVARCHAR (100)  CONSTRAINT [DF_Collection_iTunesURL] DEFAULT ('') NOT NULL,
    [ImageURL]              NVARCHAR (100)  CONSTRAINT [DF_Collection_ImageURL] DEFAULT ('') NOT NULL,
    [Featured]              SMALLINT        CONSTRAINT [DF_Collection_Featured] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED ([CollectionID] ASC),
    CONSTRAINT [FK_Collection_Institution] FOREIGN KEY ([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode]),
    CONSTRAINT [FK_Collection_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode])
);


GO
