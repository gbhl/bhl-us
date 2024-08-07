SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemUpdateThumbnailPageID]

@BarCode nvarchar(200)

AS

BEGIN

SET NOCOUNT ON

-- Get initial item information
SELECT	i.ItemID, NULL AS PageID
INTO	#tmpPageThumb
FROM	dbo.BHLBook b
		INNER JOIN dbo.BHLItem i on b.ItemID = i.ItemID
WHERE	i.ItemStatusID = 40
AND		b.BarCode = @BarCode

-- Get title page
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
from	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
		INNER JOIN dbo.BHLPagePageType ppt
			ON p.PageID = ppt.PageID
			AND ppt.PageTypeID = 1 -- title page

-- Get table of contents
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
		INNER JOIN dbo.BHLPagePageType ppt
			ON p.PageID = ppt.PageID
			AND	ppt.PageTypeID = 11 -- table of contents
WHERE	t.PageID IS NULL

-- Get page 1
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
		INNER JOIN dbo.BHLIndicatedPage ipg
			ON p.PageID = ipg.PageID
			AND ipg.PageNumber = '1'
			AND ipg.PagePrefix = 'Page'
WHERE	t.PageID IS NULL

-- Get page prior to page 2 (assume this is page 1)
UPDATE	#tmpPageThumb
SET		PageID = p2.PageID
FROM	#tmpPageThumb t2 
		INNER JOIN dbo.BHLItemPage ip ON t2.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p2 ON ip.PageID = p2.PageID
		INNER JOIN (
				SELECT	t.ItemID, p.SequenceOrder - 1 as SequenceOrder
				FROM	#tmpPageThumb t 
						INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
						INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
						INNER JOIN dbo.BHLIndicatedPage ipg
							ON p.PageID = ipg.PageID
							AND ipg.PageNumber = '2'
							AND ipg.PagePrefix = 'Page'
				where t.PageID is null
				) x
			ON p2.ItemID = x.ItemID
			AND p2.SequenceOrder = x.SequenceOrder
WHERE	t2.PageID IS NULL

-- Get plate 1
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
		INNER JOIN dbo.BHLIndicatedPage ipg
			ON p.PageID = ipg.PageID
			AND ipg.PageNumber = '1'
			AND ipg.PagePrefix = 'Plate'
WHERE	t.PageID IS NULL

-- Get plate i
UPDATE	#tmpPageThumb
SET		PageID = p.PageID
FROM	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
		INNER JOIN dbo.BHLIndicatedPage ipg
			ON p.PageID = ipg.PageID
			AND ((ipg.PageNumber = 'i' AND ipg.PagePrefix = 'Plate')
			OR ipg.PagePrefix = 'plate i')
WHERE	t.PageID IS NULL

-- Get page before first indicated page (whatever comes before first numbered page)
UPDATE	#tmpPageThumb
SET		PageID = p3.PageID
FROM	#tmpPageThumb t2 
		INNER JOIN dbo.BHLItemPage ip ON t2.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p3 ON ip.PageID = p3.PageID
		INNER JOIN (
				SELECT	p2.ItemID, CASE WHEN p2.SequenceOrder > 1 THEN p2.SequenceOrder - 1 ELSE p2.SequenceOrder END AS SequenceOrder
				FROM	dbo.BHLPage p2 INNER JOIN (
								SELECT	t.ItemID, min(p.PageID) PageID
								FROM	#tmpPageThumb t 
										INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
										INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
										INNER JOIN dbo.BHLIndicatedPage ipg
											ON p.PageID = ipg.PageID
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
FROM	#tmpPageThumb t2 
		INNER JOIN dbo.BHLItemPage ip ON t2.ItemID = ip.ItemID
		INNER JOIN dbo.BHLPage p2 ON ip.PageID = p2.PageID
		INNER JOIN (
				SELECT	t.ItemID, MIN(p.SequenceOrder) as SequenceOrder
				FROM	#tmppagethumb t 
						INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID
						INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
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
FROM	#tmpPageThumb t 
		INNER JOIN dbo.BHLItemPage ip ON t.ItemID = ip.ItemID AND ip.SequenceOrder = 1
		INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID
WHERE	t.PageID IS NULL

-- Update the item
UPDATE	dbo.BHLBook
SET		ThumbnailPageID = t.PageID
FROM	dbo.BHLBook b 
		INNER JOIN #tmpPageThumb t ON b.ItemID = t.ItemID

-- Clean up
DROP TABLE #tmpPageThumb

END

GO
