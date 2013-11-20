CREATE TABLE dbo.OAIRecordLanguage
	(
	OAILanguage nvarchar(50) NOT NULL CONSTRAINT PK_OAIRecordLanguage PRIMARY KEY CLUSTERED,
	BHLLanguageCode nvarchar(10) NULL
	)
GO
