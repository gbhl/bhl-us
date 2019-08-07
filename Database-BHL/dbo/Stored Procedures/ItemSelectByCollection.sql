CREATE PROCEDURE [dbo].[ItemSelectByCollection]

@CollectionID int

AS

BEGIN

SELECT	i.ItemID,
		i.BarCode,
		t.ShortTitle,
		i.Volume,
		t.PublicationDetails,
		dbo.fnAuthorStringForTitle(i.PrimaryTitleID) AS CreatorTextString,
		c.Subjects AS KeywordString,
		c.ItemContributors AS ContributorTextString,
		dbo.fnGetPDFFilenameForItem(i.ItemID) AS PdfFilename,
		ic.CreationDate
FROM	dbo.Item i INNER JOIN dbo.ItemCollection ic
			ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Title t
			ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti
			ON i.ItemID = ti.ItemID
			AND ti.TitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND i.ItemID = c.ItemID
WHERE	ic.CollectionID = @CollectionID
ORDER BY
		t.FullTitle, ti.ItemSequence

END
