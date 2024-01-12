CREATE TABLE [dbo].[ItemCreator](
	[ItemCreatorID] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[ImportStatusID] [int] NOT NULL,
	[ImportSourceID] [int] NULL,
	[CreatorName] [nvarchar](255) NOT NULL,
	[CreatorRoleTypeID] [int] NOT NULL,
	[SequenceOrder] [smallint] NULL,
	[ProductionDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_ItemCreator] PRIMARY KEY CLUSTERED 
	(
		[ItemCreatorID] ASC
	)
)
GO

ALTER TABLE [dbo].[ItemCreator]  WITH CHECK ADD  CONSTRAINT [FK_ItemCreator_ImportSource] FOREIGN KEY([ImportSourceID])
REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[ItemCreator]  WITH CHECK ADD  CONSTRAINT [FK_ItemCreator_ImportStatus] FOREIGN KEY([ImportStatusID])
REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
