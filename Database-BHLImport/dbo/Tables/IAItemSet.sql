CREATE TABLE [dbo].[IAItemSet] (
    [ItemID]      INT      NOT NULL,
    [SetID]       INT      NOT NULL,
    [CreatedDate] DATETIME CONSTRAINT [DF_ItemSet_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemSet] PRIMARY KEY CLUSTERED ([ItemID] ASC, [SetID] ASC),
    CONSTRAINT [FK_ItemSet_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID]),
    CONSTRAINT [FK_ItemSet_Set] FOREIGN KEY ([SetID]) REFERENCES [dbo].[IASet] ([SetID])
);

