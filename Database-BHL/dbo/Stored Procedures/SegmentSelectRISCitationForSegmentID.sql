CREATE PROCEDURE [dbo].[SegmentSelectRISCitationForSegmentID]

@SegmentID INT

AS

BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @EISSNID int
DECLARE @ISBNID int
DECLARE @DOIIdentifierID int
DECLARE @DOISEGMENTID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @EISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'eISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'
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
		s.Edition AS Edition,
		ISNULL(isbn.IdentifierValue, '') AS ISBN,
		ISNULL(issn.IdentifierValue, '') AS ISSN,
		ISNULL(eissn.IdentifierValue, '') AS EISSN,
		ISNULL(l.LanguageName, '') AS [Language],
		s.Notes,
		s.Summary AS Abstract,
		s.StartPageNumber AS StartPage,
		s.EndPageNumber AS EndPage
INTO	#RIS
FROM	dbo.vwSegment s 
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.ItemIdentifier isbn WITH (NOLOCK) ON s.ItemID = isbn.ItemID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.ItemIdentifier issn WITH (NOLOCK) ON s.ItemID = issn.ItemID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.ItemIdentifier eissn WITH (NOLOCK) ON s.ItemID = eissn.ItemID AND eissn.IdentifierID = @EISSNID
		LEFT JOIN dbo.ItemIdentifier doi WITH (NOLOCK) ON s.ItemID = doi.ItemID AND doi.IdentifierID = @DOIIdentifierID
		LEFT JOIN dbo.[Language] l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentID = @SegmentID
AND		s.SegmentStatusID IN (30, 40)
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
		EndPage
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
		EndPage

END

GO
