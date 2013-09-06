CREATE TABLE [dbo].[MarcSubField] (
    [MarcSubFieldID]   INT            IDENTITY (1, 1) NOT NULL,
    [MarcDataFieldID]  INT            NOT NULL,
    [Code]             NCHAR (1)      CONSTRAINT [DF_MarcSubField_Code] DEFAULT ('') NOT NULL,
    [Value]            NVARCHAR (200) CONSTRAINT [DF_MarcSubField_Value] DEFAULT ('') NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_MarcSubField_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_MarcSubField_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcSubField] PRIMARY KEY CLUSTERED ([MarcSubFieldID] ASC),
    CONSTRAINT [FK_MarcSubField_MarcDataField] FOREIGN KEY ([MarcDataFieldID]) REFERENCES [dbo].[MarcDataField] ([MarcDataFieldID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MarcSubField_CodeValue]
    ON [dbo].[MarcSubField]([Code] ASC, [Value] ASC)
    INCLUDE([MarcDataFieldID], [MarcSubFieldID]);


GO
CREATE NONCLUSTERED INDEX [IX_MarcSubField_MarcDataFieldIDCode]
    ON [dbo].[MarcSubField]([MarcDataFieldID] ASC, [Code] ASC)
    INCLUDE([Value]);

