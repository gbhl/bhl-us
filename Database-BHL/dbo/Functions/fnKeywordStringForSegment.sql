SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnKeywordStringForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	DECLARE @KeywordString nvarchar(1024)

	SELECT	@KeywordString = COALESCE(@KeywordString, '') + Keyword + '|'
	FROM	dbo.Keyword k 
			INNER JOIN dbo.ItemKeyword ik ON k.KeywordID = ik.KeywordID
			INNER JOIN dbo.Segment s ON ik.ItemID = s.ItemID
	WHERE	s.SegmentID = @SegmentID
	ORDER BY 
			Keyword ASC

	RETURN LTRIM(RTRIM(COALESCE(@KeywordString, '')))
END



GO
