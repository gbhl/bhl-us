﻿CREATE TABLE [dbo].[MonthlyStats] (
    [Year]             INT            NOT NULL,
    [Month]            INT            NOT NULL,
    [InstitutionName]  NVARCHAR (255) NOT NULL,
    [StatType]         NVARCHAR (100) NOT NULL,
    [StatValue]        INT            NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_MonthlyStats_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_MonthlyStats_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MonthlyStats] PRIMARY KEY CLUSTERED ([Year] ASC, [Month] ASC, [InstitutionName] ASC, [StatType] ASC)
);

