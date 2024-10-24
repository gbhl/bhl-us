CREATE TABLE [dbo].[TitleNote](
	[TitleNoteID] [int] IDENTITY(1,1) NOT NULL,
	[TitleID] [int] NOT NULL,
	[NoteTypeID] [int] NULL,
	[NoteText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NoteSequence] [smallint] NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
 CONSTRAINT [PK_TitleNote] PRIMARY KEY CLUSTERED 
(
	[TitleNoteID] ASC
)
)
GO

CREATE NONCLUSTERED INDEX [IX_TitleNoteNoteTypeID] ON [dbo].[TitleNote]
(
	[NoteTypeID] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_TitleNoteTitleID] ON [dbo].[TitleNote]
(
	[TitleID] ASC
)
GO
ALTER TABLE [dbo].[TitleNote] ADD  CONSTRAINT [DF_TitleNote_NoteText]  DEFAULT ('') FOR [NoteText]
GO
ALTER TABLE [dbo].[TitleNote] ADD  CONSTRAINT [DF_TitleNote_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[TitleNote] ADD  CONSTRAINT [DF_TitleNote_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[TitleNote]  WITH CHECK ADD  CONSTRAINT [FK_TitleNote_NoteTypeID] FOREIGN KEY([NoteTypeID])
REFERENCES [dbo].[NoteType] ([NoteTypeID])
GO
ALTER TABLE [dbo].[TitleNote] CHECK CONSTRAINT [FK_TitleNote_NoteTypeID]
GO
ALTER TABLE [dbo].[TitleNote]  WITH CHECK ADD  CONSTRAINT [FK_TitleNote_TitleID] FOREIGN KEY([TitleID])
REFERENCES [dbo].[Title] ([TitleID])
GO
ALTER TABLE [dbo].[TitleNote] CHECK CONSTRAINT [FK_TitleNote_TitleID]
GO
