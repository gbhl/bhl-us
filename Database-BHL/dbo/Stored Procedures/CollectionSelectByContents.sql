
CREATE PROCEDURE [dbo].[CollectionSelectByContents]

@CanContainTitles smallint,
@CanContainItems smallint

AS 

SET NOCOUNT ON

SELECT	[CollectionID],
		[CollectionName],
		[CollectionDescription]
FROM	[dbo].[Collection]
WHERE	[CanContainTitles] = @CanContainTitles
AND		[CanContainItems] = @CanContainItems
AND		[Active] = 1
ORDER BY 
		[CollectionName]


