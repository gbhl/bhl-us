CREATE PROCEDURE dbo.BSItemSelectByItemAndStatus

@ItemID int,
@ItemStatusID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID, i.BHLItemID
FROM	dbo.BSItem i
WHERE	i.ItemStatusID = @ItemStatusID
AND		(i.ItemID = @ItemID OR @ItemID IS NULL)
ORDER BY i.ItemID

END
GO
