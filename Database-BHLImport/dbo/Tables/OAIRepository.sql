CREATE TABLE [dbo].[OAIRepository](
	[RepositoryID] [int] IDENTITY(1,1) NOT NULL,
	[ImportSourceID] [int] NOT NULL,
	[BHLInstitutionCode] [nvarchar](10) NOT NULL,
	[RepositoryName] [nvarchar](100) NOT NULL,
	[BaseUrl] [nvarchar](150) NOT NULL,
	[ProtocolVersion] [nvarchar](10) NOT NULL,
	[EarliestDateStamp] [datetime] NULL,
	[DeletedRecord] [nvarchar](20) NOT NULL,
	[Granularity] [nvarchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT PK_OAIRepository PRIMARY KEY CLUSTERED 
(
	[RepositoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIRepository_InstitutionCode]  DEFAULT('UNKNOWN') FOR [BHLInstitutionCode]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__Respo__1B93E30A]  DEFAULT ('') FOR [RepositoryName]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__BaseU__1C880743]  DEFAULT ('') FOR [BaseUrl]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__Proto__1D7C2B7C]  DEFAULT ('') FOR [ProtocolVersion]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__Delet__1E704FB5]  DEFAULT ('') FOR [DeletedRecord]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__Granu__1F6473EE]  DEFAULT ('') FOR [Granularity]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__Creat__20589827]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAIRepository] ADD  CONSTRAINT [DF__OAIReposi__LastM__214CBC60]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO

ALTER TABLE [dbo].[OAIRepository]  WITH CHECK ADD  CONSTRAINT [FK_OAIRepository_ImportSource] FOREIGN KEY([ImportSourceID])
REFERENCES [dbo].[ImportSource] ([ImportSourceID])
GO

ALTER TABLE [dbo].[OAIRepository] CHECK CONSTRAINT [FK_OAIRepository_ImportSource]
GO


