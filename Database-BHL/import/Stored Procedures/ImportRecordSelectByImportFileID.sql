CREATE PROCEDURE [import].[ImportRecordSelectByImportFileID]

@ImportFileID int,
@NumRows int,
@StartRow int,
@SortColumn nvarchar(150) = 'Title',
@SortDirection nvarchar(4) = 'asc'

AS

BEGIN

SET NOCOUNT ON

IF @SortColumn = '' SET @SortColumn = 'Title'
IF @SortDirection <> 'asc' AND @SortDirection <> 'desc' SET @SortDirection = 'asc'

DECLARE @TotalRecords int
SELECT @TotalRecords = COUNT(*) FROM import.ImportRecord WHERE ImportFileID = @ImportFileID

DECLARE @SQL nvarchar(max)

CREATE TABLE #tmpRecord
	(
	RowNumber int NOT NULL,
	ImportRecordID int NOT NULL
	)

SET @SQL = 'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortDirection + 
			') AS RowNumber, ImportRecordID FROM import.ImportRecord WHERE ImportFileID = ' + 
			CONVERT(nvarchar(20), @ImportFileID)

INSERT #tmpRecord EXEC (@SQL)

SELECT TOP (@NumRows) 
		@TotalRecords AS TotalRecords,
		t.RowNumber,
		t.ImportRecordID,
		r.ImportRecordStatusID, 
		s.StatusName,
		CASE 
		WHEN g.SegmentGenreID IS NULL AND r.Genre NOT IN ('Book', 'BookJournal', 'Journal', 'Monograph', 'Serial') 
		THEN 'Article' 
		ELSE r.Genre END AS Genre,
		r.Title, 
		r.JournalTitle, 
		r.Volume, 
		r.Series, 
		r.Issue, 
		r.PublicationDetails,
		r.Year
FROM	#tmpRecord t INNER JOIN import.ImportRecord r 
			ON t.ImportRecordID = r.ImportRecordID
		LEFT JOIN dbo.SegmentGenre g
			ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s
			ON r.ImportRecordStatusID = s.ImportRecordStatusID
WHERE	t.RowNumber >= @StartRow
ORDER BY t.RowNumber

END

