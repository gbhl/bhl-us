CREATE TABLE [dbo].[PageTextLog](
	[PageTextLogID] [int] IDENTITY(1,1) NOT NULL,
	[PageID] [int] NOT NULL,
	[TextSource] [nvarchar](50) NOT NULL CONSTRAINT [DF_PageTextLog_TextSource]  DEFAULT (''),
	[TextImportBatchFileID] [int] NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_PageTextLog_CreationDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_PageTextLog_CreationUserID]  DEFAULT (1),
	CONSTRAINT [PK_PageTextLog] PRIMARY KEY CLUSTERED ( [PageTextLogID] ASC ),
	CONSTRAINT [FK_PageTextLog_Page] FOREIGN KEY([PageID])
		REFERENCES [dbo].[Page] ([PageID]),
	CONSTRAINT [FK_PageTextLog_TextImportBatchFile] FOREIGN KEY([TextImportBatchFileID])
		REFERENCES [txtimport].[TextImportBatchFile] ([TextImportBatchFileID])
)
