SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleSelectByNameNotLike]

@Name varchar(1000)

AS

SET NOCOUNT ON

SELECT DISTINCT
		t.[TitleID],
		b.BooKID AS ItemID,
		t.[FullTitle],
		t.[SortTitle],
		ISNULL(t.[PartNumber], '') AS PartNumber,
		ISNULL(t.[PartName], '') AS PartName,
		t.[PublicationDetails],
		CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.StartYear END AS [Year],
		t.EditionStatement,
		b.Volume,
		b.ExternalUrl,
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections
FROM	dbo.Title t
		INNER JOIN (
			-- Get the first item for each title
			SELECT	TitleID, MIN(ItemSequence) MinSeq
			FROM	dbo.ItemTitle it WITH (NOLOCK) 
					INNER JOIN dbo.Item itm WITH (NOLOCK) ON it.ItemID = itm.ItemID 
			WHERE	itm.ItemStatusID = 40
			GROUP BY TitleID
			) AS x 
			ON t.TitleID = x.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON x.TitleID = it.TitleID AND x.MinSeq = it.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady=1 
AND		t.SortTitle NOT LIKE @Name + '%'
ORDER BY 
		t.SortTitle


GO
