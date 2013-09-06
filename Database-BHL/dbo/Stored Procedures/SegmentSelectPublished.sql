
CREATE PROCEDURE [dbo].[SegmentSelectPublished]

AS

BEGIN

SELECT DISTINCT
		s.SegmentID
FROM	dbo.Segment s LEFT JOIN dbo.SegmentPage p ON s.SegmentID = p.SegmentID
WHERE	SegmentStatusID IN (10, 20)
AND		(ItemID IS NOT NULL OR
		ISNULL(Url, '') <> '' OR		
		p.SegmentPageID IS NOT NULL)

END



