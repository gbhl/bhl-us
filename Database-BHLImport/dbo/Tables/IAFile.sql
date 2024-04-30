CREATE TABLE [dbo].[IAFile] (
    [FileID]                     INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]                     INT            NOT NULL,
    [RemoteFileName]             NVARCHAR (250) CONSTRAINT [DF_File_RemoteFileName] DEFAULT ('') NOT NULL,
    [LocalFileName]              NVARCHAR (250) CONSTRAINT [DF_File_LocalFileName] DEFAULT ('') NOT NULL,
    [Source]                     NVARCHAR (20)  CONSTRAINT [DF_File_Source] DEFAULT ('') NOT NULL,
    [Format]                     NVARCHAR (50)  CONSTRAINT [DF_File_Format] DEFAULT ('') NOT NULL,
    [Original]                   NVARCHAR (50)  CONSTRAINT [DF_File_Original] DEFAULT ('') NOT NULL,
    [RemoteFileLastModifiedDate] DATETIME       NULL,
    [CreatedDate]                DATETIME       CONSTRAINT [DF_File_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]           DATETIME       CONSTRAINT [DF_File_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_File_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAFile_ItemIDFormat]
    ON [dbo].[IAFile]([ItemID] ASC, [Format] ASC)
    INCLUDE([FileID], [RemoteFileName], [LocalFileName], [Source], [Original], [RemoteFileLastModifiedDate], [CreatedDate], [LastModifiedDate]);


GO
CREATE NONCLUSTERED INDEX [IX_IAFile_ItemIDRemoteFileName]
    ON [dbo].[IAFile]([ItemID] ASC, [RemoteFileName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IAFile_RemoteFileName]
    ON [dbo].[IAFile]([RemoteFileName] ASC);

