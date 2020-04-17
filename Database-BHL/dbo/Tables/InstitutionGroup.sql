CREATE TABLE dbo.InstitutionGroup
	(
	InstitutionGroupID int IDENTITY(1,1) NOT NULL,
	InstitutionGroupName nvarchar(300) NOT NULL CONSTRAINT DF_InstitutionGroup_InstitutionGroupName DEFAULT (''),
	InstitutionGroupDescription nvarchar(MAX) NOT NULL CONSTRAINT DF_InstitutionGroup_InstitutionGroupDescription DEFAULT(''),
	CreationDate datetime NULL CONSTRAINT DF_InstitutionGroup_CreationDate  DEFAULT (GETDATE()),
	LastModifiedDate datetime NULL CONSTRAINT DF_InstitutionGroup_LastModifiedDate  DEFAULT (GETDATE()),
	CreationUserID int NULL CONSTRAINT DF_InstitutionGroup_CreationUserID  DEFAULT ((1)),
	LastModifiedUserID int NULL CONSTRAINT DF_InstitutionGroup_LastModifiedUserID  DEFAULT ((1)),
	CONSTRAINT PK_InstitutionGroup PRIMARY KEY (
		InstitutionGroupID ASC
	)
)
GO