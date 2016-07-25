CREATE PROCEDURE [dbo].[ApiSegmentSelectRelated]

@SegmentID INT

AS

BEGIN

SET NOCOUNT ON

SELECT	g.GenreName,
		st.StatusName,
		ISNULL(l.LanguageName, '') AS LanguageName,
		s.SegmentID,
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
		END AS PageRange
FROM	dbo.SegmentClusterSegment scs1 
		INNER JOIN dbo.SegmentClusterSegment scs2 ON scs1.SegmentClusterID = scs2.SegmentClusterID
		INNER JOIN dbo.Segment s ON scs2.SegmentID = s.SegmentID
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
WHERE	scs1.SegmentID = @SegmentID
AND		s.SegmentID <> @SegmentID

END
