CREATE TABLE dbo.TitleExternalResource
(
	TitleExternalResourceID	int IDENTITY(1,1) NOT NULL,
	TitleID int NOT NULL,
	TitleExternalResourceTypeID int NOT NULL,
	UrlText nvarchar(100) CONSTRAINT DF_TitleExternalResource_UrlText DEFAULT ('') NOT NULL,
	Url nvarchar(200) CONSTRAINT DF_TitleExternalResource_Url DEFAULT ('') NOT NULL,
	SequenceOrder smallint CONSTRAINT DF_TitleExternalResource_SequenceOrder DEFAULT(1) NOT NULL,
	CreationDate datetime CONSTRAINT DF_TitleExternalResource_CreationDate DEFAULT (GETDATE()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_TitleExternalResource_LastModifiedDate DEFAULT (GETDATE()) NOT NULL,
	CreationUserID int CONSTRAINT DF_TitleExternalResource_CreationUserID DEFAULT (1) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_TitleExternalResource_LastModifiedUserID DEFAULT (1) NOT NULL,
	CONSTRAINT PK_TitleExternalResource PRIMARY KEY CLUSTERED 
	(
		TitleExternalResourceID ASC
	)
)
GO

ALTER TABLE dbo.TitleExternalResource ADD CONSTRAINT FK_TitleExternalResource_Title FOREIGN KEY(TitleID)
	REFERENCES dbo.Title (TitleID)
GO

ALTER TABLE dbo.TitleExternalResource ADD CONSTRAINT FK_TitleExternalResource_TitleExternalResourceType FOREIGN KEY(TitleExternalResourceTypeID)
	REFERENCES dbo.TitleExternalResourceType (TitleExternalResourceTypeID)
GO
