CREATE TABLE [dbo].[IAMarcDataField] (
    [MarcDataFieldID]  INT       IDENTITY (1, 1) NOT NULL,
    [MarcID]           INT       NOT NULL,
    [Tag]              NCHAR (3) CONSTRAINT [DF_MarcDataField_Tag] DEFAULT ('') NOT NULL,
    [Indicator1]       NCHAR (1) CONSTRAINT [DF_MarcDataField_Indicator1] DEFAULT ('') NOT NULL,
    [Indicator2]       NCHAR (1) CONSTRAINT [DF_MarcDataField_Indicator2] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME  CONSTRAINT [DF_MarcDataField_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME  CONSTRAINT [DF_MarcDataField_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcDataField] PRIMARY KEY CLUSTERED ([MarcDataFieldID] ASC),
    CONSTRAINT [FK_MarcDataField_Marc] FOREIGN KEY ([MarcID]) REFERENCES [dbo].[IAMarc] ([MarcID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAMarcDataField_MarcIDTag]
    ON [dbo].[IAMarcDataField]([MarcID] ASC, [Tag] ASC)
    INCLUDE([MarcDataFieldID], [Indicator1], [Indicator2]);


GO
CREATE NONCLUSTERED INDEX [IX_IAMarcDataField_Tag]
    ON [dbo].[IAMarcDataField]([Tag] ASC)
    INCLUDE([MarcDataFieldID], [MarcID], [Indicator1], [Indicator2]);

