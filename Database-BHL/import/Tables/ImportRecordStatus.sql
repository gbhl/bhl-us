CREATE TABLE import.ImportRecordStatus
	(
	ImportRecordStatusID int NOT NULL CONSTRAINT PK_ImportRecordStatus PRIMARY KEY,
	StatusName nvarchar(50) NOT NULL CONSTRAINT DF_ImportRecordStatus_StatusName DEFAULT(''),
	StatusDescription nvarchar(500) NOT NULL CONSTRAINT DF_ImportRecordStatus_StatusDescription DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordStatus_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordStatus_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordStatus_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordStatus_LastModifiedUserID DEFAULT(1)
	)
