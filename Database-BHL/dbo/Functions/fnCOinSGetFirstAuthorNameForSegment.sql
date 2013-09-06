CREATE FUNCTION [dbo].[fnCOinSGetFirstAuthorNameForSegment] 
(
	@SegmentID int
)
RETURNS nvarchar(255)
AS 

BEGIN
	DECLARE @AuthorName nvarchar(255)
	SET @AuthorName = NULL

	SELECT TOP 1 @AuthorName = n.FullName
	FROM	dbo.SegmentAuthor sa 
			INNER JOIN dbo.Author a ON sa.AuthorID = a.AuthorID
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	sa.SegmentID = @SegmentID
	AND		a.IsActive = 1
	AND		n.IsPreferredName = 1
	ORDER BY sa.SequenceOrder, n.FullName

	RETURN LTRIM(RTRIM(COALESCE(@AuthorName, '')))
END

