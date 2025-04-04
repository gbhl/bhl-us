SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SegmentClusterSegment](
	[SegmentID] [int] NOT NULL,
	[SegmentClusterID] [int] NOT NULL,
	[IsPrimary] [smallint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NOT NULL,
	[LastModifiedUserID] [int] NOT NULL,
 CONSTRAINT [PK_SegmentClusterSegment] PRIMARY KEY CLUSTERED 
(
	[SegmentID] ASC,
	[SegmentClusterID] ASC
)
)

GO
CREATE NONCLUSTERED INDEX IX_SegmentClusterSegment_SegmentClusterID	ON [dbo].[SegmentClusterSegment] 
(
	[SegmentClusterID]
)
INCLUDE 
(
	[SegmentID],[IsPrimary]
)
GO	
ALTER TABLE [dbo].[SegmentClusterSegment] ADD  CONSTRAINT [DF_SegmentClusterSegment_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[SegmentClusterSegment] ADD  CONSTRAINT [DF_SegmentClusterSegment_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[SegmentClusterSegment] ADD  CONSTRAINT [DF_SegmentClusterSegment_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[SegmentClusterSegment] ADD  CONSTRAINT [DF_SegmentClusterSegment_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[SegmentClusterSegment] ADD  CONSTRAINT [DF_SegmentClusterSegment_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[SegmentClusterSegment]  WITH CHECK ADD  CONSTRAINT [FK_SegmentClusterSegment_Segment] FOREIGN KEY([SegmentID])
REFERENCES [dbo].[Segment] ([SegmentID])
GO
ALTER TABLE [dbo].[SegmentClusterSegment] CHECK CONSTRAINT [FK_SegmentClusterSegment_Segment]
GO
ALTER TABLE [dbo].[SegmentClusterSegment]  WITH CHECK ADD  CONSTRAINT [FK_SegmentClusterSegment_SegmentCluster] FOREIGN KEY([SegmentClusterID])
REFERENCES [dbo].[SegmentCluster] ([SegmentClusterID])
GO
ALTER TABLE [dbo].[SegmentClusterSegment] CHECK CONSTRAINT [FK_SegmentClusterSegment_SegmentCluster]
GO
