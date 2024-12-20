CREATE PROCEDURE [dbo].[ReportSelectPermissionsTitles]

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
print @sortcolumn
DECLARE @SQL nvarchar(max)

-- Get initial list of all TitleIDs; include a row number in the list
CREATE TABLE #tmpRecord
	(
	RowNumber int NOT NULL,
	TotalRecords int NOT NULL,
	TitleID int NOT NULL
	)

SET @SQL = 'WITH CTE AS (' + 
	'SELECT	t.TitleID, ' +
			't.FullTitle, ' +
			't.SortTitle, ' +
			't.HasMovingWall ' +
	'FROM	dbo.Book bk ' +
			'INNER JOIN dbo.Item i ON bk.ItemID = i.ItemID ' +
			'INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID ' +
			'INNER JOIN dbo.Title t ON it.TitleID = t.TitleID ' +
	'WHERE	bk.CopyrightIndicator IN (''No Known Copyright'', ''In-copyright'', ''Copyright not provided'') ' +
	'AND	i.ItemStatusID = 40 ' +
	'AND	t.PublishReady = 1 ' +
	'GROUP BY t.TitleID, t.FullTitle, t.SortTitle, t.HasMovingWall ' +
	'), ' +
	'CTECount AS (SELECT COUNT(*) TotalRecords FROM CTE) ' +
	'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ') AS RowNumber, TotalRecords, TitleID FROM CTE CROSS JOIN CTECount;'

INSERT #tmpRecord EXEC (@SQL)

-- Use the row number to extract the appropriate "page" of ImportRecordIDs
SELECT TOP (@NumRows) 
		tmp.RowNumber,
		tmp.TotalRecords,
		tmp.TitleID
INTO	#tmpRecordPage
FROM	#tmpRecord tmp 
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

SELECT	tmp.TotalRecords,
		t.TitleID,
		t.FullTitle,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		t.StartYear,
		t.EndYear,
		t.HasMovingWall,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISSN') AS ISSN,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'OCLC') AS OCLC,
		MAX(CASE WHEN d.TitleDocumentID IS NULL THEN 0 ELSE 1 END) AS HasDocumentation
FROM	#tmpRecordPage tmp
		INNER JOIN Title t ON tmp.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.TitleDocument d ON t.TitleID = d.TitleID
GROUP BY 
		tmp.RowNumber,
		tmp.TotalRecords,
		t.TitleID,
		t.FullTitle,
		b.BibliographicLevelName,
		t.StartYear,
		t.EndYear,
		t.HasMovingWall,
		d.TitleDocumentID
ORDER BY
		tmp.RowNumber

END

GO
