CREATE PROCEDURE [dbo].[NamePageDeleteByItemID]

@ItemID int

AS
--------------------------------------------------------------------------------------------------
-- Remove existing auto-added name records for an item, and reset the item/page name lookup dates.
--------------------------------------------------------------------------------------------------
BEGIN

SET NOCOUNT ON

-- 2024-09-25 Don't delete names here.  Reset name lookup dates and let the name refresh process perform the deletes. MWL
/*
DELETE	dbo.NamePage
FROM	dbo.NamePage np 
		INNER JOIN dbo.ItemPage ip ON np.PageID = ip.PageID
WHERE	ip.ItemID = @ItemID
AND		ISNULL(np.CreationUserID, 1) = 1
*/

UPDATE	p
SET		LastPageNameLookupDate = NULL 
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
WHERE	ip.ItemID = @ItemID

UPDATE dbo.Book SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID
UPDATE dbo.Segment SET LastPageNameLookupDate = NULL WHERE ItemID = @ItemID

END

GO
