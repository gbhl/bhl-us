CREATE TABLE dbo.BSSegmentStatus (
	SegmentStatusID		int				NOT NULL,
	StatusName			nvarchar(30)	CONSTRAINT DF_BSSegmentStatus_Status DEFAULT ('') NOT NULL,
	StatusLabel			nvarchar(30)	CONSTRAINT DF_BSSegmentStatus_Label DEFAULT ('') NOT NULL,
	Description			nvarchar(4000)	CONSTRAINT DF_BSSegmentStatus_Description DEFAULT ('') NOT NULL,
	CreationDate		datetime		CONSTRAINT DF_BSSegmentStatus_CreationDate DEFAULT (GETDATE())NOT NULL,
	LastModifiedDate	datetime		CONSTRAINT DF_BSSegmentStatus_LastModifiedDate DEFAULT (GETDATE()) NOT NULL,
	CONSTRAINT PK_BSSegmentStatus PRIMARY KEY CLUSTERED (SegmentStatusID ASC)
);
