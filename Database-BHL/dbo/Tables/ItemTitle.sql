SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemTitle](
	[ItemTitleID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[TitleID] [int] NOT NULL,
	[ItemSequence] [smallint] NULL,
	[IsPrimary] [smallint] NOT NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemTitle] PRIMARY KEY CLUSTERED 
(
	[ItemTitleID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemTitle_ItemID] ON [dbo].[ItemTitle]
(
	[ItemID] ASC
)
INCLUDE ( 	[TitleID],
	[ItemSequence])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ItemTitle_TitleID] ON [dbo].[ItemTitle]
(
	[TitleID] ASC,
	[ItemID] ASC
)
GO
ALTER TABLE [dbo].[ItemTitle] ADD  CONSTRAINT [DF_ItemTitle_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[ItemTitle] ADD  CONSTRAINT [DF_ItemTitle_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemTitle] ADD  CONSTRAINT [DF_ItemTitle_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemTitle] ADD  CONSTRAINT [DF_ItemTitle_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemTitle] ADD  CONSTRAINT [DF_ItemTitle_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemTitle]  WITH CHECK ADD  CONSTRAINT [FK_ItemTitle_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemTitle] CHECK CONSTRAINT [FK_ItemTitle_Item]
GO
ALTER TABLE [dbo].[ItemTitle]  WITH CHECK ADD  CONSTRAINT [FK_ItemTitle_Title] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Title] ([TitleID])
GO
ALTER TABLE [dbo].[ItemTitle] CHECK CONSTRAINT [FK_ItemTitle_Title]
GO
