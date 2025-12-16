CREATE PROCEDURE dbo.BSItemSelectQueuedByBHLItem

@BHLItemID int = NULL

AS

BEGIN

SELECT	TOP 1 i.ItemID
FROM	dbo.BSItem i
		INNER JOIN dbo.BSItemStatus st ON i.ItemStatusID = st.ItemStatusID
WHERE	st.Status IN ('New', 'Harvested', 'Preprocessed')
AND		i.BHLItemID = @BHLItemID

END
GO
