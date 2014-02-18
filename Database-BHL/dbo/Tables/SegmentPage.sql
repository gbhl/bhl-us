CREATE TABLE [dbo].[SegmentPage] (
    [SegmentPageID]      INT      IDENTITY (1, 1) NOT NULL,
    [SegmentID]          INT      NOT NULL,
    [PageID]             INT      NOT NULL,
    [SequenceOrder]      SMALLINT CONSTRAINT [DF_SegmentPage_SequenceOrder] DEFAULT ((1)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_SegmentPage_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_SegmentPage_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_SegmentPage_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_SegmentPage_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SegmentPage] PRIMARY KEY CLUSTERED ([SegmentPageID] ASC),
    CONSTRAINT [FK_SegmentPage_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID]),
    CONSTRAINT [FK_SegmentPage_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);


GO
CREATE NONCLUSTERED INDEX [<Name of Missing Index, sysname,>]
    ON [dbo].[SegmentPage]([SegmentID] ASC)
    INCLUDE([PageID]);
GO

CREATE NONCLUSTERED INDEX [IX_SegmentPage_PageID]
ON [dbo].[SegmentPage] ([PageID])
INCLUDE ([SegmentID])
GO


