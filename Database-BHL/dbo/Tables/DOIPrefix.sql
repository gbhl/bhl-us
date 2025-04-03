CREATE TABLE dbo.DOIPrefix
	(
	DOIPrefixID int IDENTITY(1,1) NOT NULL,
	Prefix nvarchar(30) NOT NULL,
	OriginalRegistrant nvarchar(200) CONSTRAINT DF_DOIPrefix_OriginalRegistrant DEFAULT ('') NOT NULL,
	AllowNew tinyint CONSTRAINT DF_DOIPrefix_AllowNew DEFAULT (0) NOT NULL,
	CreationDate datetime CONSTRAINT DF_DOIPrefix_CreationDate DEFAULT (GETDATE()) NOT NULL,
	LastModifiedDate datetime  CONSTRAINT DF_DOIPrefix_LastModifiedDate DEFAULT (GETDATE()) NOT NULL,
	CreationUserID int CONSTRAINT DF_DOIPrefix_CreationUserID DEFAULT (1) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_DOIPrefix_LastModifiedUserID DEFAULT (1) NOT NULL,
	CONSTRAINT [PK_DOIPrefix] PRIMARY KEY CLUSTERED 
	(
		[DOIPrefixID] ASC
	)
)
GO
