CREATE PROCEDURE [dbo].[MonthlyStatsSelectByDateAndInstitution]

@StartYear int,
@StartMonth int,
@EndYear int,
@EndMonth int,
@InstitutionCode nvarchar(10) = ''

AS
BEGIN

SET NOCOUNT ON

SELECT	StatType, [Year], [Month], SUM(StatValue) AS StatValue
FROM	(
		SELECT	InstitutionCode, StatType, Year, Month, StatValue FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned'
		UNION
		-- Make sure we have at least a zero entry for every institution and stattype in every month
		SELECT	InstitutionCode, StatType, Year, Month, 0
		FROM	(SELECT DISTINCT StatType FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned') X
				CROSS JOIN
				(SELECT DISTINCT InstitutionCode, Year, Month FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned') Y
		) Z
WHERE	[Year] >= 2006
AND		StatType NOT LIKE '%Scanned%'
AND		([Year] > @StartYear OR ([Year] = @StartYear AND [Month] >= @StartMonth))
AND		([Year] < @EndYear OR ([Year] = @EndYear AND [Month] <= @EndMonth))
AND		((InstitutionCode = @InstitutionCode AND StatType <> 'Titles Created') OR @InstitutionCode = '')
AND		(StatType NOT IN ('DOIs Created', 'PDFs Created'))
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
