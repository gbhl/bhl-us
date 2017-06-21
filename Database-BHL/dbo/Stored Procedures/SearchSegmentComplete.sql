CREATE PROCEDURE [dbo].[SearchSegmentComplete]

@Title nvarchar(2000)

AS 
/*
 * This rows returned by this procedure differ from the rows returned by
 * the SearchSegment procedure.  SearchSegment is used for the public-facing
 * site search, and only returns active segments.  
 * This procedure is used for admin functionality, and returns all
 * segments.
 */
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	SegmentStatusID int NOT NULL,
	ItemID int NULL,
	GenreName nvarchar(50) NOT NULL,
	Title nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	ContainerTitle nvarchar(300) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	PublicationDetails nvarchar(400) NOT NULL,
	[Date] nvarchar(20) NOT NULL,
	PageRange nvarchar(50) NOT NULL,
	Authors nvarchar(1024) NOT NULL
	)	

-- Get current status of the search catalog (Status 0 = offline)
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus

-- Only try the search catalog if it is online
IF (@CatalogStatus <> 0)
BEGIN
	-- Transform the search term into a full-text search phrase
	DECLARE @SearchCondition nvarchar(4000)
	SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@Title)

	INSERT #tmpSegment
	SELECT DISTINCT TOP (500)
			s.SegmentID,
			s.SegmentStatusID,
			s.ItemID,
			g.GenreName,
			s.Title,
			s.SortTitle,
			s.ContainerTitle,
			s.Volume,
			s.PublicationDetails,
			s.[Date],
			s.PageRange,
			REPLACE(c.Authors, '|', ';') AS Authors
	FROM	CONTAINSTABLE(SearchCatalogSegment, Title, @SearchCondition) x
			INNER JOIN SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
			INNER JOIN dbo.vwSegment s ON c.SegmentID = s.SegmentID
			INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
END

-- Now try searching without the catalog.  Newly added segments may not be in the catalog,
-- so that's why we may pick up additional rows by looking at the segment tables directly.
SELECT DISTINCT
		SegmentID
INTO	#tmpSegmentID
FROM	dbo.Segment
WHERE	Title LIKE (@Title + '%')

INSERT #tmpSegment
SELECT DISTINCT TOP (500)
		s.SegmentID,
		s.SegmentStatusID,
		s.ItemID,
		g.GenreName,
		s.Title,
		s.SortTitle,
		s.ContainerTitle,
		s.Volume,
		s.PublicationDetails,
		s.[Date],
		s.PageRange,
		REPLACE(scs.Authors, '|', ' ') AS Authors
FROM	dbo.vwSegment s 
		LEFT JOIN #tmpSegment t ON s.SegmentID = t.SegmentID
		INNER JOIN dbo.SegmentGenre g ON g.SegmentGenreID = s.SegmentGenreID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentID IN (SELECT SegmentID FROM #tmpSegmentID)
AND		t.SegmentID IS NULL

-- Return final result set
SELECT 	SegmentID,
		SegmentStatusID,
		ItemID,
		GenreName,
		Title,
		SortTitle,
		ContainerTitle,
		Volume,
		PublicationDetails,
		[Date],
		PageRange,
		Authors
FROM	#tmpSegment
ORDER BY SortTitle

END
