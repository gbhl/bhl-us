CREATE TABLE [annotation].[AnnotatedPageType] (
    [AnnotatedPageTypeID]          INT            IDENTITY (1, 1) NOT NULL,
    [AnnotatedPageTypeName]        NVARCHAR (30)  CONSTRAINT [DF_AnnotationPageType_AnnotationPageTypeName] DEFAULT ('') NOT NULL,
    [AnnotatedPageTypeDescription] NVARCHAR (500) CONSTRAINT [DF_AnnotationPageType_AnnotationPageTypeDescription] DEFAULT ('') NOT NULL,
    [CreationDate]                 DATETIME       CONSTRAINT [DF_AnnotationPageType_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]             DATETIME       CONSTRAINT [DF_AnnotationPageType_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationPageType] PRIMARY KEY CLUSTERED ([AnnotatedPageTypeID] ASC)
);

