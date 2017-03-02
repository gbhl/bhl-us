CREATE PROCEDURE [dbo].[SearchAuthorComplete]

@AuthorName nvarchar(300)

AS 
/*
 * This rows returned by this procedure differ from the rows returned by
 * the SearchAuthor procedure.  SearchAuthor is used for the public-facing
 * site search, and only returns active authors associated with one or more
 * books.  This procedure is used for admin functionality, and returns all
 * authors.
 */
BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpAuthor
	(
	AuthorID int NOT NULL,
	RedirectAuthorID int NULL,
	AuthorNameID int NOT NULL,
	FullName nvarchar(300) NOT NULL,
	Numeration nvarchar(300) NOT NULL,
	Unit nvarchar(300) NOT NULL,
	Title nvarchar(200) NOT NULL,
	Location nvarchar(200) NOT NULL,
	Dates nvarchar(60) NOT NULL,
	FullerForm nvarchar(150) NOT NULL,
	StartDate nvarchar(25) NOT NULL,
	EndDate nvarchar(25) NOT NULL,
	IsActive smallint NOT NULL
	)	

-- Get current status of the search catalog (Status 0 = offline)
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus

-- Only try the search catalog if it is online
IF (@CatalogStatus <> 0)
BEGIN
	-- Transform the search term into a full-text search phrase
	DECLARE @SearchCondition nvarchar(4000)
	SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@AuthorName)

	INSERT #tmpAuthor
	SELECT DISTINCT TOP (500)
			a.AuthorID,
			a.RedirectAuthorID,
			n.AuthorNameID,
			n.FullName,
			a.Numeration,
			a.Unit,
			a.Title,
			a.Location,
			a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
			n.FullerForm,
			a.StartDate,
			a.EndDate,
			a.IsActive
	FROM	CONTAINSTABLE(SearchCatalogCreator, CreatorName, @SearchCondition) x
			INNER JOIN SearchCatalogCreator c ON c.SearchCatalogCreatorID = x.[KEY]
			INNER JOIN dbo.Author a ON c.CreatorID = a.AuthorID
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	n.IsPreferredName = 1
	ORDER BY 
			n.FullName, a.Numeration, a.Unit, a.Title, 
			a.Location, a.StartDate, a.EndDate, n.FullerForm
END

-- Now try searching without the catalog.  Newly added authors may not be in the catalog,
-- so that's why we may pick up additional rows by looking at the author tables directly.
SELECT DISTINCT
		AuthorID
INTO	#tmpAuthorName
FROM	dbo.AuthorName
WHERE	FullName LIKE (@AuthorName + '%')

INSERT #tmpAuthor
SELECT DISTINCT TOP (500)
		a.AuthorID,
		a.RedirectAuthorID,
		n.AuthorNameID,
		n.FullName,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
		n.FullerForm,
		a.StartDate,
		a.Enddate,
		a.IsActive
FROM	dbo.Author a 
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		LEFT JOIN #tmpAuthor t ON a.AuthorID = t.AuthorID AND n.AuthorNameID = t.AuthorNameID
WHERE	a.AuthorID IN (SELECT AuthorID FROM #tmpAuthorName)
AND		n.IsPreferredName = 1
AND		t.AuthorID IS NULL
ORDER BY n.FullName

-- Return final result set
SELECT * FROM #tmpAuthor

END
