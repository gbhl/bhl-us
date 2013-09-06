CREATE TABLE [dbo].[NameSegment] (
    [NameSegmentID]      INT      IDENTITY (1, 1) NOT NULL,
    [NameID]             INT      NOT NULL,
    [SegmentID]          INT      NOT NULL,
    [NameSourceID]       INT      NOT NULL,
    [IsFirstOccurrence]  SMALLINT CONSTRAINT [DF_NameSegment_IsFirstOccurence] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_NameSegment_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_NameSegment_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      NULL,
    [LastModifiedUserID] INT      NULL,
    CONSTRAINT [PK_NameSegment] PRIMARY KEY CLUSTERED ([NameSegmentID] ASC),
    CONSTRAINT [FK_NameSegment_Name] FOREIGN KEY ([NameID]) REFERENCES [dbo].[Name] ([NameID]),
    CONSTRAINT [FK_NameSegment_NameSource] FOREIGN KEY ([NameSourceID]) REFERENCES [dbo].[NameSource] ([NameSourceID]),
    CONSTRAINT [FK_NameSegment_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);

