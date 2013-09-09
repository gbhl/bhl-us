CREATE TABLE [dbo].[TitleVariant] (
    [TitleVariantID]   INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]        NVARCHAR (50)  CONSTRAINT [DF_TitleVariant_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]   INT            NOT NULL,
    [ImportSourceID]   INT            NOT NULL,
    [MARCTag]          NVARCHAR (20)  CONSTRAINT [DF_TitleVariant_MARCTag] DEFAULT ('') NOT NULL,
    [MARCIndicator2]   NVARCHAR (20)  CONSTRAINT [DF_TitleVariant_MARCIndicator2] DEFAULT ('') NOT NULL,
    [Title]            NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_Title] DEFAULT ('') NOT NULL,
    [TitleRemainder]   NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_TitleRemainder] DEFAULT ('') NOT NULL,
    [PartNumber]       NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartNumber] DEFAULT ('') NOT NULL,
    [PartName]         NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartName] DEFAULT ('') NOT NULL,
    [ProductionDate]   DATETIME       NULL,
    [CreatedDate]      DATETIME       CONSTRAINT [DF_TitleVariant_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_TitleVariant_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TitleVariant] PRIMARY KEY CLUSTERED ([TitleVariantID] ASC),
    CONSTRAINT [FK_TitleVariant_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_TitleVariant_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

