CREATE TABLE [dbo].[OAIHarvestLog](
	[HarvestLogID] [int] IDENTITY(1,1) NOT NULL,
	[HarvestSetID] [int] NOT NULL,
	[HarvestStartDateTime] [datetime] NULL,
	[FromDateTime] [datetime] NULL,
	[UntilDateTime] [datetime] NULL,
	[ResponseDateTime] [datetime] NULL,
	[Result] [nvarchar](200) NOT NULL,
	[NumberHarvested] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
CONSTRAINT PK_OAIHarvestLog PRIMARY KEY CLUSTERED 
(
	[HarvestLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAIHarvestLog] ADD  DEFAULT ('') FOR [Result]
GO

ALTER TABLE [dbo].[OAIHarvestLog] ADD DEFAULT (0) FOR [NumberHarvested]
GO

ALTER TABLE [dbo].[OAIHarvestLog] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAIHarvestLog] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[OAIHarvestLog]  WITH CHECK ADD  CONSTRAINT [FK_OAIHarvestLog_OAIHarvestSet] FOREIGN KEY([HarvestSetID])
REFERENCES [dbo].[OAIHarvestSet] ([HarvestSetID])
GO

ALTER TABLE [dbo].[OAIHarvestLog] CHECK CONSTRAINT [FK_OAIHarvestLog_OAIHarvestSet]
GO

