CREATE TABLE [dbo].[ItemCreatorIdentifier](
	[ItemCreatorIdentifierID] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[ImportStatusID] [int] NOT NULL,
	[ImportSourceID] [int] NULL,
	[SequenceOrder] [smallint] NULL,
	[IdentifierID] [int] NOT NULL,
	[IdentifierValue] [nvarchar](125) CONSTRAINT [DF_ItemCreatorIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
	[ProductionDate] [datetime] NULL,
	[CreatedDate] [datetime]  CONSTRAINT [DF_ItemCreatorIdentifier_CreatedDate] DEFAULT (getdate()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_ItemCreatorIdentifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_ItemCreatorIdentifier] PRIMARY KEY CLUSTERED
	(
		[ItemCreatorIdentifierID] ASC
	)
)
GO

ALTER TABLE [dbo].[ItemCreatorIdentifier] WITH CHECK ADD CONSTRAINT [FK_ItemCreatorIdentifier_ImportSource] FOREIGN KEY([ImportSourceID])
REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[ItemCreatorIdentifier] WITH CHECK ADD CONSTRAINT [FK_ItemCreatorIdentifier_ImportStatus] FOREIGN KEY([ImportStatusID])
REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
