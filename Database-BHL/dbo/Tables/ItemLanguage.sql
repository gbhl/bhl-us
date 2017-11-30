CREATE TABLE [dbo].[ItemLanguage] (
    [ItemLanguageID] INT           IDENTITY (1, 1) NOT NULL,
    [ItemID]         INT           NOT NULL,
    [LanguageCode]   NVARCHAR (10) NOT NULL,
    [CreationDate]   DATETIME      CONSTRAINT [DF__ItemLangu__Creat__3BF5C5A4] DEFAULT (getdate()) NOT NULL,
    [CreationUserID] INT           CONSTRAINT [DF_ItemLanguage_CreationUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ItemLanguage] PRIMARY KEY CLUSTERED ([ItemLanguageID] ASC),
    CONSTRAINT [FK_ItemLanguage_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]),
    CONSTRAINT [FK_ItemLanguage_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_ItemLanguage_LanguageCode]
    ON [dbo].[ItemLanguage]([LanguageCode] ASC)
    INCLUDE([ItemID]);


GO

CREATE NONCLUSTERED INDEX [IX_ItemLanguage_ItemID] ON [dbo].[ItemLanguage]
(
	[ItemID] ASC
)
GO

