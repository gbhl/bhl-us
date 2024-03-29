SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BSItemAvailabilityCheck]

@BHLItemID int = null

AS

BEGIN

SET NOCOUNT ON

-- Identify the unavailable items
SELECT	bi.ItemID
INTO	#tmpItem
FROM	dbo.BSItem bi 
		LEFT JOIN dbo.BHLBook b	ON bi.BHLItemID = b.BookID
		LEFT JOIN dbo.BHLItem i ON b.ItemID = i.ItemID
WHERE	(b.RedirectBookID IS NOT NULL OR ISNULL(i.ItemStatusID, 0) <> 40)
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


GO
