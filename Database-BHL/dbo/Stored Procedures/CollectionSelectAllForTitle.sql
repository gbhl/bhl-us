
CREATE PROCEDURE [dbo].[CollectionSelectAllForTitle]

@TitleID int

AS

BEGIN

SET NOCOUNT ON 

-- Select all collections related to the specified title or any of its related items
SELECT	c.CollectionID,
		c.CollectionName
FROM	dbo.Collection c INNER JOIN dbo.TitleCollection tc
			ON c.CollectionID = tc.CollectionID
WHERE	tc.TitleID = @TitleID
AND		c.Active = 1
AND		c.CollectionTarget IN ('BHL', 'All')

UNION

SELECT	c.CollectionID,
		c.CollectionName
FROM	dbo.TitleItem ti INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
		INNER JOIN dbo.ItemCollection ic
			ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Collection c
			ON ic.CollectionID = c.CollectionID
WHERE	ti.TitleID = @TitleID
AND		i.ItemStatusID = 40
AND		c.Active = 1
AND		c.CollectionTarget IN ('BHL', 'All')

ORDER BY CollectionName

END


