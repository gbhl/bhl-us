CREATE TABLE [dbo].[SegmentGenre] (
    [SegmentGenreID]     INT           IDENTITY (1, 1) NOT NULL,
    [GenreName]          NVARCHAR (50) CONSTRAINT [DF_SegmentGenre_GenreName] DEFAULT ('') NOT NULL,
    [GenreDescription]   NVARCHAR(500) CONSTRAINT [DF_SegmentGenre_GenreDescription] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_SegmentGenre_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_SegmentGenre_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_SegmentGenre_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_SegmentGenre_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SegmentGenre] PRIMARY KEY CLUSTERED ([SegmentGenreID] ASC)
);

