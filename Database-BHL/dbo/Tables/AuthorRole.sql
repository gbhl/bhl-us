CREATE TABLE [dbo].[AuthorRole] (
    [AuthorRoleID]       INT            NOT NULL,
    [RoleDescription]    NVARCHAR (255) CONSTRAINT [DF_AuthorRole_RoleDescription] DEFAULT ('') NOT NULL,
    [MARCDataFieldTag]   NVARCHAR (3)   CONSTRAINT [DF_AuthorRole_MARCDataFieldTag] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_AuthorRole_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifedDate]    DATETIME       CONSTRAINT [DF_AuthorRole_LastModifedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_AuthorRole_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_AuthorRole_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_AuthorRole] PRIMARY KEY CLUSTERED ([AuthorRoleID] ASC)
);


GO
