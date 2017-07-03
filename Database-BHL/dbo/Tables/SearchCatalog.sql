CREATE TABLE [dbo].[SearchCatalog] (
    [SearchCatalogID]    INT             IDENTITY (1, 1) NOT NULL,
    [SearchText]         NVARCHAR (4000) CONSTRAINT [DF__SearchCat__Searc__754F09E8] DEFAULT ('') NOT NULL,
    [TitleID]            INT             NOT NULL,
    [ItemID]             INT             NOT NULL,
    [FullTitle]          NVARCHAR (2000) CONSTRAINT [DF__SearchCat__FullT__76432E21] DEFAULT ('') NOT NULL,
    [UniformTitle]       NVARCHAR (255)  CONSTRAINT [DF__SearchCat__Unifo__7737525A] DEFAULT ('') NOT NULL,
    [PublicationDetails] NVARCHAR (255)  CONSTRAINT [DF__SearchCat__Publi__782B7693] DEFAULT ('') NULL,
    [PublisherPlace]     NVARCHAR (150)  CONSTRAINT [DF__SearchCat__Publi__791F9ACC] DEFAULT ('') NULL,
    [PublisherName]      NVARCHAR (255)  CONSTRAINT [DF__SearchCat__Publi__7A13BF05] DEFAULT ('') NULL,
    [Volume]             NVARCHAR (100)  CONSTRAINT [DF__SearchCat__Volum__7B07E33E] DEFAULT ('') NOT NULL,
    [EditionStatement]   NVARCHAR (450)  CONSTRAINT [DF__SearchCat__Editi__7BFC0777] DEFAULT ('') NOT NULL,
    [Subjects]           NVARCHAR (MAX)  CONSTRAINT [DF__SearchCat__Subje__7CF02BB0] DEFAULT ('') NOT NULL,
    [Associations]       NVARCHAR (MAX)  CONSTRAINT [DF__SearchCat__Assoc__7DE44FE9] DEFAULT ('') NOT NULL,
    [Variants]           NVARCHAR (MAX)  CONSTRAINT [DF__SearchCat__Varia__7ED87422] DEFAULT ('') NOT NULL,
    [Authors]            NVARCHAR (MAX)  CONSTRAINT [DF__SearchCat__Autho__7FCC985B] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME        CONSTRAINT [DF_SearchCatalog_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME        CONSTRAINT [DF_SearchCatalog_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [HasSegments]        SMALLINT        CONSTRAINT [DF_SearchCatalog_HasSegments] DEFAULT ((0)) NOT NULL,
    [HasLocalContent]    SMALLINT        CONSTRAINT [DF_SearchCatalog_HasLocalContent] DEFAULT ((1)) NOT NULL,
    [HasExternalContent] SMALLINT        CONSTRAINT [DF_SearchCatalog_HasExternalContent] DEFAULT ((0)) NOT NULL,
	[TitleContributors]  NVARCHAR(MAX)   CONSTRAINT [DF_SearchCatalog_TitleContributors] DEFAULT ('') NOT NULL,
	[ItemContributors]   NVARCHAR(MAX)   CONSTRAINT [DF_SearchCatalog_ItemContributors] DEFAULT ('') NOT NULL,
	[FirstPageID]		 INT			 NULL,
	[SearchAuthors]      NVARCHAR(MAX)   CONSTRAINT [DF_SearchCatalog_SearchAuthors] DEFAULT('') NOT NULL,
	[HasIllustrations]   SMALLINT        CONSTRAINT [DF_SearchCatalog_HasIllustrations] DEFAULT((0)) NOT NULL, 
    CONSTRAINT [PK_SearchCatalog] PRIMARY KEY CLUSTERED ([SearchCatalogID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_SearchCatalog_TitleID]
    ON [dbo].[SearchCatalog]([TitleID] ASC)
    INCLUDE([ItemID], [Authors], [Subjects]);
GO

CREATE NONCLUSTERED INDEX [IX_SearchCatalog_ItemID] 
	ON [dbo].[SearchCatalog] ([ItemID] ASC)
	INCLUDE ([TitleID], [ItemContributors]);
GO
