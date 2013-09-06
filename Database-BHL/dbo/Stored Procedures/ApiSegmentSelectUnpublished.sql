
CREATE PROCEDURE [dbo].[ApiSegmentSelectUnpublished]

AS

SET NOCOUNT ON

-- Get all titles that are unpublished and not redirected to another title
SELECT	SegmentID
FROM	dbo.Segment
WHERE	SegmentStatusID IN (30, 40)
ORDER BY SegmentID

