CREATE TABLE  dbo.OAIRecordRelatedTitleTypeVariant
	(
		TitleType nvarchar(50) NOT NULL CONSTRAINT PK_OAIRecordTypeTypeVariant PRIMARY KEY CLUSTERED,
		BHLTitleVariantTypeID int NOT NULL
	)
GO