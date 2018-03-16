CREATE PROCEDURE [srchindex].[ItemSelectDocumentForIndex]

@ItemID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		i.ItemID,
		t.FullTitle,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForTitle(t.TitleID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForTitle(t.TitleID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnAssociationStringForTitle(t.TitleID)) + ' ', '') AS Associations,
		ISNULL(RTRIM(dbo.fnVariantStringForTitle(t.TitleID)) + ' ', '') AS Variants,
		ISNULL(RTRIM(dbo.fnContributorStringForItem(i.ItemID)) + ' ', '') AS Contributors,
		i.Volume,
		ISNULL(RTRIM(t.Datafield_260_b) + ' ', '') AS PublisherName,
		ISNULL(RTRIM(t.Datafield_260_a) + ' ', '') AS PublisherPlace,
		CASE WHEN s.SegmentID IS NULL THEN 0 ELSE 1 END AS HasSegments,
		0 AS HasLocalContent,
		CASE WHEN ISNULL(RTRIM(i.ExternalUrl), '') = '' THEN 0 ELSE 1 END AS HasExternalContent,
		0 AS HasIllustrations,
		ISNULL(t.UniformTitle, '') AS UniformTitle,
		ISNULL(t.SortTitle, '') AS SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(l.LanguageName, '') AS LanguageName,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		ISNULL(d.DOIName, '') AS DOIName,
		ISNULL(i.ExternalUrl, '') AS Url,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'OCLC') AS OCLC,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISSN') AS ISSN,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISBN') AS ISBN,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections,
		ISNULL(CASE WHEN ISNULL(i.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE i.Year END, '') as [Date],
		i.Barcode
INTO	#tmpItem
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ti.TitleID = t.TitleID
		LEFT JOIN dbo.language l WITH (NOLOCK) ON i.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m WITH(NOLOCK) ON t.MaterialTypeID = m.MaterialTypeID
		LEFT JOIN dbo.DOI d WITH (NOLOCK) ON t.TitleID = d.EntityID AND d.DOIStatusID IN (100, 200) AND d.DOIEntityTypeID = 10
		LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID AND s.SegmentStatusID IN (10, 20)
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		i.ItemID = @ItemID

UPDATE	#tmpItem
SET		HasLocalContent = 1
FROM	#tmpItem t INNER JOIN dbo.Page p ON t.ItemID = p.ItemID

/* 
-- *** Add this back in if/when BHL starts using the "HasIllustrations" field in the search index
UPDATE	#tmpItem
SET		HasIllustrations = x.HasIllustrations
FROM	#tmpItem i INNER JOIN (
			SELECT	t.ItemID, MAX(CASE WHEN ppt.PageID IS NULL THEN 0 ELSE 1 END) AS HasIllustrations
			FROM	#tmpItem t
					INNER JOIN dbo.Page p ON t.ItemID = p.ItemID
					-- 3 = Illustration
					-- 10 = Map
					-- 14 = Foldout
					-- 18 = Drawing
					-- 19 = Table
					-- 20 = Photograph
					LEFT JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID AND ppt.PageTypeID IN (3, 10, 14, 18, 19, 20)
			GROUP BY
					t.ItemID
			) x ON i.ItemID = x.ItemID
*/

SELECT * FROM #tmpItem

END
