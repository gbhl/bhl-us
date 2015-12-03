CREATE TABLE [dbo].[BSSegmentPage] (
    [SegmentPageID] INT      IDENTITY (1, 1) NOT NULL,
    [SegmentID]     INT      NOT NULL,
    [BHLPageID]     INT      NOT NULL,
    [SequenceOrder] SMALLINT CONSTRAINT [DF_BSSegmentPage_SequenceOrder] DEFAULT ((0)) NOT NULL,
    [CreationDate]  DATETIME CONSTRAINT [DF_BSSegmentPage_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BSSegmentPage] PRIMARY KEY CLUSTERED ([SegmentPageID] ASC),
    CONSTRAINT [FK_BSSegmentPage_BSSegment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[BSSegment] ([SegmentID])
);

GO
CREATE NONCLUSTERED INDEX [IX_BSSegmentPage_SegmentID] 
	ON [dbo].[BSSegmentPage]([SegmentID] ASC);

GO
