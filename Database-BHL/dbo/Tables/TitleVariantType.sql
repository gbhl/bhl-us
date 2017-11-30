CREATE TABLE [dbo].[TitleVariantType] (
    [TitleVariantTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [TitleVariantTypeName] NVARCHAR (30) CONSTRAINT [DF_TitleVariantType_TitleVariantTypeName] DEFAULT ('') NOT NULL,
    [MARCTag]              NVARCHAR (20) CONSTRAINT [DF_TitleVariantType_MARCTag] DEFAULT ('') NOT NULL,
    [MARCIndicator2]       NVARCHAR (1)  CONSTRAINT [DF_TitleVariantType_MARCIndicator2] DEFAULT ('') NOT NULL,
    [TitleVariantLabel]    NVARCHAR (30) CONSTRAINT [DF_TitleVariantType_TitleVariantLabel] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_TitleVariantType] PRIMARY KEY CLUSTERED ([TitleVariantTypeID] ASC)
);


GO
