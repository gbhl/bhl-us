CREATE PROCEDURE [dbo].[MonthlyStatsSelectByDateAndInstitution]

@StartYear int,
@StartMonth int,
@EndYear int,
@EndMonth int,
@InstitutionCode nvarchar(10) = ''

AS
BEGIN

SET NOCOUNT ON

-- Create a new table that will hold all possible year-month values
CREATE TABLE #Months (
	StatType nvarchar(100) NOT NULL,
	[Year] int NOT NULL,
	[Month] int NOT NULL
) 
 
DECLARE @Year INT 
SELECT @Year = MIN(Year) FROM dbo.MonthlyStats
DECLARE @MaxYear INT
SELECT @MaxYear = MAX(Year) FROM dbo.MonthlyStats
DECLARE @Month INT
SELECT @Month = MIN(Month) FROM dbo.MonthlyStats WHERE Year IN (SELECT MIN(Year) FROM dbo.MonthlyStats)
DECLARE @MaxMonth INT = 12 
 
WHILE @Year <= @MaxYear
BEGIN
	WHILE @Month <= @MaxMonth
	BEGIN
		INSERT INTO #Months VALUES ('Titles Created', @Year, @Month)
		INSERT INTO #Months VALUES ('Items Created', @Year, @Month)
		INSERT INTO #Months VALUES ('Pages Created', @Year, @Month)
		INSERT INTO #Months VALUES ('PageNames Created', @Year, @Month)
		INSERT INTO #Months VALUES ('Segments Created', @Year, @Month)
		SET @Month = @Month + 1
	END
	SET @Month = 1
	SET @Year = @Year + 1
END 

-- Return the final result set
SELECT	StatType, [Year], [Month], SUM(StatValue) AS StatValue
INTO	#Stats
FROM	(
		SELECT	InstitutionCode, StatType, StatLevel, Year, Month, StatValue FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned'
		UNION
		-- Make sure we have at least a zero entry for every institution and stattype in every month
		SELECT	InstitutionCode, StatType, StatLevel, Year, Month, 0
		FROM	(SELECT DISTINCT StatType, StatLevel FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned') X
				CROSS JOIN
				(SELECT DISTINCT InstitutionCode, Year, Month FROM dbo.MonthlyStats WITH (NOLOCK) WHERE StatType NOT LIKE '%Scanned') Y
		) Z
WHERE	[Year] >= 2006
AND		([Year] > @StartYear OR ([Year] = @StartYear AND [Month] >= @StartMonth))
AND		([Year] < @EndYear OR ([Year] = @EndYear AND [Month] <= @EndMonth))
AND		(InstitutionCode = @InstitutionCode OR 
		 (ISNULL(InstitutionCode, '') = @InstitutionCode AND StatLevel = 'Total'))
AND		(StatType NOT IN ('DOIs Created', 'PDFs Created'))
GROUP BY
		StatType, [Year], [Month]
UNION ALL
SELECT	StatType, [Year], [Month], 0 FROM #Months
WHERE	[Year] >= 2006
AND		([Year] > @StartYear OR ([Year] = @StartYear AND [Month] >= @StartMonth))
AND		([Year] < @EndYear OR ([Year] = @EndYear AND [Month] <= @EndMonth))
AND		(StatType NOT IN ('DOIs Created', 'PDFs Created'))

SELECT	StatType, [Year], [Month], SUM(StatValue) AS StatValue
FROM	#Stats
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
GO
