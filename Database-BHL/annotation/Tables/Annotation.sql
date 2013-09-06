CREATE TABLE [annotation].[Annotation] (
    [AnnotationID]              INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationSourceID]        INT            NOT NULL,
    [ExternalIdentifier]        NVARCHAR (50)  CONSTRAINT [DF_Annotation_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [SequenceNumber]            INT            CONSTRAINT [DF_Annotation_SequenceNumber] DEFAULT ((1)) NOT NULL,
    [AnnotationTextDescription] NVARCHAR (MAX) CONSTRAINT [DF_Annotation_AnnotationTextDescription] DEFAULT ('') NOT NULL,
    [AnnotationText]            NVARCHAR (MAX) CONSTRAINT [DF_Annotation_AnnotationText] DEFAULT ('') NOT NULL,
    [AnnotationTextClean]       NVARCHAR (MAX) CONSTRAINT [DF_Annotation_AnnotationTextClean] DEFAULT ('') NOT NULL,
    [AnnotationTextDisplay]     NVARCHAR (MAX) CONSTRAINT [DF_Annotation_AnnotationTextDisplay] DEFAULT ('') NOT NULL,
    [AnnotationTextCorrected]   NVARCHAR (MAX) CONSTRAINT [DF_Annotation_AnnotationTextCorrected] DEFAULT ('') NOT NULL,
    [Comment]                   NVARCHAR (MAX) CONSTRAINT [DF_Annotation_Comment] DEFAULT ('') NOT NULL,
    [CreationDate]              DATETIME       CONSTRAINT [DF_Annotation_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]          DATETIME       CONSTRAINT [DF_Annotation_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Annotation] PRIMARY KEY CLUSTERED ([AnnotationID] ASC),
    CONSTRAINT [FK_Annotation_AnnotationSource] FOREIGN KEY ([AnnotationSourceID]) REFERENCES [annotation].[AnnotationSource] ([AnnotationSourceID])
);

