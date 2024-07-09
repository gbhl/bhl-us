CREATE TABLE dbo.ExternalResourceTypeIDType (
	ExternalResourceTypeIDTypeID int IDENTITY NOT NULL,
	ExternalResourceTypeID int NOT NULL,
	IDTypeID int NOT NULL,
	CreationDate datetime CONSTRAINT DF_ExternalResourceTypeIDType_CreationDate DEFAULT (getdate()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_ExternalResourceTypeIDType_LastModifiedDate DEFAULT (getdate()) NOT NULL,
	CreationUserID int CONSTRAINT DF_ExternalResourceTypeIDType_CreationUserID DEFAULT ((1)) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_ExternalResourceTypeIDType_LastModifiedUserID DEFAULT ((1)) NOT NULL,
	CONSTRAINT PK_ExternalResourceTypeIDType PRIMARY KEY CLUSTERED 
	(
		ExternalResourceTypeIDTypeID ASC
	)
)
GO
ALTER TABLE dbo.ExternalResourceTypeIDType ADD CONSTRAINT FK_ExternalResourceTypeIDType_Identifier FOREIGN KEY (ExternalResourceTypeID) REFERENCES dbo.ExternalResourceType (ExternalResourceTypeID)
ALTER TABLE dbo.ExternalResourceTypeIDType ADD CONSTRAINT FK_ExternalResourceType_IDType FOREIGN KEY(IDTypeID) REFERENCES dbo.IDType (IDTypeID)
GO
