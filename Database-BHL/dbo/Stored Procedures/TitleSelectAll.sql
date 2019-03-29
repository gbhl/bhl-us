CREATE PROCEDURE [dbo].[TitleSelectAll]

@IsPublished	bit = null

AS 

SET NOCOUNT ON

SELECT 	t.[TitleID],
		t.[FullTitle],
		MAX(ISNULL(c.HasLocalContent, 0)) AS HasLocalContent,
		MAX(ISNULL(c.HasExternalContent, 0)) AS HasExternalContent
FROM	[dbo].[Title] t
		LEFT JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID
WHERE	t.PublishReady = ISNULL(@IsPublished,t.PublishReady)
GROUP BY t.TitleID,
		t.FullTitle,
		t.SortTitle
ORDER BY SortTitle
