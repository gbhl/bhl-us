CREATE PROCEDURE [dbo].[MonthlyStatsSelectSummaryStats]

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
FROM	dbo.Monthlystats s WITH (NOLOCK)
WHERE	(StatLevel = 'Total' AND @BHLMemberLibraryOnly = 0) 
OR		(StatLevel = 'MemberTotal' AND @BHLMemberLibraryOnly = 1)
OR		s.StatType IN ('PDFs Created', 'DOIs Created')
ORDER BY [Year], [Month], StatType

END
