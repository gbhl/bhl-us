CREATE TABLE [dbo].[IndicatedPage] (
    [PageID]             INT           NOT NULL,
    [Sequence]           SMALLINT      NOT NULL,
    [PagePrefix]         NVARCHAR (40) NULL,
    [PageNumber]         NVARCHAR (20) NULL,
    [Implied]            BIT           NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_IndicatedPage_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_IndicatedPage_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_IndicatedPage_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_IndicatedPage_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_IndicatedPage] PRIMARY KEY CLUSTERED ([PageID] ASC, [Sequence] ASC),
    CONSTRAINT [IndicatedPage_FK00] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IndicatedPage_PageNumber]
    ON [dbo].[IndicatedPage]([PageNumber] ASC)
    INCLUDE([PageID], [PagePrefix]);


GO
