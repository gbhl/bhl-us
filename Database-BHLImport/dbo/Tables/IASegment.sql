CREATE TABLE dbo.IASegment (
	SegmentID int IDENTITY(1,1) NOT NULL,
	ItemID int NOT NULL,
	[Sequence] int NOT NULL CONSTRAINT DF_IASegment_Sequence DEFAULT(1),
	Title nvarchar(2000) NOT NULL CONSTRAINT DF_IASegment_Title DEFAULT(''),
	Volume nvarchar(100) NOT NULL CONSTRAINT DF_IASegment_Volume DEFAULT(''),
	Issue nvarchar(100) NOT NULL CONSTRAINT DF_IASegment_Issue DEFAULT(''),
	Series nvarchar(100) NOT NULL CONSTRAINT DF_IASegment_Series DEFAULT(''),
	[Date] nvarchar(20) NOT NULL CONSTRAINT DF_IASegment_Date DEFAULT(''),
	LanguageCode nvarchar(10) NOT NULL,
	BHLSegmentGenreID int NOT NULL,
	BHLSegmentGenreName nvarchar(50) NOT NULL CONSTRAINT DF_IASegment_GenreName DEFAULT(''),
	DOI nvarchar(50) NOT NULL,
	CreatedDate datetime NOT NULL CONSTRAINT DF_IASegment_CreatedDate DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_IASegment_LastModifiedDate DEFAULT (GETDATE()),
	CONSTRAINT PK_IASegment PRIMARY KEY CLUSTERED ( SegmentID ASC )
)
GO

ALTER TABLE dbo.IASegment ADD CONSTRAINT FK_IASegment_Item 
	FOREIGN KEY(ItemID) REFERENCES dbo.IAItem (ItemID)
GO
