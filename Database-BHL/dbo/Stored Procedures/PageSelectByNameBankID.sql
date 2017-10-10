
CREATE PROCEDURE [dbo].[PageSelectByNameBankID]

@NameBankID nvarchar(100)

AS 

SET NOCOUNT ON

DECLARE @Abbreviation int
DECLARE @BPH int
DECLARE @TL2 int
DECLARE @NameBank int

SELECT @Abbreviation = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'Abbreviation'
SELECT @BPH = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'BPH'
SELECT @TL2 = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'TL2'
SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank';

-- Get the detail for the specified NameBankID
SELECT	ni.IdentifierValue AS NameBankID, nr.ResolvedNameString,
		t.TitleID, t.MARCBibID, t.ShortTitle, t.PublicationDetails, t.TL2Author, 
		bph.IdentifierValue AS BPH, tl2.IdentifierValue AS TL2, 
		abbrev.IdentifierValue AS Abbreviation,
		'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
		i.ItemID, i.BarCode, i.MARCItemID, i.CallNumber, i.Volume AS VolumeInfo,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL,
		p.PageID, p.[Year], p.Volume, p.Issue,
		ip.PagePrefix, ip.PageNumber,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
		'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
		'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
		-- Image viewer address
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(its.ImageServerUrlFormat, '{0}', ''), '{1}', ''), '{2}', ''), '{3}', 'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID)), '{4}', 'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID)), '&amp;', '&') AS ImageURL,
		'https://www.biodiversitylibrary.org/pageocr/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
		pt.PageTypeName
FROM	dbo.NameIdentifier ni WITH (NOLOCK)
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON ni.NameResolvedID = nr.NameResolvedID
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		INNER JOIN Page p WITH (NOLOCK) ON np.PageID = p.PageID
		LEFT JOIN IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
		INNER JOIN ItemSource its WITH (NOLOCK) ON i.ItemSourceID = its.ItemSourceID
		LEFT JOIN Page_PageType ppt WITH (NOLOCK) ON p.PageID = ppt.PageID
		LEFT JOIN PageType pt WITH (NOLOCK) ON ppt.PageTypeID = pt.PageTypeID
		LEFT JOIN Title_Identifier	abbrev WITH (NOLOCK)
			ON t.TitleID = abbrev.TitleID AND abbrev.IdentifierID = @Abbreviation
		LEFT JOIN Title_Identifier bph WITH (NOLOCK)
			ON t.TitleID = bph.TitleID AND bph.IdentifierID = @BPH
		LEFT JOIN Title_Identifier tl2 WITH (NOLOCK)
			ON t.TitleID = tl2.TitleID AND tl2.IdentifierID = @TL2
WHERE	ni.IdentifierValue = @NameBankID
AND		ni.IdentifierID = @NameBank
ORDER BY
		t.SortTitle, i.ItemID, p.[Year], p.Volume, ip.PageNumber


