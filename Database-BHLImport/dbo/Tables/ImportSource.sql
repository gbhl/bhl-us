CREATE TABLE [dbo].[ImportSource] (
    [ImportSourceID]   INT           NOT NULL,
    [Source]           NVARCHAR (50) NOT NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF_ImportSource_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF_ImportSource_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ImportSource] PRIMARY KEY CLUSTERED ([ImportSourceID] ASC)
);

