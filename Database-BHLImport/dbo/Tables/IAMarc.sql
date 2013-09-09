CREATE TABLE [dbo].[IAMarc] (
    [MarcID]           INT           IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT           NOT NULL,
    [Leader]           VARCHAR (200) CONSTRAINT [DF_Marc_Leader] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME      CONSTRAINT [DF_Marc_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME      CONSTRAINT [DF_Marc_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Marc] PRIMARY KEY CLUSTERED ([MarcID] ASC),
    CONSTRAINT [FK_Marc_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAMarc_ItemID]
    ON [dbo].[IAMarc]([ItemID] ASC)
    INCLUDE([MarcID], [Leader]);

