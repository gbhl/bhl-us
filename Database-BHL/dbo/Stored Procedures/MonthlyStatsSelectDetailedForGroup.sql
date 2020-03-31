CREATE PROCEDURE [dbo].[MonthlyStatsSelectDetailedForGroup]

AS

BEGIN

SET NOCOUNT ON 

-- Select stats (including cumulative stats) for every institution group
SELECT	x.InstitutionGroupID, 
		InstitutionGroupName AS InstitutionName,
		StatType, 
		[Year], 
		[Month], 
		StatValue,
		SUM(StatValue) OVER (PARTITION BY x.InstitutionGroupID, StatType ORDER BY [Year], [Month]) AS CumulativeValue
FROM (	SELECT	InstitutionGroupID, 
				StatType, 
				[Year], 
				[Month], 
				sum(StatValue) AS StatValue
		FROM	dbo.Monthlystats m WITH (NOLOCK)
				INNER JOIN dbo.InstitutionGroupInstitution ig WITH (NOLOCK) ON m.InstitutionCode = ig.InstitutionCode
		WHERE	m.InstitutionCode IS NOT NULL
		GROUP BY InstitutionGroupID, StatType, [Year], [Month]
		) x INNER JOIN dbo.InstitutionGroup inst WITH (NOLOCK)
			ON x.InstitutionGroupID = inst.InstitutionGroupID
ORDER BY InstitutionGroupName, [Year], [Month], StatType

END
