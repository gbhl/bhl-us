CREATE PROCEDURE [dbo].[SegmentSimpleSelectByAuthor]

@AuthorId int

AS

SET NOCOUNT ON

SELECT
	s.SegmentID,
	s.Title,
	s.Date,
	g.GenreName
FROM dbo.ItemAuthor sa
	INNER JOIN dbo.vwSegment s ON sa.ItemID = s.ItemID
	INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
WHERE 
	sa.AuthorID = @AuthorId
ORDER BY 
	s.SortTitle

GO
