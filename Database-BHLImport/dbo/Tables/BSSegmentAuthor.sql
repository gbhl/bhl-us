CREATE TABLE [dbo].[BSSegmentAuthor] (
    [SegmentAuthorID]  INT            IDENTITY (1, 1) NOT NULL,
    [ImportSourceID]   INT            NOT NULL,
    [SegmentID]        INT            NOT NULL,
    [BioStorID]        NVARCHAR (100) CONSTRAINT [DF_BSSegmentAuthor_BioStorID] DEFAULT ('') NOT NULL,
    [LastName]         NVARCHAR (150) CONSTRAINT [DF_BSSegmentAuthor_LastName] DEFAULT ('') NOT NULL,
    [FirstName]        NVARCHAR (150) CONSTRAINT [DF_BSSegmentAuthor_FirstName] DEFAULT ('') NOT NULL,
    [SequenceOrder]    INT            CONSTRAINT [DF_BSSegmentAuthor_SequenceOrder] DEFAULT ((1)) NOT NULL,
    [VIAFIdentifier]   NVARCHAR (20)  CONSTRAINT [DF_BSSegmentAuthor_VIAFIdentifier] DEFAULT ('') NOT NULL,
    [BHLAuthorID]      INT            NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_BSSegmentAuthor_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_BSSegmentAuthor_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BSSegmentAuthor] PRIMARY KEY CLUSTERED ([SegmentAuthorID] ASC),
    CONSTRAINT [FK_BSSegmentAuthor_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID])
);
GO

CREATE NONCLUSTERED INDEX [IX_BSSegmentAuthor_SegmentAuthor] 
	ON [dbo].[BSSegmentAuthor] ([SegmentID], [BHLAuthorID])
INCLUDE ([ImportSourceID], [SequenceOrder]);
GO

