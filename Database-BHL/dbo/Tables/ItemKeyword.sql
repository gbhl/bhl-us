SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemKeyword](
	[ItemKeywordID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[KeywordID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemKeyword] PRIMARY KEY CLUSTERED 
(
	[ItemKeywordID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemKeyword_ItemID] ON [dbo].[ItemKeyword]
(
	[ItemID] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_ItemKeyword_KeywordID] ON [dbo].[ItemKeyword]
(
	[KeywordID] ASC
)
INCLUDE ( 	[ItemID])
GO
ALTER TABLE [dbo].[ItemKeyword] ADD  CONSTRAINT [DF_ItemKeyword_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemKeyword] ADD  CONSTRAINT [DF_ItemKeyword_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemKeyword] ADD  CONSTRAINT [DF_ItemKeyword_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemKeyword] ADD  CONSTRAINT [DF_ItemKeyword_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ItemKeyword_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemKeyword] CHECK CONSTRAINT [FK_ItemKeyword_Item]
GO
ALTER TABLE [dbo].[ItemKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ItemKeyword_Keyword] FOREIGN KEY([KeywordID])
REFERENCES [dbo].[Keyword] ([KeywordID])
GO
ALTER TABLE [dbo].[ItemKeyword] CHECK CONSTRAINT [FK_ItemKeyword_Keyword]
GO
