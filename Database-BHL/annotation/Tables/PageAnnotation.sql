CREATE TABLE [annotation].[PageAnnotation] (
    [AnnotatedPageID] INT           NOT NULL,
    [AnnotationID]    INT           NOT NULL,
    [PageColumn]      NVARCHAR (20) CONSTRAINT [DF_PageAnnotation_PageColumn] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_PageAnnotation] PRIMARY KEY CLUSTERED ([AnnotatedPageID] ASC, [AnnotationID] ASC),
    CONSTRAINT [FK_PageAnnotation_AnnotatedPage] FOREIGN KEY ([AnnotatedPageID]) REFERENCES [annotation].[AnnotatedPage] ([AnnotatedPageID]),
    CONSTRAINT [FK_PageAnnotation_Annotation] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID])
);

GO
CREATE NONCLUSTERED INDEX [IX_PageAnnotationAnnotationID] ON [annotation].[PageAnnotation]
(
	[AnnotationID] ASC
)
INCLUDE ([AnnotatedPageID])
GO
