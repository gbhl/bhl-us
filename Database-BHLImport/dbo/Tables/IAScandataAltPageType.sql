CREATE TABLE [dbo].[IAScandataAltPageType] (
    [ScandataAltPageTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [ScandataID]            INT           NOT NULL,
    [PageType]              NVARCHAR (50) CONSTRAINT [DF_IAScandataAltPageType_PageType] DEFAULT ('') NOT NULL,
    [CreatedDate]           DATETIME      CONSTRAINT [DF_IAScandataAltPageType_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]      DATETIME      CONSTRAINT [DF_IAScandataAltPageType_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_IAScandataAltPageType] PRIMARY KEY CLUSTERED ([ScandataAltPageTypeID] ASC),
    CONSTRAINT [FK_IAScandataAltPageType_IAScandata] FOREIGN KEY ([ScandataID]) REFERENCES [dbo].[IAScandata] ([ScandataID])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_IAScandataAltPageType_ScandataType] 
	ON [dbo].[IAScandataAltPageType] ([ScandataID], [PageType]);
GO
