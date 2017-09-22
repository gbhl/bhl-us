CREATE TABLE [dbo].[TitleLanguage] (
    [TitleLanguageID] INT           IDENTITY (1, 1) NOT NULL,
    [TitleID]         INT           NOT NULL,
    [LanguageCode]    NVARCHAR (10) NOT NULL,
    [CreationDate]    DATETIME      CONSTRAINT [DF__TitleLang__Creat__3A0D7D32] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]  INT           CONSTRAINT [DF_TitleLanguage_CreationUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleLanguage] PRIMARY KEY CLUSTERED ([TitleLanguageID] ASC),
    CONSTRAINT [FK_TitleLanguage_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]),
    CONSTRAINT [FK_TitleLanguage_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleLanguage_LanguageCode]
    ON [dbo].[TitleLanguage]([LanguageCode] ASC)
    INCLUDE([TitleID]);


GO
