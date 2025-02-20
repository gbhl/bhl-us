SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemLanguage](
	[ItemLanguageID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[LanguageCode] [nvarchar](10) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
 CONSTRAINT [PK_ItemLanguage] PRIMARY KEY CLUSTERED 
(
	[ItemLanguageID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemLanguage_ItemID] ON [dbo].[ItemLanguage]
(
	[ItemID] ASC
)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemLanguage_LanguageCode] ON [dbo].[ItemLanguage]
(
	[LanguageCode] ASC
)
INCLUDE ( 	[ItemID])
GO
ALTER TABLE [dbo].[ItemLanguage] ADD  CONSTRAINT [DF__ItemLangu__Creat__3BF5C5A4]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemLanguage] ADD  CONSTRAINT [DF_ItemLanguage_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ItemLanguage_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemLanguage] CHECK CONSTRAINT [FK_ItemLanguage_Item]
GO
ALTER TABLE [dbo].[ItemLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ItemLanguage_Language] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[Language] ([LanguageCode])
GO
ALTER TABLE [dbo].[ItemLanguage] CHECK CONSTRAINT [FK_ItemLanguage_Language]
GO
