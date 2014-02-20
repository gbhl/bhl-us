CREATE TABLE import.ImportRecordCreator
	(
	ImportRecordCreatorID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecordCreator PRIMARY KEY,
	ImportRecordID int NOT NULL,
	FullName nvarchar(300) NOT NULL CONSTRAINT DF_ImportRecordCreator_FullName DEFAULT(''),
	FirstName nvarchar(150) NOT NULL CONSTRAINT DF_ImportRecordCreator_FirstName DEFAULT(''),
	LastName nvarchar(150) NOT NULL CONSTRAINT DF_ImportRecordCreator_LastName DEFAULT(''),
	StartYear nvarchar(25) NOT NULL CONSTRAINT DF_ImportRecordCreator_StartYear DEFAULT(''),
	EndYear nvarchar(25) NOT NULL CONSTRAINT DF_ImportRecordCreator_EndYear DEFAULT(''),
	AuthorType nvarchar(50) NOT NULL CONSTRAINT DF_ImportRecordCreator_AuthorType DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordCreator_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordCreator_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordCreator_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordCreator_LastModifiedUserID DEFAULT(1),
	)
