SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemTypeID] [int] NULL,
	[ItemStatusID] [int] NOT NULL,
	[ItemSourceID] [int] NULL,
	[VaultID] [int] NULL,
	[FileRootFolder] [nvarchar](250) NULL,
	[ItemDescription] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_Item_StatusItem] ON [dbo].[Item]
(
	[ItemStatusID] ASC,
	[ItemID] ASC
)
INCLUDE ( 	[CreationDate])
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_ItemStatusID]  DEFAULT ((10)) FOR [ItemStatusID]
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_FileRootFolder]  DEFAULT ('') FOR [FileRootFolder]
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemSource] FOREIGN KEY([ItemSourceID])
REFERENCES [dbo].[ItemSource] ([ItemSourceID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemSource]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemStatus] FOREIGN KEY([ItemStatusID])
REFERENCES [dbo].[ItemStatus] ([ItemStatusID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemStatus]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_ItemType] FOREIGN KEY([ItemTypeID])
REFERENCES [dbo].[ItemType] ([ItemTypeID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_ItemType]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Vault] FOREIGN KEY([VaultID])
REFERENCES [dbo].[Vault] ([VaultID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Vault]
GO
