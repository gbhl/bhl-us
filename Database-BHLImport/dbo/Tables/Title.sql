CREATE TABLE [dbo].[Title] (
    [TitleID]                     INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]                   NVARCHAR (50)  CONSTRAINT [DF_Title_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]              INT            NOT NULL,
    [ImportSourceID]              INT            NULL,
    [MARCBibID]                   NVARCHAR (50)  NOT NULL,
    [MARCLeader]                  NVARCHAR (24)  NULL,
    [FullTitle]                   NTEXT          NULL,
    [ShortTitle]                  NVARCHAR (255) NULL,
    [UniformTitle]                NVARCHAR (255) NULL,
    [SortTitle]                   NVARCHAR (60)  NULL,
    [CallNumber]                  NVARCHAR (100) NULL,
    [PublicationDetails]          NVARCHAR (255) NULL,
    [StartYear]                   SMALLINT       NULL,
    [EndYear]                     SMALLINT       NULL,
    [Datafield_260_a]             NVARCHAR (150) NULL,
    [Datafield_260_b]             NVARCHAR (255) NULL,
    [Datafield_260_c]             NVARCHAR (100) NULL,
    [InstitutionCode]             NVARCHAR (10)  CONSTRAINT [DF__Title__Instituti__37703C52] DEFAULT ('MO') NULL,
    [LanguageCode]                NVARCHAR (10)  NULL,
    [TitleDescription]            NTEXT          NULL,
    [TL2Author]                   NVARCHAR (100) NULL,
    [PublishReady]                BIT            CONSTRAINT [DF__Title__PublishRe__3864608B] DEFAULT ((0)) NULL,
    [Note]                        NVARCHAR (255) NULL,
    [ExternalCreationDate]        DATETIME       CONSTRAINT [DF_Title_CreationDate] DEFAULT (getdate()) NULL,
    [ExternalLastModifiedDate]    DATETIME       CONSTRAINT [DF_Title_LastModifiedDate] DEFAULT (getdate()) NULL,
    [ExternalCreationUser]        INT            CONSTRAINT [DF_Title_CreationUserID] DEFAULT ((1)) NULL,
    [ExternalLastModifiedUser]    INT            CONSTRAINT [DF_Title_LastModifiedUserID] DEFAULT ((1)) NULL,
    [ProductionDate]              DATETIME       NULL,
    [CreatedDate]                 DATETIME       CONSTRAINT [DF_Title_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]            DATETIME       CONSTRAINT [DF_Title_LastModifiedDate_1] DEFAULT (getdate()) NOT NULL,
    [RareBooks]                   BIT            CONSTRAINT [DF_Title_RareBooks] DEFAULT ((0)) NULL,
    [OriginalCatalogingSource]    NVARCHAR (100) NULL,
    [EditionStatement]            NVARCHAR (450) NULL,
    [CurrentPublicationFrequency] NVARCHAR (100) NULL,
    [ProductionTitleID]           INT            NULL,
    [PartNumber]                  NVARCHAR (255) NULL,
    [PartName]                    NVARCHAR (255) NULL,
    CONSTRAINT [aaaaaTitle_PK] PRIMARY KEY CLUSTERED ([TitleID] ASC),
    CONSTRAINT [FK_Title_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_Title_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Title_ImportKeyImportStatusImportSource]
    ON [dbo].[Title]([ImportKey] ASC, [ImportStatusID] ASC, [ImportSourceID] ASC);

