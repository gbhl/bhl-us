CREATE TABLE [dbo].[ItemSource] (
    [ItemSourceID]         INT            IDENTITY (1, 1) NOT NULL,
    [SourceName]           NVARCHAR (50)  CONSTRAINT [DF_ItemSource_SourceName] DEFAULT ('') NOT NULL,
    [DownloadUrl]          NVARCHAR (100) CONSTRAINT [DF_ItemSource_DownloadUrl] DEFAULT ('') NOT NULL,
    [CreationDate]         DATETIME       CONSTRAINT [DF_ItemSource_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]     DATETIME       CONSTRAINT [DF_ItemSource_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [ImageServerUrlFormat] NVARCHAR (200) CONSTRAINT [DF_ItemSource_ImageServerUrl] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_ItemSource] PRIMARY KEY CLUSTERED ([ItemSourceID] ASC)
);

