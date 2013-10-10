CREATE TABLE [dbo].[OAISet](
	[SetID] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryID] [int] NOT NULL,
	[SetName] [nvarchar](100) NOT NULL,
	[SetSpec] [nvarchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
CONSTRAINT PK_OAISet PRIMARY KEY CLUSTERED 
(
	[SetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAISet] ADD  DEFAULT ('') FOR [SetName]
GO

ALTER TABLE [dbo].[OAISet] ADD  DEFAULT ('') FOR [SetSpec]
GO

ALTER TABLE [dbo].[OAISet] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAISet] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[OAISet]  WITH CHECK ADD  CONSTRAINT [FK_OAISet_OAIRepository] FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[OAIRepository] ([RepositoryID])
GO

ALTER TABLE [dbo].[OAISet] CHECK CONSTRAINT [FK_OAISet_OAIRepository]
GO


