SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [import].[ImportRecordSelectByImportFileID]

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

CREATE TABLE #tmpRecord
	(
	RowNumber int NOT NULL,
	ImportRecordID int NOT NULL
	)

SET @SQL =	'SELECT	' + 
				'r.ImportRecordID, ' +
				'r.Title, ' +
				'CASE WHEN ISNULL(t.FullTitle, '''') <> '''' THEN t.FullTitle ' +
					'ELSE r.JournalTitle COLLATE SQL_Latin1_General_CP1_CI_AI END AS JournalTitle, ' +
				'CASE WHEN r.[Year] <> '''' THEN r.[Year] ' +
					'WHEN ISNULL(b.[StartYear], '''') <> '''' THEN b.[StartYear] ' +
					'ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '''') END AS [Year], ' +
				'CASE WHEN r.Volume <> '''' THEN r.Volume ' +
					'ELSE ISNULL(b.StartVolume, '''') END AS Volume, ' +
				'CASE WHEN r.Issue <> '''' THEN r.Issue ' +
					'ELSE ISNULL(b.StartIssue, '''') END AS Issue, ' +
				'r.StartPage, ' +
				's.StatusName AS Status ' +
			'INTO #tmpSort ' +
			'FROM import.ImportRecord r ' +
				'LEFT JOIN dbo.Book b ON r.ItemID = b.ItemID ' +
				'LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID ' +
				'LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID ' + 
				'INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID ' +
			'WHERE ImportFileID = ' + CONVERT(nvarchar(20), @ImportFileID) + ';' +

			'SELECT ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortDirection + ') AS RowNumber, ' +
			'ImportRecordID ' +
			'FROM #tmpSort;'

INSERT #tmpRecord EXEC (@SQL)

SELECT TOP (@NumRows) 
		@TotalRecords AS TotalRecords,
		tmp.RowNumber,
		tmp.ImportRecordID,
		b.BookID AS ItemID,
		r.SegmentID,
		r.ImportRecordStatusID, 
		s.StatusName,
		CASE WHEN g.SegmentGenreID IS NULL 
			THEN 'Article' 
			ELSE r.Genre 
			END AS Genre,
		r.Title, 
		r.TranslatedTitle,
		CASE WHEN ISNULL(t.FullTitle, '') <> ''
			THEN t.FullTitle
			ELSE r.JournalTitle COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS JournalTitle,
		CASE WHEN r.Volume <> '' 
			THEN r.Volume
			ELSE ISNULL(b.StartVolume, '')
			END AS Volume,
		CASE WHEN r.Series <> ''
			THEN r.Series
			ELSE ISNULL(b.StartSeries, '')
			END AS Series,
		CASE WHEN r.Issue <> ''
			THEN r.Issue
			ELSE ISNULL(b.StartIssue, '')
			END AS Issue,
		CASE WHEN r.Edition <> ''
			THEN r.Edition
			ELSE ISNULL(t.EditionStatement, '')
			END AS Edition,
		CASE WHEN ISNULL(t.PublicationDetails, '') <> ''
			THEN t.PublicationDetails
			ELSE r.PublicationDetails COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublicationDetails,
		CASE WHEN ISNULL(t.Datafield_260_b, '') <> ''
			THEN t.Datafield_260_b
			ELSE r.PublisherName COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublisherName,
		CASE WHEN ISNULL(t.Datafield_260_a, '') <> ''
			THEN t.Datafield_260_a
			ELSE r.PublisherPlace COLLATE SQL_Latin1_General_CP1_CI_AI
			END AS PublisherPlace,
		CASE WHEN r.[Year] <> '' THEN r.[Year]
			WHEN ISNULL(b.[StartYear], '') <> '' THEN b.[StartYear]
			ELSE ISNULL(CONVERT(nvarchar(20), t.StartYear), '')
			END AS [Year],
		r.StartYear,
		r.EndYear,
		ISNULL(r.Language, '') AS Language,
		CASE WHEN ISNULL(b.Rights, '') <> ''
			THEN b.Rights
			ELSE r.Rights
			END AS Rights,
		r.DueDiligence,
		CASE WHEN ISNULL(b.CopyrightStatus, '') <> ''
			THEN b.CopyrightStatus 
			ELSE r.CopyrightStatus 
			END AS CopyrightStatus,
		r.License,
		CASE WHEN ISNULL(b.LicenseURL, '') <> ''
			THEN b.LicenseUrl
			ELSE r.LicenseUrl
			END AS LicenseUrl,
		r.PageRange,
		r.StartPage,
		r.StartPageID,
		r.EndPage,
		r.EndPageID,
		r.Url,
		r.DownloadUrl,
		r.DOI,
		r.ISSN,
		r.ISBN,
		r.OCLC,
		r.LCCN,
		r.Summary,
		r.Notes,
		CASE WHEN @Extended = 1 THEN import.fnAuthorStringForImportRecord(r.ImportRecordID, '+++') ELSE '' END AS Authors,
		CASE WHEN @Extended = 1 THEN import.fnKeywordStringForImportRecord(r.ImportRecordID, '+++') ELSE '' END AS Keywords,
		CASE WHEN @Extended = 1 THEN import.fnErrorStringForImportRecord(r.ImportRecordID, 'Error', '+++') ELSE '' END AS Errors,
		CASE WHEN @Extended = 1 THEN import.fnErrorStringForImportRecord(r.ImportRecordID, 'Warning', '+++') ELSE '' END AS Warnings
FROM	#tmpRecord tmp 
		INNER JOIN import.ImportRecord r ON tmp.ImportRecordID = r.ImportRecordID
		LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		INNER JOIN import.ImportRecordStatus s ON r.ImportRecordStatusID = s.ImportRecordStatusID
		LEFT JOIN dbo.Book b ON r.ItemID = b.BookID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON b.ItemID = pt.ItemID
		LEFT JOIN dbo.Title t ON pt.TitleID = t.TitleID
WHERE	tmp.RowNumber >= @StartRow
ORDER BY tmp.RowNumber

END

GO
