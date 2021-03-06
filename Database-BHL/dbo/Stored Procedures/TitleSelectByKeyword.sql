SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleSelectByKeyword]

@Keyword nvarchar(50)

AS

SET NOCOUNT ON

-- Get titles tied directly to the specified TagText
SELECT DISTINCT 
		v.TitleID,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.PublicationDetails,
		t.StartYear,
		t.EditionStatement
INTO	#tmpTitle
FROM	dbo.TitleKeywordView v 
		INNER JOIN dbo.Title t ON v.TitleID = t.TitleID
		LEFT JOIN dbo.TitleLanguage tl  ON v.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il ON v.ItemID = il.ItemID
WHERE	v.Keyword = @Keyword
AND		t.PublishReady=1
ORDER BY SortTitle

-- Add supporting information for each title to the result set
SELECT	t.TitleID,
		b.BookID AS ItemID,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.StartYear END AS [Year],
		t.EditionStatement,
		b.Volume,
		b.ExternalUrl,
		c.TitleContributors AS InstitutionName,
		c.Subjects,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections--,
--		dbo.fnSeriesStringForTitle (t.TitleID) AS Associations
FROM	#tmpTitle t INNER JOIN (
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
ORDER BY 
		t.SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectByKeyword. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
