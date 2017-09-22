CREATE PROCEDURE [dbo].[MonthlyStatsSelectSummary]

@Year int,
@Month int = 0

AS

BEGIN

SET NOCOUNT ON

SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Items Created'
AND		StatLevel = 'Total'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Pages Created'
AND		StatLevel = 'Total'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'PageNames Created'
AND		StatLevel = 'Total'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Segments Created'
AND		StatLevel = 'Total'
GROUP BY StatType

END
