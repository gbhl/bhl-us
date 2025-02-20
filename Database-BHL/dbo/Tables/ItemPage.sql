SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPage](
	[ItemPageID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[PageID] [int] NOT NULL,
	[SequenceOrder] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemPage] PRIMARY KEY CLUSTERED 
(
	[ItemPageID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemPage_ItemID] ON [dbo].[ItemPage]
(
	[ItemID] ASC
)
INCLUDE ( 	[PageID],
	[SequenceOrder])
GO
CREATE NONCLUSTERED INDEX [IX_ItemPage_PageID] ON [dbo].[ItemPage]
(
	[PageID] ASC
)
INCLUDE ( 	[ItemID])
GO
ALTER TABLE [dbo].[ItemPage] ADD  CONSTRAINT [DF_ItemPage_SequenceOrder]  DEFAULT ((1)) FOR [SequenceOrder]
GO
ALTER TABLE [dbo].[ItemPage] ADD  CONSTRAINT [DF_ItemPage_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemPage] ADD  CONSTRAINT [DF_ItemPage_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemPage] ADD  CONSTRAINT [DF_ItemPage_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemPage] ADD  CONSTRAINT [DF_ItemPage_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemPage]  WITH CHECK ADD  CONSTRAINT [FK_ItemPage_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemPage] CHECK CONSTRAINT [FK_ItemPage_Item]
GO
ALTER TABLE [dbo].[ItemPage]  WITH CHECK ADD  CONSTRAINT [FK_ItemPage_Page] FOREIGN KEY([PageID])
REFERENCES [dbo].[Page] ([PageID])
GO
ALTER TABLE [dbo].[ItemPage] CHECK CONSTRAINT [FK_ItemPage_Page]
GO
