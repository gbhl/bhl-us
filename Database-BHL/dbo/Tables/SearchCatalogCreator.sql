CREATE TABLE [dbo].[SearchCatalogCreator] (
    [SearchCatalogCreatorID] INT            IDENTITY (1, 1) NOT NULL,
    [CreatorID]              INT            NOT NULL,
    [CreatorName]            NVARCHAR (2000) CONSTRAINT [DF__SearchCat__Creat__54232047] DEFAULT ('') NOT NULL,
    [CreationDate]           DATETIME       CONSTRAINT [DF_SearchCatalogCreator_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SearchCatalogCreator] PRIMARY KEY CLUSTERED ([SearchCatalogCreatorID] ASC)
);

