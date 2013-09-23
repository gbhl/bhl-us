CREATE TABLE dbo.SegmentClusterType
(
	SegmentClusterTypeID int NOT NULL,
	SegmentClusterTypeName [nvarchar](40) NOT NULL DEFAULT(''),
	SegmentClusterTypeLabel nvarchar(40) NOT NULL DEFAULT(''),
	DisplaySequence smallint NOT NULL,
	CONSTRAINT PK_SegmentClusterType PRIMARY KEY CLUSTERED
	(
		SegmentClusterTypeID ASC
	)
)

