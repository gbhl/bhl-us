CREATE PROCEDURE [dbo].[TitleSelectAll]

@IsPublished	bit = null

AS 

SET NOCOUNT ON

SELECT 	t.TitleID,
		b.ItemID,
		t.FullTitle,
		t.SortTitle,
		MAX(ISNULL(c.HasLocalContent, 0)) AS HasLocalContent,
		MAX(ISNULL(c.HasExternalContent, 0)) AS HasExternalContent
INTO	#title
FROM	dbo.Title t
		LEFT JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID
		LEFT JOIN dbo.Book b ON c.ItemID = b.BookID
WHERE	t.PublishReady = ISNULL(@IsPublished,t.PublishReady)
GROUP BY t.TitleID,
		b.ItemID,
		t.FullTitle,
		t.SortTitle;

-- Check titles with no local content to make sure there are no related virtual issues that DO have local content
WITH TitleCTE (TitleID, ItemID, HasLocalContent)  
AS  
(  
	SELECT	t.TitleID, t.ItemID, MAX(c.HasLocalContent)
	FROM	#title t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	t.HasLocalContent = 0
	GROUP BY t.TitleID, t.ItemID
)  
UPDATE	#title
SET		HasLocalContent = cte.HasLocalContent
FROM	#title t INNER JOIN TitleCTE cte ON t.TitleID = cte.TitleID AND t.ItemID = cte.ItemID;

SELECT 	TitleID,
		FullTitle,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
FROM	#title
GROUP BY TitleID,
		FullTitle,
		SortTitle
ORDER BY SortTitle

GO
