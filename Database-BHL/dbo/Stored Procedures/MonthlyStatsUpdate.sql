CREATE PROCEDURE [dbo].[MonthlyStatsUpdate]
AS
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpMonthlyStats
	(
	[Year] int NULL,
	[Month] int NULL,
	[InstitutionCode] nvarchar(10) NULL,
	[StatType] nvarchar(100) NULL,
	[StatLevel] nvarchar(100) NULL,
	[StatValue] int NULL
	)

-- Items by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, i.CreationDate) AS [Year],
		DATEPART(month, i.CreationDate) AS [Month],
		ii.InstitutionCode,
		'Items Created',
		'Institution',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	i.ItemStatusID = 40
AND		r.InstitutionRoleName = 'Holding Institution'
GROUP BY
		DATEPART(year, i.CreationDate),
		DATEPART(month, i.CreationDate),
		ii.InstitutionCode

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, i.CreationDate) AS [Year],
		DATEPART(month, i.CreationDate) AS [Month],
		NULL,
		'Items Created',
		'MemberTotal',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
WHERE	i.ItemStatusID = 40
AND		r.InstitutionRoleName = 'Holding Institution'
AND		inst.BHLMemberLibrary = 1
GROUP BY
		DATEPART(year, i.CreationDate),
		DATEPART(month, i.CreationDate)

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, i.CreationDate) AS [Year],
		DATEPART(month, i.CreationDate) AS [Month],
		NULL,
		'Items Created',
		'Total',
		COUNT(*)
FROM	dbo.Item i
WHERE	i.ItemStatusID = 40
GROUP BY
		DATEPART(year, i.CreationDate),
		DATEPART(month, i.CreationDate)

-- Pages by institution
INSERT	#tmpMonthlyStats
SELECT 	DATEPART(year, p.CreationDate) AS [Year],
		DATEPART(month, p.CreationDate) AS [Month],
		ii.InstitutionCode,
		'Pages Created',
		'Institution',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
AND		r.InstitutionRoleName = 'Holding Institution'
GROUP BY DATEPART(year, p.CreationDate),
		DATEPART(month, p.CreationDate),
		ii.InstitutionCode

INSERT	#tmpMonthlyStats
SELECT 	DATEPART(year, p.CreationDate) AS [Year],
		DATEPART(month, p.CreationDate) AS [Month],
		NULL,
		'Pages Created',
		'MemberTotal',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
AND		r.InstitutionRoleName = 'Holding Institution'
AND		inst.BHLMemberLibrary = 1
GROUP BY DATEPART(year, p.CreationDate),
		DATEPART(month, p.CreationDate)

INSERT	#tmpMonthlyStats
SELECT 	DATEPART(year, p.CreationDate) AS [Year],
		DATEPART(month, p.CreationDate) AS [Month],
		NULL,
		'Pages Created',
		'Total',
		COUNT(*)
FROM	dbo.Item i
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
GROUP BY DATEPART(year, p.CreationDate),
		DATEPART(month, p.CreationDate)

-- Pagenames by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, np.CreationDate) AS [Year],
		DATEPART(month, np.CreationDate) AS [Month],
		ii.InstitutionCode,
		'PageNames Created',
		'Institution',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
		INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
AND		r.InstitutionRoleName = 'Holding Institution'
GROUP BY DATEPART(year, np.CreationDate),
		DATEPART(month, np.CreationDate),
		ii.InstitutionCode

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, np.CreationDate) AS [Year],
		DATEPART(month, np.CreationDate) AS [Month],
		NULL,
		'PageNames Created',
		'MemberTotal',
		COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
		INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
AND		r.InstitutionRoleName = 'Holding Institution'
AND		inst.BHLMemberLibrary = 1
GROUP BY DATEPART(year, np.CreationDate),
		DATEPART(month, np.CreationDate)

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, np.CreationDate) AS [Year],
		DATEPART(month, np.CreationDate) AS [Month],
		NULL,
		'PageNames Created',
		'Total',
		COUNT(*)
FROM	dbo.Item i
		INNER JOIN dbo.Page p ON i.itemid = p.itemid
		INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
WHERE	i.ItemStatusID = 40
AND		p.active = 1
GROUP BY DATEPART(year, np.CreationDate),
		DATEPART(month, np.CreationDate)

-- Segments by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, s.CreationDate) AS [Year],
		DATEPART(month, s.CreationDate) AS [Month],
		si.InstitutionCode,
		'Segments Created',
		'Institution',
		COUNT(*)
FROM	dbo.Segment s 
		INNER JOIN dbo.SegmentInstitution si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	SegmentStatusID IN (10, 20) -- New, Published
AND		r.InstitutionRoleName = 'Contributor'
GROUP BY
		DATEPART(year, s.CreationDate),
		DATEPART(month, s.CreationDate),
		si.InstitutionCode

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, s.CreationDate) AS [Year],
		DATEPART(month, s.CreationDate) AS [Month],
		NULL,
		'Segments Created',
		'MemberTotal',
		COUNT(*)
FROM	dbo.Segment s 
		INNER JOIN (
			SELECT DISTINCT si.SegmentID
			FROM	dbo.Segment s
					INNER JOIN dbo.SegmentInstitution si ON s.SegmentID = si.SegmentID
					INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
					INNER JOIN dbo.Institution inst ON si.InstitutionCode = inst.InstitutionCode
			WHERE	SegmentStatusID IN (10, 20) -- New, Published
			AND		r.InstitutionRoleName = 'Contributor'
			AND		inst.BHLMemberLibrary = 1
			) X on s.SegmentID = X.SegmentID
GROUP BY
		DATEPART(year, s.CreationDate),
		DATEPART(month, s.CreationDate)

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, s.CreationDate) AS [Year],
		DATEPART(month, s.CreationDate) AS [Month],
		NULL,
		'Segments Created',
		'Total',
		COUNT(*)
FROM	dbo.Segment s
WHERE	SegmentStatusID IN (10, 20) -- New, Published
GROUP BY
		DATEPART(year, s.CreationDate),
		DATEPART(month, s.CreationDate)

-- PDFs generated by month
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, CreationDate) AS [Year],
		DATEPART(month, CreationDate) AS [Month],
		NULL,
		'PDFs Created',
		'Total',
		COUNT(*)
FROM	dbo.PDF 
WHERE	PDFStatusID = 30
GROUP BY
		DATEPART(year, CreationDate),
		DATEPART(month, CreationDate)

-- DOIs assigned by month
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, CreationDate) AS [Year],
		DATEPART(month, CreationDate) AS [Month],
		NULL,
		'DOIs Created',
		'Total',
		COUNT(*)
FROM	dbo.DOI 
WHERE	DOIStatusID = 100
GROUP BY
		DATEPART(year, CreationDate),
		DATEPART(month, CreationDate)


UPDATE	#tmpMonthlyStats
SET		[Year] = ISNULL([Year], 0),
		[Month] = ISNULL([Month], 0)

TRUNCATE TABLE dbo.MonthlyStats

INSERT	dbo.MonthlyStats
		(
		[Year],
		[Month],
		InstitutionCode,
		StatType,
		StatLevel,
		StatValue,
		CreationDate,
		LastModifiedDate
		)
SELECT	[Year],
		[Month],
		InstitutionCode,
		StatType,
		StatLevel,
		StatValue,
		GETDATE(),
		GETDATE()
FROM	#tmpMonthlyStats

END
