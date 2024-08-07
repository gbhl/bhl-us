SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemIdentifier](
	[ItemIdentifierID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[IdentifierID] [int] NOT NULL,
	[IdentifierValue] [nvarchar](125) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemIdentifier] PRIMARY KEY CLUSTERED 
(
	[ItemIdentifierID] ASC
)
)

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemIdentifier_IdentifierValue] ON [dbo].[ItemIdentifier]
(
	[IdentifierValue] ASC
)
INCLUDE ( 	[IdentifierID],
	[ItemID])
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemIdentifier_ItemID] ON [dbo].[ItemIdentifier]
(
	[ItemID] ASC
)
INCLUDE ( 	[IdentifierValue],
	[IdentifierID])
GO
ALTER TABLE [dbo].[ItemIdentifier] ADD  CONSTRAINT [DF_ItemIdentifier_IdentifierValue]  DEFAULT ('') FOR [IdentifierValue]
GO
ALTER TABLE [dbo].[ItemIdentifier] ADD  CONSTRAINT [DF_ItemIdentifier_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemIdentifier] ADD  CONSTRAINT [DF_ItemIdentifier_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemIdentifier] ADD  CONSTRAINT [DF_ItemIdentifier_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemIdentifier] ADD  CONSTRAINT [DF_ItemIdentifier_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemIdentifier]  WITH CHECK ADD  CONSTRAINT [FK_ItemIdentifier_Identifier] FOREIGN KEY([IdentifierID])
REFERENCES [dbo].[Identifier] ([IdentifierID])
GO
ALTER TABLE [dbo].[ItemIdentifier] CHECK CONSTRAINT [FK_ItemIdentifier_Identifier]
GO
ALTER TABLE [dbo].[ItemIdentifier]  WITH CHECK ADD  CONSTRAINT [FK_ItemIdentifier_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemIdentifier] CHECK CONSTRAINT [FK_ItemIdentifier_Item]
GO
