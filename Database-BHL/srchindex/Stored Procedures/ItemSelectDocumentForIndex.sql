CREATE PROCEDURE [srchindex].[ItemSelectDocumentForIndex]

@ItemID int

AS 

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpItem
	(
	TitleID int NOT NULL,
	ItemID int NOT NULL,
	FullTitle nvarchar(2000) NOT NULL,
	Authors nvarchar(max) NOT NULL,
	FacetAuthors nvarchar(max) NOT NULL,
	SearchAuthors nvarchar(max) NOT NULL,
	Subjects nvarchar(max) NOT NULL,
	Associations nvarchar(max) NOT NULL,
	Variants nvarchar(max) NOT NULL,
	Contributors nvarchar(max) NOT NULL,
	TitleContributors nvarchar(max) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	EditionStatement nvarchar(450) NOT NULL,
	PublicationDetails nvarchar(255) NOT NULL,
	PublisherName nvarchar(300) NOT NULL,
	PublisherPlace nvarchar(200) NOT NULL,
	HasSegments int NOT NULL,
	HasLocalContent int NOT NULL,
	HasExternalContent int NOT NULL,
	HasIllustrations int NOT NULL,
	UniformTitle nvarchar(255) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	PartNumber nvarchar(255) NOT NULL,
	PartName nvarchar(255) NOT NULL,
	LanguageName nvarchar(20) NOT NULL,
	BibliographicLevelName nvarchar(50) NOT NULL,
	MaterialTypeLabel nvarchar(60) NOT NULL,
	DOIName nvarchar(50) NOT NULL,
	Url nvarchar(500) NOT NULL,
	OCLC nvarchar(max) NOT NULL,
	ISSN nvarchar(max) NOT NULL,
	ISBN nvarchar(max) NOT NULL,
	Collections nvarchar(max) NOT NULL,
	[Date] nvarchar(20) NOT NULL,
	Barcode nvarchar(40) NOT NULL,
	FirstPageID int NULL
	)

INSERT	#tmpItem
SELECT DISTINCT
		t.TitleID,
		i.ItemID,
		t.FullTitle,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForTitle(t.TitleID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForTitle(t.TitleID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnAssociationStringForTitle(t.TitleID)) + ' ', '') AS Associations,
		ISNULL(RTRIM(dbo.fnVariantStringForTitle(t.TitleID)) + ' ', '') AS Variants,
		ISNULL(RTRIM(dbo.fnContributorStringForItem(i.ItemID)) + ' ', '') AS Contributors,
		ISNULL(RTRIM(dbo.fnContributorStringForTitle(t.TitleID, 1)) + ' ', '') AS TitleContributors,
		ISNULL(i.Volume, '') AS Volume,
		ISNULL(t.EditionStatement, '') AS EditionStatement,
		ISNULL(t.PublicationDetails, '') AS PublicationDetails,
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
		i.Barcode,
		NULL AS FirstPageID
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

-- Get the first page ID
CREATE TABLE #tmpPages (ItemID int NOT NULL, SequenceOrder int NULL)

INSERT INTO #tmpPages
SELECT	i.ItemID,
		MIN(p.SequenceOrder) AS SequenceOrder
FROM	#tmpItem i 
		INNER JOIN dbo.Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
		INNER JOIN Page_PageType ppt ON p.PageID = ppt.PageID
WHERE	p.Active = 1
AND		ppt.PageTypeID = 1 -- Use the first Title Page in the book
GROUP BY i.ItemID

UPDATE	#tmpItem
SET		FirstPageID = p.PageID
FROM	#tmpItem i
		INNER JOIN #tmpPages t ON i.ItemID = t.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON t.ItemID = p.ItemID AND t.SequenceOrder = p.SequenceOrder

UPDATE	#tmpItem
SET		FirstPageID = p.PageID	-- Just use the first physical page in the book if no title page found
FROM	#tmpItem i
		INNER JOIN dbo.Page p WITH (NOLOCK) ON p.ItemID = i.ItemID AND p.SequenceOrder = 1
WHERE	i.FirstPageID IS NULL

-- Return the final result set
SELECT * FROM #tmpItem

END
