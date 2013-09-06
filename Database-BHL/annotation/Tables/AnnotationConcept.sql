CREATE TABLE [annotation].[AnnotationConcept] (
    [AnnotationConceptCode] NVARCHAR (20)  NOT NULL,
    [AnnotationSourceID]    INT            NOT NULL,
    [ConceptText]           NVARCHAR (100) CONSTRAINT [DF_AnnotationConcept_AnnotationConceptText] DEFAULT ('') NOT NULL,
    [ParentConceptCode]     NVARCHAR (20)  NULL,
    [CreationDate]          DATETIME       CONSTRAINT [DF_AnnotationConcept_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]      DATETIME       CONSTRAINT [DF_AnnotationConcept_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationConcept] PRIMARY KEY CLUSTERED ([AnnotationConceptCode] ASC),
    CONSTRAINT [FK_AnnotationConcept_AnnotationConcept] FOREIGN KEY ([ParentConceptCode]) REFERENCES [annotation].[AnnotationConcept] ([AnnotationConceptCode]),
    CONSTRAINT [FK_AnnotationConcept_AnnotationSource] FOREIGN KEY ([AnnotationSourceID]) REFERENCES [annotation].[AnnotationSource] ([AnnotationSourceID])
);

