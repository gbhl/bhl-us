
CREATE PROCEDURE [dbo].[CollectionSelectActive]
AS 

SET NOCOUNT ON

SELECT	[CollectionID],
		[CollectionName],
		[CollectionDescription],
		[CollectionURL],
		[CanContainTitles],
		[CanContainItems],
		[Active],
		[HtmlContent]
FROM	[dbo].[Collection]
WHERE	[Active] = 1
AND		[CollectionTarget] IN ('BHL', 'All')
ORDER BY 
		[CollectionName]


