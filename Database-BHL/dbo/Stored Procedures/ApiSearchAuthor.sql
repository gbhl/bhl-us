
CREATE PROCEDURE [dbo].[ApiSearchAuthor]

@AuthorName nvarchar(4000)

AS 

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.ApiAuthorSelectNameStartsWith @AuthorName
	RETURN
END

-- Transform the search term into a full-text search phrase
DECLARE @SearchCondition nvarchar(4000)
SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@AuthorName)

SELECT DISTINCT
		a.AuthorID ,
		n.FullName ,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		n.FullerForm,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates
FROM	CONTAINSTABLE(SearchCatalogCreator, CreatorName, @SearchCondition) x
		INNER JOIN SearchCatalogCreator cat ON cat.SearchCatalogCreatorID = x.[KEY]
		INNER JOIN dbo.Author a on cat.CreatorID = a.AuthorID
		INNER JOIN dbo.TitleAuthor ta ON a.AuthorID = ta.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID AND t.PublishReady = 1
WHERE	a.IsActive = 1
ORDER BY n.FullName


