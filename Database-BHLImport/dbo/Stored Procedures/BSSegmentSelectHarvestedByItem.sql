CREATE PROCEDURE dbo.BSSegmentSelectHarvestedByItem

@ItemID int = NULL

AS

BEGIN

SELECT	s.SegmentID
FROM	dbo.BSSegment s
		INNER JOIN dbo.BSSegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
WHERE	st.StatusName = 'Harvested'
AND		BHLSegmentID IS NULL
AND		s.ItemID = @ItemID


END
GO

