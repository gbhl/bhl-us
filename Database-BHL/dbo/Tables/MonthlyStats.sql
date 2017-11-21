CREATE TABLE [dbo].[MonthlyStats] (
	[MonthlyStatID]    INT            IDENTITY(1,1) NOT NULL,
    [Year]             INT            NOT NULL,
    [Month]            INT            NOT NULL,
    [InstitutionCode]  NVARCHAR (10)  NULL,
    [StatType]         NVARCHAR (100) NOT NULL,
	[StatLevel]        NVARCHAR (100) NOT NULL,
    [StatValue]        INT            NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_MonthlyStats_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_MonthlyStats_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MonthlyStats] PRIMARY KEY CLUSTERED (MonthlyStatID)
);

CREATE UNIQUE NONCLUSTERED INDEX IX_MonthlyStats_YearMonthInstitutionType ON dbo.MonthlyStats 
( 
	Year, Month, InstitutionCode, StatType, StatLevel 
)
GO
