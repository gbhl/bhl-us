CREATE PROCEDURE srchindex.SearchName

@Name NVARCHAR(100),
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

	SET NOCOUNT ON

	SELECT	r.NameResolvedID,
			r.ResolvedNameString
	INTO	#tmpNameResolved
	FROM	dbo.NameResolved r WITH (NOLOCK)
	WHERE	r.ResolvedNameString LIKE LTRIM(RTRIM(@Name)) + '%'

    SELECT	r.NameResolvedID,
			r.ResolvedNameString,
			COUNT(np.NamePageID) AS NameCount
	INTO	#tmpName
    FROM	#tmpNameResolved r WITH (NOLOCK)
			INNER JOIN dbo.Name n WITH (NOLOCK) ON r.NameResolvedID = n.NameResolvedID
			INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
	WHERE	n.IsActive = 1
	AND		p.Active = 1
	AND		i.ItemStatusID = 40
    GROUP BY r.NameResolvedID,
            r.ResolvedNameString

	DECLARE @TotalHits int

	SELECT @TotalHits = COUNT(*) FROM #tmpName

    SELECT	@TotalHits AS TotalHits,
			NameResolvedID,
			ResolvedNameString,
			NameCount
    FROM	#tmpName
    ORDER BY ResolvedNameString
	OFFSET	@PageSize * (@StartPage - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY

END TRY
BEGIN CATCH
	DECLARE @ErrMsg nvarchar(350)
	SET @ErrMsg = 'Error searching names for ' + @Name + '.'
	RAISERROR(@ErrMsg, 16, 1)
	RETURN 9 -- error occurred
END CATCH

END
