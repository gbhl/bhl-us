CREATE TABLE [txtimport].[TextImportBatchFileStatus](
	[TextImportBatchFileStatusID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_StatusName]  DEFAULT (''),
	[StatusDescription] [nvarchar](500) NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_StatusDescription]  DEFAULT (''),
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_TextImportBatchFileStatus_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_TextImportBatchFileStatus] PRIMARY KEY CLUSTERED 
	(
		[TextImportBatchFileStatusID] ASC
	)
)
