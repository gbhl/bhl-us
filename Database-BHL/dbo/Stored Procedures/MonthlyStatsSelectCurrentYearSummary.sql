
CREATE PROCEDURE [dbo].[MonthlyStatsSelectCurrentYearSummary]

AS
BEGIN

SET NOCOUNT ON

DECLARE @CurrentYear int
SELECT @CurrentYear = DATEPART(YEAR, GETDATE())

SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		StatType = 'Titles Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		StatType = 'Items Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		StatType = 'Pages Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		StatType = 'PageNames Created'
GROUP BY StatType
UNION
SELECT	StatType, SUM(StatValue) AS 'StatValue'
FROM	dbo.MonthlyStats
WHERE	[Year] = @CurrentYear
AND		StatType = 'Segments Created'
GROUP BY StatType

END

