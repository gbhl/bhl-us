CREATE TABLE [dbo].[IAMarcSubField] (
    [MarcSubFieldID]   INT             IDENTITY (1, 1) NOT NULL,
    [MarcDataFieldID]  INT             NOT NULL,
    [Code]             NCHAR (1)       CONSTRAINT [DF_MarcSubField_Code] DEFAULT ('') NOT NULL,
    [Value]            NVARCHAR (2000) CONSTRAINT [DF_MarcSubField_Value] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME        CONSTRAINT [DF_MarcSubField_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_MarcSubField_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcSubField] PRIMARY KEY CLUSTERED ([MarcSubFieldID] ASC),
    CONSTRAINT [FK_MarcSubField_MarcDataField] FOREIGN KEY ([MarcDataFieldID]) REFERENCES [dbo].[IAMarcDataField] ([MarcDataFieldID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAMarcSubField_MarcDataFieldIDCodeValue]
    ON [dbo].[IAMarcSubField]([MarcDataFieldID] ASC, [Code] ASC)
    INCLUDE([MarcSubFieldID], [Value]);

