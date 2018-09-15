CREATE TABLE [txtimport].[TextImportBatchStatus](
	[TextImportBatchStatusID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL CONSTRAINT [DF_TextImportBatchStatus_StatusName]  DEFAULT (''),
	[StatusDescription] [nvarchar](500) NOT NULL CONSTRAINT [DF_TextImportBatchStatus_StatusDescription]  DEFAULT (''),
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchStatus_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchStatus_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchStatus_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchStatus_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_BatchStatus] PRIMARY KEY CLUSTERED 
	(
		[TextImportBatchStatusID] ASC
	)
)
