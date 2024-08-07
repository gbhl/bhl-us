SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemNameFileLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[DoCreate] [bit] NOT NULL,
	[DoUpload] [bit] NOT NULL,
	[LastCreateDate] [datetime] NULL,
	[LastUploadDate] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ItemNameFileLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)
)

GO
ALTER TABLE [dbo].[ItemNameFileLog] ADD  CONSTRAINT [DF__ItemNameF__DoCre__1C7D1A4B]  DEFAULT ((1)) FOR [DoCreate]
GO
ALTER TABLE [dbo].[ItemNameFileLog] ADD  CONSTRAINT [DF__ItemNameF__DoUpl__1D713E84]  DEFAULT ((1)) FOR [DoUpload]
GO
ALTER TABLE [dbo].[ItemNameFileLog] ADD  CONSTRAINT [DF__ItemNameF__Creat__1E6562BD]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemNameFileLog] ADD  CONSTRAINT [DF__ItemNameF__LastM__1F5986F6]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemNameFileLog]  WITH CHECK ADD  CONSTRAINT [FK_ItemNameFileLog_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemNameFileLog] CHECK CONSTRAINT [FK_ItemNameFileLog_Item]
GO
