CREATE PROCEDURE [dbo].[TitleBibTeXSelectAllTitleCitations]
AS
BEGIN

SET NOCOUNT ON

SELECT	DISTINCT
		'bhltitle' + CONVERT(NVARCHAR(10), t.TitleID) AS CitationKey,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Url,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
		ISNULL(t.Datafield_260_a, '') + ISNULL(t.Datafield_260_b, '') AS Publisher,
		CASE WHEN ISNULL(t.Datafield_260_c, '') = '' THEN ISNULL(CONVERT(NVARCHAR(20), StartYear), '') ELSE ISNULL(t.Datafield_260_c, '') END [Year],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Note,
		c.Authors,
		c.Subjects AS Keywords,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
WHERE	t.PublishReady = 1
GROUP BY t.TitleID, t.FullTitle, t.PartNumber, t.PartName, t.Datafield_260_a, t.Datafield_260_b, t.Datafield_260_c, t.StartYear, c.Authors, c.Subjects

END
