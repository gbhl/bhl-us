SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleSelectByDateRange]

@StartDate	int,
@EndDate int

AS

SET NOCOUNT ON

-- Get initial list
SELECT DISTINCT 
		T.[TitleID],
		b.BookID AS [ItemID],
		it.[ItemSequence],
		T.[FullTitle],
		T.[SortTitle],
		T.[PartNumber],
		T.[PartName],
		T.[PublicationDetails],
		CASE WHEN ISNULL(b.StartYear, '') = '' THEN CONVERT(nvarchar(20), T.StartYear) ELSE b.StartYear END AS [Year],
		T.[EditionStatement],
		b.[Volume],
		b.[ExternalUrl],
		T.[StartYear],
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(T.TitleID, IT.ItemID) AS Collections
INTO	#tmpTitle
FROM	[dbo].[Title] T WITH (NOLOCK)
		INNER JOIN dbo.ItemTitle IT WITH (NOLOCK) ON T.TitleID = IT.TitleID
		INNER JOIN [dbo].[Item] I WITH (NOLOCK) ON [IT].[ItemID] = [I].[ItemID]
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON T.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON I.ItemID = il.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	T.StartYear BETWEEN @StartDate AND @EndDate 
AND		T.PublishReady=1

-- Get specific items to return (first volume of title that is within the specified date range
SELECT	TitleID, MIN(ItemSequence) AS ItemSequence
INTO	#tmpReturn
FROM	#tmpTitle
GROUP BY
		TitleID

SELECT	t.[TitleID],
		t.[ItemID],
		t.[FullTitle],
		t.[SortTitle],
		t.[PartNumber],
		t.[PartName],
		t.[PublicationDetails],
		t.[EditionStatement],
		t.[Volume],
		t.[ExternalUrl],
		t.[StartYear],
		t.[Year],
		t.InstitutionName,
		t.Authors,
		t.Collections
FROM	#tmpTitle t INNER JOIN #tmpReturn r
			ON t.TitleID = r.TitleID
			AND	t.ItemSequence = r.ItemSequence
ORDER BY
		t.SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectByDateRange. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
