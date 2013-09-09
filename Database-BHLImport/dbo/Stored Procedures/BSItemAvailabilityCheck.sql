
CREATE PROCEDURE [dbo].[BSItemAvailabilityCheck]

@BHLItemID int = null

AS

BEGIN

SET NOCOUNT ON

-- Identify the unavailable items
SELECT	bi.ItemID
INTO	#tmpItem
FROM	dbo.BSItem bi LEFT JOIN dbo.BHLItem i
			ON bi.BHLItemID = i.ItemID
WHERE	(i.RedirectItemID IS NOT NULL OR ISNULL(i.ItemStatusID, 0) <> 40)
-- Examine either all new items from BioStor, or just the specified item
AND		((bi.ItemStatusID = 10 AND @BHLItemID IS NULL) OR bi.BHLItemID = @BHLItemID)

UPDATE	dbo.BSItem
SET		ItemStatusID = 90,	-- Item Unavailable
		LastModifiedDate = GETDATE()
FROM	#tmpItem t INNER JOIN dbo.BSItem bi ON t.ItemID = bi.ItemID

-- Return the newly identified unavailable items
SELECT	bi.ItemID,
		bi.BHLItemID,
		bi.ItemStatusID,
		bi.CreationDate,
		bi.LastModifiedDate
FROM	#tmpItem t INNER JOIN dbo.BSItem bi ON t.ItemID = bi.ItemID

END

