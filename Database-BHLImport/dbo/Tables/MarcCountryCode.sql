CREATE TABLE [dbo].[MarcCountryCode] (
    [CountryCode] NVARCHAR (3)   NOT NULL,
    [CountryName] NVARCHAR (200) DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([CountryCode] ASC)
);

