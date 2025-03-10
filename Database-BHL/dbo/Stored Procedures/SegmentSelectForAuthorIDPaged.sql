﻿CREATE PROCEDURE [dbo].[SegmentSelectForAuthorIDPaged]

@AuthorId	int,
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalSegments int OUTPUT

AS

BEGIN

SET NOCOUNT ON

-- Get the total number of segments for the author
SELECT	@TotalSegments = COUNT(s.SegmentID)
FROM	dbo.vwSegment s WITH (NOLOCK)
		INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON s.ItemID = ia.ItemID
		INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
WHERE	s.SegmentStatusID IN (30, 40)  -- New, Published
AND		ia.AuthorID = @AuthorID
AND		(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)

CREATE TABLE #Segment (SegmentID int NOT NULL, AuthorID int NOT NULL)

IF (@SortColumn = 'title')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID, ia.AuthorID
	FROM		dbo.vwSegment s WITH (NOLOCK)
				INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON s.ItemID = ia.ItemID
				INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			ia.AuthorID = @AuthorID
	AND			(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)
	ORDER BY	s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID, ia.AuthorID
	FROM		dbo.vwSegment s WITH (NOLOCK)
				INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON s.ItemID = ia.ItemID
				INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			ia.AuthorID = @AuthorID
	AND			(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)
	ORDER BY	c.Authors, s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'year')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID, ia.AuthorID
	FROM		dbo.vwSegment s WITH (NOLOCK)
				INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON s.ItemID = ia.ItemID
				INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			ia.AuthorID = @AuthorID
	AND			(c.HasLocalContent = 1 OR c.HasExternalContent = 1 OR c.ItemID IS NOT NULL)
	ORDER BY	s.Date, s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

SELECT	s.SegmentID,
		ISNULL(s.BookID, 0) AS ItemID,
		b.IsVirtual AS BookIsVirtual,
		scs.Contributors AS ContributorName,
		g.GenreName,
		s.Title,
		s.SortTitle,
		s.TranslatedTitle,
		s.ContainerTitle,
		s.ContainerTitlePartNumber,
		s.ContainerTitlePartName,
		s.PublicationDetails,
		s.PublisherName,
		s.PublisherPlace,
		s.Volume,
		s.Series,
		s.Issue,
		s.Date,
		CASE
		WHEN s.PageRange <> '' THEN s.PageRange 
		WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
		WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
		ELSE s.EndPageNumber
		END AS PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		ISNULL(l.LanguageName, '') AS LanguageName,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		scs.Authors
INTO	#Final
FROM	#Segment t
		INNER JOIN dbo.vwSegment s ON t.SegmentID = s.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY Date, SortTitle OPTION (RECOMPILE)

END

GO
