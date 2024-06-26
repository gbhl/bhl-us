SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectByCollectionAndStartsWithout]

@CollectionID int,
@StartsWith nvarchar(255)

AS

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		b.BookID AS ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.StartYear END AS [Year],
		t.EditionStatement,
		b.Volume,
		b.ExternalUrl,
		c.ItemContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) on i.ItemID = b.ItemID
		INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON i.ItemID = ic.ItemID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t WITH (NOLOCK) ON it.TitleID = t.TitleID
			AND t.SortTitle NOT LIKE @StartsWith + '%'
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) 
			ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	ic.CollectionID = @CollectionID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
ORDER BY
		t.SortTitle, it.ItemSequence

END


GO
