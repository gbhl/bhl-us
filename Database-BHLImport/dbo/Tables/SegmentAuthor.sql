CREATE TABLE [dbo].[SegmentAuthor] (
    [SegmentAuthorID]  INT            IDENTITY (1, 1) NOT NULL,
    [ImportSourceID]   INT            NOT NULL,
    [SegmentID]        INT            NOT NULL,
    [BioStorID]        NVARCHAR (100) CONSTRAINT [DF_SegmentAuthor_BioStorID] DEFAULT ('') NOT NULL,
    [LastName]         NVARCHAR (150) CONSTRAINT [DF_SegmentAuthor_LastName] DEFAULT ('') NOT NULL,
    [FirstName]        NVARCHAR (150) CONSTRAINT [DF_SegmentAuthor_FirstName] DEFAULT ('') NOT NULL,
    [SequenceOrder]    INT            CONSTRAINT [DF_SegmentAuthor_SequenceOrder] DEFAULT ((1)) NOT NULL,
    [VIAFIdentifier]   NVARCHAR (20)  CONSTRAINT [DF_SegmentAuthor_VIAFIdentifier] DEFAULT ('') NOT NULL,
    [BHLAuthorID]      INT            NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_SegmentAuthor_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_SegmentAuthor_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SegmentAuthor] PRIMARY KEY CLUSTERED ([SegmentAuthorID] ASC),
    CONSTRAINT [FK_SegmentAuthor_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
);

