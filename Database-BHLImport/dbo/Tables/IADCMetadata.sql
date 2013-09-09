CREATE TABLE [dbo].[IADCMetadata] (
    [DCMetadataID]     INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]           INT            NOT NULL,
    [DCElementName]    NVARCHAR (15)  CONSTRAINT [DF_DCMetadata_DCElementName] DEFAULT ('') NOT NULL,
    [DCElementValue]   NVARCHAR (500) CONSTRAINT [DF_DCMetadata_DCElementValue] DEFAULT ('') NOT NULL,
    [Source]           NVARCHAR (50)  CONSTRAINT [DF_DCMetadata_Source] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME       CONSTRAINT [DF_DCMetadata_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_DCMetadata_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DCMetadata] PRIMARY KEY CLUSTERED ([DCMetadataID] ASC),
    CONSTRAINT [FK_DCMetadata_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IADCMetadata_ItemIDDCElementName]
    ON [dbo].[IADCMetadata]([ItemID] ASC, [DCElementName] ASC)
    INCLUDE([DCElementValue]);


GO
CREATE NONCLUSTERED INDEX [IX_IADCMetadata_ItemIDSource]
    ON [dbo].[IADCMetadata]([ItemID] ASC, [Source] ASC)
    INCLUDE([DCMetadataID]);

