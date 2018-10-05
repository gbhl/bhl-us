CREATE PROCEDURE [dbo].[ApiPageSelectByNameBankID]

@NameBankID nvarchar(100)

AS 

SET NOCOUNT ON

/*
 This procedure supports the API v2 set of Name methods.
*/

DECLARE @NameBank int
SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank';

-- Get the detail for the specified NameBankID
SELECT	ni.IdentifierValue AS NameBankID, nr.ResolvedNameString,
		t.TitleID, t.ShortTitle, t.CallNumber, t.Datafield_260_a AS PublisherPlace, 
		t.Datafield_260_b AS PublisherName, t.Datafield_260_c AS PublicationDate, 
		'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL,
		i.ItemID, s.SourceName, i.Barcode, i.Volume AS VolumeInfo, c.ItemContributors AS InstitutionName,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL,
		p.PageID, p.[Year], p.Volume, p.Issue,
		ip.PagePrefix, ip.PageNumber,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(nvarchar(20), p.PageID) AS PageURL,
		'https://www.biodiversitylibrary.org/pagethumb/' + CONVERT(nvarchar(20), p.PageID) AS ThumbnailURL,
		'https://www.biodiversitylibrary.org/pageimage/' + CONVERT(nvarchar(20), p.PageID) AS FullSizeImageURL,
		'https://www.biodiversitylibrary.org/pagetext/' + CONVERT(nvarchar(20), p.PageID) AS OcrURL,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
FROM	dbo.NameIdentifier ni WITH (NOLOCK)
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON ni.NameResolvedID = nr.NameResolvedID
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.ItemSource s WITH (NOLOCK) ON i.ItemSourceID = s.ItemSourceID
		INNER JOIN dbo.Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = i.ItemID
WHERE	ni.IdentifierValue = @NameBankID
AND		ni.IdentifierID = @NameBank
ORDER BY
		t.SortTitle, i.ItemID, p.[Year], p.Volume, ip.PageNumber
