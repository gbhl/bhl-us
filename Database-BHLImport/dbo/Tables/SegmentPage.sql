CREATE TABLE dbo.SegmentPage (
	SegmentPageID int IDENTITY(1,1) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID int NULL,
	BarCode nvarchar(200) NOT NULL,
	SegmentSequenceOrder smallint NOT NULL,
	PageSequenceOrder int NULL,
	ProductionDate datetime NULL,
	CreationDate datetime NOT NULL CONSTRAINT DF_SegmentPage_CreationDate DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_SegmentPage_LastModifiedDate DEFAULT (GETDATE()),
	CONSTRAINT SegmentPage_PK PRIMARY KEY NONCLUSTERED ( SegmentPageID ASC )
)
GO

ALTER TABLE [dbo].[SegmentPage] ADD CONSTRAINT [FK_SegmentPage_ImportSource] 
	FOREIGN KEY([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[SegmentPage] ADD CONSTRAINT [FK_SegmentPage_ImportStatus] 
	FOREIGN KEY([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
