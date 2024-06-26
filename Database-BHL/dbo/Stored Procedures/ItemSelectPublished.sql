SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectPublished]

AS

BEGIN

SET NOCOUNT ON

SELECT	b.BookID,
		i.ItemID,
		b.IsVirtual,
		BarCode,
		b.Volume,
		HasLocalContent,
		HasExternalContent
INTO	#item
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40;

-- Check segments related to any virtual items to determine if they have local/external content
WITH ItemCTE (ItemID, HasLocalContent, HasExternalContent)  
AS  
(  
	SELECT	t.ItemID, MAX(c.HasLocalContent), MAX(c.HasExternalContent)
	FROM	#item t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	IsVirtual = 1
	GROUP BY t.ItemID
)  
UPDATE	#item
SET		HasLocalContent = cte.HasLocalContent,
		HasExternalContent = cte.HasExternalcontent
FROM	#item t INNER JOIN ItemCTE cte ON t.ItemID = cte.ItemID;

SELECT	BookID AS ItemID,
		BarCode,
		Volume,
		HasLocalContent,
		HasExternalContent
FROM	#item

END

GO
