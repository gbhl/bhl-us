
CREATE PROCEDURE [dbo].[SegmentSimpleSelectByAuthor]

@AuthorId	int

AS

SET NOCOUNT ON

SELECT
	s.SegmentID,
	s.Title,
	g.GenreName
FROM dbo.SegmentAuthor sa
	INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
	INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
WHERE 
	sa.AuthorID = @AuthorId
ORDER BY 
	s.SortTitle


