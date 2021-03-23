CREATE PROCEDURE [dbo].[TitleSelectByKeywordPaged]

@Keyword nvarchar(50),
@PageNum int = 1,
@NumRows int = 100,
@SortColumn nvarchar(150) = 'title',
@TotalTitles int OUTPUT

AS

BEGIN

SET NOCOUNT ON

-- Get the total number of titles for the keyword
SELECT @TotalTitles = COUNT(DISTINCT t.TitleID)
FROM	dbo.Title t
		INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
WHERE	k.Keyword = @Keyword
AND		t.PublishReady=1

CREATE TABLE #Title (TitleID int NOT NULL)

-- Get page of titles tied directly to the specified keyword
IF (@SortColumn = 'title')
BEGIN
	INSERT		#Title
	SELECT		x.TitleID
	FROM		(
				SELECT DISTINCT t.TitleID, t.SortTitle
				FROM	dbo.Title t
						INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
						INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
				WHERE	k.Keyword = @Keyword
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
				FROM	dbo.Title t
						INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
						INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
						INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID
				WHERE	k.Keyword = @Keyword
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
				FROM	dbo.Title t
						INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
						INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
				WHERE	k.Keyword = @Keyword
				AND		t.PublishReady=1
				) x
	ORDER BY	x.StartYear, x.SortTitle
	OFFSET		@NumRows * (@PageNum - 1) ROWS
	FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)
END

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
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections
INTO	#Final
FROM	#Title tmp INNER JOIN (
				-- Get the first item for each title
				SELECT	t.TitleID, MIN(ItemSequence) MinSeq
				FROM	#Title t 
						INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON t.TitleID = it.TitleID
						INNER JOIN dbo.Item itm WITH (NOLOCK) ON it.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY t.TitleID
				) AS x 
				ON tmp.TitleID = x.TitleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON x.TitleID = it.TitleID AND x.MinSeq = it.ItemSequence
		INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON tmp.TitleID = c.TitleID AND b.BookID = c.ItemID

IF (@SortColumn = 'title') SELECT * FROM #Final ORDER BY SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'author') SELECT * FROM #Final ORDER BY Authors, SortTitle OPTION (RECOMPILE)
IF (@SortColumn = 'year') SELECT * FROM #Final ORDER BY [Year], SortTitle OPTION (RECOMPILE)

END
