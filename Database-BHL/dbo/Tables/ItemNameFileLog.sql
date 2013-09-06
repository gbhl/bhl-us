CREATE TABLE [dbo].[ItemNameFileLog] (
    [LogID]            INT      IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT      NOT NULL,
    [DoCreate]         BIT      CONSTRAINT [DF__ItemNameF__DoCre__1C7D1A4B] DEFAULT ((1)) NOT NULL,
    [DoUpload]         BIT      CONSTRAINT [DF__ItemNameF__DoUpl__1D713E84] DEFAULT ((1)) NOT NULL,
    [LastCreateDate]   DATETIME NULL,
    [LastUploadDate]   DATETIME NULL,
    [CreationDate]     DATETIME CONSTRAINT [DF__ItemNameF__Creat__1E6562BD] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME CONSTRAINT [DF__ItemNameF__LastM__1F5986F6] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemNameFileLog] PRIMARY KEY CLUSTERED ([LogID] ASC),
    CONSTRAINT [FK_ItemNameFileLog_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
);

