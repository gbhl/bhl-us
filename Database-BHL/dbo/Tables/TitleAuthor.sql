CREATE TABLE [dbo].[TitleAuthor] (
    [TitleAuthorID]      INT      IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT      NOT NULL,
    [AuthorID]           INT      NOT NULL,
    [AuthorRoleID]       INT      NULL,
	[SequenceOrder]      SMALLINT NOT NULL CONSTRAINT [DF_TitleAuthor_SequenceOrder] DEFAULT(0),
	[Relationship]       NVARCHAR(150)  NOT NULL CONSTRAINT [DF_TitleAuthor_Relationship] DEFAULT(''),
	[TitleOfWork]        NVARCHAR(500)  NOT NULL CONSTRAINT [DF_TitleAuthor_TitleOfWork] DEFAULT(''),
    [CreationDate]       DATETIME CONSTRAINT [DF_TitleAuthor_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_TitleAuthor_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_TitleAuthor_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_TitleAuthor_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAuthor] PRIMARY KEY CLUSTERED ([TitleAuthorID] ASC),
    CONSTRAINT [FK_TitleAuthor_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_TitleAuthor_AuthorRole] FOREIGN KEY ([AuthorRoleID]) REFERENCES [dbo].[AuthorRole] ([AuthorRoleID]),
    CONSTRAINT [FK_TitleAuthor_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);
GO

CREATE NONCLUSTERED INDEX [IX_TitleAuthor_AuthorID]
    ON [dbo].[TitleAuthor]([AuthorID] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_TitleAuthor_TitleID]
    ON [dbo].[TitleAuthor]([TitleID] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_TitleAuthor_AuthorRole] 
	ON [dbo].[TitleAuthor] ([AuthorRoleID] ASC)
	INCLUDE ([TitleID], [AuthorID]);
GO
