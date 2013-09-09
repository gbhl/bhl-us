CREATE TABLE [dbo].[ExportScanListDates] (
    [ItemID]    INT          NOT NULL,
    [StartYear] NVARCHAR (4) CONSTRAINT [DF__ExportSca__Start__6B24EA82] DEFAULT ('') NOT NULL,
    [EndYear]   NVARCHAR (4) CONSTRAINT [DF__ExportSca__EndYe__6C190EBB] DEFAULT ('') NOT NULL
);

