CREATE TABLE [dbo].[MaterialType](
	[MaterialTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialTypeName] [nvarchar](60) CONSTRAINT [DF_MaterialType_MaterialTypeName]  DEFAULT ('') NOT NULL,
	[MaterialTypeLabel] [nvarchar](60) CONSTRAINT [DF_MaterialType_MaterialTypeLabel]  DEFAULT ('')NOT NULL,
	[MARCCode] [nchar](1) CONSTRAINT [DF_MaterialType_MARCCode]  DEFAULT ('') NOT NULL,
	CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED ( [MaterialTypeID] ASC )
)
GO
