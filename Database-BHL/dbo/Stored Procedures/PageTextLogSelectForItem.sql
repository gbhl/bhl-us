CREATE PROCEDURE dbo.PageTextLogSelectForItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	[PageTextLogID],
		l.[PageID],
		[TextSource],
		[TextImportBatchFileID],
		l.[CreationDate],
		l.[CreationUserID]
FROM	dbo.PageTextLog l
		INNER JOIN dbo.Page p ON l.PageID = p.PageID
WHERE	p.ItemID = @ItemID

END
