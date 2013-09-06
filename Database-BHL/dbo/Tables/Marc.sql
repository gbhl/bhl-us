CREATE TABLE [dbo].[Marc] (
    [MarcID]             INT            IDENTITY (1, 1) NOT NULL,
    [MarcImportStatusID] INT            NOT NULL,
    [MarcImportBatchID]  INT            NOT NULL,
    [MarcFileLocation]   NVARCHAR (500) CONSTRAINT [DF_Marc_MarcFileLocation] DEFAULT ('') NOT NULL,
    [InstitutionCode]    NVARCHAR (10)  NULL,
    [Leader]             NVARCHAR (200) CONSTRAINT [DF_Marc_Leader] DEFAULT ('') NOT NULL,
    [TitleID]            INT            NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_Marc_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_Marc_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Marc] PRIMARY KEY CLUSTERED ([MarcID] ASC),
    CONSTRAINT [FK_Marc_MarcImportBatch] FOREIGN KEY ([MarcImportBatchID]) REFERENCES [dbo].[MarcImportBatch] ([MarcImportBatchID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Marc_MarcImportStatus] FOREIGN KEY ([MarcImportStatusID]) REFERENCES [dbo].[MarcImportStatus] ([MarcImportStatusID])
);

