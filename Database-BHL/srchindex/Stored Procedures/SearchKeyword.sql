CREATE PROCEDURE srchindex.SearchKeyword

@Keyword NVARCHAR(300),
@StartPage int = 1,
@PageSize int = 10

AS 

BEGIN

SET NOCOUNT ON

-- Raise an error if the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	RAISERROR('Search Catalog offline.', 16, 1)
	RETURN 9 -- error occurred
END

BEGIN TRY

	DECLARE @SearchCondition nvarchar(4000)

	-- Transform the search term into a full-text search phrase
	SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@Keyword)

	-- Perform the keyword search
	SELECT	cat.KeywordID
	INTO	#tmpID
	FROM	CONTAINSTABLE(SearchCatalogKeyword, Keyword, @SearchCondition) x
			INNER JOIN SearchCatalogKeyword cat ON cat.SearchCatalogKeywordID = x.[KEY]

	-- Limit to only those keywords that are related to active titles/segments
	SELECT	k.KeywordID, k.Keyword
	INTO	#tmpDetail
	FROM	#tmpID tmp
			INNER JOIN dbo.Keyword k ON tmp.KeywordID = k.KeywordID
			INNER JOIN dbo.TitleKeyword tk ON k.KeywordID = tk.KeywordID
			INNER JOIN dbo.Title t ON tk.TitleID = t.TitleID
	WHERE	t.PublishReady = 1
	UNION
	SELECT	k.KeywordID, k.Keyword
	FROM	#tmpID tmp
			INNER JOIN dbo.Keyword k ON tmp.KeywordID = k.KeywordID
			INNER JOIN dbo.SegmentKeyword sk ON k.KeywordID = sk.KeywordID
			INNER JOIN dbo.Segment s ON sk.SegmentID = s.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20)

	DECLARE @TotalHits int

	SELECT @TotalHits = COUNT(*) FROM #tmpDetail

	SELECT	@TotalHits AS TotalHits,
			KeywordID,
			Keyword
	FROM	#tmpDetail
	ORDER BY Keyword
	OFFSET	@PageSize * (@StartPage - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY

END TRY
BEGIN CATCH
	DECLARE @ErrMsg nvarchar(350)
	SET @ErrMsg = 'Error searching keywords for ' + @Keyword + '.'
	RAISERROR(@ErrMsg, 16, 1)
	RETURN 9 -- error occurred
END CATCH

END
