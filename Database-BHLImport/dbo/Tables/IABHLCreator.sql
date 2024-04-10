CREATE TABLE dbo.IABHLCreator (
	[BHLCreatorID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[Name] [nvarchar] (300) CONSTRAINT [DF_IABHLCreator_Name] DEFAULT('') NOT NULL,
	[CreatedDate] [datetime] CONSTRAINT [DF_IABHLCreator_CreatedDate] DEFAULT(GETDATE()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_IABHLCreator_LastModifiedDate] DEFAULT(GETDATE()) NOT NULL,
	CONSTRAINT [PK_IABHLCreator] PRIMARY KEY CLUSTERED 
	(
		[BHLCreatorID] ASC
	)
)
GO

ALTER TABLE [dbo].[IABHLCreator] ADD CONSTRAINT [FK_IABHLCreator_IAItem] FOREIGN KEY([ItemID])
REFERENCES [dbo].[IAItem] ([ItemID])
GO
