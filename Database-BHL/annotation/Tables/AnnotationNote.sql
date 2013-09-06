CREATE TABLE [annotation].[AnnotationNote] (
    [AnnotationNoteID] INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationID]     INT            NOT NULL,
    [NoteText]         NVARCHAR (MAX) CONSTRAINT [DF_AnnotationNote_NoteText] DEFAULT ('') NOT NULL,
    [NoteTextClean]    NVARCHAR (MAX) CONSTRAINT [DF_AnnotationNote_NoteTextClean] DEFAULT ('') NOT NULL,
    [NoteTextDisplay]  NVARCHAR (MAX) CONSTRAINT [DF_AnnotationNote_NoteTextDisplay] DEFAULT ('') NOT NULL,
    [IsAlternate]      TINYINT        CONSTRAINT [DF_AnnotationNote_AltNoteText] DEFAULT ((0)) NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_AnnotationNote_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_AnnotationNote_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationNote] PRIMARY KEY CLUSTERED ([AnnotationNoteID] ASC),
    CONSTRAINT [FK_AnnotationNote_AnnotationNote] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID])
);

