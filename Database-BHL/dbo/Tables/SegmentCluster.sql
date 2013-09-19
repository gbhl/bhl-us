CREATE TABLE [dbo].[SegmentCluster] (
    [SegmentClusterID]   INT      IDENTITY (1, 1) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_SegmentCluster_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_SegmentCluster_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_SegmentCluster_CreationUserID] DEFAULT ((1)) NOT NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_SegmentCluster_LastModifiedUserID] DEFAULT ((1)) NOT NULL,
    [SegmentClusterTypeID] INT	  CONSTRAINT [DF_SegmentCluster_SegmentClusterTypeID] DEFAULT 10 NOT NULL , 
    CONSTRAINT [PK_SegmentCluster] PRIMARY KEY CLUSTERED ([SegmentClusterID] ASC)
);

