CREATE TABLE [dbo].[NoteType](
	[NoteTypeID] [int] IDENTITY(1,1) NOT NULL,
	[NoteTypeName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NoteTypeDisplay] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MarcDataFieldTag] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MarcIndicator1] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
 CONSTRAINT [PK_NoteType] PRIMARY KEY CLUSTERED 
(
	[NoteTypeID] ASC
)
)

GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_NoteTypeName]  DEFAULT ('') FOR [NoteTypeName]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_NoteTypeDisplay]  DEFAULT ('') FOR [NoteTypeDisplay]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_MarcDataFieldTag]  DEFAULT ('') FOR [MarcDataFieldTag]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_MarcIndicator1]  DEFAULT ('') FOR [MarcIndicator1]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[NoteType] ADD  CONSTRAINT [DF_NoteType_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
