CREATE TABLE [annotation].[AnnotationRelation] (
    [AnnotationID]              INT            NOT NULL,
    [RelatedExternalIdentifier] NVARCHAR (50)  CONSTRAINT [DF_AnnotationRelation_RelatedExternalIdentifier] DEFAULT ('') NOT NULL,
    [Note]                      NVARCHAR (MAX) CONSTRAINT [DF_AnnotationRelation_Note] DEFAULT ('') NOT NULL,
    [CreationDate]              DATETIME       CONSTRAINT [DF_AnnotationRelation_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]          DATETIME       CONSTRAINT [DF_AnnotationRelation_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationRelation] PRIMARY KEY CLUSTERED ([AnnotationID] ASC, [RelatedExternalIdentifier] ASC),
    CONSTRAINT [FK_AnnotationRelation_Annotation] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID])
);

