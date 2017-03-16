CREATE PROCEDURE [dbo].[ItemSelectAllRISCitations]
AS
BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @ISBNID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'

SELECT	DISTINCT
		ISNULL(b.BibliographicLevelName, '') AS Genre,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title,
		ISNULL(i.Volume, '') AS Volume,
		'http://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), i.ItemID) AS Url,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(t.Datafield_260_a, '') AS PublicationPlace,
		CASE 
			WHEN ISNULL(i.Year, '') <> '' THEN i.Year
			WHEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') <> '' THEN CONVERT(NVARCHAR(20), t.StartYear)
			ELSE ISNULL(t.Datafield_260_c, '')
		END AS [Year],
		c.Authors,
		c.Subjects AS Keywords,
		ISNULL(t.EditionStatement, '') AS Edition,
		ISNULL(isbn.IdentifierValue, ISNULL(issn.IdentifierValue, '')) AS ISSNISBN,
		ISNULL(l.LanguageName, '') AS [Language],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Notes
INTO	#RIS
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier isbn WITH (NOLOCK) ON t.TitleID = isbn.TitleID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.Title_Identifier issn WITH (NOLOCK) ON t.TitleID = issn.TitleID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.Language l WITH (NOLOCK) ON i.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1

SELECT 	Genre,
		Title,
		Volume,
		Url,
		Publisher,
		PublicationPlace,
		[Year],
		Authors,
		Keywords,
		Edition,
		MIN(ISSNISBN) AS ISSNISBN,
		[Language],
		Notes
FROM	#RIS
GROUP BY
		Genre,
		Title,
		Volume,
		Url,
		Publisher,
		PublicationPlace,
		[Year],
		Authors,
		Keywords,
		Edition,
		[Language],
		Notes

END
