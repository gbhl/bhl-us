CREATE PROCEDURE dbo.PageBibTeXSelectForPageID

@PageID INT

AS
BEGIN

SET NOCOUNT ON

SELECT	'bhlpage' + CONVERT(NVARCHAR(10), ip.PageID) AS CitationKey,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(NVARCHAR(10), ip.PageID) AS Url,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Note,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.PublicationDetails, '') AS Publisher,
		CASE WHEN b.StartYear IS NULL THEN ISNULL(t.Datafield_260_c, '') ELSE b.StartYear END AS [Year],
		ISNULL(b.Volume, '') AS Volume , ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
		c.Authors,
		ISNULL(ind.PageNumber, '') AS PageRange,
		c.Subjects AS Keywords
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		LEFT JOIN dbo.IndicatedPage ind ON ip.PageID = ind.PageID AND ind.Sequence = 1
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		ip.PageID = @PageID
ORDER BY it.ItemSequence
			
END

GO
