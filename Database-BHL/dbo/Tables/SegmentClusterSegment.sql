CREATE TABLE [dbo].[SegmentClusterSegment] (
    [SegmentID]          INT      NOT NULL,
    [SegmentClusterID]   INT      NOT NULL,
    [IsPrimary]          SMALLINT CONSTRAINT [DF_SegmentClusterSegment_IsPrimary] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_SegmentClusterSegment_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_SegmentClusterSegment_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_SegmentClusterSegment_CreationUserID] DEFAULT ((1)) NOT NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_SegmentClusterSegment_LastModifiedUserID] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SegmentClusterSegment] PRIMARY KEY CLUSTERED ([SegmentID] ASC),
    CONSTRAINT [FK_SegmentClusterSegment_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID]),
    CONSTRAINT [FK_SegmentClusterSegment_SegmentCluster] FOREIGN KEY ([SegmentClusterID]) REFERENCES [dbo].[SegmentCluster] ([SegmentClusterID])
);

