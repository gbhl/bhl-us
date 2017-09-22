CREATE TABLE [dbo].[AuthorIdentifier] (
    [AuthorIdentifierID] INT            IDENTITY (1, 1) NOT NULL,
    [AuthorID]           INT            NOT NULL,
    [IdentifierID]       INT            NOT NULL,
    [IdentifierValue]    NVARCHAR (125) CONSTRAINT [DF_AuthorIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_AuthorIdentifier_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_AuthorIdentifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_AuthorIdentifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_AuthorIdentifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_AuthorIdentifier] PRIMARY KEY CLUSTERED ([AuthorIdentifierID] ASC),
    CONSTRAINT [FK_AuthorIdentifier_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_AuthorIdentifier_Identifier] FOREIGN KEY ([IdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID])
);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorIdentifier_AuthorID]
    ON [dbo].[AuthorIdentifier]([AuthorID] ASC)
    INCLUDE([IdentifierID], [IdentifierValue]);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorIdentifier_IdentifierValue]
    ON [dbo].[AuthorIdentifier]([IdentifierValue] ASC)
    INCLUDE([IdentifierID], [AuthorID]);


GO
