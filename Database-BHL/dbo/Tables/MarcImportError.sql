CREATE TABLE [dbo].[MarcImportError] (
    [MarcImportErrorID] INT             IDENTITY (1, 1) NOT NULL,
    [MarcImportBatchID] INT             NULL,
    [ItemID]            INT             NULL,
    [ErrorDate]         DATETIME        CONSTRAINT [DF_MarcImportError_ErrorDate] DEFAULT (getdate()) NOT NULL,
    [Number]            INT             NULL,
    [Severity]          INT             NULL,
    [State]             INT             NULL,
    [Procedure]         NVARCHAR (126)  CONSTRAINT [DF_MarcImportError_Procedure] DEFAULT ('') NULL,
    [Line]              INT             NULL,
    [Message]           NVARCHAR (4000) CONSTRAINT [DF_MarcImportError_Message] DEFAULT ('') NULL,
    CONSTRAINT [PK_MarcImportError] PRIMARY KEY CLUSTERED ([MarcImportErrorID] ASC)
);

