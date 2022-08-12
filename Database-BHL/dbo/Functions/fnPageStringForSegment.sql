CREATE FUNCTION dbo.fnPageStringForSegment 
(
	@SegmentID int
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @PageString nvarchar(max)

	SELECT	@PageString = COALESCE(@PageString, '') + CONVERT(nvarchar(10), ip.PageID) + '|'
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
	WHERE	s.SegmentID = @SegmentID
	ORDER BY ip.SequenceOrder ASC

	SET @PageString = CASE WHEN LEN(@PageString) >= 1 THEN SUBSTRING(@PageString, 1, LEN(@PageString) - 1) ELSE @PageString END

	RETURN LTRIM(RTRIM(COALESCE(@PageString, '')))
END

GO
