CREATE TABLE [dbo].[IAPage] (
    [PageID]           INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT            NOT NULL,
    [LocalFileName]    NVARCHAR (50)  CONSTRAINT [DF_pAGE_LocalFileName] DEFAULT ('') NOT NULL,
    [Sequence]         INT            NULL,
    [ExternalUrl]      NVARCHAR (500) NULL,
    [CreatedDate]      DATETIME       CONSTRAINT [DF_pAGE_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_pAGE_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_pAGE] PRIMARY KEY CLUSTERED ([PageID] ASC),
    CONSTRAINT [FK_Page_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAPage]
    ON [dbo].[IAPage]([ItemID] ASC, [Sequence] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IAPage_1]
    ON [dbo].[IAPage]([ItemID] ASC, [Sequence] ASC, [LocalFileName] ASC)
    INCLUDE([ExternalUrl]);

