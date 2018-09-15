CREATE TABLE [txtimport].[TextImportBatchFile] (
	[TextImportBatchFileID] [int] IDENTITY(1,1) NOT NULL,
	[TextImportBatchID] [int] NOT NULL,
	[TextImportBatchFileStatusID] [int] NOT NULL,
	[ItemID] [int] NULL,
	[Filename] [nvarchar](500) NOT NULL CONSTRAINT [DF_TextImportBatchFile_FileName] DEFAULT (''),
	[FileFormat] [nvarchar](100) NOT NULL CONSTRAINT [DF_TextImportBatchFile_FileFormat] DEFAULT (''),
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchFile_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchFile_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchFile_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchFile_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_TextImportBatchFile] PRIMARY KEY CLUSTERED ([TextImportBatchFileID] ASC),
	CONSTRAINT [FK_TextImportBatchFile_BatchID] FOREIGN KEY([TextImportBatchID])
		REFERENCES [txtimport].[TextImportBatch] ([TextImportBatchID]),
	CONSTRAINT [FK_TextImportBatchFile_BatchFileStatus] FOREIGN KEY([TextImportBatchFileStatusID])
		REFERENCES [txtimport].[TextImportBatchFileStatus] ([TextImportBatchFileStatusID]),
	CONSTRAINT [FK_TextImportBatchFile_ItemID] FOREIGN KEY([ItemID])
		REFERENCES [dbo].[Item] ([ItemID])
)
