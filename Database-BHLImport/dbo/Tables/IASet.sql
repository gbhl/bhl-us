CREATE TABLE [dbo].[IASet] (
    [SetID]                INT            IDENTITY (1, 1) NOT NULL,
    [SetSpecification]     NVARCHAR (200) CONSTRAINT [DF_Set_SetSpecification] DEFAULT ('') NOT NULL,
    [DownloadAll]          BIT            CONSTRAINT [DF_Set_DownloadAll] DEFAULT ((0)) NOT NULL,
    [LastDownloadDate]     DATETIME       NULL,
    [CreatedDate]          DATETIME       CONSTRAINT [DF_Set_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]     DATETIME       CONSTRAINT [DF_Set_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [LastFullDownloadDate] DATETIME       NULL,
    CONSTRAINT [PK_Set] PRIMARY KEY CLUSTERED ([SetID] ASC)
);

