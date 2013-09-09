CREATE TABLE [dbo].[IAFileFormat] (
    [FileFormatID]     INT           IDENTITY (1, 1) NOT NULL,
    [Format]           NVARCHAR (50) CONSTRAINT [DF_FileFormat_Format] DEFAULT ('') NOT NULL,
    [Download]         BIT           CONSTRAINT [DF_FileFormat_Download] DEFAULT ((0)) NOT NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF_FileFormat_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF_FileFormat_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_FileFormat] PRIMARY KEY CLUSTERED ([FileFormatID] ASC)
);

