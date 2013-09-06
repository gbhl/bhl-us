CREATE FUNCTION [dbo].[fnCOinSAuthorStringForSegment]
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
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) +  n.FullName,
			@CurrentRecord = @CurrentRecord + 1
	FROM	SegmentAuthor sa
			INNER JOIN Author a ON sa.AuthorID = a.AuthorID
			INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	sa.SegmentID = @SegmentID
	AND		a.IsActive = 1
	AND		n.IsPreferredName = 1
	ORDER BY sa.SequenceOrder, n.FullName

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END

