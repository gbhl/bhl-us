CREATE PROCEDURE [srchindex].[ItemSelectDocumentForIndex]

@ItemID int

AS 

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpItem
	(
	TitleID int NOT NULL,
	BookID int NOT NULL,
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
	Notes nvarchar(max) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	EditionStatement nvarchar(450) NOT NULL,
	PublicationDetails nvarchar(255) NOT NULL,
	PublisherName nvarchar(300) NOT NULL,
	PublisherPlace nvarchar(200) NOT NULL,
	BookIsVirtual int NOT NULL,
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
	Barcode nvarchar(200) NOT NULL,
	FirstPageID int NULL
	)

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

INSERT	#tmpItem
SELECT DISTINCT
		t.TitleID,
		bk.BookID,
		bi.ItemID,
		t.FullTitle,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 1)) + ' ', '') AS Authors,
		dbo.fnAuthorFacetStringForTitle(t.TitleID) AS FacetAuthors,
		ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(t.TitleID, 0)) + ' ', '') AS SearchAuthors,
		ISNULL(RTRIM(dbo.fnKeywordStringForTitle(t.TitleID)) + ' ', '') AS Subjects,
		ISNULL(RTRIM(dbo.fnAssociationStringForTitle(t.TitleID)) + ' ', '') AS Associations,
		ISNULL(RTRIM(dbo.fnVariantStringForTitle(t.TitleID)) + ' ', '') AS Variants,
		ISNULL(RTRIM(dbo.fnContributorStringForItem(bi.ItemID)) + ' ', '') AS Contributors,
		ISNULL(RTRIM(dbo.fnContributorStringForTitle(t.TitleID, 1)) + ' ', '') AS TitleContributors,
		ISNULL(RTRIM(dbo.fnNoteIndexStringForTitle(t.TitleID, '')) + ' ', '') AS Notes,
		ISNULL(bk.Volume, '') AS Volume,
		ISNULL(t.EditionStatement, '') AS EditionStatement,
		ISNULL(t.PublicationDetails, '') AS PublicationDetails,
		ISNULL(RTRIM(t.Datafield_260_b) + ' ', '') AS PublisherName,
		ISNULL(RTRIM(t.Datafield_260_a) + ' ', '') AS PublisherPlace,
		bk.IsVirtual AS BookIsVirtual,
		CASE WHEN s.SegmentID IS NULL THEN 0 ELSE 1 END AS HasSegments,
		0 AS HasLocalContent,
		CASE WHEN ISNULL(RTRIM(bk.ExternalUrl), '') = '' THEN 0 ELSE 1 END AS HasExternalContent,
		0 AS HasIllustrations,
		ISNULL(t.UniformTitle, '') AS UniformTitle,
		-- Remove diacritics and force to lowercase for SortTitle
		LOWER(ISNULL(TRANSLATE(REPLACE(REPLACE(t.SortTitle, 'Œ', 'OE'), 'œ', 'oe') COLLATE Latin1_General_CS_AI, 'AEIOUCDNRSTXYZaeioucdnrstxyz', 'AEIOUCDNRSTXYZaeioucdnrstxyz'), '')) AS SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(l.LanguageName, '') AS LanguageName,
		ISNULL(b.BibliographicLevelLabel, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		ISNULL(ti.IdentifierValue, '') AS DOIName,
		ISNULL(bk.ExternalUrl, '') AS Url,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'OCLC') AS OCLC,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISSN') AS ISSN,
		dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISBN') AS ISBN,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, bi.ItemID) AS Collections,
		ISNULL(
			CASE 
			WHEN ISNULL(bk.StartYear, '') = '' AND ISNULL(bk.EndYear, '') = '' THEN
				CASE 
					WHEN t.StartYear IS NULL AND t.EndYear IS NULL THEN ''
					WHEN t.StartYear IS NULL THEN CONVERT(nvarchar(20), t.EndYear)
					ELSE CONVERT(nvarchar(20), t.StartYear) + CASE WHEN t.EndYear IS NULL THEN '' ELSE '-' + CONVERT(nvarchar(20), t.EndYear) END
				END
			WHEN ISNULL(bk.StartYear, '') = '' THEN bk.EndYear
			ELSE bk.StartYear + CASE WHEN ISNULL(bk.EndYear, '') = '' THEN '' ELSE '-' + bk.EndYear END
			END
			, '') as [Date],
		bk.Barcode,
		bk.ThumbnailPageID AS FirstPageID
FROM	dbo.Item bi WITH (NOLOCK)
		INNER JOIN dbo.Book bk WITH (NOLOCK) ON bi.ItemID = bk.ItemID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON bi.ItemID = it.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
		LEFT JOIN dbo.language l WITH (NOLOCK) ON bk.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m WITH(NOLOCK) ON t.MaterialTypeID = m.MaterialTypeID
		LEFT JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @IdentifierIDDOI
		LEFT JOIN dbo.ItemRelationship ir ON bi.ItemID = ir.ParentID
		LEFT JOIN dbo.Item si ON ir.ChildID = si.ItemID
		LEFT JOIN dbo.Segment s ON si.ItemID = s.ItemID AND si.ItemStatusID IN (30, 40)
WHERE	t.PublishReady = 1
AND		bi.ItemStatusID = 40
AND		bk.BookID = @ItemID

UPDATE	#tmpItem
SET		HasLocalContent = 1
FROM	#tmpItem t 
		INNER JOIN dbo.ItemPage ip ON t.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID

-- Get the first page ID
CREATE TABLE #tmpPages (ItemID int NOT NULL, SequenceOrder int NULL)

INSERT INTO #tmpPages
SELECT	i.ItemID,
		MIN(ip.SequenceOrder) AS SequenceOrder
FROM	#tmpItem i 
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
		INNER JOIN Page_PageType ppt ON p.PageID = ppt.PageID
WHERE	p.Active = 1
AND		ppt.PageTypeID = 1 -- Use the first Title Page in the book
GROUP BY i.ItemID

UPDATE	#tmpItem
SET		FirstPageID = ip.PageID
FROM	#tmpItem i
		INNER JOIN #tmpPages t ON i.ItemID = t.ItemID
		INNER JOIN dbo.ItemPage ip ON t.ItemID = ip.ItemID AND t.SequenceOrder = ip.SequenceOrder
WHERE	i.FirstPageID IS NULL

UPDATE	#tmpItem
SET		FirstPageID = ip.PageID	-- Just use the first physical page in the book if no title page found
FROM	#tmpItem i
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON i.ItemID = ip.ItemID AND ip.SequenceOrder = 1
WHERE	i.FirstPageID IS NULL


-- Now get the first page IDs for Virtual Items
CREATE TABLE #tmpSegmentPages (RowNum int NOT NULL, SegmentSequenceOrder int NULL, PageSequenceOrder int NULL, ItemID int NOT NULL, PageID int NOT NULL)

INSERT	#tmpSegmentPages
SELECT	ROW_NUMBER() OVER (ORDER BY ir.SequenceOrder, ip.SequenceOrder) AS RowNum,  
		ir.SequenceOrder AS SegmentSequenceOrder, ip.SequenceOrder AS PageSequenceOrder, i.ItemID, p.PageID
FROM	#tmpItem i
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID AND b.IsVirtual = 1
		INNER JOIN dbo.ItemRelationship ir WITH (NOLOCK) ON i.ItemID = ir.ParentID
		INNER JOIN dbo.vwSegment s WITH (NOLOCK) ON ir.ChildID = s.ItemID
		INNER JOIN dbo.ItemPage ip WITH (NOLOCK) ON s.ItemID = ip.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON ip.PageID = p.PageID
WHERE	i.FirstPageID IS NULL
AND		s.SegmentStatusID IN (30, 40)
AND		p.Active = 1

UPDATE	i
SET		FirstPageID = sp.PageID
FROM	#tmpSegmentPages sp
		INNER JOIN (
			SELECT	MIN(RowNum) AS RowNum, sp.ItemID
			FROM	#tmpSegmentPages sp
					INNER JOIN Page_PageType ppt WITH (NOLOCK) ON sp.PageID = ppt.PageID
			WHERE	ppt.PageTypeID = 1 -- Use the first Title Page for each item
			GROUP BY sp.ItemID 
			) x ON sp.RowNum = x.RowNum
		INNER JOIN #tmpItem i ON sp.ItemID = i.ItemID

UPDATE	#tmpItem
SET		FirstPageID = sp.PageID	-- Just use the first physical page in the book if no title page found
FROM	#tmpItem i
		INNER JOIN #tmpSegmentPages sp ON i.ItemID = sp.ItemID AND sp.SegmentSequenceOrder = 1 AND sp.PageSequenceOrder = 1
WHERE	i.FirstPageID IS NULL


-- Return the final result set
SELECT	TitleID,
		BookID AS ItemID,
		FullTitle,
		Authors,
		FacetAuthors,
		SearchAuthors,
		Subjects,
		Associations,
		Variants,
		Contributors,
		TitleContributors,
		Notes,
		Volume,
		EditionStatement,
		PublicationDetails,
		PublisherName,
		PublisherPlace,
		BookIsVirtual,
		HasSegments,
		HasLocalContent,
		HasExternalContent,
		HasIllustrations,
		UniformTitle,
		SortTitle,
		PartNumber,
		PartName,
		LanguageName,
		BibliographicLevelName,
		MaterialTypeLabel,
		DOIName,
		Url,
		OCLC,
		ISSN,
		ISBN,
		Collections,
		[Date],
		Barcode,
		FirstPageID
FROM	#tmpItem

END

GO
