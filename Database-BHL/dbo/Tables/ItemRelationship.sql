SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemRelationship](
	[RelationshipID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[ChildID] [int] NOT NULL,
	[SequenceOrder] [int] NOT NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemRelationship] PRIMARY KEY CLUSTERED 
(
	[RelationshipID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX IX_ItemRelationship_ChildID ON [dbo].[ItemRelationship] 
(
	[ChildID], 
	[SequenceOrder]
)
INCLUDE (	[ParentID])
GO
CREATE NONCLUSTERED INDEX IX_ItemRelationship_ParentID ON [dbo].[ItemRelationship] 
(
	[ParentID], 
	[SequenceOrder]
)
INCLUDE (	[ChildID])
GO
ALTER TABLE [dbo].[ItemRelationship] ADD  CONSTRAINT [DF_ItemRelationship_Sequence]  DEFAULT ((1)) FOR [SequenceOrder]
GO
ALTER TABLE [dbo].[ItemRelationship] ADD  CONSTRAINT [DF_ItemRelationship_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemRelationship] ADD  CONSTRAINT [DF_ItemRelationship_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemRelationship] ADD  CONSTRAINT [DF_ItemRelationship_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemRelationship] ADD  CONSTRAINT [DF_ItemRelationship_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemRelationship]  WITH CHECK ADD  CONSTRAINT [FK_ItemRelationship_ChildItem] FOREIGN KEY([ChildID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemRelationship] CHECK CONSTRAINT [FK_ItemRelationship_ChildItem]
GO
ALTER TABLE [dbo].[ItemRelationship]  WITH CHECK ADD  CONSTRAINT [FK_ItemRelationship_ParentItem] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemRelationship] CHECK CONSTRAINT [FK_ItemRelationship_ParentItem]
GO
