CREATE PROCEDURE dbo.BSItemSelectByBHLItemAndStatus

@BHLItemID int,
@ItemStatusID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID, i.BHLItemID
FROM	dbo.BSItem i
WHERE	i.ItemStatusID = @ItemStatusID
AND		(i.BHLItemID = @BHLItemID OR @BHLItemID IS NULL)

END
GO
