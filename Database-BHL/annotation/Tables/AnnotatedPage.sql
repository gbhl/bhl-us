CREATE TABLE [annotation].[AnnotatedPage] (
    [AnnotatedPageID]     INT           IDENTITY (1, 1) NOT NULL,
    [AnnotatedItemID]     INT           NOT NULL,
    [PageID]              INT           NULL,
    [ExternalIdentifier]  NVARCHAR (50) CONSTRAINT [DF_AnnotatedPage_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [AnnotatedPageTypeID] INT           NOT NULL,
    [PageNumber]          NVARCHAR (20) CONSTRAINT [DF_AnnotatedPage_PageNumber] DEFAULT ('') NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_AnnotatedPage_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]    DATETIME      CONSTRAINT [DF_AnnotatedPage_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotatedPage] PRIMARY KEY CLUSTERED ([AnnotatedPageID] ASC),
    CONSTRAINT [FK_AnnotatedPage_AnnotatedItem] FOREIGN KEY ([AnnotatedItemID]) REFERENCES [annotation].[AnnotatedItem] ([AnnotatedItemID]),
    CONSTRAINT [FK_AnnotatedPage_AnnotatedPageType] FOREIGN KEY ([AnnotatedPageTypeID]) REFERENCES [annotation].[AnnotatedPageType] ([AnnotatedPageTypeID])
);

