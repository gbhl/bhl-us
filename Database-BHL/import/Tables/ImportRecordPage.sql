CREATE TABLE import.ImportRecordPage
	(
	ImportRecordPageID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecordPage PRIMARY KEY,
	ImportRecordID int NOT NULL,
	PageID int NOT NULL,
	SequenceOrder smallint NOT NULL CONSTRAINT DF_ImportRecordPage_SequenceOrder DEFAULT(1),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordPage_CreationDate DEFAULT (getdate()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordPage_LastModifiedDate DEFAULT (getdate()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordPage_CreationUserID DEFAULT (1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordPage_LastModifiedUserID DEFAULT (1)
	)
