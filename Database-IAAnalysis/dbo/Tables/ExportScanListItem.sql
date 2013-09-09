CREATE TABLE [dbo].[ExportScanListItem] (
    [ItemID]         INT            NOT NULL,
    [BHLItemID]      INT            NOT NULL,
    [BHLTitleID]     INT            NOT NULL,
    [Title]          NVARCHAR (600) CONSTRAINT [DF__ExportSca__Title__5EBF139D] DEFAULT ('') NOT NULL,
    [Author]         NVARCHAR (600) CONSTRAINT [DF__ExportSca__Autho__5FB337D6] DEFAULT ('') NOT NULL,
    [Volume]         NVARCHAR (200) CONSTRAINT [DF__ExportSca__Volum__60A75C0F] DEFAULT ('') NOT NULL,
    [LocalNumber]    NVARCHAR (200) CONSTRAINT [DF__ExportSca__Local__619B8048] DEFAULT ('') NOT NULL,
    [OCLC]           NVARCHAR (200) CONSTRAINT [DF__ExportScan__OCLC__628FA481] DEFAULT ('') NOT NULL,
    [CallNumber]     NVARCHAR (200) CONSTRAINT [DF__ExportSca__CallN__6383C8BA] DEFAULT ('') NOT NULL,
    [Publisher]      NVARCHAR (200) CONSTRAINT [DF__ExportSca__Publi__6477ECF3] DEFAULT ('') NOT NULL,
    [PublisherPlace] NVARCHAR (200) CONSTRAINT [DF__ExportSca__Publi__656C112C] DEFAULT ('') NOT NULL,
    [Chronology]     NVARCHAR (200) CONSTRAINT [DF__ExportSca__Chron__66603565] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK__ExportScanListIt__5DCAEF64] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);

