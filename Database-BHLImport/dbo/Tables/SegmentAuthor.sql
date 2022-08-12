CREATE TABLE dbo.SegmentAuthor
	(
	SegmentAuthorID int IDENTITY(1,1) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID	int NOT NULL,
	BarCode	nvarchar(200) NOT NULL CONSTRAINT DF_SegmentAuthor_BarCode DEFAULT(''),
	SegmentSequenceOrder int NOT NULL,
	SequenceOrder int NOT NULL,
	LastName nvarchar(150) NOT NULL CONSTRAINT DF_SegmentAuthor_LastName DEFAULT(''),
	FirstName nvarchar(150) NOT NULL CONSTRAINT DF_SegmentAuthor_FirstName DEFAULT(''),
	StartDate nvarchar(25) NOT NULL CONSTRAINT DF_SegmentAuthor_StartDate DEFAULT(''),
	EndDate nvarchar(25) NOT NULL CONSTRAINT DF_SegmentAuthor_EndDate DEFAULT(''),
	ProductionAuthorID int NULL,
	ProductionDate datetime NULL,
	CreatedDate datetime NOT NULL CONSTRAINT DF_SegmentAuthor_CreateDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_SegmentAuthor_LastModifiedDate DEFAULT(GETDATE()),
	CONSTRAINT SegmentAuthor_PK PRIMARY KEY NONCLUSTERED ( SegmentAuthorID ASC )
)
GO

ALTER TABLE [dbo].[SegmentAuthor] ADD CONSTRAINT [FK_SegmentAuthor_ImportSource] 
	FOREIGN KEY([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[SegmentAuthor] ADD CONSTRAINT [FK_SegmentAuthor_ImportStatus] 
	FOREIGN KEY([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
