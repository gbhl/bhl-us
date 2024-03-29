SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		f.ErrorMessage,
		f.[Filename],
		f.FileFormat,
		f.ItemID,
		ISNULL(COALESCE(t.FullTitle, seg.Title COLLATE SQL_Latin1_General_CP1_CI_AI), '') AS Title,
		ISNULL(COALESCE(b.StartVolume, seg.Volume), '') AS Volume,
		ISNULL(COALESCE(CASE WHEN ISNULL(b.[StartYear], '') <> '' THEN b.[StartYear]
			ELSE CONVERT(nvarchar(20), t.StartYear)
			END, seg.Date), '') AS [Year]
FROM	#tmpFile tmp 
		INNER JOIN txtimport.TextImportBatchFile f ON tmp.TextImportBatchFileID = f.TextImportBatchFileID
		INNER JOIN txtimport.TextImportBatchFileStatus s ON f.TextImportBatchFileStatusID = s.TextImportBatchFileStatusID
		LEFT JOIN dbo.Book b ON f.ItemID = b.ItemID
		LEFT JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.Segment seg ON f.ItemID = seg.ItemID
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

END

GO
