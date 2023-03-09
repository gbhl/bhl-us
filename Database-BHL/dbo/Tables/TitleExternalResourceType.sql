CREATE TABLE dbo.TitleExternalResourceType
(
	TitleExternalResourceTypeID int IDENTITY(1,1) NOT NULL,
	ExternalResourceTypeName varchar(100) CONSTRAINT DF_TitleExternalResourceType_TypeName DEFAULT ('') NOT NULL,
	ExternalResourceTypeLabel varchar(100) CONSTRAINT DF_TitleExternalResourceType_TypeLabel DEFAULT ('') NOT NULL,
	CreationDate datetime CONSTRAINT DF_TitleExternalResourceType_CreationDate DEFAULT (GETDATE()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_TitleExternalResourceType_LastModifiedDate DEFAULT (GETDATE()) NOT NULL,
	CreationUserID int CONSTRAINT DF_TitleExternalResourceType_CreationUserID DEFAULT (1) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_TitleExternalResourceType_LastModifiedUserID DEFAULT (1) NOT NULL,
	CONSTRAINT PK_TitleExternalResourceType PRIMARY KEY CLUSTERED 
	(
		TitleExternalResourceTypeID ASC
	)
)
GO
