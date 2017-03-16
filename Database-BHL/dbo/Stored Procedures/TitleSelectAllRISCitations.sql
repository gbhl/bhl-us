CREATE PROCEDURE [dbo].[TitleSelectAllRISCitations]
AS
BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @ISBNID int
DECLARE @DOITITLEID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @DOITITLEID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'

SELECT	DISTINCT
		ISNULL(b.BibliographicLevelName, '') AS Genre,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title,
		'http://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Url,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(t.Datafield_260_a, '') AS PublicationPlace,
		CASE WHEN ISNULL(CONVERT(NVARCHAR(20), StartYear), '') = '' THEN ISNULL(t.Datafield_260_c, '') ELSE ISNULL(CONVERT(NVARCHAR(20), StartYear), '') END [Year],
		c.Authors,
		c.Subjects AS Keywords,
		ISNULL(d.DOIName, '') AS DOI,
		ISNULL(t.EditionStatement, '') AS Edition,
		ISNULL(isbn.IdentifierValue, ISNULL(issn.IdentifierValue, '')) AS ISSNISBN,
		ISNULL(l.LanguageName, '') AS [Language],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Notes
INTO	#RIS
FROM	dbo.Title t WITH (NOLOCK)
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier isbn WITH (NOLOCK) ON t.TitleID = isbn.TitleID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.Title_Identifier issn WITH (NOLOCK) ON t.TitleID = issn.TitleID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.Language l WITH (NOLOCK) ON t.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.DOI d WITH (NOLOCK) ON t.TitleID = d.EntityID AND d.DOIEntityTypeID = @DOITITLEID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
WHERE	t.PublishReady = 1

SELECT 	Genre,
		Title,
		Url,
		Publisher,
		PublicationPlace,
		[Year],
		Authors,
		Keywords,
		DOI,
		Edition,
		MIN(ISSNISBN) AS ISSNISBN,
		[Language],
		Notes
FROM	#RIS
GROUP BY
		Genre,
		Title,
		Url,
		Publisher,
		PublicationPlace,
		[Year],
		Authors,
		Keywords,
		DOI,
		Edition,
		[Language],
		Notes

END
