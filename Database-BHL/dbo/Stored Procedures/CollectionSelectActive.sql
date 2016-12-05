CREATE PROCEDURE [dbo].[CollectionSelectActive]
AS 

SET NOCOUNT ON

-- Only include collections that have content
SELECT	DISTINCT
		c.[CollectionID],
		c.[CollectionName],
		c.[CollectionDescription],
		c.[CollectionURL],
		c.[CanContainTitles],
		c.[CanContainItems],
		c.[Active],
		c.[HtmlContent]
FROM	[dbo].[Collection] c INNER JOIN [dbo].[TitleCollection] tc ON c.CollectionID = tc.CollectionID
WHERE	c.[Active] = 1
AND		c.[CollectionTarget] IN ('BHL', 'All')

UNION

SELECT	DISTINCT
		c.[CollectionID],
		c.[CollectionName],
		c.[CollectionDescription],
		c.[CollectionURL],
		c.[CanContainTitles],
		c.[CanContainItems],
		c.[Active],
		c.[HtmlContent]
FROM	[dbo].[Collection] c INNER JOIN [dbo].[ItemCollection] ic ON c.CollectionID = ic.CollectionID
WHERE	c.[Active] = 1
AND		c.[CollectionTarget] IN ('BHL', 'All')
ORDER BY 
		c.[CollectionName]
