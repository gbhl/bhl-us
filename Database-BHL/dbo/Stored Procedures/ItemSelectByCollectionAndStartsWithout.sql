CREATE PROCEDURE [dbo].[ItemSelectByCollectionAndStartsWithout]

@CollectionID int,
@StartsWith nvarchar(255)

AS

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		i.ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(i.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE i.Year END AS [Year],
		t.EditionStatement,
		i.Volume,
		i.ExternalUrl,
		c.ItemContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.ItemCollection ic WITH (NOLOCK)
			ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Title t WITH (NOLOCK)
			ON i.PrimaryTitleID = t.TitleID
			AND t.SortTitle NOT LIKE @StartsWith + '%'
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK)
			ON i.ItemID = ti.ItemID
			AND ti.TitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) 
			ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	ic.CollectionID = @CollectionID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
ORDER BY
		t.SortTitle, ti.ItemSequence

END
