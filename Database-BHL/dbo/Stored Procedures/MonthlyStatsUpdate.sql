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
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
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
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
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
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	i.ItemStatusID = 40
GROUP BY
		DATEPART(year, i.CreationDate),
		DATEPART(month, i.CreationDate)

-- Pages by institution
INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], InstitutionCode, 'Pages Created', 'Institution', COUNT(*)
FROM	(
		-- book pages
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month],
				ii.InstitutionCode
		FROM	dbo.Item i 
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID = 40
		AND		p.Active = 1
		AND		r.InstitutionRoleName = 'Holding Institution'
		UNION ALL
		-- segment pages (virtual issues)
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month],
				ii.InstitutionCode
		FROM	dbo.Item i 
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.Active = 1
		AND		r.InstitutionRoleName = 'Contributor'
		) X
GROUP BY [Year], [Month], InstitutionCode

INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], NULL, 'Pages Created', 'MemberTotal', COUNT(*)
FROM	(
		-- book pages
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month]
		FROM	dbo.Item i 
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID = 40
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Holding Institution'
		AND		inst.BHLMemberLibrary = 1
		UNION ALL
		-- segment pages (virtual issues)
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month]
		FROM	dbo.Item i 
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Contributor'
		AND		inst.BHLMemberLibrary = 1
		) X
GROUP BY [Year], [Month]

INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], NULL, 'Pages Created', 'Total', COUNT(*)
FROM	(
		-- book pages
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month]
		FROM	dbo.Item i
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID = 40
		AND		p.active = 1
		UNION ALL
		-- segment pages (virtual issues)
		SELECT 	DATEPART(year, p.CreationDate) AS [Year],
				DATEPART(month, p.CreationDate) AS [Month]
		FROM	dbo.Item i
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.active = 1
		) X
GROUP BY [Year], [Month]

-- Pagenames by institution
INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], InstitutionCode, 'PageNames Created', 'Institution', COUNT(*)
FROM	(
		-- book page names
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month],
				ii.InstitutionCode
		FROM	dbo.Item i 
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID = 40
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Holding Institution'
		UNION ALL
		-- segment page names (virtual issues)
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month],
				ii.InstitutionCode
		FROM	dbo.Item i 
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Contributor'
		) X
GROUP BY [Year], [Month], InstitutionCode

INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], NULL, 'PageNames Created', 'MemberTotal', COUNT(*)
FROM	(
		-- book page names
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month]
		FROM	dbo.Item i 
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID = 40
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Holding Institution'
		AND		inst.BHLMemberLibrary = 1
		UNION ALL
		-- segment page names (virtual issues)
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month]
		FROM	dbo.Item i 
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
				INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
				INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.active = 1
		AND		r.InstitutionRoleName = 'Contributor'
		AND		inst.BHLMemberLibrary = 1
		) X
GROUP BY [Year], [Month]

INSERT	#tmpMonthlyStats
SELECT	[Year], [Month], NULL, 'PageNames Created', 'Total', COUNT(*)
FROM	(
		-- book page names
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month]
		FROM	dbo.Item i
				INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID = 40
		AND		p.active = 1
		UNION ALL
		-- segment page names (virtual issues)
		SELECT	DATEPART(year, np.CreationDate) AS [Year],
				DATEPART(month, np.CreationDate) AS [Month]
		FROM	dbo.Item i
				INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
				INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
				INNER JOIN dbo.Page p ON ip.PageID = p.PageID
				INNER JOIN dbo.NamePage np ON p.pageid = np.pageid
		WHERE	i.ItemStatusID IN (30, 40)
		AND		p.active = 1
		) X
GROUP BY [Year], [Month]

-- Segments by institution
INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, s.CreationDate) AS [Year],
		DATEPART(month, s.CreationDate) AS [Month],
		si.InstitutionCode,
		'Segments Created',
		'Institution',
		COUNT(*)
FROM	dbo.Segment s 
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution si ON i.ItemID = si.ItemID
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	ItemStatusID IN (30, 40) -- New, Published
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
			SELECT DISTINCT s.SegmentID
			FROM	dbo.Segment s
					INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
					INNER JOIN dbo.ItemInstitution si ON i.ItemID = si.ItemID
					INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
					INNER JOIN dbo.Institution inst ON si.InstitutionCode = inst.InstitutionCode
			WHERE	ItemStatusID IN (30, 40) -- New, Published
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
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
WHERE	ItemStatusID IN (30, 40) -- New, Published
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
DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

INSERT	#tmpMonthlyStats
SELECT	DATEPART(year, CreationDate) AS [Year],
		DATEPART(month, CreationDate) AS [Month],
		NULL,
		'DOIs Created',
		'Total',
		COUNT(*)
FROM	(
		SELECT	CreationDate
		FROM	dbo.Title_Identifier
		WHERE	IdentifierID = @DOIIdentifierID
		AND		SUBSTRING(IdentifierValue, 1, 
						CASE WHEN CHARINDEX('/', IdentifierValue) > 0 
							THEN CHARINDEX('/', IdentifierValue) - 1 
							ELSE LEN(IdentifierValue) 
						END) IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 1)
		UNION ALL
		SELECT	CreationDate
		FROM	dbo.ItemIdentifier
		WHERE	IdentifierID = @DOIIdentifierID
		AND		SUBSTRING(IdentifierValue, 1, 
						CASE WHEN CHARINDEX('/', IdentifierValue) > 0 
							THEN CHARINDEX('/', IdentifierValue) - 1 
							ELSE LEN(IdentifierValue) 
						END) IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 1)
		) X
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

GO
