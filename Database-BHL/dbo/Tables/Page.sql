SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[FileNamePrefix] [nvarchar](200) NOT NULL,
	[SequenceOrder] [int] NULL,
	[PageDescription] [nvarchar](255) NULL,
	[Illustration] [bit] NOT NULL,
	[Note] [nvarchar](255) NULL,
	[FileSize_Temp] [int] NULL,
	[FileExtension] [nvarchar](5) NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[CreationUserID] [int] NULL,
	[LastModifiedUserID] [int] NULL,
	[Active] [bit] NOT NULL,
	[Year] [nvarchar](20) NULL,
	[Series] [nvarchar](20) NULL,
	[Volume] [nvarchar](20) NULL,
	[Issue] [nvarchar](20) NULL,
	[ExternalURL] [nvarchar](500) NULL,
	[IssuePrefix] [nvarchar](20) NULL,
	[LastPageNameLookupDate] [datetime] NULL,
	[PaginationUserID] [int] NULL,
	[PaginationDate] [datetime] NULL,
	[AltExternalURL] [nvarchar](500) NULL,
 CONSTRAINT [aaaaaPage_PK] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX [ItemPage] ON [dbo].[Page]
(
	[ItemID] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_Page_Active] ON [dbo].[Page]
(
	[Active] ASC
)
GO
CREATE NONCLUSTERED INDEX [IX_Page_ActiveItemID] ON [dbo].[Page]
(
	[Active] ASC,
	[ItemID] ASC
)
INCLUDE ( 	[PageID],
	[CreationDate])
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_Page_ItemIDActiveSeqOrder] ON [dbo].[Page]
(
	[ItemID] ASC,
	[Active] ASC,
	[SequenceOrder] ASC
)
INCLUDE ( 	[PageID],
	[FileNamePrefix],
	[Illustration],
	[AltExternalURL],
	[Year],
	[Series],
	[Volume],
	[Issue],
	[ExternalURL],
	[IssuePrefix])
GO
CREATE NONCLUSTERED INDEX [IX_Page_ItemIDSequence] ON [dbo].[Page]
(
	[ItemID] ASC,
	[SequenceOrder] ASC
)
INCLUDE ( 	[PageID])
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_Page_LastPageNameLookupDate] ON [dbo].[Page]
(
	[LastPageNameLookupDate] ASC
)
INCLUDE ( 	[PageID],
	[ItemID],
	[FileNamePrefix],
	[SequenceOrder])
GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveItem] ON [dbo].[Page]
(
	[PageID] ASC,
	[Active] ASC,
	[ItemID] ASC
)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveSequence] ON [dbo].[Page]
(
	[PageID] ASC,
	[Active] ASC,
	[SequenceOrder] ASC
)
INCLUDE ( 	[ItemID],
	[Year])
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [Sequence] ON [dbo].[Page]
(
	[FileNamePrefix] ASC
)
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_Illustration]  DEFAULT ((0)) FOR [Illustration]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF__Page__Created__2610A626]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF__Page__Changed__2704CA5F]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Item_Active]  DEFAULT ((1)) FOR [Active]
GO
