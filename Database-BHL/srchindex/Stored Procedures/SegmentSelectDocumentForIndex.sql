CREATE PROCEDURE [srchindex].[SegmentSelectDocumentForIndex]

@ItemID int,
@SegmentID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		RTRIM(s.Title) AS Title,
		RTRIM(s.TranslatedTitle) AS TranslatedTitle,
		ISNULL(s.SortTitle, '') AS SortTitle,
		ISNULL(CASE WHEN t.FullTitle IS NULL THEN s.ContainerTitle COLLATE SQL_Latin1_General_CP1_CI_AI ELSE t.FullTitle END, '') AS ContainerTitle,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForSegment(s.SegmentID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(s.SegmentID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForSegment(s.SegmentID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnContributorStringForSegment(s.SegmentID)) + ' ', '') AS Contributors,
		RTRIM(s.Volume) AS Volume,
		RTRIM(s.Series) AS Series,
		RTRIM(s.Issue) AS Issue,
		ISNULL(RTRIM(t.Datafield_260_b) + ' ', '') AS PublisherName,
		ISNULL(RTRIM(t.Datafield_260_a) + ' ', '') AS PublisherPlace,
		0 AS HasLocalContent,
		RTRIM(CASE WHEN s.Url = '' THEN 0 ELSE 1 END) AS HasExternalContent,
		0 AS HasIllustrations,
		ISNULL(l.LanguageName, '') AS LanguageName,
		g.GenreName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		ISNULL(d.DOIName, '') AS DOIName,
		ISNULL(s.Url, '') AS Url,
		s.StartPageID,
		CASE WHEN ISNULL(PageRange, '') = '' THEN StartPageNumber +'--' + EndPageNumber ELSE PageRange END AS PageRange,
		RTRIM(s.[Date]) AS [Date]
INTO	#tmpSegment
FROM	dbo.Segment s WITH(NOLOCK)
		LEFT JOIN dbo.Language l WITH(NOLOCK) ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentGenre g WITH(NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.DOI d WITH(NOLOCK) ON s.SegmentID = d.EntityID AND d.DOIStatusID IN(100, 200) AND d.DOIEntityTypeID = 40
		LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
WHERE	(s.ItemID = @ItemID OR @ItemID IS NULL)
AND		(s.SegmentID = @SegmentID OR @SegmentID IS NULL)

UPDATE	#tmpSegment
SET		HasLocalContent = 1
FROM	#tmpSegment t INNER JOIN dbo.SegmentPage p ON t.SegmentID = p.SegmentID

/* 
-- *** Add this back in if/when BHL starts using the "HasIllustrations" field in the search index
UPDATE	#tmpSegment
SET		HasIllustrations = x.HasIllustrations
FROM	#tmpSegment s INNER JOIN (
			SELECT	t.SegmentID, MAX(CASE WHEN ppt.PageID IS NULL THEN 0 ELSE 1 END) AS HasIllustrations
			FROM	#tmpSegment t
					INNER JOIN dbo.SegmentPage p ON t.SegmentID = p.SegmentID
					-- 3 = Illustration
					-- 10 = Map
					-- 14 = Foldout
					-- 18 = Drawing
					-- 19 = Table
					-- 20 = Photograph
					LEFT JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID AND ppt.PageTypeID IN (3, 10, 14, 18, 19, 20)
			GROUP BY
					t.SegmentID
			) x ON s.SegmentID = x.SegmentID
*/

SELECT * FROM #tmpSegment

END
