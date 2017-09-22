CREATE TABLE [dbo].[PaginationStatus] (
    [PaginationStatusID]   INT           NOT NULL,
    [PaginationStatusName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PaginationStatus] PRIMARY KEY CLUSTERED ([PaginationStatusID] ASC)
);


GO
