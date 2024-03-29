SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectByCollection]

@CollectionID int

AS

BEGIN

SELECT	b.BookID AS ItemID,
		b.BarCode,
		t.ShortTitle,
		b.Volume,
		t.PublicationDetails,
		dbo.fnAuthorStringForTitle(t.TitleID) AS CreatorTextString,
		c.Subjects AS KeywordString,
		c.ItemContributors AS ContributorTextString,
		dbo.fnGetPDFFilenameForItem(i.ItemID) AS PdfFilename,
		ic.CreationDate
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t WITH (NOLOCK)ON it.TitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND b.BookID = c.ItemID
WHERE	ic.CollectionID = @CollectionID
ORDER BY
		t.FullTitle, it.ItemSequence

END


GO
