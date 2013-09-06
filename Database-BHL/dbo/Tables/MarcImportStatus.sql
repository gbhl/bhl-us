CREATE TABLE [dbo].[MarcImportStatus] (
    [MarcImportStatusID] INT           NOT NULL,
    [StatusDescription]  NVARCHAR (30) CONSTRAINT [DF_MarcImportStatus_StatusDescription] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_MarcImportStatus] PRIMARY KEY CLUSTERED ([MarcImportStatusID] ASC)
);

