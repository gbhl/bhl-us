CREATE TABLE import.ImportFileStatus
	(
	ImportFileStatusID int NOT NULL CONSTRAINT PK_ImportFileStatus PRIMARY KEY,
	StatusName nvarchar(50)  NOT NULL CONSTRAINT DF_ImportFileStatus_StatusName DEFAULT(''),
	StatusDescription nvarchar(500) NOT NULL CONSTRAINT DF_ImportFileStatus_StatusDescription DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportFileStatus_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportFileStatus_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportFileStatus_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportFileStatus_LastModifiedUserID DEFAULT(1)
	)
