CREATE TABLE dbo.OAIRecordRelatedTitleTypeAssociation
	(
		TitleType nvarchar(50) NOT NULL CONSTRAINT PK_OAIRecordTitleTypeAssociation PRIMARY KEY CLUSTERED,
		BHLTitleAssociationTypeID int NOT NULL
	)
GO