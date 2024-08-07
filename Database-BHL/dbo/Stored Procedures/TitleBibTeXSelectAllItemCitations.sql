CREATE PROCEDURE [dbo].[TitleBibTeXSelectAllItemCitations]

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpItem
	(
	TitleID int NOT NULL,
	BookID int NOT NULL,
	IsVirtual tinyint NOT NULL,
	ItemID int NOT NULL,
	CitationKey nvarchar(17) NOT NULL,
	Url nvarchar(50) NOT NULL,
	Note nvarchar(max) NOT NULL,
	Title nvarchar(2500) NOT NULL,
	Publisher nvarchar(405) NOT NULL,
	[Year] nvarchar(100) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Authors nvarchar(max) NOT NULL,
	Pages int NOT NULL,
	Keywords nvarchar(max) NOT NULL,
	HasLocalContent smallint NOT NULL,
	HasExternalContent smallint NOT NULL
	)

INSERT INTO #tmpItem
SELECT	t.TitleID, 
		b.BookID, 
		b.IsVirtual,
		i.ItemID, 
		'bhlitem' + CONVERT(NVARCHAR(10), b.BookID) AS CitationKey,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), b.BookID) AS Url,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) + dbo.fnNoteStringForTitle(t.TitleID, ' --- ') AS Note,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.PublicationDetails, '') AS Publisher,
		CASE WHEN b.StartYear IS NULL THEN ISNULL(t.Datafield_260_c, '') ELSE b.StartYear END AS [Year],
		ISNULL(b.Volume, '') AS Volume , 
		ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
		c.Authors, 
		0, 
		c.Subjects AS Keywords, 
		c.HasLocalContent, 
		c.HasExternalContent
FROM	dbo.Title t  WITH (NOLOCK)
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40

UPDATE	#tmpItem
SET		Pages = (	SELECT	COUNT(pg.PageID)
					FROM	dbo.Page pg
							INNER JOIN dbo.ItemPage ipg ON pg.PageID = ipg.PageID
							INNER JOIN dbo.Book bk ON ipg.ItemID = bk.ItemID
					WHERE	bk.BookID = #tmpItem.BookID);

-- Check segments related to any virtual items to determine if they have local/external content
WITH ItemCTE (ItemID, HasLocalContent, HasExternalContent)  
AS  
(  
	SELECT	t.ItemID, MAX(c.HasLocalContent), MAX(c.HasExternalContent)
	FROM	#tmpItem t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	IsVirtual = 1
	GROUP BY t.ItemID
)  
UPDATE	#tmpItem
SET		HasLocalContent = cte.HasLocalContent,
		HasExternalContent = cte.HasExternalcontent
FROM	#tmpItem t INNER JOIN ItemCTE cte ON t.ItemID = cte.ItemID;

SELECT	CitationKey, Url, Note, Title, Publisher, [Year], Volume, CopyrightStatus, Authors, Pages, Keywords, HasLocalContent, HasExternalContent
FROM	#tmpItem

END

GO
