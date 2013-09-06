CREATE TABLE [annotation].[AnnotationSubjectCategory] (
    [AnnotationSubjectCategoryID] INT           IDENTITY (1, 1) NOT NULL,
    [AnnotationSourceID]          INT           NOT NULL,
    [SubjectCategoryCode]         NVARCHAR (20) CONSTRAINT [DF_AnnotationSubjectCategory_SubjectCategoryCode] DEFAULT ('') NOT NULL,
    [SubjectCategoryName]         NVARCHAR (50) CONSTRAINT [DF_AnnotationSubjectCategory_SubjectCategoryName] DEFAULT ('') NOT NULL,
    [CreationDate]                DATETIME      CONSTRAINT [DF_AnnotationSubjectCategory_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]            DATETIME      CONSTRAINT [DF_AnnotationSubjectCategory_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationSubjectCategory] PRIMARY KEY CLUSTERED ([AnnotationSubjectCategoryID] ASC),
    CONSTRAINT [FK_AnnotationSubjectCategory_AnnotationSource] FOREIGN KEY ([AnnotationSourceID]) REFERENCES [annotation].[AnnotationSource] ([AnnotationSourceID])
);

