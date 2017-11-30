CREATE TABLE [dbo].[TitleKeyword] (
    [TitleKeywordID]     INT           IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT           NOT NULL,
    [KeywordID]          INT           NOT NULL,
    [MarcDataFieldTag]   NVARCHAR (50) NULL,
    [MarcSubFieldCode]   NVARCHAR (50) NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_TitleKeyword_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_TitleKeyword_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_TitleKeyword_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_TitleKeyword_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleKeyword] PRIMARY KEY CLUSTERED ([TitleKeywordID] ASC),
    CONSTRAINT [FK_TitleKeyword_Keyword] FOREIGN KEY ([KeywordID]) REFERENCES [dbo].[Keyword] ([KeywordID]),
    CONSTRAINT [FK_TitleKeyword_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleKeyword_KeywordID]
    ON [dbo].[TitleKeyword]([KeywordID] ASC)
    INCLUDE([TitleID]);


GO
CREATE NONCLUSTERED INDEX [IX_TitleKeyword_TitleID]
    ON [dbo].[TitleKeyword]([TitleID] ASC)
    INCLUDE([KeywordID]);


GO
