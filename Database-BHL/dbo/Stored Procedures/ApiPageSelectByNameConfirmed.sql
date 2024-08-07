CREATE PROCEDURE [dbo].[ApiPageSelectByNameConfirmed]

@NameConfirmed nvarchar(100)

AS 

SET NOCOUNT ON

/*
 This procedure supports the API v2 set of Name methods.
*/

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
SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE ResolvedNameString = @NameConfirmed

-- If no name found, see if the argument matches a Canonical form
IF (@CanonicalNameString IS NULL) SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE CanonicalNameString = @NameConfirmed

;WITH CTE AS (

	-- Get the detail for the specified name string
	SELECT	ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString,
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
			p.PageID, p.[Year], p.Volume, p.Issue,
			COALESCE(l.TextSource, 'OCR') AS TextSource,
			ip.PagePrefix, ip.PageNumber,
			'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
			'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
			'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
			'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
			dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
	FROM	dbo.NameResolved nr
			LEFT JOIN dbo.NameIdentifier ni ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
			INNER JOIN Name n ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN NamePage np ON n.NameID = np.NameID
			INNER JOIN Page p ON np.PageID = p.PageID
			OUTER APPLY (
					SELECT  TOP 1 TextSource
					FROM    dbo.PageTextLog 
					WHERE   PageID = p.PageID
					ORDER BY PageTextLogID DESC
				) l
			LEFT JOIN IndicatedPage ip ON p.PageID = ip.PageID
			INNER JOIN dbo.ItemPage ipg ON p.PageID = ipg.PageID
			INNER JOIN Item i ON ipg.ItemID = i.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemSource s ON i.ItemSourceID = s.ItemSourceID
			INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
			INNER JOIN dbo.Title t ON it.TitleID = t.TitleID AND it.IsPrimary = 1
			INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = b.BookID
			LEFT JOIN Title_Identifier	abbrev
				ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
			LEFT JOIN Title_Identifier bph
				ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
			LEFT JOIN Title_Identifier tl2
				ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
	WHERE	nr.CanonicalNameString = @CanonicalNameString

	UNION

	SELECT	ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString,
			t.TitleID, t.MARCBibID, t.ShortTitle, t.SortTitle,
			CASE WHEN ISNULL(b.CallNumber, '') = '' THEN t.CallNumber else b.CallNumber END AS CallNumber, 
			t.Datafield_260_a AS PublisherPlace, 
			t.Datafield_260_b AS PublisherName, t.Datafield_260_c AS PublicationDate, 
			t.TL2Author, bph.IdentifierValue AS BPH, tl2.IdentifierValue AS TL2, 
			abbrev.IdentifierValue AS Abbreviation,
			'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
			b.BookID AS ItemID, src.SourceName, b.Barcode, b.MARCItemID, b.Volume AS VolumeInfo, 
			c.Contributors AS InstitutionName,
			'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), b.BookID) AS ItemURL,
			p.PageID, p.[Year], p.Volume, p.Issue,
			COALESCE(l.TextSource, 'OCR') AS TextSource,
			ip.PagePrefix, ip.PageNumber,
			'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
			'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
			'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
			'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
			dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
	FROM	dbo.NameResolved nr
			LEFT JOIN dbo.NameIdentifier ni ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
			INNER JOIN Name n ON nr.NameResolvedID = n.NameResolvedID
			INNER JOIN NamePage np ON n.NameID = np.NameID
			INNER JOIN Page p ON np.PageID = p.PageID
			OUTER APPLY (
					SELECT  TOP 1 TextSource
					FROM    dbo.PageTextLog 
					WHERE   PageID = p.PageID
					ORDER BY PageTextLogID DESC
				) l
			LEFT JOIN IndicatedPage ip ON p.PageID = ip.PageID
			INNER JOIN dbo.ItemPage ipg ON p.PageID = ipg.PageID
			INNER JOIN Item i ON ipg.ItemID = i.ItemID
			INNER JOIN vwSegment s ON  i.ItemID = s.ItemID
			INNER JOIN dbo.Book b ON s.BookID = b.BookID
			INNER JOIN dbo.ItemSource src ON i.ItemSourceID = src.ItemSourceID
			INNER JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID
			INNER JOIN dbo.Title t ON it.TitleID = t.TitleID AND it.IsPrimary = 1
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
			LEFT JOIN Title_Identifier	abbrev
				ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
			LEFT JOIN Title_Identifier bph
				ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
			LEFT JOIN Title_Identifier tl2
				ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
	WHERE	nr.CanonicalNameString = @CanonicalNameString

)
SELECT	*
INTO	#Final
FROM	CTE

SELECT	NameBankID, NameResolvedID, ResolvedNameString,	TitleID, MARCBibID, ShortTitle, 
		CallNumber, PublisherPlace, PublisherName, PublicationDate, TL2Author, BPH, TL2, 
		Abbreviation, TitleURL, ItemID, SourceName, Barcode, MARCItemID, VolumeInfo, 
		InstitutionName, ItemURL, PageID, [Year], Volume, Issue, TextSource,
		PagePrefix, PageNumber, PageURL, ThumbnailURL, FullSizeImageURL, OcrURL,
		PageTypeName
FROM	#Final
ORDER BY
		SortTitle, ItemID, [Year], VolumeInfo, PageNumber

GO
