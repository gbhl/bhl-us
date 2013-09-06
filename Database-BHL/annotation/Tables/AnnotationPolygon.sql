CREATE TABLE [annotation].[AnnotationPolygon] (
    [AnnotationPolygonID] INT      IDENTITY (1, 1) NOT NULL,
    [AnnotationID]        INT      NOT NULL,
    [PolygonX1]           INT      NULL,
    [PolygonY1]           INT      NULL,
    [PolygonX2]           INT      NULL,
    [PolygonY2]           INT      NULL,
    [CreationDate]        DATETIME CONSTRAINT [DF_annotation.AnnotationPolygon_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]    DATETIME CONSTRAINT [DF_annotation.AnnotationPolygon_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_annotation.AnnotationPolygon] PRIMARY KEY CLUSTERED ([AnnotationPolygonID] ASC),
    CONSTRAINT [FK_annotation.AnnotationPolygon_Annotation] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID])
);

