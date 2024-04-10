CREATE TABLE [dbo].[Item] (
    [ItemID]                    INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]                 NVARCHAR (50)  CONSTRAINT [DF_Item_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]            INT            NOT NULL,
    [ImportSourceID]            INT            NULL,
    [MARCBibID]                 NVARCHAR (50)  NOT NULL,
    [BarCode]                   NVARCHAR (200) NOT NULL,
    [ItemSequence]              SMALLINT       NULL,
    [MARCItemID]                NVARCHAR (200) NULL,
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
	[ScanningInstitutionCode]	NVARCHAR (10)  NULL,
	[RightsHolderCode]			NVARCHAR (10)  NULL,
	[EndYear]                   NVARCHAR(20)   CONSTRAINT [DF_Item_EndYear] DEFAULT '' NOT NULL ,
	[StartVolume]               NVARCHAR(10)   CONSTRAINT [DF_Item_StartVolume] DEFAULT '' NOT NULL,
	[EndVolume]                 NVARCHAR(10)   CONSTRAINT [DF_Item_EndVolume] DEFAULT '' NOT NULL,
	[StartIssue]                NVARCHAR(10)   CONSTRAINT [DF_Item_StartIssue] DEFAULT '' NOT NULL,
	[EndIssue]                  NVARCHAR(10)   CONSTRAINT [DF_Item_EndIssue] DEFAULT '' NOT NULL,
	[StartNumber]               NVARCHAR(10)   CONSTRAINT [DF_Item_StartNumber] DEFAULT '' NOT NULL,
	[EndNumber]                 NVARCHAR(10)   CONSTRAINT [DF_Item_EndNumber] DEFAULT '' NOT NULL,
	[StartSeries]               NVARCHAR(10)   CONSTRAINT [DF_Item_StartSeries] DEFAULT '' NOT NULL,
	[EndSeries]                 NVARCHAR(10)   CONSTRAINT [DF_Item_EndSeries] DEFAULT '' NOT NULL,
	[StartPart]                 NVARCHAR(10)   CONSTRAINT [DF_Item_StartPart] DEFAULT '' NOT NULL,
	[EndPart]                   NVARCHAR(10)   CONSTRAINT [DF_Item_EndPart] DEFAULT '' NOT NULL,
    [PageProgression]           NVARCHAR(10)   CONSTRAINT [DF_Item_PageProgression] DEFAULT '' NOT NULL,
	[VirtualVolume]             NVARCHAR(100)  CONSTRAINT [DF_Item_VirtualVolume] DEFAULT '' NOT NULL,
	[VirtualTitleID]            INT            NULL,
	[Summary]                   NVARCHAR(max)  CONSTRAINT [DF_Item_Summary] DEFAULT '' NOT NULL,
	[SegmentGenreID]            INT            NULL,
	[PublicationDetails]        NVARCHAR(400)  CONSTRAINT [DF_Item_PublicationDetails] DEFAULT '' NOT NULL,
	[PublisherName]             NVARCHAR(250)  CONSTRAINT [DF_Item_PublisherName] DEFAULT '' NOT NULL,
	[Issue]                     NVARCHAR(100)  CONSTRAINT [DF_Item_Issue] DEFAULT '' NOT NULL,
	[SegmentDate]               NVARCHAR(20)   CONSTRAINT [DF_Item_SegmentDate] DEFAULT '' NOT NULL,
	[StartPage]                 NVARCHAR(20)   CONSTRAINT [DF_Item_StartPage] DEFAULT '' NOT NULL,
	[EndPage]                   NVARCHAR(20)   CONSTRAINT [DF_Item_EndPage] DEFAULT '' NOT NULL,
	[Title] 					NVARCHAR(2000) CONSTRAINT [DF_Item_Title] DEFAULT '' NOT NULL,
	[SortTitle] 				NVARCHAR(2000) CONSTRAINT [DF_Item_SortTitle] DEFAULT '' NOT NULL,
	[ContainerTitle]            NVARCHAR(2000) CONSTRAINT [DF_Item_ContainerTitle] DEFAULT '' NOT NULL,
    CONSTRAINT [aaaaaItem_PK] PRIMARY KEY CLUSTERED ([ItemID] ASC),
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

