CREATE TABLE dbo.InstitutionGroupInstitution
	(
	InstitutionGroupInstitutionID int IDENTITY(1,1) NOT NULL,
	InstitutionGroupID int NOT NULL,
	InstitutionCode nvarchar(10) NOT NULL,
	CreationDate datetime NULL CONSTRAINT DF_InstitutionGroupInstitution_CreationDate  DEFAULT (GETDATE()),
	CreationUserID int NULL CONSTRAINT DF_InstitutionGroupInstitution_CreationUserID  DEFAULT ((1)),
	CONSTRAINT PK_InstitutionGroupInstitution PRIMARY KEY (
		InstitutionGroupInstitutionID ASC
	)
)
GO

ALTER TABLE dbo.InstitutionGroupInstitution ADD CONSTRAINT FK_InstitutionGroupInstitution_InstitutionGroup FOREIGN KEY(InstitutionGroupID)
REFERENCES dbo.InstitutionGroup (InstitutionGroupID)
GO

ALTER TABLE dbo.InstitutionGroupInstitution ADD CONSTRAINT FK_InstitutionGroupInstitution_Institution FOREIGN KEY(InstitutionCode)
REFERENCES dbo.Institution (InstitutionCode)
GO
