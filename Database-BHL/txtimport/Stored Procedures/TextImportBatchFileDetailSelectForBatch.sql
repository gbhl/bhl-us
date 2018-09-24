CREATE PROCEDURE [txtimport].[TextImportBatchFileDetailSelectForBatch]

@TextImportBatchID int,
@NumRows int,
@StartRow int,
@SortColumn nvarchar(150) = 'Filename',
@SortDirection nvarchar(4) = 'asc'

AS

BEGIN

SET NOCOUNT ON

IF @SortColumn = '' SET @SortColumn = 'Filename'
IF @SortDirection <> 'asc' AND @SortDirection <> 'desc' SET @SortDirection = 'asc'

DECLARE @TotalFiles int
SELECT @TotalFiles = COUNT(*) FROM txtimport.TextImportBatchFile WHERE TextImportBatchID = @TextImportBatchID

DECLARE @SQL nvarchar(max)

CREATE TABLE #tmpFile
	(
	RowNumber int NOT NULL,
	TextImportBatchFileID int NOT NULL
	)

SET @SQL =	'SELECT	' + 
				'f.TextImportBatchFIleID, ' +
				'f.Filename, ' +
				's.StatusName ' + 
			'INTO #tmpSort ' +
			'FROM txtimport.TextImportBatchFile f ' +
				'INNER JOIN txtimport.TextImportBatchFileStatus s ON f.TextImportBatchFileStatusID = s.TextImportBatchFileStatusID ' +
			'WHERE TextImportBatchID = ' + CONVERT(nvarchar(20), @TextImportBatchID) + ';' +

			'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortDirection + ') AS RowNumber, ' +
			'TextImportBatchFileID ' +
			'FROM #tmpSort;'

INSERT #tmpFile EXEC (@SQL)

SELECT TOP (@NumRows) 
		@TotalFiles AS TotalFiles,
		tmp.RowNumber,
		tmp.TextImportBatchFileID,
		f.TextImportBatchFileStatusID,
		s.StatusName,
		f.[Filename],
		f.FileFormat,
		f.ItemID,
		ISNULL(t.FullTitle, '') AS Title,
		ISNULL(i.StartVolume, '')AS Volume,
		CASE WHEN ISNULL(i.[Year], '') <> '' THEN i.[Year]
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS [Year]
FROM	#tmpFile tmp 
		INNER JOIN txtimport.TextImportBatchFile f ON tmp.TextImportBatchFileID = f.TextImportBatchFileID
		INNER JOIN txtimport.TextImportBatchFileStatus s ON f.TextImportBatchFileStatusID = s.TextImportBatchFileStatusID
		LEFT JOIN dbo.Item i ON f.ItemID = i.ItemiD
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

END
