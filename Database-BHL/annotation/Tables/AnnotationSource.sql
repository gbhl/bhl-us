CREATE TABLE [annotation].[AnnotationSource] (
    [AnnotationSourceID]          INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationSourceName]        NVARCHAR (30)  CONSTRAINT [DF_AnnotationSource_AnnotationSourceName] DEFAULT ('') NOT NULL,
    [AnnotationSourceDescription] NVARCHAR (MAX) CONSTRAINT [DF_AnnotationSource_AnnotationSourceDescription] DEFAULT ('') NOT NULL,
    [CreationDate]                DATETIME       CONSTRAINT [DF_AnnotationSource_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]            DATETIME       CONSTRAINT [DF_AnnotationSource_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationSource] PRIMARY KEY CLUSTERED ([AnnotationSourceID] ASC)
);

