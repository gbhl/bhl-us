CREATE TABLE [dbo].[PageNameCount] (
    [NameConfirmed] NVARCHAR (100) NOT NULL,
    [Qty]           INT            NOT NULL,
    [RefreshDate]   DATETIME       NOT NULL,
    CONSTRAINT [PK_PageNameCount] PRIMARY KEY CLUSTERED ([NameConfirmed] ASC)
);

