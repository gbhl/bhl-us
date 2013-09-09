CREATE TABLE [dbo].[TitleLanguage] (
    [TitleLanguageID]  INT           IDENTITY (1, 1) NOT NULL,
    [ImportKey]        NVARCHAR (50) CONSTRAINT [DF__TitleLang__Impor__08CB2759] DEFAULT ('') NOT NULL,
    [ImportStatusID]   INT           NOT NULL,
    [ImportSourceID]   INT           NULL,
    [LanguageCode]     NVARCHAR (10) CONSTRAINT [DF__TitleLang__Langu__09BF4B92] DEFAULT ('') NOT NULL,
    [ProductionDate]   DATETIME      NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF__TitleLang__Creat__0AB36FCB] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF__TitleLang__LastM__0BA79404] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TitleLanguage] PRIMARY KEY CLUSTERED ([TitleLanguageID] ASC),
    CONSTRAINT [FK_TitleLanguage_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_TitleLanguage_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

