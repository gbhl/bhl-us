CREATE TABLE dbo.SegmentExternalResource (
	SegmentExternalResourceID int IDENTITY(1,1) NOT NULL,
	SegmentID int NOT NULL,
	ExternalResourceTypeID int NOT NULL,
	UrlText nvarchar(100) CONSTRAINT DF_SegmentExternalResource_UrlText DEFAULT ('') NOT NULL,
	Url nvarchar(200) CONSTRAINT DF_SegmentExternalResource_Url DEFAULT ('') NOT NULL,
	SequenceOrder smallint CONSTRAINT DF_SegmentExternalResource_SequenceOrder DEFAULT ((1)) NOT NULL,
	CreationDate datetime CONSTRAINT DF_SegmentExternalResource_CreationDate DEFAULT (getdate()) NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_SegmentExternalResource_LastModifiedDate DEFAULT (getdate()) NOT NULL,
	CreationUserID int CONSTRAINT DF_SegmentExternalResource_CreationUserID DEFAULT ((1)) NOT NULL,
	LastModifiedUserID int CONSTRAINT DF_SegmentExternalResource_LastModifiedUserID DEFAULT ((1)) NOT NULL,
	CONSTRAINT PK_SegmentExternalResource PRIMARY KEY CLUSTERED 
	(
		SegmentExternalResourceID ASC
	)
)
GO
ALTER TABLE dbo.SegmentExternalResource ADD CONSTRAINT FK_SegmentExternalResource_Segment FOREIGN KEY(SegmentID) REFERENCES dbo.Segment (SegmentID)
ALTER TABLE dbo.SegmentExternalResource ADD CONSTRAINT FK_SegmentExternalResource_ExternalResourceType FOREIGN KEY(ExternalResourceTypeID) REFERENCES dbo.ExternalResourceType (ExternalResourceTypeID)
GO
