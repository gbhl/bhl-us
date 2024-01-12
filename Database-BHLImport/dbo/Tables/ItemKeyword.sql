CREATE TABLE [dbo].[ItemKeyword](
	[ItemKeywordID] [int] IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[ImportStatusID] [int] NOT NULL,
	[ImportSourceID] [int] NULL,
	[Keyword] [nvarchar](50) NOT NULL,
	[ProductionDate] [datetime] NULL,
	[CreatedDate] [datetime] CONSTRAINT [DF_ItemKeyword_CreatedDate] DEFAULT (getdate()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_ItemKeyword_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_ItemKeyword] PRIMARY KEY CLUSTERED 
	(
		[ItemKeywordID] ASC
	)
)
GO

ALTER TABLE [dbo].[ItemKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ItemKeyword_ImportSource] FOREIGN KEY([ImportSourceID])
REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO
ALTER TABLE [dbo].[ItemKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ItemKeyword_ImportStatus] FOREIGN KEY([ImportStatusID])
REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
GO
