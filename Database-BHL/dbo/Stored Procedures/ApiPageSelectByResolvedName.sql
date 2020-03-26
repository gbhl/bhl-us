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
IF (@CanonicalNameString IS NULL) SELECT @CanonicalNameString = CanonicalNameString FROM dbo.NameResolved WHERE CanonicalNameString = @ResolvedNameString

-- Get the detail for the specified NameBankID
SELECT DISTINCT
		ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString, nr.CanonicalNameString, nr.CreationDate,
		t.TitleID, t.MARCBibID, t.ShortTitle, t.SortTitle,
		CASE WHEN ISNULL(i.CallNumber, '') = '' THEN t.CallNumber else i.CallNumber END AS CallNumber, 
		t.Datafield_260_a AS PublisherPlace, 
		t.Datafield_260_b AS PublisherName, t.Datafield_260_c AS PublicationDate, 
		t.TL2Author, bph.IdentifierValue AS BPH, tl2.IdentifierValue AS TL2, 
		abbrev.IdentifierValue AS Abbreviation,
		'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
		i.ItemID, s.SourceName, i.Barcode, i.MARCItemID, i.Volume AS VolumeInfo, 
		c.ItemContributors AS InstitutionName,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL,
		p.PageID, p.[Year], p.Volume, p.Issue,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		ip.PagePrefix, ip.PageNumber,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
		'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
		'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
		'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
INTO	#Final
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
		LEFT JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.ItemSource s WITH (NOLOCK) ON i.ItemSourceID = s.ItemSourceID
		INNER JOIN Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = i.ItemID
		LEFT JOIN Title_Identifier	abbrev WITH (NOLOCK)
			ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
		LEFT JOIN Title_Identifier bph WITH (NOLOCK)
			ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
		LEFT JOIN Title_Identifier tl2 WITH (NOLOCK)
			ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
WHERE	nr.CanonicalNameString = @CanonicalNameString

SELECT	NameBankID, NameResolvedID, ResolvedNameString, CanonicalNameString, CreationDate, TitleID, MARCBibID, ShortTitle, 
		CallNumber, PublisherPlace, PublisherName, PublicationDate, TL2Author, BPH, TL2, Abbreviation, TitleURL, 
		ItemID, SourceName, Barcode, MARCItemID, VolumeInfo, InstitutionName, ItemURL, PageID, [Year], Volume, 
		Issue, TextSource, PagePrefix, PageNumber, PageURL, ThumbnailURL, FullSizeImageURL, OcrURL, PageTypeName
FROM	#Final
ORDER BY
		SortTitle, ItemID, [Year], Volume, PageNumber
