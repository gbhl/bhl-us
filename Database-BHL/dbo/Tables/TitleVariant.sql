CREATE TABLE [dbo].[TitleVariant] (
    [TitleVariantID]     INT            IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT            NOT NULL,
    [TitleVariantTypeID] INT            NOT NULL,
    [Title]              NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_Title] DEFAULT ('') NOT NULL,
    [TitleRemainder]     NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_TitleRemainder] DEFAULT ('') NOT NULL,
    [PartNumber]         NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartNumber] DEFAULT ('') NOT NULL,
    [PartName]           NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartName] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_TitleVariant_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_TitleVariant_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            NULL,
    [LastModifiedUserID] INT            NULL,
    CONSTRAINT [PK_TitleVariant] PRIMARY KEY CLUSTERED ([TitleVariantID] ASC),
    CONSTRAINT [FK_TitleVariant_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleVariant_TitleVariantType] FOREIGN KEY ([TitleVariantTypeID]) REFERENCES [dbo].[TitleVariantType] ([TitleVariantTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleVariant_TitleID]
    ON [dbo].[TitleVariant]([TitleID] ASC);


GO
