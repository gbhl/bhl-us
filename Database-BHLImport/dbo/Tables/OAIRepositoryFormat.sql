CREATE TABLE [dbo].[OAIRepositoryFormat](
	[RepositoryFormatID] [int] IDENTITY(1,1) NOT NULL,
	[RepositoryID] [int] NOT NULL,
	[FormatID] [int] NOT NULL,
	[Schema] [nvarchar](150) NOT NULL,
	[Namespace] [nvarchar](150) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT PK_OAIRepositoryFormat PRIMARY KEY CLUSTERED 
(
	[RepositoryFormatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAIRepositoryFormat] ADD  CONSTRAINT [DF__OAIFormat__Schem__120A78D0]  DEFAULT ('') FOR [Schema]
GO

ALTER TABLE [dbo].[OAIRepositoryFormat] ADD  CONSTRAINT [DF__OAIFormat__Names__12FE9D09]  DEFAULT ('') FOR [Namespace]
GO

ALTER TABLE [dbo].[OAIRepositoryFormat] ADD  CONSTRAINT [DF__OAIFormat__Creat__13F2C142]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAIRepositoryFormat] ADD  CONSTRAINT [DF__OAIFormat__LastM__14E6E57B]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[OAIRepositoryFormat]  WITH CHECK ADD  CONSTRAINT [FK_OAIRepositoryFormat_OAIFormat] FOREIGN KEY([FormatID])
REFERENCES [dbo].[OAIFormat] ([FormatID])
GO

ALTER TABLE [dbo].[OAIRepositoryFormat] CHECK CONSTRAINT [FK_OAIRepositoryFormat_OAIFormat]
GO

ALTER TABLE [dbo].[OAIRepositoryFormat]  WITH CHECK ADD  CONSTRAINT [FK_OAIRepositoryFormat_OAIRepository] FOREIGN KEY([RepositoryID])
REFERENCES [dbo].[OAIRepository] ([RepositoryID])
GO

ALTER TABLE [dbo].[OAIRepositoryFormat] CHECK CONSTRAINT [FK_OAIRepositoryFormat_OAIRepository]
GO


