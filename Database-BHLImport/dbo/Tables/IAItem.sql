﻿CREATE TABLE [dbo].[IAItem] (
    [ItemID]                    INT            IDENTITY (1, 1) NOT NULL,
    [ItemStatusID]              INT            CONSTRAINT [DF_Item_ItemStatusID] DEFAULT ((10)) NOT NULL,
    [IAIdentifierPrefix]        NVARCHAR (50)  CONSTRAINT [DF_Item_IAIdentifierPrefix] DEFAULT ('') NOT NULL,
    [IAIdentifier]              NVARCHAR (50)  CONSTRAINT [DF_Table_1_ExternalIdentifier] DEFAULT ('') NOT NULL,
    [Sponsor]                   NVARCHAR (100) CONSTRAINT [DF_Item_Sponsor] DEFAULT ('') NOT NULL,
    [SponsorName]               NVARCHAR (50)  NULL,
    [ScanningCenter]            NVARCHAR (50)  CONSTRAINT [DF_Item_ScanningCenter] DEFAULT ('') NOT NULL,
    [CallNumber]                NVARCHAR (50)  CONSTRAINT [DF_Item_CallNumber] DEFAULT ('') NOT NULL,
    [ImageCount]                INT            NULL,
    [IdentifierAccessUrl]       NVARCHAR (100) NULL,
    [Volume]                    NVARCHAR (50)  CONSTRAINT [DF_Item_Volume] DEFAULT ('') NOT NULL,
    [Note]                      NVARCHAR (255) CONSTRAINT [DF_Item_Note] DEFAULT ('') NOT NULL,
    [ScanOperator]              NVARCHAR (100) CONSTRAINT [DF_Item_Operator] DEFAULT ('') NOT NULL,
    [ScanDate]                  NVARCHAR (50)  CONSTRAINT [DF_Item_ScanDate] DEFAULT ('') NOT NULL,
    [ExternalStatus]            NVARCHAR (50)  CONSTRAINT [DF_Item_ExternalStatus] DEFAULT ('') NOT NULL,
    [MARCBibID]                 NVARCHAR (50)  CONSTRAINT [DF_IAItem_MARCBibID] DEFAULT ('') NOT NULL,
    [BarCode]                   NVARCHAR (40)  CONSTRAINT [DF_IAItem_BarCode] DEFAULT ('') NOT NULL,
    [IADateStamp]               DATETIME       NULL,
    [IAAddedDate]               DATETIME       NULL,
    [LastOAIDataHarvestDate]    DATETIME       NULL,
    [LastXMLDataHarvestDate]    DATETIME       NULL,
    [LastProductionDate]        DATETIME       NULL,
    [CreatedDate]               DATETIME       CONSTRAINT [DF_Item_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]          DATETIME       CONSTRAINT [DF_Item_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [ShortTitle]                NVARCHAR (255) NULL,
    [SponsorDate]               NVARCHAR (50)  NULL,
    [TitleID]                   NVARCHAR (50)  CONSTRAINT [DF_IAItem_TitleID] DEFAULT ('') NOT NULL,
    [Year]                      NVARCHAR (20)  CONSTRAINT [DF_IAItem_Year] DEFAULT ('') NOT NULL,
    [IdentifierBib]             NVARCHAR (50)  CONSTRAINT [DF_IAItem_IdentifierBib] DEFAULT ('') NOT NULL,
    [ZQuery]                    NVARCHAR (200) CONSTRAINT [DF_IAItem_ZQuery] DEFAULT ('') NOT NULL,
    [LicenseUrl]                NVARCHAR (MAX) CONSTRAINT [DF_IAItem_LicenseUrl] DEFAULT ('') NOT NULL,
    [Rights]                    NVARCHAR (MAX) CONSTRAINT [DF_IAItem_Rights] DEFAULT ('') NOT NULL,
    [DueDiligence]              NVARCHAR (MAX) CONSTRAINT [DF_IAItem_DueDiligence] DEFAULT ('') NOT NULL,
    [PossibleCopyrightStatus]   NVARCHAR (MAX) CONSTRAINT [DF_IAItem_PossibleCopyrightStatus] DEFAULT ('') NOT NULL,
    [CopyrightRegion]           NVARCHAR (50)  CONSTRAINT [DF_IAItem_CopyrightRegion] DEFAULT ('') NOT NULL,
    [CopyrightComment]          NVARCHAR (MAX) CONSTRAINT [DF_IAItem_CopyrightComment] DEFAULT ('') NOT NULL,
    [CopyrightEvidence]         NVARCHAR (MAX) CONSTRAINT [DF_IAItem_CopyrightEvidence] DEFAULT ('') NOT NULL,
    [CopyrightEvidenceOperator] NVARCHAR (100) CONSTRAINT [DF_IAItem_CopyrightEvidenceOperator] DEFAULT ('') NOT NULL,
    [CopyrightEvidenceDate]     NVARCHAR (30)  CONSTRAINT [DF_IAItem_CopyrightEvidenceDate] DEFAULT ('') NOT NULL,
    [LocalFileFolder]           NVARCHAR (200) CONSTRAINT [DF_IAItem_LocalFileFolder] DEFAULT ('') NOT NULL,
    [NoMARCOk]                  TINYINT        CONSTRAINT [DF_IAItem_NoMARCOk] DEFAULT ((0)) NOT NULL,
	[ScanningInstitution]		NVARCHAR(500)  CONSTRAINT [DF_IAItem_ScanningInstitution] DEFAULT '' NOT NULL,
	[RightsHolder]				NVARCHAR(500)  CONSTRAINT [DF_IAItem_RightsHolder] DEFAULT '' NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([ItemID] ASC),
    CONSTRAINT [FK_Item_ItemStatus] FOREIGN KEY ([ItemStatusID]) REFERENCES [dbo].[IAItemStatus] ([ItemStatusID])
);

GO

CREATE NONCLUSTERED INDEX [IX_IAItem_IAIdentifier]
	ON [dbo].[IAItem] ([IAIdentifier]);
GO

CREATE NONCLUSTERED INDEX [IX_IAItem_ItemStatus] 
	ON [dbo].[IAItem] ([ItemStatusID])
INCLUDE ([IAAddedDate], [CreatedDate]);
GO

