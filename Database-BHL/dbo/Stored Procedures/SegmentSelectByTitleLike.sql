CREATE PROCEDURE [dbo].[SegmentSelectByTitleLike]

@Title nvarchar(2000),
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalSegments int OUTPUT

AS

BEGIN

SET NOCOUNT ON

-- Get the total number of segments
SELECT	@TotalSegments = COUNT(s.SegmentID)
FROM	dbo.vwSegment s 
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentStatusID IN (30, 40)  -- New, Published
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
AND		s.SortTitle LIKE @Title + '%'

CREATE TABLE #Segment (SegmentID int NOT NULL)

IF (@SortColumn = 'title')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID
	FROM		dbo.vwSegment s 
				INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
	AND			s.SortTitle LIKE @Title + '%'
	ORDER BY	s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID
	FROM		dbo.vwSegment s 
				INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
	AND			s.SortTitle LIKE @Title + '%'
	ORDER BY	scs.Authors, s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'year')
BEGIN
	INSERT		#Segment
	SELECT		s.SegmentID
	FROM		dbo.vwSegment s 
				INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
	WHERE		s.SegmentStatusID IN (30, 40)  -- New, Published
	AND			(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)
	AND			s.SortTitle LIKE @Title + '%'
	ORDER BY	s.Date, s.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

SELECT	s.SegmentID,
		ISNULL(s.BookID, 0) AS ItemID,
		b.IsVirtual AS BookIsVirtual,
		s.SegmentStatusID,
		st.ItemStatusName AS StatusName,
		s.SequenceOrder,
		pt.TitleID,
		scs.Contributors AS ContributorName,
		s.SegmentGenreID,
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
		s.Notes,
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
		s.LanguageCode,
		l.LanguageName,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		CAST(NULL AS DATETIME) AS ContributorCreationDate,
		CAST(NULL AS DATETIME) AS ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID,
		scs.Authors
INTO	#Final
FROM	#Segment tmp
		INNER JOIN dbo.vwSegment s ON tmp.SegmentID = s.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.ItemRelationship ir ON s.ItemID = ir.ChildID
		LEFT JOIN dbo.vwItemPrimaryTitle pt ON ir.ParentID = pt.ItemID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY Date, SortTitle OPTION (RECOMPILE)

END

GO
