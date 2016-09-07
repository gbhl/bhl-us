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
AND		StatType = 'Titles Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Items Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Pages Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'PageNames Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @Year
AND		([Month] = @Month OR @Month = 0)
AND		StatType = 'Segments Created'
GROUP BY StatType

END
