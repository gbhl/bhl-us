CREATE TABLE [annotation].[AnnotationMarkType] (
    [AnnotationMarkTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnnotationMarkTypeName] NVARCHAR (30) CONSTRAINT [DF_AnnotationMarkType_AnnotationMarkTypeName] DEFAULT ('') NOT NULL,
    [CreationDate]           DATETIME      CONSTRAINT [DF_AnnotationMarkType_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]       DATETIME      CONSTRAINT [DF_AnnotationMarkType_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotationMarkType] PRIMARY KEY CLUSTERED ([AnnotationMarkTypeID] ASC)
);

