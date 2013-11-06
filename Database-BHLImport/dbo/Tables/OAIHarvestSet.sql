CREATE TABLE [dbo].[OAIHarvestSet](
	[HarvestSetID] [int] IDENTITY(1,1) NOT NULL,
	[HarvestSetName] [nvarchar](150) NOT NULL,
	[SetID] [int] NULL,
	[RepositoryFormatID] [int] NOT NULL,
	[DefaultRecordType] [nvarchar](20) NOT NULL,
	[IsActive] [smallint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT PK_OAIHarvestSet PRIMARY KEY CLUSTERED 
(
	[HarvestSetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAIHarvestSet] ADD  CONSTRAINT [DF__OAIHarves__Harve__18B7765F]  DEFAULT ('') FOR [HarvestSetName]
GO

ALTER TABLE [dbo].[OAIHarvestSet] ADD CONSTRAINT [DF__OAIHarvestSet__DefaultRecordType] DEFAULT ('') FOR [DefaultRecordType]
GO

ALTER TABLE [dbo].[OAIHarvestSet] ADD CONSTRAINT [DF_OAIHarvestSet_IsActive] DEFAULT (1) FOR [IsActive]
GO

ALTER TABLE [dbo].[OAIHarvestSet] ADD  CONSTRAINT [DF__OAIHarves__Creat__19AB9A98]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAIHarvestSet] ADD  CONSTRAINT [DF__OAIHarves__LastM__1A9FBED1]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[OAIHarvestSet]  WITH CHECK ADD  CONSTRAINT [FK_OAIHarvestSet_OAIRepositoryFormat] FOREIGN KEY([RepositoryFormatID])
REFERENCES [dbo].[OAIRepositoryFormat] ([RepositoryFormatID])
GO

ALTER TABLE [dbo].[OAIHarvestSet] CHECK CONSTRAINT [FK_OAIHarvestSet_OAIRepositoryFormat]
GO

ALTER TABLE [dbo].[OAIHarvestSet]  WITH CHECK ADD  CONSTRAINT [FK_OAIHarvestSet_OAISet] FOREIGN KEY([SetID])
REFERENCES [dbo].[OAISet] ([SetID])
GO

ALTER TABLE [dbo].[OAIHarvestSet] CHECK CONSTRAINT [FK_OAIHarvestSet_OAISet]
GO


