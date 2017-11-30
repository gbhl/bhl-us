CREATE TABLE [dbo].[ItemStatus] (
    [ItemStatusID]   INT           NOT NULL,
    [ItemStatusName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ItemStatus] PRIMARY KEY CLUSTERED ([ItemStatusID] ASC)
);


GO
