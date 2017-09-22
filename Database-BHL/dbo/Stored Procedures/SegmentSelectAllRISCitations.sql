CREATE PROCEDURE [dbo].[SegmentSelectAllRISCitations]
AS
BEGIN

SET NOCOUNT ON

DECLARE @ISSNID int
DECLARE @ISBNID int
DECLARE @DOISEGMENTID int

SELECT @ISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @ISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
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
		ISNULL(d.DOIName, '') AS DOI,
		s.Edition,
		ISNULL(isbn.IdentifierValue, ISNULL(issn.IdentifierValue, '')) AS ISSNISBN,
		ISNULL(l.LanguageName, '') AS [Language],
		s.Notes,
		s.Summary AS Abstract,
		s.StartPageNumber AS StartPage,
		s.EndPageNumber AS EndPage
INTO	#RIS
FROM	dbo.vwSegment s 
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		LEFT JOIN dbo.SegmentIdentifier isbn WITH (NOLOCK) ON s.SegmentID = isbn.SegmentID AND isbn.IdentifierID = @ISBNID
		LEFT JOIN dbo.SegmentIdentifier issn WITH (NOLOCK) ON s.SegmentID = issn.SegmentID AND issn.IdentifierID = @ISSNID
		LEFT JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = @DOISEGMENTID AND d.IsValid = 1
		LEFT JOIN dbo.[Language] l WITH (NOLOCK) ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentStatusID IN (10, 20)
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
		MIN(ISSNISBN) AS ISSNISBN,
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
