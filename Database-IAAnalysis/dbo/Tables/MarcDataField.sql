CREATE TABLE [dbo].[MarcDataField] (
    [MarcDataFieldID] INT       IDENTITY (1, 1) NOT NULL,
    [ItemID]          INT       NOT NULL,
    [Tag]             NCHAR (3) CONSTRAINT [DF__MarcDataFie__Tag__30F848ED] DEFAULT ('') NOT NULL,
    [Indicator1]      NCHAR (1) CONSTRAINT [DF__MarcDataF__Indic__31EC6D26] DEFAULT ('') NOT NULL,
    [Indicator2]      NCHAR (1) CONSTRAINT [DF__MarcDataF__Indic__32E0915F] DEFAULT ('') NOT NULL,
    [CreationDate]    DATETIME  CONSTRAINT [DF__MarcDataF__Creat__33D4B598] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcDataField] PRIMARY KEY CLUSTERED ([MarcDataFieldID] ASC),
    CONSTRAINT [FK_MarcDataField_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_MarcDataField_Tag]
    ON [dbo].[MarcDataField]([Tag] ASC)
    INCLUDE([MarcDataFieldID], [ItemID]);


GO
CREATE NONCLUSTERED INDEX [IX_MarcDataField_ItemID]
    ON [dbo].[MarcDataField]([ItemID] ASC)
    INCLUDE([MarcDataFieldID], [Tag], [Indicator1], [Indicator2]);

