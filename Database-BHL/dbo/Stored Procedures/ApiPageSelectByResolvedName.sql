SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiPageSelectByResolvedName]

@ResolvedNameString nvarchar(100)

AS 

SET NOCOUNT ON

DECLARE @Abbreviation int
DECLARE @BPH int
DECLARE @TL2 int
DECLARE @IdentifierID int
DECLARE @NameBank int

SELECT @Abbreviation = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'Abbreviation'
SELECT @BPH = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'BPH'
SELECT @TL2 = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'TL2'
SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank';

-- Use the Canonical form of the Resolved name to search for pages
DECLARE @CanonicalNameString nvarchar(100)
SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE ResolvedNameString = @ResolvedNameString

-- If no name found, see if the argument matches a Canonical form
IF (@CanonicalNameString IS NULL) SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE CanonicalNameString = @ResolvedNameString;

WITH CTE AS (

	-- Get the detail for the specified Resolved Name
	SELECT	ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString, nr.CanonicalNameString, nr.CreationDate,
			t.TitleID, t.MARCBibID, t.ShortTitle, t.SortTitle,
			CASE WHEN ISNULL(b.CallNumber, '') = '' THEN t.CallNumber else b.CallNumber END AS CallNumber, 
			t.Datafield_260_a AS PublisherPlace, 
			t.Datafield_260_b AS PublisherName, t.Datafield_260_c AS PublicationDate, 
			t.TL2Author, bph.IdentifierValue AS BPH, tl2.IdentifierValue AS TL2, 
			abbrev.IdentifierValue AS Abbreviation,
			'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
			b.BookID AS ItemID, s.SourceName, b.Barcode, b.MARCItemID, b.Volume AS VolumeInfo, 
			c.ItemContributors AS InstitutionName,
			'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), b.BookID) AS ItemURL,
			it.ItemSequence,
			NULL AS SegmentID, NULL AS Title, NULL AS SegmentSourceName, NULL AS SegmentBarcode, 
			NULL AS SegmentContributors, NULL AS StartPageNumber, NULL AS EndPageNumber,
			NULL AS SegmentURL, null as SegmentSequence, 
			ip.Sequence AS PageSequence,
			p.PageID, p.[Year], p.Volume, p.Issue,
			COALESCE(l.TextSource, 'OCR') AS TextSource,
			ip.PagePrefix, ip.PageNumber,
			'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
			'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
			'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
			'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
			dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
	FROM	dbo.NameResolved nr WITH (NOLOCK) 
			LEFT JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
			INNER JOIN Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			INNER JOIN Page p WITH (NOLOCK)	ON np.PageID = p.PageID
			OUTER APPLY (
					SELECT  TOP 1 TextSource
					FROM    dbo.PageTextLog 
					WHERE   PageID = p.PageID
					ORDER BY PageTextLogID DESC
				) l
			LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN dbo.ItemPage ipg WITH (NOLOCK) ON p.PageID = ipg.PageID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ipg.ItemID = i.ItemID
			INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemSource s WITH (NOLOCK) ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON b.ItemID = it.ItemID AND it.IsPrimary = 1
			INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
			INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = b.BookID
			LEFT JOIN Title_Identifier	abbrev WITH (NOLOCK)
				ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
			LEFT JOIN Title_Identifier bph WITH (NOLOCK)
				ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
			LEFT JOIN Title_Identifier tl2 WITH (NOLOCK)
				ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
	WHERE	nr.CanonicalNameString = @CanonicalNameString

	UNION

	SELECT	ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString, nr.CanonicalNameString, nr.CreationDate,
			t.TitleID, t.MARCBibID, t.ShortTitle, t.SortTitle,
			CASE WHEN ISNULL(b.CallNumber, '') = '' THEN t.CallNumber else b.CallNumber END AS CallNumber, 
			t.Datafield_260_a AS PublisherPlace, 
			t.Datafield_260_b AS PublisherName, t.Datafield_260_c AS PublicationDate, 
			t.TL2Author, bph.IdentifierValue AS BPH, tl2.IdentifierValue AS TL2, 
			abbrev.IdentifierValue AS Abbreviation,
			'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
			b.BookID AS ItemID, NULL AS SourceName, NULL AS Barcode, NULL AS MARCItemID, b.Volume AS VolumeInfo, 
			NULL AS InstitutionName,
			'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), b.BookID) AS ItemURL,
			it.ItemSequence,
			s.SegmentID, s.Title, src.SourceName, s.BarCode, c.Contributors, s.StartPageNumber, s.EndPageNumber,
			'https://www.biodiversitylibrary.org/part/' + CONVERT(nvarchar(20), s.SegmentID) AS SegmentURL,
			ir.SequenceOrder,
			ip.Sequence,
			p.PageID, p.[Year], p.Volume, p.Issue,
			COALESCE(l.TextSource, 'OCR') AS TextSource,
			ip.PagePrefix, ip.PageNumber,
			'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
			'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
			'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
			'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
			dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
	FROM	dbo.NameResolved nr WITH (NOLOCK) 
			LEFT JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
			INNER JOIN Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			INNER JOIN Page p WITH (NOLOCK)	ON np.PageID = p.PageID
			OUTER APPLY (
					SELECT  TOP 1 TextSource
					FROM    dbo.PageTextLog 
					WHERE   PageID = p.PageID
					ORDER BY PageTextLogID DESC
				) l
			LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
			INNER JOIN dbo.ItemPage ipg WITH (NOLOCK) ON p.PageID = ipg.PageID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ipg.ItemID = i.ItemID
			INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
			INNER JOIN dbo.ItemRelationship ir WITH (NOLOCK) ON s.ItemID = ir.ChildID
			INNER JOIN dbo.Book b WITH (NOLOCK) ON ir.ParentID = b.ItemID AND b.IsVirtual = 1
			INNER JOIN dbo.Item ib WITH (NOLOCK) ON b.ItemID = ib.ItemID
			INNER JOIN dbo.ItemSource src WITH (NOLOCK) ON i.ItemSourceID = src.ItemSourceID
			INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON b.ItemID = it.ItemID AND it.IsPrimary = 1
			INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
			INNER JOIN dbo.SearchCatalogSegment c ON c.SegmentID = s.SegmentID
			LEFT JOIN Title_Identifier	abbrev WITH (NOLOCK)
				ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
			LEFT JOIN Title_Identifier bph WITH (NOLOCK)
				ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
			LEFT JOIN Title_Identifier tl2 WITH (NOLOCK)
				ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
	WHERE	nr.CanonicalNameString = @CanonicalNameString

)
SELECT	*
INTO	#Final
FROM	CTE

SELECT	NameBankID, NameResolvedID, ResolvedNameString, CanonicalNameString, CreationDate, TitleID, MARCBibID, ShortTitle, 
		CallNumber, PublisherPlace, PublisherName, PublicationDate, TL2Author, BPH, TL2, Abbreviation, TitleURL, 
		ItemID, SourceName, Barcode, MARCItemID, VolumeInfo, InstitutionName, ItemURL,		
		SegmentID, Title, SegmentSourceName, SegmentBarcode, SegmentContributors, StartPageNumber, EndPageNumber, SegmentURL,
		PageID, [Year], Volume, Issue, TextSource, PagePrefix, PageNumber, PageURL, ThumbnailURL, FullSizeImageURL, OcrURL, PageTypeName
FROM	#Final
ORDER BY
		SortTitle, ItemSequence, SegmentSequence, PageSequence

GO
