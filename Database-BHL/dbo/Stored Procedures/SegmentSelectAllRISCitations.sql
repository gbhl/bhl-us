CREATE PROCEDURE [dbo].[SegmentSelectAllRISCitations]
AS
BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @EISSNID int
DECLARE @ISBNID int
DECLARE @DOIID int
DECLARE @DOISEGMENTID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @EISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @DOIID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'
SELECT @DOISEGMENTID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

SELECT	DISTINCT
		g.GenreName AS Genre,
		s.Title,
		-- Would prefer to use Title.FullTitle as the Journal name, but the journal title 
		-- is not always the Primary title associated with the item
		s.ContainerTitle AS Journal,
		s.Volume,
		s.Issue,
		'https://www.biodiversitylibrary.org/part/' + CONVERT(NVARCHAR(10), s.SegmentID) AS Url,
		s.PublisherName AS Publisher,
		s.PublisherPlace AS PublicationPlace,
		s.[Date] AS [Year],
		scs.Authors,
		scs.Subjects AS Keywords,
		ISNULL(doi.IdentifierValue, '') AS DOI,
		s.Edition,
		ISNULL(isbn.IdentifierValue, '') AS ISBN,
		ISNULL(issn.IdentifierValue, '') AS ISSN,
		ISNULL(eissn.IdentifierValue, '') AS EISSN,
		ISNULL(l.LanguageName, '') AS [Language],
		s.Notes,
		s.Summary AS Abstract,
		s.StartPageNumber AS StartPage,
		s.EndPageNumber AS EndPage,
		scs.HasLocalContent,
		scs.HasExternalContent
INTO	#RIS
FROM	dbo.vwSegment s WITH (NOLOCK) 
		INNER JOIN dbo.SegmentGenre g WITH (NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.ItemRelationship r WITH (NOLOCK) ON s.ItemID = r.ChildID
		LEFT JOIN dbo.Item bi WITH (NOLOCK) ON r.ParentID = bi.ItemID
		LEFT JOIN dbo.ItemTitle it WITH (NOLOCK) ON bi.ItemID = it.ItemID AND it.IsPrimary = 1
		LEFT JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
		LEFT JOIN dbo.ItemIdentifier isbn WITH (NOLOCK) ON s.ItemID = isbn.ItemID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.ItemIdentifier issn WITH (NOLOCK) ON s.ItemID = issn.ItemID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.ItemIdentifier eissn WITH (NOLOCK) ON s.ItemID = eissn.ItemID AND eissn.IdentifierID = @EISSNID
		LEFT JOIN dbo.ItemIdentifier doi WITH (NOLOCK) ON s.ItemID = doi.ItemID AND doi.IdentifierID = @DOIID
		LEFT JOIN dbo.[Language] l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs WITH (NOLOCK) ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentStatusID IN (30, 40)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)

SELECT 	Genre,
		Title,
		Journal,
		Volume,
		Issue,
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
		Abstract,
		StartPage,
		EndPage,
		HasLocalContent,
		HasExternalContent
FROM	#RIS
GROUP BY
		Genre,
		Title,
		Journal,
		Volume,
		Issue,
		Url,
		Publisher,
		PublicationPlace,
		[Year],
		Authors,
		Keywords,
		DOI,
		Edition,
		[Language],
		Notes,
		Abstract,
		StartPage,
		EndPage,
		HasLocalContent,
		HasExternalContent

END

GO
