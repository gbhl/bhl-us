
CREATE PROCEDURE [dbo].[CollectionSelectAll]
AS 

SET NOCOUNT ON

SELECT	[CollectionID],
		[CollectionName],
		[CollectionDescription],
		[CollectionURL],
		[CanContainTitles],
		[CanContainItems],
		[Active]
FROM	[dbo].[Collection]
ORDER BY 
		[CollectionName]


