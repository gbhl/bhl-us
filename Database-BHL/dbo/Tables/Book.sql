SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[RedirectBookID] [int] NULL,
	[ThumbnailPageID] [int] NULL,
	[LanguageCode] [nvarchar](10) NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[MARCItemID] [nvarchar](200) NULL,
	[CallNumber] [nvarchar](100) NULL,
	[Volume] [nvarchar](100) NULL,
	[StartYear] [nvarchar](20) NULL,
	[EndYear] [nvarchar](20) NOT NULL,
	[StartVolume] [nvarchar](10) NOT NULL,
	[EndVolume] [nvarchar](10) NOT NULL,
	[StartIssue] [nvarchar](10) NOT NULL,
	[EndIssue] [nvarchar](10) NOT NULL,
	[StartNumber] [nvarchar](10) NOT NULL,
	[EndNumber] [nvarchar](10) NOT NULL,
	[StartSeries] [nvarchar](10) NOT NULL,
	[EndSeries] [nvarchar](10) NOT NULL,
	[StartPart] [nvarchar](10) NOT NULL,
	[EndPart] [nvarchar](10) NOT NULL,
	[IdentifierBib] [nvarchar](50) NULL,
	[ZQuery] [nvarchar](200) NULL,
	[Sponsor] [nvarchar](100) NULL,
	[ExternalUrl] [nvarchar](500) NULL,
	[LicenseUrl] [nvarchar](max) NULL,
	[Rights] [nvarchar](max) NULL,
	[DueDiligence] [nvarchar](max) NULL,
	[CopyrightStatus] [nvarchar](max) NULL,
	[CopyrightRegion] [nvarchar](50) NULL,
	[CopyrightComment] [nvarchar](max) NULL,
	[CopyrightEvidence] [nvarchar](max) NULL,
	[ScanningUser] [nvarchar](100) NULL,
	[ScanningDate] [datetime] NULL,
	[PaginationStatusID] [int] NULL,
	[PaginationStatusDate] [datetime] NULL,
	[PaginationStatusUserID] [int] NULL,
	[PaginationCompleteDate] [datetime] NULL,
	[PaginationCompleteUserID] [int] NULL,
	[LastPageNameLookupDate] [datetime] NULL,
	[IsVirtual] [tinyint] NOT NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
	[PageProgression] [nvarchar](10) NOT NULL,
	[VirtualVolumeKey] [nvarchar](100) NOT NULL,
	[CopyrightIndicator] [nvarchar](100) NOT NULL,
	CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
	(
		[BookID] ASC
	)
)

GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Book_BarCode] ON [dbo].[Book]
(
	[BarCode] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_Book_ItemID] ON [dbo].[Book]
(
	[ItemID] ASC
)
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndYear]  DEFAULT ('') FOR [EndYear]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_StartVolume]  DEFAULT ('') FOR [StartVolume]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndVolume]  DEFAULT ('') FOR [EndVolume]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_StartIssue]  DEFAULT ('') FOR [StartIssue]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndIssue]  DEFAULT ('') FOR [EndIssue]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_StartNumber]  DEFAULT ('') FOR [StartNumber]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndNumber]  DEFAULT ('') FOR [EndNumber]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_StartSeries]  DEFAULT ('') FOR [StartSeries]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndSeries]  DEFAULT ('') FOR [EndSeries]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_StartPart]  DEFAULT ('') FOR [StartPart]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_EndPart]  DEFAULT ('') FOR [EndPart]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_ExternalUrl]  DEFAULT ('') FOR [ExternalUrl]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_LicenseUrl]  DEFAULT ('') FOR [LicenseUrl]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Rights]  DEFAULT ('') FOR [Rights]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_DueDiligence]  DEFAULT ('') FOR [DueDiligence]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CopyrightStatus]  DEFAULT ('') FOR [CopyrightStatus]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CopyrightRegion]  DEFAULT ('') FOR [CopyrightRegion]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CopyrightComment]  DEFAULT ('') FOR [CopyrightComment]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CopyrightEvidence]  DEFAULT ('') FOR [CopyrightEvidence]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_IsVirtual]  DEFAULT ((0)) FOR [IsVirtual]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [DF_Book_PageProgression] DEFAULT ('') FOR [PageProgression]
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [DF_Book_VirtualVolumeKey] DEFAULT ('') FOR [VirtualVolumeKey]
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [DF_Book_CopyrightIndicator] DEFAULT ('') FOR [CopyrightIndicator]
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [CK_Book_CopyrightIndicator] CHECK ([CopyrightIndicator] IN ('', 'Public Domain', 'No Known Copyright', 'In-copyright', 'Copyright not provided'))
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Book] FOREIGN KEY([RedirectBookID])
REFERENCES [dbo].[Book] ([BookID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Book]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Item]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Language] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[Language] ([LanguageCode])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Language]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_PaginationStatus] FOREIGN KEY([PaginationStatusID])
REFERENCES [dbo].[PaginationStatus] ([PaginationStatusID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_PaginationStatus]
GO
