CREATE TABLE [dbo].[Language] (
    [LanguageCode] NVARCHAR (10)  NOT NULL,
    [LanguageName] NVARCHAR (20)  CONSTRAINT [DF_Language_LanguageName] DEFAULT ('') NOT NULL,
    [Note]         NVARCHAR (255) NULL,
    CONSTRAINT [aaaaaLanguage_PK] PRIMARY KEY CLUSTERED ([LanguageCode] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Language_LanguageCode]
    ON [dbo].[Language]([LanguageCode] ASC)
    INCLUDE([LanguageName]);


GO
