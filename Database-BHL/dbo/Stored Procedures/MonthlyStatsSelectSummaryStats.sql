CREATE PROCEDURE dbo.MonthlyStatsSelectSummaryStats

@BHLMemberLibraryOnly bit

AS

BEGIN

SET NOCOUNT ON 

-- Select stats (including cumulative stats) for all BHL
SELECT	'' AS InstitutionCode, 
		'' AS InstitutionName,
		StatType, 
		[Year], 
		[Month], 
		StatValue,
		SUM(StatValue) OVER (PARTITION BY StatType ORDER BY [Year], [Month]) AS CumulativeValue
FROM (	SELECT	StatType, 
				[Year], 
				[Month], 
				sum(StatValue) as StatValue
		FROM	dbo.Monthlystats s WITH (NOLOCK) LEFT JOIN dbo.Institution inst WITH (NOLOCK)
					ON s.InstitutionCode = inst.InstitutionCode
		WHERE	(inst.BHLMemberLibrary = @BHLMemberLibraryOnly OR @BHLMemberLibraryOnly = 0)
		OR		s.InstitutionCode = 'N/A'
		GROUP BY StatType, [Year], [Month]
		) x
ORDER BY InstitutionName, [Year], [Month], StatType

END
