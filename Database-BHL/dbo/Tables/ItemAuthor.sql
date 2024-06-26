SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemAuthor](
	[ItemAuthorID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
	[SequenceOrder] [smallint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_ItemAuthor] PRIMARY KEY CLUSTERED 
(
	[ItemAuthorID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [IX_ItemAuthor_AuthorID] ON [dbo].[ItemAuthor]
(
	[AuthorID] ASC
)
INCLUDE ( 	[ItemID])
GO
CREATE NONCLUSTERED INDEX [IX_ItemAuthor_ItemID] ON [dbo].[ItemAuthor]
(
	[ItemID] ASC
)
INCLUDE ( 	[AuthorID],
	[SequenceOrder])
GO
ALTER TABLE [dbo].[ItemAuthor] ADD  CONSTRAINT [DF_ItemAuthor_SequenceOrder]  DEFAULT ((1)) FOR [SequenceOrder]
GO
ALTER TABLE [dbo].[ItemAuthor] ADD  CONSTRAINT [DF_ItemAuthor_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemAuthor] ADD  CONSTRAINT [DF_ItemAuthor_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemAuthor] ADD  CONSTRAINT [DF_ItemAuthor_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemAuthor] ADD  CONSTRAINT [DF_ItemAuthor_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemAuthor]  WITH CHECK ADD  CONSTRAINT [FK_ItemAuthor_Author] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Author] ([AuthorID])
GO
ALTER TABLE [dbo].[ItemAuthor] CHECK CONSTRAINT [FK_ItemAuthor_Author]
GO
ALTER TABLE [dbo].[ItemAuthor]  WITH CHECK ADD  CONSTRAINT [FK_ItemAuthor_Segment] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemAuthor] CHECK CONSTRAINT [FK_ItemAuthor_Segment]
GO
