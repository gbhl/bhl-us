CREATE TABLE [dbo].[MarcControl] (
    [MarcControlID] INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]        INT            NOT NULL,
    [Tag]           NCHAR (3)      CONSTRAINT [DF__MarcControl__Tag__276EDEB3] DEFAULT ('') NOT NULL,
    [Value]         NVARCHAR (200) CONSTRAINT [DF__MarcContr__Value__286302EC] DEFAULT ('') NOT NULL,
    [CreationDate]  DATETIME       CONSTRAINT [DF__MarcContr__Creat__29572725] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcControl] PRIMARY KEY CLUSTERED ([MarcControlID] ASC),
    CONSTRAINT [FK_MarcControl_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_MarcControl_ItemID]
    ON [dbo].[MarcControl]([ItemID] ASC)
    INCLUDE([MarcControlID]);

