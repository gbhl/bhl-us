
CREATE PROCEDURE [dbo].[MonthlyStatsUpdate]
AS
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpMonthlyStats
	(
	[Year] int NULL,
	[Month] int NULL,
	[InstitutionName] nvarchar(255) NULL,
	[StatType] nvarchar(100) NULL,
	[StatValue] int NULL
	)

-- Titles by institution
INSERT	#tmpMonthlyStats
SELECT	[Year], 
		[Month], 
		InstitutionName, 
		'Titles Created', 
		COUNT(*)
FROM	(
		SELECT DISTINCT 
			n.institutionname, 
			t.titleid,
			DATEPART(year, t.CreationDate) AS [Year],
			DATEPART(month, t.CreationDate) AS [Month]
		FROM dbo.Title t INNER JOIN dbo.Institution n
				ON t.institutioncode = n.institutioncode
		WHERE t.publishready = 1
		) x
GROUP BY [Year], [Month], InstitutionName
		
-- Items by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, i.CreationDate) AS [Year],
		DATEPART(month, i.CreationDate) AS [Month],
		n.InstitutionName,
		'Items Created',
		COUNT(*)
FROM	dbo.Item i INNER JOIN dbo.Institution n
			ON i.institutioncode = n.institutioncode
WHERE	i.ItemStatusID = 40
GROUP BY
		DATEPART(year, i.CreationDate),
		DATEPART(month, i.CreationDate),
		n.InstitutionName

-- Items scanned by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, i.ScanningDate) AS [Year],
		DATEPART(month, i.ScanningDate) AS [Month],
		n.InstitutionName,
		'Items Scanned',
		COUNT(*)
FROM	dbo.Item i INNER JOIN dbo.Institution n
			ON i.institutioncode = n.institutioncode
WHERE	i.ItemStatusID = 40
GROUP BY
		DATEPART(year, i.ScanningDate),
		DATEPART(month, i.ScanningDate),
		n.InstitutionName

-- Pages by institution
INSERT	#tmpMonthlyStats
SELECT 	DATEPART(year, p.CreationDate) AS [Year],
		DATEPART(month, p.CreationDate) AS [Month],
		n.institutionname, 
		'Pages Created',
		COUNT(*)
FROM	dbo.Item i INNER JOIN dbo.Institution n
			ON i.institutioncode = n.institutioncode
		INNER JOIN dbo.Page p
			ON i.itemid = p.itemid
WHERE	itemstatusid = 40
AND		p.active = 1
GROUP BY DATEPART(year, p.CreationDate),
		DATEPART(month, p.CreationDate),
		n.institutionname

-- Pagenames by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, np.CreationDate) AS [Year],
		DATEPART(month, np.CreationDate) AS [Month],
		n.institutionname, 
		'PageNames Created',
		COUNT(*)
FROM	dbo.Item i INNER JOIN dbo.Institution n
			ON i.institutioncode = n.institutioncode
		INNER JOIN dbo.Page p
			ON i.itemid = p.itemid
		INNER JOIN dbo.NamePage np
			ON p.pageid = np.pageid
WHERE	itemstatusid = 40
AND		p.active = 1
GROUP BY DATEPART(year, np.CreationDate),
		DATEPART(month, np.CreationDate),
		n.institutionname

-- Segments by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, s.CreationDate) AS [Year],
		DATEPART(month, s.CreationDate) AS [Month],
		inst.InstitutionName,
		'Segments Created',
		COUNT(*)
FROM	dbo.Segment s INNER JOIN dbo.Institution inst
			ON s.ContributorCode = inst.InstitutionCode
WHERE	SegmentStatusID IN (10, 20) -- New, Published
GROUP BY
		DATEPART(year, s.CreationDate),
		DATEPART(month, s.CreationDate),
		inst.InstitutionName


UPDATE	#tmpMonthlyStats
SET		[Year] = ISNULL([Year], 0),
		[Month] = ISNULL([Month], 0),
		InstitutionName = ISNULL(InstitutionName, '')

TRUNCATE TABLE dbo.MonthlyStats

INSERT	dbo.MonthlyStats
SELECT	[Year],
		[Month],
		InstitutionName,
		StatType,
		StatValue,
		GETDATE(),
		GETDATE()
FROM	#tmpMonthlyStats

END


