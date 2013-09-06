CREATE TABLE [dbo].[SearchCatalogKeyword] (
    [SearchCatalogKeywordID] INT           IDENTITY (1, 1) NOT NULL,
    [KeywordID]              INT           NOT NULL,
    [Keyword]                NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF__SearchCat__Keywo__3FBD003E] DEFAULT ('') NOT NULL,
    [CreationDate]           DATETIME      CONSTRAINT [DF_SearchCatalogKeyword_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SearchCatalogKeyword] PRIMARY KEY CLUSTERED ([SearchCatalogKeywordID] ASC)
);

