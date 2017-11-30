CREATE TABLE [dbo].[PageType] (
    [PageTypeID]          INT            IDENTITY (1, 1) NOT NULL,
    [PageTypeName]        NVARCHAR (30)  NOT NULL,
    [PageTypeDescription] NVARCHAR (255) NULL,
    CONSTRAINT [aaaaaPageType_PK] PRIMARY KEY CLUSTERED ([PageTypeID] ASC)
);


GO
