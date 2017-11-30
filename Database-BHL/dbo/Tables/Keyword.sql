CREATE TABLE [dbo].[Keyword] (
    [KeywordID]          INT           IDENTITY (1, 1) NOT NULL,
    [Keyword]            NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_Keyword_Keyword] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_Keyword_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_Keyword_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_Keyword_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_Keyword_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Keyword] PRIMARY KEY CLUSTERED ([KeywordID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Keyword_Keyword]
    ON [dbo].[Keyword]([Keyword] ASC);


GO
