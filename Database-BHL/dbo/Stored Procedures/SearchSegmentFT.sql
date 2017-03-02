CREATE PROCEDURE [dbo].[SearchSegmentFT]

@SearchText nvarchar(2000) = '',
@ReturnCount int = 100,
@SortBy nvarchar(50) = 'Title'

AS 
/*
 * This procedure returns only active segments.
 */
BEGIN

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.SearchSegment @SearchText, '', '', '', '', '', '', @ReturnCount, @SortBy
	RETURN
END

DECLARE @Search nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @Search = dbo.fnGetFullTextSearchString(@SearchText)

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	ItemID int NULL,
	SegmentStatusID int NOT NULL,
	GenreName nvarchar(50) NOT NULL,
	Title nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	ContainerTitle nvarchar(2000) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	PublicationDetails nvarchar(400) NOT NULL,
	[Date] nvarchar(20) NOT NULL,
	StartPageID int NULL,
	Url nvarchar(200) NOT NULL,
	DownloadUrl nvarchar(200) NOT NULL,
	PageRange nvarchar(50) NOT NULL,
	StartPageNumber nvarchar(20) NOT NULL,
	EndPageNumber nvarchar(20) NOT NULL,
	Authors nvarchar(2000) NOT NULL,
	HasLocalContent smallint NULL,
	[RANK] int NULL
	)	

-- Get initial result set
INSERT #tmpSegment
SELECT DISTINCT 
		s.SegmentID,
		s.ItemID,
		s.SegmentStatusID,
		g.GenreName,
		s.Title,
		s.SortTitle,
		s.ContainerTitle,
		s.Volume,
		s.PublicationDetails,
		s.[Date],
		s.StartPageID,
		s.Url,
		s.DownloadUrl,
		s.PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		REPLACE(c.Authors, '|', ';') AS Authors,
		c.HasLocalContent,
		x.[RANK]
FROM	CONTAINSTABLE(SearchCatalogSegment, (SearchText), @Search) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)

-- De-emphasize ranking of any segments:
--	1) Without local content
UPDATE	#tmpSegment
SET		[Rank] = [Rank] / 100.00
WHERE	HasLocalContent = 0

-- Return final result set
IF (@SortBy = 'Title')
BEGIN
	SELECT TOP (@ReturnCount)
			SegmentID,
			ItemID,
			SegmentStatusID,
			GenreName,
			Title,
			SortTitle,
			ContainerTitle,
			Volume,
			PublicationDetails,
			[Date],
			StartPageID,
			Url,
			DownloadUrl,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors,
			[RANK]
	FROM	#tmpSegment 
	ORDER BY SortTitle, [Date]
END
IF (@SortBy = 'Author')
BEGIN
	SELECT TOP (@ReturnCount)
			SegmentID,
			ItemID,
			SegmentStatusID,
			GenreName,
			Title,
			SortTitle,
			ContainerTitle,
			Volume,
			PublicationDetails,
			[Date],
			StartPageID,
			Url,
			DownloadUrl,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors,
			[RANK]
	FROM	#tmpSegment 
	ORDER BY Authors, SortTitle
END
IF (@SortBy = 'Date')
BEGIN
	SELECT TOP (@ReturnCount)
			SegmentID,
			ItemID,
			SegmentStatusID,
			GenreName,
			Title,
			SortTitle,
			ContainerTitle,
			Volume,
			PublicationDetails,
			[Date],
			StartPageID,
			Url,
			DownloadUrl,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors,
			[RANK]
	FROM	#tmpSegment 
	ORDER BY [Date], SortTitle
END
IF (@SortBy = 'Rank')
BEGIN
	SELECT TOP (@ReturnCount)
			SegmentID,
			ItemID,
			SegmentStatusID,
			GenreName,
			Title,
			SortTitle,
			ContainerTitle,
			Volume,
			PublicationDetails,
			[Date],
			StartPageID,
			Url,
			DownloadUrl,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors,
			[RANK]
	FROM	#tmpSegment 
	ORDER BY [RANK] DESC, SortTitle, [Date]
END

END
