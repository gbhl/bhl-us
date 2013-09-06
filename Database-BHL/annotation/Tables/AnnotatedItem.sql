CREATE TABLE [annotation].[AnnotatedItem] (
    [AnnotatedItemID]    INT           IDENTITY (1, 1) NOT NULL,
    [AnnotatedTitleID]   INT           NOT NULL,
    [ItemID]             INT           NULL,
    [ExternalIdentifier] NVARCHAR (50) CONSTRAINT [DF_annotation.AnnotatedItemID_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [Volume]             NVARCHAR (10) CONSTRAINT [DF_annotation.AnnotatedItemID_Volume] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_annotation.AnnotatedItemID_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_annotation.AnnotatedItemID_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotatedItem_1] PRIMARY KEY CLUSTERED ([AnnotatedItemID] ASC),
    CONSTRAINT [FK_AnnotatedItem_AnnotatedTitle] FOREIGN KEY ([AnnotatedTitleID]) REFERENCES [annotation].[AnnotatedTitle] ([AnnotatedTitleID])
);

