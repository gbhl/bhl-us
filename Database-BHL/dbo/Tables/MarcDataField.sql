CREATE TABLE [dbo].[MarcDataField] (
    [MarcDataFieldID]  INT       IDENTITY (1, 1) NOT NULL,
    [MarcID]           INT       NOT NULL,
    [Tag]              NCHAR (3) CONSTRAINT [DF_MarcDataField_Tag] DEFAULT ('') NOT NULL,
    [Indicator1]       NCHAR (1) CONSTRAINT [DF_MarcDataField_Indicator1] DEFAULT ('') NOT NULL,
    [Indicator2]       NCHAR (1) CONSTRAINT [DF_MarcDataField_Indicator2] DEFAULT ('') NOT NULL,
    [CreationDate]     DATETIME  CONSTRAINT [DF_MarcDataField_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME  CONSTRAINT [DF_MarcDataField_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcDataField] PRIMARY KEY CLUSTERED ([MarcDataFieldID] ASC),
    CONSTRAINT [FK_MarcDataField_Marc] FOREIGN KEY ([MarcID]) REFERENCES [dbo].[Marc] ([MarcID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_MarcDataField_MarcIDTag]
    ON [dbo].[MarcDataField]([MarcID] ASC, [Tag] ASC);

