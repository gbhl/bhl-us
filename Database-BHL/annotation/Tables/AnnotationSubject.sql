CREATE TABLE [annotation].[AnnotationSubject] (
    [AnnotationSubjectID]         INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationID]                INT            NOT NULL,
    [AnnotationSubjectCategoryID] INT            NULL,
    [AnnotationKeywordTargetID]   INT            NOT NULL,
    [SubjectText]                 NVARCHAR (150) CONSTRAINT [DF_AnnotationSubject_SubjectText] DEFAULT ('') NOT NULL,
    [CreationDate]                DATETIME       CONSTRAINT [DF_AnnotationSubject_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]            DATETIME       CONSTRAINT [DF_AnnotationSubject_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationSubject] PRIMARY KEY CLUSTERED ([AnnotationSubjectID] ASC),
    CONSTRAINT [FK_AnnotationSubject_Annotation] FOREIGN KEY ([AnnotationID]) REFERENCES [annotation].[Annotation] ([AnnotationID]),
    CONSTRAINT [FK_AnnotationSubject_AnnotationKeywordTarget] FOREIGN KEY ([AnnotationKeywordTargetID]) REFERENCES [annotation].[AnnotationKeywordTarget] ([AnnotationKeywordTargetID]),
    CONSTRAINT [FK_AnnotationSubject_AnnotationSubjectCategory] FOREIGN KEY ([AnnotationSubjectCategoryID]) REFERENCES [annotation].[AnnotationSubjectCategory] ([AnnotationSubjectCategoryID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AnnotationSubject]
    ON [annotation].[AnnotationSubject]([AnnotationID] ASC, [AnnotationSubjectCategoryID] ASC, [AnnotationKeywordTargetID] ASC, [SubjectText] ASC);

