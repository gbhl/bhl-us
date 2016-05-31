CREATE PROCEDURE [dbo].[TitleSelectByCollectionAndStartsWith]

@CollectionID int,
@StartsWith varchar(1000)

AS

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		itm.ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(itm.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE itm.Year END AS [Year],
		t.EditionStatement,
		itm.Volume,
		itm.ExternalUrl,
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, itm.ItemID) AS Collections
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleCollection tc WITH (NOLOCK)
			ON t.TitleID = tc.TitleID
		INNER JOIN (
				-- Get the first item for each title
				SELECT	TitleID, MIN(ItemSequence) MinSeq
				FROM	dbo.TitleItem ti WITH (NOLOCK) INNER JOIN dbo.Item itm WITH (NOLOCK)
						ON ti.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON x.TitleID = ti.TitleID AND x.MinSeq = ti.ItemSequence
		INNER JOIN dbo.Item itm WITH (NOLOCK) ON ti.ItemID = itm.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND itm.ItemID = c.ItemID
WHERE	tc.CollectionID = @CollectionID
AND		t.SortTitle LIKE @StartsWith + '%'
AND		t.PublishReady = 1
ORDER BY
		t.SortTitle

END
