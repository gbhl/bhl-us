CREATE PROCEDURE srchindex.SegmentSelectByItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentID
FROM	dbo.Segment
WHERE	ItemID = @ItemID

END
