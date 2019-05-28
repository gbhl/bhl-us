CREATE PROCEDURE [dbo].[ApiPageSelectByNameIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

SET NOCOUNT ON

DECLARE @Abbreviation int
DECLARE @BPH int
DECLARE @TL2 int
DECLARE @IdentifierID int

SELECT @Abbreviation = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'Abbreviation'
SELECT @BPH = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'BPH'
SELECT @TL2 = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'TL2'
SELECT @IdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = @IdentifierName

-- Get the detail for the specified NameBankID
SELECT	ni.IdentifierValue AS NameBankID, nr.NameResolvedID, nr.ResolvedNameString,
		t.TitleID, t.MARCBibID, t.ShortTitle, 
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
		-- Image viewer address
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(s.ImageServerUrlFormat, '{0}', ''), '{1}', ''), '{2}', ''), '{3}', 'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID)), '{4}', 'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID)), '&amp;', '&') AS ImageURL,
		'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
		pt.PageTypeName
FROM	dbo.NameIdentifier ni WITH (NOLOCK)
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON ni.NameResolvedID = nr.NameResolvedID
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		INNER JOIN Page p WITH (NOLOCK) ON np.PageID = p.PageID
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
		LEFT JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN ItemSource s WITH (NOLOCK) ON i.ItemSourceID = s.ItemSourceID
		INNER JOIN Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = i.ItemID
		LEFT JOIN Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
		LEFT JOIN PageType pt WITH (NOLOCK) ON ppt.PageTypeID = pt.PageTypeID
		LEFT JOIN Title_Identifier	abbrev WITH (NOLOCK)
			ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
		LEFT JOIN Title_Identifier bph WITH (NOLOCK)
			ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
		LEFT JOIN Title_Identifier tl2 WITH (NOLOCK)
			ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
WHERE	ni.IdentifierValue = @IdentifierValue
AND		ni.IdentifierID = @IdentifierID
ORDER BY
		t.SortTitle, i.ItemID, p.[Year], p.Volume, ip.PageNumber
