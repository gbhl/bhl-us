CREATE TABLE [dbo].[TitleDocument] (
	[TitleDocumentID]	 INT            IDENTITY(1,1) NOT NULL,
	[TitleID]            INT            NOT NULL,
	[DocumentTypeID]     INT            NOT NULL,
	[Name]               NVARCHAR(200)  CONSTRAINT [DF_TitleDocument_Name] DEFAULT('') NOT NULL,
	[Url]                NVARCHAR(200)  CONSTRAINT [DF_TitleDocument_Url] DEFAULT('') NOT NULL,
	[CreationDate]       DATETIME       CONSTRAINT [DF_TitleDocument_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_TitleDocument_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_TitleDocument_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_TitleDocument_LastModifiedUserID] DEFAULT ((1)) NULL,
	CONSTRAINT [PK_TitleDocument] PRIMARY KEY CLUSTERED ([TitleDocumentID] ASC),
	CONSTRAINT [FK_TitleDocument_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleDocument_DocumentType] FOREIGN KEY ([DocumentTypeID]) REFERENCES [dbo].[DocumentType] ([DocumentTypeID])
)
GO
