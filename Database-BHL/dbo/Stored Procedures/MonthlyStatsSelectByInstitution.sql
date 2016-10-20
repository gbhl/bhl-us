CREATE PROCEDURE dbo.MonthlyStatsSelectByInstitution

@InstitutionCode nvarchar(10)

AS

BEGIN

SET NOCOUNT ON

-- Select stats (including cumulative stats) for a particular institution
SELECT	s.InstitutionCode, 
		InstitutionName,
		StatType, 
		[Year], 
		[Month], 
		StatValue,
		SUM(StatValue) OVER (PARTITION BY StatType ORDER BY [Year], [Month]) AS CumulativeValue
FROM	dbo.Monthlystats s WITH (NOLOCK) 
		INNER JOIN dbo.Institution inst WITH (NOLOCK) ON s.InstitutionCode = inst.InstitutionCode
WHERE	s.InstitutionCode = @InstitutionCode
ORDER BY [Year], [Month], StatType

END