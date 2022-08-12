CREATE TABLE dbo.DOIHarvestLog
(
	DOIHarvestLogID		int				IDENTITY(1,1) NOT NULL,
	HarvesterName		nvarchar(200)	CONSTRAINT DF_DOIHarvestLog_HarvesterName  DEFAULT('') NOT NULL,
	DOIEntityTypeID		int				NOT NULL,
	EntityID			int				NOT NULL,
	OriginalDOIName		nvarchar(200)	CONSTRAINT DF_DOIHarvestLog_OriginalDOIName DEFAULT('') NOT NULL,
	NewDOIName			nvarchar(200)	CONSTRAINT DF_DOIHarvestLog_NewDOIName DEFAULT('') NOT NULL,
	CreationDate		datetime		CONSTRAINT DF_DOIHarvestLog_CreationDate DEFAULT(GETDATE()) NOT NULL,
	CONSTRAINT PK_DOIHarvestLog PRIMARY KEY CLUSTERED 
	(	
		DOIHarvestLogID ASC 
	)
)
GO
