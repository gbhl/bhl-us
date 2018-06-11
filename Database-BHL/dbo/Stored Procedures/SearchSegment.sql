CREATE PROCEDURE [dbo].[SearchSegment]

@Title nvarchar(2000) = '',
@ContainerTitle nvarchar(2000) = '',
@Author nvarchar(2000) = '',
@Date nvarchar(20) = '',
@Volume nvarchar(100) = '',
@Series nvarchar(100) = '',
@Issue nvarchar(100) = '',
@ReturnCount int = 100,
@SortBy nvarchar(50) = 'Title',
@IncludeNoContent smallint = 0

AS 
/*
 * This procedure returns only active segments.
 */
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	ItemID int NULL,
	SegmentStatusID int NOT NULL,
	GenreName nvarchar(50) NOT NULL,
	Title nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	ContainerTitle nvarchar(300) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	PublicationDetails nvarchar(400) NOT NULL,
	[Date] nvarchar(20) NOT NULL,
	StartPageID int NULL,
	Url nvarchar(200) NOT NULL,
	DownloadUrl nvarchar(200) NOT NULL,
	DOIName nvarchar(50) NOT NULL,
	PageRange nvarchar(50) NOT NULL,
	StartPageNumber nvarchar(20) NOT NULL,
	EndPageNumber nvarchar(20) NOT NULL,
	Authors nvarchar(2000) NOT NULL,
	SearchAuthors nvarchar(2000) NOT NULL
	)	

-- Get initial result set, filtering by Titles, Date, and Volume/Series/Issue
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
		ISNULL(d.DOIName, '') AS DOIName,
		s.PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		REPLACE(scs.Authors, '|', ';') AS Authors,
		scs.SearchAuthors
FROM	dbo.vwSegment s 
		INNER JOIN dbo.SegmentGenre g ON g.SegmentGenreID = s.SegmentGenreID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.DOI d 
			ON s.SegmentID = d.EntityID 
			AND d.DOIEntityTypeID = 40 -- segment
			AND d.DOIStatusID IN (100, 200)
WHERE	(s.Title LIKE (@Title + '%') OR @Title = '')
AND		(s.ContainerTitle LIKE (@ContainerTitle + '%') OR @ContainerTitle = '')
AND		(s.[Date] LIKE ('%' + @Date + '%') OR @Date = '')
AND		(s.Volume LIKE ('%' + @Volume + '%') OR @Volume = '')
AND		(s.Series LIKE ('%' + @Series+ '%') OR @Series = '')
AND		(s.Issue LIKE ('%' + @Issue + '%') OR @Issue = '')
AND		s.SegmentStatusID IN (10, 20) -- New, Published
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL OR @IncludeNoContent = 1)

-- Apply the author filter
SELECT	*
INTO	#tmpSegment2
FROM	#tmpSegment
WHERE	SearchAuthors LIKE ('%' + @Author + '%') OR @Author = ''

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
			DOIName,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors
	FROM	#tmpSegment2 
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
			DOIName,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors
	FROM	#tmpSegment2 
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
			DOIName,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			Authors
	FROM	#tmpSegment2 
	ORDER BY [Date], SortTitle
END

END
