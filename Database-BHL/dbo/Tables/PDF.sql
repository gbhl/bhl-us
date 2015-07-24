CREATE TABLE [dbo].[PDF] (
    [PdfID]                   INT            IDENTITY (1000, 1) NOT NULL,
    [ItemID]                  INT            NOT NULL,
    [FileLocation]            NVARCHAR (200) CONSTRAINT [DF_PDF_FileLocation] DEFAULT ('') NOT NULL,
    [EmailAddress]            NVARCHAR (200) CONSTRAINT [DF_PDF_EmailAddress] DEFAULT ('') NOT NULL,
    [ShareWithEmailAddresses] NVARCHAR (MAX) CONSTRAINT [DF_PDF_ShareWithEmailAddresses] DEFAULT ('') NOT NULL,
    [ArticleTitle]            NVARCHAR (MAX) CONSTRAINT [DF_PDF_ArticleTitle] DEFAULT ('') NOT NULL,
    [ArticleCreators]         NVARCHAR (MAX) CONSTRAINT [DF_PDF_ArticleCreators] DEFAULT ('') NOT NULL,
    [ArticleTags]             NVARCHAR (MAX) CONSTRAINT [DF_PDF_ArticleTags] DEFAULT ('') NOT NULL,
    [CreationDate]            DATETIME       CONSTRAINT [DF_PDF_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]        DATETIME       CONSTRAINT [DF_PDF_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [ImagesOnly]              BIT            CONSTRAINT [DF_PDF_ImagesOnly] DEFAULT ((1)) NOT NULL,
    [FileUrl]                 NVARCHAR (200) CONSTRAINT [DF_PDF_FileUrl] DEFAULT ('') NOT NULL,
    [FileGenerationDate]      DATETIME       NULL,
    [FileDeletionDate]        DATETIME       NULL,
    [PdfStatusID]             INT            CONSTRAINT [DF_PDF_PdfStatusID] DEFAULT ((10)) NOT NULL,
    [NumberImagesMissing]     INT            CONSTRAINT [DF_PDF_NumberImagesMissing] DEFAULT ((0)) NOT NULL,
    [NumberOcrMissing]        INT            CONSTRAINT [DF_PDF_NumberOcrMissing] DEFAULT ((0)) NOT NULL,
    [Comment]                 NVARCHAR (MAX) CONSTRAINT [DF_PDF_Comment] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_PDF] PRIMARY KEY CLUSTERED ([PdfID] ASC),
    CONSTRAINT [FK_PDF_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]),
    CONSTRAINT [FK_PDF_PDFStatus] FOREIGN KEY ([PdfStatusID]) REFERENCES [dbo].[PDFStatus] ([PdfStatusID])
);

GO

CREATE NONCLUSTERED INDEX [IX_PDF_PdfStatusID]
	ON [dbo].[PDF] ([PdfStatusID])
	INCLUDE ([PdfID]);
GO

CREATE NONCLUSTERED INDEX [IX_PDF_FileLocation]
	ON [dbo].[PDF] ([FileLocation])
	INCLUDE ([FileGenerationDate]);
GO

CREATE NONCLUSTERED INDEX [IX_PDF_FileDeletionDate]
	ON [dbo].[PDF] ([FileDeletionDate]);
GO
