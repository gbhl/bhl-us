CREATE TABLE [dbo].[Item] (
    [ItemID]                  INT            IDENTITY (1, 1) NOT NULL,
    [Identifier]              NVARCHAR (50)  CONSTRAINT [DF__Item__Identifier] DEFAULT ('') NOT NULL,
    [MARCLeader]              NVARCHAR (200) NOT NULL,
    [Sponsor]                 NVARCHAR (50)  CONSTRAINT [DF__Item__Sponsor] DEFAULT ('') NOT NULL,
    [Contributor]             NVARCHAR (100) CONSTRAINT [DF__Item__Contributor] DEFAULT ('') NOT NULL,
    [ScanningCenter]          NVARCHAR (50)  CONSTRAINT [DF__Item__ScanningCenter] DEFAULT ('') NOT NULL,
    [CollectionLibrary]       NVARCHAR (20)  CONSTRAINT [DF__Item__CollectionLibrary] DEFAULT ('') NOT NULL,
    [CallNumber]              NVARCHAR (50)  CONSTRAINT [DF__Item__CallNumber] DEFAULT ('') NOT NULL,
    [ImageCount]              INT            NULL,
    [CurationState]           NVARCHAR (50)  CONSTRAINT [DF__Item__CurationState] DEFAULT ('') NOT NULL,
    [PossibleCopyrightStatus] NVARCHAR (50)  CONSTRAINT [DF__Item__PossibleCopyrightStatus] DEFAULT ('') NOT NULL,
    [Volume]                  NVARCHAR (200) CONSTRAINT [DF_Item_Volume] DEFAULT ('') NOT NULL,
    [ScanDate]                NVARCHAR (50)  CONSTRAINT [DF__Item__ScanDate] DEFAULT ('') NOT NULL,
    [AddedDate]               DATETIME       NULL,
    [PublicDate]              DATETIME       NULL,
    [UpdateDate]              DATETIME       NULL,
    [SponsorDate]             NVARCHAR (50)  NULL,
    [ItemStatusID]            INT            CONSTRAINT [DF__Item__ItemStatus] DEFAULT ((10)) NOT NULL,
    [MetaGetStatus]           NVARCHAR (30)  CONSTRAINT [DF__Item__MetaGetStatus] DEFAULT ('') NOT NULL,
    [MarcGetStatus]           NVARCHAR (30)  CONSTRAINT [DF__Item__MarcGetStatus] DEFAULT ('') NOT NULL,
    [CreationDate]            DATETIME       CONSTRAINT [DF__Item__CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Item_Identifier]
    ON [dbo].[Item]([Identifier] ASC);

