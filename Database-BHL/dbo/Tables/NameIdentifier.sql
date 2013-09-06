CREATE TABLE [dbo].[NameIdentifier] (
    [NameIdentifierID]   INT            IDENTITY (1, 1) NOT NULL,
    [NameResolvedID]     INT            NOT NULL,
    [IdentifierID]       INT            NOT NULL,
    [IdentifierValue]    NVARCHAR (100) CONSTRAINT [DF_NameIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_NameIdentifier_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_NameIdentifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            NULL,
    [LastModifiedUserID] INT            NULL,
    CONSTRAINT [PK_NameIdentifier] PRIMARY KEY CLUSTERED ([NameIdentifierID] ASC),
    CONSTRAINT [FK_NameIdentifier_Identifier] FOREIGN KEY ([IdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID]),
    CONSTRAINT [FK_NameIdentifier_NameResolved] FOREIGN KEY ([NameResolvedID]) REFERENCES [dbo].[NameResolved] ([NameResolvedID])
);


GO
CREATE NONCLUSTERED INDEX [IX_NameIdentifier_IdentifierIDValue]
    ON [dbo].[NameIdentifier]([IdentifierID] ASC, [IdentifierValue] ASC)
    INCLUDE([NameResolvedID]);


GO
CREATE NONCLUSTERED INDEX [IX_NameIdentifier_NameResolvedID]
    ON [dbo].[NameIdentifier]([NameResolvedID] ASC)
    INCLUDE([IdentifierID], [IdentifierValue]);

