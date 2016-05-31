CREATE PROCEDURE [dbo].[MonthlyStatsSelectByStatType]

@StatType nvarchar(100),
@InstitutionName nvarchar(255) = '',
@ShowMonthly bit = 0

AS
BEGIN

SET NOCOUNT ON

SELECT	x.InstitutionName, 
		CASE WHEN @ShowMonthly = 1 THEN m.[Year] ELSE 0 END AS [Year],
		CASE WHEN @ShowMonthly = 1 THEN m.[Month] ELSE 0 END AS [Month],
		SUM(ISNULL(m.StatValue, 0)) AS StatValue
INTO	#tmpData
FROM	(SELECT DISTINCT InstitutionName FROM dbo.MonthlyStats) x
		LEFT JOIN dbo.MonthlyStats m
			ON x.InstitutionName = m.InstitutionName
			AND m.StatType = @StatType
WHERE	(x.InstitutionName = @InstitutionName OR @InstitutionName = '')
AND		((@StatType = 'Titles Created' AND x.InstitutionName = 'N/A') OR
		 (@StatType <> 'Titles Created' AND x.InstitutionName <> 'N/A'))
GROUP BY
		CASE WHEN @ShowMonthly = 1 THEN m.[Year] ELSE 0 END,
		CASE WHEN @ShowMonthly = 1 THEN m.[Month] ELSE 0 END,
		x.InstitutionName
		
SELECT	*
FROM	#tmpData
WHERE	[Year] IS NOT NULL
ORDER BY
		InstitutionName
		
END
