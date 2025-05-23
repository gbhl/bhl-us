SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.PageNameDeleteForInactivePages

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

-- Get the Inactive pages with names
SELECT DISTINCT	p.PageID
INTO	#tmpPage
FROM	dbo.Page p
		INNER JOIN dbo.NamePage n ON p.PageID = n.PageID
WHERE	p.Active = 0

-- Delete the names
DELETE	dbo.NamePage
FROM	dbo.NamePage n INNER JOIN #tmpPage p
			ON n.PageID = p.PageID

-- Set the LastPageNameUpdateDates to NULL for inactive pages.
-- (This assures that the pages will be re-submitted to a name-finding 
-- service if the items/pages become active in the future.)
UPDATE	dbo.Page
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage t INNER JOIN dbo.Page p
			ON t.PageID = p.PageID
WHERE	p.LastPageNameLookupDate IS NOT NULL

DROP TABLE #tmpPage

----------------------

-- Get the pages with names that are associated with inactive items
SELECT DISTINCT	i.ItemID, ip.PageID
INTO	#tmpPage2
FROM	dbo.Item i 
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		INNER JOIN dbo.NamePage n ON ip.PageID = n.PageID
WHERE	i.ItemStatusID NOT IN (30, 40)

-- Omit pages that are also associated with active items
DELETE #tmpPage2
WHERE PageID IN (
	SELECT DISTINCT t.PageID
	FROM	#tmpPage2 t
			INNER JOIN dbo.ItemPage ip ON t.PageID = ip.PageID
			INNER JOIN dbo.Item i on ip.ItemID = i.ItemID
	WHERE	i.ItemStatusID IN (30, 40)  
	)

-- Delete the names
DELETE	dbo.NamePage
FROM	dbo.NamePage n INNER JOIN #tmpPage2 p
			ON n.PageID = p.PageID

-- Set the LastPageNameUpdateDates to NULL for inactive pages.
-- (This assures that the pages will be re-submitted to a name-finding 
-- service if the items/pages become active in the future.)
UPDATE	dbo.Page
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage2 t INNER JOIN dbo.Page p
			ON t.PageID = p.PageID
WHERE	p.LastPageNameLookupDate IS NOT NULL

UPDATE	dbo.Book
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage2 t 
		INNER JOIN dbo.Item i ON t.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	i.ItemStatusID NOT IN (30, 40)
AND		b.LastPageNameLookupDate IS NOT NULL

UPDATE	dbo.Segment
SET		LastPageNameLookupDate = NULL
FROM	#tmpPage2 t 
		INNER JOIN dbo.Item i ON t.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	i.ItemStatusID NOT IN (30, 40)
AND		s.LastPageNameLookupDate IS NOT NULL

DROP TABLE #tmpPage2

END

GO
