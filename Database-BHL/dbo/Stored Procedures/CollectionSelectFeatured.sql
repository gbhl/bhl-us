CREATE PROCEDURE [dbo].[CollectionSelectFeatured]
AS 

SET NOCOUNT ON

SELECT	[CollectionID],
		[CollectionName],
		[CollectionURL],
		[ImageURL],
		[HtmlContent]
FROM	[dbo].[Collection]
WHERE	[Featured] = 1
AND		[Active] = 1
AND		[CollectionTarget] IN ('BHL', 'All')
ORDER BY 
		[CollectionName]



