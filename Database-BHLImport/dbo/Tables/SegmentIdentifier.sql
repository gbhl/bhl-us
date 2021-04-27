CREATE TABLE dbo.SegmentIdentifier (
	SegmentIdentifierID int IDENTITY(1,1) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID int NULL,
	BarCode nvarchar(200) NOT NULL,
	SegmentSequenceOrder smallint NOT NULL,
	IdentifierName nvarchar(40) NOT NULL,
	IdentifierValue nvarchar(125) NOT NULL,
	ProductionDate datetime NULL,
	CreationDate datetime NOT NULL CONSTRAINT DF_SegmentIdentifier_CreationDate DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_SegmentIdentifier_LastModifiedDate DEFAULT (GETDATE()),
	CONSTRAINT SegmentIdentifier_PK PRIMARY KEY NONCLUSTERED ( SegmentIdentifierID ASC )
)
GO

ALTER TABLE [dbo].[SegmentIdentifier] ADD CONSTRAINT [FK_SegmentIdentifier_ImportSource] 
	FOREIGN KEY([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[SegmentIdentifier] ADD CONSTRAINT [FK_SegmentIdentifier_ImportStatus] 
	FOREIGN KEY([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO

