CREATE PROCEDURE [dbo].[TitleSelectByDateRange]

@StartDate	int,
@EndDate int,
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalTitles int OUTPUT

AS

SET NOCOUNT ON

-- Get the total number of titles
SELECT	@TotalTitles = COUNT(DISTINCT t.TitleID)
FROM	[dbo].[Title] t WITH (NOLOCK)
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID
WHERE	t.StartYear BETWEEN @StartDate AND @EndDate 
AND		t.PublishReady=1

CREATE TABLE #Title (TitleID int NOT NULL)

-- Get the Title IDs for the requested "page" of data
IF (@SortColumn = 'title')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle
				FROM	[dbo].[Title] t WITH (NOLOCK)
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID
				WHERE	t.StartYear BETWEEN @StartDate AND @EndDate 
				AND		t.PublishReady=1
				) x
	ORDER BY	x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'author')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle, c.Authors
				FROM	[dbo].[Title] t WITH (NOLOCK)
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID
				WHERE	t.StartYear BETWEEN @StartDate AND @EndDate 
				AND		t.PublishReady=1
				) x
	ORDER BY	x.Authors, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END
IF (@SortColumn = 'year')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle, t.StartYear
				FROM	[dbo].[Title] t WITH (NOLOCK)
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID
				WHERE	t.StartYear BETWEEN @StartDate AND @EndDate 
				AND		t.PublishReady=1
				) x
	ORDER BY	x.StartYear, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

-- Get initial list
SELECT DISTINCT 
		t.[TitleID],
		b.BookID AS [ItemID],
		it.[ItemSequence],
		t.[FullTitle],
		t.[SortTitle],
		t.[PartNumber],
		t.[PartName],
		t.[PublicationDetails],
		CONVERT(nvarchar(10), t.StartYear) AS [Year],
		t.[EditionStatement],
		b.[Volume],
		b.[ExternalUrl],
		c.TitleContributors AS InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(T.TitleID, IT.ItemID) AS Collections
INTO	#Final
FROM	#Title tmp
		INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN (
				-- Get the first item for each title
				SELECT	tmp.TitleID, MIN(ItemSequence) MinSeq
				FROM	#Title tmp
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON tmp.TitleID = it.TitleID
						INNER JOIN dbo.Item itm WITH (NOLOCK) ON it.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY tmp.TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON x.TitleID = it.TitleID AND x.MinSeq = it.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON [it].[ItemID] = [i].[ItemID]
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) on t.TitleID = c.TitleID AND b.BookID = c.ItemID

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY [Year], SortTitle OPTION (RECOMPILE)

GO
