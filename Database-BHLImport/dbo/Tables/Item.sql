CREATE TABLE [dbo].[Item] (
    [ItemID]                    INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]                 NVARCHAR (50)  CONSTRAINT [DF_Item_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]            INT            NOT NULL,
    [ImportSourceID]            INT            NULL,
    [MARCBibID]                 NVARCHAR (50)  NOT NULL,
    [BarCode]                   NVARCHAR (40)  NOT NULL,
    [ItemSequence]              SMALLINT       NULL,
    [MARCItemID]                NVARCHAR (50)  NULL,
    [CallNumber]                NVARCHAR (100) NULL,
    [Volume]                    NVARCHAR (100) NULL,
    [InstitutionCode]           NVARCHAR (10)  CONSTRAINT [DF__Item__Institutio__2EDAF651] DEFAULT ('MO') NULL,
    [LanguageCode]              NVARCHAR (10)  NULL,
    [Sponsor]                   NVARCHAR (100) NULL,
    [ItemDescription]           NTEXT          NULL,
    [ScannedBy]                 INT            NULL,
    [PDFSize]                   INT            NULL,
    [VaultID]                   INT            NULL,
    [NumberOfFiles]             SMALLINT       NULL,
    [Note]                      NVARCHAR (255) NULL,
    [ItemStatusID]              INT            CONSTRAINT [DFLT_Item_ItemStatusID] DEFAULT ((10)) NOT NULL,
    [ScanningUser]              NVARCHAR (100) NULL,
    [ScanningDate]              DATETIME       NULL,
    [PaginationCompleteUserID]  INT            NULL,
    [PaginationCompleteDate]    DATETIME       NULL,
    [PaginationStatusID]        INT            NULL,
    [PaginationStatusUserID]    INT            NULL,
    [PaginationStatusDate]      DATETIME       NULL,
    [LastPageNameLookupDate]    DATETIME       NULL,
    [ExternalCreationDate]      DATETIME       NULL,
    [ExternalLastModifiedDate]  DATETIME       NULL,
    [ExternalCreationUser]      INT            CONSTRAINT [DFLT_Item_CreationUserID] DEFAULT ((1)) NULL,
    [ExternalLastModifiedUser]  INT            CONSTRAINT [DFLT_Item_LastModifiedUserID] DEFAULT ((1)) NULL,
    [ProductionDate]            DATETIME       NULL,
    [CreatedDate]               DATETIME       CONSTRAINT [DF_Item_CreatedDate_1] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]          DATETIME       CONSTRAINT [DF_Item_LastModifiedDate_1] DEFAULT (getdate()) NOT NULL,
    [Year]                      NVARCHAR (20)  NULL,
    [IdentifierBib]             NVARCHAR (50)  NULL,
    [ZQuery]                    NVARCHAR (200) NULL,
    [LicenseUrl]                NVARCHAR (MAX) NULL,
    [Rights]                    NVARCHAR (MAX) NULL,
    [DueDiligence]              NVARCHAR (MAX) NULL,
    [CopyrightStatus]           NVARCHAR (MAX) NULL,
    [CopyrightRegion]           NVARCHAR (50)  NULL,
    [CopyrightComment]          NVARCHAR (MAX) NULL,
    [CopyrightEvidence]         NVARCHAR (MAX) NULL,
    [CopyrightEvidenceOperator] NVARCHAR (100) NULL,
    [CopyrightEvidenceDate]     NVARCHAR (30)  NULL,
    CONSTRAINT [aaaaaItem_PK] PRIMARY KEY NONCLUSTERED ([ItemID] ASC),
    CONSTRAINT [FK_Item_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_Item_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [BarCode]
    ON [dbo].[Item]([BarCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Item]
    ON [dbo].[Item]([ItemID] ASC)
    INCLUDE([BarCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Item_ImportStatusImportSourceItemStatus]
    ON [dbo].[Item]([ImportStatusID] ASC, [ImportSourceID] ASC, [ItemStatusID] ASC)
    INCLUDE([BarCode]);

