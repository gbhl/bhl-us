CREATE TABLE [dbo].[WDEntityIdentifier](
	[BHLEntityType]		NVARCHAR(20) CONSTRAINT [DF_WDEntityIdentifier_BHLEntityType] DEFAULT('') NOT NULL,
	[BHLEntityID]		INT NOT NULL,
	[IdentifierType]	NVARCHAR(40) NOT NULL,
	[IdentifierValue]	NVARCHAR(125) NOT NULL,
	[HarvestDate]		DATETIME CONSTRAINT [DF_WDDEntityIdentifier_HarvestDate] DEFAULT (GETDATE()) NOT NULL,
	CONSTRAINT [PK_WDEntityIdentifier] PRIMARY KEY CLUSTERED 
	(
		[BHLEntityType] ASC,
		[BHLEntityID] ASC,
		[IdentifierType] ASC,
		[IdentifierValue] ASC
	)
)
GO
