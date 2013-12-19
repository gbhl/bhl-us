
CREATE PROCEDURE [dbo].[TitleBibTeXSelectAllTitleCitations]
AS
BEGIN

SET NOCOUNT ON

SELECT	DISTINCT
		'bhltitle' + CONVERT(NVARCHAR(10), t.TitleID) AS CitationKey,
		'http://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Url,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.Datafield_260_a, '') + ISNULL(t.Datafield_260_b, '') AS Publisher,
		CASE WHEN ISNULL(t.Datafield_260_c, '') = '' THEN ISNULL(CONVERT(NVARCHAR(20), StartYear), '') ELSE ISNULL(t.Datafield_260_c, '') END [Year],
		c.Authors,
		c.Subjects AS Keywords
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID
WHERE	t.PublishReady = 1

END

