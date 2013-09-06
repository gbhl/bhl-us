CREATE FUNCTION [dbo].[fnGetPageCountForSegment]
(
	@SegmentID int
)
RETURNS int
AS 

BEGIN
	DECLARE @PageCount int
	SET @PageCount = 0

	SELECT	@PageCount = COUNT(SegmentPageID)
	FROM	dbo.SegmentPage
	WHERE	SegmentID = @SegmentID

	RETURN COALESCE(@PageCount, 0)
END

