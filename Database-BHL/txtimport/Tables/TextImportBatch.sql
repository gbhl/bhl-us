CREATE TABLE [txtimport].[TextImportBatch] (
	[TextImportBatchID] [int] IDENTITY(1,1) NOT NULL,
	[TextImportBatchStatusID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatch_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatch_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatch_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatch_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_TextImportBatch] PRIMARY KEY CLUSTERED ([TextImportBatchID] ASC),
	CONSTRAINT [FK_TextImportBatch_BatchStatus] FOREIGN KEY([TextImportBatchStatusID])
		REFERENCES [txtimport].[TextImportBatchStatus] ([TextImportBatchStatusID])
)
