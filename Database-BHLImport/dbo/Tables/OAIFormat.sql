CREATE TABLE [dbo].[OAIFormat](
	[FormatID] [int] IDENTITY(1,1) NOT NULL,
	[Prefix] [nvarchar](20) NOT NULL,
	[AssemblyName] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
CONSTRAINT PK_OAIFormat PRIMARY KEY CLUSTERED 
(
	[FormatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OAIFormat] ADD  DEFAULT ('') FOR [Prefix]
GO

ALTER TABLE [dbo].[OAIFormat] ADD  DEFAULT ('') FOR [AssemblyName]
GO

ALTER TABLE [dbo].[OAIFormat] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[OAIFormat] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO


