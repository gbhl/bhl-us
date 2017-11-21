CREATE PROCEDURE [dbo].[MonthlyStatsSelectDetailed]

@BHLMemberLibraryOnly bit

AS

BEGIN

SET NOCOUNT ON 

-- Select stats (including cumulative stats) for every institution
SELECT	x.InstitutionCode, 
		InstitutionName,
		StatType, 
		[Year], 
		[Month], 
		StatValue,
		SUM(StatValue) OVER (PARTITION BY x.InstitutionCode, StatType ORDER BY [Year], [Month]) AS CumulativeValue
FROM (	SELECT	InstitutionCode, 
				StatType, 
				[Year], 
				[Month], 
				sum(StatValue) AS StatValue
		FROM	dbo.Monthlystats WITH (NOLOCK)
		WHERE	InstitutionCode IS NOT NULL
		GROUP BY InstitutionCode, StatType, [Year], [Month]
		) x INNER JOIN dbo.Institution inst WITH (NOLOCK)
			ON x.InstitutionCode = inst.InstitutionCode
WHERE	inst.BHLMemberLibrary = @BHLMemberLibraryOnly OR @BHLMemberLibraryOnly = 0
ORDER BY InstitutionName, [Year], [Month], StatType

END
