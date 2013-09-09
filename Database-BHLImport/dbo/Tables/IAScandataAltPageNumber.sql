CREATE TABLE [dbo].[IAScandataAltPageNumber] (
    [ScandataAltPageNumberID] INT           IDENTITY (1, 1) NOT NULL,
    [ScandataID]              INT           NOT NULL,
    [Sequence]                INT           NOT NULL,
    [PagePrefix]              NVARCHAR (40) CONSTRAINT [DF_IAScandataAltPageNumber_PagePrefix] DEFAULT ('') NOT NULL,
    [PageNumber]              NVARCHAR (20) CONSTRAINT [DF_IAScandataAltPageNumber_PageNumber] DEFAULT ('') NOT NULL,
    [Implied]                 BIT           CONSTRAINT [DF_IAScandataAltPageNumber_Implied] DEFAULT ((0)) NOT NULL,
    [CreatedDate]             DATETIME      CONSTRAINT [DF_IAScandataAltPageNumber_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]        DATETIME      CONSTRAINT [DF_IAScandataAltPageNumber_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_IAScandataAltPageNumber] PRIMARY KEY CLUSTERED ([ScandataAltPageNumberID] ASC),
    CONSTRAINT [FK_IAScandataAltPageNumber_IAScandata] FOREIGN KEY ([ScandataID]) REFERENCES [dbo].[IAScandata] ([ScandataID])
);

