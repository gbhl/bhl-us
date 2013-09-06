CREATE TABLE [dbo].[MarcImportBatch] (
    [MarcImportBatchID] INT            IDENTITY (1, 1) NOT NULL,
    [FileName]          NVARCHAR (500) CONSTRAINT [DF_MarcImportBatch_FileName] DEFAULT ('') NOT NULL,
    [InstitutionCode]   NVARCHAR (10)  NULL,
    [CreationDate]      DATETIME       CONSTRAINT [DF_MarcImportBatch_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcImportBatch] PRIMARY KEY CLUSTERED ([MarcImportBatchID] ASC)
);

