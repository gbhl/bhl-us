CREATE PROCEDURE [dbo].[ItemSelectAllRISCitations]

AS

BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @EISSNID int
DECLARE @ISBNID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @EISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'

SELECT	DISTINCT
		ISNULL(bl.BibliographicLevelName, '') AS Genre,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title,
		ISNULL(b.Volume, '') AS Volume,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), b.BookID) AS Url,
		b.ItemID,
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
		b.IsVirtual,
		c.HasLocalContent,
		c.HasExternalContent
INTO	#RIS
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.BibliographicLevel bl ON t.BibliographicLevelID = bl.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier isbn ON t.TitleID = isbn.TitleID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.Title_Identifier issn ON t.TitleID = issn.TitleID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.Title_Identifier eissn ON t.TitleID = eissn.TitleID AND eissn.IdentifierID = @EISSNID
		LEFT JOIN dbo.Language l ON b.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1;

-- Check segments related to any virtual items to determine if they have local/external content
WITH ItemCTE (ItemID, HasLocalContent, HasExternalContent)  
AS  
(  
	SELECT	t.ItemID, MAX(c.HasLocalContent), MAX(c.HasExternalContent)
	FROM	#RIS t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	IsVirtual = 1
	GROUP BY t.ItemID
)  
UPDATE	#RIS
SET		HasLocalContent = cte.HasLocalContent,
		HasExternalContent = cte.HasExternalcontent
FROM	#RIS t INNER JOIN ItemCTE cte ON t.ItemID = cte.ItemID;

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
		HasLocalContent,
		HasExternalContent
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
		HasLocalContent,
		HasExternalContent

END

GO
