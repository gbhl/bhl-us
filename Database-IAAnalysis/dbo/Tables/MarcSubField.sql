CREATE TABLE [dbo].[MarcSubField] (
    [MarcSubFieldID]  INT            IDENTITY (1, 1) NOT NULL,
    [MarcDataFieldID] INT            NOT NULL,
    [Code]            NCHAR (1)      CONSTRAINT [DF__MarcSubFie__Code__3B75D760] DEFAULT ('') NOT NULL,
    [Value]           NVARCHAR (200) CONSTRAINT [DF__MarcSubFi__Value__3C69FB99] DEFAULT ('') NOT NULL,
    [CreationDate]    DATETIME       CONSTRAINT [DF__MarcSubFi__Creat__3D5E1FD2] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcSubField] PRIMARY KEY CLUSTERED ([MarcSubFieldID] ASC),
    CONSTRAINT [FK_MarcSubField_MarcDataField] FOREIGN KEY ([MarcDataFieldID]) REFERENCES [dbo].[MarcDataField] ([MarcDataFieldID]) ON DELETE CASCADE
)
WITH (DATA_COMPRESSION = PAGE);
GO

CREATE NONCLUSTERED INDEX [IX_MarcSubField_CodeValue]
    ON [dbo].[MarcSubField]([Code] ASC, [Value] ASC)
    INCLUDE([MarcDataFieldID], [MarcSubFieldID]);
GO

CREATE NONCLUSTERED INDEX [IX_MarcSubField_MarcDataFieldID]
    ON [dbo].[MarcSubField]([MarcDataFieldID] ASC)
    INCLUDE([MarcSubFieldID], [Code], [Value]);
