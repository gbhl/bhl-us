CREATE TABLE dbo.IASegmentPage (
	SegmentPageID int IDENTITY(1,1) NOT NULL,
	SegmentID int NOT NULL,
	PageSequence int NOT NULL,
	CreatedDate datetime NOT NULL CONSTRAINT DF_IASegmentPage_CreatedDate DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_IASegmentPage_LastModifiedDate DEFAULT (GETDATE()),
	CONSTRAINT PK_IASegmentPage PRIMARY KEY CLUSTERED ( SegmentPageID ASC )
)
GO

ALTER TABLE dbo.IASegmentPage ADD CONSTRAINT FK_IASegmentPage_Segment
	FOREIGN KEY(SegmentID) REFERENCES dbo.IASegment (SegmentID)
GO
