
CREATE PROCEDURE [dbo].[CollectionSelectByTitleID]

@TitleID int

AS 

SET NOCOUNT ON

SELECT	c.[CollectionID],
		c.[CollectionName],
		c.[CollectionDescription],
		c.[CollectionURL],
		c.[HtmlContent],
		c.[CanContainTitles],
		c.[CanContainItems],
		c.[Active]
FROM	[dbo].[Collection] c INNER JOIN [dbo].[TitleCollection] tc
			ON c.CollectionID = tc.CollectionID
WHERE	c.[Active] = 1
AND		tc.TitleID = @TitleID
ORDER BY 
		c.[CollectionName]


