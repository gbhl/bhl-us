CREATE PROCEDURE [dbo].[ItemCollectionSelectByItem]

@ItemID INT 

AS 

SET NOCOUNT ON

SELECT 	i.ItemCollectionID,
		i.[ItemID],
		i.[CollectionID],
		c.CollectionName,
		c.CollectionDescription
FROM	[dbo].[ItemCollection] i INNER JOIN dbo.[Collection] c
			ON i.CollectionID = c.CollectionID
WHERE	i.[ItemID] = @ItemID 
ORDER BY c.CollectionName

