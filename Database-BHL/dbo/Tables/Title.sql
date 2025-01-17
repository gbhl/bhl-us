CREATE TABLE [dbo].[Title] (
    [TitleID]                     INT             IDENTITY (1, 1) NOT NULL,
    [MARCBibID]                   NVARCHAR (50)   NOT NULL,
    [MARCLeader]                  NVARCHAR (24)   NULL,
    [TropicosTitleID]             INT             NULL,
    [RedirectTitleID]             INT             NULL,
    [FullTitle]                   NVARCHAR (2000) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_Title_FullTitle] DEFAULT ('') NOT NULL,
    [ShortTitle]                  NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [UniformTitle]                NVARCHAR (255)  NULL,
    [SortTitle]                   NVARCHAR (60)   COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [CallNumber]                  NVARCHAR (100)  NULL,
    [PublicationDetails]          NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [StartYear]                   SMALLINT        NULL,
    [EndYear]                     SMALLINT        NULL,
    [Datafield_260_a]             NVARCHAR (150)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [Datafield_260_b]             NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [Datafield_260_c]             NVARCHAR (100)  NULL,
    [LanguageCode]                NVARCHAR (10)   NULL,
    [TitleDescription]            NTEXT           NULL,
    [TL2Author]                   NVARCHAR (100)  NULL,
    [PublishReady]                BIT             CONSTRAINT [DF__Title__PublishRe__725BF7F6] DEFAULT ((0)) NOT NULL,
    [RareBooks]                   BIT             CONSTRAINT [DF_Title_RareBooks] DEFAULT ((0)) NOT NULL,
    [Note]                        NVARCHAR (MAX)  NULL,
    [CreationDate]                DATETIME        CONSTRAINT [DF__Title__Created__74444068] DEFAULT (getdate()) NULL,
    [LastModifiedDate]            DATETIME        CONSTRAINT [DF__Title__Changed__753864A1] DEFAULT (getdate()) NULL,
    [CreationUserID]              INT             CONSTRAINT [DF_Title_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]          INT             CONSTRAINT [DF_Title_LastModifiedUserID] DEFAULT ((1)) NULL,
    [OriginalCatalogingSource]    NVARCHAR (100)  NULL,
    [EditionStatement]            NVARCHAR (450)  NULL,
    [CurrentPublicationFrequency] NVARCHAR (100)  NULL,
    [PartNumber]                  NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [PartName]                    NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [BibliographicLevelID]        INT             NULL,
	[MaterialTypeID]              INT             NULL,
	[HasMovingWall]               SMALLINT        CONSTRAINT [DF_Title_HasMovingWall] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [aaaaaTitle_PK] PRIMARY KEY CLUSTERED ([TitleID] ASC),
    CONSTRAINT [CK Title EndYear] CHECK ([EndYear]>=(1400) AND [EndYear]<=(2025) OR [EndYear] IS NULL),
    CONSTRAINT [CK Title StartYear] CHECK ([StartYear]>=(1400) AND [StartYear]<=(2025) OR [StartYear] IS NULL),
    CONSTRAINT [FK_Title_BibliographicLevel] FOREIGN KEY ([BibliographicLevelID]) REFERENCES [dbo].[BibliographicLevel] ([BibliographicLevelID]),
	CONSTRAINT [FK_Title_MaterialType] FOREIGN KEY ([MaterialTypeID]) REFERENCES [dbo].[MaterialType] ([MaterialTypeID]),
    CONSTRAINT [FK_Title_Title] FOREIGN KEY ([RedirectTitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [Title_FK01] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Title_BibIDShortTitle]
    ON [dbo].[Title]([MARCBibID] ASC, [ShortTitle] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title_TitleIDSortTitle]
    ON [dbo].[Title]([TitleID] ASC, [SortTitle] ASC)
    INCLUDE([ShortTitle], [MARCBibID]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_PublishReadySortTitle]
    ON [dbo].[Title]([PublishReady] ASC, [SortTitle] ASC)
    INCLUDE([TitleID], [FullTitle], [ShortTitle], [PublicationDetails], [LanguageCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_PublishReadyStartYear]
    ON [dbo].[Title]([PublishReady] ASC, [StartYear] ASC)
    INCLUDE([PublicationDetails], [TitleID], [FullTitle], [LanguageCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_TitleIDCovering]
    ON [dbo].[Title]([TitleID] ASC)
    INCLUDE([FullTitle], [ShortTitle], [SortTitle], [BibliographicLevelID], [CallNumber], [StartYear], [EndYear], [PublicationDetails], [Datafield_260_a], [Datafield_260_b], [Datafield_260_c], [PublishReady], [EditionStatement], [PartNumber], [PartName]);


GO
