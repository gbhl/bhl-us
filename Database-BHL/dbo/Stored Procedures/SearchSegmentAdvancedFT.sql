
CREATE PROCEDURE [dbo].[SearchSegmentAdvancedFT]

@Title nvarchar(2000) = '',
@ContainerTitle nvarchar(2000) = '',
@Author nvarchar(2000) = '',
@Date nvarchar(20) = '',
@Volume nvarchar(100) = '',
@Series nvarchar(100) = '',
@Issue nvarchar(100) = '',
@ReturnCount int = 100,
@SortBy nvarchar(50) = 'Rank',
@IncludeNoContent smallint = 0

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
	exec dbo.SearchSegment @Title, @ContainerTitle, @Author, @Date, 
		@Volume, @Series, @Issue, @ReturnCount, @SortBy
	RETURN
END

DECLARE @SearchTitle nvarchar(4000)
DECLARE @SearchContainerTitle nvarchar(4000)
DECLARE @SearchAuthor nvarchar(4000)
DECLARE @SearchDate nvarchar(4000)
DECLARE @SearchVolume nvarchar(4000)
DECLARE @SearchSeries nvarchar(4000)
DECLARE @SearchIssue nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @SearchTitle = dbo.fnGetFullTextSearchString(@Title)
SELECT @SearchContainerTitle = dbo.fnGetFullTextSearchString(@ContainerTitle)
SELECT @SearchAuthor = dbo.fnGetFullTextSearchString(@Author)
SELECT @SearchDate = dbo.fnGetFullTextSearchString(@Date)
SELECT @SearchVolume = dbo.fnGetFullTextSearchString(@Volume)
SELECT @SearchSeries = dbo.fnGetFullTextSearchString(@Series)
SELECT @SearchIssue = dbo.fnGetFullTextSearchString(@Issue)

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	ItemID int NULL,
	SegmentStatusID int NULL,
	GenreName nvarchar(50) NOT NULL DEFAULT(''),
	Title nvarchar(2000) NOT NULL DEFAULT(''),
	SortTitle nvarchar(2000) NOT NULL DEFAULT(''),
	ContainerTitle nvarchar(300) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	PublicationDetails nvarchar(400) NOT NULL DEFAULT(''),
	[Date] nvarchar(20) NOT NULL DEFAULT(''),
	StartPageID int NULL,
	Url nvarchar(200) NOT NULL DEFAULT(''),
	DownloadUrl nvarchar(200) NOT NULL DEFAULT(''),
	PageRange nvarchar(50) NOT NULL DEFAULT(''),
	StartPageNumber nvarchar(20) NOT NULL DEFAULT(''),
	EndPageNumber nvarchar(20) NOT NULL DEFAULT(''),
	Authors nvarchar(1024) NOT NULL DEFAULT(''),
	[RANK] int NULL
	)	

CREATE TABLE #tmpInitialResult
	(
	SegmentID int NOT NULL,
	TitleRank int NULL,
	AuthorRank int NULL
	)

-- Get initial result set, filtering by Title and/or Author
IF (@SearchTitle <> '"**"' AND @SearchAuthor <> '"**"')
BEGIN
	INSERT #tmpInitialResult (SegmentID, TitleRank, AuthorRank)
	SELECT	c.SegmentID,
			x.RANK,
			y.RANK
	FROM	CONTAINSTABLE(SearchCatalogSegment, (Title), @SearchTitle) x
			INNER JOIN CONTAINSTABLE(SearchCatalogSegment, (Authors), @SearchAuthor) y ON x.[KEY] = y.[KEY]
			INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
			INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
	AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL OR @IncludeNoContent = 1)
END

IF (@SearchTitle <> '"**"' AND @SearchAuthor = '"**"')
BEGIN
	INSERT #tmpInitialResult (SegmentID, TitleRank)
	SELECT	c.SegmentID,
			x.RANK
	FROM	CONTAINSTABLE(SearchCatalogSegment, (Title), @SearchTitle) x
			INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
			INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
	AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL OR @IncludeNoContent = 1)
END

IF (@SearchTitle = '"**"' AND @SearchAuthor <> '"**"')
BEGIN
	INSERT #tmpInitialResult (SegmentID, AuthorRank)
	SELECT	c.SegmentID,
			x.RANK
	FROM	CONTAINSTABLE(SearchCatalogSegment, (Authors), @SearchAuthor) x
			INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
			INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
	AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL OR @IncludeNoContent = 1)
END

-- Limit results by container title
SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, x.[RANK] AS ContainerTitleRank
INTO	#tmpLimitContainer
FROM	CONTAINSTABLE(SearchCatalogSegment, (ContainerTitle), @SearchContainerTitle) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN #tmpInitialResult t ON c.SegmentID = t.SegmentID
UNION
SELECT	SegmentID, TitleRank, AuthorRank, 0 AS ContainerTitleRank FROM #tmpInitialResult WHERE @SearchContainerTitle = '"**"'

-- Limit results by date
SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.ContainerTitleRank, x.[RANK] AS DateRank
INTO	#tmpLimitDate
FROM	CONTAINSTABLE(SearchCatalogSegment, ([Date]), @SearchDate) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN #tmpLimitContainer t ON c.SegmentID = t.SegmentID
UNION
SELECT	SegmentID, TitleRank, AuthorRank, ContainerTitleRank, 0 AS DateRank 
FROM	#tmpLimitContainer WHERE @SearchDate = '"**"'

-- Limit results by volume
SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.ContainerTitleRank, t.DateRank, x.[RANK] AS VolumeRank
INTO	#tmpLimitVolume
FROM	CONTAINSTABLE(SearchCatalogSegment, (Volume), @SearchVolume) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN #tmpLimitDate t ON c.SegmentID = t.SegmentID
UNION
SELECT	SegmentID, TitleRank, AuthorRank, ContainerTitleRank, DateRank, 0 AS VolumeRank 
FROM	#tmpLimitDate WHERE @SearchVolume = '"**"'

-- Limit results by series
SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.ContainerTitleRank, t.DateRank, t.VolumeRank, x.[RANK] AS SeriesRank
INTO	#tmpLimitSeries
FROM	CONTAINSTABLE(SearchCatalogSegment, (Series), @SearchSeries) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN #tmpLimitVolume t ON c.SegmentID = t.SegmentID
UNION
SELECT	SegmentID, TitleRank, AuthorRank, ContainerTitleRank, DateRank, VolumeRank, 0 AS SeriesRank 
FROM	#tmpLimitVolume WHERE @SearchSeries = '"**"'

-- Limit results by issue
SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.ContainerTitleRank, t.DateRank, t.VolumeRank, t.SeriesRank, x.[RANK] AS IssueRank
INTO	#tmpLimitFinal
FROM	CONTAINSTABLE(SearchCatalogSegment, (Issue), @SearchIssue) x
		INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
		INNER JOIN #tmpLimitSeries t ON c.SegmentID = t.SegmentID
UNION
SELECT	SegmentID, TitleRank, AuthorRank, ContainerTitleRank, DateRank, VolumeRank, SeriesRank, 0 AS IssueRank 
FROM	#tmpLimitSeries WHERE @SearchIssue = '"**"'

-- Get the rest of the segment details
INSERT	#tmpSegment
SELECT	s.SegmentID,
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
		REPLACE(scs.Authors, '|', ';') AS Authors,
		ISNULL(t.TitleRank, 0) + ISNULL(t.AuthorRank, 0) + ISNULL(t.ContainerTitleRank, 0) +
				ISNULL(t.DateRank, 0) -- + ISNULL(t.VolumeRank, 0) + ISNULL(t.SeriesRank, 0) + ISNULL(t.IssueRank, 0)
FROM	#tmpLimitFinal t INNER JOIN dbo.Segment s ON t.SegmentID = s.SegmentID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID

-- Return final result set
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

END






