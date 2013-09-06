CREATE TABLE [dbo].[SegmentAuthor] (
    [SegmentAuthorID]    INT      IDENTITY (1, 1) NOT NULL,
    [SegmentID]          INT      NOT NULL,
    [AuthorID]           INT      NOT NULL,
    [SequenceOrder]      SMALLINT CONSTRAINT [DF_SegmentAuthor_SequenceOrder] DEFAULT ((1)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_SegmentAuthor_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_SegmentAuthor_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_SegmentAuthor_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_SegmentAuthor_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SegmentAuthor] PRIMARY KEY CLUSTERED ([SegmentAuthorID] ASC),
    CONSTRAINT [FK_SegmentAuthor_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_SegmentAuthor_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);

