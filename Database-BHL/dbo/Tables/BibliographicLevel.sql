CREATE TABLE [dbo].[BibliographicLevel] (
    [BibliographicLevelID]   INT           IDENTITY (1, 1) NOT NULL,
    [BibliographicLevelName] NVARCHAR (50) CONSTRAINT [DF_BibliographicLevel_BibliographicLevelName] DEFAULT ('') NOT NULL,
    [BibliographicLevelLabel] NVARCHAR (50) CONSTRAINT [DF_BibliographicLevel_BibliographicLevelLabel] DEFAULT ('') NOT NULL,
    [MARCCode]               NCHAR (1)     CONSTRAINT [DF_BibliographicLevel_MARCCode] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_BibliographicLevel] PRIMARY KEY CLUSTERED ([BibliographicLevelID] ASC)
);

GO
