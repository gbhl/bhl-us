CREATE TABLE [dbo].[SegmentIdentifier] (
    [SegmentIdentifierID]   INT            IDENTITY (1, 1) NOT NULL,
    [SegmentID]             INT            NOT NULL,
    [IdentifierID]          INT            NOT NULL,
    [IdentifierValue]       NVARCHAR (125) CONSTRAINT [DF_SegmentIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [IsContainerIdentifier] SMALLINT       NULL,
    [CreationDate]          DATETIME       CONSTRAINT [DF_SegmentIdentifier_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]      DATETIME       CONSTRAINT [DF_SegmentIdentifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]        INT            CONSTRAINT [DF_SegmentIdentifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]    INT            CONSTRAINT [DF_SegmentIdentifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SegmentIdentifier] PRIMARY KEY CLUSTERED ([SegmentIdentifierID] ASC),
    CONSTRAINT [FK_SegmentIdentifier_Identifier] FOREIGN KEY ([IdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID]),
    CONSTRAINT [FK_SegmentIdentifier_Segment] FOREIGN KEY ([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);


GO
CREATE NONCLUSTERED INDEX [IX_SegmentIdentifier_IdentifierValue]
    ON [dbo].[SegmentIdentifier]([IdentifierValue] ASC)
    INCLUDE([IdentifierID], [SegmentID]);


GO
CREATE NONCLUSTERED INDEX [IX_SegmentIdentifier_SegmentID]
    ON [dbo].[SegmentIdentifier]([SegmentID] ASC)
    INCLUDE([IdentifierValue], [IdentifierID]);

