CREATE PROCEDURE [dbo].[TitleSelectByNameLike]

@Name varchar(1000)

AS

SET NOCOUNT ON

SELECT DISTINCT
		t.[TitleID],
		itm.ItemID,
		t.[FullTitle],
		t.[SortTitle],
		ISNULL(t.[PartNumber], '') AS PartNumber,
		ISNULL(t.[PartName], '') AS PartName,
		t.[PublicationDetails],
		CASE WHEN ISNULL(itm.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE itm.Year END AS [Year],
		t.EditionStatement,
		itm.Volume,
		itm.ExternalUrl,
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, itm.ItemID) AS Collections
FROM	[dbo].[Title] t WITH (NOLOCK)
		LEFT OUTER JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID
		INNER JOIN (
				-- Get the first item for each title
				SELECT	TitleID, MIN(ItemSequence) MinSeq
				FROM	dbo.TitleItem ti WITH (NOLOCK) INNER JOIN dbo.Item itm  WITH (NOLOCK)
						ON ti.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON x.TitleID = ti.TitleID AND x.MinSeq = ti.ItemSequence
		INNER JOIN dbo.Item itm WITH (NOLOCK) ON ti.ItemID = itm.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID  AND itm.ItemID = c.ItemID
WHERE	t.PublishReady=1 
AND		t.SortTitle LIKE @Name + '%'
ORDER BY t.SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectByNameLike. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
