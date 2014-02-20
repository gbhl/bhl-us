CREATE TABLE import.ImportRecordErrorLog
	(
	ImportRecordErrorLogID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecordErrorLog PRIMARY KEY,
	ImportRecordID int NOT NULL,
	ErrorDate datetime NOT NULL CONSTRAINT DF_ImportRecordErrorLog_ErrorDate DEFAULT(GETDATE()),
	ErrorMessage nvarchar(max) NOT NULL CONSTRAINT DF_ImportRecordErrorLog_ErrorMessage DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordErrorLog_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordErrorLog_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordErrorLog_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordErrorLog_LastModifiedUserID DEFAULT(1),
	CONSTRAINT FK_ImportRecordErrorLog_ImportRecord FOREIGN KEY (ImportRecordID) 
			REFERENCES import.ImportRecord (ImportRecordID)
	)
