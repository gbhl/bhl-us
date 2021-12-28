CREATE PROCEDURE [dbo].[TitleSelectAllRISCitations]

AS

BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int, @EISSNID int, @ISBNID int, @DOIID int
SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @EISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @DOIID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	DISTINCT
		c.TitleID,
		bk.ItemID,
		ISNULL(b.BibliographicLevelName, '') AS Genre,
		t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title,
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Url,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(t.Datafield_260_a, '') AS PublicationPlace,
		CASE WHEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') = '' THEN ISNULL(t.Datafield_260_c, '') ELSE ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') END [Year],
		c.Authors,
		c.Subjects AS Keywords,
		ISNULL(doi.IdentifierValue, '') AS DOI,
		ISNULL(t.EditionStatement, '') AS Edition,
		ISNULL(isbn.IdentifierValue, '') AS ISBN,
		ISNULL(issn.IdentifierValue, '') AS ISSN,
		ISNULL(eissn.IdentifierValue, '') AS EISSN,
		ISNULL(l.LanguageName, '') AS [Language],
		dbo.fnNoteStringForTitle(t.TitleID, '') AS Notes,
		c.HasLocalContent,
		c.HasExternalContent
INTO	#RIS
FROM	dbo.Title t WITH (NOLOCK)
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier isbn WITH (NOLOCK) ON t.TitleID = isbn.TitleID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.Title_Identifier issn WITH (NOLOCK) ON t.TitleID = issn.TitleID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.Title_Identifier eissn WITH (NOLOCK) ON t.TitleID = eissn.TitleID AND eissn.IdentifierID = @EISSNID
		LEFT JOIN dbo.Language l WITH (NOLOCK) ON t.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.Title_Identifier doi WITH (NOLOCK) ON t.TitleID = doi.TitleID AND doi.IdentifierID = @DOIID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
		INNER JOIN dbo.Book bk ON c.ItemID = bk.BookID
WHERE	t.PublishReady = 1;

-- Check titles with no local content to make sure there are no related virtual issues that DO have local content
WITH TitleCTE (TitleID, ItemID, HasLocalContent)  
AS  
(  
	SELECT	t.TitleID, t.ItemID, MAX(c.HasLocalContent)
	FROM	#RIS t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	t.HasLocalContent = 0
	GROUP BY t.TitleID, t.ItemID
)  
UPDATE	#RIS
SET		HasLocalContent = cte.HasLocalContent
FROM	#RIS t INNER JOIN TitleCTE cte ON t.TitleID = cte.TitleID AND t.ItemID = cte.ItemID;

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
		MIN(ISBN) AS ISBN,
		MIN(ISSN) AS ISSN,
		MIN(EISSN) AS EISSN,
		[Language],
		Notes,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
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

GO
