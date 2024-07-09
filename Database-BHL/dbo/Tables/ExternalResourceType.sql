CREATE TABLE dbo.ExternalResourceType 
(
	ExternalResourceTypeID int IDENTITY(1,1) NOT NULL,
	ExternalResourceTypeName varchar(100) CONSTRAINT DF_ExternalResourceType_TypeName DEFAULT ('') NOT NULL,
	ExternalResourceTypeLabel varchar(100) CONSTRAINT DF_ExternalResourceType_TypeLabel DEFAULT ('') NOT NULL,
	CreationDate datetime CONSTRAINT DF_ExternalResourceType_CreationDate DEFAULT (getdate()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_ExternalResourceType_LastModifiedDate DEFAULT (getdate()) NOT NULL,
	CreationUserID int CONSTRAINT DF_ExternalResourceType_CreationUserID DEFAULT ((1)) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_ExternalResourceType_LastModifiedUserID DEFAULT ((1)) NOT NULL,
	CONSTRAINT PK_ExternalResourceType PRIMARY KEY CLUSTERED 
	(
		ExternalResourceTypeID ASC
	)
)
GO
