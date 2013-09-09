CREATE TABLE [dbo].[BotanicusHarvestLog] (
    [BotanicusHarvestLogID] INT      IDENTITY (1, 1) NOT NULL,
    [HarvestStartDate]      DATETIME NOT NULL,
    [HarvestEndDate]        DATETIME NOT NULL,
    [AutomaticHarvest]      BIT      CONSTRAINT [DF__Botanicus__Autom__6A3BB341] DEFAULT ((1)) NOT NULL,
    [SuccessfulHarvest]     BIT      CONSTRAINT [DF_BotanicusHarvestLog_SuccessfulHarvest] DEFAULT ((1)) NOT NULL,
    [Title]                 INT      CONSTRAINT [DF__Botanicus__Title__6B2FD77A] DEFAULT ((0)) NOT NULL,
    [TitleTag]              INT      CONSTRAINT [DF__Botanicus__Title__6C23FBB3] DEFAULT ((0)) NOT NULL,
    [TitleCreator]          INT      CONSTRAINT [DF__Botanicus__Title__6D181FEC] DEFAULT ((0)) NOT NULL,
    [Creator]               INT      CONSTRAINT [DF__Botanicus__Creat__6E0C4425] DEFAULT ((0)) NOT NULL,
    [Item]                  INT      CONSTRAINT [DF__BotanicusH__Item__6F00685E] DEFAULT ((0)) NOT NULL,
    [Page]                  INT      CONSTRAINT [DF__BotanicusH__Page__6FF48C97] DEFAULT ((0)) NOT NULL,
    [IndicatedPage]         INT      CONSTRAINT [DF__Botanicus__Indic__70E8B0D0] DEFAULT ((0)) NOT NULL,
    [PagePageType]          INT      CONSTRAINT [DF__Botanicus__PageP__71DCD509] DEFAULT ((0)) NOT NULL,
    [PageName]              INT      CONSTRAINT [DF__Botanicus__PageN__72D0F942] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BotanicusHarvestLog] PRIMARY KEY CLUSTERED ([BotanicusHarvestLogID] ASC)
);

