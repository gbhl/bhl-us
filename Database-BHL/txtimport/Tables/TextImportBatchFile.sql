SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [txtimport].[TextImportBatchFile](
	[TextImportBatchFileID] [int] IDENTITY(1,1) NOT NULL,
	[TextImportBatchID] [int] NOT NULL,
	[TextImportBatchFileStatusID] [int] NOT NULL,
	[ItemID] [int] NULL,
	[Filename] [nvarchar](500) NOT NULL,
	[FileFormat] [nvarchar](100) NOT NULL,
	[ErrorMessage] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NOT NULL,
	[LastModifiedUserID] [int] NOT NULL,
 CONSTRAINT [PK_TextImportBatchFile] PRIMARY KEY CLUSTERED 
(
	[TextImportBatchFileID] ASC
)
)

GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_FileName]  DEFAULT ('') FOR [Filename]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_FileFormat]  DEFAULT ('') FOR [FileFormat]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_ErrorMessage]  DEFAULT ('') FOR [ErrorMessage]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [txtimport].[TextImportBatchFile] ADD  CONSTRAINT [DF_TextImportBatchFile_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [txtimport].[TextImportBatchFile]  WITH CHECK ADD  CONSTRAINT [FK_TextImportBatchFile_BatchFileStatus] FOREIGN KEY([TextImportBatchFileStatusID])
REFERENCES [txtimport].[TextImportBatchFileStatus] ([TextImportBatchFileStatusID])
GO
ALTER TABLE [txtimport].[TextImportBatchFile] CHECK CONSTRAINT [FK_TextImportBatchFile_BatchFileStatus]
GO
ALTER TABLE [txtimport].[TextImportBatchFile]  WITH CHECK ADD  CONSTRAINT [FK_TextImportBatchFile_BatchID] FOREIGN KEY([TextImportBatchID])
REFERENCES [txtimport].[TextImportBatch] ([TextImportBatchID])
GO
ALTER TABLE [txtimport].[TextImportBatchFile] CHECK CONSTRAINT [FK_TextImportBatchFile_BatchID]
GO
ALTER TABLE [txtimport].[TextImportBatchFile]  WITH CHECK ADD  CONSTRAINT [FK_TextImportBatchFile_ItemID] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [txtimport].[TextImportBatchFile] CHECK CONSTRAINT [FK_TextImportBatchFile_ItemID]
GO
