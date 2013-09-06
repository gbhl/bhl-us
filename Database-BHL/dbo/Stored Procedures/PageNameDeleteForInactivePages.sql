
CREATE PROCEDURE [dbo].[PageNameDeleteForInactivePages]

AS

BEGIN

/*
 This procedure should be run on a nightly basis to assure that
 names associated with inactive items/pages do not appear in 
 search results.  

 Simply removing the names from the names table (as we do here)
 allows us to create a more efficient search query (no need to join
 to the Page and Item tables to determine if they are active).

 An alternate solution to this procedure would be to modify the
 administration site to update the name data as item and page
 statuses are edited.
 */

SET NOCOUNT ON

-- Get the inactive pages with names
SELECT DISTINCT	i.ItemID, p.PageID
INTO	#tmpPage
FROM	dbo.Item i INNER JOIN dbo.Page p
			ON i.ItemID = p.ItemID
		INNER JOIN dbo.NamePage n
			ON p.PageID = n.PageID
WHERE	ItemStatusID = 5
OR		p.Active = 0

-- Delete the names
DELETE	dbo.NamePage
FROM	dbo.NamePage n INNER JOIN #tmpPage p
			ON n.PageID = p.PageID

-- Set the LastPageNameUpdateDates to NULL for inactive pages and items.
-- (This assures that the names will be re-retrieved from UBIO if the 
-- items/pages become active in the future.)
UPDATE	dbo.Page
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage t INNER JOIN dbo.Page p
			ON t.PageID = p.PageID
WHERE	p.LastPageNameLookupDate IS NOT NULL

UPDATE	dbo.Item
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage t INNER JOIN dbo.Item i
			ON t.ItemID = i.ItemID
WHERE	i.ItemStatusID = 5
AND		i.LastPageNameLookupDate IS NOT NULL

END


