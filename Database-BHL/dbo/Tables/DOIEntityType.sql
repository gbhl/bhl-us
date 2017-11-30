CREATE TABLE [dbo].[DOIEntityType] (
    [DOIEntityTypeID]   INT           NOT NULL,
    [DOIEntityTypeName] NVARCHAR (50) CONSTRAINT [DF_Table_1_EntityTypeName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_DOIEntityType] PRIMARY KEY CLUSTERED ([DOIEntityTypeID] ASC)
);


GO
