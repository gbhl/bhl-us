CREATE FUNCTION [dbo].[fnKeywordStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	DECLARE @KeywordString nvarchar(1024)

	SELECT	@KeywordString = COALESCE(@KeywordString, '') + Keyword + '|'
	FROM	dbo.Keyword k INNER JOIN dbo.SegmentKeyword sk
				ON k.KeywordID = sk.KeywordID
	WHERE	sk.SegmentID = @SegmentID
	ORDER BY 
			Keyword ASC

	RETURN LTRIM(RTRIM(COALESCE(@KeywordString, '')))
END

