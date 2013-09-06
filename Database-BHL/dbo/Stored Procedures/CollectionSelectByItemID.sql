
CREATE PROCEDURE [dbo].[CollectionSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

SELECT	c.[CollectionID],
		c.[CollectionName],
		c.[CollectionDescription],
		c.[CollectionURL],
		c.[CanContainTitles],
		c.[CanContainItems],
		c.[Active]
FROM	[dbo].[Collection] c INNER JOIN [dbo].[ItemCollection] ic
			ON c.CollectionID = ic.CollectionID
WHERE	c.[Active] = 1
AND		ic.ItemID = @ItemID
ORDER BY 
		c.[CollectionName]


