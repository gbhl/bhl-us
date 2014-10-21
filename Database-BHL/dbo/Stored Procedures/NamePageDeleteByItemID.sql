CREATE PROCEDURE dbo.NamePageDeleteByItemID

@ItemID int

AS
---------------------------------------------------------------------------------------
-- Remove existing name records for an item, and reset the item/page name lookup dates.
---------------------------------------------------------------------------------------
BEGIN

DELETE	dbo.NamePage
FROM	dbo.NamePage np INNER JOIN dbo.Page p ON np.PageID = p.PageID
WHERE	p.ItemID = @ItemID

UPDATE dbo.Page SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID
UPDATE dbo.Item SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID

END
