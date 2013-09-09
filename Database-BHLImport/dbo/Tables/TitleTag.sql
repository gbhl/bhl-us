CREATE TABLE [dbo].[TitleTag] (
    [TitleTagID]               INT           IDENTITY (1, 1) NOT NULL,
    [TagText]                  NVARCHAR (50) NOT NULL,
    [ImportKey]                NVARCHAR (50) CONSTRAINT [DF_TitleTag_ImportItemID] DEFAULT ('') NOT NULL,
    [ImportStatusID]           INT           NOT NULL,
    [ImportSourceID]           INT           NULL,
    [MarcDataFieldTag]         NVARCHAR (50) NULL,
    [MarcSubFieldCode]         NVARCHAR (50) NULL,
    [ExternalCreationDate]     DATETIME      NULL,
    [ExternalLastModifiedDate] DATETIME      NULL,
    [ProductionDate]           DATETIME      NULL,
    [CreatedDate]              DATETIME      CONSTRAINT [DF_TitleTag_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME      CONSTRAINT [DF_TitleTag_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TitleTag] PRIMARY KEY CLUSTERED ([TitleTagID] ASC),
    CONSTRAINT [FK_TitleTag_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_TitleTag_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

