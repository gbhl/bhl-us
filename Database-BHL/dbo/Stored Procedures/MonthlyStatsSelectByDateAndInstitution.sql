
CREATE PROCEDURE [dbo].[MonthlyStatsSelectByDateAndInstitution]

@StartYear int,
@StartMonth int,
@EndYear int,
@EndMonth int,
@InstitutionName nvarchar(255) = ''

AS
BEGIN

SET NOCOUNT ON

SELECT	StatType, [Year], [Month], SUM(StatValue) AS StatValue
--FROM	dbo.MonthlyStats
FROM	(
		SELECT	InstitutionName, StatType, Year, Month, StatValue FROM MonthlyStats WHERE StatType NOT LIKE '%Scanned'
		UNION
		-- Make sure we have at least a zero entry for every institution and stattype in every month
		SELECT	InstitutionName, StatType, Year, Month, 0
		FROM	(SELECT DISTINCT StatType FROM MonthlyStats WHERE StatType NOT LIKE '%Scanned') X
				CROSS JOIN
				(SELECT DISTINCT InstitutionName, Year, Month FROM MonthlyStats WHERE StatType NOT LIKE '%Scanned') Y
		) Z
WHERE	[Year] >= 2006
AND		StatType NOT LIKE '%Scanned%'
AND		([Year] > @StartYear OR ([Year] = @StartYear AND [Month] >= @StartMonth))
AND		([Year] < @EndYear OR ([Year] = @EndYear AND [Month] <= @EndMonth))
AND		(InstitutionName = @InstitutionName OR @InstitutionName = '')
GROUP BY
		StatType, [Year], [Month]
ORDER BY
		CASE StatType
		WHEN 'Titles Created' THEN 1
		WHEN 'Items Created' THEN 2
		WHEN 'Pages Created' THEN 3
		WHEN 'PageNames Created' THEN 4
		WHEN 'Segments Created' THEN 5
		END,
		[Year], [Month]

END

