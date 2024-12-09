CREATE PROCEDURE dbo.PageSelectRISCitationForPageID

@PageID INT,
@TitleID INT = NULL

AS

BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @ISBNID int
DECLARE @EISSNID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @EISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'

SELECT	DISTINCT
		ISNULL(bl.BibliographicLevelName, '') AS Genre,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title,
		ISNULL(b.Volume, '') AS Volume,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(NVARCHAR(10), ip.PageID) AS Url,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(t.Datafield_260_a, '') AS PublicationPlace,
		CASE 
			WHEN ISNULL(b.StartYear, '') <> '' THEN b.StartYear
			WHEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') <> '' THEN CONVERT(NVARCHAR(20), t.StartYear)
			ELSE ISNULL(t.Datafield_260_c, '')
		END AS [Year],
		c.Authors,
		c.Subjects AS Keywords,
		ISNULL(t.EditionStatement, '') AS Edition,
		ISNULL(isbn.IdentifierValue, '') AS ISBN,
		ISNULL(issn.IdentifierValue, '') AS ISSN,
		ISNULL(eissn.IdentifierValue, '') AS EISSN,
		ISNULL(l.LanguageName, '') AS [Language],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Notes,
		ISNULL(ind.PageNumber, '') AS StartPage
INTO	#RIS
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND ((it.IsPrimary = 1 AND @TitleID IS NULL) OR it.TitleID = @TitleID)
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		LEFT JOIN dbo.IndicatedPage ind ON ip.PageID = ind.PageID AND ind.Sequence = 1
		LEFT JOIN dbo.BibliographicLevel bl ON t.BibliographicLevelID = bl.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier isbn ON t.TitleID = isbn.TitleID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.Title_Identifier issn ON t.TitleID = issn.TitleID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.Title_Identifier eissn ON t.TitleID = eissn.TitleID AND eissn.IdentifierID = @EISSNID
		LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	ip.PageID = @PageID

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
		MIN(ISBN) AS ISBN,
		MIN(ISSN) AS ISSN,
		MIN(EISSN) AS EISSN,
		[Language],
		Notes,
		StartPage
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
		Notes,
		StartPage

END

GO
