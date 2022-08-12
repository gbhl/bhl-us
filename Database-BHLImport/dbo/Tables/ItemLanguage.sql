CREATE TABLE [dbo].[ItemLanguage] (
    [ItemLanguageID]   INT           IDENTITY (1, 1) NOT NULL,
    [ImportStatusID]   INT           NOT NULL,
    [ImportSourceID]   INT           NULL,
    [BarCode]          NVARCHAR (200) NOT NULL,
    [LanguageCode]     NVARCHAR (10) CONSTRAINT [DF__ItemLangu__Langu__0D8FDC76] DEFAULT ('') NOT NULL,
    [ProductionDate]   DATETIME      NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF__ItemLangu__Creat__0E8400AF] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF__ItemLangu__LastM__0F7824E8] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemLanguage] PRIMARY KEY CLUSTERED ([ItemLanguageID] ASC),
    CONSTRAINT [FK_ItemLanguage_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_ItemLanguage_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

