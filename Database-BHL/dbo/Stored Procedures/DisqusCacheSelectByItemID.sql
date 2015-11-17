
CREATE PROCEDURE [dbo].[DisqusCacheSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

SELECT      *
FROM  dbo.DisqusCache a
WHERE a. ItemID = @ItemID

