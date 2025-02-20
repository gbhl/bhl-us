SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PDF](
	[PdfID] [int] IDENTITY(1000,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[FileLocation] [nvarchar](200) NOT NULL,
	[EmailAddress] [nvarchar](200) NOT NULL,
	[ShareWithEmailAddresses] [nvarchar](max) NOT NULL,
	[ArticleTitle] [nvarchar](max) NOT NULL,
	[ArticleCreators] [nvarchar](max) NOT NULL,
	[ArticleTags] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ImagesOnly] [bit] NOT NULL,
	[FileUrl] [nvarchar](200) NOT NULL,
	[FileGenerationDate] [datetime] NULL,
	[FileDeletionDate] [datetime] NULL,
	[PdfStatusID] [int] NOT NULL,
	[NumberImagesMissing] [int] NOT NULL,
	[NumberOcrMissing] [int] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PDF] PRIMARY KEY CLUSTERED 
(
	[PdfID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_PDF_FileDeletionDate] ON [dbo].[PDF]
(
	[FileDeletionDate] ASC
)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_PDF_FileLocation] ON [dbo].[PDF]
(
	[FileLocation] ASC
)
INCLUDE ( 	[FileGenerationDate])
GO
CREATE NONCLUSTERED INDEX [IX_PDF_PdfStatusID] ON [dbo].[PDF]
(
	[PdfStatusID] ASC
)
INCLUDE ( 	[PdfID])
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_FileLocation]  DEFAULT ('') FOR [FileLocation]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_EmailAddress]  DEFAULT ('') FOR [EmailAddress]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_ShareWithEmailAddresses]  DEFAULT ('') FOR [ShareWithEmailAddresses]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_ArticleTitle]  DEFAULT ('') FOR [ArticleTitle]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_ArticleCreators]  DEFAULT ('') FOR [ArticleCreators]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_ArticleTags]  DEFAULT ('') FOR [ArticleTags]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_ImagesOnly]  DEFAULT ((1)) FOR [ImagesOnly]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_FileUrl]  DEFAULT ('') FOR [FileUrl]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_PdfStatusID]  DEFAULT ((10)) FOR [PdfStatusID]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_NumberImagesMissing]  DEFAULT ((0)) FOR [NumberImagesMissing]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_NumberOcrMissing]  DEFAULT ((0)) FOR [NumberOcrMissing]
GO
ALTER TABLE [dbo].[PDF] ADD  CONSTRAINT [DF_PDF_Comment]  DEFAULT ('') FOR [Comment]
GO
ALTER TABLE [dbo].[PDF]  WITH CHECK ADD  CONSTRAINT [FK_PDF_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[PDF] CHECK CONSTRAINT [FK_PDF_Item]
GO
ALTER TABLE [dbo].[PDF]  WITH CHECK ADD  CONSTRAINT [FK_PDF_PDFStatus] FOREIGN KEY([PdfStatusID])
REFERENCES [dbo].[PDFStatus] ([PdfStatusID])
GO
ALTER TABLE [dbo].[PDF] CHECK CONSTRAINT [FK_PDF_PDFStatus]
GO
