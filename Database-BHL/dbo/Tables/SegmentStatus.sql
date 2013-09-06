CREATE TABLE [dbo].[SegmentStatus] (
    [SegmentStatusID]    INT            NOT NULL,
    [StatusName]         NVARCHAR (50)  CONSTRAINT [DF_SegmentStatus_StatusName] DEFAULT ('') NOT NULL,
    [StatusDescription]  NVARCHAR (500) CONSTRAINT [DF_SegmentStatus_StatusDescription] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_SegmentStatus_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_SegmentStatus_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_SegmentStatus_CreationUserID] DEFAULT ((1)) NOT NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_SegmentStatus_LastModifiedUserID] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SegmentStatus] PRIMARY KEY CLUSTERED ([SegmentStatusID] ASC)
);

