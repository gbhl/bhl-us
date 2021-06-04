CREATE PROCEDURE import.ImportRecordSelectForReviewByImportFileID

@ImportFileID int,
@NumRows int,
@StartRow int,
@SortColumn nvarchar(150) = 'Title',
@SortDirection nvarchar(4) = 'asc',
@Extended int = 0

AS

BEGIN

SET NOCOUNT ON

IF @SortColumn = '' SET @SortColumn = 'Title'
IF @SortDirection <> 'asc' AND @SortDirection <> 'desc' SET @SortDirection = 'asc'

DECLARE @TotalRecords int
SELECT @TotalRecords = COUNT(*) FROM import.ImportRecord WHERE ImportFileID = @ImportFileID

DECLARE @SQL nvarchar(max)

-- Get initial list of all ImportRecordIDs; include a row number in the list
CREATE TABLE #tmpRecord
	(
	RowNumber int NOT NULL,
	ImportRecordID int NOT NULL
	)

SET @SQL =	'WITH CTE (ImportRecordID, Title, JournalTitle, [Year], Volume, Issue, StartPage, Status) AS ' +
			'( ' +
			'SELECT r.ImportRecordID, r.Title, ' +
				't.FullTitle AS JournalTitle, ' + 
				'CASE ' + -- WHEN r.[Year] <> '''' THEN r.[Year] ' +
					'WHEN ISNULL(b.[StartYear], '''') <> '''' THEN b.[StartYear] ' +
					'ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '''') END AS [Year], ' +
				'b.StartVolume AS Volume, ' +
				'b.StartIssue AS Issue, ' + 
				--'CASE WHEN ISNULL(t.FullTitle, '''') <> '''' THEN t.FullTitle ' +
				--	'ELSE r.JournalTitle COLLATE SQL_Latin1_General_CP1_CI_AI END AS JournalTitle, ' +
				--'CASE WHEN r.Volume <> '''' THEN r.Volume ' +
				--	'ELSE ISNULL(b.StartVolume, '''') END AS Volume, ' +
				--'CASE WHEN r.Issue <> '''' THEN r.Issue ' +
				--	'ELSE ISNULL(b.StartIssue, '''') END AS Issue, ' +
				'CASE WHEN r.StartPage <> '''' THEN r.StartPage ' +
					'ELSE ISNULL(seg.StartPageNumber, '''') END AS StartPage, ' +
				's.StatusName AS Status ' +
			'FROM import.ImportRecord r  ' +
				'LEFT JOIN dbo.Book b ON r.ItemID = b.BookID ' +
				'LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID ' +
				'LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID ' + 
				'LEFT JOIN dbo.Segment seg ON r.ImportSegmentID = seg.SegmentID ' +
				'INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID ' +
			'WHERE ImportFileID = ' + CONVERT(nvarchar(20), @ImportFileID) +
			' ) ' +
			'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortDirection + ') AS RowNumber, ImportRecordID FROM CTE;'

INSERT #tmpRecord EXEC (@SQL)

-- Use the row number to extract the appropriate "page" of ImportRecordIDs
SELECT TOP (@NumRows) 
		tmp.RowNumber,
		tmp.ImportRecordID
INTO	#tmpRecordPage
FROM	#tmpRecord tmp 
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

-- Collect the data for each ImportRecordID and return it in the appropriate order

SELECT	@TotalRecords AS TotalRecords,
		tmp.RowNumber,
		tmp.ImportRecordID,
		r.SegmentID,
		r.ImportSegmentID,
		r.ImportRecordStatusID, 
		s.StatusName,
		-- New container info (NC = New Container)
		b.BookID AS NCItemID,
		t.FullTitle AS NCTitle,
		b.StartVolume AS NCVolume,
		b.StartSeries AS NCSeries,
		b.StartIssue AS NCIssue,
		CASE WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS NCYear,
		-- Existing container info (EC = Existing container)
		CAST(NULL AS int) AS ECItemID,
		-- New segment info (NS = New Segment)
		CASE WHEN g.SegmentGenreID IS NULL THEN 'Article' ELSE r.Genre END AS NSGenre,
		r.Title AS NSTitle, 
		r.JournalTitle AS NSJournalTitle,
		r.Volume AS NSVolume,
		r.Series AS NSSeries,
		r.Issue AS NSIssue,
		r.[Year] AS NSYear,
		r.StartPage AS NSStartPage,
		r.StartPageID AS NSStartPageID,
		r.EndPage AS NSEndPage,
		r.EndPageID AS NSEndPageID,
		-- Existing segment info (ES = existing segment)
		CAST(NULL AS nvarchar(2000)) AS ESTitle, 
		CAST(NULL AS nvarchar(20)) AS ESStartPage,
		CAST(NULL AS int) AS ESStartPageID,
		CAST(NULL AS nvarchar(20)) AS ESEndPage,
		CAST(NULL AS int) AS ESEndPageID
FROM	#tmpRecordPage tmp 
		INNER JOIN import.ImportRecord r ON tmp.ImportRecordID = r.ImportRecordID
		LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID
		LEFT JOIN dbo.Book b ON r.ItemID = b.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID
WHERE	ImportSegmentID IS NULL

UNION

SELECT	@TotalRecords AS TotalRecords,
		tmp.RowNumber,
		tmp.ImportRecordID,
		r.SegmentID,
		r.ImportSegmentID,
		r.ImportRecordStatusID, 
		s.StatusName,
		-- New container info (NC = New Container)
		b.BookID AS NCItemID,
		t.FullTitle AS NCTitle,
		b.StartVolume AS NCVolume,
		b.StartSeries AS NCSeries,
		b.StartIssue AS NCIssue,
		CASE WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS NCYear,
		-- Existing container info (EC = Existing container)
		eb.BookID AS ECItemID,
		-- New segment info (NS = New Segment)
		CASE WHEN g.SegmentGenreID IS NULL THEN 'Article' ELSE r.Genre END AS NSGenre,
		r.Title AS NSTitle, 
		r.JournalTitle AS NSJournalTitle,
		r.Volume AS NSVolume,
		r.Series AS NSSeries,
		r.Issue AS NSIssue,
		r.[Year] AS NSYear,
		r.StartPage AS NSStartPage,
		r.StartPageID AS NSStartPageID,
		r.EndPage AS NSEndPage,
		r.EndPageID AS NSEndPageID,
		-- Existing segment info (ES = existing segment)
		es.Title AS ESTitle, 
		es.StartPageNumber AS ESStartPage,
		es.StartPageID AS ESStartPageID,
		es.EndPageNumber AS ESEndPage,
		CAST(NULL AS int) AS ESEndPageID
FROM	#tmpRecordPage tmp 
		INNER JOIN import.ImportRecord r ON tmp.ImportRecordID = r.ImportRecordID
		LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID
		LEFT JOIN dbo.Book b ON r.ItemID = b.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID
		LEFT JOIN dbo.Segment es ON r.ImportSegmentID = es.SegmentID
		LEFT JOIN dbo.vwSegment vs ON r.ImportSegmentID = vs.SegmentID 
		LEFT JOIN dbo.SegmentGenre eg ON es.SegmentGenreID = eg.SegmentGenreID
		LEFT JOIN dbo.Book eb on vs.BookID = eb.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle ept ON eb.ItemID = ept.ItemID
		LEFT JOIN dbo.Title et ON ept.TitleID = et.TitleID
WHERE	ImportSegmentID IS NOT NULL
ORDER BY tmp.RowNumber

END

GO
