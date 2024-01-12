CREATE TABLE [dbo].[ItemIdentifier](
	[ItemIdentifierID] [int] IDENTITY(1,1) NOT NULL,
	[ImportStatusID] [int] NOT NULL,
	[ImportSourceID] [int] NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[IdentifierName] [nvarchar](40) NOT NULL,
	[IdentifierValue] [nvarchar](125) CONSTRAINT [DF_Item_Identifier_IdentifierValue] DEFAULT ('') NOT NULL,
	[ProductionDate] [datetime] NULL,
	[CreatedDate] [datetime] CONSTRAINT [DF_Item_Identifier_CreatedDate] DEFAULT (getdate()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_Item_Identifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_Item_Identifier] PRIMARY KEY CLUSTERED 
	(
		[ItemIdentifierID] ASC
	)
)
GO

ALTER TABLE [dbo].[ItemIdentifier]  WITH CHECK ADD  CONSTRAINT [FK_ItemIdentifier_ImportSource] FOREIGN KEY([ImportSourceID])
REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[ItemIdentifier]  WITH CHECK ADD  CONSTRAINT [FK_ItemIdentifier_ImportStatus] FOREIGN KEY([ImportStatusID])
REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
