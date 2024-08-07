SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetPageCountForSegment]
(
	@SegmentID int
)
RETURNS int
AS 

BEGIN
	DECLARE @PageCount int
	SET @PageCount = 0

	SELECT	@PageCount = COUNT(ItemPageID)
	FROM	dbo.Segment s
			INNER JOIN dbo.ItemPage ip on s.ItemID = ip.ItemID
	WHERE	s.SegmentID = @SegmentID

	RETURN COALESCE(@PageCount, 0)
END



GO
