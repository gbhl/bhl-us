CREATE TABLE [dbo].[AuthorType] (
    [AuthorTypeID]       INT           IDENTITY (1, 1) NOT NULL,
    [AuthorTypeName]     NVARCHAR (50) CONSTRAINT [DF_AuthorType_AuthorTypeName] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_AuthorType_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_AuthorType_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_AuthorType_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_AuthorType_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_AuthorType] PRIMARY KEY CLUSTERED ([AuthorTypeID] ASC)
);


GO
