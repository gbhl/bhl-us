SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCollection](
	[ItemCollectionID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[CollectionID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ItemCollection] PRIMARY KEY CLUSTERED 
(
	[ItemCollectionID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemCollection_CollectionID] ON [dbo].[ItemCollection]
(
	[CollectionID] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_ItemCollection_ItemIDCollectionID] ON [dbo].[ItemCollection]
(
	[ItemID] ASC,
	[CollectionID] ASC
)
GO
ALTER TABLE [dbo].[ItemCollection] ADD  CONSTRAINT [DF_ItemCollection_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemCollection]  WITH CHECK ADD  CONSTRAINT [FK_ItemCollection_Collection] FOREIGN KEY([CollectionID])
REFERENCES [dbo].[Collection] ([CollectionID])
GO
ALTER TABLE [dbo].[ItemCollection] CHECK CONSTRAINT [FK_ItemCollection_Collection]
GO
ALTER TABLE [dbo].[ItemCollection]  WITH CHECK ADD  CONSTRAINT [FK_ItemCollection_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemCollection] CHECK CONSTRAINT [FK_ItemCollection_Item]
GO
