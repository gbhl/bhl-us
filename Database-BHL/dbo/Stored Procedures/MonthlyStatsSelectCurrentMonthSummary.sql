
CREATE PROCEDURE [dbo].[MonthlyStatsSelectCurrentMonthSummary]

AS
BEGIN

SET NOCOUNT ON

DECLARE @CurrentYear int
DECLARE @CurrentMonth int
SELECT @CurrentYear = DATEPART(YEAR, GETDATE())
SELECT @CurrentMonth = DATEPART(MONTH, GETDATE())

SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		[Month] = @CurrentMonth
AND		StatType = 'Titles Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		[Month] = @CurrentMonth
AND		StatType = 'Items Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		[Month] = @CurrentMonth
AND		StatType = 'Pages Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		[Month] = @CurrentMonth
AND		StatType = 'PageNames Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		[Month] = @CurrentMonth
AND		StatType = 'Segments Created'
GROUP BY StatType

END
