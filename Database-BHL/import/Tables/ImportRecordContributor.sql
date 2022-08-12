CREATE TABLE import.ImportRecordContributor
(
	ImportRecordContributorID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportRecordContributor PRIMARY KEY,
	ImportRecordID int NOT NULL,
	InstitutionCode nvarchar(10) NOT NULL CONSTRAINT DF_ImportRecordContributor_InstitutionCode DEFAULT(''),
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportRecordContributor_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportRecordContributor_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportRecordContributor_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportRecordContributor_LastModifiedUserID DEFAULT(1),
)
GO
