SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Segment](
	[SegmentID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[RedirectSegmentID] [int] NULL,
	[SegmentGenreID] [int] NOT NULL,
	[StartPageID] [int] NULL,
	[ThumbnailPageID] [int] NULL,
	[LanguageCode] [nvarchar](10) NULL,
	[BarCode] [nvarchar](200) NULL,
	[MARCItemID] [nvarchar](200) NULL,
	[Title] [nvarchar](2000) NOT NULL,
	[SortTitle] [nvarchar](2000) NOT NULL,
	[TranslatedTitle] [nvarchar](2000) NOT NULL,
	[ContainerTitle] [nvarchar](2000) NOT NULL,
	[PublicationDetails] [nvarchar](400) NOT NULL,
	[PublisherName] [nvarchar](250) NOT NULL,
	[PublisherPlace] [nvarchar](150) NOT NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[Volume] [nvarchar](100) NOT NULL,
	[Series] [nvarchar](100) NOT NULL,
	[Issue] [nvarchar](100) NOT NULL,
	[Edition] [nvarchar](400) NOT NULL,
	[Date] [nvarchar](20) NOT NULL,
	[PageRange] [nvarchar](50) NOT NULL,
	[StartPageNumber] [nvarchar](20) NOT NULL,
	[EndPageNumber] [nvarchar](20) NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
	[DownloadUrl] [nvarchar](200) NOT NULL,
	[LicenseName] [nvarchar](200) NOT NULL,
	[LicenseUrl] [nvarchar](200) NOT NULL,
	[RightsStatus] [nvarchar](500) NOT NULL,
	[RightsStatement] [nvarchar](max) NOT NULL,
	[CopyrightStatus] [nvarchar](max) NOT NULL,
	[CopyrightRegion] [nvarchar](50) NOT NULL,
	[CopyrightComment] [nvarchar](max) NOT NULL,
	[CopyrightEvidence] [nvarchar](max) NOT NULL,
	[ScanningUser] [nvarchar](100) NULL,
	[ScanningDate] [datetime] NULL,
	[PaginationStatusID] [int] NULL,
	[PaginationStatusDate] [datetime] NULL,
	[PaginationStatusUserID] [int] NULL,
	[PaginationCompleteDate] [datetime] NULL,
	[PaginationCompleteUserID] [int] NULL,
	[LastPageNameLookupDate] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
	[PageProgression] [nvarchar](10) NOT NULL,
	[PreferredContainerTitleID] [int] NULL,
 CONSTRAINT [PK_Segment] PRIMARY KEY CLUSTERED 
(
	[SegmentID] ASC
)
)

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_Segment_BarCode] ON [dbo].[Segment]
(
	[BarCode] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_Segment_ItemID] ON [dbo].[Segment]
(
	[ItemID] ASC
)
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Title]  DEFAULT ('') FOR [Title]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_SortTitle]  DEFAULT ('') FOR [SortTitle]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_TranslatedTitle]  DEFAULT ('') FOR [TranslatedTitle]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_ParentTitle]  DEFAULT ('') FOR [ContainerTitle]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_PublicationDetails]  DEFAULT ('') FOR [PublicationDetails]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_PublisherName]  DEFAULT ('') FOR [PublisherName]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_PublisherPlace]  DEFAULT ('') FOR [PublisherPlace]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Summary]  DEFAULT ('') FOR [Summary]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Volume]  DEFAULT ('') FOR [Volume]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Series]  DEFAULT ('') FOR [Series]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Issue]  DEFAULT ('') FOR [Issue]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Edition]  DEFAULT ('') FOR [Edition]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Date]  DEFAULT ('') FOR [Date]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_PageRange]  DEFAULT ('') FOR [PageRange]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_StartPageNumber]  DEFAULT ('') FOR [StartPageNumber]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_EndPageNumber]  DEFAULT ('') FOR [EndPageNumber]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_Url]  DEFAULT ('') FOR [Url]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_DownloadUrl]  DEFAULT ('') FOR [DownloadUrl]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_LicenseName]  DEFAULT ('') FOR [LicenseName]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_LicenseUrl]  DEFAULT ('') FOR [LicenseUrl]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_RightsStatus]  DEFAULT ('') FOR [RightsStatus]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_RightsStatement]  DEFAULT ('') FOR [RightsStatement]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CopyrightStatus]  DEFAULT ('') FOR [CopyrightStatus]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CopyrightRegion]  DEFAULT ('') FOR [CopyrightRegion]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CopyrightComment]  DEFAULT ('') FOR [CopyrightComment]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CopyrightEvidence]  DEFAULT ('') FOR [CopyrightEvidence]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[Segment] ADD  CONSTRAINT [DF_Segment_PageProgression] DEFAULT ('') FOR [PageProgression]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_Item]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_Language] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[Language] ([LanguageCode])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_Language]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_Page] FOREIGN KEY([StartPageID])
REFERENCES [dbo].[Page] ([PageID])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_Page]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_PaginationStatus] FOREIGN KEY([PaginationStatusID])
REFERENCES [dbo].[PaginationStatus] ([PaginationStatusID])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_PaginationStatus]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_Segment] FOREIGN KEY([RedirectSegmentID])
REFERENCES [dbo].[Segment] ([SegmentID])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_Segment]
GO
ALTER TABLE [dbo].[Segment]  WITH CHECK ADD  CONSTRAINT [FK_Segment_SegmentGenre] FOREIGN KEY([SegmentGenreID])
REFERENCES [dbo].[SegmentGenre] ([SegmentGenreID])
GO
ALTER TABLE [dbo].[Segment] CHECK CONSTRAINT [FK_Segment_SegmentGenre]
GO
