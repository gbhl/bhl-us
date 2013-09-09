CREATE PROCEDURE dbo.ItemUpdateThumbnailPageID

@BarCode nvarchar(40)

AS

BEGIN

SET NOCOUNT ON

-- Get initial item information
SELECT	ItemID, NULL AS PageID
INTO	#tmpPageThumb
FROM	dbo.BHLItem
WHERE	ItemStatusID = 40
AND		BarCode = @BarCode

-- Get title page
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
from	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
		INNER JOIN dbo.BHLPagePageType ppt
			ON p.PageID = ppt.PageID
			AND ppt.PageTypeID = 1 -- title page

-- Get table of contents
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
		INNER JOIN dbo.BHLPagePageType ppt
			ON p.PageID = ppt.PageID
			AND	ppt.PageTypeID = 11 -- table of contents
WHERE	t.PageID IS NULL

-- Get page 1
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
		INNER JOIN dbo.BHLIndicatedPage ip
			ON p.PageID = ip.PageID
			AND ip.PageNumber = '1'
			AND ip.PagePrefix = 'Page'
WHERE	t.PageID IS NULL

-- Get page prior to page 2 (assume this is page 1)
UPDATE	#tmpPageThumb
SET		PageID = p2.PageID
FROM	#tmpPageThumb t2 INNER JOIN dbo.BHLPage p2 
			ON t2.ItemID = p2.ItemID
		INNER JOIN (
				SELECT	t.ItemID, p.SequenceOrder - 1 as SequenceOrder
				FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
							ON t.ItemID = p.ItemID
						INNER JOIN dbo.BHLIndicatedPage ip
							ON p.PageID = ip.PageID
							AND ip.PageNumber = '2'
							AND ip.PagePrefix = 'Page'
				where t.PageID is null
				) x
			ON p2.ItemID = x.ItemID
			AND p2.SequenceOrder = x.SequenceOrder
WHERE	t2.PageID IS NULL

-- Get plate 1
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
		INNER JOIN dbo.BHLIndicatedPage ip
			ON p.PageID = ip.PageID
			AND ip.PageNumber = '1'
			AND ip.PagePrefix = 'Plate'
WHERE	t.PageID IS NULL

-- Get plate i
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
		INNER JOIN dbo.BHLIndicatedPage ip
			ON p.PageID = ip.PageID
			AND ((ip.PageNumber = 'i' AND ip.PagePrefix = 'Plate')
			OR ip.PagePrefix = 'plate i')
WHERE	t.PageID IS NULL

-- Get page before first indicated page (whatever comes before first numbered page)
UPDATE	#tmpPageThumb
SET		PageID = p3.PageID
FROM	#tmpPageThumb t2 INNER JOIN dbo.BHLPage p3
			ON t2.ItemID = p3.ItemID
		INNER JOIN (
				SELECT	p2.ItemID, CASE WHEN p2.SequenceOrder > 1 THEN p2.SequenceOrder - 1 ELSE p2.SequenceOrder END AS SequenceOrder
				FROM	dbo.BHLPage p2 INNER JOIN (
								SELECT	t.ItemID, min(p.PageID) PageID
								FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
											ON t.ItemID = p.ItemID
										INNER JOIN dbo.BHLIndicatedPage ip
											ON p.PageID = ip.PageID
								WHERE	t.PageID IS NULL
								GROUP BY t.ItemID
								) x
							ON p2.PageID = x.PageID
				) y
			ON p3.ItemID = y.ItemID
			AND p3.SequenceOrder = y.SequenceOrder
WHERE	t2.PageID IS NULL

-- Get first non-'cover' page
UPDATE	#tmpPageThumb
SET		PageID = p2.PageID
FROM	#tmpPageThumb t2 INNER JOIN dbo.BHLPage p2
			ON t2.ItemID = p2.ItemID
		INNER JOIN (
				SELECT	t.ItemID, MIN(p.SequenceOrder) as SequenceOrder
				FROM	#tmppagethumb t INNER JOIN dbo.BHLPage p
							ON t.ItemID = p.ItemID
						INNER JOIN dbo.BHLPagePageType ppt
							ON p.PageID = ppt.PageID
							AND ppt.PageTypeID <> 8 -- cover
				WHERE	t.PageID IS NULL
				GROUP BY t.ItemID
				) x
			ON p2.ItemID = x.ItemID
			AND p2.SequenceOrder = x.SequenceOrder
WHERE	t2.PageID IS NULL
		
-- Get the item's first page as a last resort
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t INNER JOIN dbo.BHLPage p
			ON t.ItemID = p.ItemID
			AND p.SequenceOrder = 1
WHERE	t.PageID IS NULL

-- Update the item
UPDATE	dbo.BHLItem
SET		ThumbnailPageID = t.PageID
FROM	dbo.BHLItem i INNER JOIN #tmpPageThumb t
			ON i.ItemID = t.ItemID

-- Clean up
DROP TABLE #tmpPageThumb

END
