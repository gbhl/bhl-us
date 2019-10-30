CREATE PROCEDURE srchindex.SearchCatalogSegmentDeleteForItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

DELETE
FROM	dbo.SearchCatalogSegment
WHERE	ItemID = @ItemID

END
