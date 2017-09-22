CREATE TABLE [dbo].[Title_Identifier] (
    [TitleIdentifierID]  INT            IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT            NOT NULL,
    [IdentifierID]       INT            NOT NULL,
    [IdentifierValue]    NVARCHAR (125) CONSTRAINT [DF_Title_Identifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_Title_Identifier_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_Title_Identifier_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_Title_Identifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_Title_Identifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Title_Identifier] PRIMARY KEY CLUSTERED ([TitleIdentifierID] ASC),
    CONSTRAINT [FK_Title_Identifier_Identifier] FOREIGN KEY ([IdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID]),
    CONSTRAINT [FK_Title_Identifier_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Title_Identifier_IdentifierValue]
    ON [dbo].[Title_Identifier]([IdentifierValue] ASC)
    INCLUDE([TitleID], [IdentifierID]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_Identifier_TitleID]
    ON [dbo].[Title_Identifier]([TitleID] ASC)
    INCLUDE([IdentifierValue], [IdentifierID]);


GO
