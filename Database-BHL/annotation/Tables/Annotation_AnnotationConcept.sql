CREATE TABLE [annotation].[Annotation_AnnotationConcept] (
    [AnnotationID]              INT           NOT NULL,
    [AnnotationConceptCode]     NVARCHAR (20) NOT NULL,
    [AnnotationKeywordTargetID] INT           NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [LastModifiedDate]          DATETIME      NOT NULL,
    CONSTRAINT [PK_Annotation_AnnotationConcept] PRIMARY KEY CLUSTERED ([AnnotationID] ASC, [AnnotationConceptCode] ASC, [AnnotationKeywordTargetID] ASC),
    CONSTRAINT [FK_Annotation_AnnotationConcept_Annotation] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID]),
    CONSTRAINT [FK_Annotation_AnnotationConcept_AnnotationConcept] FOREIGN KEY ([AnnotationConceptCode]) REFERENCES [annotation].[AnnotationConcept] ([AnnotationConceptCode]),
    CONSTRAINT [FK_Annotation_AnnotationConcept_AnnotationKeywordTarget] FOREIGN KEY ([AnnotationKeywordTargetID]) REFERENCES [annotation].[AnnotationKeywordTarget] ([AnnotationKeywordTargetID])
);

