CREATE TABLE [dbo].[IAAnalysisHarvestLog] (
    [IAAnalysisHarvestLogID] INT      IDENTITY (1, 1) NOT NULL,
    [HarvestDate]            DATETIME CONSTRAINT [DF__IAAnalysi__Harve__52050254] DEFAULT (getdate()) NOT NULL,
    [SuccessfulHarvest]      TINYINT  CONSTRAINT [DF__IAAnalysi__Succe__52F9268D] DEFAULT ((1)) NOT NULL,
    [Item]                   INT      CONSTRAINT [DF__IAAnalysis__Item__53ED4AC6] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IAAnalysisHarvestLog] PRIMARY KEY CLUSTERED ([IAAnalysisHarvestLogID] ASC)
);

