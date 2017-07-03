CREATE TABLE [dbo].[SearchCatalogSegment] (
    [SearchCatalogSegmentID] INT             IDENTITY (1, 1) NOT NULL,
    [SearchText]             NVARCHAR (4000) CONSTRAINT [DF_SearchCatalogSegment_SearchText] DEFAULT ('') NOT NULL,
    [SegmentID]              INT             NOT NULL,
    [Title]                  NVARCHAR (2000) CONSTRAINT [DF_SearchCatalogSegment_Title] DEFAULT ('') NOT NULL,
    [TranslatedTitle]        NVARCHAR (2000) CONSTRAINT [DF_SearchCatalogSegment_TranslatedTitle] DEFAULT ('') NOT NULL,
    [ContainerTitle]         NVARCHAR (2000) CONSTRAINT [DF_SearchCatalogSegment_ContainerTitle] DEFAULT ('') NOT NULL,
    [PublicationDetails]     NVARCHAR (400)  CONSTRAINT [DF_Table_1_PublisherName] DEFAULT ('') NOT NULL,
    [Volume]                 NVARCHAR (100)  CONSTRAINT [DF_SearchCatalogSegment_Volume] DEFAULT ('') NOT NULL,
    [Series]                 NVARCHAR (100)  CONSTRAINT [DF_SearchCatalogSegment_Series] DEFAULT ('') NOT NULL,
    [Issue]                  NVARCHAR (100)  CONSTRAINT [DF_SearchCatalogSegment_Issue] DEFAULT ('') NOT NULL,
    [Date]                   NVARCHAR (20)   CONSTRAINT [DF_SearchCatalogSegment_Date] DEFAULT ('') NOT NULL,
    [Subjects]               NVARCHAR (MAX)  CONSTRAINT [DF_SearchCatalogSegment_Subjects] DEFAULT ('') NOT NULL,
    [Authors]                NVARCHAR (MAX)  CONSTRAINT [DF_SearchCatalogSegment_Authors] DEFAULT ('') NOT NULL,
    [CreationDate]           DATETIME        CONSTRAINT [DF_SearchCatalogSegment_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]       DATETIME        CONSTRAINT [DF_SearchCatalogSegment_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [ItemID]                 INT             NULL,
    [HasLocalContent]        SMALLINT        CONSTRAINT [DF_SearchCatalogSegment_HasLocalContent] DEFAULT ((1)) NOT NULL,
    [HasExternalContent]     SMALLINT        CONSTRAINT [DF_SearchCatalogSegment_HasExternalContent] DEFAULT ((0)) NOT NULL,
	[Contributors]           NVARCHAR(MAX)   CONSTRAINT [DF_SearchCatalogSegment_Contributors] DEFAULT ('') NOT NULL,
	[SearchAuthors]          NVARCHAR(MAX)   CONSTRAINT [DF_SearchCatalogSegment_SearchAuthors] DEFAULT ('') NOT NULL,
	[HasIllustrations]       SMALLINT        CONSTRAINT [DF_SearchCatalogSegment_HasIllustrations] DEFAULT((0)) NOT NULL, 
    CONSTRAINT [PK_SearchCatalogSegment] PRIMARY KEY CLUSTERED ([SearchCatalogSegmentID] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SearchCatalogSegment_SegmentID]
ON [dbo].[SearchCatalogSegment] ([SegmentID])
INCLUDE ([Subjects],[Authors],[ItemID],[HasLocalContent],[HasExternalContent])
GO
