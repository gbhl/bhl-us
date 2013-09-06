
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpOpenUrlCitation
	(
	PageID int NULL DEFAULT(0),
	ItemID int NULL DEFAULT(0),
	TitleID int NULL DEFAULT(0),
	SegmentID int NULL DEFAULT(0),
	FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
	SegmentTitle nvarchar(2000) NOT NULL DEFAULT(''),
	ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
	PublisherPlace nvarchar(150) NOT NULL DEFAULT(''),
	PublisherName nvarchar(255) NOT NULL DEFAULT(''),
	Date nvarchar(20) NOT NULL DEFAULT(''),
	LanguageName nvarchar(20) NOT NULL DEFAULT(''),
	Volume nvarchar(100) NOT NULL DEFAULT(''),
	EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
	CurrentPublicationFrequency nvarchar(100) NOT NULL DEFAULT(''),
	Genre nvarchar(20) NOT NULL DEFAULT(''),
	Authors nvarchar(1000) NOT NULL DEFAULT(''),
	Subjects nvarchar(1000) NOT NULL DEFAULT(''),
	StartPage nvarchar(40) NOT NULL DEFAULT(''),
	EndPage nvarchar(40) NOT NULL DEFAULT(''),
	Pages nvarchar(40) NOT NULL DEFAULT(''),
	ISSN nvarchar(125) NOT NULL DEFAULT(''),
	ISBN nvarchar(125) NOT NULL DEFAULT(''),
	LCCN nvarchar(125) NOT NULL DEFAULT(''),
	OCLC nvarchar(125) NOT NULL DEFAULT(''),
	Abbreviation nvarchar(125) NOT NULL DEFAULT('')
	)

-- Get the basic title/item/page information
INSERT INTO #tmpOpenUrlCitation	(
	SegmentID, SegmentTitle, ContainerTitle, PublisherPlace, PublisherName, Date, LanguageName,
	Volume, Genre, Authors, Subjects, StartPage, EndPage, Pages
	)
SELECT	s.SegmentID,
		s.Title,
		s.ContainerTitle,
		s.PublisherPlace,
		CASE WHEN s.PublisherName = '' THEN s.PublicationDetails ELSE s.PublisherName END,
		s.Date,
		ISNULL(l.LanguageName, ''),
		s.Volume,
		ISNULL(g.GenreName, ''),
		scs.Authors,
		scs.Subjects,
		s.StartPageNumber,
		s.EndPageNumber,
		s.PageRange
FROM	dbo.Segment s WITH (NOLOCK) INNER JOIN dbo.SegmentGenre g WITH (NOLOCK)
			ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l WITH (NOLOCK)
			ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalogSegment scs
			ON s.SegmentID = scs.SegmentID
WHERE	s.SegmentID = @SegmentID
AND		s.SegmentStatusID IN (10, 20)
AND		(scs.HasLocalContent = 1 OR scs.HasExternalContent = 1 OR scs.ItemID IS NOT NULL)

-- Get the identifiers
UPDATE	#tmpOpenUrlCitation
SET		ISSN = si.IdentifierValue
FROM	#tmpOpenUrlCitation t INNER JOIN SegmentIdentifier si WITH (NOLOCK)
			ON t.SegmentID = si.SegmentID
			AND si.IdentifierID = 2 -- ISSN

-- Return the final result set
SELECT * FROM #tmpOpenUrlCitation ORDER BY SegmentTitle, Volume, Date, StartPage

END







