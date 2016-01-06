CREATE PROCEDURE [dbo].[NamePageDeleteByItemID]

@ItemID int

AS
--------------------------------------------------------------------------------------------------
-- Remove existing auto-added name records for an item, and reset the item/page name lookup dates.
--------------------------------------------------------------------------------------------------
BEGIN

SET NOCOUNT ON

DELETE	dbo.NamePage
FROM	dbo.NamePage np INNER JOIN dbo.Page p ON np.PageID = p.PageID
WHERE	p.ItemID = @ItemID
AND		ISNULL(np.CreationUserID, 1) = 1

UPDATE dbo.Page SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID
UPDATE dbo.Item SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID

END

