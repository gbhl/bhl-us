CREATE TABLE dbo.CurrentStats (
	TitleActive int NOT NULL,
	TitleTotal int NOT NULL,
	BookActive int NOT NULL,
	BookTotal int NOT NULL,
	PageActive int NOT NULL,
	PageTotal int NOT NULL,
	SegmentActive int NOT NULL,
	SegmentTotal int NOT NULL,
	BookSegmentActive int NOT NULL,
	BookSegmentTotal int NOT NULL,
	NameActive int NOT NULL,
	NameTotal int NOT NULL,
	UniqueNameActive int NOT NULL,
	UniqueNameTotal int NOT NULL,
	VerifiedNameActive int NOT NULL,
	VerifiedNameTotal int NOT NULL,
	LastModifiedDate datetime CONSTRAINT DF_CurrentStats_LastModifiedDate DEFAULT GETDATE() NOT NULL
)
GO
