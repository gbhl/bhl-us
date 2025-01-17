CREATE PROCEDURE [dbo].[ReportSelectPermissionsTitles]

@TitleID int = NULL,
@NotKnown int = 0,
@InCopyright int = 1,
@NotProvided int = 0,
@NumRows int = 25,
@StartRow int = 1,
@SortColumn nvarchar(150) = 'TitleID',
@SortDirection nvarchar(4) = 'asc'

AS

BEGIN

SET NOCOUNT ON

IF @SortColumn = '' SET @SortColumn = 'TitleID'
IF @SortDirection <> 'asc' AND @SortDirection <> 'desc' SET @SortDirection = 'asc'
SET @SortColumn = @SortColumn + ' ' + @SortDirection + ',TitleID'
DECLARE @SQL nvarchar(max)

-- Get initial list of all TitleIDs; include a row number in the list
CREATE TABLE #tmpRecord
	(
	RowNumber int NOT NULL,
	TotalRecords int NOT NULL,
	TitleID int NOT NULL,
	NumNoKnownCopyright int NOT NULL,
	NumInCopyright int NOT NULL,
	NumNotProvided int NOT NULL,
	HasDocumentation int NOT NULL
	)

SET @SQL = 'WITH CTE AS (' + 
	'SELECT	t.TitleID, ' +
			't.FullTitle, ' +
			't.SortTitle, ' +
			't.HasMovingWall, ' +
			'SUM(CASE WHEN bk.CopyrightIndicator = ''No Known Copyright'' THEN 1 ELSE 0 END) AS NumNoKnownCopyright, ' +
			'SUM(CASE WHEN bk.CopyrightIndicator = ''In-copyright'' THEN 1 ELSE 0 END) AS NumInCopyright, ' +
			'SUM(CASE WHEN bk.CopyrightIndicator = ''Copyright not provided'' THEN 1 ELSE 0 END) AS NumNotProvided, ' +
			'MAX(CASE WHEN d.TitleDocumentID IS NULL THEN 0 ELSE 1 END) AS HasDocumentation ' +
	'FROM	dbo.Book bk ' +
			'INNER JOIN dbo.Item i ON bk.ItemID = i.ItemID ' +
			'INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID ' +
			'INNER JOIN dbo.Title t ON it.TitleID = t.TitleID ' +
			'LEFT JOIN dbo.TitleDocument d ON t.TitleID = d.TitleID ' +
	'WHERE	(t.TitleID = ' + CONVERT(VARCHAR(10), ISNULL(@TitleID, 0)) + ' OR ' + CASE WHEN @TitleID IS NULL THEN 'NULL' ELSE CONVERT(VARCHAR(10), @TitleID) END + ' IS NULL) ' +
	'AND	bk.CopyrightIndicator IN (''No Known Copyright'', ''In-copyright'', ''Copyright not provided'') ' +
	'AND	i.ItemStatusID = 40 ' +
	'AND	t.PublishReady = 1 ' +
	'GROUP BY t.TitleID, t.FullTitle, t.SortTitle, t.HasMovingWall ' +
	'), ' +
	'CTECount AS (SELECT COUNT(*) TotalRecords FROM CTE ' + 
		'WHERE NumNoKnownCopyright > 0 AND ' + CONVERT(varchar(1), @NotKnown) + '=1 ' + 
		'OR NumInCopyright > 0 AND ' + CONVERT(varchar(1), @InCopyright) + '=1 ' + 
		'OR NumNotProvided > 0 AND ' + CONVERT(varchar(1), @NotProvided) + '=1 ' +
		') ' +
	'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ') AS RowNumber, TotalRecords, TitleID, NumNoKnownCopyright, NumInCopyright, NumNotProvided, HasDocumentation ' +
	'FROM CTE CROSS JOIN CTECount ' + 
	'WHERE NumNoKnownCopyright > 0 AND ' + CONVERT(varchar(1), @NotKnown) + '=1 ' + 
	'OR NumInCopyright > 0 AND ' + CONVERT(varchar(1), @InCopyright) + '=1 ' + 
	'OR NumNotProvided > 0 AND ' + CONVERT(varchar(1), @NotProvided) + '=1;'

INSERT #tmpRecord EXEC (@SQL)

-- Use the row number to extract the appropriate "page" of ImportRecordIDs
SELECT TOP (@NumRows) 
		tmp.RowNumber,
		tmp.TotalRecords,
		tmp.TitleID,
		tmp.NumNoKnownCopyright,
		tmp.NumInCopyright,
		tmp.NumNotProvided,
		tmp.HasDocumentation
INTO	#tmpRecordPage
FROM	#tmpRecord tmp 
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

SELECT	tmp.TotalRecords,
		t.TitleID,
		t.FullTitle,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		t.StartYear,
		t.EndYear,
		t.HasMovingWall,
		tmp.NumNoKnownCopyright,
		tmp.NumInCopyright,
		tmp.NumNotProvided,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISSN') AS ISSN,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'OCLC') AS OCLC,
		tmp.HasDocumentation
FROM	#tmpRecordPage tmp
		INNER JOIN Title t ON tmp.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
GROUP BY 
		tmp.RowNumber,
		tmp.TotalRecords,
		t.TitleID,
		t.FullTitle,
		b.BibliographicLevelName,
		m.MaterialTypeLabel,
		t.StartYear,
		t.EndYear,
		t.HasMovingWall,
		tmp.NumNoKnownCopyright,
		tmp.NumInCopyright,
		tmp.NumNotProvided,
		tmp.HasDocumentation
ORDER BY
		tmp.RowNumber

END

GO
