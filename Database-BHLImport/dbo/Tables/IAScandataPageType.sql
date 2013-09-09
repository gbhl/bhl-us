CREATE TABLE [dbo].[IAScandataPageType] (
    [ExternalPageType] NVARCHAR (50) NOT NULL,
    [BHLPageTypeName]  NVARCHAR (30) NOT NULL,
    [BHLPageTypeID]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ExternalPageType] ASC)
);

