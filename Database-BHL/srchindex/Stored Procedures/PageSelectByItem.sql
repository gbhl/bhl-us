CREATE PROCEDURE srchindex.PageSelectByItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	PageID
FROM	dbo.[Page]
WHERE	ItemID = @ItemID

END
