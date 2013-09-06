
CREATE PROCEDURE [dbo].[TitleSelectByDateRangeAndInstitution]

@StartDate	int,
@EndDate int,
@InstitutionCode nvarchar(10) = '',
@LanguageCode nvarchar(10) = ''

AS

SET NOCOUNT ON

-- Get initial list
SELECT DISTINCT 
		T.[TitleID],
		IT.[ItemID],
		TI.[ItemSequence],
		T.[FullTitle],
		T.[SortTitle],
		T.[PartNumber],
		T.[PartName],
		T.[PublicationDetails],
		CASE WHEN ISNULL(IT.Year, '') = '' THEN CONVERT(nvarchar(20), T.StartYear) ELSE IT.Year END AS [Year],
		T.[EditionStatement],
		IT.[Volume],
		IT.[ExternalUrl],
		T.[StartYear],
		I.InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(T.TitleID, IT.ItemID) AS Collections
INTO	#tmpTitle
FROM	[dbo].[Title] T WITH (NOLOCK)
		LEFT OUTER JOIN Institution I WITH (NOLOCK) ON I.InstitutionCode = T.InstitutionCode
		INNER JOIN dbo.TitleItem TI WITH (NOLOCK) ON T.TitleID = TI.TitleID
		INNER JOIN [dbo].[Item] IT WITH (NOLOCK) ON [TI].[ItemID] = [IT].[ItemID]
		LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON T.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON IT.ItemID = il.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID AND it.ItemID = c.ItemID
WHERE	T.StartYear BETWEEN @StartDate AND @EndDate 
AND		T.PublishReady=1
AND		(T.InstitutionCode = @InstitutionCode OR 
		IT.InstitutionCode = @InstitutionCode OR 
		@InstitutionCode = '')
AND		(T.LanguageCode = @LanguageCode OR
		IT.LanguageCode = @LanguageCode OR
		 ISNULL(tl.LanguageCode, '') = @LanguageCode OR
		 ISNULL(il.LanguageCode, '') = @LanguageCode OR
		@LanguageCode = '')

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
	RAISERROR('An error occurred in procedure TitleSelectByDateRangeAndInstitution. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



