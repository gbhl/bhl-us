CREATE TABLE [dbo].[NameSource] (
    [NameSourceID]       INT           IDENTITY (1, 1) NOT NULL,
    [SourceName]         NVARCHAR (50) CONSTRAINT [DF_NameSource_SourceName] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_NameSource_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_NameSource_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT           NULL,
    [LastModifiedUserID] INT           NULL,
    CONSTRAINT [PK_NameSource] PRIMARY KEY CLUSTERED ([NameSourceID] ASC)
);

