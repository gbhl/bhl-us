CREATE TABLE import.ImportRecordKeyword
	(
	ImportRecordKeywordID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecordKeyword PRIMARY KEY,
	ImportRecordID int NOT NULL,
	Keyword nvarchar(50) NOT NULL CONSTRAINT DF_ImportRecordKeyword_Keyword DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordKeyword_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordKeyword_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordKeyword_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordKeyword_LastModifiedUserID DEFAULT(1),
	)
