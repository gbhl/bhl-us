CREATE TABLE [dbo].[Segment] (
    [SegmentID]                   INT             IDENTITY (1, 1) NOT NULL,
    [ItemID]                      INT             NULL,
    [SegmentStatusID]             INT             NOT NULL,
    [SequenceOrder]               SMALLINT        CONSTRAINT [DF_Segment_SequenceOrder] DEFAULT ((1)) NOT NULL,
    [SegmentGenreID]              INT             NOT NULL,
    [Title]                       NVARCHAR (2000) CONSTRAINT [DF_Segment_Title] DEFAULT ('') NOT NULL,
    [TranslatedTitle]             NVARCHAR (2000) CONSTRAINT [DF_Segment_TranslatedTitle] DEFAULT ('') NOT NULL,
    [ContainerTitle]              NVARCHAR (2000) CONSTRAINT [DF_Segment_ParentTitle] DEFAULT ('') NOT NULL,
    [PublicationDetails]          NVARCHAR (400)  CONSTRAINT [DF_Segment_PublicationDetails] DEFAULT ('') NOT NULL,
    [PublisherName]               NVARCHAR (250)  CONSTRAINT [DF_Segment_PublisherName] DEFAULT ('') NOT NULL,
    [PublisherPlace]              NVARCHAR (150)  CONSTRAINT [DF_Segment_PublisherPlace] DEFAULT ('') NOT NULL,
    [Notes]                       NVARCHAR (MAX)  CONSTRAINT [DF_Segment_Description] DEFAULT ('') NOT NULL,
	[Summary]				      NVARCHAR (MAX)  CONSTRAINT [DF_Segment_Summary] DEFAULT ('') NOT NULL,
    [Volume]                      NVARCHAR (100)  CONSTRAINT [DF_Segment_Volume] DEFAULT ('') NOT NULL,
    [Series]                      NVARCHAR (100)  CONSTRAINT [DF_Segment_Series] DEFAULT ('') NOT NULL,
    [Issue]                       NVARCHAR (100)  CONSTRAINT [DF_Segment_Issue] DEFAULT ('') NOT NULL,
	[Edition]					  NVARCHAR (400)  CONSTRAINT [DF_Segment_Edition] DEFAULT ('') NOT NULL,
    [Date]                        NVARCHAR (20)   CONSTRAINT [DF_Segment_Date] DEFAULT ('') NOT NULL,
    [PageRange]                   NVARCHAR (50)   CONSTRAINT [DF_Segment_PageRange] DEFAULT ('') NOT NULL,
    [StartPageNumber]             NVARCHAR (20)   CONSTRAINT [DF_Segment_StartPageNumber] DEFAULT ('') NOT NULL,
    [EndPageNumber]               NVARCHAR (20)   CONSTRAINT [DF_Segment_EndPageNumber] DEFAULT ('') NOT NULL,
    [StartPageID]                 INT             NULL,
    [LanguageCode]                NVARCHAR (10)   NULL,
    [Url]                         NVARCHAR (200)  CONSTRAINT [DF_Segment_Url] DEFAULT ('') NOT NULL,
    [DownloadUrl]                 NVARCHAR (200)  CONSTRAINT [DF_Segment_DownloadUrl] DEFAULT ('') NOT NULL,
    [RightsStatus]                NVARCHAR (500)  CONSTRAINT [DF_Segment_RightsStatus] DEFAULT ('') NOT NULL,
    [RightsStatement]             NVARCHAR (500)  CONSTRAINT [DF_Segment_RightsStatement] DEFAULT ('') NOT NULL,
    [LicenseName]                 NVARCHAR (200)  CONSTRAINT [DF_Segment_LicenseName] DEFAULT ('') NOT NULL,
    [LicenseUrl]                  NVARCHAR (200)  CONSTRAINT [DF_Segment_LicenseUrl] DEFAULT ('') NOT NULL,
    [ContributorCreationDate]     DATETIME        NULL,
    [ContributorLastModifiedDate] DATETIME        NULL,
    [CreationDate]                DATETIME        CONSTRAINT [DF_Segment_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]            DATETIME        CONSTRAINT [DF_Segment_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]              INT             CONSTRAINT [DF_Segment_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]          INT             CONSTRAINT [DF_Segment_LastModifiedUserID] DEFAULT ((1)) NULL,
    [SortTitle]                   NVARCHAR (2000) CONSTRAINT [DF_Segment_SortTitle] DEFAULT ('') NOT NULL,
    [RedirectSegmentID] INT NULL, 
    CONSTRAINT [PK_Segment] PRIMARY KEY CLUSTERED ([SegmentID] ASC),
    CONSTRAINT [FK_Segment_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]),
    CONSTRAINT [FK_Segment_Page] FOREIGN KEY ([StartPageID]) REFERENCES [dbo].[Page] ([PageID]),
    CONSTRAINT [FK_Segment_SegmentGenre] FOREIGN KEY ([SegmentGenreID]) REFERENCES [dbo].[SegmentGenre] ([SegmentGenreID]),
    CONSTRAINT [FK_Segment_SegmentStatus] FOREIGN KEY ([SegmentStatusID]) REFERENCES [dbo].[SegmentStatus] ([SegmentStatusID]),
	CONSTRAINT [FK_Segment_Segment] FOREIGN KEY ([RedirectSegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Segment_ItemID]
    ON [dbo].[Segment]([ItemID] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Segment_SegmentStatusID]
ON [dbo].[Segment] ([SegmentStatusID])
INCLUDE ([ItemID], [SegmentID]);
GO
