CREATE PROCEDURE srchindex.SearchCatalogSegmentDelete

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalogSegment
WHERE	SegmentID = @SegmentID

END
