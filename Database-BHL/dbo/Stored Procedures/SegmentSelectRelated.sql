CREATE PROCEDURE [dbo].[SegmentSelectRelated]

@SegmentID INT

AS

BEGIN

SET NOCOUNT ON

SELECT	scs2.SegmentClusterID,
		sct.SegmentClusterTypeID,
		sct.SegmentClusterTypeLabel,
		scs2.IsPrimary, 
		g.GenreName,
		st.ItemStatusName AS StatusName,
		ISNULL(l.LanguageName, '') AS LanguageName,
		s.SegmentID,
		s.ItemID,
		s.BookID,
		s.SequenceOrder,
		s.Title,
		s.ContainerTitle,
		s.Volume,
		s.Series,
		s.Issue,
		s.[Date],
		CASE
		WHEN s.PageRange <> '' THEN s.PageRange 
		WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
		WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
		ELSE s.EndPageNumber
		END AS PageRange,
		dbo.fnAuthorStringForSegment(s.SegmentID, '; ') AS Authors,
		REPLACE(scs.Authors, '|', ';') AS Authors
FROM	dbo.SegmentClusterSegment scs1 
		INNER JOIN dbo.SegmentClusterSegment scs2 ON scs1.SegmentClusterID = scs2.SegmentClusterID
		INNER JOIN dbo.SegmentCluster sc ON scs2.SegmentClusterID = sc.SegmentClusterID
		INNER JOIN dbo.SegmentClusterType sct ON sc.SegmentClusterTypeID = sct.SegmentClusterTypeID
		INNER JOIN dbo.vwSegment s ON scs2.SegmentID = s.SegmentID
		INNER JOIN dbo.ItemStatus st ON s.SegmentStatusID = st.ItemStatusID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	scs1.SegmentID = @SegmentID
AND		s.SegmentID <> @SegmentID
AND		s.SegmentStatusID IN (30, 40)
ORDER BY 
		sct.DisplaySequence, 
		s.Date, 
		s.ContainerTitle, 
		RIGHT(REPLICATE('0', 100) + s.Volume, 100), 
		RIGHT('0000000000' + s.StartPageNumber, 10)

END

GO
