CREATE TABLE [dbo].[ExportScanListOCLC] (
    [ItemID] INT            NOT NULL,
    [o035]   NVARCHAR (200) CONSTRAINT [DF__ExportScan__o035__68487DD7] DEFAULT ('') NULL,
    [o010]   NVARCHAR (200) CONSTRAINT [DF__ExportScan__o010__693CA210] DEFAULT ('') NULL
);

