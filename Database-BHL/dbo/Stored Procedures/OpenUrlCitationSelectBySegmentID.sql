
CREATE PROCEDURE [dbo].[OpenUrlCitationSelectBySegmentID]

@SegmentID int

AS

BEGIN

SET NOCOUNT ON

-- If the segment has been redirected to a different segment, then use
-- that segment instead.  Follow the "redirect" chain up to ten levels.
SELECT	@SegmentID = COALESCE(s10.SegmentID, s9.SegmentID, s8.SegmentID, s7.SegmentID, s6.SegmentID,
						s5.SegmentID, s4.SegmentID, s3.SegmentID, s2.SegmentID, s1.SegmentID)
FROM	dbo.Segment s1
		LEFT JOIN dbo.Segment s2 ON s1.RedirectSegmentID = s2.SegmentID
		LEFT JOIN dbo.Segment s3 ON s2.RedirectSegmentID = s3.SegmentID
		LEFT JOIN dbo.Segment s4 ON s3.RedirectSegmentID = s4.SegmentID
		LEFT JOIN dbo.Segment s5 ON s4.RedirectSegmentID = s5.SegmentID
		LEFT JOIN dbo.Segment s6 ON s5.RedirectSegmentID = s6.SegmentID
		LEFT JOIN dbo.Segment s7 ON s6.RedirectSegmentID = s7.SegmentID
		LEFT JOIN dbo.Segment s8 ON s7.RedirectSegmentID = s8.SegmentID
		LEFT JOIN dbo.Segment s9 ON s8.RedirectSegmentID = s9.SegmentID
		LEFT JOIN dbo.Segment s10 ON s9.RedirectSegmentID = s10.SegmentID
WHERE	s1.SegmentID = @SegmentID


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






