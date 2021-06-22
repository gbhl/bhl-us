CREATE PROCEDURE [dbo].[TitleBibTeXSelectAllTitleCitations]
AS
BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		b.ItemID,
		'bhltitle' + CONVERT(NVARCHAR(10), t.TitleID) AS CitationKey,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Url,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.Datafield_260_a, '') + ISNULL(t.Datafield_260_b, '') AS Publisher,
		CASE WHEN ISNULL(t.Datafield_260_c, '') = '' THEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') ELSE ISNULL(t.Datafield_260_c, '') END [Year],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Note,
		c.Authors,
		c.Subjects AS Keywords,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
INTO	#title
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
		INNER JOIN dbo.Book b ON c.ItemID = b.BookID
WHERE	t.PublishReady = 1
GROUP BY t.TitleID, b.ItemID, t.FullTitle, t.PartNumber, t.PartName, t.Datafield_260_a, t.Datafield_260_b, t.Datafield_260_c, t.StartYear, c.Authors, c.Subjects;

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

SELECT	CitationKey,
		Url,
		Title, 
		Publisher,
		[Year],
		Note,
		Authors,
		Keywords,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
FROM	#title
GROUP BY 
		CitationKey,
		Url,
		Title, 
		Publisher,
		[Year],
		Note,
		Authors,
		Keywords;

END

GO
