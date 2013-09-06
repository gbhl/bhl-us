CREATE TABLE [annotation].[AnnotationMark] (
    [AnnotationMarkID]     INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationID]         INT            NOT NULL,
    [ExternalIdentifier]   NVARCHAR (50)  CONSTRAINT [DF_AnnotationMark_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [SequenceNumber]       INT            CONSTRAINT [DF_AnnotationMark_SequenceNumber] DEFAULT ((1)) NOT NULL,
    [Position]             NVARCHAR (50)  CONSTRAINT [DF_AnnotationMark_Position] DEFAULT ('') NOT NULL,
    [AnnotationMarkTypeID] INT            NULL,
    [Content]              NVARCHAR (MAX) CONSTRAINT [DF_AnnotationMark_Content] DEFAULT ('') NOT NULL,
    [CorrectedContent]     NVARCHAR (MAX) CONSTRAINT [DF_AnnotationMark_CorrectedContent] DEFAULT ('') NOT NULL,
    [Comment]              NVARCHAR (MAX) CONSTRAINT [DF_AnnotationMark_Comment] DEFAULT ('') NOT NULL,
    [PolygonX1]            INT            NULL,
    [PolygonY1]            INT            NULL,
    [PolygonX2]            INT            NULL,
    [PolygonY2]            INT            NULL,
    [CreationDate]         DATETIME       CONSTRAINT [DF_AnnotationMark_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]     DATETIME       CONSTRAINT [DF_AnnotationMark_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationMark] PRIMARY KEY CLUSTERED ([AnnotationMarkID] ASC),
    CONSTRAINT [FK_AnnotationMark_AnnotationMarkType] FOREIGN KEY ([AnnotationMarkTypeID]) REFERENCES [annotation].[AnnotationMarkType] ([AnnotationMarkTypeID])
);

