CREATE FUNCTION [dbo].[fnAuthorIDStringForSegment]
(
	@SegmentID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(MAX)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	SELECT	@AuthorString = COALESCE(@AuthorString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE ';' END) + 
					CONVERT(NVARCHAR(20), x.AuthorID),
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			SELECT	MIN(ia.SequenceOrder) AS SequenceOrder, a.AuthorID
			FROM	dbo.Segment s
					INNER JOIN dbo.ItemAuthor ia ON s.ItemID = ia.ItemID
					INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
					INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
			WHERE	s.SegmentID = @SegmentID
			AND		a.IsActive = 1
			AND		n.IsPreferredName = 1
			GROUP BY a.AuthorID
			) x
	ORDER BY x.SequenceOrder, x.AuthorID

	RETURN COALESCE(@AuthorString, '')
END

GO
