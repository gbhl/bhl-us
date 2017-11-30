CREATE TABLE [dbo].[AuthorName] (
    [AuthorNameID]       INT            IDENTITY (1, 1) NOT NULL,
    [AuthorID]           INT            NOT NULL,
    [FullName]           NVARCHAR (300) CONSTRAINT [DF_AuthorName_FullName] DEFAULT ('') NOT NULL,
    [LastName]           NVARCHAR (150) CONSTRAINT [DF_AuthorName_LastName] DEFAULT ('') NOT NULL,
    [FirstName]          NVARCHAR (150) CONSTRAINT [DF_AuthorName_FirstName] DEFAULT ('') NOT NULL,
    [FullerForm]         NVARCHAR (150) CONSTRAINT [DF_AuthorName_FullerForm] DEFAULT ('') NOT NULL,
    [IsPreferredName]    SMALLINT       CONSTRAINT [DF_AuthorName_IsPreferredName] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_AuthorName_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_AuthorName_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_AuthorName_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_AuthorName_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_AuthorName] PRIMARY KEY CLUSTERED ([AuthorNameID] ASC),
    CONSTRAINT [FK_AuthorName_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID])
);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorName_AuthorID]
    ON [dbo].[AuthorName]([AuthorID] ASC, [IsPreferredName] ASC)
    INCLUDE([FullName]);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorName_FullName]
    ON [dbo].[AuthorName]([FullName] ASC)
    INCLUDE([AuthorID]);


GO
CREATE NONCLUSTERED INDEX IX_AuthorName_LastFirst
	ON dbo.AuthorName (LastName,FirstName)
GO
