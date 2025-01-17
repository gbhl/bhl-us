CREATE TABLE [dbo].[DocumentType] (
	[DocumentTypeID]	 INT			IDENTITY(1,1) NOT NULL,
	[Name]				 NVARCHAR (40)	CONSTRAINT [DF_DocumentType_Name] DEFAULT ('') NOT NULL,
	[Label]				 NVARCHAR (50)	CONSTRAINT [DF_DocumentType_Label] DEFAULT ('') NOT NULL,
	[CreationDate]       DATETIME       CONSTRAINT [DF_DocumentType_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_DocumentType_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_DocumentType_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_DocumentType_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC)
)
GO
