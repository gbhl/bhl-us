CREATE TABLE [annotation].[AnnotatedTitle] (
    [AnnotatedTitleID]   INT            IDENTITY (1, 1) NOT NULL,
    [AnnotationSourceID] INT            NOT NULL,
    [TitleID]            INT            NULL,
    [ExternalIdentifier] NVARCHAR (50)  CONSTRAINT [DF_AnnotatedItem_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [Author]             NVARCHAR (100) CONSTRAINT [DF_AnnotatedItem_Author] DEFAULT ('') NOT NULL,
    [Title]              NVARCHAR (200) CONSTRAINT [DF_AnnotatedItem_Title] DEFAULT ('') NOT NULL,
    [Edition]            NVARCHAR (50)  CONSTRAINT [DF_AnnotatedItem_Edition] DEFAULT ('') NOT NULL,
    [Volume]             NVARCHAR (50)  CONSTRAINT [DF_AnnotatedItem_Volume] DEFAULT ('') NOT NULL,
    [PublicationDetails] NVARCHAR (100) CONSTRAINT [DF_AnnotatedItem_PublicationDetails] DEFAULT ('') NOT NULL,
    [Date]               NVARCHAR (20)  CONSTRAINT [DF_AnnotatedItem_Date] DEFAULT ('') NOT NULL,
    [Location]           NVARCHAR (50)  CONSTRAINT [DF_AnnotatedItem_Location] DEFAULT ('') NOT NULL,
    [IsBeagleEra]        NVARCHAR (200) CONSTRAINT [DF_AnnotatedItem_IsBeagleEra] DEFAULT ('') NOT NULL,
    [Inscription]        NVARCHAR (200) CONSTRAINT [DF_AnnotatedItem_HasDarwinSignature] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_AnnotatedItem_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_AnnotatedItem_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotatedItem] PRIMARY KEY CLUSTERED ([AnnotatedTitleID] ASC),
    CONSTRAINT [FK_AnnotatedItem_AnnotationSource] FOREIGN KEY ([AnnotationSourceID]) REFERENCES [annotation].[AnnotationSource] ([AnnotationSourceID])
);

