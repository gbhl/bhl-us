CREATE PROCEDURE srchindex.SearchAuthor

@AuthorName nvarchar(300),
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
	SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@AuthorName)

	-- Get the matching authors
	SELECT	c.CreatorID
	INTO	#tmpID
	FROM	CONTAINSTABLE(SearchCatalogCreator, CreatorName, @SearchCondition) x
			INNER JOIN SearchCatalogCreator c ON c.SearchCatalogCreatorID = x.[KEY]
		
	SELECT	v.AuthorID,
			v.FullName,
			v.Unit,
			v.Title,
			v.Location,
			v.FullerForm
	INTO	#tmpDetail
	FROM	#tmpID t
			INNER JOIN dbo.TitleAuthorView v ON t.CreatorID = v.AuthorID
	WHERE	v.PublishReady = 1
	AND		v.IsActive = 1
	AND		v.IsPreferredName = 1
	UNION
	SELECT	a.AuthorID, 
			n.FullName, 
			a.Unit, 
			a.Title, 
			a.Location,
			n.FullerForm
	FROM	#tmpID t
			INNER JOIN dbo.SegmentAuthor sa ON t.CreatorID = sa.AuthorID
			INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
			INNER JOIN dbo.Author a ON sa.AuthorID = a.AuthorID
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	s.SegmentStatusID IN (10, 20)
	AND		a.IsActive = 1
	AND		n.IsPreferredName = 1

	DECLARE @TotalHits int

	SELECT @TotalHits = COUNT(*) FROM #tmpDetail

	SELECT	@TotalHits AS TotalHits,
			AuthorID, 
			--dbo.fnAuthorSearchStringForAuthor(AuthorID, '|') AS AuthorNames,
			LTRIM(RTRIM(FullName + ' ' +
                ISNULL(NULLIF(FullerForm + ' ', ' '), '') +
                ISNULL(NULLIF(Title + ' ', ' '), '') +
                ISNULL(NULLIF(Unit + ' ', ' '), '') +
                ISNULL(NULLIF(Location + ' ', ' '), ''))) AS PrimaryAuthorName
	FROM	#tmpDetail
	ORDER BY FullName, Unit, Title, Location, FullerForm
	OFFSET	@PageSize * (@StartPage - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY

END TRY
BEGIN CATCH
	DECLARE @ErrMsg nvarchar(350)
	SET @ErrMsg = 'Error searching authors for ' + @AuthorName + '.'
	RAISERROR(@ErrMsg, 16, 1)
	RETURN 9 -- error occurred
END CATCH

END
