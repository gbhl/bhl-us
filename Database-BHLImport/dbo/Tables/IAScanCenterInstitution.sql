CREATE TABLE [dbo].[IAScanCenterInstitution] (
    [ScanningCenterCode] NVARCHAR (200) NOT NULL,
    [InstitutionCode]    NVARCHAR (10)  NOT NULL,
    [Sponsor]            NVARCHAR (50)  CONSTRAINT [DF__ScanCente__Spons__6E565CE8] DEFAULT ('') NOT NULL,
    [SponsorName]        NVARCHAR (50)  CONSTRAINT [DF__ScanCente__Spons__6F4A8121] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK__ScanCenterInstit__6D6238AF] PRIMARY KEY CLUSTERED ([ScanningCenterCode] ASC)
);

