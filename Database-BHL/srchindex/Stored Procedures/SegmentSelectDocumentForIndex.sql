CREATE PROCEDURE [srchindex].[SegmentSelectDocumentForIndex]

@ItemID int,
@SegmentID int

AS 

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpSegment
	(
	SegmentID int NOT NULL,
	ItemID int NULL,
	Title nvarchar(2000) NOT NULL,
	TranslatedTitle nvarchar(2000) NOT NULL,
	SortTitle nvarchar(2000) NOT NULL,
	ContainerTitle nvarchar(2000) NOT NULL,
	ContainerTitlePartNumber nvarchar(255) NOT NULL,
	ContainerTitlePartName nvarchar(255) NOT NULL,
	Authors nvarchar(max) NOT NULL,
	FacetAuthors nvarchar(max) NOT NULL,
	SearchAuthors nvarchar(max) NOT NULL,
	Subjects nvarchar(max) NOT NULL,
	Contributors nvarchar(max) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	Series nvarchar(100) NOT NULL,
	Issue nvarchar(100) NOT NULL,
	PublisherName nvarchar(300) NOT NULL,
	PublisherPlace nvarchar(200) NOT NULL,
	BookIsVirtual int NOT NULL,
	HasLocalContent int NOT NULL,
	HasExternalContent int NOT NULL,
	HasIllustrations int NOT NULL,
	LanguageName nvarchar(20) NOT NULL,
	GenreName nvarchar(50) NOT NULL,
	MaterialTypeLabel nvarchar(60) NOT NULL,
	DOIName nvarchar(50) NOT NULL,
	Url nvarchar(200) NOT NULL,
	StartPageID int NULL,
	PageRange nvarchar(50) NOT NULL,
	[Date] nvarchar(20) NOT NULL
	)

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

INSERT	#tmpSegment
SELECT	s.SegmentID,
		b.BookID AS ItemID,
		RTRIM(s.Title) AS Title,
		RTRIM(s.TranslatedTitle) AS TranslatedTitle,
		-- Remove diacritics and force to lowercase for SortTitle
		LOWER(ISNULL(TRANSLATE(REPLACE(REPLACE(s.SortTitle, 'Œ', 'OE'), 'œ', 'oe') COLLATE Latin1_General_CS_AI, 'AEIOUCDNRSTXYZaeioucdnrstxyz', 'AEIOUCDNRSTXYZaeioucdnrstxyz'), '')) AS SortTitle,
		RTRIM(s.ContainerTitle) AS ContainerTitle,
		ISNULL(s.ContainerTitlePartNumber, '') AS ContainerTitlePartNumber,
		ISNULL(s.ContainerTitlePartName, '') AS ContainerTitlePartName,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForSegment(s.SegmentID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForSegment(s.SegmentID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnContributorStringForSegment(s.SegmentID)) + ' ', '') AS Contributors,
		RTRIM(s.Volume) AS Volume,
		RTRIM(s.Series) AS Series,
		RTRIM(s.Issue) AS Issue,
		RTRIM(s.PublisherName) AS PublisherName,
		RTRIM(s.PublisherPlace) AS PublisherPlace,
		ISNULL(b.IsVirtual, 0) AS BookIsVirtual,
		0 AS HasLocalContent,
		RTRIM(CASE WHEN s.Url = '' THEN 0 ELSE 1 END) AS HasExternalContent,
		0 AS HasIllustrations,
		ISNULL(l.LanguageName, '') AS LanguageName,
		ISNULL(g.GenreName, '') AS GenreName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		ISNULL(ii.IdentifierValue, '') AS DOIName,
		ISNULL(s.Url, '') AS Url,
		s.StartPageID,
		CASE WHEN ISNULL(PageRange, '') = '' THEN StartPageNumber +'--' + EndPageNumber ELSE PageRange END AS PageRange,
		RTRIM(s.[Date]) AS [Date]
FROM	dbo.vwSegment s
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierIDDOI
		LEFT JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		LEFT JOIN dbo.Item i ON r.ParentID = i.ItemID
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
WHERE	b.BookID = @ItemID
UNION
SELECT	s.SegmentID,
		b.BookID AS ItemID,
		RTRIM(s.Title) AS Title,
		RTRIM(s.TranslatedTitle) AS TranslatedTitle,
		-- Remove diacritics and force to lowercase for SortTitle
		LOWER(ISNULL(TRANSLATE(REPLACE(REPLACE(s.SortTitle, 'Œ', 'OE'), 'œ', 'oe') COLLATE Latin1_General_CS_AI, 'AEIOUCDNRSTXYZaeioucdnrstxyz', 'AEIOUCDNRSTXYZaeioucdnrstxyz'), '')) AS SortTitle,
		RTRIM(s.ContainerTitle) AS ContainerTitle,
		ISNULL(s.ContainerTitlePartNumber, '') AS ContainerTitlePartNumber,
		ISNULL(s.ContainerTitlePartName, '') AS ContainerTitlePartName,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForSegment(s.SegmentID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForSegment(s.SegmentID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnContributorStringForSegment(s.SegmentID)) + ' ', '') AS Contributors,
		RTRIM(s.Volume) AS Volume,
		RTRIM(s.Series) AS Series,
		RTRIM(s.Issue) AS Issue,
		RTRIM(s.PublisherName) AS PublisherName,
		RTRIM(s.PublisherPlace) AS PublisherPlace,
		ISNULL(b.IsVirtual, 0) AS BookIsVirtual,
		0 AS HasLocalContent,
		RTRIM(CASE WHEN s.Url = '' THEN 0 ELSE 1 END) AS HasExternalContent,
		0 AS HasIllustrations,
		ISNULL(l.LanguageName, '') AS LanguageName,
		ISNULL(g.GenreName, '') AS GenreName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		ISNULL(ii.IdentifierValue, '') AS DOIName,
		ISNULL(s.Url, '') AS Url,
		s.StartPageID,
		CASE WHEN ISNULL(PageRange, '') = '' THEN StartPageNumber +'--' + EndPageNumber ELSE PageRange END AS PageRange,
		RTRIM(s.[Date]) AS [Date]
FROM	dbo.vwSegment s
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierIDDOI
		LEFT JOIN dbo.ItemRelationship r ON s.ItemID = r.ChildID
		LEFT JOIN dbo.Item i ON r.ParentID = i.ItemID
		LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
		LEFT JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
WHERE	s.SegmentID = @SegmentID

UPDATE	#tmpSegment
SET		HasLocalContent = 1
FROM	#tmpSegment t 
		INNER JOIN dbo.Segment s ON t.SegmentID = s.SegmentID
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID

SELECT * FROM #tmpSegment

END

GO
