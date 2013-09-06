CREATE TABLE [dbo].[PDFStatus] (
    [PdfStatusID]   INT        NOT NULL,
    [PdfStatusName] NCHAR (10) CONSTRAINT [DF_PDFStatus_PDFStatusName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_PDFStatus] PRIMARY KEY CLUSTERED ([PdfStatusID] ASC)
);

