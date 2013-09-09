CREATE TABLE [dbo].[IAScandata] (
    [ScandataID]       INT           IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT           NOT NULL,
    [Sequence]         INT           NOT NULL,
    [PageType]         NVARCHAR (50) CONSTRAINT [DF_Scandata_PageType] DEFAULT ('') NOT NULL,
    [PageNumber]       NVARCHAR (20) CONSTRAINT [DF_Scandata_PageNumber] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF_Scandata_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF_Scandata_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [Year]             NVARCHAR (20) NULL,
    [Volume]           NVARCHAR (20) NULL,
    [Issue]            NVARCHAR (20) NULL,
    [IssuePrefix]      NVARCHAR (20) NULL,
    CONSTRAINT [PK_Scandata] PRIMARY KEY CLUSTERED ([ScandataID] ASC),
    CONSTRAINT [FK_Scandata_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAScandata]
    ON [dbo].[IAScandata]([ItemID] ASC, [Sequence] ASC, [PageNumber] ASC, [PageType] ASC);

