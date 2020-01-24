CREATE TABLE dbo.IASegmentAuthor (
	SegmentAuthorID int IDENTITY(1,1) NOT NULL,
	SegmentID int NOT NULL,
	[Sequence] int NOT NULL CONSTRAINT DF_IASegmentAuthor_Sequence DEFAULT(1),
	BHLAuthorID int NULL,
	FullName nvarchar(300) NOT NULL CONSTRAINT DF_IASegmentAuthor_FullName DEFAULT(''),
	LastName nvarchar(150) NOT NULL CONSTRAINT DF_IASegmentAuthor_LastName DEFAULT(''),
	FirstName nvarchar(150) NOT NULL CONSTRAINT DF_IASegmentAuthor_FirstName DEFAULT(''),
	StartDate nvarchar(25) NOT NULL CONSTRAINT DF_IASegmentAuthor_StartDate DEFAULT(''),
	EndDate nvarchar(25) NOT NULL CONSTRAINT DF_IASegmentAuthor_EndDate DEFAULT(''),
	BHLIdentifierID int NULL,
	IdentifierValue nvarchar(125) NOT NULL CONSTRAINT DF_IASegmentAuthor_IdentifierValue DEFAULT(''),
	CreatedDate datetime NOT NULL CONSTRAINT DF_IASegmentAuthor_CreatedDate DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_IASegmentAuthor_LastModifiedDate DEFAULT (GETDATE()),
	CONSTRAINT PK_IASegmentAuthor PRIMARY KEY CLUSTERED ( SegmentAuthorID ASC )
)
GO

ALTER TABLE dbo.IASegmentAuthor ADD CONSTRAINT FK_IASegmentAuthor_Segment
	FOREIGN KEY(SegmentID) REFERENCES dbo.IASegment (SegmentID)
GO
