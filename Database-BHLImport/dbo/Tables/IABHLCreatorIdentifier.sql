CREATE TABLE dbo.IABHLCreatorIdentifier (
	[BHLCreatorIdentifierID] [int] IDENTITY(1,1) NOT NULL,
	[BHLCreatorID] [int] NOT NULL,
	[Type] [nvarchar] (40) CONSTRAINT [DF_IABHLCreatorIdentifier_Type] DEFAULT('') NOT NULL,
	[Value] [nvarchar] (125) CONSTRAINT [DF_IABHLCreatorIdentifier_Value] DEFAULT('') NOT NULL,
	[CreatedDate] [datetime] CONSTRAINT [DF_IABHLCreatorIdentifier_CreatedDate] DEFAULT(GETDATE()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_IABHLCreatorIdentifier_LastModifiedDate] DEFAULT(GETDATE()) NOT NULL,
	CONSTRAINT [PK_IABHLCreatorIdentifier] PRIMARY KEY CLUSTERED 
	(
		[BHLCreatorIdentifierID] ASC
	)
)
GO

ALTER TABLE [dbo].[IABHLCreatorIdentifier] ADD CONSTRAINT [FK_IABHLCreatorIdentifier_IABHLCreator] FOREIGN KEY([BHLCreatorID])
REFERENCES [dbo].[IABHLCreator] ([BHLCreatorID])
GO

