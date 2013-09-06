CREATE TABLE [annotation].[AnnotationKeywordTarget] (
    [AnnotationKeywordTargetID] INT           IDENTITY (1, 1) NOT NULL,
    [KeywordTargetName]         NVARCHAR (20) CONSTRAINT [DF_AnnotationConceptTarget_ConceptTargetName] DEFAULT ('') NOT NULL,
    [CreationDate]              DATETIME      CONSTRAINT [DF_AnnotationConceptTarget_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]          DATETIME      CONSTRAINT [DF_AnnotationConceptTarget_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationConceptTarget] PRIMARY KEY CLUSTERED ([AnnotationKeywordTargetID] ASC)
);

