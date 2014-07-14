CREATE TABLE [dbo].[SegmentKeyword] (
    [SegmentKeywordID]   INT      IDENTITY (1, 1) NOT NULL,
    [SegmentID]          INT      NOT NULL,
    [KeywordID]          INT      NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_SegmentKeyword_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_SegmentKeyword_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_SegmentKeyword_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_SegmentKeyword_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SegmentKeyword] PRIMARY KEY CLUSTERED ([SegmentKeywordID] ASC),
    CONSTRAINT [FK_SegmentKeyword_Keyword] FOREIGN KEY ([KeywordID]) REFERENCES [dbo].[Keyword] ([KeywordID]),
    CONSTRAINT [FK_SegmentKeyword_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);
GO

CREATE NONCLUSTERED INDEX [IX_SegmentKeyword_KeywordID]
ON [dbo].[SegmentKeyword] ([KeywordID])
INCLUDE ([SegmentID]);
GO

CREATE NONCLUSTERED INDEX [IX_SegmentKeyword_SegmentID]
ON [dbo].[SegmentKeyword] ([SegmentID]);
GO
