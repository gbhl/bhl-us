CREATE TABLE dbo.SegmentAuthorIdentifier
	(
	SegmentAuthorIdentifierID int IDENTITY(1,1) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID	int NOT NULL,
	BarCode	nvarchar(200) NOT NULL CONSTRAINT DF_SegmentAuthorIdentifier_BarCode DEFAULT(''),
	SegmentSequenceOrder int NOT NULL,
	SequenceOrder int NOT NULL,
	ProductionIdentifierID int NOT NULL,
	IdentifierValue nvarchar(125) NOT NULL CONSTRAINT DF_SegmentAuthorIdentifier_IdentifierValue DEFAULT(''),
	ProductionDate datetime NULL,
	CreatedDate datetime NOT NULL CONSTRAINT DF_SegmentAuthorIdentifier_CreateDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_SegmentAuthorIdentifier_LastModifiedDate DEFAULT(GETDATE()),
	CONSTRAINT SegmentAuthorIdentifier_PK PRIMARY KEY NONCLUSTERED ( SegmentAuthorIdentifierID ASC )
)
GO

ALTER TABLE [dbo].[SegmentAuthorIdentifier] ADD CONSTRAINT [FK_SegmentAuthorIdentifier_ImportSource] 
	FOREIGN KEY([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[SegmentAuthorIdentifier] ADD CONSTRAINT [FK_SegmentAuthorIdentifier_ImportStatus] 
	FOREIGN KEY([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
