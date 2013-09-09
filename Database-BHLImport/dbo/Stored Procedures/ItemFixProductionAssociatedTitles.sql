CREATE PROCEDURE dbo.ItemFixProductionAssociatedTitles

@ItemSourceID INT

AS

BEGIN
/*
 * Look for items that have been associated with titles that have been "un-published" and redirected
 * to a different title.  For such items, add the appropriate TitleItem records and update the 
 * PrimaryTitleID.
 *
 * Typically, this should be only used for Botanicus items (@ItemSourceID = 2).  IA items may be 
 * left attached to a redirected title on purpose.  For example, they may have been replaced by
 * a different item.  In that case, they should have had their own status set to something other 
 * than Published... but it's possible that did not happen.  For that reason, more care should be
 * taken with IA items.  Botanicus items are unlikely to be affected by this scenario.
 *
*/

SET NOCOUNT ON

-- Get detail of the items that need to be associated with a different dbo.Title
SELECT	t.TitleID,
		COALESCE(t10.titleid, t9.titleid, t8.titleid, t7.titleid, t6.titleid, 
			t5.titleid, t4.titleid, t3.titleid, t2.titleid, t.titleid) AS RedirectTitleID,
		i.ItemID, i.Volume
INTO	#tmpRedirectedItems
FROM	dbo.BHLItem i INNER JOIN dbo.BHLTitle t ON i.PrimaryTitleID = t.TitleID
		-- Follow title redirects 10 levels deep
		LEFT JOIN dbo.BHLTitle t2 ON t.RedirectTitleID = t2.TitleID
		LEFT JOIN dbo.BHLTitle t3 ON t2.RedirectTitleID = t3.TitleID
		LEFT JOIN dbo.BHLTitle t4 ON t3.RedirectTitleID = t4.TitleID
		LEFT JOIN dbo.BHLTitle t5 ON t4.RedirectTitleID = t5.TitleID
		LEFT JOIN dbo.BHLTitle t6 ON t5.RedirectTitleID = t6.TitleID
		LEFT JOIN dbo.BHLTitle t7 ON t6.RedirectTitleID = t7.TitleID
		LEFT JOIN dbo.BHLTitle t8 ON t7.RedirectTitleID = t8.TitleID
		LEFT JOIN dbo.BHLTitle t9 ON t8.RedirectTitleID = t9.TitleID
		LEFT JOIN dbo.BHLTitle t10 ON t9.RedirectTitleID = t10.TitleID
WHERE	ItemID in (
			-- Filter to only those items that are associated with a single dbo.Title (a "replaced" dbo.Title)
			SELECT	ItemID 
			FROM	dbo.BHLTitleItem 
			WHERE	ItemID IN (
						-- Select identifiers for all active items associated with "replaced" titles
						SELECT	i.ItemID
						FROM	dbo.BHLTitle t INNER JOIN dbo.BHLTitleItem ti ON t.TitleID = ti.TitleID
								INNER JOIN dbo.BHLItem i ON ti.ItemID = i.ItemID
						WHERE	RedirectTitleID IS NOT NULL
						AND		i.itemstatusid = 40
						AND		i.RedirectItemID IS NULL
						AND		i.ItemSourceID = @ItemSourceID
						)
			GROUP BY ItemID HAVING COUNT(*) = 1
			)

-- Update items with new PrimaryTitleID
UPDATE dbo.BHLItem
SET PrimaryTitleID = t.RedirectTitleID
FROM #tmpRedirectedItems t INNER JOIN dbo.BHLItem i
	ON t.ItemID = i.ItemID

-- Add new TitleItem records.  Don't try to fix the item sequences, just put
-- these at the end.  Auto-fixing sequences will likely just mess up someone's
-- manual work.
INSERT	dbo.BHLTitleItem (TitleID, ItemID, ItemSequence)
SELECT	RedirectTitleID, ItemID, 1000 FROM #tmpRedirectedItems

DROP TABLE #tmpRedirectedItems

END
